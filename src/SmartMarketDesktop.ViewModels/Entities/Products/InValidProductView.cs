using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("invalid_product")]
    public class InValidProductView
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("product_sale_id")]
        public Guid ProductSaleId { get; set; }
        public ProductSaleView ProductSaleView { get; set; }

        [Column("return_reason")]
        public string ReturnReason { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
