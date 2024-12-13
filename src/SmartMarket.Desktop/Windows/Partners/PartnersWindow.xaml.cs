﻿using SmartMarket.Desktop.Components.PartnersForComponent;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Runtime.InteropServices;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Components.Loader;
using SmartMarket.Domain.Entities.Partners;

namespace SmartMarket.Desktop.Windows.Partners;

/// <summary>
/// Interaction logic for PartnersWindow.xaml
/// </summary>
public partial class PartnersWindow : Window
{

    private readonly IPartnerService _partnerService;

    public PartnersWindow()
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllPartners();
        EnableBlur();
    }

    public async void GetAllPartners()
    {
        St_Partners.Children.Clear();
        var partners = await Task.Run(async () => await _partnerService.GetAll());

        ShowPartners(partners);
    }

    public async void ShowPartners(List<Partner> partners)
    {
        Loader.Visibility = Visibility.Collapsed;
        int count = 1;

        if (partners.Count > 0)
        {
            foreach (var partner in partners)
            {
                PartnersComponent partnersComponent = new PartnersComponent();
                partnersComponent.lb_Count.Content = count;
                partnersComponent.SetData(partner);
                St_Partners.Children.Add(partnersComponent);
                count++;
            }
        }
        else
        {
            EmptyDataPartners.Visibility = Visibility.Visible;
        }
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        EnableBlur();
    }
}
