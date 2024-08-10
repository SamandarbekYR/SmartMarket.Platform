using SmartMarket.DataAccess.Entities.Expenses;
using SmartMarket.DataAccess.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.PayDesks
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

        public List<Expense> Expenses { get; set; }
        public List<ProductSale> ProductSales { get; set; }
    }
}
