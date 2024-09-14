using SmartMarket.Domain.Entities.Partners;
using System.Windows.Controls;

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
}
