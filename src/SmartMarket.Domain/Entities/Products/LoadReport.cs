﻿using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Products
{
    [Table("load_report")]
    public class LoadReport : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("contragent_id")]
        public Guid ContrAgentId { get; set; }
        public ContrAgent ContrAgent { get; set; }

        [Column("total_price")]
        public double TotalPrice { get; set; }
        [Column("count")]
        public int Count { get; set; }
    }
}
