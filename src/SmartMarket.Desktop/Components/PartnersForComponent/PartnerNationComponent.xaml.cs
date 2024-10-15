using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Domain.Entities.Partners;
using System.Windows;
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

    public static PartnersNationWindow GetPartnersNationWindow()
    {
        PartnersNationWindow partnerWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(PartnersNationWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                partnerWindow = (PartnersNationWindow)window;
                if (partnerWindow != null)
                {
                    break;
                }
            }
        }
        return partnerWindow!;
    }

    public void SetData(Partner partner, int count)
    {
        lb_Count.Content = count.ToString();
        lb_Firstname.Content = partner.FirstName;
        lb_Lastname.Content = partner.LastName;
        lb_Phonenumber.Content = partner.PhoneNumber;
        lb_Nation.Content = partner.TotalDebt;
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var partner = this.Tag as Partner;
        PartnersNationWindow nationWindow = GetPartnersNationWindow();
        nationWindow.SelectPartner(this, partner!.Id);
    }
}
