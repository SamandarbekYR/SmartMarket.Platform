using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SmartMarket.Desktop.Components.ExpenseForComponents;

/// <summary>
/// Interaction logic for RunningProductComponent.xaml
/// </summary>
public partial class RunningProductComponent : UserControl
{
    private readonly IProductService productService;
    public RunningProductComponent()
    {
        InitializeComponent();
        this.productService = new ProductService();
    }
    public Guid ProductId { get; set; }

    public void SetData(FullProductDto dto)
    {
        var imgPath = @"D:\SmartPartnersProjects\src\SmartMarket.Desktop\Assets\Basket.png";

        tbName.Content = dto.Name;
        tbCount.Content = dto.Count;
        tbPrice.Content = dto.Price;
        productImg.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute)); //imgPath dto yoqligi uchun testga localdan
    }
}
