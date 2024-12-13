﻿using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductServer
{
    Task<List<FullProductDto>> GetAllAsync();
    Task<List<FullProductDto>> GetByCategoryIdAsync(Guid categoryId);
    Task<bool> AddAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto, Guid Id);
    Task<FullProductDto> GetByBarCodeAsync(string barcode);
    Task<List<FullProductDto>> GetByPCodeAsync(string PCode);    
    Task<List<FullProductDto>> GetByProductNameAsync(string productName);
    Task<List<FullProductDto>> GetFinishedProductsAsync();
    Task<bool> UpdateProductCount(List<UpdateProductDto> dto);
}

