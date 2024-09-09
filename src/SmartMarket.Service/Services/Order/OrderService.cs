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

namespace SmartMarket.Service.Services.Order
{
    public class OrderService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddOrderDto> validator) : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddOrderDto> _validator = validator;

        public async Task<bool> AddAsync(AddOrderDto dto)
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
            
            var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
            if (!productExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            var order = _mapper.Map<Et.Order>(dto);
            return await _unitOfWork.Order.Add(order);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var order = await _unitOfWork.Order.GetById(Id);
            if (order == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found.");
            }

            return await _unitOfWork.Order.Remove(order);
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.Order.GetOrdersFullInformationAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<bool> UpdateAsync(AddOrderDto dto, Guid Id)
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

            var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
            if (!productExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            _mapper.Map(dto, order);

            return await _unitOfWork.Order.Update(order);
        }
    }
}