using SmartMarketDesktop.ViewModels.Entities.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.PayDesk
{
    [Table("pay_desk")]
    public class PayDeskView : BaseEntity
    {

        [Column("name")]
        public string Name { get; set; }
        [Column("income")]
        public double Income { get; set; }
        [Column("outlay")]
        public double Outlay { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<ExpenseView> ExpenseViews { get; set; }
        public List<ProductSaleView> ProductSaleViews { get; set; }
    }
}
