using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Components.SaleForComponent;
using SmartMarket.Desktop.ViewModels.Transactions;
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
        if (tbCalculator.Text.Length > 0)
        {
            tbCalculator.Text = tbCalculator.Text.Substring(0, tbCalculator.Text.Length - 1);
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
    }

    private void Page_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        barcodes += e.Text;

        if (e.Text == "\r")
        {
            if (barcodes.Length >= 2)
            {
                barcode = barcodes.Substring(0, barcodes.Length - 2);
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
                if (!tvm.Transactions.Any(t => t.Barcode == barcode))
                {
                    tvm.Add(product);
                    AddNewProduct(product);
                }
                else
                    tvm.Increment(barcode);
            }

        }
    }

    private void AddNewProduct(ProductDto product)
    {
        SaleProductForComponent saleProductForComponent = new SaleProductForComponent();
        saleProductForComponent.DataContext = new TransactionDto
        {
            Name = product.Name,
            Barcode = product.Barcode,
            Price = product.Price,
            Quantity = 1,
            TotalPrice = product.Price,
            AvailableCount = product.Count,
            Discount = 0
        };
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
    public void SelectCategory(SaleProductForComponent product)
    {
        if (selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
        }

        product.product_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6")); ;

        selectedControl = product;
    }

    private void delete_button_Click(object sender, RoutedEventArgs e)
    {
        
    }

    private void plus_button_Click(object sender, RoutedEventArgs e)
    {
        //if(selectedControl != null)
        //{
        //    selectedControl.tbQuantity.Text = (int.Parse(selectedControl.tbQuantity.Text) + 1).ToString();
        //    selectedControl.tbTotalPrice.Text = (double.Parse(selectedControl.tbQuantity.Text) * double.Parse(selectedControl.tbPrice.Text)).ToString();
        //}

        if(selectedControl != null)
        {
            if (int.Parse(selectedControl.tbQuantity.Text) < selectedControl.AvailableCount)
            {
                foreach (var item in tvm.Transactions)
                {
                    if(item.Barcode == selectedControl.Barcode)
                    {
                        item.Quantity++;
                        item.TotalPrice = item.Quantity * item.Price;
                    }
                }
            }
        }

    }

    private void minus_button_Click(object sender, RoutedEventArgs e)
    {
        //if (selectedControl != null)
        //{
        //    int quantity = int.Parse(selectedControl.tbQuantity.Text);
        //    if (quantity > 1)
        //    {
        //        selectedControl.tbQuantity.Text = (quantity - 1).ToString();
        //        selectedControl.tbTotalPrice.Text = (double.Parse(selectedControl.tbQuantity.Text) * double.Parse(selectedControl.tbPrice.Text)).ToString();
        //    }
        //}

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
                        item.TotalPrice = item.Quantity * item.Price;
                    }
                }
            }
        }

    }

    private void percent_button_Click(object sender, RoutedEventArgs e)
    {

    }

    private void search_button_Click(object sender, RoutedEventArgs e)
    {
        SearchProductWindow searchProductWindow = new SearchProductWindow();
        searchProductWindow.ShowDialog();
    }
}
