using Microsoft.Win32;

using Prism.Services.Dialogs;

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
        public SettingsScalesComponent()
        {
            _productService = new ProductService();
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


        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _selectedPath = openFileDialog.FileName;
            }

            //using (var dialog = new FolderBrowserDialog())
            //{
            //    dialog.Description = "Kerakli papkani tanlang";
            //    dialog.ShowNewFolderButton = true;

            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        _selectedFolderPath = dialog.SelectedPath;
            //        MessageBox.Show($"Tanlangan papka: {_selectedFolderPath}");
            //    }
            //}

            UpdateScaleFile();
        }


        public void SetData(string scaleName, int time)
        {
            tbScalesName.Text = scaleName;
            _updateInterval = time * 1000; 

            if (_updateTimer != null)
            {
                _updateTimer.Stop();
            }

            _updateTimer = new Timer(_updateInterval);
            _updateTimer.Elapsed += (s, e) => UpdateScaleFile();
            _updateTimer.Start();
        }

        private void UpdateScaleFile()
        {
            if (string.IsNullOrEmpty(_selectedPath))
            {
                notifierThis.ShowWarning("Iltimos, avval faylni tanlang!");
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
                notifierThis.ShowError("Faylni yangilashda xatolik.");
            }
        }

        private async void UpdateExistingFile(string filePath)
        {
            var productData = await GetProductDataAsync();
            File.WriteAllText(filePath, productData);
            notifier.ShowSuccess($"Fayl yangilandi.");
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
