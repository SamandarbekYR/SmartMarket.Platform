using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.DebtPayment;
using SmartMarket.Service.Interfaces.DebtPayment;
using System.Net;

namespace SmartMarket.Service.Services.DebtPayment;

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
        var debtPayments = await _unitOfWork.DebtPayment.GetAll().ToListAsync();
        return _mapper.Map<List<DebtPaymentDto>>(debtPayments);
    }

    public async Task<bool> UpdateAsync(AddDebtPaymentDto dto, Guid Id)
    {
        var debtPayment = await _unitOfWork.DebtPayment.GetById(Id);
        if (debtPayment == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Debt Payment not found.");
        }

        _mapper.Map(dto, debtPayment);

        return await _unitOfWork.DebtPayment.Update(debtPayment);
    }
}