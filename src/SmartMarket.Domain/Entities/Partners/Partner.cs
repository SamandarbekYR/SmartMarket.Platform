using SmartMarket.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Partners
{
    [Table("partner")]
    public class Partner : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber {  get; set; } = string.Empty;
        [Column("total_debt")]
        public double TotalDebt { get; set; }
        [Column("last_payment")]
        public DateTime LastPayment {  get; set; }
        public string PaymentType { get; set; }

        public List<Debtors> Debtors { get; set; }
    }
}
