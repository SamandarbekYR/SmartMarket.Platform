using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void EditBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {

            //txtName.Text = categoryView.Name;
            //txtDescribtion.Text=categoryView.Description;

            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Name=txtName.Text;
            categoryDto.Description=txtDescribtion.Text;

            await categoryServer.UpdateAsync(categoryDto, categoryView.Id);

         //   if (_mainPage != null) _mainPage.GetAllCategory();
            Clear();
            this.Close();
        }


        public void GetData(CategoryView _category)
        {
            categoryView = _category;
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
