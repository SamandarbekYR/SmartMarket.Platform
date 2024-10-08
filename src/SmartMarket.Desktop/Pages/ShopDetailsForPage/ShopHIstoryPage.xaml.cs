using SmartMarket.Desktop.Components.ShopDetailsForComponent;

using SmartMarketDeskop.Integrated.Services.Products.ProductSale;

using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages.ShopDetailsForPage
{
    /// <summary>
    /// Interaction logic for ShopHIstoryPage.xaml
    /// </summary>
    public partial class ShopHIstoryPage : Page
    {
        List<Product> products=new List<Product>();
        private IProductSaleService _productSaleService;

        private int rowNumber = 1;

        public ShopHIstoryPage()
        {
            InitializeComponent();
            _productSaleService = new ProductSaleService();
            GetAllProduct();
        }

        public async void GetAllProduct()
        { 
            var productSales = await _productSaleService.GetAllAsync();
            St_productList.Visibility = Visibility.Visible;
            St_productList.Children.Clear();
            var newlist = ProductList();

            foreach (var item in productSales)
            {
                ShopDetailsProductComponent shopDetailsProductComponent = new ShopDetailsProductComponent();
                shopDetailsProductComponent.Tag = rowNumber;
                shopDetailsProductComponent.SetValues(rowNumber, item.TransactionNumber, item.ProductName, item.BarCode, item.CategoryName, item.WorkerName,
                6, item.Count, item.TotalCost, "2", item.Price, item.CreatedDate);
                shopDetailsProductComponent.BorderThickness = new Thickness(2);
                St_productList.Children.Add(shopDetailsProductComponent);
                rowNumber++;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllProduct();
        }
    }
}
