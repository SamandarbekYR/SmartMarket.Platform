using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.Workers
{
    [Table("worker_role")]
    public class WorkerRole : BaseEntity
    {
        [Column("role_name")]
        public string RoleName { get; set; } = string.Empty;
        public List<Worker> Workers { get; set; }
    }
}
