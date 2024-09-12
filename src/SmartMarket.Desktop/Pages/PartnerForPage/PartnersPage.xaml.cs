using SmartMarket.Desktop.Components.PartnersForComponent;
using System.Windows;
using System.Windows.Controls;

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
        }


        public class Debttor()
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string DebtSum { get; set; }
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
                partnersComponent.SetData(item.Id,item.FirstName,item.LastName,item.PhoneNumber);
                St_partners.Children.Add(partnersComponent);    
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllDebtor();
        }
    }
}
