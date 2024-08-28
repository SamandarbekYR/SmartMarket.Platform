using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.WorkerDebt;
using SmartMarket.Service.Interfaces.Worker.WorkerDebt;

namespace SmartMarket.Service.Services.Worker.WorkerDebt;

public class WorkerDebtService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddWorkerDebtDto> validator) : IWorkerDebtService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddWorkerDebtDto> _validator = validator;

    public async Task<bool> AddAsync(AddWorkerDebtDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var workerDebt = _mapper.Map<Et.WorkerDebt>(dto);
        return await _unitOfWork.WorkerDebt.Add(workerDebt);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var workerDebt = await _unitOfWork.WorkerDebt.GetById(Id);
        if (workerDebt == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Debt not found.");
        }

        return await _unitOfWork.WorkerDebt.Remove(workerDebt);
    }

    public async Task<List<WorkerDebtDto>> GetAllAsync()
    {
        var workerDebts = await _unitOfWork.WorkerDebt.GetWorkerDebtsFullInformationAsync();
        return _mapper.Map<List<WorkerDebtDto>>(workerDebts);
    }

    public async Task<bool> UpdateAsync(AddWorkerDebtDto dto, Guid Id)
    {
        var workerDebt = await _unitOfWork.WorkerDebt.GetById(Id);
        if (workerDebt == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Debt not found.");
        }

        _mapper.Map(dto, workerDebt);

        return await _unitOfWork.WorkerDebt.Update(workerDebt);
    }
}
