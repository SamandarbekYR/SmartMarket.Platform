using SmartMarket.Desktop.Components.ShopDetailsAndExpensesComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for RunningOutOfProductsPage.xaml
    /// </summary>
    public partial class RunningOutOfProductsPage : Page
    {
        public RunningOutOfProductsPage()
        {
            InitializeComponent();
        }

        public async void GetAllRunningOutOfProducts()
        {
            RunningOutOfProductComponent runningOutOfProductComponent = new RunningOutOfProductComponent();
            St_Products.Children.Add(runningOutOfProductComponent);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllRunningOutOfProducts();
        }
    }
}
