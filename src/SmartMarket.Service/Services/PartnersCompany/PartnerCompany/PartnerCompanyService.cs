using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarket.Service.Interfaces.PartnersCompany.PartnerCompany;

namespace SmartMarket.Service.Services.PartnersCompany.PartnerCompany;

public class PartnerCompanyService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddPartnerCompanyDto> validator) : IPartnerCompanyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddPartnerCompanyDto> _validator = validator;

    public async Task<bool> AddAsync(AddPartnerCompanyDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var partnerCompany = _mapper.Map<Et.PartnerCompany>(dto);
        return await _unitOfWork.PartnerCompany.Add(partnerCompany);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var partnerCompany = await _unitOfWork.PartnerCompany.GetById(Id);
        if (partnerCompany == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Partner Company not found.");
        }

        return await _unitOfWork.PartnerCompany.Remove(partnerCompany);
    }

    public async Task<List<PartnerCompanyDto>> GetAllAsync()
    {
        var partnerCompanies = await _unitOfWork.PartnerCompany.GetAll().ToListAsync();
        return _mapper.Map<List<PartnerCompanyDto>>(partnerCompanies);
    }

    public async Task<bool> UpdateAsync(AddPartnerCompanyDto dto, Guid Id)
    {
        var partnerCompany = await _unitOfWork.PartnerCompany.GetById(Id);
        if (partnerCompany == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Partner Company not found.");
        }

        _mapper.Map(dto, partnerCompany);

        return await _unitOfWork.PartnerCompany.Update(partnerCompany);
    }
}