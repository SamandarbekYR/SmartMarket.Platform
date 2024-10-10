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

namespace SmartMarket.Service.Services.Products.ProductSale
{
    public class ProductSaleService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<AddProductSaleDto> validator) : IProductSaleService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductSaleDto> _validator = validator;

        public async Task<bool> AddAsync(AddProductSaleDto dto)
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

            var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
            if (!workerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
            }

            var transactionExists = await _unitOfWork.Transaction.GetById(dto.TransactionId) != null;
            if (!transactionExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Transaction not found.");
            }

            var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
            if (!payDeskExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
            }

            var productSale = _mapper.Map<Et.ProductSale>(dto);
            return await _unitOfWork.ProductSale.Add(productSale);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var productSale = await _unitOfWork.ProductSale.GetById(Id);
            if (productSale == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product Sale not found.");
            }

            return await _unitOfWork.ProductSale.Remove(productSale);
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
                    ps => ps.Product.Name.Contains(dto.ProductName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(dto.WorkerName))
            {
                productSales = productSales.Where(
                    ps => ps.Worker.FirstName.Contains(dto.WorkerName)).ToList();
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
            var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();
                
            return _mapper.Map<List<ProductSaleViewModel>>(productSales);
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


        public async Task<List<ProductSaleDto>> GetProductSalesByTransactionAsync(Guid transactionId)
        {
            var productSales = await _unitOfWork.ProductSale.GetProductSalesFullInformationAsync();

            var filteredProductSales = productSales
                .Where(ps => ps.TransactionId == transactionId)
                .ToList();

            if (!filteredProductSales.Any())
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "No product sales found for the specified transaction.");
            }

            return _mapper.Map<List<ProductSaleDto>>(filteredProductSales);
        }


        public async Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id)
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

            var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;
            if (!workerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
            }

            var transactionExists = await _unitOfWork.Transaction.GetById(dto.TransactionId) != null;
            if (!transactionExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Transaction not found.");
            }

            var payDeskExists = await _unitOfWork.PayDesk.GetById(dto.PayDeskId) != null;
            if (!payDeskExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Pay Desk not found.");
            }

            _mapper.Map(dto, productSale);

            return await _unitOfWork.ProductSale.Update(productSale);
        }
    }
}