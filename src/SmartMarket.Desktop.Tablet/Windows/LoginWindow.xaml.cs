using SmartMarketDeskop.Integrated.Interfaces.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Services.Auth;
using SmartMarketDesktop.DTOs.DTOs.Auth;
using Squirrel;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Tablet.Windows;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{

    private IAuthService _authService;

    public LoginWindow()
    {
        InitializeComponent();
        this._authService = new AuthService();
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private bool IsInternetAvailable()
    {
        try
        {
            using (Ping ping = new Ping())
            {
                PingReply reply = ping.Send("www.google.com");
                return (reply.Status == IPStatus.Success);
            }
        }
        catch
        {
            return false;
        }
    }

    private void SetIdentitySingleton(string token)
    {
        var t = TokenHandler.ParseToken(token);
        var identity = IdentitySingelton.GetInstance();
        identity.Token = token;
        identity.Id = t.Id;
        identity.FirstName = t.FirstName;
        identity.LastName = t.LastName;
        identity.PhoneNumber = t.PhoneNumber;
        identity.RoleName = t.RoleName;
    }

    private async void Login_Button_Click(object sender, RoutedEventArgs e)
    {
        //try
        //{
        //    if (IsInternetAvailable())
        //    {
        //        Login_Button.Visibility = Visibility.Collapsed;
        //        Loader.Visibility = Visibility.Visible;

        //        UserLoginDto dto = new UserLoginDto();
        //        dto.PhoneNumber = tbPhoneNumber.Text;
        //        dto.password = pbPassword.Password.ToString();
        //        (bool Result, string Token) result = await _authService.LoginAsync(dto);

        //        if (result.Result)
        //        {
        //            TokenHandler.ParseToken(result.Token);
        //            SetIdentitySingleton(result.Token);

        //            Loader.Visibility = Visibility.Collapsed;
        //            Login_Button.Visibility = Visibility.Visible;

        //            MainWindow window = new MainWindow();
        //            window.Show();
        //            this.Close();
                    
        //        }
        //        else
        //        {
        //            notifier.ShowWarning("Bunday foydalanuvchi mavjud emas!");
        //            Loader.Visibility = Visibility.Collapsed;
        //            Login_Button.Visibility = Visibility.Visible;
        //        }
        //    }
        //    else
        //    {
        //        notifier.ShowWarning("Internetingizni tekshiring.");
        //    }
        //}
        //catch
        //{

        //}

        MainWindow window = new MainWindow();
        window.Show();
        this.Close();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Loader.Visibility = Visibility.Collapsed;
        //await CheckForUpdates();
    }

    private async Task CheckForUpdates()
    {
        using (var updateManager = await UpdateManager.GitHubUpdateManager("https://github.com/SamandarbekYR/SmartMarket.Platform"))
        {
            var updateResult = await updateManager.UpdateApp();

            if (updateResult != null)
            {
                UpdateManager.RestartApp();
            }
        }
    }

    private void btnVisible_Click(object sender, RoutedEventArgs e)
    {
        string password = pbPassword.Password;
        tbPassword.Text = password;
        tbPassword.Visibility = Visibility.Visible;
        pbPassword.Visibility = Visibility.Collapsed;
        btnVisible.Visibility = Visibility.Collapsed;
        btnDisVisible.Visibility = Visibility.Visible;
    }

    private void btnDisVisible_Click(object sender, RoutedEventArgs e)
    {
        string password = tbPassword.Text;
        pbPassword.Password = password;
        tbPassword.Visibility = Visibility.Collapsed;
        pbPassword.Visibility = Visibility.Visible;
        btnVisible.Visibility = Visibility.Visible;
        btnDisVisible.Visibility = Visibility.Collapsed;
    }
}
