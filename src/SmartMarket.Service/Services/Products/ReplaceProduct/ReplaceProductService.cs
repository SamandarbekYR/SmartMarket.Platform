using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.Interfaces.Products.ReplaceProduct;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Products.ReplaceProduct
{
    public class ReplaceProductService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddReplaceProductDto> validator,
                             ILogger<ReplaceProductService> logger) : IReplaceProductService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddReplaceProductDto> _validator = validator;
        private readonly ILogger<ReplaceProductService> _logger = logger; 

        public async Task<bool> AddAsync(AddReplaceProductDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var productSaleExists = await _unitOfWork.ProductSale.GetById(dto.ProductSaleId) != null;
                if (!productSaleExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var replaceProduct = _mapper.Map<Et.ReplaceProduct>(dto);
                return await _unitOfWork.ReplaceProduct.Add(replaceProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a replace product.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var replaceProduct = await _unitOfWork.ReplaceProduct.GetById(Id);
                if (replaceProduct == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Replace Product not found.");
                }

                return await _unitOfWork.ReplaceProduct.Remove(replaceProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a replace product.");
                throw;
            }
        }

        public async Task<List<ReplaceProductDto>> GetAllAsync()
        {
            try
            {
                var replaceProducts = await _unitOfWork.ReplaceProduct.GetReplaceProductsFullInformationAsync();
                return _mapper.Map<List<ReplaceProductDto>>(replaceProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all replace products.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id)
        {
            try
            {
                var replaceProduct = await _unitOfWork.ReplaceProduct.GetById(Id);
                if (replaceProduct == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Replace Product not found.");
                }

                var productSaleExists = await _unitOfWork.ProductSale.GetById(dto.ProductSaleId) != null;
                if (!productSaleExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                _mapper.Map(dto, replaceProduct);

                return await _unitOfWork.ReplaceProduct.Update(replaceProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a replace product.");
                throw;
            }
        }
    }
}