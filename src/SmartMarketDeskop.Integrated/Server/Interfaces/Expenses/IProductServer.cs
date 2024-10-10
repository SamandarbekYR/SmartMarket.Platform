using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses
{
    public interface IProductServer
    {
        Task<List<ProductView>> GetAllProduct();
    }
}
