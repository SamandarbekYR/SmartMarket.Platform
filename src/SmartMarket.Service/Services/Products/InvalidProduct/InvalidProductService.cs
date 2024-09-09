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

namespace SmartMarket.Service.Services.Products.InvalidProduct
{
    public class InvalidProductService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddInvalidProductDto> validator) : IInvalidProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddInvalidProductDto> _validator = validator;

        public async Task<bool> AddAsync(AddInvalidProductDto dto)
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

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var invalidProduct = await _unitOfWork.InvalidProduct.GetById(Id);
            if (invalidProduct == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Invalid Product not found.");
            }

            return await _unitOfWork.InvalidProduct.Remove(invalidProduct);
        }

        public async Task<List<InvalidProductDto>> GetAllAsync()
        {
            var invalidProducts = await _unitOfWork.InvalidProduct.GetInvalidProductsFullInformationAsync();
            return _mapper.Map<List<InvalidProductDto>>(invalidProducts);
        }

        public async Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id)
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
    }
}