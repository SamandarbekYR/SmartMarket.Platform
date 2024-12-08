using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Category;

/// <summary>
/// Interaction logic for CategoryCreateWindow.xaml
/// </summary>
public partial class CategoryCreateWindow : Window
{
    private ICategoryService categoryService;
    
    public CategoryCreateWindow()
    {
        InitializeComponent();
        
        this.categoryService = new SmartMarketDeskop.Integrated.Services.Categories.Category.CategoryService();
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
            offsetX: 50,
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
            offsetX: 50,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });


    private async void btnCategoryCreate_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (btnCategoryCreate.IsEnabled == false)
            return;

        btnCategoryCreate.IsEnabled = false;

        if (!string.IsNullOrEmpty(txtCategoryName.Text) && !string.IsNullOrEmpty(txtdescribtion.Text))
        {
            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Name = txtCategoryName.Text;
            categoryDto.Description = txtdescribtion.Text;
            bool result = await categoryService.AddAsync(categoryDto);

            if (result)
            {
                Clear();
                this.Close();
                notifier.ShowSuccess("Kategoriya muvafaqqiyatli qo'shildi");
            }
            else
            {
                notifierThis.ShowError("Kategoriya qo'shishda xatolik yuz berdi");
                btnCategoryCreate.IsEnabled = true;
            }

        }
        else
        {
            notifierThis.ShowWarning("Kategoriya malumotlarini to'liq emas!");
            btnCategoryCreate.IsEnabled = true;
        }
    }
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
        Clear();
    }

    public void Clear()
    {
        txtCategoryName.Text=txtdescribtion.Text=string.Empty;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
    }
}
