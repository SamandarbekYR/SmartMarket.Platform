using SmartMarket.Desktop.Components.AccountSettingsForComponent;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Desktop.Windows.Position;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.AccountSettingsForPage
{
    /// <summary>
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {

        List<Worker>    workers = new List<Worker>();
        public AccountSettingsPage()
        {
            InitializeComponent();
            GetAllworkers();
        }





        public class Worker()
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }




        public void GetAllworkers()
        {
            var newlist = WorkerList();
            Wr_Account.Visibility = Visibility.Visible; 
            Wr_Account.Children.Clear();
            foreach (var item in newlist)
            {
                AccountSettingsComponent accountSettingsComponent = new AccountSettingsComponent(); 
                accountSettingsComponent.Tag = item;
                accountSettingsComponent.SetData(item.FirstName,item.LastName);
                accountSettingsComponent.BorderThickness=new Thickness(15,5,15,5);
                Wr_Account.Children.Add(accountSettingsComponent);
            }
        }


        public List<Worker> WorkerList()
        {
            workers.Add(new Worker() { FirstName="Alisher",LastName="Ergashev"});
            workers.Add(new Worker() { FirstName = "Ramziddin", LastName = "Aliyev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Xumoyun", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Ulug;bek", LastName = "Abdullayev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            workers.Add(new Worker() { FirstName = "Alisher", LastName = "Ergashev" });
            return workers; 
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountCreateWindow accountCreateWindow = new AccountCreateWindow();
            accountCreateWindow.ShowDialog();
        }

        private void btnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionCreateWindow positionCreateWindow = new PositionCreateWindow(); 
            positionCreateWindow.ShowDialog();
        }
    }
}
