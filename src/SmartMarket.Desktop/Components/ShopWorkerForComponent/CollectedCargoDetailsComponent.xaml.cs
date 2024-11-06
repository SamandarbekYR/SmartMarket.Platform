using SmartMarket.Service.DTOs.Products.LoadReport;
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
        public Guid LoadReportId { get; set; }
        public void SetData(CollectedLoadReportDto loadReport, int count)
        {
            LoadReportId = loadReport.Id;
            tbNumber.Text = count.ToString();
            tbTransaction.Text = "123654478553"; //apidan chiqarish kerak
            tbClientName.Text = "Sobir aka"; //apidan chiqarish kerak
            tbCargoSum.Text = loadReport.TotalPrice.ToString();
            tbDate.Text = loadReport.CreatedDate.ToString();
            tbSellerName.Text = loadReport.Worker.FirstName.ToString();
        }
    }
}
