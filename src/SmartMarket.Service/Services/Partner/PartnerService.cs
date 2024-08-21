using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.Interfaces.Partner;
using System.Net;

namespace SmartMarket.Service.Services.Partner;

public class PartnerService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddPartnerDto> validator) : IPartnerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddPartnerDto> _validator = validator;

    public async Task<bool> AddAsync(AddPartnerDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var partner = _mapper.Map<Et.Partner>(dto);
        return await _unitOfWork.Partner.Add(partner);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var partner = await _unitOfWork.Partner.GetById(Id);
        if (partner == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
        }

        return await _unitOfWork.Partner.Remove(partner);
    }

    public async Task<List<PartnerDto>> GetAllAsync()
    {
        var partners = await _unitOfWork.Partner.GetAll().ToListAsync();
        return _mapper.Map<List<PartnerDto>>(partners);
    }

    public async Task<bool> UpdateAsync(AddPartnerDto dto, Guid Id)
    {
        var partner = await _unitOfWork.Partner.GetById(Id);
        if (partner == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
        }

        _mapper.Map(dto, partner);

        return await _unitOfWork.Partner.Update(partner);
    }
}