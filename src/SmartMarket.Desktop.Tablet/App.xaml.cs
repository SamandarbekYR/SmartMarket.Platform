using System.Windows;
using System.Windows.Input;

namespace SmartMarket.Desktop.Tablet;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        EventManager.RegisterClassHandler(
            typeof(Window),
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
