using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Pages.MainForPage;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{

    private ICategoryService _categoryService;
    private IContrAgentService _contrAgentService;
    private IProductService _productService;

    int categoryCount = 1;
    int productCount = 1;
    int contragenCount = 1;

    public MainPage()
    {
        InitializeComponent();
        this._categoryService = new CategoryService();
        this._contrAgentService = new ContrAgentService();
        this._productService = new ProductService();
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await GetAllProducts();
        await GetAllCategory();
        await GetAllContrAgents();
    }

    private void btnAddCategory_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
        categoryCreateWindow.ShowDialog();
    }

    private void btnProductCreate_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ProductCreateWindow productCreateWindow = new ProductCreateWindow();
        productCreateWindow.ShowDialog();
    }

    private void btnContrAgentCreate_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ContrAgentCreateWindow contrAgentCreateWindow = new ContrAgentCreateWindow();
        contrAgentCreateWindow.ShowDialog();
    }

    public async Task GetAllCategory()
    {
        //var categoryViews = await _categoryService.GetAllAsync();

        //St_categoryList.Children.Clear();
        //foreach (var category in categoryViews)
        //{
        //    MainCategoryComponent categoryComponent = new MainCategoryComponent();
        //    categoryComponent.Tag = category;
        //    categoryComponent.SetValues(category, categoryCount);
        //    St_categoryList.Children.Add(categoryComponent);
        //    categoryCount++;
        //}


        for (int i = 0; i < 30; i++)
        {
            MainCategoryComponent categoryComponent = new MainCategoryComponent();
            St_categoryList.Children.Add(categoryComponent);
        }
    }


    private MainCategoryComponent selectedControl = null!;
    public void SelectCategory(MainCategoryComponent category)
    {
        if (selectedControl != null)
        {
            selectedControl.brCategory.Background = Brushes.White;
        }

        category.brCategory.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6")); ;

        selectedControl = category;
    }

    public async Task GetAllContrAgents()
    {
        //var contrAgents = await _contrAgentService.GetAll();

        //St_contragent.Children.Clear();

        //foreach (var contrAgent in contrAgents)
        //{
        //    MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
        //    mainKontrAgentComponent.Tag = contrAgent;
        //    mainKontrAgentComponent.GetData(contrAgent, contragenCount);
        //    St_contragent.Children.Add(mainKontrAgentComponent);
        //    contragenCount++;
        //}

        for (int i = 0; i < 15; i++)
        {
            MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
            St_contragent.Children.Add(mainKontrAgentComponent);
        }
    }

    public async Task GetAllProducts()
    {
        //var products = await _productService.GetAll();

        //St_product.Children.Clear();
        //foreach (var product in products)
        //{
        //    MainProductComponent productComponent = new MainProductComponent();
        //    productComponent.Tag = product;
        //    productComponent.GetData(product, productCount);
        //    St_product.Children.Add(productComponent);
        //    productCount++;
        //}

        for (int i = 0; i < 15; i++)
        {
            MainProductComponent productComponent = new MainProductComponent();
            St_product.Children.Add(productComponent);
        }
    }
}
