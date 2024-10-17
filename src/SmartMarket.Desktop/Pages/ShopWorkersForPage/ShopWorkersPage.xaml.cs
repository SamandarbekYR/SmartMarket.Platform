using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Services.Workers.Worker;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage
{
    /// <summary>
    /// Interaction logic for ShopWorkersPage.xaml
    /// </summary>
    public partial class ShopWorkersPage : Page
    {
        private IWorkerService _workerService;
        public ShopWorkersPage()
        {
            InitializeComponent();
            _workerService = new WorkerService();   
        }

        public async void GetWorkers()
        {
            var workers = await _workerService.GetAllAsync();

            St_Workers.Visibility = Visibility.Visible;
            St_Workers.Children.Clear();

            int rowNumber = 1;
            foreach (var worker in workers)
            {
                WorkerListComponent workerListComponent = new WorkerListComponent();
                workerListComponent.Tag = worker;
                workerListComponent.SetValues(rowNumber, worker.FirstName, worker.LastName);
                workerListComponent.BorderThickness = new Thickness(3, 2, 3, 2);
                St_Workers.Children.Add(workerListComponent);
                rowNumber++;
            }
        }

        private WorkerListComponent selectedControl = null!;
        public void SelectWorker(WorkerListComponent workerComponent, WorkerDto? worker)
        {
            if (selectedControl != null)
            {
                selectedControl.brWorker.Background = Brushes.White;
            }

            workerComponent.brWorker.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));

            if (worker != null)
            {
                lb_WorkerName.Content = worker.FirstName;
                lb_WorkerLastName.Content = worker.LastName;
                lb_Salary.Content = worker.Salary;
                lb_PhoneNumber.Content = worker.PhoneNumber;
                lb_PaidPart.Content = worker.Advance;
                lb_UnpaidPart.Content = worker.Salary - worker.Advance;
            }

            selectedControl = workerComponent;
        }

        private void UpdateWorker_Button_Click(object sender, RoutedEventArgs e)
        {
            var worker = selectedControl.Tag as WorkerDto;

            AccountUpdateWindow workerUpdateWindow = new AccountUpdateWindow(worker);
            workerUpdateWindow.ShowDialog();
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

        private void CollectedCargoPageNavigator_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WorkersoldProduct();
            GetWorkers();
            CollectedCargoPageView();
        }
    }
}