using SmartMarket.DataAccess.Entities.Categories;
using SmartMarket.DataAccess.Entities.Orders;
using SmartMarket.DataAccess.Entities.PartnersCompany;
using SmartMarket.DataAccess.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Products
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [Column("category_id")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        [Column("contragent_id")]
        public Guid ContrAgentId { get; set; }
        public ContrAgent ContrAgent { get; set; }
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("barcode")]
        public string Barcode { get; set; } = string.Empty;
        [Column("pcode")]
        public string PCode { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("count")]
        public int Count { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("sell_price")]
        public double SellPrice { get; set; }
        [Column("sell_price_persentage")]
        public int SellPricePersentage { get; set; }
        [Column("unit_of_measure")]
        public string UnitOfMeasure { get; set; } = string.Empty;
        [Column("payment_status")]
        public string PaymentStatus {  get; set; } = string.Empty;

        public List<ProductImage> ProductImages { get; set; }
        public List<ProductSale> ProductSale { get; set; }
        public List<Debtors> Debtors { get; set; }
        public List<LoadReport> LoadReport { get; set; }
        public List<Order> Order { get; set; }
    }
}
