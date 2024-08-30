using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface IProductImage : IRepository<ProductImageView>
{
    public Task<List<ProductImageView>> GetProductImagesFullInformationAsync();
}
