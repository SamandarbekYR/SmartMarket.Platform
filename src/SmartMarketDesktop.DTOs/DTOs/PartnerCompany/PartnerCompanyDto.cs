using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.DTOs.DTOs.PartnerCompany
{
    public class PartnerCompanyDto
    {
        public string Name { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Describtion { get; set; } = string.Empty;
        public bool IsSynced { get; set; }
    }
}
