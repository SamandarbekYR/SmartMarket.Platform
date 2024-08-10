using SmartMarket.DataAccess.Entities.Partners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Products
{
    [Table("debtors")]
    public class Debtors : BaseEntity
    {
        [Column("partner_id")]
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Column("description")] 
        public string Description { get; set; } = string.Empty;
        [Column("debt_sum")]
        public double DeptSum { get; set; }

        public List<DebtPayment> DebtPayment { get; set; }
    }
}
