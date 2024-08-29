using SmartMarketDesktop.ViewModels.Entities.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Orders;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("worker")]
    public class WorkerView : BaseEntity
    {
        [Column("position_id")]
        public Guid PositionId { get; set; }
        public PositionView PositionView { get; set; }
        [Column("worker_roleid")]
        public Guid WorkerRoleId { get; set; }
        public WorkerRoleView WorkerRoleView { get; set; }

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
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<SalaryWorkerView> SalaryWorkerViews { get; set; }
        public List<SalaryCheckView> SalaryCheckViews { get; set; }
        public List<WorkerDebtView> WorkerDebtViews { get; set; }
        public List<ProductView> ProductViews { get; set; }
        public List<ProductSaleView> ProductSaleViews { get; set; }
        public List<LoadReportView> LoadReportViews { get; set; }
        public List<ReplaceProductView> ReplaceProductViews { get; set; }
        public List<InValidProductView> InValidProductViews { get; set; }
        public List<OrderView> OrderViews { get; set; }
        public List<ExpenseView> ExpenseViews { get; set; }
    }
}
