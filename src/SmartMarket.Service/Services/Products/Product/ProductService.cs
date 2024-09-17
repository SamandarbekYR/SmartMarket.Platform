using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Interfaces;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Validators;
using System.Net;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Products.Product;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.Common.Extentions;

namespace SmartMarket.Service.Services.Products.Product
{
    public class ProductService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddProductDto> validator) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductDto> _validator = validator;

        public async Task<Guid> AddAsync(AddProductDto dto)
        {
            var validationResult = _validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.First().ErrorMessage);
            }

            var categoryExists = await _unitOfWork.Category.GetById(dto.CategoryId) != null;

            if (!categoryExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
            }

            var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;

            if (!workerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
            }

            string pCode = GeneratePCode();

            var product = _mapper.Map<Et.Product>(dto);
            product.PCode = pCode;

            await _unitOfWork.Product.Add(product);
            await _unitOfWork.SaveAsync(); 

            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var product = await _unitOfWork.Product.GetById(Id);
            if (product == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            return await _unitOfWork.Product.Remove(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(PaginationParams @params)
        {
            var products = await _unitOfWork.Product.GetAll().ToPagedListAsync(@params);
            return products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
        }

        public async Task<bool> UpdateAsync(AddProductDto dto, Guid Id)
        {
            var product = await _unitOfWork.Product.GetById(Id);
            if (product == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            var categoryExists = await _unitOfWork.Category.GetById(dto.CategoryId) != null;

            if (!categoryExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found.");
            }

            var workerExists = await _unitOfWork.Worker.GetById(dto.WorkerId) != null;

            if (!workerExists)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found.");
            }

            _mapper.Map(dto, product);

            return await _unitOfWork.Product.Update(product);
        }

        private string GeneratePCode()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();
        }
        public async Task<ProductDto> GetProductByBarcodeAsync(string barcode)
        {
            var product = await _unitOfWork.Product.GetAll()
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            if (product == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductByPCodeAsync(string pCode)
        {
            var product = await _unitOfWork.Product.GetAll()
                .FirstOrDefaultAsync(p => p.PCode == pCode);

            if (product == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProductByWorkerAsync(Guid workerId)
        {
            var product = await _unitOfWork.Product.GetAll()
                .FirstOrDefaultAsync(p => p.WorkerId == workerId);

            if (product == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }

    }
}