using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Orders;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.Interfaces.Order;
using System.Net;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.Common.Extentions;
using Microsoft.Extensions.Logging;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.Service.Services.Order
{
    public class OrderService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddOrderDto> validator,
                             ILogger<OrderService> logger) : IOrderService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddOrderDto> _validator = validator;
        private readonly ILogger<OrderService> _logger = logger; 

        public async Task<bool> AddAsync(AddOrderDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var partnerExists = await _unitOfWork.Partner.GetById(dto.PartnerId) != null;
                if (!partnerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                var order = _mapper.Map<Et.Order>(dto);

                int count = 0;
                foreach (var productItem in order.ProductOrderItems)
                {
                    var product = await _unitOfWork.Product.GetById(productItem.ProductId);
                    if (product == null)
                    {
                        throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                    }

                    if (product.Count < productItem.Count)
                    {
                        throw new StatusCodeException(HttpStatusCode.BadRequest, "Insufficient product count.");
                    }

                    product.Count -= productItem.Count;

                    var result = await _unitOfWork.Product.Update(product);
                    if (!result)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Error updating product count.");
                }

                return await _unitOfWork.Order.Add(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding an order.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var order = await _unitOfWork.Order.GetById(Id);
                if (order == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found.");
                }

                return await _unitOfWork.Order.Remove(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting an order.");
                throw;
            }
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            try
            {
                var orders = await _unitOfWork.Order.GetOrdersFullInformationAsync();
                return _mapper.Map<List<OrderDto>>(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all orders.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddOrderDto dto, Guid Id)
        {
            try
            {
                var order = await _unitOfWork.Order.GetById(Id);
                if (order == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var partnerExists = await _unitOfWork.Partner.GetById(dto.PartnerId) != null;
                if (!partnerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                _mapper.Map(dto, order);

                return await _unitOfWork.Order.Update(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an order.");
                throw;
            }
        }
    }
}