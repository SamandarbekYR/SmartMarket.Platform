using SmartMarket.Desktop.Pages.ShopWorkersForPage;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent;

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
    }

    private void border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var dto = this.Tag as SalesRequestDto;
        var page = FindParentPage(this);
        if (page is CollectedCargoDetailsPage reportPage)
        {
            reportPage.SelectCargo(this);
            WorkerSoldProductPage worker = new WorkerSoldProductPage();
            worker.SelectWorkerSaleProduct(dto!);

            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is ShopWorkersPage workerPage)
                {
                    workerPage.WorkerSoldProductPageNavigator.Content = worker;
                    break;
                }
            }
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
