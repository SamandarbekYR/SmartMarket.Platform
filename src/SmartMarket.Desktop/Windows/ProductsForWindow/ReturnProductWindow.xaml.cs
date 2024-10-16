using SmartMarket.Desktop.Components.ShopDetailsForComponent;
using SmartMarket.Desktop.Pages.ShopDetailsForPage;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Services.Products.ProductSale;

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for ReturnProductWindow.xaml
/// </summary>
public partial class ReturnProductWindow : Window
{
    private IProductSaleService _productSaleService;
    public ReturnProductWindow()
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
        var productSales = await _productSaleService.GetAllAsync();

        List<string> workerNames = productSales
            .Select(ps => ps.Worker.FirstName)
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

        var filteredProductSales = await _productSaleService.FilterProductSaleAsync(filterProductSaleDto);

        ShowProductSales(filteredProductSales);
    }

    private void ShowProductSales(IEnumerable<ProductSaleViewModel> productSales)
    {
        productSales = productSales.Where(ps => ps.Count != 0)
            .OrderByDescending(ps => ps.CreatedDate).ToList();

        St_ReturnProduct.Visibility = Visibility.Visible;
        St_ReturnProduct.Children.Clear();
        int rowNumber = 1;

        foreach (var item in productSales)
        {
            ReturnProductComponent product = new ReturnProductComponent();
            product.Tag = item;
            product.SetValues(
                rowNumber,
                item.TransactionNumber,
                item.Product.Name,
                item.Product.SellPrice,
                item.Count,
                item.TotalCost,
                item.Discount,
                item.Worker.FirstName,
                item.CreatedDate);

            product.BorderThickness = new Thickness(2);
            St_ReturnProduct.Children.Add(product);
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

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        GetAllProduct();
    }                                   
}
