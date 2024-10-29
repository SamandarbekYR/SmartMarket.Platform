using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows.Controls;

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
        tbName.Content = dto.Name;
        tbCount.Content = dto.Count;
        tbPrice.Content = dto.Price;
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var dto = this.Tag as FullProductDto;
        RunningOutOfProductDetailWindow runningOutOfProductDetailWindow = new RunningOutOfProductDetailWindow();
        runningOutOfProductDetailWindow.SetData(dto!);
        runningOutOfProductDetailWindow.ShowDialog();
    }
}
