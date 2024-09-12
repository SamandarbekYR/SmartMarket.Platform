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


    public void SetData(int id, string firstname, string lastname,string phone)
    {
        lb_Count.Content = id.ToString();
        lb_Firstname.Content = firstname;
        lb_Lastname.Content = lastname;
        lb_Phone_Number.Content = phone;
    }
}
