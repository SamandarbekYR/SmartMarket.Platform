using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Security;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.Interfaces.Common;
using SmartMarket.Service.Interfaces.Worker.Workers;
using System.Net;
using Microsoft.Extensions.Logging; 

namespace SmartMarket.Service.Services.Worker.Workers
{
    public class WorkerService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IValidator<AddWorkerDto> validator,
                           IFileService fileService,
                           ILogger<WorkerService> logger) : IWorkerService 
    {
        public readonly IMapper _mapper = mapper;
        public readonly IValidator<AddWorkerDto> _validator = validator;
        public readonly IUnitOfWork _unitOfWork = unitOfWork;
        public readonly IFileService _fileService = fileService;
        private readonly ILogger<WorkerService> _logger = logger;
        private const string ROOT_PATH = "WorkerImages";

        public async Task<bool> AddAsync(AddWorkerDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors.First().ErrorMessage);

                var positionExists = await _unitOfWork.Position.GetById(dto.PositionId) != null;
                if (!positionExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Position not found.");
                }

                var workerRoleExists = await _unitOfWork.WorkerRole.GetById(dto.WorkerRoleId) != null;
                if (!workerRoleExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker Role not found.");
                }

                (string Hash, string Salt) = PasswordHasher.Hash(dto.Password);
                var imagePath = await _fileService.UploadImageAsync(dto.ImgPath, ROOT_PATH);
                var worker = _mapper.Map<Et.Worker>(dto);
                worker.ImgPath = imagePath;
                worker.PasswordHash = Hash;
                worker.PasswordSalt = Salt;

                return await _unitOfWork.Worker.Add(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a worker.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var worker = await _unitOfWork.Worker.GetById(Id);

                if (worker == null)
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");

                return await _unitOfWork.Worker.Remove(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a worker.");
                throw;
            }
        }

        public async Task<List<Et.Worker>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.Worker.GetWorkersFullInformationAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all workers.");
                throw;
            }
        }

        public Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id)
        {
            try
            {
                var workerId = _unitOfWork.Worker.GetById(Id);

                if (workerId == null)
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found");

                var validationResult = _validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors.First().ErrorMessage);

                var worker = _mapper.Map<Et.Worker>(dto);

                return _unitOfWork.Worker.Update(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a worker.");
                throw;
            }
        }

        public async Task<WorkerDto> GetWorkerByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var worker = await _unitOfWork.Worker.GetPhoneNumberAsync(phoneNumber);

                if (worker == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                return _mapper.Map<WorkerDto>(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a worker by phone number.");
                throw;
            }
        }

        public async Task<WorkerDto> GetWorkerByNameAsync(string firstName)
        {
            try
            {
                var worker = await _unitOfWork.Worker.GetAll()
                    .FirstOrDefaultAsync(w => w.FirstName.ToLower() == firstName.ToLower());

                if (worker == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                return _mapper.Map<WorkerDto>(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a worker by first name.");
                throw;
            }
        }
    }
}