using System.Windows.Controls;

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent;

/// <summary>
/// Interaction logic for ShopWorkerAccountComponent.xaml
/// </summary>
public partial class ShopWorkerAccountComponent : UserControl
{
    public ShopWorkerAccountComponent()
    {
        InitializeComponent();
    }

    public void SetValues(string firstname,string lastName,string salary,string phonenumber, string RecivedSum, string RemainingSum)
    {
        tbFirstName.Text = firstname;
        tbLastName.Text = lastName;
        tbSalary.Text = salary;
        tbPhoneNumber.Text = phonenumber;
        tbReceivedSum.Text = RecivedSum;
        tbRemainingSum.Text = RemainingSum;
    }

}
