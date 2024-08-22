using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.WorkerRole;
using SmartMarket.Service.Interfaces.WorkerRole;
using System.Net;

namespace SmartMarket.Service.Services.WorkerRole
{
    public class WorkerRoleService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddWorkerRoleDto> validator) : IWorkerRoleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddWorkerRoleDto> _validator = validator;

        public async Task<bool> AddAsync(AddWorkerRoleDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var workerRole = _mapper.Map<Et.WorkerRole>(dto);
            return await _unitOfWork.WorkerRole.Add(workerRole);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var workerRole = await _unitOfWork.WorkerRole.GetById(Id);
            if (workerRole == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Role not found.");
            }

            return await _unitOfWork.WorkerRole.Remove(workerRole);
        }

        public async Task<List<WorkerRoleDto>> GetAllAsync()
        {
            var workerRoles = await _unitOfWork.WorkerRole.GetAll().ToListAsync();
            return _mapper.Map<List<WorkerRoleDto>>(workerRoles);
        }

        public async Task<bool> UpdateAsync(AddWorkerRoleDto dto, Guid Id)
        {
            var workerRole = await _unitOfWork.WorkerRole.GetById(Id);
            if (workerRole == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Role not found.");
            }

            _mapper.Map(dto, workerRole);

            return await _unitOfWork.WorkerRole.Update(workerRole);
        }
    }
}