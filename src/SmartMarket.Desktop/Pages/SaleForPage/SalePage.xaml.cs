using SmartMarket.Desktop.Components.SaleForComponent;
using SmartMarket.Desktop.ViewModels.Transactions;
using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Desktop.Windows.Sales;
using SmartMarket.Desktop.Windows.Settings;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Pages.SaleForPage;

/// <summary>
/// Interaction logic for SalePage.xaml
/// </summary>
public partial class SalePage : Page
{
    private readonly IProductService _productService;

    private System.Timers.Timer timer = new System.Timers.Timer();

    private DispatcherTimer time;

    TransactionViewModel tvm;
    string message = "";


    int activeTextboxIndex = 2;
    int productCount = 1;
    string barcode = "";
    string barcodes = "";

    public SalePage()
    {
        InitializeComponent();
        this.tvm = new TransactionViewModel();
        this._productService = new ProductService();

        timer.Elapsed += vaqt_ketdi;
        timer.Interval = 500;
        timer.Enabled = true;

        time = new DispatcherTimer();
        time.Interval = TimeSpan.FromMilliseconds(200);
        time.Tick += Timer_Tick;
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.BottomCenter,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private void Timer_Tick(object sender, EventArgs e)
    {
        timer.Stop();
        ProcessBarcode(barcode);
        barcode = "";
        barcodes = "";
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

        var button = (Button)sender;
        WriteNumber(button.Content.ToString() ?? "");

    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        tbCalculator.Text = string.Empty;
    }

    private void btnBackKlav_Click(object sender, RoutedEventArgs e)
    {
        if(activeTextboxIndex == 2 && tbCalculator.Text.Length > 0)
        {
            tbCalculator.Text = tbCalculator.Text.Substring(0, tbCalculator.Text.Length - 1);
        }
        else if(activeTextboxIndex == 3)
        {
            if (selectedControl.tbDiscount.Text.Length == 1)
                selectedControl.tbDiscount.Text = "0";
            else if (selectedControl.tbDiscount.Text.Length > 1)
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text.Substring(0, selectedControl.tbDiscount.Text.Length - 1);
        }
    }

    private void btnReturnProduct_Click(object sender, RoutedEventArgs e)
    {
        ReturnProductWindow returnProductWindow = new ReturnProductWindow();
        returnProductWindow.ShowDialog();
    }

    private void btnPay_Click(object sender, RoutedEventArgs e)
    {
        PaymentTypeWindow paymentTypeWindow = new PaymentTypeWindow();
        paymentTypeWindow.ShowDialog();
    }

    public void GetData()
    {
        tbDate.Text = DateTime.UtcNow.Month + "." + DateTime.UtcNow.Day + "." + DateTime.UtcNow.Year;
        tbhour.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }

    private void vaqt_ketdi(object sender, ElapsedEventArgs e)
    {
        this.Dispatcher.Invoke(() =>
        {
            if (tbhour.Opacity == 1)
            {
                tbhour.Opacity = 1;
            }
            else
                tbhour.Opacity = 0.5;
            GetData();
        });
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        activeTextboxIndex = int.Parse(((TextBox)sender).Uid);
    }

    private void WriteNumber(string number)
    {
        if (activeTextboxIndex == 2)
        {
            tbCalculator.Text = tbCalculator.Text.ToString() + number;
        }
        else if(activeTextboxIndex == 3)
        {
            if(selectedControl.tbDiscount.Text == "0")
            {
                selectedControl.tbDiscount.Text = "";
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text + number;
            }
            else
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text + number;
        }
    }

    private void Page_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
                AddNewProductTvm(product);
            }
            else
            {
                notifier.ShowWarning("Bunday maxsulot topilmadi.");
            }
        }
    }

    public void AddNewProductTvm(ProductDto product)
    {
        string barcode = product.Barcode;
        if (!tvm.Transactions.Any(t => t.Barcode == barcode))
        {
            tvm.Add(product);
            AddNewProduct(product);
        }
        else
        {
            double totalPrice = 0;
            double discountPrice = 0;
            foreach (SaleProductForComponent child in St_product.Children)
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

    private void AddNewProduct(ProductDto product)
    {
        SaleProductForComponent saleProductForComponent = new SaleProductForComponent();
        int quantity;
        if (string.IsNullOrEmpty(tbCalculator.Text))
            quantity = 1;
        else
            quantity = int.Parse(tbCalculator.Text!);
        
        saleProductForComponent.DataContext = new TransactionDto
        {
            Name = product.Name,
            Barcode = product.Barcode,
            Price = product.SellPrice,
            TotalPrice = product.SellPrice,
            AvailableCount = product.Count,
            Discount = 0,
            Quantity = quantity,
        };

        GetPrice(product, quantity);
        ColculateTotalPrice();

        saleProductForComponent.SetData(product, productCount);
        St_product.Children.Add(saleProductForComponent);
        productCount++;
    }

    private void Nasiya_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnersNationWindow nationWindow = new PartnersNationWindow();
        nationWindow.ShowDialog();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetData();
        St_product.Focus();
    }

    private void Harajat_Click(object sender, RoutedEventArgs e)
    {
        ExpensesWindow expensesWindow = new ExpensesWindow();
        expensesWindow.ShowDialog();
    }

    private void Hamkorlar_Click(object sender, RoutedEventArgs e)
    {
        PartnersWindow partnersWindow = new PartnersWindow();
        partnersWindow.ShowDialog();
    }

    private void Sozlamalar_Click(object sender, RoutedEventArgs e)
    {
        SettingPrinterWindow settingPrinterWindow = new SettingPrinterWindow();
        settingPrinterWindow.ShowDialog();
    }

    private void Sotuv_Tarixi_Click(object sender, RoutedEventArgs e)
    {
        SaleHistoryWindow saleHistoryWindow = new SaleHistoryWindow();
        saleHistoryWindow.ShowDialog();
    }

    private void Log_Out_Click(object sender, RoutedEventArgs e)
    {

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
        if(selectedControl != null)
        {
            if (int.Parse(selectedControl.tbQuantity.Text) < selectedControl.AvailableCount)
            {
                foreach (var item in tvm.Transactions)
                {
                    if(item.Barcode == selectedControl.Barcode)
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

    // Jami summani hisoblash uchun
    private void ColculateTotalPrice()
    {
        if (tvm != null)
        {
            tvm.TransactionPrice = tvm.Transactions.Sum(x => x.TotalPrice);
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

    // Har bir maxsulotni narxini hisoblash uchun
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

    private void GetPrice(ProductDto product, int quantity)
    {
        Product_Count.Text = quantity.ToString();
        Product_Price.Text = product.SellPrice.ToString();
        Total_Price.Text = (quantity * product.SellPrice).ToString();
        Product_Name.Text = product.Name;
        Product_Barcode.Text = product.Barcode.ToString();
    }

    private void EmptyPrice()
    {
        Product_Count.Text = "0";
        Product_Price.Text = "0";
        Total_Price.Text = "0";
        Product_Name.Text = "";
        Product_Barcode.Text = "";
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
        if(selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
            selectedControl = null!;
        }
        SearchProductWindow searchProductWindow = new SearchProductWindow();
        searchProductWindow.ShowDialog();
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        tvm = null!;
    }
}
