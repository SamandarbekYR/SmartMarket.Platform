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

namespace SmartMarket.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnLogin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

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
