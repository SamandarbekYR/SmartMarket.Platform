﻿using AutoMapper;
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

namespace SmartMarket.Service.Services.Worker.Workers;

public class WorkerService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IValidator<AddWorkerDto> validator,
                           IFileService fileService) : IWorkerService
{
    public readonly IMapper _mapper = mapper;
    public readonly IValidator<AddWorkerDto> _validator = validator;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IFileService _fileService = fileService;
    private const string ROOT_PATH = "WorkerImages";

    public async Task<bool> AddAsync(AddWorkerDto dto)
    {
        var validationResult = _validator.Validate(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.First().ErrorMessage);


        (string Hash, string Salt) = PasswordHasher.Hash(dto.Password);
        var imagePath = await _fileService.UploadImageAsync(dto.ImgPath, ROOT_PATH);
        var worker = _mapper.Map<Et.Worker>(dto);
        worker.ImgPath = imagePath;
        worker.PasswordHash = Hash;
        worker.PasswordSalt = Salt;

        return await _unitOfWork.Worker.Add(worker);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var worker = await _unitOfWork.Worker.GetById(Id);

        if (worker == null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");

        return await _unitOfWork.Worker.Remove(worker);
    }

    public async Task<List<Et.Worker>> GetAllAsync()
    => await _unitOfWork.Worker.GetWorkersFullInformationAsync();

    public Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id)
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
}