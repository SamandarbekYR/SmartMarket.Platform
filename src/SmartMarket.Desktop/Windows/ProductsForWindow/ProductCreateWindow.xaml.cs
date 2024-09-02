using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for ProductCreateWindow.xaml
/// </summary>
public partial class ProductCreateWindow : Window
{
    public ProductCreateWindow()
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

    private void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void btnCreateCategory_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }

    private void Border_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.Close();   
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }
}
