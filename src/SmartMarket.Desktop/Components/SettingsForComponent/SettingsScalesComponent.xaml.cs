using Microsoft.Win32;
using System.Windows.Forms;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.ProductImages;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

using Path = System.IO.Path;
using Timer = System.Timers.Timer;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Scales;
using SmartMarketDeskop.Integrated.Services.Scales;
using SmartMarketDesktop.DTOs.DTOs.Scales;
using SmartMarket.Desktop.Windows.Settings;

namespace SmartMarket.Desktop.Components.SettingsForComponent
{
    /// <summary>
    /// Interaction logic for SettingsScalesComponent.xaml
    /// </summary>
    public partial class SettingsScalesComponent : UserControl
    {
        private Timer _updateTimer;
        private string _selectedPath;
        private string _selectedFolderPath;
        private int _updateInterval; 
        private IProductService _productService;
        private IScaleService _scaleService;
        public Func<Task> GetScales {  get; set; }
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

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 200,
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

            //_updateInterval = time * 1000; 

            //if (_updateTimer != null)
            //{
            //    _updateTimer.Stop();
            //}

            //_updateTimer = new Timer(_updateInterval);
            //_updateTimer.Elapsed += (s, e) => UpdateScaleFile();
            //_updateTimer.Start();
        }

        private async void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectScale = this.Tag as ScaleDto;
            if (selectScale != null)
            {
                bool result = await _scaleService.DeleteScaleAsync(selectScale.Id);

                if (result)
                    notifier.ShowError("Tarozini o'chirildi.");
                else
                    notifier.ShowError("Tarozini o'chirishda xatolik mavjud.");
            }
            else
                notifier.ShowError("Tarozini o'chirishda xatolik mavjud.");
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        { 
            ScaleUpdateWindow scaleUpdateWindow = new ScaleUpdateWindow();
            scaleUpdateWindow.Show();
        }

        private void UpdateScaleFile()
        {
            if (string.IsNullOrEmpty(_selectedPath))
            {
                //notifierThis.ShowWarning("Iltimos, avval faylni tanlang!");
                return;
            }

            try
            {
                if (File.Exists(_selectedPath))
                {
                    UpdateExistingFile(_selectedPath);
                }
            }
            catch 
            {
                //notifierThis.ShowError("Faylni yangilashda xatolik.");
            }
        }

        private async void UpdateExistingFile(string filePath)
        {
            var productData = await GetProductDataAsync();
            File.WriteAllText(filePath, productData);
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
