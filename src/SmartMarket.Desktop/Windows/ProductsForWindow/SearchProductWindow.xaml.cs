using SmartMarket.Desktop.Components.SaleForComponent;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interop;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for SearchProductWindow.xaml
/// </summary>
public partial class SearchProductWindow : Window
{
    private readonly IProductService _productService;

    public SearchProductWindow()
    {
        InitializeComponent();
        this._productService = new ProductService();
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        tb_search.Focus();
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, @"^\d+$");
    }

    private void SetProduct(List<FullProductDto> product)
    {
        Loader.Visibility = Visibility.Collapsed;
        St_Products.Children.Clear();
        if (product.Count > 0)
        {
            foreach (var item in product)
            {
                SearchProductComponent searchProductComponent = new SearchProductComponent();
                searchProductComponent.Tag = item;
                searchProductComponent.SetData(item);
                St_Products.Children.Add(searchProductComponent);
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }
    private async void tb_search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        St_Products.Children.Clear();
        string search = tb_search.Text;

        await Task.Run(async () =>
        {
            if (IsNumeric(search) && search.Length >= 5)
            {
                var products = await _productService.GetByPCode(search);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SetProduct(products);
                });
            }
            else if (!IsNumeric(search) && search.Length >= 2)
            {
                var products = await _productService.GetByProductName(search);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SetProduct(products);
                });
            }
        });

    }
}
