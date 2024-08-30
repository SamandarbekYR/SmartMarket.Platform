using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface IProductImage : IRepository<ProductImageView>
{
    public Task<List<ProductImageView>> GetProductImagesFullInformationAsync();
}
