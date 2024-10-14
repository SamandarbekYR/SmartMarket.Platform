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
using Microsoft.Extensions.Logging;
using SmartMarket.Service.DTOs.Products.ProductImage;

namespace SmartMarket.Service.Services.Products.Product
{
    public class ProductService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddProductDto> validator,
                                 ILogger<ProductService> logger) : IProductService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductDto> _validator = validator;
        private readonly ILogger<ProductService> _logger = logger; 

        public async Task<Guid> AddAsync(AddProductDto dto)
        {
            try
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

                string pCode = await GenerateUniquePCodeAsync();

                var product = _mapper.Map<Et.Product>(dto);
                product.PCode = pCode;

                await _unitOfWork.Product.Add(product);
                await _unitOfWork.SaveAsync();

                return product.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var product = await _unitOfWork.Product.GetById(Id);
                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                return await _unitOfWork.Product.Remove(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a product.");
                throw;
            }
        }

        public async Task<IEnumerable<Et.Product>> GetAllAsync(PaginationParams paginationParams)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                                        .AsNoTracking()
                                        .ToPagedListAsync(paginationParams);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all products.");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(AddProductDto dto, Guid Id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a product.");
                throw;
            }
        }

        private async Task<string> GenerateUniquePCodeAsync()
        {
            Random random = new Random();
            string pCode;
            bool exists;

            do
            {
                pCode = random.Next(10000, 99999).ToString();
                exists = await CheckPCodeExistsAsync(pCode);
            } while (exists);

            return pCode;
        }

        private async Task<bool> CheckPCodeExistsAsync(string pCode)
        {
            return await _unitOfWork.Product.GetAll()
                .AnyAsync(p => p.PCode == pCode);
        }
        public async Task<ProductDto> GetProductByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await _unitOfWork.Product.GetAll()
                    .FirstOrDefaultAsync(p => p.Barcode == barcode);

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a product by barcode.");
                throw;
            }
        }

        public async Task<ProductDto> GetProductByPCodeAsync(string pCode)
        {
            try
            {
                var product = await _unitOfWork.Product.GetAll()
                    .FirstOrDefaultAsync(p => p.PCode == pCode);

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a product by PCode.");
                throw;
            }
        }

        public async Task<ProductDto> GetProductByWorkerIdAsync(Guid workerId)
        {
            try
            {
                var product = await _unitOfWork.Product.GetAll()
                    .FirstOrDefaultAsync(p => p.WorkerId == workerId);

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a product by worker ID.");
                throw;
            }
        }
        public async Task<bool> SellProductAsync(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode) || barcode.Length != 13)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Invalid barcode format.");
                }

                string quantityType = barcode.Substring(0, 2);

                string pCode = barcode.Substring(2, 5);

                int soldQuantity = int.Parse(barcode.Substring(7, 5));

                var product = await _unitOfWork.Product.GetAll()
                    .FirstOrDefaultAsync(p => p.PCode == pCode);

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                if (product.Count < soldQuantity)
                {
                    throw new StatusCodeException(HttpStatusCode.BadRequest, "Insufficient product quantity.");
                }

                product.Count -= soldQuantity;

                await _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while selling a product.");
                throw;
            }
        }

        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            try
            {
                var product = await _unitOfWork.Product.GetAll()
                    .FirstOrDefaultAsync(p => p.Name == name);

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
                }

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a product by name.");
                throw;
            }
        }
        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, PaginationParams @params)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAll()
                    .Where(p => p.CategoryId == categoryId)
                    .AsNoTracking()
                    .ToPagedListAsync(@params);
                return products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products by category ID.");
                throw;
            }
        }

        public async Task<IEnumerable<Et.Product>> GetProductsFullInformationAsync(PaginationParams paginationParams)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .ToPagedListAsync(paginationParams);
                return _mapper.Map<IEnumerable<Et.Product>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products full information.");
                throw;
            }
        }
        public async Task<IEnumerable<ProductDto>> GetFinishedProductsAsync(PaginationParams @params)
        {
            try
            {
                var finishedProducts = await _unitOfWork.Product.GetAll()
                    .Where(p => p.Count <= p.NoteAmount)
                    .AsNoTracking()
                    .ToPagedListAsync(@params);

                return _mapper.Map<IEnumerable<ProductDto>>(finishedProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting finished products.");
                throw;
            }
        }
    }
}