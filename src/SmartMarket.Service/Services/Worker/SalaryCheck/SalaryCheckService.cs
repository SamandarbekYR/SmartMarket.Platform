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
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.SalaryCheck
{
    public class SalaryCheckService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryCheckDto> validator,
                             ILogger<SalaryCheckService> logger) : ISalaryCheckService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddSalaryCheckDto> _validator = validator;
        private readonly ILogger<SalaryCheckService> _logger = logger; 

        public async Task<bool> AddAsync(AddSalaryCheckDto dto)
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

                var salaryCheck = _mapper.Map<Et.SalaryCheck>(dto);
                return await _unitOfWork.SalaryCheck.Add(salaryCheck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a salary check.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var salaryCheck = await _unitOfWork.SalaryCheck.GetById(Id);
                if (salaryCheck == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Check not found.");
                }

                return await _unitOfWork.SalaryCheck.Remove(salaryCheck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a salary check.");
                throw;
            }
        }

        public async Task<List<SalaryCheckDto>> GetAllAsync()
        {
            try
            {
                var salaryChecks = await _unitOfWork.SalaryCheck.GetSalaryChecksFullInformationAsync();
                return _mapper.Map<List<SalaryCheckDto>>(salaryChecks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all salary checks.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddSalaryCheckDto dto, Guid Id)
        {
            try
            {
                var salaryCheck = await _unitOfWork.SalaryCheck.GetById(Id);
                if (salaryCheck == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary Check not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                _mapper.Map(dto, salaryCheck);

                return await _unitOfWork.SalaryCheck.Update(salaryCheck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a salary check.");
                throw;
            }
        }
    }
}