using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.DTOs.DTOs.Product
{
    public class ProductImageDto
    {
        public string ImagePath { get; set; }=string.Empty;
        public Guid ProductId { get; set; }
    }
}
