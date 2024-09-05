using SmartMarket.Desktop.Components.MainForComponents;
using SmartMarket.Desktop.Windows.Category;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDeskop.Integrated.Services.Categories.Category;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages.MainForPage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        private ICategoryService categoryService;
        private IContrAgentService contrAgentService;
        List<CategoryView> categoryViews=new List<CategoryView>();
        List<ContrAgentViewModels> contrAgents=new List<ContrAgentViewModels>();    
        int NumberCategory = 1;
        int NumberContrAgent= 1;    
        
        public MainPage()
        {
            InitializeComponent();
            this.categoryService = new CategoryService();
            this.contrAgentService = new ContrAgentService();
            GetAllCategory();
            GetAllContrAgents();
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
                categoryComponent.SetValues(NumberCategory,i.Name);
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
