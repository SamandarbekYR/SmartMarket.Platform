using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.ReplaceProduct;
using SmartMarket.Service.Interfaces.ReplaceProduct;
using System.Net;

namespace SmartMarket.Service.Services.ReplaceProduct;

public class ReplaceProductService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddReplaceProductDto> validator) : IReplaceProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddReplaceProductDto> _validator = validator;

    public async Task<bool> AddAsync(AddReplaceProductDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var replaceProduct = _mapper.Map<Et.ReplaceProduct>(dto);
        return await _unitOfWork.ReplaceProduct.Add(replaceProduct);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var replaceProduct = await _unitOfWork.ReplaceProduct.GetById(Id);
        if (replaceProduct == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Replace Product not found.");
        }

        return await _unitOfWork.ReplaceProduct.Remove(replaceProduct);
    }

    public async Task<List<ReplaceProductDto>> GetAllAsync()
    {
        var replaceProducts = await _unitOfWork.ReplaceProduct.GetAll().ToListAsync();
        return _mapper.Map<List<ReplaceProductDto>>(replaceProducts);
    }

    public async Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id)
    {
        var replaceProduct = await _unitOfWork.ReplaceProduct.GetById(Id);
        if (replaceProduct == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Replace Product not found.");
        }

        _mapper.Map(dto, replaceProduct);

        return await _unitOfWork.ReplaceProduct.Update(replaceProduct);
    }
}