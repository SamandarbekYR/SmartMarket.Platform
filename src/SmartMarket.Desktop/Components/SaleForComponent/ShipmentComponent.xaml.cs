using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for ShipmentComponent.xaml
/// </summary>
public partial class ShipmentComponent : UserControl
{
    public ShipmentComponent()
    {
        InitializeComponent();
    }

    public void SetData(string firstName, string lastName, double totalSum)
    {
        lbFullName.Content = firstName + " " + lastName;
        lbTotalSum.Content = totalSum.ToString();
    }
}
