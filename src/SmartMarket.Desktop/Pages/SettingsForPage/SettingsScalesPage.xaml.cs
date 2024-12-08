using SmartMarket.Desktop.Components.PartnersForComponent;
using SmartMarket.Desktop.Components.SettingsForComponent;
using SmartMarket.Desktop.Windows.Settings;

using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Scales;

using SmartMarketDesktop.DTOs.DTOs.Scales;

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.SettingsForPage;

/// <summary>
/// Interaction logic for SettingsScalesPage.xaml
/// </summary>
public partial class SettingsScalesPage : Page
{
    private readonly IScaleService _scaleService;
    private SettingsScalesComponent _settingsScalesComponent = new SettingsScalesComponent();
    public SettingsScalesPage()
    {
        _settingsScalesComponent.OnPageReload +=  GetAllScales;
        _scaleService = new ScaleService();
        InitializeComponent();
    }

    private void bntAddScales_Click(object sender, RoutedEventArgs e)
    {
        ScaleCreateWindow scaleCreateWindow = new ScaleCreateWindow();
        scaleCreateWindow.CreateScale = GetAllScales;
        scaleCreateWindow.ShowDialog();
    }
    // SettingsScalesPage.xaml.cs

    public async Task GetAllScales()
    {
        Dispatcher.Invoke(() =>
        {
            EmptyData.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
            St_Scales.Children.Clear();
        });

        var scales = await _scaleService.GetAllScalesAsync();

        Dispatcher.Invoke(() =>
        {
            Loader.Visibility = Visibility.Collapsed;

            int count = 1;
            if (scales.Count > 0)
            {
                foreach (var scale in scales)
                {
                    SettingsScalesComponent settingsScalesComponent = new SettingsScalesComponent();
                    settingsScalesComponent.Tag = scale;
                    settingsScalesComponent.SetData(count, scale);
                    settingsScalesComponent.GetScales = GetAllScales; 
                    settingsScalesComponent.OnPageReload += async () => await GetAllScales(); 
                    settingsScalesComponent.BorderThickness = new Thickness(0, 0, 0, 5);
                    St_Scales.Children.Add(settingsScalesComponent);
                    count++;
                }
            }
            else
            {
                EmptyData.Visibility = Visibility.Visible;
            }
        });
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllScales(); 
    }

}
