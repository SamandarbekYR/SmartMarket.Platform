using SmartMarket.Desktop.Tablet.Windows;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Tablet.Components;

/// <summary>
/// Interaction logic for SearchProductComponent.xaml
/// </summary>
public partial class SearchProductComponent : UserControl
{
    public SearchProductComponent()
    {
        InitializeComponent();
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        QuantityWindow quantityWindow = new QuantityWindow();
        quantityWindow.ShowDialog();
    }
}
