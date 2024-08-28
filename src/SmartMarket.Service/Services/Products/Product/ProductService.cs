using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Products.Product;

namespace SmartMarket.Service.Services.Products.Product;

public class ProductService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddProductDto> validator) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddProductDto> _validator = validator;

    public async Task<bool> AddAsync(AddProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var product = _mapper.Map<Et.Product>(dto);
        return await _unitOfWork.Product.Add(product);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var product = await _unitOfWork.Product.GetById(Id);
        if (product == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
        }

        return await _unitOfWork.Product.Remove(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _unitOfWork.Product.GetProductsFullInformationAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<bool> UpdateAsync(AddProductDto dto, Guid Id)
    {
        var product = await _unitOfWork.Product.GetById(Id);
        if (product == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
        }

        _mapper.Map(dto, product);

        return await _unitOfWork.Product.Update(product);
    }
}