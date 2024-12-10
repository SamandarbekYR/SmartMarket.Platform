using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Desktop.Pages.SettingsForPage;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        SettingsScalesPage settingsPage = new SettingsScalesPage();
        await settingsPage.GetAllScales();

        EventManager.RegisterClassHandler(
            typeof(ScrollViewer),
            UIElement.ManipulationBoundaryFeedbackEvent,
            new EventHandler<ManipulationBoundaryFeedbackEventArgs>((sender, args) => args.Handled = true)
        );

        EventManager.RegisterClassHandler(
            typeof(Window),
            Window.LoadedEvent,
            new RoutedEventHandler((sender, args) =>
            {
                if (sender is Window window)
                {
                    Stylus.SetIsPressAndHoldEnabled(window, false);
                    Stylus.SetIsFlicksEnabled(window, false);
                }
            })
        );
    }

}
