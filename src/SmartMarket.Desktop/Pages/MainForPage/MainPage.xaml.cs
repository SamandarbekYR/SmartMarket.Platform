using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.MainForPage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        private ICategoryService categoryService;
        private IContrAgentService contrAgentService;
        private IProductService productService;

        List<CategoryView> categoryViews=new List<CategoryView>();
        List<ContrAgentViewModels> contrAgents=new List<ContrAgentViewModels>();
        List<ProductViewModels> products = new List<ProductViewModels>(); 
        int NumberCategory = 1;
        int NumberContrAgent= 1; 
        int NumberProduct= 1;   
        
        public MainPage()
        {
            InitializeComponent();
            this.categoryService = new CategoryService();
            this.contrAgentService = new ContrAgentService();
            this.productService = new ProductService();  
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllCategory();
            GetAllContrAgents();
            GetAllProducts();
        }

        private void btnProductCreate_Click(object sender, RoutedEventArgs e)
        {
            ProductCreateWindow productCreateWindow = new ProductCreateWindow();
            productCreateWindow.ShowDialog();
        }


        public async void GetAllCategory()
        {
         
            categoryViews=await this.categoryService.GetAllAsync();

            St_categoryList.Visibility = Visibility.Visible;
            St_categoryList.Children.Clear();
            foreach (var i in categoryViews)
            {
                MainCategoryComponent categoryComponent = new MainCategoryComponent();    
                categoryComponent.Tag = i;
                categoryComponent.SetValues(NumberCategory, i.Name);
                NumberCategory++;
                categoryComponent.BorderThickness=new Thickness(3,2,3,2);
                St_categoryList.Children.Add(categoryComponent);    
            }
            
        }
        

        public async void GetAllContrAgents()
        {
            contrAgents = await this.contrAgentService.GetAll();  
            St_contrAgents.Visibility = Visibility.Visible;
            St_contrAgents.Children.Clear();
            foreach(var i in contrAgents)
            {
                
                MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();    
                mainKontrAgentComponent.Tag = i;
                mainKontrAgentComponent.GetData(NumberContrAgent,i.CompanyName,i.FirstName,i.LastName,i.PhoneNumber,i.DebtSum,i.PayedSum,i.LastPayedSum,i.LastPayedDate);
                NumberContrAgent++;
                mainKontrAgentComponent.BorderThickness= new Thickness(3,2,3,2);
                St_contrAgents.Children.Add(mainKontrAgentComponent);
            }
        }
        

        public async void GetAllProducts()
        {
           products=await productService.GetAll();

            St_product.Visibility = Visibility.Visible; 
            St_product.Children.Clear();
            foreach (var i in products)
            {
                MainProductComponent productComponent = new MainProductComponent(); 
                productComponent.Tag = i;
                productComponent.GetData(NumberProduct, i.P_Code, i.BarCode, i.ProductName, i.CateogoryName, i.WorkerName, i.Price, i.Count, i.TotalPrice, i.UnitOfMeasure, i.SellPrice);
                productComponent.BorderThickness=new Thickness(3,2,3,3);
                St_product.Children.Add(productComponent);
            }    
        }
        

      
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryCreateWindow categoryCreateWindow=new CategoryCreateWindow();
            categoryCreateWindow.ShowDialog();
        }

        private void btnAddKontrAgent_Click(object sender, RoutedEventArgs e)
        {
            ContrAgentCreateWindow contrAgentCreateWindow=new ContrAgentCreateWindow(); 
            contrAgentCreateWindow.ShowDialog();
        }

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            CompanyCreateWindow companyCreateWindow = new CompanyCreateWindow();
            companyCreateWindow.ShowDialog();   
        }

    }

}
