using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.SalaryCheck;
using SmartMarket.Service.Interfaces.Worker.SalaryCheck;

namespace SmartMarket.Service.Services.Worker.SalaryCheck;

public class SalaryCheckService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryCheckDto> validator) : ISalaryCheckService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddSalaryCheckDto> _validator = validator;

    public async Task<bool> AddAsync(AddSalaryCheckDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var salaryCheck = _mapper.Map<Et.SalaryCheck>(dto);
        return await _unitOfWork.SalaryCheck.Add(salaryCheck);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var salaryCheck = await _unitOfWork.SalaryCheck.GetById(Id);
        if (salaryCheck == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Check not found.");
        }

        return await _unitOfWork.SalaryCheck.Remove(salaryCheck);
    }

    public async Task<List<SalaryCheckDto>> GetAllAsync()
    {
        var salaryChecks = await _unitOfWork.SalaryCheck.GetSalaryChecksFullInformationAsync();
        return _mapper.Map<List<SalaryCheckDto>>(salaryChecks);
    }

    public async Task<bool> UpdateAsync(AddSalaryCheckDto dto, Guid Id)
    {
        var salaryCheck = await _unitOfWork.SalaryCheck.GetById(Id);
        if (salaryCheck == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Check not found.");
        }

        _mapper.Map(dto, salaryCheck);

        return await _unitOfWork.SalaryCheck.Update(salaryCheck);
    }
}