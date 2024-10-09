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

namespace SmartMarket.Service.Services.Products.Product;

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

        string pCode = await GenerateUniquePCodeAsync();

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
        var products = await _unitOfWork.Product.GetAll().AsNoTracking().ToPagedListAsync(@params);
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

    public async Task<ProductDto> GetProductByWorkerIdAsync(Guid workerId)
    {
        var product = await _unitOfWork.Product.GetAll()
            .FirstOrDefaultAsync(p => p.WorkerId == workerId);

        if (product == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> SellProductAsync(string barcode)
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

    public async Task<ProductDto> GetProductByNameAsync(string name)
    {
        var product = await _unitOfWork.Product.GetAll()
            .FirstOrDefaultAsync(p => p.Name == name);

        if (product == null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found.");
        }

        return _mapper.Map<ProductDto>(product);
    }
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, PaginationParams @params)
    {
        var products = await _unitOfWork.Product.GetAll()
            .Where(p => p.CategoryId == categoryId)
            .AsNoTracking()
            .ToPagedListAsync(@params);
        return products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
    }

    public async Task<IEnumerable<Et.Product>> GetProductsFullInformationAsync(PaginationParams paginationParams)
    {
        var products = await _unitOfWork.Product.GetAllProductsFullInformation()
            .AsNoTracking()
            .ToPagedListAsync(paginationParams);
        return _mapper.Map<IEnumerable<Et.Product>>(products);
    }

    public async Task<IEnumerable<Et.Product>> GetProductsWithRequiredInformationAsync(PaginationParams paginationParams)
    {
        var products = await _unitOfWork.Product.GetProductsWithRequiredInformationAsync()
            .AsNoTracking()
            .ToPagedListAsync(paginationParams);
        return _mapper.Map<IEnumerable<Et.Product>>(products);
    }
}
