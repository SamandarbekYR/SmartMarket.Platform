﻿using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductSale;

public class ProductSaleService : IProductSaleService
{
    private IProductSaleServer productSaleServer;

    public ProductSaleService()
    {
        this.productSaleServer = new ProductSaleServer();
    }

    public Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto)
    {
        if (IsInternetAvailable())
        {
            return productSaleServer.FilterProductSaleAsync(dto);
        }
        else
        {
            return Task.FromResult(new List<ProductSaleViewModel>());
        }
    }

    public async Task<List<ProductSaleViewModel>> GetAllAsync()
    {
        if (IsInternetAvailable())
        {
            var products = await productSaleServer.GetAllAsync();

            return products;
        }
        else
        {
            return new List<ProductSaleViewModel>();
        }
    }

    public async Task<ProductSaleViewModel> GetByIdAsync(Guid id)
    {
        if (IsInternetAvailable())
        {
            var productSale = await productSaleServer.GetByIdAsync(id);
            return productSale;
        }
        else
        {
            return new ProductSaleViewModel();
        }
    }

    public Task<bool> UpdateAsync(AddProductSaleDto dto, Guid id)
    {
        if (IsInternetAvailable())
        {
            return productSaleServer.UpdateAsync(dto, id);
        }
        else
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        if (IsInternetAvailable())
        {
            return productSaleServer.DeleteAsync(id);
        }
        else
        {
            return Task.FromResult(false);
        }
    }

    public bool IsInternetAvailable()
    {
        try
        {
            using (var client = new WebClient()!)
            using (client.OpenRead("http://google.com"))
                return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> CreateAsync(AddProductSaleDto dto)
    {
        throw new NotImplementedException();
    }
}
