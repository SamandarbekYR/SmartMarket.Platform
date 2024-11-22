using SmartMarket.Desktop.Windows.Sales;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for SaleProductForComponent.xaml
/// </summary>
public partial class ShipmentSaleComponent : UserControl
{
    public Guid Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public int AvailableCount { get; set; }

    public ShipmentSaleComponent()
    {
        InitializeComponent();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        ShipmentsSaleWindow saleWindow = GetShipmentSaleWindow();
        saleWindow.SelectProduct(this);
    }

    public static ShipmentsSaleWindow GetShipmentSaleWindow()
    {
        ShipmentsSaleWindow shipmentWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(ShipmentsSaleWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                shipmentWindow = (ShipmentsSaleWindow)window;
                if (shipmentWindow != null)
                {
                    break;
                }
            }
        }
        return shipmentWindow!;
    }

    public void SetData(FullProductDto product)
    {
        Id = product.Id;
        Barcode = product.Barcode;
        AvailableCount = product.Count;
    }
}
