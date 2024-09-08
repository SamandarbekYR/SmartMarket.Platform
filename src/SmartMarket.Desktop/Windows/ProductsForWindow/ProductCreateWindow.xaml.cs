using SmartMarket.Desktop.Windows.Category;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.Product;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.ProductImages;
using Microsoft.Win32;
using SmartMarket.Domain.Entities.Products;
using System.Windows.Media.Imaging;
using System.IO;

namespace SmartMarket.Desktop.Windows.ProductsForWindow
{
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

                GetAllCategory();
                GetAllContrAgent();
            }

            private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
            {
                AddProductDto addProductDto = new AddProductDto();

                addProductDto.BarCode = txtBarCode.Text;
                addProductDto.P_Code = txtPCode.Text;
                addProductDto.ProductName = txtProductName.Text;

                {
                    CategoryView categoryView = categories.Where(a => a.Name == comboCategory.SelectedValue).FirstOrDefault()!;
                    addProductDto.CategoryId = categoryView.Id;
                }

                addProductDto.Count = int.Parse(txtQuantity.Text);
                addProductDto.Price = double.Parse(txtPrice.Text);
                addProductDto.SellPrice = double.Parse(txtProductPriceSum.Text);
                addProductDto.SellPricePercantage = int.Parse(txtProductPricePersentage.Text);

                addProductDto.UnitOfMeasure = comboMeasurement.Text;
                {
                    ContrAgentViewModels contrAgentViewModels = contrAgents.Where(a => a.FirstName == comboDelivery.SelectedValue).FirstOrDefault()!;
                    addProductDto.ContrAgentId = contrAgentViewModels.Id;
                }

                addProductDto.PaymentStatus = "Active";
                addProductDto.NoteAmount=int.Parse(txtNoteAmount.Text);
                await productService.CreateProduct(addProductDto);
               
                ProductImageDto productImageDto = new ProductImageDto();
                productImageDto.ImagePath = imagepath;
               
                  this.Close();
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

           

            private void btnCreateCategory_MouseDown(object sender, MouseButtonEventArgs e)
            {
                CategoryCreateWindow categoryCreate = new CategoryCreateWindow();
                categoryCreate.ShowDialog();
            }
           

            private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
            {
                 Clear();
            }


            public async void GetAllCategory()
            {
                categories = await categoryService.GetAllAsync();
                if (categories != null && categories.Any())
                {
                    comboCategory.ItemsSource = categories.Select(a => a.Name);
                    comboCategory.Items.Refresh();
                }
            }

            public async void GetAllContrAgent()
            {
                contrAgents = await contrAgentService.GetAll();
                if (contrAgents != null && contrAgents.Any())
                {
                    comboDelivery.ItemsSource = contrAgents.Select(a => a.FirstName);
                    comboDelivery.Items.Refresh();
                }
            }


           

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                EnableBlur();
            }


        private void Path_MouseUp(object sender, MouseButtonEventArgs e)
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

        public void Clear()
        {
            txtBarCode.Text=txtPCode.Text=txtProductName.Text=comboCategory.Text=txtQuantity.Text=txtPrice.Text=
            txtProductPriceSum.Text=txtProductPricePersentage.Text=comboDelivery.Text=txtNoteAmount.Text=string.Empty;
        }

        private void BrClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Clear();
        }
    }
}
