using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Service.DTOs.Workers.Worker;
using System.Windows.Controls;
using System.Windows.Input;

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


        public void SetData(WorkerDto dto)
        {
            lbName.Text = dto.FirstName + "  " + dto.LastName;
            lbPositon.Text = dto.Position.Name;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var worker = this.Tag as WorkerDto;

            AccountUpdateWindow accountUpdateWindow = new AccountUpdateWindow(worker!);
            accountUpdateWindow.ShowDialog();   
        }
    }
}
