using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("worker")]
    public class Worker : BaseEntity
    {
        [Column("position_id")]
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

        [Column("worker_roleid")]
        public Guid WorkerRoleId { get; set; }
        public WorkerRole Role { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("img_path")]
        public string ImgPath { get; set; } = string.Empty;
        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("password_salt")]
        public string PasswordSalt { get; set; } = string.Empty;

        public List<SalaryWorker> SalaryWorkers { get; set;} 
    }
}
