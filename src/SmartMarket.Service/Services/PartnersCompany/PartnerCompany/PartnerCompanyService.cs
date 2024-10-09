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
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.PartnersCompany.PartnerCompany
{
    public class PartnerCompanyService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddPartnerCompanyDto> validator,
                                 ILogger<PartnerCompanyService> logger) : IPartnerCompanyService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPartnerCompanyDto> _validator = validator;
        private readonly ILogger<PartnerCompanyService> _logger = logger;

        public async Task<bool> AddAsync(AddPartnerCompanyDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var partnerCompany = _mapper.Map<Et.PartnerCompany>(dto);
                return await _unitOfWork.PartnerCompany.Add(partnerCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a partner company.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var partnerCompany = await _unitOfWork.PartnerCompany.GetById(Id);
                if (partnerCompany == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner Company not found.");
                }

                return await _unitOfWork.PartnerCompany.Remove(partnerCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a partner company.");
                throw;
            }
        }

        public async Task<List<PartnerCompanyDto>> GetAllAsync()
        {
            try
            {
                var partnerCompanies = await _unitOfWork.PartnerCompany.GetAll().ToListAsync();
                return _mapper.Map<List<PartnerCompanyDto>>(partnerCompanies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all partner companies.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddPartnerCompanyDto dto, Guid Id)
        {
            try
            {
                var partnerCompany = await _unitOfWork.PartnerCompany.GetById(Id);
                if (partnerCompany == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner Company not found.");
                }

                _mapper.Map(dto, partnerCompany);

                return await _unitOfWork.PartnerCompany.Update(partnerCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a partner company.");
                throw;
            }
        }
    }
}