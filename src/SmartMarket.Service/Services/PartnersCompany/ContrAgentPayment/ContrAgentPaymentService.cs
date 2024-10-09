using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgentPayment;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.PartnersCompany.ContrAgentPayment
{
    public class ContrAgentPaymentService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddContrAgentPaymentDto> validator,
                                 ILogger<ContrAgentPaymentService> logger) : IContrAgentPaymentService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddContrAgentPaymentDto> _validator = validator;
        private readonly ILogger<ContrAgentPaymentService> _logger = logger; 

        public async Task<bool> AddAsync(AddContrAgentPaymentDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;
                if (!contrAgentExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                var contrAgentPayment = _mapper.Map<Et.ContrAgentPayment>(dto);
                return await _unitOfWork.ContrAgentPayment.Add(contrAgentPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a counteragent payment.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var contrAgentPayment = await _unitOfWork.ContrAgentPayment.GetById(Id);
                if (contrAgentPayment == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent Payment not found.");
                }

                if (contrAgentPayment.TotalDebt > 0)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Counteragent Payment has outstanding debt and cannot be deleted.");
                }

                return await _unitOfWork.ContrAgentPayment.Remove(contrAgentPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a counteragent payment.");
                throw;
            }
        }

        public async Task<List<ContrAgentPaymentDto>> GetAllAsync()
        {
            try
            {
                var contrAgentPayments = await _unitOfWork.ContrAgentPayment.GetContrAgentPaymentsFullInformationAsync();
                return _mapper.Map<List<ContrAgentPaymentDto>>(contrAgentPayments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all counteragent payments.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddContrAgentPaymentDto dto, Guid Id)
        {
            try
            {
                var contrAgentPayment = await _unitOfWork.ContrAgentPayment.GetById(Id);
                if (contrAgentPayment == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent Payment not found.");
                }

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;
                if (!contrAgentExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                _mapper.Map(dto, contrAgentPayment);

                return await _unitOfWork.ContrAgentPayment.Update(contrAgentPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a counteragent payment.");
                throw;
            }
        }
    }
}