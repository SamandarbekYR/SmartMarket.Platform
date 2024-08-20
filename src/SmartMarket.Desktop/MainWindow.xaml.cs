using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Pages.SaleForPage;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }


        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            PageNavigator.Content = mainPage;
        }

        private void btnsale_Click(object sender, RoutedEventArgs e)
        {
            SalePage salePage=new SalePage();
            PageNavigator.Content = salePage;
        }
    }
}