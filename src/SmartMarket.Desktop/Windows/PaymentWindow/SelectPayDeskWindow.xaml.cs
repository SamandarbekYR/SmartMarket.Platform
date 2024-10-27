using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using System.Windows.Controls;
using SmartMarketDeskop.Integrated.Security;

namespace SmartMarket.Desktop.Windows.PaymentWindow;

/// <summary>
/// Interaction logic for SelectPayDeskWindow.xaml
/// </summary>
public partial class SelectPayDeskWindow : Window
{

    private readonly IPayDeskService _payDeskService;
    private bool isComboBoxLoaded = false;

    public SelectPayDeskWindow()
    {
        InitializeComponent();
        this._payDeskService = new PayDeskService();
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

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        await GetPayDesk();
        isComboBoxLoaded = true;
    }

    private async Task GetPayDesk()
    {
        var payDesks = await _payDeskService.GetAll();

        foreach (var item in payDesks)
        {
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Content = item.Name;
            comboBoxItem.Tag = item.Id;
            cb_payDesk.Items.Add(comboBoxItem);
        }
    }

    private void cb_payDesk_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isComboBoxLoaded) return;

        if (cb_payDesk.SelectedItem is ComboBoxItem selectedItem)
        {
            var id = selectedItem.Tag;
            if (chbRememberMe.IsChecked == true)
            {
                Properties.Settings.Default.PayDesk = id.ToString();
                Properties.Settings.Default.PayDeskName = selectedItem.Content.ToString();
                Properties.Settings.Default.Save();
                IdentitySingelton.GetInstance().PayDeskId = Guid.Parse(id.ToString()!);
            }
            else
            {
                IdentitySingelton.GetInstance().PayDeskId = Guid.Parse(id.ToString()!);
                IdentitySingelton.GetInstance().PayDeskName = selectedItem.Content.ToString()!;
            }
        }
        this.Close();
    }

}
