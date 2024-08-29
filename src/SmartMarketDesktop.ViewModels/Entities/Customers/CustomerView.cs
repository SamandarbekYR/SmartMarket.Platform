using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Customers
{
    [Table("customer")]
    public class CustomerView : BaseEntity
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
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
