using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.ViewModelsForUI.Products
{
    public class ProductViewModels
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }=string.Empty;
        public string P_Code { get; set; }=string.Empty ;
        public string ProductName { get; set; } = string.Empty ;
        public string CateogoryName { get; set; } = string.Empty;
        public string MyProperty { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
        public string UnitOfMeasure { get; set; } = string.Empty;
        public decimal SellPrice { get; set; }
    }
}
