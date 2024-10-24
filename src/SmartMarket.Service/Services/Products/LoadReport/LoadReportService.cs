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
using Microsoft.Extensions.Logging;
using SmartMarket.Service.DTOs.Expence;

namespace SmartMarket.Service.Services.Products.LoadReport
{
    public class LoadReportService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddLoadReportDto> validator,
                             ILogger<LoadReportService> logger) : ILoadReportService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddLoadReportDto> _validator = validator;
        private readonly ILogger<LoadReportService> _logger = logger; 

        public async Task<bool> AddAsync(AddLoadReportDto dto)
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

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;
                if (!contrAgentExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                var loadReport = _mapper.Map<Et.LoadReport>(dto);
                return await _unitOfWork.LoadReport.Add(loadReport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a load report.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var loadReport = await _unitOfWork.LoadReport.GetById(Id);
                if (loadReport == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Load Report not found.");
                }

                return await _unitOfWork.LoadReport.Remove(loadReport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a load report.");
                throw;
            }
        }

        public async Task<List<LoadReportDto>> FilterLoadReportAsync(FilterLoadReportDto dto)
        {
            try
            {
                var loadReports = await _unitOfWork.LoadReport.GetLoadReportsFullInformationAsync();

                if(dto.FromDateTime.HasValue && dto.ToDateTime.HasValue)
                {
                    loadReports = loadReports.Where(
                        l => l.CreatedDate >= dto.FromDateTime.Value
                        && l.CreatedDate <= dto.ToDateTime.Value).ToList();
                }
                else
                {
                    loadReports = loadReports.Where(
                        l => l.CreatedDate.Value.Date == DateTime.Today).ToList();
                }

                if(!string.IsNullOrWhiteSpace(dto.ProductName))
                {
                    loadReports = loadReports.Where(
                        l => l.Product.Name.Contains(dto.ProductName)).ToList();
                }

                if(!string.IsNullOrWhiteSpace(dto.WorkerName))
                {
                    loadReports = loadReports.Where(
                        l => l.Worker.FirstName.Contains(dto.WorkerName)).ToList();
                }

                var loadReportDtos = loadReports.Select(l => new LoadReportDto
                {
                    Id = l.Id,
                    WorkerId = l.WorkerId,
                    ProductId = l.ProductId,
                    ContrAgentId = l.ContrAgentId,
                    TotalPrice = l.TotalPrice,
                    ProductName = l.Product.Name,
                    ProductPrice = l.Product.Price,
                    ProductCount = l.Product.Count
                }).ToList();

                return loadReportDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while filter loadReport");
                throw;
            }
        }

        public async Task<List<LoadReportDto>> GetAllAsync()
        {
            try
            {
                var loadReports = await _unitOfWork.LoadReport.GetLoadReportsFullInformationAsync();

                var loadReportDto = loadReports.Select(l => new LoadReportDto
                {
                    Id = l.Id,
                    WorkerId = l.WorkerId,
                    Worker = l.Worker,
                    ProductId = l.ProductId,
                    ContrAgentId = l.ContrAgentId,
                    TotalPrice = l.TotalPrice,
                    ProductName = l.Product.Name,
                    ProductCount= l.Product.Count,
                    ProductPrice= l.Product.Price
                }).ToList();

                return loadReportDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all load reports.");
                throw;
            }
        }

        public async Task<LoadReportDto> GetByIdAsync(Guid Id)
        {
            try
            {
                var loadReport = await _unitOfWork.LoadReport.GetLoadReportsFullInformationAsync();

                var loadReportDto = loadReport.FirstOrDefault(x => x.Id == Id);

                var loadReportResult = new LoadReportDto
                {
                    Id = loadReportDto.Id,
                    WorkerId = loadReportDto.WorkerId,
                    ProductId = loadReportDto.ProductId,
                    ContrAgentId = loadReportDto.ContrAgentId,
                    TotalPrice = loadReportDto.TotalPrice,
                    ProductName = loadReportDto.Product.Name,
                    ProductCount =loadReportDto.Product.Count,
                    ProductPrice = loadReportDto.Product.Price
                };

                return loadReportResult;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting load report by id.");
                throw;
            }
        }

        public Task<List<LoadReportDto>> GetLoadReportsByCompanyNameAsync(string companyName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoadReportDto>> GetLoadReportsByContrAgentIdAsync(Guid contrAgentId)
        {
            try
            {
                var loadReports = await _unitOfWork.LoadReport.GetLoadReportsFullInformationAsync();

                if(contrAgentId != Guid.Empty)
                {
                    loadReports = loadReports.Where(
                        x => x.ContrAgentId == contrAgentId).ToList();
                }

                var loadReportDtos = loadReports.Select(l => new LoadReportDto
                {
                    Id = l.Id,
                    WorkerId = l.WorkerId,
                    Worker = l.Worker,
                    ProductId = l.ProductId,
                    ContrAgentId = l.ContrAgentId,
                    TotalPrice = l.TotalPrice,
                    ProductName = l.Product.Name,
                    ProductCount = l.Product.Count,
                    ProductPrice = l.Product.Price
                }).ToList();

                return loadReportDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all load reports by contr agent id.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddLoadReportDto dto, Guid Id)
        {
            try
            {
                var loadReport = await _unitOfWork.LoadReport.GetById(Id);
                if (loadReport == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Load Report not found.");
                }

                var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
                if (!workerExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
                }

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;
                if (!contrAgentExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Counteragent not found.");
                }

                _mapper.Map(dto, loadReport);

                return await _unitOfWork.LoadReport.Update(loadReport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a load report.");
                throw;
            }
        }
    }
}