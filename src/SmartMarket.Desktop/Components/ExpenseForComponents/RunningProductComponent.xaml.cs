using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
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
    /// Interaction logic for RunningProductComponent.xaml
    /// </summary>
    public partial class RunningProductComponent : UserControl
    {
        public RunningProductComponent()
        {
            InitializeComponent();
        }

        public void SetData(ProductDto product)
        {
            tbName.Content = product.Name;
            tbPrice.Content = product.Price;
            tbCount.Content = product.Count;
        }
    }
}
