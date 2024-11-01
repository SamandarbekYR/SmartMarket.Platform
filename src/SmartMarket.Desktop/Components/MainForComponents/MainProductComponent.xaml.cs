using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainProductComponent.xaml
/// </summary>
public partial class MainProductComponent : UserControl
{
    private IProductService productService;
    FullProductDto products = null!;
    public MainProductComponent()
    {
        InitializeComponent();
        this.productService = new ProductService();
    }
    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    public void GetData(FullProductDto product, int count)
    {
        tbNumber.Text = count.ToString();
        tbP_Code.Text = product.PCode;
        TbBarcode.Text = product.Barcode;
        tbProductName.Text = product.Name;
        tbPrice.Text = product.SellPrice.ToString();
        tbCount.Text = product.Count.ToString();

    }

    private void btnedit_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        products = (this.Tag as FullProductDto)!;
        var window = new ProductUpdateWindow();
        window.SetData(products);
        window.ShowDialog();
    }

    private async void btndelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        products = (this.Tag as FullProductDto)!;

        var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);
        if(messageBoxResult == MessageBoxResult.Yes)
        {
            var res = await productService.DeleteProduct(products!.Id);

            if (res)
                notifier.ShowInformation("Mahsulot muvaffaqiyatli o'chirildi.");
            else
                notifier.ShowError("Xato yuz berdi.");
        }
    }

    private void btnDocument_Click(object sender, RoutedEventArgs e)
    {
        products = (this.Tag as FullProductDto)!;
        var window = new RunningOutOfProductDetailWindow();
        window.SetData(products);
        window.ShowDialog();
    }
}
