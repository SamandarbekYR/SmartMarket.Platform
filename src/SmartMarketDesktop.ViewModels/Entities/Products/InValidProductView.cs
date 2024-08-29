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
    public class InValidProductView : BaseEntity
    {

        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("product_sale_id")]
        public Guid ProductSaleViewId { get; set; }
        public ProductSaleView ProductSaleView { get; set; }

        [Column("return_reason")]
        public string ReturnReason { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
