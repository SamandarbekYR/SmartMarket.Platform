using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.PayDesks
{
    [Table("pay_desk")]
    public class PayDesk : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("income")]
        public double Income { get; set; }
        [Column("outlay")]
        public double Outlay { get; set; }

        public List<ContrAgentPayment> ContrAgentPayments { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<ProductSale> ProductSales { get; set; }
    }
}
