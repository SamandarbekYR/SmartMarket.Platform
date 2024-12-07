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
        private Timer _updateTimer;
        private int _updateInterval = 120;
        private IProductService _productService;
        private IScaleService _scaleService;
        private List<Timer> _updateTimers = new List<Timer>();
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

        public void SetData(int id, string scaleName, int time, string selectPath, string fileName)
        {
            lb_Count.Content = id;
            lb_Name.Content = scaleName;
            lb_UpdateTime.Content = time;
            lb_FileName.Content = fileName;
            lb_Location.Content = selectPath;

            StartUpdateTimer(time, selectPath);
        }

        private void StartUpdateTimer(int interval, string selectPath)
        {
            var timer = new Timer(interval * 1000); 
            timer.Elapsed += (s, e) => UpdateScaleFile(selectPath, interval);
            timer.Start();

            _updateTimers.Add(timer);
        }

        private async void UpdateScaleFile(string selectedPath, int interval)
        {
            int asd = interval;
            if (File.Exists(selectedPath))
            {
                try
                {
                    var productData = await GetProductDataAsync();

                    using (var fileStream = new FileStream(selectedPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                        {
                            await writer.WriteAsync(productData);
                        }
                    }
                }
                catch
                {
                   // Dispatcher.Invoke(() => notifier.ShowError("Faylni yangilashda xatolik."));
                }
            }
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectScale = this.Tag as ScaleDto;
            if (selectScale != null)
            {
                bool result = await _scaleService.DeleteScaleAsync(selectScale.Id);

                if (result)
                    Dispatcher.Invoke(() => notifier.ShowError("Tarozini o'chirildi."));
                else
                    Dispatcher.Invoke(() => notifier.ShowError("Tarozini o'chirishda xatolik mavjud."));
            }
            else
                Dispatcher.Invoke(() => notifier.ShowError("Tarozini o'chirishda xatolik mavjud."));
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectScale = this.Tag as ScaleDto;
            ScaleUpdateWindow scaleUpdateWindow = new ScaleUpdateWindow(selectScale);
            scaleUpdateWindow.Show();
        }

        private void SelectFile(string selectedPath)
        {
            if (string.IsNullOrEmpty(selectedPath))
                return;

            try
            {
                if (File.Exists(selectedPath))
                {
                    UpdateExistingFile(selectedPath);
                }
            }
            catch
            {
                //notifierThis.ShowError("Faylni yangilashda xatolik.");
            }
        }
        private async void UpdateExistingFile(string filePath)
        {
            if (IsFileInUse(filePath))
            {
                Dispatcher.Invoke(() => notifier.ShowError("Fayl hozirda boshqa jarayon tomonidan ishlatilmoqda."));
                return;
            }

            try
            {
                var productData = await GetProductDataAsync();

                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        await writer.WriteAsync(productData);
                    }
                }

                Dispatcher.Invoke(() => notifier.ShowInformation("Fayl muvaffaqiyatli yangilandi."));
            }
            catch 
            {
                Dispatcher.Invoke(() => notifier.ShowError("Faylni yangilashda xatolik."));
            }
        }

        private bool IsFileInUse(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true; 
            }
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
