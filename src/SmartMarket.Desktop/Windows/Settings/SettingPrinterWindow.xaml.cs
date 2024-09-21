using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Pages.SettingsForPage;

namespace SmartMarket.Desktop.Windows.Settings;

/// <summary>
/// Interaction logic for SettingPrinterWindow.xaml
/// </summary>
public partial class SettingPrinterWindow : Window
{
    public SettingPrinterWindow()
    {
        InitializeComponent();
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        SettingsPrinterPage settingsPrinterPage = new SettingsPrinterPage();
        PageNavigator.Content = settingsPrinterPage;
    }

    private void btnclose_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.Close();
    }
}
