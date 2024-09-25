using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.MainForPage;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{
    
    private ICategoryService _categoryService;
    private IContrAgentService _contrAgentService;
    private IProductService _productService;

    int NumberCategory = 1;
    int NumberContrAgent= 1; 
    int NumberProduct= 1;   
    
    public MainPage()
    {
        InitializeComponent();
        this._categoryService = new CategoryService();
        this._contrAgentService = new ContrAgentService();
        this._productService = new ProductService();  
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
     
        var categoryViews = await _categoryService.GetAllAsync();

        St_categoryList.Visibility = Visibility.Visible;
        St_categoryList.Children.Clear();
        foreach (var category in categoryViews)
        {
            MainCategoryComponent categoryComponent = new MainCategoryComponent();
            categoryComponent.tbNumber.Text = NumberCategory.ToString();
            categoryComponent.SetValues(category);
            St_categoryList.Children.Add(categoryComponent);    
            NumberCategory++;
        }
        
    }
    

    public async void GetAllContrAgents()
    {
        var contrAgents = await _contrAgentService.GetAll();  
        St_contrAgents.Visibility = Visibility.Visible;
        St_contrAgents.Children.Clear();
        foreach(var contrAgent in contrAgents)
        {
            MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
            mainKontrAgentComponent.Tag = contrAgent;
            mainKontrAgentComponent.tbNumber.Text = NumberContrAgent.ToString();
            mainKontrAgentComponent.GetData(contrAgent);
            St_contrAgents.Children.Add(mainKontrAgentComponent);
            NumberContrAgent++;
        }
    }
    

    public async void GetAllProducts()
    {
        var products = await _productService.GetAll();

        St_product.Visibility = Visibility.Visible; 
        St_product.Children.Clear();
        foreach (var product in products)
        {
            MainProductComponent productComponent = new MainProductComponent(); 
            productComponent.Tag = product;
            productComponent.tbNumber.Text = NumberProduct.ToString();
            productComponent.GetData(product);
            St_product.Children.Add(productComponent);
            NumberProduct++;
        }    
    }
    

  
    private void btnAddCategory_Click(object sender, RoutedEventArgs e)
    {
        CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
        categoryCreateWindow.ShowDialog();
    }

    private void btnAddKontrAgent_Click(object sender, RoutedEventArgs e)
    {
        ContrAgentCreateWindow contrAgentCreateWindow = new ContrAgentCreateWindow(); 
        contrAgentCreateWindow.ShowDialog();
    }

    private void btnAddCompany_Click(object sender, RoutedEventArgs e)
    {
        CompanyCreateWindow companyCreateWindow = new CompanyCreateWindow();
        companyCreateWindow.ShowDialog();   
    }

}
