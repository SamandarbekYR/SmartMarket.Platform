using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Components.PartnersForComponent;

namespace SmartMarket.Desktop.Windows.Partners;

/// <summary>
/// Interaction logic for PartnersNationWindow.xaml
/// </summary>
public partial class PartnersNationWindow : Window
{
    public PartnersNationWindow()
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

    private void Create_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateWindow partnerCreateWindow = new PartnerCreateWindow();
        this.Hide();
        partnerCreateWindow.ShowDialog();
        this.Show();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();

        for (int i = 1; i <= 20; i++)
        {
            PartnerNationComponent partnerNationComponent = new PartnerNationComponent();
            partnerNationComponent.lb_Count.Content = i;
            St_Nationer.Children.Add(partnerNationComponent);
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
