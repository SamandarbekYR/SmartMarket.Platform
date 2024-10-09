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
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.Salary
{
    public class SalaryService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddSalaryDto> validator,
                             ILogger<SalaryService> logger) : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddSalaryDto> _validator = validator;
        private readonly ILogger<SalaryService> _logger = logger;

        public async Task<bool> AddAsync(AddSalaryDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var salary = _mapper.Map<Et.Salary>(dto);
                return await _unitOfWork.Salary.Add(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a salary.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var salary = await _unitOfWork.Salary.GetById(Id);
                if (salary == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
                }

                return await _unitOfWork.Salary.Remove(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a salary.");
                throw;
            }
        }

        public async Task<List<SalaryDto>> GetAllAsync()
        {
            try
            {
                var salaries = await _unitOfWork.Salary.GetAll().ToListAsync();
                return _mapper.Map<List<SalaryDto>>(salaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all salaries.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddSalaryDto dto, Guid Id)
        {
            try
            {
                var salary = await _unitOfWork.Salary.GetById(Id);
                if (salary == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Salary not found.");
                }

                _mapper.Map(dto, salary);

                return await _unitOfWork.Salary.Update(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a salary.");
                throw;
            }
        }
    }
}