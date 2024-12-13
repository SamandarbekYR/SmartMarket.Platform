﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarket.Service.Interfaces.Common;
using SmartMarket.Service.Interfaces.Products.ProductImage;
using Microsoft.Extensions.Logging; // Import ILogger

namespace SmartMarket.Service.Services.Products.ProductImage
{
    public class ProductImageService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddProductImageDto> validator,
                                 IFileService fileService,
                                 ILogger<ProductImageService> logger) : IProductImageService // Inject ILogger
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductImageDto> _validator = validator;
        private readonly IFileService _fileService = fileService;
        private readonly ILogger<ProductImageService> _logger = logger;
        private const string ROOT_PATH = "ProductImages";

        public async Task<bool> AddAsync(AddProductImageDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                var imagePath = await _fileService.UploadImageAsync(dto.ImagePath, ROOT_PATH);

                var productImage = _mapper.Map<Et.ProductImage>(dto);
                productImage.ImagePath = imagePath;

                return await _unitOfWork.ProductImage.Add(productImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product image.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var productImage = await _unitOfWork.ProductImage.GetById(Id);
                if (productImage == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Image not found.");
                }

                var deleteResult = await _fileService.DeleteImageAsync(productImage.ImagePath);
                if (!deleteResult)
                {
                    throw new StatusCodeException(HttpStatusCode.InternalServerError, "Image deletion failed.");
                }

                return await _unitOfWork.ProductImage.Remove(productImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a product image.");
                throw;
            }
        }

        public async Task<List<ProductImageDto>> GetAllAsync()
        {
            try
            {
                var productImages = await _unitOfWork.ProductImage.GetProductImagesFullInformationAsync();
                return _mapper.Map<List<ProductImageDto>>(productImages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all product images.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddProductImageDto dto, Guid Id)
        {
            try
            {
                var productImage = await _unitOfWork.ProductImage.GetById(Id);
                if (productImage == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Image not found.");
                }

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                var deleteResult = await _fileService.DeleteImageAsync(productImage.ImagePath);
                if (!deleteResult)
                {
                    throw new StatusCodeException(HttpStatusCode.InternalServerError, "Image deletion failed.");
                }

                var newImagePath = await _fileService.UploadImageAsync(dto.ImagePath, ROOT_PATH);

                _mapper.Map(dto, productImage);
                productImage.ImagePath = newImagePath;

                return await _unitOfWork.ProductImage.Update(productImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a product image.");
                throw;
            }
        }
    }
}