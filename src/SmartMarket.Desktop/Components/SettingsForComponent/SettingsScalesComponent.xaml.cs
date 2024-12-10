using SmartMarket.Desktop.Windows.Settings;

using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Scales;

using SmartMarketDesktop.DTOs.DTOs.Scales;

using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

using Timer = System.Timers.Timer;

namespace SmartMarket.Desktop.Components.SettingsForComponent
{
    /// <summary>
    /// Interaction logic for SettingsScalesComponent.xaml
    /// </summary>
    public partial class SettingsScalesComponent : UserControl
    {
        private IScaleService _scaleService;
        private IProductService _productService;

        private Timer _updateTimer;
        private int _updateInterval = 120;

        public event Func<Task> OnPageReload;
        private List<KeyValuePair<ScaleDto, Timer>> _updateTimers = new List<KeyValuePair<ScaleDto, Timer>>();
        public Func<Task> GetScales { get; set; }
        public SettingsScalesComponent()
        {
            _productService = new ProductService();
            _scaleService = new ScaleService();
            InitializeComponent();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public void SetData(int id, ScaleDto? scale)
        {
            lb_Count.Content = id;
            lb_Name.Content = scale?.Name;
            lb_UpdateTime.Content = scale?.UpdateTimeInterval;
            lb_FileName.Content = scale?.TXTFileName;
            lb_Location.Content = scale?.SelectFilePath;

            StartUpdateTimer(scale);
        }

        private void StartUpdateTimer(ScaleDto scale)
        {
            var timer = new Timer(scale.UpdateTimeInterval * 1000); 
            timer.Elapsed += (s, e) => UpdateScaleFile(scale.SelectFilePath);
            timer.Start();

            _updateTimers.Add(new KeyValuePair<ScaleDto, Timer>(scale, timer));
        }

        private async void UpdateScaleFile(string selectedPath)
        {
            try
            {
                var productData = await GetProductDataAsync();

                using (var fileStream = new FileStream(selectedPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(fileStream, new UTF8Encoding(false))) // BOMsiz UTF-8 yozish
                    {
                        await writer.WriteAsync(productData);
                    }
                }
            }
            catch 
            {
                // Dispatcher.Invoke(() => notifier.ShowError($"Faylni yangilashda xatolik: {ex.Message}"));
            }
        }


        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectScale = this.Tag as ScaleDto;

            var timerEntry = _updateTimers.FirstOrDefault(t => t.Key.Id == selectScale?.Id);
            if (timerEntry.Key != null)
            {
                timerEntry.Value.Stop();
                _updateTimers.Remove(timerEntry);
            }

            if (selectScale != null)
            {
                bool result = await _scaleService.DeleteScaleAsync(selectScale.Id);

                if (result)
                {
                    Dispatcher.Invoke(() => notifier.ShowSuccess("Tarozini o'chirildi."));
                    await (OnPageReload?.Invoke() ?? Task.CompletedTask);
                }
                else
                    Dispatcher.Invoke(() => notifier.ShowError("Tarozini o'chirishda xatolik mavjud."));
            }
            else
                Dispatcher.Invoke(() => notifier.ShowError("Tarozini o'chirishda xatolik mavjud."));
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectScale = this.Tag as ScaleDto;
            ScaleUpdateWindow scaleUpdateWindow = new ScaleUpdateWindow();
            scaleUpdateWindow.SetData(selectScale);

            scaleUpdateWindow.Closed += async (s, args) =>
            {
                await (OnPageReload?.Invoke() ?? Task.CompletedTask); 
            };

            scaleUpdateWindow.Show();
        }

        private async Task<string> GetProductDataAsync()
        {
            var products = await _productService.GetAll();
            var dataBuilder = new StringBuilder();

            long id = 1;
            foreach (var product in products)
            {
                int unitOfMeasure = product.UnitOfMeasure switch
                {
                    "dona" or "litr" => 1,
                    "kg" => 0,
                    _ => -1
                };

                dataBuilder.AppendLine(
                    $"{id};{product.Name};;{product.SellPrice};0;0;0;{product.PCode};0;0;;" +
                    $"{DateTime.Now:dd/MM/yy};{unitOfMeasure};0;0;0;{DateTime.Now:dd/MM/yy}");

                id++;
            }

            return dataBuilder.ToString();
        }

    }
}
