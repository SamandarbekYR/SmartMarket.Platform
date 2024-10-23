using SmartMarket.Desktop.Tablet.Components;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using static SmartMarket.Desktop.Tablet.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Tablet.Windows;

/// <summary>
/// Interaction logic for QuantityWindow.xaml
/// </summary>
public partial class QuantityWindow : Window
{
    private readonly SearchProductComponent _searchProductComponent;
    public QuantityWindow(SearchProductComponent searchProductComponent)
    {
        InitializeComponent();
        this._searchProductComponent = searchProductComponent;
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

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        tb_quantity.Focus();
        lb_AvailableCount.Content = _searchProductComponent.AvailabeCount;
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(tb_quantity.Text))
        {
            int quantity = int.Parse(tb_quantity.Text);
            if (quantity > 0)
                _searchProductComponent.Quantity = quantity;
        }
        this.Close();
    }

    private void tb_quantity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        string newText = tb_quantity.Text + e.Text;

        if (int.TryParse(newText, out int result) && result > _searchProductComponent.AvailabeCount)
        {
            e.Handled = true; 
        }
    }
}
