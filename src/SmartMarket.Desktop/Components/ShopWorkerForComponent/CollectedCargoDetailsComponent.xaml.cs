using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using SmartMarket.Desktop.Windows;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.DTOs.Products.SalesRequest;
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
    /// Interaction logic for CollectedCargoDetailsComponent.xaml
    /// </summary>
    public partial class CollectedCargoDetailsComponent : UserControl
    {
        public CollectedCargoDetailsComponent()
        {
            InitializeComponent();
        }

        public void SetData(SalesRequestDto loadReport, int count)
        {
            tbNumber.Text = count.ToString();
            tbTransaction.Text = loadReport.TransactionId.ToString(); 
            tbClientName.Text = "Sobir aka";
            tbCargoSum.Text = loadReport.TotalCost.ToString();
            tbDate.Text = loadReport.CreatedDate.HasValue
                  ? loadReport.CreatedDate.Value.ToString("yyyy-MM-dd")
                  : "N/A";
            tbSellerName.Text = loadReport.Worker.FirstName.ToString();

            this.Tag = loadReport;
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is Border border && this.Tag is SalesRequestDto loadReport)
            {
                WorkerSoldProductPage workerSoldProductPage = new WorkerSoldProductPage();
                workerSoldProductPage.SelectWorkerSaleProduct(loadReport);
                workerSoldProductPage.Page_Navigator.Navigate(loadReport);
            }
        }
    }
}
