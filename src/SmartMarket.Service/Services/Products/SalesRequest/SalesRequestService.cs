﻿using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.Interfaces.Products.SalesRequest;

using System.Net;

using SR = SmartMarket.Domain.Entities.Products;

namespace SmartMarket.Service.Services.Products.SalesRequest
{
    public class SalesRequestService(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                IValidator<AddSalesRequestDto> validator,
                                ILogger<SalesRequestService> logger) : ISalesRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddSalesRequestDto> _validator = validator;
        private readonly ILogger<SalesRequestService> _logger = logger;

        public async Task<bool> AddAsync(AddSalesRequestDto dto)
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

                var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
                if (!payDeskExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                var salesRequest = _mapper.Map<SR.SalesRequest>(dto);
                salesRequest.CreatedDate = DateTime.UtcNow;
               
                int count = 0;
                foreach (var productItem in salesRequest.ProductSaleItems)
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

                return await _unitOfWork.SalesRequest.Add(salesRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding SalesRequest");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var salesRequest = await _unitOfWork.SalesRequest.GetById(Id);
                if (salesRequest == null)
                {
                    _logger.LogError($"SalesRequest with Id: {Id} not found");
                    return false;
                }

                return await _unitOfWork.SalesRequest.Remove(salesRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting SalesRequest with Id: {Id}");
                return false;
            }
        }

        public async Task<List<SalesRequestDto>> GetAllAsync()
        {
            try
            {
                var salesRequests = await _unitOfWork.SalesRequest.GetSalesRequestsFullInformationAsync();
                return _mapper.Map<List<SalesRequestDto>>(salesRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all SalesRequests");
                return new List<SalesRequestDto>();
            }
        }

        public async Task<List<SalesRequestDto>> FilterProductSaleAsync(FilterSalesRequestDto dto)
        {
            try
            {
                var salesRequests = await _unitOfWork.SalesRequest.GetSalesRequestsFullInformationAsync();

                if (dto.FromDateTime.HasValue && dto.ToDateTime.HasValue)
                {
                    salesRequests = salesRequests.Where(
                        ps => ps.CreatedDate >= dto.FromDateTime.Value
                        && ps.CreatedDate <= dto.ToDateTime.Value).ToList();
                }
                else
                {
                    salesRequests = salesRequests.Where(
                        ps => ps.CreatedDate.Value.Date == DateTime.Today).ToList();
                }

                if (!string.IsNullOrWhiteSpace(dto.ProductName))
                {
                    salesRequests = salesRequests
                        .Where(ps => ps.ProductSaleItems
                            .Any(item => item.Product.Name.ToLower().Contains(dto.ProductName.ToLower())))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(dto.WorkerName))
                {
                    salesRequests = salesRequests.Where(
                        ps => ps.Worker.FirstName.Contains(dto.WorkerName)).ToList();
                }

                var result = _mapper.Map<List<SalesRequestDto>>(salesRequests);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering SalesRequests");
                return new List<SalesRequestDto>();
            }
        }

        public async Task<SalesRequestDto> GetByIdAsync(Guid Id)
        {
            try
            {
                var salesRequest = await _unitOfWork.SalesRequest.GetById(Id);
                if (salesRequest == null)
                {
                    _logger.LogError($"SalesRequest with Id: {Id} not found");
                    return null;
                }

                return _mapper.Map<SalesRequestDto>(salesRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting SalesRequest with Id: {Id}");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(AddSalesRequestDto dto, Guid Id)
        {
            try
            {
                var salesRequest = await _unitOfWork.SalesRequest.GetById(Id);
                if (salesRequest == null)
                {
                    _logger.LogError($"SalesRequest with Id: {Id} not found");
                    return false;
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
                if (!payDeskExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
                }

                _mapper.Map(dto, salesRequest);
                return await _unitOfWork.SalesRequest.Update(salesRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating SalesRequest with Id: {Id}");
                return false;
            }
        }
    }
}