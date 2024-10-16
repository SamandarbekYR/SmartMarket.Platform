using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Service.DTOs.Workers.Worker;

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


        public void SetData(string firstName,string lastName, string position)
        {
            lbName.Text = firstName + "  " + lastName;
            lbPositon.Text = position;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var worker = this.Tag as WorkerDto;

            AccountUpdateWindow accountUpdateWindow = new AccountUpdateWindow(worker);
            accountUpdateWindow.ShowDialog();   
        }
    }
}
