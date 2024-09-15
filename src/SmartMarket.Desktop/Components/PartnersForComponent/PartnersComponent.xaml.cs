using SmartMarket.Domain.Entities.Partners;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.PartnersForComponent;

/// <summary>
/// Interaction logic for PartnersComponent.xaml
/// </summary>
public partial class PartnersComponent : UserControl
{
    public PartnersComponent()
    {
        InitializeComponent();
    }

    public void SetData(Partner partner)
    {
        lb_Firstname.Content = partner.FirstName;
        lb_Lastname.Content = partner.LastName;
        lb_Phone_Number.Content = partner.PhoneNumber;
    }

    private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Partner_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E1E1E1"));
    }

    private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Partner_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F5F5"));
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }
}
