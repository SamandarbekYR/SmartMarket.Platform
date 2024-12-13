﻿using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public class ProductService : IProductService
{
    private IProductServer productServer;
    public ProductService()
    {
        this.productServer = new ProductServer();
    }

    public async Task<bool> CreateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto)
    {
        if(IsInternetAvailable())
        {
            bool result = await productServer.AddAsync(dto);
            if(result) 
                return true;
            else 
                return false;
        }
        else
        {
            // local baza uchun malumot saqlanadi
            return false;
        }
    }

    public async Task<bool> DeleteProduct(Guid Id)
    {
        if (IsInternetAvailable())
        {
            await productServer.DeleteAsync(Id);
            return true;
        }
        else
        {

            // local bazaga saqlanadi
            return false;   
        }
    }

    public async Task<List<FullProductDto>> GetAll()
    {
        if (IsInternetAvailable())
        {
            var products = await productServer.GetAllAsync();

            return products;
        }
        else
        {
            return new List<FullProductDto>(); 
        }
    
    }

    public async Task<bool> UpdateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto product, Guid Id)
    {
        if (IsInternetAvailable())
        {
            await productServer.UpdateAsync(product, Id);
            return true;
        }
        else
        {
            // local bazaga saqlanadi
            return false;
        }
    }

    public bool IsInternetAvailable()
    {
        try
        {
            using (var client = new WebClient())
            using (client.OpenRead("http://google.com"))
                return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<FullProductDto> GetByBarCode(string barCode)
    {
        if (IsInternetAvailable())
        {
            var productDto  = await productServer.GetByBarCodeAsync(barCode);

            return productDto;
        }
        else
        {
            return null!;
        }
    }

    public async Task<List<FullProductDto>> GetByCategoryId(Guid categoryId)
    {
        if (IsInternetAvailable())
        {
            var products = await productServer.GetByCategoryIdAsync(categoryId);

            return products;
        }
        else
        {
            return new List<FullProductDto>();
        }
    }

    public async Task<List<FullProductDto>> GetByPCode(string PCode)
    {
        if (IsInternetAvailable())
        {
            var products = await productServer.GetByPCodeAsync(PCode);

            return products;
        }
        else
        {
            return new List<FullProductDto>();
        }
    }

    public async Task<List<FullProductDto>> GetByProductName(string productName)
    {
        if (IsInternetAvailable())
        {
            var products = await productServer.GetByProductNameAsync(productName);

            return products;
        }
        else
        {
            return new List<FullProductDto>();
        }
    }

    public async Task<List<FullProductDto>> GetFinishedProducts()
    {
        if(IsInternetAvailable())
        {
            var products = await productServer.GetFinishedProductsAsync();

            return products;
        }
        else
        {
            return null!;
        }
    }

    public async Task<bool> UpdateProductCountAsync(List<UpdateProductDto> dto)
    {
        if (IsInternetAvailable())
        {
            await productServer.UpdateProductCount(dto);
            return true;
        }
        else
        {
            return false;
        }
    }
}
