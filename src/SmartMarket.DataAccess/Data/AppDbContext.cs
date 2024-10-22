using Microsoft.EntityFrameworkCore;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Customers;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {
            /*Database.EnsureCreated();
            Database.Migrate();*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ContrAgent>(entity =>
            {
                entity.HasOne(ca => ca.PartnerCompany)
                      .WithMany(pc => pc.ContrAgents)
                      .HasForeignKey(ca => ca.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ContrAgentPayment>(entity =>
            {
                entity.HasOne(cap => cap.ContrAgent)
                      .WithMany(ca => ca.ContrAgentPayment)
                      .HasForeignKey(cap => cap.ContrAgentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasOne(e => e.PayDesk)
                      .WithMany(pd => pd.Expenses)
                      .HasForeignKey(e => e.PayDeskId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Worker)
                      .WithMany(w => w.Expenses)
                      .HasForeignKey(e => e.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.Product)
                      .WithMany(p => p.Order)
                      .HasForeignKey(o => o.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Worker)
                      .WithMany(w => w.Orders)
                      .HasForeignKey(o => o.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Debtors>(entity =>
            {
                entity.HasOne(d => d.Partner)
                      .WithMany(p => p.Debtors)
                      .HasForeignKey(d => d.PartnerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Debtors)
                      .HasForeignKey(d => d.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DebtPayment>(entity =>
            {
                entity.HasOne(dp => dp.Debtor)
                      .WithMany(d => d.DebtPayment)
                      .HasForeignKey(dp => dp.DebtorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InvalidProduct>(entity =>
            {
                entity.HasOne(ip => ip.Worker)
                      .WithMany(w => w.InvalidProducts)
                      .HasForeignKey(ip => ip.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ip => ip.ProductSale)
                      .WithMany(ps => ps.InvalidProducts)
                      .HasForeignKey(ip => ip.ProductSaleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LoadReport>(entity =>
            {
                entity.HasOne(lr => lr.Worker)
                      .WithMany(w => w.LoadReports)
                      .HasForeignKey(lr => lr.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lr => lr.Product)
                      .WithMany(p => p.LoadReport)
                      .HasForeignKey(lr => lr.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lr => lr.ContrAgent)
                      .WithMany(ca => ca.LoadReports)
                      .HasForeignKey(lr => lr.ContrAgentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.ContrAgent)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.ContrAgentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Worker)
                      .WithMany(w => w.Products)
                      .HasForeignKey(p => p.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);


                //entity.HasIndex(p => p.Barcode)
                //    .IsUnique()
                //    .HasName("IX_Product_Barcode");

                //entity.HasIndex(p => p.PCode)
                //    .IsUnique()
                //    .HasName("IX_Product_PCode");
            });
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Barcode, p.PCode, p.Name })
                .IsUnique();

            modelBuilder.Entity<ContrAgent>()
                .HasIndex(c => new {c.PhoneNumber })
                .IsUnique();
            modelBuilder.Entity<ContrAgent>()
                .HasIndex(c => c.FirstName);

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasOne(pi => pi.Product)
                      .WithMany(p => p.ProductImages)
                      .HasForeignKey(p => p.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductSale>(entity =>
            {
                entity.HasOne(ps => ps.Product)
                      .WithMany(p => p.ProductSale)
                      .HasForeignKey(ps => ps.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ps => ps.SalesRequest)
                     .WithMany(p => p.ProductSaleItems)
                     .HasForeignKey(ps => ps.SalesRequesId)
                     .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<SalesRequest>()
                .HasIndex(c => new { c.TransactionId })
                .IsUnique();

            modelBuilder.Entity<SalesRequest>()
                .Property(e => e.TransactionId)
                .HasColumnName("transaction_id") 
                .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<ReplaceProduct>(entity =>
            {
                entity.HasOne(rp => rp.ProductSale)
                      .WithMany(ps => ps.ReplaceProducts)
                      .HasForeignKey(rp => rp.ProductSaleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(rp => rp.Worker)
                      .WithMany(w => w.ReplaceProducts)
                      .HasForeignKey(rp => rp.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalaryCheck>(entity =>
            {
                entity.HasOne(sc => sc.Worker)
                      .WithMany(w => w.SalaryChecks)
                      .HasForeignKey(sc => sc.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalaryWorker>(entity =>
            {
                entity.HasOne(sw => sw.Worker)
                      .WithMany(w => w.SalaryWorkers)
                      .HasForeignKey(sw => sw.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sw => sw.Salary)
                      .WithMany(s => s.SalaryWorkers)
                      .HasForeignKey(sw => sw.SalaryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasOne(w => w.Position)
                      .WithMany(p => p.Workers)
                      .HasForeignKey(w => w.PositionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(w => w.WorkerRole)
                      .WithMany(wr => wr.Workers)
                      .HasForeignKey(w => w.WorkerRoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkerDebt>(entity =>
            {
                entity.HasOne(wd => wd.Worker)
                      .WithMany(w => w.WorkerDebts)
                      .HasForeignKey(wd => wd.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Expense> Expenses { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Partner> Partners { get; set; }
        DbSet<PartnerCompany> PartnersCompany { get; set; }
        DbSet<ContrAgent> ContrAgent { get; set; }
        DbSet<ContrAgentPayment> ContrAgentPayment { get; set; }
        DbSet<PayDesk> PayDesks { get; set; }
        DbSet<Debtors> Debtors { get; set; }
        DbSet<DebtPayment> DebtPayment { get; set; }
        DbSet<InvalidProduct> InvalidProducts { get; set; }
        DbSet<LoadReport> LoadReports { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<ProductSale> ProductSale { get; set; }
        DbSet<SalesRequest> SalesRequests { get; set; }
        DbSet<ReplaceProduct> ReplaceProducts { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Salary> Salarys { get; set; }
        DbSet<SalaryCheck> SalaryChecks { get; set; }
        DbSet<SalaryWorker> SalaryWorkers { get; set; }
        DbSet<Worker> Workers { get; set; }
        DbSet<WorkerDebt> WorkerDebt { get; set; }
        DbSet<WorkerRole> WorkerRoles { get; set; }
    }
}