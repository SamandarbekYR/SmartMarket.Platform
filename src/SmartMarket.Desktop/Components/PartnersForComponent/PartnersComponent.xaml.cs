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

namespace SmartMarket.Desktop.Components.PartnersForComponent
{
    /// <summary>
    /// Interaction logic for PartnersComponent.xaml
    /// </summary>
    public partial class PartnersComponent : UserControl
    {
        public PartnersComponent()
        {
            InitializeComponent();
        }


        public void SetData(int id, string Firstname,string lastname,string phone,string debtsum,string payedsum,string remaingsum,string date)
        {
            tbNumber.Text = id.ToString();
            tbFirstName.Text = Firstname;
            tbLastName.Text = lastname;
            tbPhoneNumber.Text = phone;
            tbDebtSum.Text = debtsum;
            tbPayedSum.Text = payedsum;
            tbRemainingSum.Text = remaingsum;
            tbLastPaymentDate.Text = date;
        }
    }
}
