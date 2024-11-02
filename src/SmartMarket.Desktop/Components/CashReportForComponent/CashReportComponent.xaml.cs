using SmartMarket.Desktop.Pages.CashReportForPage;
using SmartMarket.Service.DTOs.PayDesks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.CashReportForComponent;

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
        btnCashReport.Background = new SolidColorBrush(Colors.Gray);
        CheckOutFirstPage checkOutFirstPage = new CheckOutFirstPage();
        checkOutFirstPage.SetPayDesk(payDesk!);
    }
}
