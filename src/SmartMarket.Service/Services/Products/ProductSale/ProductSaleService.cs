﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.Interfaces.Products.ProductSale;

namespace SmartMarket.Service.Services.Products.ProductSale;

public class ProductSaleService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddProductSaleDto> validator) : IProductSaleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddProductSaleDto> _validator = validator;

    public async Task<bool> AddAsync(AddProductSaleDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var productSale = _mapper.Map<Et.ProductSale>(dto);
        return await _unitOfWork.ProductSale.Add(productSale);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var productSale = await _unitOfWork.ProductSale.GetById(Id);
        if (productSale == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
        }

        return await _unitOfWork.ProductSale.Remove(productSale);
    }

    public async Task<List<ProductSaleDto>> GetAllAsync()
    {
        var productSales = await _unitOfWork.ProductSale.GetAll().ToListAsync();
        return _mapper.Map<List<ProductSaleDto>>(productSales);
    }

    public async Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id)
    {
        var productSale = await _unitOfWork.ProductSale.GetById(Id);
        if (productSale == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
        }

        _mapper.Map(dto, productSale);

        return await _unitOfWork.ProductSale.Update(productSale);
    }
}