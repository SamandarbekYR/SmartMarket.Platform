using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Desktop.Components.ShopDetailsAndExpensesComponent;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
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
        private readonly IProductServer productServer;
        public RunningOutOfProductsPage()
        {
            InitializeComponent();
            this.productServer = new ProductServer();
        }

        public async void GetAllFinishedProducts()
        {
            //St_Products.Children.Clear();

            //var finishedProducts = await productServer.GetFinishedProductsAsync();

            //int count = 1;

            //if(finishedProducts != null)
            //{
            //    foreach(var item in finishedProducts)
            //    {
            //        RunningProductComponent runningProductComponent = new RunningProductComponent();
            //        runningProductComponent.SetData(item);
            //        St_Products.Children.Add(runningProductComponent);
            //        count++;
            //    }
            //}
            //else { }
            for(int i = 0; i < 20; i++)
            {
                RunningOutOfProductComponent runningOutOfProductComponent = new RunningOutOfProductComponent();
                St_Products.Children.Add(runningOutOfProductComponent);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllFinishedProducts();
        }
    }
}
