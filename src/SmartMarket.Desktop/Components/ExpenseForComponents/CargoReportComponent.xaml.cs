using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Services.Expenses;
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
        public void SetData(LoadReportDto dto)
        {
            tbProductName.Text = dto.ProductName;
            tbTotalPrice.Text = (dto.Product.Price * dto.Count).ToString();
            tbPrice.Text = dto.Product.Price.ToString();
            tbCount.Text = dto.Count.ToString();

            this.DataContext = dto;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is Border border && border.Tag is LoadReportDto dto)
            {
                CargoReportDetailWindow cargoReportDetailWindow = new CargoReportDetailWindow();
                cargoReportDetailWindow.SetData(dto);
                cargoReportDetailWindow.ShowDialog();
            }
        }
    }
}
