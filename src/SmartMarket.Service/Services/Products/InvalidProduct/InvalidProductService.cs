using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.Interfaces.Products.InvalidProduct;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Products.InvalidProduct
{
    public class InvalidProductService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddInvalidProductDto> validator,
                             ILogger<InvalidProductService> logger) : IInvalidProductService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddInvalidProductDto> _validator = validator;
        private readonly ILogger<InvalidProductService> _logger = logger; 

        public async Task<bool> AddAsync(AddInvalidProductDto dto)
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

                var productSaleExists = await _unitOfWork.ProductSale.GetById(dto.ProductSaleId) != null;
                if (!productSaleExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                var invalidProduct = _mapper.Map<Et.InvalidProduct>(dto);
                return await _unitOfWork.InvalidProduct.Add(invalidProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding an invalid product.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var invalidProduct = await _unitOfWork.InvalidProduct.GetById(Id);
                if (invalidProduct == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Invalid Product not found.");
                }

                return await _unitOfWork.InvalidProduct.Remove(invalidProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting an invalid product.");
                throw;
            }
        }

        public async Task<List<InvalidProductDto>> FilterInvalidProductAsync(FilterInvalidProductDto filterInvalidProductDto)
        {
            try
            {
                var invalidProducts = await _unitOfWork.InvalidProduct.GetInvalidProductsFullInformationAsync();
                if (filterInvalidProductDto.FromDateTime != null && filterInvalidProductDto.ToDateTime != null)
                {
                    invalidProducts = invalidProducts.Where(
                        ps => ps.CreatedDate >= filterInvalidProductDto.FromDateTime 
                        && ps.CreatedDate <= filterInvalidProductDto.ToDateTime)
                        .ToList();
                }

                if (!string.IsNullOrEmpty(filterInvalidProductDto.WorkerName))
                {
                    invalidProducts = invalidProducts.Where(
                        ps => ps.Worker.FirstName.ToLower().Contains(filterInvalidProductDto.WorkerName.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(filterInvalidProductDto.ProductName))
                {
                    invalidProducts = invalidProducts.Where(
                        ps => ps.ProductSale.Product.Name.ToLower().Contains(filterInvalidProductDto.ProductName.ToLower()))
                        .ToList();
                }

                return _mapper.Map<List<InvalidProductDto>>(invalidProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filtering invalid products.");
                throw;
            }
        }

        public async Task<List<InvalidProductDto>> GetAllAsync()
        {
            try
            {
                var invalidProducts = await _unitOfWork.InvalidProduct.GetInvalidProductsFullInformationAsync();
                return _mapper.Map<List<InvalidProductDto>>(invalidProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all invalid products.");
                throw;
            }
        }

        public Task<List<InvalidProductDto>> GetInvalidProductsByProductNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id)
        {
            try
            {
                var invalidProduct = await _unitOfWork.InvalidProduct.GetById(Id);
                if (invalidProduct == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Invalid Product not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var productSaleExists = await _unitOfWork.ProductSale.GetById(dto.ProductSaleId) != null;
                if (!productSaleExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                _mapper.Map(dto, invalidProduct);

                return await _unitOfWork.InvalidProduct.Update(invalidProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an invalid product.");
                throw;
            }
        }
    }
}