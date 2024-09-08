using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany
{
    public class ContrAgentViewModels
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }=string.Empty;
        public string FirstName { get; set; }= string.Empty;    
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber    { get; set; } = string.Empty;
        public decimal DebtSum { get; set; }
        public decimal PayedSum { get; set; }
        public decimal  LastPayedSum  { get; set; }
        public string LastPayedDate { get; set; }
    }
}
