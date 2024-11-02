using MaterialDesignThemes.Wpf;

using SmartMarket.Desktop.Pages.CashReportForPage;
using SmartMarket.Service.DTOs.PayDesks;

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

namespace SmartMarket.Desktop.Components.CashReportForComponent
{
    /// <summary>
    /// Interaction logic for CashReportComponent.xaml
    /// </summary>
    public partial class CashReportComponent : UserControl
    {
        public CashReportComponent()
        {
            InitializeComponent();
        }

        public void SetValues(string name)
        {
            lbName.Content = name;
        }

        private void btnCashReport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var payDesk = this.Tag as PayDesksDto;
            CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
            checkOutFirstPage.SetPayDesk(payDesk);
        }

        private void btnCashReport_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCashReport.Background = new SolidColorBrush(Colors.LightGray); 
        }

        private void btnCashReport_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCashReport.Background = new SolidColorBrush(Colors.White); 
        }

    }
}
