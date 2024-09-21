using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SmartMarket.Desktop.Pages.SaleForPage;

/// <summary>
/// Interaction logic for SalePage.xaml
/// </summary>
public partial class SalePage : Page
{
    private System.Timers.Timer timer = new System.Timers.Timer();

    private DispatcherTimer time;


    int activeTextboxIndex = 2;
    string barcode = "";
    string barcodes = "";

    public SalePage()
    {
        InitializeComponent();

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

    private void ProcessBarcode(string barcode)
    {
        if (!string.IsNullOrEmpty(barcode))
        {
            Label lblBarcode = new Label();
            lblBarcode.Content = barcode.ToString();
            lblBarcode.FontSize = 16;
            lblBarcode.HorizontalAlignment = HorizontalAlignment.Center;

            St_product.Children.Add(lblBarcode);

            scrollViewer.ScrollToEnd();
        }
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

    }

    private void Sotuv_Tarixi_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Log_Out_Click(object sender, RoutedEventArgs e)
    {

    }
}
