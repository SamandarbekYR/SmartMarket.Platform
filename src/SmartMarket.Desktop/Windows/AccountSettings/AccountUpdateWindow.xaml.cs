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

namespace SmartMarket.Desktop.Windows.AccountSettings;

/// <summary>
/// Interaction logic for AccountUpdateWindow.xaml
/// </summary>
public partial class AccountUpdateWindow : Window
{
    private IWorkerService _workerService;
    private IPositionService _positionService;

    private WorkerDto _worker;
    public AccountUpdateWindow(WorkerDto worker)
    {
        InitializeComponent();
        _worker = worker;
        _workerService = new WorkerService();
        _positionService = new PositionService();
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

    private async void btnUpdateAccount_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var positionId = Guid.Parse(cbPosition.SelectedValue.ToString());
        var firstName = txtFirstName.Text.Trim();
        var lastName = txtLastName.Text.Trim();
        var password = txtPassword.Text.Trim(); 
        var phone = txtPhoneNumber.Text.Trim();

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
            }
            else
            {
                MessageBox.Show("Yangilashda xatolik yuz berdi.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private async void btnDeleteAccount_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var result = MessageBox.Show("Ishchi haqida ma'lumotlar o'chiriladi. Davom etasizmi?", "O'chirish", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            var resultDelete = await _workerService.DeleteAsync(_worker.Id);

            if (resultDelete)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("O'chirishda xatolik yuz berdi.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private async void SetValues()
    {
        txtFirstName.Text = _worker.FirstName;
        txtLastName.Text = _worker.LastName;
        txtPhoneNumber.Text = _worker.PhoneNumber;

        var salaryWorker = _worker.SalaryWorkers.FirstOrDefault(x => x.WorkerId == _worker.Id);
        if (salaryWorker?.Salary != null)
        {
            txtSalary.Text = salaryWorker.Salary.Amount.ToString();
        }

        var positions = await _positionService.GetAllAsync();
        cbPosition.ItemsSource = positions.DistinctBy(p => p.Name).ToList(); 
        cbPosition.Tag = positions; 
        cbPosition.DisplayMemberPath = "Name"; 
        cbPosition.SelectedValuePath = "Id"; 
        cbPosition.SelectedValue = _worker.Position.Id;
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
