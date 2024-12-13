﻿using Microsoft.Win32;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.Expenses;
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
using S = SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Desktop.Windows.ProductsForWindow;

/// <summary>
/// Interaction logic for ProductUpdateWindow.xaml
/// </summary>
public partial class ProductUpdateWindow : Window
{
    private ICategoryService categoryService;
    private IContrAgentService contrAgentService;
    private IProductService productService;
    private ILoadReportService loadReportService;
    private IProductImageService productImageService;


    List<CategoryView> categories = new List<CategoryView>();
    List<ContrAgentViewModels> contrAgents = new List<ContrAgentViewModels>();
    private string imagepath;
    private Guid productId = Guid.Empty;
    private Guid workerId = Guid.Empty;
    private int productCount = 0;

    public ProductUpdateWindow()
    {
        InitializeComponent();
        categoryService = new CategoryService();
        contrAgentService = new ContrAgentService();
        productService = new ProductService();
        productImageService = new ProductImageService();
        loadReportService = new LoadReportService();
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

    public void SetData(S.FullProductDto product)
    {
        productId = product.Id;
        workerId = product.WorkerId;
        txtBarCode.Text = product.Barcode.ToString();
        txtProductName.Text = product.Name;
        comboCategory.SelectedValue = product.CategoryName;
        txtQuantity.Text = product.Count.ToString();
        txtPrice.Text = product.Price.ToString();
        txtProductPriceSum.Text = product.SellPrice.ToString();
        int persentage = (int)((product.SellPrice - product.Price) * 100 / product.Price);
        comboMeasurement.Text = product.UnitOfMeasure;
        comboDelivery.Text = product.WorkerFirstName;
        txtNoteAmount.Text = product.NoteAmount.ToString();

        productCount = product.Count;
    }

    private async void btnUpdate_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (btnUpdateContainer.IsEnabled == false) return;

        btnUpdateContainer.IsEnabled = false;
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

            if (comboCategory.SelectedValue != null)
            {
                CategoryView categoryView = categories.Where(a => a.Name == comboCategory.SelectedValue.ToString()).FirstOrDefault()!;
                addProductDto.CategoryId = categoryView.Id;
            }
            else
            {
                notifierThis.ShowWarning("Kategoriya tanlanmagan.");
                btnUpdateContainer.IsEnabled = true;
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0 ||
                !double.TryParse(txtPrice.Text, out double price) || price <= 0 ||
                !double.TryParse(txtProductPriceSum.Text, out double sellPrice) || sellPrice <= 0)
            {
                notifierThis.ShowError("Mahsulot soni yoki narxi 0 bo'lmasligi kerak.");
                btnUpdateContainer.IsEnabled = true;
                return;
            }

            addProductDto.Count = quantity;
            addProductDto.Price = price;
            addProductDto.SellPrice = sellPrice;

            addProductDto.UnitOfMeasure = comboMeasurement.Text;
            if (comboDelivery.SelectedValue != null)
            {
                ContrAgentViewModels contrAgentViewModels = contrAgents.Where(a => a.FirstName == comboDelivery.SelectedValue).FirstOrDefault()!;
                addProductDto.ContrAgentId = contrAgentViewModels.Id;
            }
            else
            {
                notifierThis.ShowWarning("Yetkazib beruvchi tanlanmagan");
                btnUpdateContainer.IsEnabled = true;
                return;
            }

            addProductDto.WorkerId = workerId;
            addProductDto.PaymentStatus = "Active";
            addProductDto.NoteAmount = int.Parse(txtNoteAmount.Text);

            var previousCount = int.Parse(txtQuantity.Text);
            var res = await Task.Run(async () => await productService.UpdateProduct(addProductDto, productId));

            ProductImageDto productImageDto = new ProductImageDto();
            productImageDto.ImagePath = imagepath;

            if (res)
            {
                var countDifference = quantity;

                if(countDifference > 0)
                {
                    var loadReport = new AddLoadReportDto
                    {
                        WorkerId = workerId,
                        ProductId = productId,
                        ContrAgentId = addProductDto.ContrAgentId,
                        TotalPrice = (addProductDto.SellPrice * countDifference),
                        Count = countDifference,
                    };

                    var loadReportResult = await loadReportService.AddAsync(loadReport);
                }

                this.Close();
                notifier.ShowInformation("Mahsulot muvaffaqiyatli o'zgartirildi.");
            }
            else
            {
                notifierThis.ShowError("Mahsulot yaratishda xato yuz berdi.");
                btnUpdateContainer.IsEnabled = true;
            }
        }
        else
        {
            notifierThis.ShowError("Mahsulot malumotlari to'liq emas.");
            btnUpdateContainer.IsEnabled = true;
        }
    }

    private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Clear();
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

    public async Task GetAllCategory()
    {
        categories = await Task.Run(async () => await categoryService.GetAllAsync());
        if (categories != null && categories.Any())
        {
            comboCategory.ItemsSource = categories.Select(a => a.Name);
            comboCategory.Items.Refresh();
        }
    }

    public async Task GetAllContrAgent()
    {
        contrAgents = await Task.Run(async () => await contrAgentService.GetAll());
        if (contrAgents != null && contrAgents.Any())
        {
            comboDelivery.ItemsSource = contrAgents.Select(a => a.FirstName);
            comboDelivery.Items.Refresh();
        }
    }

    public void Clear()
    {
        txtBarCode.Text = txtProductName.Text = comboCategory.Text = txtQuantity.Text = txtPrice.Text =
        txtProductPriceSum.Text = comboDelivery.Text = txtNoteAmount.Text = string.Empty;
    }

    private void BrClose_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.Close();
        Clear();
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

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        await GetAllCategory();
        await GetAllContrAgent();
    }
}
