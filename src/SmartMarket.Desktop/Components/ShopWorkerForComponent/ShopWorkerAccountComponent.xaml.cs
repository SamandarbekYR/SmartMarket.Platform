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
        //tbFullName.Text = firstname+" "+lastName;
        //tbSalary.Text = salary;
        //tbReceievedSum.Text = RecivedSum;
        //tbRemainingSum.Text = RemainingSum;
        //tbPhoneNumber.Text=phonenumber;
        //tbPosition.Text = "Sotuvchi";
    }

}
