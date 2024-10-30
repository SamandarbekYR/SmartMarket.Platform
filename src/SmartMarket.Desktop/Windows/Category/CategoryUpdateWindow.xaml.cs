using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using ToastNotifications;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;

namespace SmartMarket.Desktop.Windows.Category
{
    /// <summary>
    /// Interaction logic for CategoryUpdateWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        private ICategoryServer categoryServer;
       
        CategoryView categoryView;
        public CategoryUpdateWindow()
        {
            InitializeComponent();
            
            this.categoryServer = new SmartMarketDeskop.Integrated.Server.Repositories.Categories.CategoryServer();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void EditBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtDescribtion.Text))
            {
                //txtName.Text = categoryView.Name;
                //txtDescribtion.Text=categoryView.Description;

                CategoryDto categoryDto = new CategoryDto();
                categoryDto.Name=txtName.Text;
                categoryDto.Description=txtDescribtion.Text;

                var res = await categoryServer.UpdateAsync(categoryDto, categoryView.Id);

                if(res)
                {
                 //   if (_mainPage != null) _mainPage.GetAllCategory();
                    Clear();
                    this.Close();
                }
                else
                {
                    notifier.ShowError("Category o'zgartirishda xatolik yuz berdi!");
                }

            }
            else
            {
                notifier.ShowWarning("Category malumotlarini to'liq emas!");
            }
        }


        public void GetData(CategoryView _category)
        {
            categoryView =_category ;
        }


        public void Clear()
        {
            txtDescribtion.Text=txtName.Text=string.Empty;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
    }
}
