using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Windows.Sales;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.PaymentWindow;

/// <summary>
/// Interaction logic for ClikAndCashWindow.xaml
/// </summary>
public partial class ClikAndCashWindow : Window
{
    public double TotalCost { get; set; }
    public int SendWhere { get; set; }

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
        if (BtnPay.IsEnabled == false) return;

        BtnPay.IsEnabled = false;
        double cashsum = double.Parse(tbCash.Text);
        double clicksum = double.Parse(tbClik.Text);

        if (ColculatePrice(cashsum, clicksum))
        {
            if (SendWhere == 1)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    var frame = window.FindName("PageNavigator") as Frame;
                    if (frame != null && frame.Content is SalePage salePage)
                    {
                        salePage.PaymentType = "clickandcash";
                        salePage.ClickSum = clicksum;
                        salePage.CashSum = cashsum;
                        salePage.ConvertTransaction(false);
                        break;
                    }
                }
            }
            else if(SendWhere == 2)
            {
                ShipmentsSaleWindow shipmentsSaleWindow = GetShipmentSaleWindow();
                shipmentsSaleWindow.PaymentType = "clickandcash";
                shipmentsSaleWindow.ClickSum = clicksum;
                shipmentsSaleWindow.CashSum = cashsum;
                shipmentsSaleWindow.ConvertTransaction(false);
            }
            this.Close();
        }
        else
        {
            BtnPay.IsEnabled = true;
            notEnaughMoney.Foreground = Brushes.Red;
            await Task.Delay(3000);
            notEnaughMoney.Foreground = Brushes.White;
        }
    }

    public static ShipmentsSaleWindow GetShipmentSaleWindow()
    {
        ShipmentsSaleWindow shipmentWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(ShipmentsSaleWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                shipmentWindow = (ShipmentsSaleWindow)window;
                if (shipmentWindow != null)
                {
                    break;
                }
            }
        }
        return shipmentWindow!;
    }

    private bool ColculatePrice(double cashsum, double clicksum)
    {
        if(TotalCost <= (cashsum + clicksum))
            return true; 
        return false;
    }
}
