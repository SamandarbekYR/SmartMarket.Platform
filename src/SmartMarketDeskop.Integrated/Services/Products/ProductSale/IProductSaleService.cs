﻿using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductSale
{
    public interface IProductSaleService
    {
        Task<List<ProductSaleViewModel>> GetAllAsync();
    }
}