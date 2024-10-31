using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Repositories.Categories;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Category
{
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
            if(!string.IsNullOrEmpty(txtCategoryName.Text) && !string.IsNullOrEmpty(txtdescribtion.Text))
            {
                CategoryDto categoryDto = new CategoryDto();
                categoryDto.Name = txtCategoryName.Text;
                categoryDto.Description = txtdescribtion.Text;
                bool result = await categoryService.AddAsync(categoryDto);

                if (result)
                {
                    Clear();
                    this.Close();
                    notifier.ShowSuccess("Category muvafaqqiyatli qo'shildi");

                }
                else
                    notifier.ShowError("Category qo'shishda xatolik yuz berdi");

            }
            else
            {
                notifier.ShowWarning("Category malumotlarini to'liq emas!");
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
}
