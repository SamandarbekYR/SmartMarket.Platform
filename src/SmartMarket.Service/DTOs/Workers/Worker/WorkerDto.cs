using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.Products;
using O = SmartMarket.Domain.Entities.Orders;
using W = SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.DTOs.Workers.Worker;

public class WorkerDto
{
    public Guid Id { get; set; }
    public Guid PositionId { get; set; }
    public Guid WorkerRoleId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public W.Position Position { get; set; }
    public W.WorkerRole WorkerRole { get; set; }
    public List<W.SalaryWorker> SalaryWorkers { get; set; }
    public List<W.SalaryCheck> SalaryChecks { get; set; }
    public List<W.WorkerDebt> WorkerDebts { get; set; }
    public List<Product> Products { get; set; }
    public List<ProductSale> ProductSales { get; set; }
    public List<LoadReport> LoadReports { get; set; }
    public List<ReplaceProduct> ReplaceProducts { get; set; }
    public List<InvalidProduct> InvalidProducts { get; set; }
    public List<O.Order> Orders { get; set; }
    public List<Expense> Expenses { get; set; }
}