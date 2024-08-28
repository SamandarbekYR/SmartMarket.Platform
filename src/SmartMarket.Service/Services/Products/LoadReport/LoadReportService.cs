using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.Interfaces.Products.LoadReport;

namespace SmartMarket.Service.Services.Products.LoadReport;

public class LoadReportService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddLoadReportDto> validator) : ILoadReportService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddLoadReportDto> _validator = validator;

    public async Task<bool> AddAsync(AddLoadReportDto dto)
    {
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
        }

        var loadReport = _mapper.Map<Et.LoadReport>(dto);
        return await _unitOfWork.LoadReport.Add(loadReport);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var loadReport = await _unitOfWork.LoadReport.GetById(Id);
        if (loadReport == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Load Report not found.");
        }

        return await _unitOfWork.LoadReport.Remove(loadReport);
    }

    public async Task<List<LoadReportDto>> GetAllAsync()
    {
        var loadReports = await _unitOfWork.LoadReport.GetLoadReportsFullInformationAsync();
        return _mapper.Map<List<LoadReportDto>>(loadReports);
    }

    public async Task<bool> UpdateAsync(AddLoadReportDto dto, Guid Id)
    {
        var loadReport = await _unitOfWork.LoadReport.GetById(Id);
        if (loadReport == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Load Report not found.");
        }

        _mapper.Map(dto, loadReport);

        return await _unitOfWork.LoadReport.Update(loadReport);
    }
}