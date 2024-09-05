using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.DTOs.DTOs.Product
{
    public class AddProductDto
    {
        public string BarCode { get; set; }=string.Empty;
        public string P_Code { get; set; }=string.Empty ;   
        public string ProductName { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid ContrAgentId { get; set; }
        public Guid WorkerId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double  SellPrice { get; set; }
        public double  SellPricePercantage { get; set; }
        public string UnitOfMeasure { get; set; }
        public string PaymentStatus { get; set; }=string.Empty;
    }
}
