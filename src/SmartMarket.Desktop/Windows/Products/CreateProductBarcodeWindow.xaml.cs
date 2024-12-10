using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Integrated.Services.Products.Print;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace SmartMarket.Desktop.Windows.Products;

/// <summary>
/// Interaction logic for CreateProductBarcodeWindow.xaml
/// </summary>
public partial class CreateProductBarcodeWindow : Window
{
    public FullProductDto Product { get; set; } = new FullProductDto();
    public CreateProductBarcodeWindow()
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

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        if (tb_quantity.Text.Length > 0) 
        {
            BarcodePrintService printService = new BarcodePrintService();
            printService.PrintLabel(Product, int.Parse(tb_quantity.Text));
        }
        else
        {
            tb_quantity.Foreground = Brushes.Red;

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 5,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2)
            };

            var transform = new TranslateTransform();
            tb_quantity.RenderTransform = transform;

            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        tb_quantity.Focus();
    }
}
