using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarketDeskop.Integrated.Services.Products.ProductSale;
using SmartMarketDeskop.Integrated.Services.Workers.Worker;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for ReturnProductViewWindow.xaml
/// </summary>
public partial class ReturnProductViewWindow : Window
{
    private IReplaceProductServer _replaceProductServer;
    private IInvalidProductServer _invalidProductServer;
    private IProductSaleService _productSaleService;
    private IWorkerService _workerService;
    private ProductSaleViewModel _productSale;

    public ReturnProductViewWindow(ProductSaleViewModel productSale)
    {
        InitializeComponent();
        _productSale = productSale;
        _replaceProductServer = new ReplaceProductServer();
        _invalidProductServer = new InvalidProductServer();
        _productSaleService = new ProductSaleService();
        _workerService = new WorkerService();
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

    private async void GetProductSale()
    {
        if (_productSale != null)
        {
            tb_Id.Text = _productSale.Id.ToString();
            tb_PCode.Text = _productSale.Product.PCode;
            tb_Description.Text = _productSale.Product.Category.Description;
            tb_Transaction.Text = _productSale.TransactionNumber.ToString();
            tb_Price.Text = _productSale.Product.Price.ToString();
            tb_Discount.Text = _productSale.Discount.ToString();
            tb_Quantity.Text = _productSale.Count.ToString();
            tb_Total.Text = _productSale.TotalCost.ToString();
            tb_Seller.Text = _productSale.Worker.FirstName;
            tb_Returner.Text = _productSale.Product.Worker.FirstName;
        }
    }

    private async void SaveReturnProduct_Button_Click(object sender, RoutedEventArgs e)
    {
        var replaceProductDto = new AddReplaceProductDto
        {
            ProductSaleId = Guid.TryParse(tb_Id.Text, out var productSaleId) ? productSaleId : Guid.Empty,
            WorkerId = _productSale.Product.WorkerId,
            Count = int.TryParse(tb_Cancel_Quantity.Text, out var count) ? count : 0,
            Reason = tb_Reason.Text
        };

        if (replaceProductDto.ProductSaleId == Guid.Empty)
        {
            MessageBox.Show("Product Sale Id noto'g'ri yoki kiritilmagan!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        if (replaceProductDto.WorkerId == Guid.Empty)
        {
            MessageBox.Show("Qaytarib oluvchi tanlanmagan!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        if (replaceProductDto.Count <= 0 || replaceProductDto.Count > _productSale.Count)
        {
            MessageBox.Show("Qaytarilgan miqdori noto'g'ri yoki nolga teng!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        if (string.IsNullOrWhiteSpace(replaceProductDto.Reason))
        {
            MessageBox.Show("Sabab ko'rsatilmagan!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        bool resultInvalidProduct = false, resultReplaceProduct;
        if (cb_Valid.Text.Equals("Ha"))
        {
            resultReplaceProduct = await _replaceProductServer.AddAsync(replaceProductDto);
        }
        else
        {
            resultInvalidProduct = await _invalidProductServer.AddAsync(replaceProductDto);
            resultReplaceProduct = await _replaceProductServer.AddAsync(replaceProductDto);
        }

        if(resultReplaceProduct || resultInvalidProduct)
        {
            AddProductSaleDto productSaleDto = new AddProductSaleDto()
            {
                ProductId = _productSale.ProductId,
                WorkerId = _productSale.Product.WorkerId,
                TransactionId = _productSale.TransactionId,
                PayDeskId = _productSale.PayDeskId,
                TransactionNumber = _productSale.TransactionNumber,
                Count = _productSale.Count - replaceProductDto.Count,
                TotalCost = _productSale.TotalCost - _productSale.Product.SellPrice * replaceProductDto.Count,
                CashSum = _productSale.CashSum,
                CardSum = _productSale.CardSum,
                TransferMoney = _productSale.TransferMoney,
                DebtSum = _productSale.DebtSum
            };

            var result =  await _productSaleService.UpdateAsync(productSaleDto, _productSale.Id);
            
            if(!result)
            {
                MessageBox.Show("Product Sale ma'lumotlari yangilashda xatolik yuz berdi!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                this.Close();
            }
        }
        else
        {
            MessageBox.Show("Saqlashda xatolik yuz berdi!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void CountTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private void CountTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            string text = (string)e.DataObject.GetData(typeof(string));
            if (!IsTextAllowed(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private static bool IsTextAllowed(string text)
    {
        return text.All(char.IsDigit);
    }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        GetProductSale();
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
