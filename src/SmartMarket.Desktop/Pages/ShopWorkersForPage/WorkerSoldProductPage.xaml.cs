using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Services.Workers.Worker;

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
    /// Interaction logic for WorkerSoldProductPage.xaml
    /// </summary>
    public partial class WorkerSoldProductPage : UserControl
    {
        private IWorkerService _workerService;

        public WorkerSoldProductPage()
        {
            InitializeComponent();
            _workerService = new WorkerService();
        }

        public async void LoadData()
        {
            var workers = await _workerService.GetAllAsync();
            ShowData(workers);
        }

        private void ShowData(List<WorkerDto> workers)
        {
            var prodcutSales = workers.SelectMany(x => x.ProductSales).ToList();

            int rowNumber = 1;
            foreach (var worker in workers)
            {
                //WorkerListComponent workerListComponent = new WorkerListComponent();
                //workerListComponent.SetValues(worker.Id.ToString(), worker.Name);
                //WorkerList.Children.Add(workerListComponent);

               foreach (var productSale in worker.ProductSales)
               {
                    //ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                    //shopDetailsProductComponent.SetValues(productSale.Id, productSale.TransactionNumber, productSale.ProductName, productSale.Price, productSale.Count, productSale.TotalPrice);
                    //shopDetailsProductComponent.Tag = productSale;
                    //shopDetailsProductComponent.Return_Button.Click += Return_Button_Click;
                    //ShopDetails.Children.Add(shopDetailsProductComponent);
                }

                rowNumber++;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
