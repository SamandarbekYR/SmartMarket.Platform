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

namespace SmartMarket.Desktop.Components.PaymentForComponent
{
    /// <summary>
    /// Interaction logic for PaymentContrAgentComponent.xaml
    /// </summary>
    public partial class PaymentContrAgentComponent : UserControl
    {
        public PaymentContrAgentComponent()
        {
            InitializeComponent();
        }

        public void SetValues(int number, string companyName, string firstName, string lastName, string phoneNumber, double paymentAmount, string paymentType, DateTime? date, string fromWhere)
        {
            lb_Number.Content = number.ToString();
            lb_CompanyName.Content = companyName;
            lb_FirstName.Content = firstName;
            lb_LastName.Content = lastName;
            lb_PhoneNumber.Content = phoneNumber;
            lb_PaymentAmount.Content = paymentAmount.ToString();
            lb_PaymentType.Content = paymentType;
            lb_Date.Content = date.HasValue ? date.Value.ToString("dd.MM.yyyy") : "00.00.00";
            lb_FromWhere.Content = fromWhere;
        }
    }
}
