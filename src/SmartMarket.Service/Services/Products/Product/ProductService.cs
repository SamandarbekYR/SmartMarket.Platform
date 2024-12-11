using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Extentions;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarket.Service.Interfaces.Products.Product;
using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Interfaces.Products.LoadReport;
using SmartMarket.Service.DTOs.Products.LoadReport;

namespace SmartMarket.Service.Services.Products.Product
{
    public class ProductService(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 IValidator<AddProductDto> validator,
                                 ILoadReportService loadReportService,
                                 ILogger<ProductService> logger) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<AddProductDto> _validator = validator;
        private readonly ILoadReportService _loadReportService = loadReportService;
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

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;

                if (contrAgentExists)
                {
                    var contrAgentPaymentDto = new AddContrAgentPaymentDto
                    {
                        ContrAgentId = dto.ContrAgentId,
                        PayDeskId = dto.PayDeskId,
                        TotalDebt = dto.Price * dto.Count,
                        LastPayment = 0,
                        PaymentType = "none",
                    };

                    var contrAgentPayment = _mapper.Map<ContrAgentPayment>(contrAgentPaymentDto);

                    var result = await _unitOfWork.ContrAgentPayment.Add(contrAgentPayment);

                    if (!result)
                        throw new StatusCodeException(HttpStatusCode.NotFound, "Error occurred while adding a contrAgentPayment.");
                }
                else
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "ContrAgent not found.");
                }

                var productExists = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .Where(p => p.Barcode == dto.BarCode)
                    .FirstOrDefaultAsync();

                if (productExists != null)
                {
                    int count = productExists.Count;
                    var updatedProduct = _mapper.Map(dto, productExists);
                    updatedProduct.Count = count + dto.Count;

                    await _unitOfWork.Product.Update(updatedProduct);
                    return updatedProduct.Id;
                }

                string pCode = await GenerateUniquePCodeAsync();

                var product = _mapper.Map<Et.Product>(dto);
                product.PCode = pCode;

                await _unitOfWork.Product.Add(product);
                await _unitOfWork.SaveAsync();

                var loadReport = new AddLoadReportDto
                {
                    WorkerId = dto.WorkerId,
                    ContrAgentId = dto.ContrAgentId,
                    ProductId = product.Id,
                    TotalPrice = product.SellPrice * product.Count,
                    Count = product.Count
                };

                await _loadReportService.AddAsync(loadReport);

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

        public async Task<IList<FullProductDto>> GetAllAsync(PaginationParams paginationParams)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                                        .AsNoTracking()
                                        .ToPagedListAsync(paginationParams);

                var productDtos = products.Select(p => new FullProductDto
                {
                    Id = p.Id,
                    PCode = p.PCode,
                    Name = p.Name,
                    Barcode = p.Barcode,
                    Price = p.Price,
                    SellPrice = p.SellPrice,
                    Count = p.Count,
                    UnitOfMeasure = p.UnitOfMeasure,
                    CategoryId = p.Category.Id,
                    CategoryName = p.Category.Name,
                    WorkerId = p.Worker.Id,
                    WorkerFirstName = p.Worker.FirstName,
                    WorkerLastName = p.Worker.LastName,
                    ProductImages = p.ProductImages.Select(img => new ProductImageDto
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath!
                    }).ToList(),
                    NoteAmount = p.NoteAmount
                }).ToList();

                return productDtos;
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

                var contrAgentExists = await _unitOfWork.ContrAgent.GetById(dto.ContrAgentId) != null;
                if (!contrAgentExists)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "Contr Agent not found.");
                }

                var originalCount = product.Count;
                _mapper.Map(dto, product);
                product.Count = originalCount + dto.Count;

                var asda = await _unitOfWork.Product.Update(product);
                return asda;
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
        public async Task<FullProductDto> GetProductByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .Where(p => p.Barcode == barcode)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No products found with the specified barcode.");
                }

                var fullProductDtos = new FullProductDto
                {
                    Id = product.Id!,
                    PCode = product.PCode,
                    Name = product.Name,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    SellPrice = product.SellPrice,
                    Count = product.Count,
                    UnitOfMeasure = product.UnitOfMeasure,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name,
                    WorkerId = product.Worker.Id,
                    WorkerFirstName = product.Worker.FirstName,
                    WorkerLastName = product.Worker.LastName,
                    ProductImages = product.ProductImages.Select(img => new ProductImageDto
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath!
                    }).ToList(),
                    NoteAmount = product.NoteAmount
                };

                return fullProductDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products by barcode.");
                throw;
            }
        }



        public async Task<IList<FullProductDto>> GetProductByPCodeAsync(string pCode)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .Where(p => p.PCode == pCode)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No products found with the specified PCode.");
                }

                var fullProductDtos = products.Select(product => new FullProductDto
                {
                    Id = product.Id,
                    PCode = product.PCode,
                    Name = product.Name,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    SellPrice = product.SellPrice,
                    Count = product.Count,
                    UnitOfMeasure = product.UnitOfMeasure,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name,
                    WorkerId = product.Worker.Id,
                    WorkerFirstName = product.Worker.FirstName,
                    WorkerLastName = product.Worker.LastName,
                    ProductImages = product.ProductImages.Select(img => new ProductImageDto
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath!
                    }).ToList(),
                    NoteAmount = product.NoteAmount
                }).ToList();

                return fullProductDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products by PCode.");
                throw;
            }
        }



        public async Task<IList<FullProductDto>> GetProductByWorkerIdAsync(Guid workerId)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .Where(p => p.Worker.Id == workerId)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No products found for the specified worker ID.");
                }

                var fullProductDtos = products.Select(product => new FullProductDto
                {
                    Id = product.Id,
                    PCode = product.PCode,
                    Name = product.Name,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    SellPrice = product.SellPrice,
                    Count = product.Count,
                    UnitOfMeasure = product.UnitOfMeasure,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name,
                    WorkerId = product.Worker.Id,
                    WorkerFirstName = product.Worker.FirstName,
                    WorkerLastName = product.Worker.LastName,
                    ProductImages = product.ProductImages.Select(img => new ProductImageDto
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath!
                    }).ToList(),
                    NoteAmount = product.NoteAmount
                }).ToList();

                return fullProductDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products by worker ID.");
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

        public async Task<IList<FullProductDto>> GetProductsByNameAsync(string name)
        {
            try
            {
                var products = await _unitOfWork.Product.GetAllProductsFullInformation()
                    .AsNoTracking()
                    .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    throw new StatusCodeException(HttpStatusCode.NotFound, "No products found.");
                }

                var fullProductDtos = products.Select(product => new FullProductDto
                {
                    Id = product.Id,
                    PCode = product.PCode,
                    Name = product.Name,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    SellPrice = product.SellPrice,
                    Count = product.Count,
                    UnitOfMeasure = product.UnitOfMeasure,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name,
                    WorkerId = product.Worker.Id,
                    WorkerFirstName = product.Worker.FirstName,
                    WorkerLastName = product.Worker.LastName,
                    ProductImages = product.ProductImages.Select(img => new ProductImageDto
                    {
                        Id = img.Id,
                        ImagePath = img.ImagePath!
                    }).ToList(),
                    NoteAmount = product.NoteAmount
                }).ToList();

                return fullProductDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting products by name.");
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

        public async Task<FullProductDto> GetByIdAsync(Guid Id)
        {
            var product = await _unitOfWork.Product.GetProductsFullInformationAsync();

            var productDto = product.FirstOrDefault(x => x.Id == Id);

            var productResultDto = new FullProductDto
            {
                Id = productDto!.Id,
                PCode = productDto.PCode,
                Name = productDto.Name,
                Barcode = productDto.Barcode,
                Price = productDto.Price,
                SellPrice = productDto.SellPrice,
                Count = productDto.Count,
                UnitOfMeasure = productDto.UnitOfMeasure,
                CategoryId = productDto.Category.Id,
                CategoryName = productDto.Category.Name,
                WorkerId = productDto.Worker.Id,
                WorkerFirstName = productDto.Worker.FirstName,
                WorkerLastName = productDto.Worker.LastName,
                ProductImages = productDto.ProductImages.Select(img => new ProductImageDto
                {
                    Id = img.Id,
                    ImagePath = img.ImagePath!
                }).ToList(),
                NoteAmount = productDto.NoteAmount
            };

            return productResultDto;
        }

        public async Task<bool> UpdateProductCountAsync(List<UpdateProductDto> items)
        {
            try
            {
                var updatedProducts = new List<Et.Product>();

                foreach (var item in items)
                {
                    var product = await _unitOfWork.Product.GetById(item.ProductId)
                              ?? throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");

                    product.Count += item.IsIncrement ? item.Count : -item.Count;

                    updatedProducts.Add(product);
                }

                var result = await _unitOfWork.Product.UpdateRange(updatedProducts);

                if (result)
                {
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product counts.");
                throw;
            }

        }

    }
}