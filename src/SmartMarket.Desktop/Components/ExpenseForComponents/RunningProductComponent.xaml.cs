using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace SmartMarket.Desktop.Components.ExpenseForComponents
{
    /// <summary>
    /// Interaction logic for RunningProductComponent.xaml
    /// </summary>
    public partial class RunningProductComponent : UserControl
    {
        private readonly IProductService productService;
        public RunningProductComponent()
        {
            InitializeComponent();
            this.productService = new ProductService();
        }
        public Guid ProductId { get; set; }

        public void SetData(ProductDto dto)
        {
            var imgPath = @"D:\SmartPartnersProjects\src\SmartMarket.Desktop\Assets\Basket.png";

            tbName.Content = dto.Name;
            tbCount.Content = dto.Count;
            tbPrice.Content = dto.Price;
            productImg.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute)); //imgPath dto yoqligi uchun testga localdan
        }
    }
}
