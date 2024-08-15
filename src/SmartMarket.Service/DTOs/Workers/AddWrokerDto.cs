using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.DTOs.Workers
{
    public class AddWrokerDto
    {
        public Guid PositionId { get; set; }
        public Guid WorkerRoleId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ImgPath { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
