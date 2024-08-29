using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("worker_role")]
    public class WorkerRoleView : BaseEntity
    {

        [Column("role_name")]
        public string RoleName { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
        public List<WorkerView> WorkerViews { get; set; }
    }
}
