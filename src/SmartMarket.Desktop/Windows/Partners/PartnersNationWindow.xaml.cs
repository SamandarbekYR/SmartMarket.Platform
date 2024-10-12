using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Components.PartnersForComponent;
using SmartMarketDeskop.Integrated.Services.Partners;

namespace SmartMarket.Desktop.Windows.Partners;

/// <summary>
/// Interaction logic for PartnersNationWindow.xaml
/// </summary>
public partial class PartnersNationWindow : Window
{
    private readonly IPartnerService _partnerService;

    public PartnersNationWindow()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
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

        GetPartners();
    }

    private async void GetPartners()
    {
        var partners = await _partnerService.GetAll();

        St_Nationer.Children.Clear();
        int count = 1;

        if(partners.Count > 0)
        {
            foreach (var partner in partners)
            {
                PartnerNationComponent partnerNationComponent = new PartnerNationComponent();
                partnerNationComponent.SetData(partner, count);
                St_Nationer.Children.Add(partnerNationComponent);
                count++;
            }
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
