using SmartMarket.Desktop.Components.MainForComponents;
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
        int id;
        List<Category> categories=new List<Category>();
        private List<Product> products = new List<Product>();
        public MainPage()
        {
            InitializeComponent();
            GetAllCategory();
            GetAllProduct();
        }

        private void btnProductCreate_Click(object sender, RoutedEventArgs e)
        {
            
        }




        public class Category()
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
        public class Product()
        {
            public int  Id { get; set; }
            public string P_code { get; set; }
            public string  ProductName { get; set; }
            public string Barcode { get; set; }
            public string Category { get; set; }
            public string Worker { get; set; }
            public string BodyPrice { get; set; }
            public int count { get; set; }
            public string TotalPrice { get; set; }
            public string Measure { get; set; }
            public string Price { get; set; }
        }


        
        public  void GetAllCategory()
        {
            categories = CategoryList();
            St_categoryList.Visibility = Visibility.Visible;
            St_categoryList.Children.Clear();
            foreach (var i in categories)
            {
                MainCategoryComponent categoryComponent = new MainCategoryComponent();
                categoryComponent.Tag = i.Id;
                categoryComponent.SetValues(i.Id,i.Name);
                categoryComponent.BorderThickness = new Thickness(2, 3,2,3);
                St_categoryList.Children.Add(categoryComponent);
            }
        }


        public void GetAllProduct()
        {
            var newProduct = ProductList();
            St_product.Visibility = Visibility.Visible;
            St_product.Children.Clear();
            foreach (var i in newProduct)
            {
                MainProductComponent component = new MainProductComponent();
                component.Tag = i.Id;
                component.SetValues(i.Id,i.P_code,i.Barcode,i.ProductName,i.Category,i.Worker,i.BodyPrice,i.count,i.TotalPrice,i.Measure,i.Price);
                component.BorderThickness = new Thickness(2);
                St_product.Children.Add(component);
            }
            
        }
        
        
        
        
        
        
        
        public List<Category> CategoryList()
        {
            categories.Add(new Category { Id = 1, Name = "choy" });
            categories.Add(new Category { Id = 2, Name = "choy" });
            categories.Add(new Category { Id = 3, Name = "choy" });
            categories.Add(new Category { Id = 4, Name = "choy" });
            categories.Add(new Category { Id = 5, Name = "choy" });
            categories.Add(new Category { Id = 6, Name = "choy" });
            categories.Add(new Category { Id = 7, Name = "choy" });
            categories.Add(new Category { Id = 8, Name = "choy" });
            categories.Add(new Category { Id = 9, Name = "choy" });
            return categories;
        }

        public List<Product> ProductList()
        {
            products.Add(new Product()
            {
                Id = 1, P_code = "000755", Barcode = "#14121001135", ProductName = "Nestle Kasha 9", Category = "Mevalar",
                Worker = "Sherzod", BodyPrice = "25000.0", count = 10, TotalPrice = "300000.0", Measure = "Dona",
                Price = "200000.0"
            });
            products.Add(new Product()
            {
                Id = 1, P_code = "000755", Barcode = "#14121001", ProductName = "Nestle Kasha 9", Category = "Mevalar",
                Worker = "Sherzod", BodyPrice = "25000.0", count = 10, TotalPrice = "300000.0", Measure = "Dona",
                Price = "200000.0"
            });
            products.Add(new Product()
            {
                Id = 1, P_code = "000755", Barcode = "#14121001", ProductName = "Nestle Kasha 9", Category = "Mevalar",
                Worker = "Sherzod", BodyPrice = "25000.0", count = 10, TotalPrice = "300000.0", Measure = "Dona",
                Price = "200000.0"
            });
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product(){Id = 1,P_code = "000755",Barcode = "#14121001",ProductName = "Nestle Kasha 9",Category = "Mevalar",Worker = "Sherzod",BodyPrice = "25000.0",count = 10,TotalPrice = "300000.0",Measure = "Dona",Price = "200000.0"});
            products.Add(new Product()
            {
                Id = 1, P_code = "000755", Barcode = "#14121001", ProductName = "Nestle Kasha 9", Category = "Mevalar",
                Worker = "Sherzod", BodyPrice = "25000.0", count = 10, TotalPrice = "300000.0", Measure = "Dona",
                Price = "200000.0"
            });
            return products;
        }
        
        
      

        
    }

}
