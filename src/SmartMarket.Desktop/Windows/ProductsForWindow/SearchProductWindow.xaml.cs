using SmartMarket.Desktop.Components.SaleForComponent;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

    private void SetProduct(IList<FullProductDto> product)
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

    private CancellationTokenSource _cancellationTokenSource;

    private async void tb_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        string search = tb_search.Text;

        St_Products.Children.Clear();
        EmptyData.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            Loader.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(async () =>
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested) return;

                    IList<FullProductDto> products = new List<FullProductDto>();

                    if (IsNumeric(search) && search.Length >= 5)
                    {
                        products = await _productService.GetByPCode(search);
                    }
                    else if (!IsNumeric(search) && search.Length >= 1)
                    {
                        products = await _productService.GetByProductName(search);
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (search == tb_search.Text)
                        {
                            SetProduct(products);
                        }
                    });
                },
                _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                Loader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            St_Products.Children.Clear();
        }
    }
}
