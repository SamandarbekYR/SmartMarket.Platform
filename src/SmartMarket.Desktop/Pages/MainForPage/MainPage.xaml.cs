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
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Desktop.Components.Loader;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;

namespace SmartMarket.Desktop.Pages.MainForPage;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{

    private ICategoryService _categoryService;
    private IContrAgentService _contrAgentService;
    private IProductService _productService;
    private IPartnerCompanyService _partnerCompanyService;

    public MainPage()
    {
        InitializeComponent();
        this._categoryService = new CategoryService();
        this._contrAgentService = new ContrAgentService();
        this._productService = new ProductService();
        this._partnerCompanyService = new PartnerCompanyService();
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await GetAllProducts();
        await GetAllCategory();
        await GetAllContrAgents();
        await GetAllCompany();
    }

    private async void btnAddCategory_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
        categoryCreateWindow.ShowDialog();
        await GetAllCategory();
    }

    private async void btnProductCreate_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ProductCreateWindow productCreateWindow = new ProductCreateWindow();
        productCreateWindow.ShowDialog();
        await GetAllProducts();
    }

    private async void btnContrAgentCreate_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ContrAgentCreateWindow contrAgentCreateWindow = new ContrAgentCreateWindow();
        contrAgentCreateWindow.ShowDialog();
        await GetAllContrAgents();
    }
    private async void btnAddCompany_Click(object sender, RoutedEventArgs e)
    {
        CompanyCreateWindow companyCreateWindow = new CompanyCreateWindow();
        companyCreateWindow.ShowDialog();
        await GetAllCompany();
    }

    public async Task GetAllCategory()
    {
        CategoryLoader.Visibility = Visibility.Visible;
        St_categoryList.Children.Clear();

        var categoryViews = await Task.Run(async () => await _categoryService.GetAllAsync());
        CategoryLoader.Visibility = Visibility.Collapsed;

        int categoryCount = 1;

        if (categoryViews.Count > 0)
        {
            EmptyDataCategory.Visibility = Visibility.Collapsed;

            foreach (var category in categoryViews)
            {
                MainCategoryComponent categoryComponent = new MainCategoryComponent();
                categoryComponent.Tag = category;
                categoryComponent.SetValues(category, categoryCount);
                categoryComponent.GetCategorys = GetAllCategory;
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

    public async Task GetAllCompany()
    {
        CompanyLoader.Visibility = Visibility.Visible;
        St_Company.Children.Clear();

        var companies = await Task.Run(async () => await _partnerCompanyService.GetAllCompany());
        CompanyLoader.Visibility = Visibility.Collapsed;

        int companyCount = 1;

        if(companies.Count > 0)
        {
            EmptyDataCompany.Visibility = Visibility.Collapsed;

            foreach(var company in companies)
            {
                MainCompanyComponent mainCompanyComponent = new MainCompanyComponent();
                mainCompanyComponent.Tag = company;
                mainCompanyComponent.SetValues(company, companyCount);
                mainCompanyComponent.GetAllCompanies = GetAllCompany;
                St_Company.Children.Add(mainCompanyComponent);
                companyCount++;
            }
        }
        else
        {
            EmptyDataCompany.Visibility = Visibility.Visible;
        }
    }

    public async Task GetAllContrAgents()
    {
        ContragentLoader.Visibility = Visibility.Visible;
        St_contragent.Children.Clear();

        var contrAgents = await Task.Run(async () => await _contrAgentService.GetAll());
        ContragentLoader.Visibility = Visibility.Collapsed;

        int contragenCount = 1;

        if (contrAgents.Count > 0)
        {
            EmptyDataContragent.Visibility = Visibility.Collapsed;

            foreach (var contrAgent in contrAgents)
            {
                MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
                mainKontrAgentComponent.Tag = contrAgent;
                mainKontrAgentComponent.GetData(contrAgent, contragenCount);
                mainKontrAgentComponent.GetContrAgents = GetAllContrAgents;
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

    private CancellationTokenSource _cancellationTokenSource;
    private async void tb_search_ProductTextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        string search = tb_search_Product.Text;

        try
        {
            await Task.Delay(500, token);
        }
        catch (Exception ex)
        {
            return;
        }

        if (token.IsCancellationRequested)
        {
            EmptyDataProduct.Visibility = Visibility.Collapsed;
            return;
        }

        St_product.Children.Clear();
        EmptyDataProduct.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            ProductLoader.Visibility = Visibility.Visible;
            try
            {
                await Task.Run(async () =>
                {
                    if (search.Length >= 1)
                    {
                        var products = await _productService.GetByProductName(search);

                        foreach (var product in products)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                SetProduct(product);
                            });
                        }
                    }
                }, token);
            }
            catch (TaskCanceledException)
            {

            }
            finally
            {
                ProductLoader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            EmptyDataProduct.Visibility = Visibility.Collapsed;
            await GetAllProducts();
        }
    }

    private void SetProduct(FullProductDto product)
    {
        ProductLoader.Visibility = Visibility.Collapsed;
        St_product.Children.Clear();
        int count = 1;

        if (product != null)
        {
            EmptyDataProduct.Visibility = Visibility.Collapsed;

            MainProductComponent mainProductComponent = new MainProductComponent();
            mainProductComponent.Tag = product;
            mainProductComponent.GetData(product, count);
            St_product.Children.Add(mainProductComponent);
            count++;
        }
        else
        {
            EmptyDataProduct.Visibility = Visibility.Visible;
        }
    }

    private async void tb_search_ContrAgentTextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        string search = tb_search_ContrAgent.Text;

        try
        {
            await Task.Delay(500, token);
        }
        catch(TaskCanceledException)
        {
            return;
        }

        if(token.IsCancellationRequested)
        {
            EmptyDataContragent.Visibility = Visibility.Collapsed;
            return;
        }

        St_contragent.Children.Clear();
        EmptyDataContragent.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrEmpty(search))
        {
            ContragentLoader.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(async () =>
                {
                    if(search.Length >= 1)
                    {
                        var contrAgents = await _contrAgentService.GetByName(search);

                        foreach(var contrAgent in contrAgents)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                SetContrAgent(contrAgent);
                            });
                        }
                    }
                }, token);
            }
            catch (TaskCanceledException)
            {

            }
            finally
            {
                ContragentLoader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            St_contragent.Children.Clear();
            EmptyDataContragent.Visibility = Visibility.Collapsed;

            await GetAllContrAgents();
        }
    }

    private void SetContrAgent(ContrAgentViewModels contrAgent)
    {
        ContragentLoader.Visibility = Visibility.Collapsed;
        St_contragent.Children.Clear();

        int count = 1;

        if(contrAgent != null)
        {
            EmptyDataContragent.Visibility = Visibility.Collapsed;

            MainKontrAgentComponent mainKontrAgentComponent = new MainKontrAgentComponent();
            mainKontrAgentComponent.Tag = contrAgent;
            mainKontrAgentComponent.GetData(contrAgent, count);
            St_contragent.Children.Add(mainKontrAgentComponent);
            count++;
        }
        else
        {
            EmptyDataContragent.Visibility = Visibility.Visible;
        }
    }

    public async Task GetAllProducts()
    {
        ProductLoader.Visibility = Visibility.Visible;
        St_product.Children.Clear();

        var products = await Task.Run(async () => await _productService.GetAll());
        ProductLoader.Visibility = Visibility.Collapsed;    

        int productCount = 1;

        if (products.Count > 0)
        {
            EmptyDataProduct.Visibility = Visibility.Collapsed;

            foreach (var product in products)
            {
                MainProductComponent productComponent = new MainProductComponent();
                productComponent.Tag = product;
                productComponent.GetData(product, productCount);
                productComponent.GetProducts = GetAllProducts;
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
