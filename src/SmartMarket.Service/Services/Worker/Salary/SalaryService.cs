using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.Salary;
using SmartMarket.Service.Interfaces.Worker.Salary;

namespace SmartMarket.Service.Services.Worker.Salary;

public class SalaryService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryDto> validator) : ISalaryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddSalaryDto> _validator = validator;

    public async Task<bool> AddAsync(AddSalaryDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var salary = _mapper.Map<Et.Salary>(dto);
        return await _unitOfWork.Salary.Add(salary);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var salary = await _unitOfWork.Salary.GetById(Id);
        if (salary == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
        }

        return await _unitOfWork.Salary.Remove(salary);
    }

    public async Task<List<SalaryDto>> GetAllAsync()
    {
        var salaries = await _unitOfWork.Salary.GetAll().ToListAsync();
        return _mapper.Map<List<SalaryDto>>(salaries);
    }

    public async Task<bool> UpdateAsync(AddSalaryDto dto, Guid Id)
    {
        var salary = await _unitOfWork.Salary.GetById(Id);
        if (salary == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
        }

        _mapper.Map(dto, salary);

        return await _unitOfWork.Salary.Update(salary);
    }
}