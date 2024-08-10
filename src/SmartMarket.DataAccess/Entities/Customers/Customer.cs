using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.DataAccess.Entities.Customers
{
    [Table("customer")]
    public class Customer : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("phone_Number2")]
        public string PhoneNumber2 { get; set; } = string.Empty;
        [Column("adress")]
        public string Adress { get; set; } = string.Empty;
    }
}
