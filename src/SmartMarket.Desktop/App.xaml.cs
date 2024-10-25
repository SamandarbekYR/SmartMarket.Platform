using System.Windows;

namespace SmartMarket.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await CheckForUpdates();
    }

    private async Task CheckForUpdates()
    {
        //using (var updateManager = await UpdateManager.GitHubUpdateManager("https://github.com/SamandarbekYR/SmartMarket.Platform"))
        //{
        //    await updateManager.UpdateApp();
        //}
    }
}
