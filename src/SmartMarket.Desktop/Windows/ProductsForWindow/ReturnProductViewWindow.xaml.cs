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
    private Guid ProductId { get; set; }

    public ReturnProductViewWindow(Guid productId)
    {
        InitializeComponent();
        ProductId = productId;
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
        var productSale = await _productSaleService.GetByIdAsync(ProductId);

        if (productSale != null)
        {
            tb_Id.Text = productSale.Id.ToString();
            tb_PCode.Text = productSale.Product.PCode;
            tb_Description.Text = productSale.Product.Category.Description;
            tb_Transaction.Text = productSale.TransactionNumber.ToString();
            tb_Price.Text = productSale.Product.Price.ToString();
            tb_Quantity.Text = productSale.Count.ToString();
            tb_Total.Text = productSale.TotalCost.ToString();
            tb_Seller.Text = productSale.Worker.FirstName;
        }

        var workers = await _workerService.GetAllAsync();
        cb_Workers.DisplayMemberPath = "FirstName";  
        cb_Workers.SelectedValuePath = "Id";         
        cb_Workers.ItemsSource = workers;

    }

    private async void SaveReturnProduct_Button_Click(object sender, RoutedEventArgs e)
    {
        var replaceProductDto = new AddReplaceProductDto
        {
            ProductSaleId = Guid.TryParse(tb_Id.Text, out var productSaleId) ? productSaleId : Guid.Empty,
            WorkerId = cb_Workers.SelectedValue is Guid workerId ? workerId : Guid.Empty,
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
        if (replaceProductDto.Count <= 0)
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
            var productSale = await _productSaleService.GetByIdAsync(ProductId);
            AddProductSaleDto productSaleDto = new AddProductSaleDto()
            {
                ProductId = productSale.ProductId,
                WorkerId = (Guid)cb_Workers.SelectedValue,
                TransactionId = productSale.TransactionId,
                PayDeskId = productSale.PayDeskId,
                TransactionNumber = productSale.TransactionNumber,
                Count = productSale.Count - replaceProductDto.Count,
                TotalCost = productSale.TotalCost - productSale.Product.Price * replaceProductDto.Count,
                CashSum = productSale.CashSum,
                CardSum = productSale.CardSum,
                TransferMoney = productSale.TransferMoney,
                DebtSum = productSale.DebtSum
            };

            await _productSaleService.UpdateAsync(productSaleDto, productSale.Id);
        }
        else
        {
            MessageBox.Show("Saqlashda xatolik yuz berdi!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
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
