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

namespace SmartMarket.Desktop.Pages.SettingsForPage
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            SettingsPrinterPage printerPage = new SettingsPrinterPage();
            SettingsPageNavigator.Content = printerPage;
        }

        private void btnPrinter_Click(object sender, RoutedEventArgs e)
        {
            SettingsPrinterPage printerPage = new SettingsPrinterPage();
            SettingsPageNavigator.Content = printerPage;    
        }

        private void btnScales_Click(object sender, RoutedEventArgs e)
        {
            SettingsScalesPage scalesPage = new SettingsScalesPage();   
            SettingsPageNavigator.Content = scalesPage;
        }
    }
}
