using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Service.DTOs.Workers.Worker;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage
{
    /// <summary>
    /// Interaction logic for WorkerSoldProductPage.xaml
    /// </summary>
    public partial class WorkerSoldProductPage : Page
    {
        public WorkerSoldProductPage()
        {
            InitializeComponent();
        }

        private WorkerListComponent selectedControl = null!;
        public void SelectWorkerProductSold(WorkerListComponent workerComponent, WorkerDto? worker)
        {
            if (selectedControl != null)
            {
                selectedControl.brWorker.Background = Brushes.White;
            }

            workerComponent.brWorker.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));

            ShowWorkerSoldProducts(worker);

            selectedControl = workerComponent;
        }

        public void ShowWorkerSoldProducts(WorkerDto? worker)
        {
            var productSales = worker?.ProductSales.Where(p => p.Count != 0)
                .OrderByDescending(p => p.CreatedDate).ToList();

            St_WorkerSoldProducts.Visibility = Visibility.Visible;
            St_WorkerSoldProducts.Children.Clear();

            int rowNumber = 1;
            foreach (var workerSoldProduct in productSales)
            {
                WorkerSoldProductComponent workerSoldProductComponent = new WorkerSoldProductComponent();
                workerSoldProductComponent.Tag = workerSoldProduct;
                //workerSoldProductComponent.SetValues(
                //    rowNumber,
                //    workerSoldProduct.TransactionNumber,
                //    workerSoldProduct.Product.Name,
                //    workerSoldProduct.Product.SellPrice,
                //    workerSoldProduct.Count,
                //    workerSoldProduct.TotalCost);

                workerSoldProductComponent.BorderThickness = new Thickness(3, 2, 3, 2);
                St_WorkerSoldProducts.Children.Add(workerSoldProductComponent);
                rowNumber++;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

    }
}
