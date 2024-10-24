using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using SmartMarket.Desktop.Pages.SaleForPage;
using System.Reflection.Emit;
using System.Windows.Media;

namespace SmartMarket.Desktop.Windows.PaymentWindow;

/// <summary>
/// Interaction logic for ClikAndCashWindow.xaml
/// </summary>
public partial class ClikAndCashWindow : Window
{
    double TotalCost = 0;

    public ClikAndCashWindow()
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
        foreach (Window window in Application.Current.Windows)
        {
            var frame = window.FindName("PageNavigator") as Frame;
            if (frame != null && frame.Content is SalePage salePage)
            {
                TotalCost = salePage.TotalPrice;
                break;
            }
        }
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

    private async void BtnPay_Click(object sender, RoutedEventArgs e)
    {
        double cashsum = double.Parse(tbCash.Text);
        double clicksum = double.Parse(tbClik.Text);

        if (ColculatePrice(cashsum, clicksum))
        {
            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is SalePage salePage)
                {
                    salePage.PaymentType = "clickandcash";
                    salePage.ClickSum = clicksum;
                    salePage.CashSum = cashsum;
                    salePage.SaleProducts(false);
                    break;
                }
            }
            this.Close();
        }
        else
        {
            notEnaughMoney.Foreground = Brushes.Red;
            await Task.Delay(3000);
            notEnaughMoney.Foreground = Brushes.White;
        }
    }

    private bool ColculatePrice(double cashsum, double clicksum)
    {
        if(TotalCost <= (cashsum + clicksum))
            return true; 
        return false;
    }
}
