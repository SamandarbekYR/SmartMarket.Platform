using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;
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

namespace SmartMarket.Desktop.Components.ExpenseForComponents
{
    /// <summary>
    /// Interaction logic for ProductsComponent.xaml
    /// </summary>
    public partial class ProductsComponent : UserControl
    {
        private readonly IProductServer server;
        public ProductsComponent()
        {
            InitializeComponent();
            this.server = new ProductServer();
        }
        public Guid ProductId { get; set; }
        //ProductDto ni orniga ProductViewga otqazish kerak.
        public void SetData(ProductView product)
        {
            tbPCode.Text = product.PCode;
            tbBarcode.Text = product.Barcode;
            tbProductName.Text = product.Name;
            tbCategory.Text = product.CategoryView.Name;
            tbWorker.Text = product.WorkerView.FirstName;
            tbSellPrice.Text = product.SellPrice.ToString();
            tbTotalPrice.Text = (product.SellPrice * product.Count).ToString();
            tbCount.Text = product.Count.ToString();
            tbPrice.Text = product.Price.ToString();
            tbUnitOfMeasure.Text = product.UnitOfMeasure;
        }
    }
}
