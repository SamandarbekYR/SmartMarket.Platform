using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.SaleForPage
{
    /// <summary>
    /// Interaction logic for SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        private System.Timers.Timer timer = new System.Timers.Timer();


        int activeTextboxIndex = 2;

        public SalePage()
        {
            InitializeComponent();
            GetData();
            timer.Elapsed += vaqt_ketdi;
            timer.Interval = 500;
            timer.Enabled = true;
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
    }
}
