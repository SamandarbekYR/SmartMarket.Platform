using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDeskop.Integrated.Services.Expenses;
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
        private readonly ILoadReportService loadReportService;
        public CargoReportComponent()
        {
            InitializeComponent();
            this.loadReportService = new LoadReportService();
        }

        public Guid LoadReportId { get; set; }
        public void SetData(LoadReportDto loadReport)
        {
            tbProductName.Text = "Daftar";
            tbPrice.Text = "25000";
            tbTotalPrice.Text = loadReport.TotalPrice.ToString();
            tbCount.Text = "100";
        }
    }
}
