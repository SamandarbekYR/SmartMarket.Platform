using SmartMarketDesktop.ViewModels.Entities.Products;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Orders
{
    [Table("order")]
    public class OrderView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public ProductView ProductView { get; set; }

        [Column("transaction_number")]
        public string TransactionNumber { get; set; } = string.Empty;
        [Column("count")]
        public int Count { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
