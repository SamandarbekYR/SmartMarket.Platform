using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace SmartMarket.Desktop.Windows.PaymentWindow;

/// <summary>
/// Interaction logic for PaymentKontrAgentWindow.xaml
/// </summary>
public partial class PaymentKontrAgentWindow : Window
{
    public PaymentKontrAgentWindow()
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

    private void btnclose_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }

    private void phone_number_TextChanged(object sender, TextChangedEventArgs e)
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

    private void BtnPay_Click(object sender, RoutedEventArgs e)
    {

    }

    private void BtnPayHistory_Click(object sender, RoutedEventArgs e)
    {

    }
}
