using SmartMarket.Service.DTOs.Products.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.DTOs.Products.Product
{
    public class FullProductDto
    {
        public Guid Id { get; set; }
        public string PCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public double Price { get; set; }
        public double SellPrice { get; set; }
        public int Count { get; set; }
        public string UnitOfMeasure { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Guid WorkerId { get; set; }
        public string WorkerFirstName { get; set; } = string.Empty;
        public string WorkerLastName { get; set; } = string.Empty;
        public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
        public double NoteAmount { get; set; }
    }
}
