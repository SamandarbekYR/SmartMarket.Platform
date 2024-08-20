using SmartMarket.Desktop.Components.ShopDetailsForComponent;
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

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    /// <summary>
    /// Interaction logic for ShopHIstoryPage.xaml
    /// </summary>
    public partial class ShopHIstoryPage : Page
    {
        List<Product> products=new List<Product>();
        public ShopHIstoryPage()
        {
            InitializeComponent();
            GetAllProduct();
        }




        public void GetAllProduct()
        { 

            St_productList.Visibility = Visibility.Visible;
            St_productList.Children.Clear();
            var newlist = ProductList();

            foreach (var item in newlist)
            {
                ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                shopDetailsProductComponent.Tag = item.Id;
                shopDetailsProductComponent.SetValues(item.Id, item.TransactionNumber, item.ProductName, item.Barcode, item.Category, item.Worker,
                    item.Discount, item.count, item.TotalPrice, item.Kasa, item.Price, item.Date);
                shopDetailsProductComponent.BorderThickness = new Thickness(2);
                St_productList.Children.Add(shopDetailsProductComponent);
            }
        }




        public class Product()
        {
            public int Id { get; set; }

            public string TransactionNumber { get; set; }
            public string ProductName { get; set; }
            public string Barcode { get; set; }
            public string Category { get; set; }
            public string Worker { get; set; }

            public string Discount { get; set; }
            public int count { get; set; }
            public string TotalPrice { get; set; }
            public string Kasa { get; set; }
            public string Price { get; set; }
            public string Date { get; set; }
        }



        public List<Product> ProductList()
        {
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#141214455135",
                ProductName = "Nestle Kasha 9",
                Category = "Mevalar",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#14121001135",
                ProductName = "Nestle Kasha 9",
                Category = "kasha",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#14121001135",
                ProductName = "Nestle Kasha 9",
                Category = "Mevalar",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#14121001135",
                ProductName = "Nestle Kasha 9",
                Category = "Mevalar",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#14121001135",
                ProductName = "Nestle Kasha 9",
                Category = "Mevalar",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            products.Add(new Product()
            {
                Id = 1,
                TransactionNumber = "0007512555",
                Barcode = "#14121001135",
                ProductName = "Nestle Kasha 9",
                Category = "Mevalar",
                Worker = "Sherzod",
                Discount = "0%",
                count = 10,
                TotalPrice = "300000.0",
                Kasa = "#2",
                Price = "200000.0",
                Date = "20/08/2024",
            });
            return products;
        }
    }
}
