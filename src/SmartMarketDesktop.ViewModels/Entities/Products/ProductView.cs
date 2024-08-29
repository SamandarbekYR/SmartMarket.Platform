using SmartMarketDesktop.ViewModels.Entities.Categories;
using SmartMarketDesktop.ViewModels.Entities.Orders;
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
    [Table("product")]
    public class ProductView : BaseEntity
    {

        [Column("category_id")]
        public Guid CategoryViewId { get; set; }
        public CategoryView CategoryView { get; set; }
        [Column("contragent_id")]
        public Guid ContrAgenViewtId { get; set; }
        public ContrAgentView ContrAgentView { get; set; }
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }

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
        public string PaymentStatus { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<ProductImageView> ProductImageViews { get; set; }
        public List<ProductSaleView> ProductSaleViews { get; set; }
        public List<DebttorsView> DebttorsViews { get; set; }
        public List<LoadReportView> LoadReportViews { get; set; }
        public List<OrderView> OrderViews{ get; set; }
    }
}
