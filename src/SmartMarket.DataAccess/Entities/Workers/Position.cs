using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("position")]
    public class Position : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        public List<Worker> Workers { get; set; } 
    }
}
