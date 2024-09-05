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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.ProductsForWindow
{
    /// <summary>
    /// Interaction logic for ProductCreateWindow.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        ICategoryService categoryService;
        IContrAgentService contrAgentService;


        List<CategoryView> categories = new List<CategoryView>();
        List<ContrAgentViewModels> contrAgents = new List<ContrAgentViewModels>();
        public ProductCreateWindow()
        {
            InitializeComponent();
            categoryService = new CategoryService();
            contrAgentService = new ContrAgentService();

            GetAllCategory();
            GetAllContrAgent();
        }

        private void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddProductDto addProductDto = new AddProductDto();

            addProductDto.BarCode = txtBarCode.Text;
            addProductDto.P_Code = txtPCode.Text;
            addProductDto.ProductName = txtProductName.Text;
            {
                CategoryView categoryView = categories.Where(a=>a.Name==comboCategory.SelectedValue).FirstOrDefault();
                addProductDto.CategoryId = categoryView.Id;
            }
            addProductDto.Count = int.Parse(txtQuantity.Text);
            addProductDto.Price=double.Parse(txtPrice.Text);
            addProductDto.SellPrice = double.Parse(txtProductPriceSum.Text);
            addProductDto.SellPricePercantage = double.Parse(txtProductPricePersentage.Text);
            addProductDto.UnitOfMeasure = comboMeasurement.SelectedValue.ToString();
            {
                ContrAgentViewModels contrAgentViewModels = contrAgents.Where(a => a.FirstName == comboDelivery.SelectedValue).FirstOrDefault();
                addProductDto.ContrAgentId = contrAgentViewModels.Id;
            }
            

            




        }

        private void btnCreateCategory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CategoryCreateWindow categoryCreate=new CategoryCreateWindow();
            categoryCreate.ShowDialog();
        }

        private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();   
        }
        
        public async void GetAllCategory()
        {
            categories=await categoryService.GetAllAsync();
            if(categories!=null && categories.Any())
            {
                comboCategory.ItemsSource = categories.Select(a=>a.Name);
                comboCategory.Items.Refresh();
            }
        }

        public async void GetAllContrAgent()
        {
            contrAgents = await contrAgentService.GetAll();
            if(contrAgents!=null && contrAgents.Any())
            {
                comboDelivery.ItemsSource = contrAgents.Select(a => a.FirstName);
                comboDelivery.Items.Refresh();
            }
        }

       
    }
}
