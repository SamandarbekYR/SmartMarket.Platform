using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("position")]
    public class PositionView : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
        public List<WorkerView> WorkerViews { get; set; }
    }
}
