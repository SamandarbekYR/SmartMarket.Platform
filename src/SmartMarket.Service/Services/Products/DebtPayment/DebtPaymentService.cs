using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.DebtPayment;
using SmartMarket.Service.Interfaces.Products.DebtPayment;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Products.DebtPayment
{
    public class DebtPaymentService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddDebtPaymentDto> validator,
                             ILogger<DebtPaymentService> logger) : IDebtPaymentService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddDebtPaymentDto> _validator = validator;
        private readonly ILogger<DebtPaymentService> _logger = logger; 

        public async Task<bool> AddAsync(AddDebtPaymentDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var debtorExists = await _unitOfWork.Debtors.GetById(dto.DebtorId) != null;
                if (!debtorExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Debtor not found.");
                }

                var debtPayment = _mapper.Map<Et.DebtPayment>(dto);
                return await _unitOfWork.DebtPayment.Add(debtPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a debt payment.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var debtPayment = await _unitOfWork.DebtPayment.GetById(Id);
                if (debtPayment == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Debt Payment not found.");
                }

                return await _unitOfWork.DebtPayment.Remove(debtPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a debt payment.");
                throw;
            }
        }

        public async Task<List<DebtPaymentDto>> GetAllAsync()
        {
            try
            {
                var debtPayments = await _unitOfWork.DebtPayment.GetDebtPaymentsFullInformationAsync();
                return _mapper.Map<List<DebtPaymentDto>>(debtPayments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all debt payments.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddDebtPaymentDto dto, Guid Id)
        {
            try
            {
                var debtPayment = await _unitOfWork.DebtPayment.GetById(Id);
                if (debtPayment == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Debt Payment not found.");
                }

                var debtorExists = await _unitOfWork.Debtors.GetById(dto.DebtorId) != null;
                if (!debtorExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Debtor not found.");
                }

                _mapper.Map(dto, debtPayment);

                return await _unitOfWork.DebtPayment.Update(debtPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a debt payment.");
                throw;
            }
        }
    }
}