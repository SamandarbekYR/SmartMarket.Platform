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
using System.Reflection.Emit;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

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

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    Notifier notifierThis = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private async void GetProductSale()
    {
        if (_productSale != null)
        {
            tb_Id.Text = _productSale.Id.ToString();
            tb_PCode.Text = _productSale.Product.PCode;
            tb_Description.Text = _productSale.Product.Category.Description;
            tb_Transaction.Text = _productSale.SalesRequest.TransactionId.ToString();
            tb_Price.Text = _productSale.Product.Price.ToString();
            tb_Discount.Text = _productSale.Discount.ToString();
            tb_Quantity.Text = _productSale.Count.ToString();
            tb_Total.Text = _productSale.ItemTotalCost.ToString();
            tb_Seller.Text = _productSale.SalesRequest.Worker.FirstName;
            tb_Returner.Text = _productSale.Product.Worker?.FirstName;
        }
    }

    private async void SaveReturnProduct_Button_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(tb_Id.Text) &&
            !string.IsNullOrEmpty(tb_Seller.Text) &&
            !string.IsNullOrEmpty(tb_Cancel_Quantity.Text) &&
            !string.IsNullOrEmpty(tb_Reason.Text))
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
                notifierThis.ShowWarning("Product Sale Id noto'g'ri yoki kiritilmagan!");
            }
            if (replaceProductDto.WorkerId == Guid.Empty)
            {
                notifierThis.ShowWarning("Qaytarib oluvchi tanlanmagan!");
            }
            if (replaceProductDto.Count <= 0 || replaceProductDto.Count > _productSale.Count)
            {
                notifierThis.ShowWarning("Qaytarilgan mahsulot soni noto'g'ri yoki nolga teng!");
            }
            if (string.IsNullOrWhiteSpace(replaceProductDto.Reason))
            {
                notifierThis.ShowWarning("Sabab ko'rsatilmagan!");
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

            if (resultReplaceProduct || resultInvalidProduct)
            {
                AddProductSaleDto productSaleDto = new AddProductSaleDto()
                {
                    ProductId = _productSale.ProductId,
                    Count = _productSale.Count - replaceProductDto.Count,
                    Discount = _productSale.Discount,
                    SalesRequestId = _productSale.SalesRequestId,
                    ItemTotalCost = _productSale.ItemTotalCost - _productSale.Product.SellPrice * replaceProductDto.Count,
                };

                var result = await Task.Run(async () => await _productSaleService.UpdateAsync(productSaleDto, _productSale.Id));

                if (!result)
                {
                    notifierThis.ShowWarning("Product sale ma'lumotlarini yangilashda xatolik yuz berdi.");
                }
                else
                {
                    this.Close();
                    notifier.ShowSuccess("Product sale ma'lumotlari yangilandi.");
                }
            }
            else
            {
                notifierThis.ShowError("Saqlashda xatolik yuz berdi.");
            }
        }
        else
        {
            notifierThis.ShowWarning("Product sale ma'lumotlari to'liq emas!");
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

    private void tb_Cancel_Quantity_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (int.TryParse(tb_Cancel_Quantity.Text, out int value))
        {
            if (value > _productSale.Count)
            {
                tb_Cancel_Quantity.Text = _productSale.Count.ToString();
                tb_Cancel_Quantity.CaretIndex = tb_Cancel_Quantity.Text.Length; 
            }
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
