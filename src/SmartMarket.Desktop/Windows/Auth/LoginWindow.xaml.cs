using System;
using System.Collections.Generic;
using System.Linq;
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
using SmartMarketDeskop.Integrated.Interfaces.Auth;
using SmartMarketDeskop.Integrated.Services.Auth;
using SmartMarketDesktop.DTOs.DTOs.Worker;

namespace SmartMarket.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService(); 
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void btnLogin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(tbusername.Text) || string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xatolik");
                return;
            }

            var loginDto = new WorkerLoginDto
            {
                PhoneNumber = tbusername.Text,
                Password = pbPassword.Password
            };

            try
            {
                var loginResult = await _authService.LoginAsync(loginDto);

                if (loginResult.Result)
                {
                    this.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Noto'g'ri telefon raqami yoki parol kiritildi. Iltimos, qayta urinib ko'ring.", "Xatolik");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xatolik");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
}