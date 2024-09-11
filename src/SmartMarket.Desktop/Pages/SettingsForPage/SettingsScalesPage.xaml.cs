using SmartMarket.Desktop.Components.SettingsForComponent;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.SettingsForPage;

/// <summary>
/// Interaction logic for SettingsScalesPage.xaml
/// </summary>
public partial class SettingsScalesPage : Page
{
    public SettingsScalesPage()
    {
        InitializeComponent();
        GetScales();
    }

    private void bntAddScales_Click(object sender, RoutedEventArgs e)
    {

    }


    public void GetScales()
    {
        St_Scales.Visibility = Visibility.Visible;
        St_Scales.Children.Clear();

        SettingsScalesComponent settingsScalesComponent = new SettingsScalesComponent();
        settingsScalesComponent.Tag = 1;
        settingsScalesComponent.SetData("Tarozi 1:");
        settingsScalesComponent.BorderThickness = new Thickness(0, 0, 0, 5);
        St_Scales.Children.Add(settingsScalesComponent);

    }
}
