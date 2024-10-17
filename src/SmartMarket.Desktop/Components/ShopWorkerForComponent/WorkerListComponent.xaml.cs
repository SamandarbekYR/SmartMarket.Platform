using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Service.DTOs.Workers.Worker;

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

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent
{
    /// <summary>
    /// Interaction logic for WorkerListComponent.xaml
    /// </summary>
    public partial class WorkerListComponent : UserControl
    {
        public WorkerListComponent()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var worker = this.Tag as WorkerDto;

            var page = FindParentPage(this);
            if (page is ShopWorkersPage shopWorkerPage)
            {
                shopWorkerPage.SelectWorker(this, worker);
            }

            if (page is WorkerSoldProductPage workerSoldProductPage)
            { 
                workerSoldProductPage.SelectWorkerProductSold(this, worker);
            }
        }

        private Page FindParentPage(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject is Page page)
            {
                return page;
            }
            else if (parentObject != null)
            {
                return FindParentPage(parentObject);
            }
            return null!;
        }

        public void SetValues(int id, string name, string lastName)
        {
            tbNumber.Text = id.ToString();
            tbName.Text = name + " " + lastName;
        }

        private void UpdateWorker_Button_Click(object sender, RoutedEventArgs e)
        {
            var worker = this.Tag as WorkerDto;

            AccountUpdateWindow updateWorkerWindow = new AccountUpdateWindow(worker);
            updateWorkerWindow.ShowDialog();
        }
    }
}
