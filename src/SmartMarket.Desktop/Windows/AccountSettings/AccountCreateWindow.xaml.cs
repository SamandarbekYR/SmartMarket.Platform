using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Services.Workers.Position;
using SmartMarketDeskop.Integrated.Services.Workers.Worker;
using SmartMarketDeskop.Integrated.Services.Workers.WorkerRoles;

using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Windows.AccountSettings
{
    /// <summary>
    /// Interaction logic for AccountCreateWindow.xaml
    /// </summary>
    public partial class AccountCreateWindow : Window
    {
        private IWorkerService _workerService;
        private IPositionService _positionService;
        private IWorkerRoleService _workerRoleService;
        public AccountCreateWindow()
        {
            InitializeComponent();
            _workerService = new WorkerService();
            _positionService = new PositionService();
            _workerRoleService = new WorkerRoleService();
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private async void btnCreateAccount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtFirstName.Text) &&
                    !string.IsNullOrEmpty(txtLastName.Text) &&
                    !string.IsNullOrEmpty(txtPhoneNumber.Text) &&
                    !string.IsNullOrEmpty(txtPassword.Text) &&
                    !string.IsNullOrEmpty(txtSalary.Text) &&
                    !string.IsNullOrEmpty(txtAdvance.Text))
                {
                    var positionId = Guid.Parse(cbPosition.SelectedValue?.ToString() ?? Guid.Empty.ToString());
                    var roleId = Guid.Parse(cbRole.SelectedValue?.ToString() ?? Guid.Empty.ToString());

                    var worker = new AddWorkerDto
                    {
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = txtPhoneNumber.Text,
                        PositionId = positionId,
                        WorkerRoleId = roleId,
                        Password = txtPassword.Text,
                        Salary = double.Parse(txtSalary.Text),
                        Advance = double.Parse(txtAdvance.Text)
                    };

                    var result = await _workerService.CreateAsync(worker);

                    if (result)
                    {
                        this.Close();
                        notifier.ShowInformation("Account muvaffaqiyatli yaratildi.");
                    }
                    else
                    {
                        notifier.ShowError("Account yaratishda xatolik yuz berdi.");
                    }
                }
                else
                {
                    notifier.ShowWarning("Account malumotlari to'liq emas!");
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError("Account yaratishda xatolik");
            }
        }

        private async void LoadRolesAndPositionsAsync()
        {
            try
            {
                var roles = await _workerRoleService.GetAllAsync();
                cbRole.ItemsSource = roles.DistinctBy(r => r.RoleName).ToList();
                cbRole.DisplayMemberPath = "RoleName";
                cbRole.SelectedValuePath = "Id";

                var positions = await _positionService.GetAllAsync();
                cbPosition.ItemsSource = positions.DistinctBy(p => p.Name).ToList();
                cbPosition.DisplayMemberPath = "Name";
                cbPosition.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                notifier.ShowError("Xato yuz berdi.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            LoadRolesAndPositionsAsync();
        }
    }
}
