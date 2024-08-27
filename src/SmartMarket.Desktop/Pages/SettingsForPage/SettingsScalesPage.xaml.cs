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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartMarket.Desktop.Components.SettingsForComponent;

namespace SmartMarket.Desktop.Pages.SettingsForPage
{
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
}
