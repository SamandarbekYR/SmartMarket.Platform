using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.DTOs.DTOs.PartnerCompany
{
    public class ContrAgentDto
    {
        public Guid CompanyId { get; set; }
        public string FirstName { get; set; }=string .Empty;    
        public string LastName { get; set; } =string .Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsSynced { get; set; }

    }
}
