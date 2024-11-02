using System.Management;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.SettingsForPage;

/// <summary>
/// Interaction logic for SettingsPrinterPage.xaml
/// </summary>
public partial class SettingsPrinterPage : Page
{
    public SettingsPrinterPage()
    {
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

        bool printerFound = false;

        foreach (ManagementObject printer in searcher.Get())
        {
            string printerName = printer["Name"].ToString()!;

            if (printer["PortName"] != null && printer["PortName"].ToString()!.ToLower().Contains("usb"))
            {
                cb_Printers.Items.Add(new ComboBoxItem { Content = printerName, FontSize = 18 });
                printerFound = true;
            }
        }
        if (!printerFound)
        {
            ComboBoxItem noPrinterItem = new ComboBoxItem
            {
                Content = "Printer topilmadi.",
                IsEnabled = false,
                FontSize = 18
            };
            cb_Printers.Items.Add(noPrinterItem);
        }
    }

    private void cb_Printers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(cb_Printers.SelectedItem is  ComboBoxItem selectedItem)
        {
            Properties.Settings.Default.PrinterName = selectedItem.Content.ToString();
        }
    }
}
