using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
