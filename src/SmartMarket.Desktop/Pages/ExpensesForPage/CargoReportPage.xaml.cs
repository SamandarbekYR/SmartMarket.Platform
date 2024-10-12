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
using ToastNotifications.Utilities;

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

        public async void GetAllCargoReport()
        {
            St_CargoReports.Children.Clear();

            var loadReports = await loadReportServer.GetAllAsync();

            int count = 1;

            if(loadReports != null)
            {
                foreach(var report in loadReports)
                {
                    CargoReportComponent cargoReportComponent = new CargoReportComponent();
                    cargoReportComponent.tbNumber.Text = count.ToString();
                    cargoReportComponent.SetData(report);
                    St_CargoReports.Children.Add(cargoReportComponent);
                    count++;
                }
            }
            else { }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllCargoReport();
        }
    }
}
