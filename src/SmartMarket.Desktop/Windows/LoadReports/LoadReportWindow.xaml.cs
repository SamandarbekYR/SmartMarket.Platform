using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Services.Expenses;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.LoadReports
{
    /// <summary>
    /// Interaction logic for LoadReportWindow.xaml
    /// </summary>
    public partial class LoadReportWindow : Window
    {
        private ILoadReportService _loadReportService;
        public LoadReportWindow()
        {
            InitializeComponent();
            _loadReportService = new LoadReportService();
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
        public Guid ContrAgentId { get; set; }
        public void SetContrAgentId(Guid contrAgentId)
        {
            ContrAgentId = contrAgentId;
        }

        public async void GetAllCargoReport()
        {
            St_CargoReports.Children.Clear();

            var loadReports = await Task.Run(async () => await _loadReportService.GetByContrAgentIdAsync(ContrAgentId));

            showLoadReport(loadReports);
        }

        private async void FilterLoadReport()
        {
            Loader.Visibility = Visibility.Visible;
            St_CargoReports.Children.Clear();
            FilterLoadReportDto loadReportDto = new FilterLoadReportDto();

            if (fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                loadReportDto.FromDateTime = fromDateTime.SelectedDate.Value;
                loadReportDto.ToDateTime = toDateTime.SelectedDate.Value;
            }

            if (searchTextBox != null)
            {
                loadReportDto.ProductName = searchTextBox.Text;
            }

            var filterLoadReports = await Task.Run(async () => await _loadReportService.FilterAsync(loadReportDto));

            var results = filterLoadReports.Where(
                x => x.ContrAgentId == ContrAgentId).ToList();

            showLoadReport(results);
        }

        private void showLoadReport(IEnumerable<LoadReportDto> loadReports)
        {
            Loader.Visibility = Visibility.Collapsed;
            St_CargoReports.Children.Clear();

            int count = 1;

            if (loadReports != null)
            {
                foreach (var report in loadReports)
                {
                    CargoReportComponent cargoReportComponent = new CargoReportComponent();
                    cargoReportComponent.tbNumber.Text = count.ToString();
                    cargoReportComponent.SetData(report);
                    St_CargoReports.Children.Add(cargoReportComponent);
                    count++;
                }
            }
            else { }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterLoadReport();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FilterLoadReport();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            GetAllCargoReport();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
