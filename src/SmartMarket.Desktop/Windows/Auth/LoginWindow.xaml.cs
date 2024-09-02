using SmartMarketDeskop.Integrated.Interfaces.Auth;
using SmartMarketDeskop.Integrated.Services.Auth;
using SmartMarketDesktop.DTOs.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using SmartMarketDeskop.Integrated.Security;
using System.IdentityModel.Tokens.Jwt;

namespace SmartMarket.Desktop.Windows.Auth
{
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

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(IsInternetAvailable())
                {
                    UserLoginDto dto = new UserLoginDto();
                    dto.PhoneNumber = tbPhoneNumber.Text;
                    dto.password = pbPassword.Password.ToString();
                    (bool Result, string Token) result = await _authService.LoginAsync(dto);
                    
                    if (result.Result)
                    {

                        IdentitySingelton.GetInstance().Token = TokenHandler.ParseToken(result.Token).Token;
                       
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        notifier.ShowWarning("Bunday foydalanuvchi mavjud emas !");

                    }
                }
                else
                {
                    notifier.ShowWarning("Internetizni tekshiring");
                }
               // notifier.Dispose();
            }
            catch (Exception ex) { }

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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
             this.Close();
        }
    }
}
