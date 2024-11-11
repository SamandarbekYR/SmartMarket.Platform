using SmartMarket.Desktop.Pages.CashReportForPage;
using SmartMarket.Service.DTOs.PayDesks;
using System.Windows;
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

        var page = FindParentPage(this);
        if (page is CashReportPage reportPage)
        {
            reportPage.SelectCashReport(this, payDesk!);
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
}
