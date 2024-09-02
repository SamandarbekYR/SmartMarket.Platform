using SmartMarket.Desktop.Components.PartnersForComponent;
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

namespace SmartMarket.Desktop.Pages.PartnerForPage
{
    /// <summary>
    /// Interaction logic for PartnersPage.xaml
    /// </summary>
    public partial class PartnersPage : Page
    {
        List<Debttor> debttors = new List<Debttor>();
        public PartnersPage()
        {
            InitializeComponent();
            GetAllDebtor();

        }


        public class Debttor()
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string    DebtSum { get; set; }
            public string PayedSum { get; set; }
            public string RemainingSum { get; set; }
            public string LastPaymentDate { get; set; }

        }

        public List<Debttor> DebtList()
        {
            debttors.Add(new Debttor() { 
                Id=1,
                FirstName="Adham",
                LastName="Ergashev",
                PhoneNumber="+998906661132",
                DebtSum="7,000,000",
                PayedSum="500,000",
                RemainingSum="6,500,000",
                LastPaymentDate="8/21/2024"
            });
        


            return debttors;
        }

        public void GetAllDebtor()
        {
            var newlist = DebtList();
            St_partners.Visibility = Visibility.Visible;
            St_partners.Children.Clear();
            foreach (var item in newlist)
            {
                PartnersComponent partnersComponent = new PartnersComponent();
                partnersComponent.Tag = item.Id;
                partnersComponent.SetData(item.Id,item.FirstName,item.LastPaymentDate,item.PhoneNumber,item.DebtSum,item.PayedSum,item.RemainingSum,
                    item.LastPaymentDate);
                partnersComponent.BorderThickness=new Thickness(2);
                St_partners.Children.Add(partnersComponent);    
            }
        }
    }
}
