using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent
{
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
            tbFullName.Text = firstname+" "+lastName;
            tbSalary.Text = salary;
            tbReceievedSum.Text = RecivedSum;
            tbRemainingSum.Text = RemainingSum;
            tbPhoneNumber.Text=phonenumber;
            tbPosition.Text = "Sotuvchi";
        }

    }
}
