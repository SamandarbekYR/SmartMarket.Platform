using Microsoft.Win32;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Service.DTOs.Products.Product;

using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.ProductImages;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.Product;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

using AddProductDto = SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for ProductCreateWindow.xaml
/// </summary>
public partial class ProductCreateWindow : Window
{
    ICategoryService categoryService;
    IContrAgentService contrAgentService;
    IProductService productService;
    IProductImageService productImageService;


    List<CategoryView> categories = new List<CategoryView>();
    List<ContrAgentViewModels> contrAgents = new List<ContrAgentViewModels>();
    string imagepath;
    public ProductCreateWindow()
    {
        InitializeComponent();
        categoryService = new CategoryService();
        contrAgentService = new ContrAgentService();
        productService = new ProductService();
        productImageService = new ProductImageService();
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

    private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(btnCreateContainer.IsEnabled == false) return;

        btnCreateContainer.IsEnabled = false;
        AddProductDto addProductDto = new AddProductDto();

        if (!string.IsNullOrWhiteSpace(txtBarCode.Text) &&
            !string.IsNullOrEmpty(txtProductName.Text) &&
            !string.IsNullOrWhiteSpace(txtQuantity.Text) &&
            !string.IsNullOrWhiteSpace(txtPrice.Text) &&
            !string.IsNullOrWhiteSpace(txtProductPriceSum.Text) &&
            !string.IsNullOrWhiteSpace(comboMeasurement.Text) &&
            !string.IsNullOrWhiteSpace(txtNoteAmount.Text))
        {
            addProductDto.BarCode = txtBarCode.Text;
            addProductDto.Name = txtProductName.Text;

            if (comboCategory.SelectedValue is Guid categoryId)
            {
                addProductDto.CategoryId = categoryId;
            }
            else
            {
                notifierThis.ShowWarning("Kategoriya tanlanmagan.");
                btnCreateContainer.IsEnabled = true;
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0 ||
                !double.TryParse(txtPrice.Text, out double price) || price <= 0 ||
                !double.TryParse(txtProductPriceSum.Text, out double sellPrice) || sellPrice <= 0)
            {
                notifierThis.ShowError("Mahsulot soni yoki narxi 0 bo'lmasligi kerak.");
                btnCreateContainer.IsEnabled = true;
                return;
            }

            addProductDto.Count = quantity;
            addProductDto.Price = price;
            addProductDto.SellPrice = sellPrice;

            var payDeskId = Properties.Settings.Default.PayDesk;
            if (string.IsNullOrEmpty(payDeskId))
            {
                notifierThis.ShowWarning("Kassa tanlanmagan.");
                btnCreateContainer.IsEnabled = true;
                return;
            }

            addProductDto.UnitOfMeasure = comboMeasurement.Text;
            if (comboDelivery.SelectedValue != null)
            {
                ContrAgentViewModels contrAgentViewModels = contrAgents!.Where(a => a.FirstName == comboDelivery.SelectedValue).FirstOrDefault()!;
                addProductDto.ContrAgentId = contrAgentViewModels.Id;
            }
            else
            {
                notifierThis.ShowWarning("Yetkazib beruvchi tanlanmagan.");
                btnCreateContainer.IsEnabled = true;
                return;
            }

            addProductDto.PaymentStatus = "Active";
            addProductDto.NoteAmount = int.Parse(txtNoteAmount.Text);
        
            bool result = await productService.CreateProduct(addProductDto);
            if (result)
            {
                this.Close();
                notifier.ShowSuccess("Maxsulot yaratildi.");
            }
            else
            {
                notifierThis.ShowError("Maxsulot yaratishda xatolik maxjud.");
                btnCreateContainer.IsEnabled = true;
            }

            ProductImageDto productImageDto = new ProductImageDto();
            productImageDto.ImagePath = imagepath;
        }
        else
        {
            notifierThis.ShowWarning("Mahsulot malumotlari to'liq emas.");
            btnCreateContainer.IsEnabled = true;
        }
    }

    private void txtBarCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
    }

    private void txtBarCode_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (txtBarCode.Text.Length == 13)
        {
            ProcessBarCode(txtBarCode.Text);
        }
        Clear();
    }

    private void ProcessBarCode(string barCode)
    {
        Task.Run(async () =>
        {
            var existingProduct = await productService.GetByBarCode(barCode);
            if (existingProduct != null)
            {
                Dispatcher.Invoke(() => FillInputsFromProduct(existingProduct));
            }
            else
            {
                Dispatcher.Invoke(() => notifierThis.ShowWarning("Mahsulot mavjud emas. Yangi mahsulot qo'shing."));
            }
        });
    }

    private void FillInputsFromProduct(FullProductDto product)
    {
        txtBarCode.Text = product.Barcode;
        txtProductName.Text = product.Name;
        comboCategory.SelectedValue = product.CategoryId;
        txtQuantity.Text = product.Count.ToString();
        txtPrice.Text = product.Price.ToString();
        txtProductPriceSum.Text = product.SellPrice.ToString();
        comboMeasurement.Text = product.UnitOfMeasure;
        txtNoteAmount.Text = product.NoteAmount.ToString();
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

    private void phone_number_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        string text = textBox.Text;
        string filteredText = Regex.Replace(text, "[^0-9]+", "");

        if (text != filteredText)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = filteredText;
            textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
        }
    }

    private void btnCreateCategory_MouseDown(object sender, MouseButtonEventArgs e)
    {
        CategoryCreateWindow categoryCreate = new CategoryCreateWindow();
        categoryCreate.ShowDialog();
    }

    private void btnCreateContrAgent_MouseDown(object sender, MouseButtonEventArgs e)
    {
        ContrAgentCreateWindow contrAgentCreate = new ContrAgentCreateWindow();
        contrAgentCreate.ShowDialog();
    }


    private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Clear();
    }

    public async void GetAllCategory()
    {
        categories = await Task.Run(async () => await categoryService.GetAllAsync());
        if (categories != null && categories.Any())
        {
            comboCategory.ItemsSource = categories;
            comboCategory.Items.Refresh();
        }
    }

    public async void GetAllContrAgent()
    {
        contrAgents = await Task.Run(async () => await contrAgentService.GetAll());
        if (contrAgents != null && contrAgents.Any())
        {
            comboDelivery.ItemsSource = contrAgents.Select(a => a.FirstName);
            comboDelivery.Items.Refresh();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        GetAllCategory();
        GetAllContrAgent();
    }

    public void Clear()
    {
        txtProductName.Text = comboCategory.Text = txtQuantity.Text = txtPrice.Text =
        txtProductPriceSum.Text = comboDelivery.Text = txtNoteAmount.Text = string.Empty;
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";
        if (openFileDialog.ShowDialog() == true)
        {
            imagepath = openFileDialog.FileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagepath, UriKind.Absolute);
            bitmap.EndInit();
            lbImage.Content = Path.GetFileName(imagepath);
        }
    }

    private void btn_Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        Clear();
    }
}
