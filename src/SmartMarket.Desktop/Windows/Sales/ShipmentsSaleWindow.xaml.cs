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

namespace SmartMarket.Desktop.Windows.Sales;

/// <summary>
/// Interaction logic for ShipmentsSaleWindow.xaml
/// </summary>
public partial class ShipmentsSaleWindow : Window
{

    public TransactionViewModel tvm;

    int activeTextboxIndex = 2;
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
        this.tvm = new TransactionViewModel();
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
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private SaleProductForComponent selectedControl = null!;
    public void SelectProduct(SaleProductForComponent product)
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

    private void percent_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            activeTextboxIndex = 3;
            float discount = int.Parse(selectedControl.tbDiscount.Text);

            if (discount >= 0)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedControl.Barcode)
                    {
                        item.Discount = discount;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;

                        selectedControl.tbDiscount.Text = item.Discount.ToString();
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

}
