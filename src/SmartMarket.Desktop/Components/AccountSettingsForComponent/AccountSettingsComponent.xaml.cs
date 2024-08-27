using SmartMarket.Desktop.Windows.AccountSettings;
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

namespace SmartMarket.Desktop.Components.AccountSettingsForComponent
{
    /// <summary>
    /// Interaction logic for AccountSettingsComponent.xaml
    /// </summary>
    public partial class AccountSettingsComponent : UserControl
    {
        public AccountSettingsComponent()
        {
            InitializeComponent();
        }


        public void SetData(string FirstName,string LastName)
        {
            lbName.Text=FirstName+"  "+LastName;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AccountUpdateWindow accountUpdateWindow = new AccountUpdateWindow();
            string[] res = lbName.Text.Split(" ");
            accountUpdateWindow.txtFirstName.Text = res[0];
            accountUpdateWindow.txtLastName.Text = res[2];
            accountUpdateWindow.txtRole.Text=lbRole.Text;
            accountUpdateWindow.ShowDialog();   
        }
    }
}
