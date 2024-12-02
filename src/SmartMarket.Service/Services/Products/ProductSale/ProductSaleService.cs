using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.Interfaces.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using Microsoft.Extensions.Logging; // Import ILogger

namespace SmartMarket.Service.Services.Products.ProductSale
{
    public class ProductSaleService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddProductSaleDto> validator,
                             ILogger<ProductSaleService> logger) : IProductSaleService // Inject ILogger
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductSaleDto> _validator = validator;
        private readonly ILogger<ProductSaleService> _logger = logger; // Store the logger

        public async Task<bool> AddAsync(AddProductSaleDto dto)
        {
            try
            {
                var validationResult = _validator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
                }

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                var salesRequestExists = await _unitOfWork.SalesRequest.GetById(dto.SalesRequestId) != null;
                if(!salesRequestExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Sales request not found.");
                }

                DateTime now = DateTime.UtcNow.AddHours(5);

                var productSale = _mapper.Map<Et.ProductSale>(dto);
                productSale.CreatedDate = now;
                return await _unitOfWork.ProductSale.Add(productSale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product sale.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var productSale = await _unitOfWork.ProductSale.GetById(Id);
                if (productSale == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                return await _unitOfWork.ProductSale.Remove(productSale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a product sale.");
                throw;
            }
        }

        public async Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto)
        {
            var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();

            if (dto.FromDateTime.HasValue && dto.ToDateTime.HasValue)
            {
                productSales = productSales.Where(
                    ps => ps.CreatedDate >= dto.FromDateTime.Value 
                    && ps.CreatedDate <= dto.ToDateTime.Value).ToList();
            }
            else
            {
                productSales = productSales.Where(
                    ps => ps.CreatedDate.Value.Date == DateTime.Today).ToList();
            }

            if (!string.IsNullOrWhiteSpace(dto.ProductName))
            {
                productSales = productSales.Where(
                    ps => ps.Product.Name.ToLower().Contains(dto.ProductName.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(dto.WorkerName))
            {
                productSales = productSales.Where(
                    ps => ps.SalesRequest.Worker.FirstName.Contains(dto.WorkerName)).ToList();
            }

            if (dto.Count.HasValue)
            {
                productSales = productSales.Where(ps => ps.Count >= dto.Count.Value).ToList();
            }

            var result = _mapper.Map<List<ProductSaleViewModel>>(productSales);

            return result;
        }

        public async Task<List<ProductSaleViewModel>> GetAllAsync()
        {
            try
            {
                var productSales = (await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync());

                return _mapper.Map<List<ProductSaleViewModel>>(productSales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all product sales.");
                throw;
            }
        }

        public async Task<ProductSaleViewModel> GetByIdAsync(Guid Id)
        {
            var productSale = await _unitOfWork.ProductSale.GetById(Id);
            if (productSale == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
            }

            return _mapper.Map<ProductSaleViewModel>(productSale);
        }

        public async Task<List<ProductSaleDto>> GetProductSalesByProductNameAsync(string productName)
        {
            try
            {
                var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();

                var filteredProductSales = productSales
                    .Where(ps => ps.Product.Name.ToLower().Contains(productName.ToLower()))
                    .ToList();

                if (!filteredProductSales.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No product sales found for the specified product name.");
                }

                return _mapper.Map<List<ProductSaleDto>>(filteredProductSales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product sales by product name.");
                throw;
            }
        }


        public async Task<List<ProductSaleDto>> GetProductSalesByTransactionAsync(Guid transactionId)
        {
            try
            {
                var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();

                var filteredProductSales = productSales;
                    

                if (!filteredProductSales.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No product sales found for the specified transaction.");
                }

                return _mapper.Map<List<ProductSaleDto>>(filteredProductSales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product sales by transaction ID.");
                throw;
            }
        }

        public async Task<List<ProductSaleDto>> GetProductSalesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();

                var filteredProductSales = productSales
                    .Where(ps => ps.CreatedDate >= fromDate && ps.CreatedDate <= toDate)
                    .ToList();

                if (!filteredProductSales.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No product sales found for the specified date range.");
                }

                return _mapper.Map<List<ProductSaleDto>>(filteredProductSales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product sales by date range.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id)
        {
            try
            {
                var productSale = await _unitOfWork.ProductSale.GetById(Id);
                if (productSale == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
                }

                var productExists = await _unitOfWork.Product.GetById(dto.ProductId) != null;
                if (!productExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                _mapper.Map(dto, productSale);

                return await _unitOfWork.ProductSale.Update(productSale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a product sale.");
                throw;
            }
        }
    }
}