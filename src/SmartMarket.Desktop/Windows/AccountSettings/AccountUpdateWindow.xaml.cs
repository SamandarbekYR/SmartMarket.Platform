using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Services.Workers.Worker;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using SmartMarketDeskop.Integrated.Services.Workers.Position;
using SmartMarketDeskop.Integrated.Services.Workers.WorkerRoles;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;
using ToastNotifications.Messages;

namespace SmartMarket.Desktop.Windows.AccountSettings;

/// <summary>
/// Interaction logic for AccountUpdateWindow.xaml
/// </summary>
public partial class AccountUpdateWindow : Window
{
    private IWorkerService _workerService;
    private IPositionService _positionService;
    private IWorkerRoleService _workerRoleService;

    private WorkerDto _worker;
    public AccountUpdateWindow(WorkerDto worker)
    {
        InitializeComponent();
        _worker = worker;
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
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });
    private async void btnUpdateAccount_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var positionId = Guid.Parse(cbPosition.SelectedValue?.ToString() ?? Guid.Empty.ToString());
        var roleId = Guid.Parse(cbRole.SelectedValue?.ToString() ?? Guid.Empty.ToString());
        var firstName = txtFirstName.Text.Trim();
        var lastName = txtLastName.Text.Trim();
        var password = txtPassword.Text.Trim(); 
        var phone = txtPhoneNumber.Text.Trim();
        var salary = txtSalary.Text.Trim();
        var advance = txtAdvance.Text.Trim();

        bool isValid = true;
        StringBuilder errorMessage = new StringBuilder();

        if (positionId == Guid.Empty)
        {
            errorMessage.AppendLine("Lovozim tanlanmagan.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(firstName) || !firstName.All(c => char.IsLetter(c) || c == '\''))
        {
            errorMessage.AppendLine("Ism faqat harflardan iborat bo'lishi va bo'sh bo'lmasligi kerak.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(lastName) || !lastName.All(c => char.IsLetter(c) || c == '\''))
        {
            errorMessage.AppendLine("Familiya faqat harflardan iborat bo'lishi va bo'sh bo'lmasligi kerak.");
            isValid = false;
        }

        var phoneRegex = @"^\+998\d{9}$";
        if (!Regex.IsMatch(phone, phoneRegex))
        {
            errorMessage.AppendLine("Telefon raqami +998 formatida 9 ta raqamdan iborat bo'lishi kerak.");
            isValid = false;
        }

        if (!isValid)
        {
            MessageBox.Show(errorMessage.ToString(), "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            var workerDto = new AddWorkerDto()
            {
                WorkerRoleId = _worker.WorkerRoleId,
                PositionId = positionId,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone,
            };

            if(!string.IsNullOrWhiteSpace(password))
            {
                workerDto.Password = password;
            }

            var result = await _workerService.UpdateAsync(workerDto, _worker.Id);

            if (result)
            {
                this.Close();
                notifier.ShowInformation("Account yangilandi.");
            }
            else
            {
                //MessageBox.Show("Yangilashda xatolik yuz berdi.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                notifier.ShowError("Yangilashda xatolik yuz berdi.");
            }
        }
    }

    private async void btnDeleteAccount_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var result = MessageBox.Show("Ishchi haqida ma'lumotlar o'chiriladi. Davom etasizmi?", "O'chirish", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //var message = "Ishchi haqida ma'lumotlar o'chiriladi. Davom etasizmi?";
        //var messageBox = new MessageBoxWindow(message, MessageType.Confirmation, MessageButtons.YesNo);
        //messageBox.ShowDialog();

        if (result == MessageBoxResult.Yes)
        {
            var resultDelete = await _workerService.DeleteAsync(_worker.Id);

            if (resultDelete)
            {
                this.Close();
            }
            else
            {
                //MessageBox.Show("O'chirishda xatolik yuz berdi.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                notifier.ShowError("O'chirishda xatolik yuz berdi.");
            }
        }
    }

    private async void SetValues()
    {
        txtFirstName.Text = _worker.FirstName;
        txtLastName.Text = _worker.LastName;
        txtPhoneNumber.Text = _worker.PhoneNumber;
        txtSalary.Text = _worker.Salary.ToString();  
        txtAdvance.Text = _worker.Advance.ToString();  

        var positions = await _positionService.GetAllAsync();
        cbPosition.ItemsSource = positions.DistinctBy(p => p.Name).ToList();
        cbPosition.Tag = positions;
        cbPosition.DisplayMemberPath = "Name";
        cbPosition.SelectedValuePath = "Id";
        cbPosition.SelectedValue = _worker.Position.Id;

        var roles = await _workerRoleService.GetAllAsync();
        cbRole.ItemsSource = roles.DistinctBy(r => r.RoleName).ToList();
        cbRole.Tag = roles;
        cbRole.DisplayMemberPath = "RoleName";
        cbRole.SelectedValuePath = "Id";
        cbRole.SelectedValue = _worker.WorkerRole.Id;
    }

    private void Border_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        SetValues();
    }
}
