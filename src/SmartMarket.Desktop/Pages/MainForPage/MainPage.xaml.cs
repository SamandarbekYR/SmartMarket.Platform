using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SmartMarket.Desktop.Pages.MainForPage;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{

    private ICategoryService _categoryService;
    private IContrAgentService _contrAgentService;
    private IProductService _productService;

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
        St_categoryList.Children.Clear();

        var categoryViews = await Task.Run(async () => await _categoryService.GetAllAsync());
        CategoryLoader.Visibility = Visibility.Collapsed;

        int categoryCount = 1;

        if (categoryViews.Count > 0)
        {
            foreach (var category in categoryViews)
            {
                MainCategoryComponent categoryComponent = new MainCategoryComponent();
                categoryComponent.Tag = category;
                categoryComponent.SetValues(category, categoryCount);
                St_categoryList.Children.Add(categoryComponent);
                categoryCount++;
            }
        }
        else
        {
            EmptyDataCategory.Visibility = Visibility.Visible;
        }
    }


    private MainCategoryComponent selectedControl = null!;
    public async void SelectCategory(MainCategoryComponent category, Guid categoryId)
    {
        if (selectedControl != null)
        {
            selectedControl.brCategory.Background = Brushes.White;
        }

        category.brCategory.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6")); 

        await GetProductsByCategoryId(categoryId);

        selectedControl = category;
    }

    public async Task GetAllContrAgents()
    {
        St_contragent.Children.Clear();

        var contrAgents = await Task.Run(async () => await _contrAgentService.GetAll());
        ContragentLoader.Visibility = Visibility.Collapsed;

        int contragenCount = 1;

        if (contrAgents.Count > 0)
        {
            foreach (var contrAgent in contrAgents)
            {
                MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
                mainKontrAgentComponent.Tag = contrAgent;
                mainKontrAgentComponent.GetData(contrAgent, contragenCount);
                St_contragent.Children.Add(mainKontrAgentComponent);
                contragenCount++;
            }
        }
        else
        {
            EmptyDataContragent.Visibility = Visibility.Visible;
        }
    }

    public async Task GetProductsByCategoryId(Guid Id)
    {
        St_product.Children.Clear();
        EmptyDataProduct.Visibility = Visibility.Collapsed;

        var products = await Task.Run(async () => await _productService.GetByCategoryId(Id));
        ProductLoader.Visibility = Visibility.Collapsed;

        int productCount = 1;

        if (products.Count > 0)
        {
            foreach (var product in products)
            {
                MainProductComponent productComponent = new MainProductComponent();
                productComponent.Tag = product;
                productComponent.GetData(product, productCount);
                St_product.Children.Add(productComponent);
                productCount++;
            }
        }
        else
        {
            EmptyDataProduct.Visibility = Visibility.Visible;
        }
    }

    public async Task GetAllProducts()
    {
        St_product.Children.Clear();

        var products = await Task.Run(async () => await _productService.GetAll());
        ProductLoader.Visibility = Visibility.Collapsed;    

        int productCount = 1;

        if (products.Count > 0)
        {
            foreach (var product in products)
            {
                MainProductComponent productComponent = new MainProductComponent();
                productComponent.Tag = product;
                productComponent.GetData(product, productCount);
                St_product.Children.Add(productComponent);
                productCount++;
            }
        }
        else
        {
            EmptyDataProduct.Visibility = Visibility.Visible;
        }
    }

}
