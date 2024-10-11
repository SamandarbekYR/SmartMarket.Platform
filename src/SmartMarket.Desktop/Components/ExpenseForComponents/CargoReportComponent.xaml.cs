using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
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

namespace SmartMarket.Desktop.Components.ExpenseForComponents
{
    /// <summary>
    /// Interaction logic for CargoReportComponent.xaml
    /// </summary>
    public partial class CargoReportComponent : UserControl
    {
        private readonly ILoadReportServer loadReportServer;
        public CargoReportComponent()
        {
            InitializeComponent();
            this.loadReportServer = new LoadReportServer();
        }

        public Guid LoadReportId { get; set; }
        public void SetData(LoadReportViewModel loadReport)
        {
            //tbPCode.Text = loadReport.PCode;
            //tbBarcode.Text = loadReport.Barcode;
            //tbCategory.Text = loadReport.CategoryName;
            //tbCount.Text = loadReport.Count.ToString();
            //tbDate.Text = loadReport.Date.ToString();
            //tbManuFacturer.Text = loadReport.ManuFacture;
            //tbPrice.Text = loadReport.Price.ToString(); //SellPrice yoki Price?
            //tbWorker.Text = loadReport.WorkerName;
            //tbTotalPrice.Text = loadReport.TotalPrice.ToString();
        }
    }
}
