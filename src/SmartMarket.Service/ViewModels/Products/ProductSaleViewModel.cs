using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;

using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Service.ViewModels.Products
{
    public class ProductSaleViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Discount { get; set; }
        public double ItemTotalCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid SalesRequestId { get; set; }
        public SalesRequest SalesRequest { get; set; }

        public List<ReplaceProduct> ReplaceProducts { get; set; }
        public List<InvalidProduct> InvalidProducts { get; set; }
    }
}
