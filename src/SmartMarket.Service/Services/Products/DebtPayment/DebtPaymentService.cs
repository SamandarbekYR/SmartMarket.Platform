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

namespace SmartMarket.Service.Services.Products.DebtPayment
{
    public class DebtPaymentService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddDebtPaymentDto> validator) : IDebtPaymentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddDebtPaymentDto> _validator = validator;

        public async Task<bool> AddAsync(AddDebtPaymentDto dto)
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

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var debtPayment = await _unitOfWork.DebtPayment.GetById(Id);
            if (debtPayment == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Debt Payment not found.");
            }

            return await _unitOfWork.DebtPayment.Remove(debtPayment);
        }

        public async Task<List<DebtPaymentDto>> GetAllAsync()
        {
            var debtPayments = await _unitOfWork.DebtPayment.GetDebtPaymentsFullInformationAsync();
            return _mapper.Map<List<DebtPaymentDto>>(debtPayments);
        }

        public async Task<bool> UpdateAsync(AddDebtPaymentDto dto, Guid Id)
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
    }
}