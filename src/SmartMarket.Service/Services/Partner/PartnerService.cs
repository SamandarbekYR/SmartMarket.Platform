using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.Interfaces.Partner;

using System.Net;
using Et = SmartMarket.Domain.Entities.Partners;

namespace SmartMarket.Service.Services.Partner
{
    public class PartnerService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddPartnerDto> validator,
                             ILogger<PartnerService> logger) : IPartnerService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddPartnerDto> _validator = validator;
        private readonly ILogger<PartnerService> _logger = logger; 

        public async Task<bool> AddAsync(AddPartnerDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var partner = _mapper.Map<Et.Partner>(dto);
                return await _unitOfWork.Partner.Add(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a partner.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetById(Id);
                if (partner == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                return await _unitOfWork.Partner.Remove(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a partner.");
                throw;
            }
        }

        public async Task<IEnumerable<PartnerDto>> FilterExpenceAsync(FilterPartnerDto filterDto)
        {
            try
            {

                var partners = await _unitOfWork.Partner.GetPartnersFullInformationAsync();

                if (filterDto.FromDateTime.HasValue && filterDto.ToDateTime.HasValue)
                {
                    partners = partners.Where(
                    ps => ps.CreatedDate >= filterDto.FromDateTime.Value
                        && ps.CreatedDate <= filterDto.ToDateTime.Value).ToList();
                }
                else
                {
                    partners = partners.Where(
                        ps => ps.CreatedDate.Value.Date == DateTime.Today).ToList();
                }

                if (filterDto.Id.HasValue)
                {
                    partners = partners.Where(p => p.PayDeskId == filterDto.Id.Value).ToList();
                }

                return _mapper.Map<List<PartnerDto>>(partners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filtering partners.");
                throw;
            }
        }

        public async Task<List<PartnerDto>> GetAllAsync()
        {
            try
            {
                var partners = await _unitOfWork.Partner.GetPartnersFullInformationAsync();
                return _mapper.Map<List<PartnerDto>>(partners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all partners.");
                throw;
            }
        }

        public async Task<PartnerDto> GetPartnerByFirstNameAsync(string firstName)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetAll()
                    .FirstOrDefaultAsync(p => p.FirstName.ToLower() == firstName.ToLower());

                if (partner == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                return _mapper.Map<PartnerDto>(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting partner by first name.");
                throw;
            }
        }

        public async Task<PartnerDto> GetPartnerByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetAll()
                    .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);

                if (partner == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                return _mapper.Map<PartnerDto>(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting partner by phone number.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddPartnerDto dto, Guid partnerId)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetById(partnerId);
                if (partner == null)
                {
                    throw new Exception("Partner not found");
                }

                partner.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : partner.FirstName;
                partner.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : partner.LastName;
                partner.PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : partner.PhoneNumber;

                if (dto.LastPayment.HasValue)
                {
                    double newPaidDebt = (partner.PaidDebt ?? 0) + dto.LastPayment.Value;
                    double newTotalDebt = (partner.TotalDebt ?? 0) - dto.LastPayment.Value;

                    partner.PaidDebt = newPaidDebt >= 0 ? newPaidDebt : partner.PaidDebt;
                    partner.TotalDebt = newTotalDebt >= 0 ? newTotalDebt : partner.TotalDebt;
                }

                partner.LastPayment = dto.LastPaymentDate != default ? dto.LastPaymentDate.ToUniversalTime() : partner.LastPayment;
                partner.PaymentType = !string.IsNullOrEmpty(dto.PaymentType) ? dto.PaymentType : partner.PaymentType;

                var result = await _unitOfWork.Partner.Update(partner);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating partner");
                throw;
            }
        }

        public async Task<bool> UpdatePartnerDebtSumAsync(double debtSum, Guid Id)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetById(Id);
                if (partner == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Partner not found.");
                }

                partner.TotalDebt = (partner.TotalDebt ?? 0) + debtSum; ;
                return await _unitOfWork.Partner.Update(partner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a partner.");
                throw;
            }
        }
    }
}