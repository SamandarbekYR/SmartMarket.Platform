using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.DTOs.Expence
{
    public class FilterExpenseDto
    {
        public Guid? PayDeskId { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? Reason { get; set; }
        public string? WorkerName { get; set; }
    }
}
