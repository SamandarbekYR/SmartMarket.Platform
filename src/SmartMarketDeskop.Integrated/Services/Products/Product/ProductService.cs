using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Categories;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public class ProductService : IProductService
{
    private IProductServer productServer;

    private IWorkerServer workerServer;
    private ICategoryServer categoryServer;
    public ProductService()
    {
        this.productServer = new ProductServer();
        this.workerServer=new WorkerServer();
        this.categoryServer=new CategoryServer();

    }

    public async Task<bool> CreateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto)
    {
        if(IsInternetAvailable())
        {
            await productServer.AddAsync(dto);
            return true;    
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
            
            //var categorys=await categoryServer.GetAllAsync();
            //var workers=await workerServer.GetAllAsync();

            return products;
        }
        else
        {
            return new List<FullProductDto>(); 
        }
    
    }

    public Task<bool> UpdateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto product, Guid Id)
    {
        throw new NotImplementedException();
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


    public async Task<ProductDto> GetByBarCode(string barCode)
    {
        if (IsInternetAvailable())
        {
            var productDto  = await productServer.GetByBarCodeAsync(barCode);

            return productDto;
        }
        else
        {
            return new ProductDto();
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

    public async Task<List<ProductDto>> GetFinishedProducts()
    {
        if(IsInternetAvailable())
        {
            var finishedProducts = await productServer.GetFinishedProductsAsync();
            return finishedProducts;
        }
        else { return new List<ProductDto>(); }
    }
}
