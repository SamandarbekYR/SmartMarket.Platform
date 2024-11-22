using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using ToastNotifications;
using SmartMarket.Desktop.Components.SaleForComponent;
using System.Windows.Media;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using SmartMarket.Desktop.ViewModels.Transactions;
using SmartMarket.Service.DTOs.Products.Product;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;
using System.Windows.Threading;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using SmartMarketDeskop.Integrated.Services.Products.Product;

namespace SmartMarket.Desktop.Windows.Sales;

/// <summary>
/// Interaction logic for ShipmentsSaleWindow.xaml
/// </summary>
public partial class ShipmentsSaleWindow : Window
{
    private readonly IProductService _productService;

    private DispatcherTimer time;
    public TransactionViewModel tvm;

    int productCount = 1;

    string message = string.Empty;
    string barcode = string.Empty;
    string barcodes = string.Empty;
    public string PaymentType { get; set; } = string.Empty;
    public double TotalPrice { get; set; }
    public double CashSum { get; set; }
    public double ClickSum { get; set; }


    public ShipmentsSaleWindow()
    {
        InitializeComponent();

        this._productService = new ProductService();
        this.tvm = new TransactionViewModel();

        time = new DispatcherTimer();
        time.Interval = TimeSpan.FromMilliseconds(50);
        time.Tick += Timer_Tick!;
    }


    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.BottomCenter,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private void Timer_Tick(object sender, EventArgs e)
    {
        time.Stop();
        ProcessBarcode(barcode);
        barcode = "";
        barcodes = "";
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
        St_product.Focus();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private ShipmentSaleComponent selectedControl = null!;
    public void SelectProduct(ShipmentSaleComponent product)
    {
        if (selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
        }

        product.product_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
        EmptyPrice();
        ColculateTotalPrice();
        selectedControl = product;
    }

    private void delete_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            message = selectedControl.tbProductName.Text;
            foreach (var item in tvm.Transactions.ToList())
            {
                if (item.Barcode == selectedControl.Barcode)
                {
                    var messageBox = new MessageBoxWindow(message + "ni o'chirmoqchimisiz?", MessageType.Confirmation, MessageButtons.OkCancel);
                    var result = messageBox.ShowDialog();
                    if (result == true)
                    {
                        tvm.Transactions.Remove(item);
                        St_product.Children.Remove(selectedControl);
                        ColculateTotalPrice();
                        selectedControl.product_Border.Background = Brushes.White;
                        selectedControl = null!;
                        break;
                    }
                }
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void plus_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            if (int.Parse(selectedControl.tbQuantity.Text) < selectedControl.AvailableCount)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedControl.Barcode)
                    {
                        item.Quantity++;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedControl.tbQuantity.Text = item.Quantity.ToString();
                        selectedControl.tbTotalPrice.Text = item.TotalPrice.ToString();
                        ColculateTotalPrice();
                    }
                }
            }
            else
            {
                notifier.ShowInformation("Bu maxsulot tugadi.");
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }

    }

    private void minus_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            int quantity = int.Parse(selectedControl.tbQuantity.Text);

            if (quantity > 1)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedControl.Barcode)
                    {
                        item.Quantity--;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedControl.tbQuantity.Text = item.Quantity.ToString();
                        selectedControl.tbTotalPrice.Text = item.TotalPrice.ToString();
                        ColculateTotalPrice();
                    }
                }
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void search_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
            selectedControl = null!;
        }
        SearchProductWindow searchProductWindow = new SearchProductWindow();
        searchProductWindow.ShowDialog();
    }

    private void ColculateTotalPrice()
    {
        if (tvm != null)
        {
            TotalPrice = tvm.TransactionPrice = tvm.Transactions.Sum(x => x.TotalPrice);
            tvm.DiscountPrice = tvm.Transactions.Sum(x => x.DiscountSum);
            tbTotalAmount.Text = tvm.TransactionPrice.ToString();
            tbDiscountAmount.Text = tvm.DiscountPrice.ToString();
            tbAmount.Text = (tvm.TransactionPrice + tvm.DiscountPrice).ToString();
        }
        else
        {
            tbAmount.Text = "0";
            tbDiscountAmount.Text = "0";
            tbTotalAmount.Text = "0";
        }
    }

    private (double totalPrice, double discountprice) SetPrice(double price, float discount, int quantity)
    {
        double totalPrice = 0;
        double discountPrice = 0;

        if (discount > 0)
        {
            discountPrice = ((price / 100) * discount) * quantity;
            totalPrice = (quantity * price) - discountPrice;
        }
        else
        {
            totalPrice = quantity * price;
        }

        return (totalPrice, discountPrice);
    }

    private void GetPrice(FullProductDto product, int quantity)
    {
        Product_Count.Text = quantity.ToString();
        Product_Price.Text = product.SellPrice.ToString();
        Total_Price.Text = (quantity * product.SellPrice).ToString();
        Product_Name.Text = product.Name;
        Product_Barcode.Text = product.Barcode.ToString();
    }

    public void EmptyPrice()
    {
        Product_Count.Text = "0";
        Product_Price.Text = "0";
        Total_Price.Text = "0";
        Product_Name.Text = "";
        Product_Barcode.Text = "";
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void NationButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void SaleButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Window_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        barcodes += e.Text;

        if (e.Text == "\r")
        {
            if (barcodes.Length >= 2)
            {
                barcode = barcodes.Substring(0, barcodes.Length - 1);
            }
        }

        time.Stop();
        time.Start();
    }

    private async void ProcessBarcode(string barcode)
    {
        if (!string.IsNullOrEmpty(barcode))
        {
            var product = await _productService.GetByBarCode(barcode);

            if (product != null)
            {
                AddNewProductTvm(product, 0);
            }
            else
            {
                notifier.ShowWarning("Bunday maxsulot topilmadi.");
            }
        }
    }

    public void AddNewProductTvm(FullProductDto product, int count)
    {
        string barcode = product.Barcode;
        if (!tvm.Transactions.Any(t => t.Barcode == barcode))
        {
            if (count == 0)
                tvm.Add(product, 1);
            else
                tvm.Add(product, count);
            AddNewProduct(product, count);
        }
        else
        {
            double totalPrice = 0;
            double discountPrice = 0;
            foreach (ShipmentSaleComponent child in St_product.Children)
            {
                if (child.Barcode == barcode)
                {
                    int quantity = int.Parse(child.tbQuantity.Text);
                    if (quantity < child.AvailableCount)
                    {
                        quantity++;
                        (totalPrice, discountPrice) = SetPrice(double.Parse(child.tbPrice.Text), float.Parse(child.tbDiscount.Text), quantity);

                        child.tbTotalPrice.Text = totalPrice.ToString();
                        child.tbQuantity.Text = quantity.ToString();

                        GetPrice(product, quantity);
                    }
                }
            }
            tvm.Increment(barcode, totalPrice, discountPrice);
        }
        ColculateTotalPrice();
    }

    private void AddNewProduct(FullProductDto product, int quantity)
    {
        if (quantity == 0)
            quantity = 1;

        ShipmentSaleComponent saleProductForComponent = new ShipmentSaleComponent();
        saleProductForComponent.DataContext = new TransactionDto
        {
            Name = product.Name,
            Barcode = product.Barcode,
            Price = product.SellPrice,
            TotalPrice = product.SellPrice * quantity,
            AvailableCount = product.Count,
            Discount = 0,
            Quantity = quantity,
        };

        GetPrice(product, quantity);
        ColculateTotalPrice();

        saleProductForComponent.SetData(product);
        St_product.Children.Add(saleProductForComponent);
    }

}
