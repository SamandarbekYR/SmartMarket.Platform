﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;
using SmartMarket.Service.Interfaces.Worker.SalaryWorker;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.SalaryWorker
{
    public class SalaryWorkerService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryWorkerDto> validator,
                             ILogger<SalaryWorkerService> logger) : ISalaryWorkerService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddSalaryWorkerDto> _validator = validator;
        private readonly ILogger<SalaryWorkerService> _logger = logger;

        public async Task<bool> AddAsync(AddSalaryWorkerDto dto)
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

                var salaryExists = await _unitOfWork.Salary.GetById(dto.SalaryId) != null;
                if (!salaryExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
                }

                var salaryWorker = _mapper.Map<Et.SalaryWorker>(dto);
                return await _unitOfWork.SalaryWorker.Add(salaryWorker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a salary worker relationship.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var salaryWorker = await _unitOfWork.SalaryWorker.GetById(Id);
                if (salaryWorker == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Worker relationship not found.");
                }

                return await _unitOfWork.SalaryWorker.Remove(salaryWorker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a salary worker relationship.");
                throw;
            }
        }

        public async Task<List<SalaryWorkerDto>> GetAllAsync()
        {
            try
            {
                var salaryWorkers = await _unitOfWork.SalaryWorker.GetSalaryWorkersFullInformationAsync();
                return _mapper.Map<List<SalaryWorkerDto>>(salaryWorkers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all salary worker relationships.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddSalaryWorkerDto dto, Guid Id)
        {
            try
            {
                var salaryWorker = await _unitOfWork.SalaryWorker.GetById(Id);
                if (salaryWorker == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Worker relationship not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var salaryExists = await _unitOfWork.Salary.GetById(dto.SalaryId) != null;
                if (!salaryExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
                }

                _mapper.Map(dto, salaryWorker);

                return await _unitOfWork.SalaryWorker.Update(salaryWorker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a salary worker relationship.");
                throw;
            }
        }
    }
}