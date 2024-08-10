using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Products
{
    [Table("product_image")]
    public class ProductImage : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("image_path")]
        public string ImagePath { get; set; } = string.Empty;
    }
}
