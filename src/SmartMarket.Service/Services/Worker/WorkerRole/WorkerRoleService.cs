using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Workers.WorkerRole;
using SmartMarket.Service.Interfaces.Worker.WorkerRole;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.WorkerRole
{
    public class WorkerRoleService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddWorkerRoleDto> validator,
                                 ILogger<WorkerRoleService> logger) : IWorkerRoleService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddWorkerRoleDto> _validator = validator;
        private readonly ILogger<WorkerRoleService> _logger = logger; 

        public async Task<bool> AddAsync(AddWorkerRoleDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var workerRole = _mapper.Map<Et.WorkerRole>(dto);
                return await _unitOfWork.WorkerRole.Add(workerRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a worker role.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var workerRole = await _unitOfWork.WorkerRole.GetById(Id);
                if (workerRole == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Role not found.");
                }

                return await _unitOfWork.WorkerRole.Remove(workerRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a worker role.");
                throw;
            }
        }

        public async Task<List<WorkerRoleDto>> GetAllAsync()
        {
            try
            {
                var workerRoles = await _unitOfWork.WorkerRole.GetAll().ToListAsync();
                return _mapper.Map<List<WorkerRoleDto>>(workerRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all worker roles.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddWorkerRoleDto dto, Guid Id)
        {
            try
            {
                var workerRole = await _unitOfWork.WorkerRole.GetById(Id);
                if (workerRole == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Role not found.");
                }

                _mapper.Map(dto, workerRole);

                return await _unitOfWork.WorkerRole.Update(workerRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a worker role.");
                throw;
            }
        }
    }
}