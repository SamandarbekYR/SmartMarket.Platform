using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using System.Management;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.SettingsForPage;

/// <summary>
/// Interaction logic for SettingsPrinterPage.xaml
/// </summary>
public partial class SettingsPrinterPage : Page
{

    private readonly IPayDeskService _payDeskService;

    public SettingsPrinterPage()
    {
        InitializeComponent();

        this._payDeskService = new PayDeskService();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetPrinters();
        await GetPaydesks();
    }

    private async Task GetPaydesks()
    {
        var paydesk = await Task.Run(async () => await _payDeskService.GetAll());
        if (paydesk.Count > 0)
        {
            foreach (var item in paydesk)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = item.Name;
                comboBoxItem.Tag = item.Id;
                comboBoxItem.FontSize = 18;
                cb_Paydesk.Items.Add(comboBoxItem);
            }
        }
        else
        {
            ComboBoxItem item = new ComboBoxItem()
            {
                Content = "Kassa topilmadi.",
                IsEnabled = false,
                FontSize = 18
            };
            cb_Paydesk.Items.Add(item);
        }
    }

    private void GetPrinters()
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
            Properties.Settings.Default.Save();
        }
    }

    private void cb_Paydesk_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cb_Paydesk.SelectedItem is ComboBoxItem selectedItem)
        {
            Properties.Settings.Default.PayDesk = selectedItem.Tag.ToString();
            Properties.Settings.Default.PayDeskName = selectedItem.Content.ToString();
            Properties.Settings.Default.Save();
            IdentitySingelton.GetInstance().PayDeskId = Guid.Parse(selectedItem.Tag.ToString()!);
            IdentitySingelton.GetInstance().PayDeskName = selectedItem.Content.ToString()!;
            
        }
    }
}
