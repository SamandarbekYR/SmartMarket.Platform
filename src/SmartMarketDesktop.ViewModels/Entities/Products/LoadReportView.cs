using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("load_report")]
    public class LoadReportView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public ProductView ProductView { get; set; }
        [Column("contragent_id")]
        public Guid ContrAgentId { get; set; }
        public ContrAgentView ContrAgentView { get; set; }

        [Column("total_price")]
        public double TotalPrice { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
