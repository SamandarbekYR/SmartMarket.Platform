using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;
using SmartMarket.Service.Interfaces.Worker.SalaryWorker;

namespace SmartMarket.Service.Services.Worker.SalaryWorker;

public class SalaryWorkerService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryWorkerDto> validator) : ISalaryWorkerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddSalaryWorkerDto> _validator = validator;

    public async Task<bool> AddAsync(AddSalaryWorkerDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var salaryWorker = _mapper.Map<Et.SalaryWorker>(dto);
        return await _unitOfWork.SalaryWorker.Add(salaryWorker);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var salaryWorker = await _unitOfWork.SalaryWorker.GetById(Id);
        if (salaryWorker == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Worker relationship not found.");
        }

        return await _unitOfWork.SalaryWorker.Remove(salaryWorker);
    }

    public async Task<List<SalaryWorkerDto>> GetAllAsync()
    {
        var salaryWorkers = await _unitOfWork.SalaryWorker.GetSalaryWorkersFullInformationAsync();
        return _mapper.Map<List<SalaryWorkerDto>>(salaryWorkers);
    }

    public async Task<bool> UpdateAsync(AddSalaryWorkerDto dto, Guid Id)
    {
        var salaryWorker = await _unitOfWork.SalaryWorker.GetById(Id);
        if (salaryWorker == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Worker relationship not found.");
        }

        _mapper.Map(dto, salaryWorker);

        return await _unitOfWork.SalaryWorker.Update(salaryWorker);
    }
}
