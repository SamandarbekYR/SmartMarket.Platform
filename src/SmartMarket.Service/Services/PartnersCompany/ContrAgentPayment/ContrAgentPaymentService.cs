using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgentPayment;

using System.Net;

using Et = SmartMarket.Domain.Entities.PartnersCompany;

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

        public async Task<List<ContrAgentPaymentDto>> GetAllByContrAgentIdAsync(Guid id)
        {
            var contrAgentPayments = await _unitOfWork.ContrAgentPayment
                .GetContrAgentPaymentsFullInformationAsync();

            var filteredPayments = contrAgentPayments
                .Where(payment => payment.ContrAgentId == id)
                .ToList();

            var paymentDtos = filteredPayments.Select(payment => new ContrAgentPaymentDto
            {
                Id = payment.Id,
                ContrAgentId = payment.ContrAgentId,
                PayDeskId = payment.PayDeskId,
                PaymentType = payment.PaymentType,
                TotalDebt = payment.TotalDebt,
                LastPayment = payment.LastPayment ?? 0,
                LastPaymentDate = payment.LastPaymentDate
            })
                .Where(cap => cap.TotalDebt > 0)
                .ToList();

            return paymentDtos;
        }


        public async Task<bool> UpdateAsync(AddContrAgentPaymentDto dto)
        {
            try
            {
                var contrAgent = (await _unitOfWork.ContrAgent
                    .GetContrAgentsFullInformationAsync())
                    .FirstOrDefault(c => c.Id == dto.ContrAgentId);

                if (contrAgent == null)
                    throw new Exception("ContrAgent not found.");

                var paymentRecords = contrAgent.ContrAgentPayment
                    .Where(p => p.TotalDebt > 0)
                    .OrderBy(p => p.LastPaymentDate)
                    .ToList();

                if (!paymentRecords.Any())
                    throw new Exception("To'lov uchun qarz topilmadi!");

                double remainingPayment = dto.LastPayment;
                bool result = false;
                foreach (var paymentRecord in paymentRecords)
                {
                    if (remainingPayment <= 0)
                        break;

                    double debtToPay = Math.Min(paymentRecord.TotalDebt, remainingPayment);

                    paymentRecord.TotalDebt -= debtToPay;
                    paymentRecord.LastPayment = debtToPay;
                    paymentRecord.LastPaymentDate = DateTime.UtcNow;

                    remainingPayment -= debtToPay;

                    result = await _unitOfWork.ContrAgentPayment.Update(paymentRecord);
                }

                if (remainingPayment > 0)
                    throw new Exception($"To'lov miqdori qarzdan oshib ketdi!");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a counteragent payment.");
                throw;
            }
        }
    }
}