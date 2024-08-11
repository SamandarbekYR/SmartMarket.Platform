using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Workers
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

        public List<SalaryWorker> SalaryWorkers { get; set; }
        public List<SalaryCheck> SalaryChecks { get; set; }
        public List<WorkerDebt> WorkerDebts { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductSale> ProductSales { get; set; }
        public List<LoadReport> LoadReports { get; set; }
        public List<ReplaceProduct> ReplaceProducts { get; set; }
        public List<InvalidProduct> InvalidProducts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
