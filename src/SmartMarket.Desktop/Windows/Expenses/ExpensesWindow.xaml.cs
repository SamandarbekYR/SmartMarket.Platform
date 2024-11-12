using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Services.Expenses;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using TextBox = System.Windows.Controls.TextBox;

namespace SmartMarket.Desktop.Windows.Expenses;

/// <summary>
/// Interaction logic for ExpensesWindow.xaml
/// </summary>
public partial class ExpensesWindow : Window
{
    private readonly IExpenseService _expenseService;
    private readonly IPayDeskService _paydeskService;

    public Guid PayDeskId { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;

    public ExpensesWindow()
    {
        InitializeComponent();
        this._expenseService = new ExpenseService();
        this._paydeskService = new PayDeskService();
    }

    Notifier notifierthis = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

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

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        await GetPayDesk();
    }

    private async Task GetPayDesk()
    {
        var paydesks = await Task.Run(async () => await _paydeskService.GetAll());
        foreach (var paydesk in paydesks)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = paydesk.Name;
            item.Tag = paydesk.Id;
            where_txt.Items.Add(item);
        }
    }

    private void close_button_Click(object sender, RoutedEventArgs e)
    {
        this.Close(); 
    }

    private void expenceSumma_txt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        string text = textBox.Text;
        string filteredText = Regex.Replace(text, "[^0-9]+", "");

        if (text != filteredText)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = filteredText;
            textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        AddExpenceDto dto = new AddExpenceDto();
        dto.Reason = Reason;
        dto.PayDeskId = PayDeskId;
        dto.Amount = double.Parse(expenceSumma_txt.Text);
        dto.TypeOfPayment = PaymentType;

        bool result = await _expenseService.CreateExpense(dto);
        if(result)
        {
            notifierthis.ShowSuccess("Harajat qabul qilindi.");
            this.Close();
        }
        else
            notifierthis.ShowError("Qandaydir xatolik mavjud.");

    }

    private void where_txt_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (where_txt.SelectedItem as ComboBoxItem)!;

        if (selectedItem != null)
        {
            PayDeskId = Guid.Parse(selectedItem.Tag.ToString()!);
        }
    }

    private void PaymentType_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (PaymentType_combo.SelectedItem as ComboBoxItem)!;

        if (selectedItem != null)
        {
            PaymentType = selectedItem.Content.ToString()!;
        }
    }

    private void ExpenseReason_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (ExpenseReason_combo.SelectedItem as ComboBoxItem)!;

        if (selectedItem != null)
        {
            Reason = selectedItem.Content.ToString()!;
        }
    }
}
