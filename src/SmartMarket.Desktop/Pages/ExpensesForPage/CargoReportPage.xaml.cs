using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Domain.Entities.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
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

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for CargoReportPage.xaml
    /// </summary>
    public partial class CargoReportPage : Page
    {
        private readonly ILoadReportServer loadReportServer;
        public CargoReportPage()
        {
            InitializeComponent();
            this.loadReportServer = new LoadReportServer();
        }
        private readonly List<LoadReportViewModel> loadReports;

        public async void GetAllCargoReport()
        {
            St_CargoReports.Children.Clear();

            //var loadReports = await loadReportServer.GetAllAsync();
            int count = 1;

            if(loadReports != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    loadReports.Add(new LoadReportViewModel
                    {
                        PCode = "1001",
                        Barcode = "10101101101",
                        CategoryName = "Daftar",
                        WorkerName = "Abdulvosid",
                        Price = 4000,
                        SellPrice = 5000,
                        Count = 200,
                        TotalPrice = 1000000,
                        Date = new DateTime(2024, 10, 11),
                        ManuFacture = "..."
                    });
                }

                foreach (var loadReport in loadReports)
                {
                    CargoReportComponent cargoReportComponent = new CargoReportComponent();
                    cargoReportComponent.tbNumber.Text = count.ToString();
                    cargoReportComponent.SetData(loadReport);
                    St_CargoReports.Children.Add(cargoReportComponent);
                    count++;
                }
            }
            else { }
        }
    }
}
