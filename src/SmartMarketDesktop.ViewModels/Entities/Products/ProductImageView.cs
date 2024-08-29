using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("product_image")]
    public class ProductImageView : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public ProductView ProductView { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
