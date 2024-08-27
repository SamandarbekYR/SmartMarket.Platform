using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Desktop.Windows.AccountSettings;
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

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage
{
    /// <summary>
    /// Interaction logic for ShopWorkersPage.xaml
    /// </summary>
    public partial class ShopWorkersPage : Page
    {
        public ShopWorkersPage()
        {
            InitializeComponent();
            GetWorker();
            GetWorkers();
            CollectedCargoPageView();
            WorkersoldProduct();
        }



        public void GetWorker()
        {
            WorkerAccount.Visibility = Visibility.Visible;
            WorkerAccount.Children.Clear();

            ShopWorkerAccountComponent shopWorkerAccountComponent = new ShopWorkerAccountComponent();   
            shopWorkerAccountComponent.Tag= this;
            shopWorkerAccountComponent.SetValues("Sherzod", "Aliyev", "4,000,000", "+998 906661132", "300,000", "3,700,000");
            shopWorkerAccountComponent.BorderThickness=new Thickness(5);
            WorkerAccount.Children.Add(shopWorkerAccountComponent);
        }



        public void GetWorkers()
        {
            St_Workers.Visibility = Visibility.Visible;
            St_Workers.Children.Clear();




            WorkerListComponent workerListComponent = new WorkerListComponent();    
            workerListComponent.Tag = this;
            workerListComponent.SetValues("1", "Sherzod");
            workerListComponent.BorderThickness = new Thickness(3,2,3,2);
            St_Workers.Children.Add(workerListComponent);

        }

        public void CollectedCargoPageView()
        {
            CollectedCargoDetailsPage collectedCargoDetailsPage = new CollectedCargoDetailsPage();
            CollectedCargoPageNavigator.Content = collectedCargoDetailsPage;
        }

        public void WorkersoldProduct()
        {
            WorkerSoldProductPage workerSoldProductPage = new WorkerSoldProductPage();
            WorkerSoldProductPageNavigator.Content= workerSoldProductPage;
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            AccountCreateWindow accountCreateWindow = new AccountCreateWindow();    
            accountCreateWindow.ShowDialog();
        }
    }
}