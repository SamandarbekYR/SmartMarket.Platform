using Microsoft.Win32;

using SmartMarketDeskop.Integrated.Services.Scales;

using SmartMarketDesktop.DTOs.DTOs.Scales;

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Windows.Settings
{
    /// <summary>
    /// Interaction logic for ScaleCreateWindow.xaml
    /// </summary>
    public partial class ScaleCreateWindow : Window
    {
        private readonly IScaleService _scaleService;
        public Func<Task> CreateScale {  get; set; }

        private string _selectedPath;
        public ScaleCreateWindow()
        {
            _scaleService = new ScaleService();
            InitializeComponent();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 40,
                offsetY: 40);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(2),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        Notifier notifierthis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 40,
                offsetY: 40);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(2),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });


        private async void Scale_Create_Button_Click(object sender, RoutedEventArgs e)
        {
            AddScaleDto scale = new AddScaleDto();
            scale.Name = tb_ScaleName.Text;
            scale.UpdateTimeInterval = int.Parse(tb_UpdateTime.Text);
            scale.SelectFilePath = _selectedPath;

            if (scale.SelectFilePath == null)
                notifier.ShowWarning("Fayl tanlanmagan.");
            else
                scale.TXTFileName = scale.SelectFilePath.Substring(scale.SelectFilePath.LastIndexOf('\\') + 1);

            bool result = await _scaleService.CreateScaleAsync(scale);

            if (result)
            {
                this.Close();
                notifier.ShowSuccess("Tarozi yaratildi.");
                await CreateScale();
            }
            else
                notifierthis.ShowError("Tarozi yaratishda qandaydir xatolik bor !");
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedPath = openFileDialog.FileName;
            }
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void TimeTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextNumeric(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }
    }
}
