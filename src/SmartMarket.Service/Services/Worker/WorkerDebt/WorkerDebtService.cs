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
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.WorkerDebt
{
    public class WorkerDebtService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddWorkerDebtDto> validator,
                             ILogger<WorkerDebtService> logger) : IWorkerDebtService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddWorkerDebtDto> _validator = validator;
        private readonly ILogger<WorkerDebtService> _logger = logger; 

        public async Task<bool> AddAsync(AddWorkerDebtDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var workerDebt = _mapper.Map<Et.WorkerDebt>(dto);
                return await _unitOfWork.WorkerDebt.Add(workerDebt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a worker debt.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var workerDebt = await _unitOfWork.WorkerDebt.GetById(Id);
                if (workerDebt == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Debt not found.");
                }

                return await _unitOfWork.WorkerDebt.Remove(workerDebt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a worker debt.");
                throw;
            }
        }

        public async Task<List<WorkerDebtDto>> GetAllAsync()
        {
            try
            {
                var workerDebts = await _unitOfWork.WorkerDebt.GetWorkerDebtsFullInformationAsync();
                return _mapper.Map<List<WorkerDebtDto>>(workerDebts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all worker debts.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddWorkerDebtDto dto, Guid Id)
        {
            try
            {
                var workerDebt = await _unitOfWork.WorkerDebt.GetById(Id);
                if (workerDebt == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Debt not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                _mapper.Map(dto, workerDebt);

                return await _unitOfWork.WorkerDebt.Update(workerDebt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a worker debt.");
                throw;
            }
        }
    }
}