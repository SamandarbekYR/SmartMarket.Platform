using SmartMarket.Domain.Entities.Partners;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.PartnersForComponent;

/// <summary>
/// Interaction logic for PartnerNationComponent.xaml
/// </summary>
public partial class PartnerNationComponent : UserControl
{
    public PartnerNationComponent()
    {
        InitializeComponent();
    }

    public void SetData(Partner partner, int count)
    {
        lb_Count.Content = count.ToString();
        lb_Firstname.Content = partner.FirstName;
        lb_Lastname.Content = partner.LastName;
        lb_Phonenumber.Content = partner.PhoneNumber;
        lb_Nation.Content = partner.TotalDebt;
    }
}
