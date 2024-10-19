using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.ViewModels.Products;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent
{
    /// <summary>
    /// Interaction logic for WorkerSoldProductComponent.xaml
    /// </summary>
    public partial class WorkerSoldProductComponent : UserControl
    {
        public WorkerSoldProductComponent()
        {
            InitializeComponent();
        }

        public void SetValues(int id, string productName, double price, int count)
        {
            lb_Count.Content = id.ToString();
            lb_Price.Content = price.ToString();
            lb_Productname.Content = productName;
            lb_Product_Count.Content = count.ToString();
            //lb_Total_Price.Content = totalPrice.ToString();
            //lb_Transaction.Content = transactionNumber.ToString();
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            var productSale = this.Tag as ProductSaleViewModel;

            ReturnProductViewWindow returnProductViewWindow = new ReturnProductViewWindow(productSale);
            returnProductViewWindow.ShowDialog();
        }
    }
}
