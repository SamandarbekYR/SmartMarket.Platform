using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Sales;

/// <summary>
/// Interaction logic for SaleHistoryWindow.xaml
/// </summary>
public partial class SaleHistoryWindow : Window
{
    private IProductSaleService _productSaleService;
    public SaleHistoryWindow()
    {
        InitializeComponent();
        _productSaleService = new ProductSaleService();
    }

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    internal void EnableBlur()
    {
        var windowHelper = new WindowInteropHelper(this);

        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData();
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
        data.SizeOfData = accentStructSize;
        data.Data = accentPtr;

        SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        Marshal.FreeHGlobal(accentPtr);
    }

    public async void GetAllProduct()
    {
        var productSales = await Task.Run(async () => await _productSaleService.GetAllAsync());

        List<string> workerNames = productSales
            .Select(ps => ps.SalesRequest.Worker.FirstName)
            .Distinct()
            .ToList();

        workerNames.Insert(0, "Barcha sotuvchi");
        workerComboBox.ItemsSource = workerNames;

        var today = DateTime.Today;
        productSales = productSales.Where(ps => ps.CreatedDate.Value.Date == today).ToList();

        ShowProductSales(productSales);
    }

    private async void FilterProductSales()
    {
        FilterProductSaleDto filterProductSaleDto = new FilterProductSaleDto();

        if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
        {
            filterProductSaleDto.FromDateTime = fromDateTime.SelectedDate.Value;
            filterProductSaleDto.ToDateTime = toDateTime.SelectedDate.Value;
        }

        var selectedWorkerName = workerComboBox.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedWorkerName) && !selectedWorkerName.Equals("Barcha sotuvchi"))
        {
            filterProductSaleDto.WorkerName = selectedWorkerName;
        }

        var searchTerm = searchTextBox.Text.ToLower();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            filterProductSaleDto.ProductName = searchTerm;
        }

        var filteredProductSales = await Task.Run(async () => await _productSaleService.FilterProductSaleAsync(filterProductSaleDto));

        ShowProductSales(filteredProductSales);
    }

    private async void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
    {
        productSales = productSales.Where(ps => ps.Count != 0)
            .OrderByDescending(ps => ps.CreatedDate).ToList();

        St_productList.Visibility = Visibility.Visible;
        St_productList.Children.Clear();
        int rowNumber = 1;

        foreach (var item in productSales)
        {
            ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
            shopDetailsProductComponent.Tag = item;
            shopDetailsProductComponent.SetValues(
                rowNumber,
                item.SalesRequest.TransactionId,
                item.Product.Name,
                item.Product.SellPrice,
                item.Count,
                item.ItemTotalCost);

            shopDetailsProductComponent.BorderThickness = new Thickness(2);
            St_productList.Children.Add(shopDetailsProductComponent);
            rowNumber++;
        }
    }

    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterProductSales();
    }

    private void WorkerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FilterProductSales();
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            FilterProductSales();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        GetAllProduct();
    }

    private void btnclose_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.Close();
    }
}
