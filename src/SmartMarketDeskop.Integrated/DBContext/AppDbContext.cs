using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using SmartMarketDesktop.ViewModels.Entities.Customers;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Orders;
using SmartMarketDesktop.ViewModels.Entities.Partners;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using SmartMarketDesktop.ViewModels.Entities.PayDesk;
using SmartMarketDesktop.ViewModels.Entities.Products;
using SmartMarketDesktop.ViewModels.Entities.Transactions;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.DBContext
{
    public class AppDbContext : DbContext
    {
       private readonly string connectionString = "Host=localhost;Database=SmartPartners_Desktop;User ID=postgres;Port=5432;Password=1234;";

        // Constructor for DI (Dependency Injection) scenarios
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {
           
        }

        // Constructor for manual configuration
       

        // Override OnConfiguring to use the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string is not provided.");
                }
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContrAgentView>(entity =>
            {
                entity.HasOne(ca => ca.PartnerCompanyView)
                      .WithMany(pc => pc.ContrAgentViews)
                      .HasForeignKey(ca => ca.CompanyViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ContrAgentPaymentView>(entity =>
            {
                entity.HasOne(cap => cap.ContrAgentView)
                      .WithMany(ca => ca.ContrAgentPaymentViews)
                      .HasForeignKey(cap => cap.ContrAgentViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ExpenseView>(entity =>
            {
                entity.HasOne(e => e.PayDeskView)
                      .WithMany(pd => pd.ExpenseViews)
                      .HasForeignKey(e => e.PayDeskViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.WorkerView)
                      .WithMany(w => w.ExpenseViews)
                      .HasForeignKey(e => e.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderView>(entity =>
            {
                entity.HasOne(o => o.ProductView)
                      .WithMany(p => p.OrderViews)
                      .HasForeignKey(o => o.ProductViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.WorkerView)
                      .WithMany(w => w.OrderViews)
                      .HasForeignKey(o => o.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DebttorsView>(entity =>
            {
                entity.HasOne(d => d.PartnerView)
                      .WithMany(p => p.DebttorsViews)
                      .HasForeignKey(d => d.PartnerViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ProductView)
                      .WithMany(p => p.DebttorsViews)
                      .HasForeignKey(d => d.ProductViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DebtPaymentView>(entity =>
            {
                entity.HasOne(dp => dp.DebttorsView)
                      .WithMany(d => d.DebtPaymentViews)
                      .HasForeignKey(dp => dp.DebtorViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InValidProductView>(entity =>
            {
                entity.HasOne(ip => ip.WorkerView)
                      .WithMany(w => w.InValidProductViews)
                      .HasForeignKey(ip => ip.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ip => ip.ProductSaleView)
                      .WithMany(ps => ps.InValidProductViews)
                      .HasForeignKey(ip => ip.ProductSaleViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LoadReportView>(entity =>
            {
                entity.HasOne(lr => lr.WorkerView)
                      .WithMany(w => w.LoadReportViews)
                      .HasForeignKey(lr => lr.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lr => lr.ProductView)
                      .WithMany(p => p.LoadReportViews)
                      .HasForeignKey(lr => lr.ProductViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lr => lr.ContrAgentView)
                      .WithMany(ca => ca.LoadReportViews)
                      .HasForeignKey(lr => lr.ContrAgentViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductView>(entity =>
            {
                entity.HasOne(p => p.CategoryView)
                      .WithMany(c => c.ProductViews)
                      .HasForeignKey(p => p.CategoryViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.ContrAgentView)
                      .WithMany(c => c.ProductViews)
                      .HasForeignKey(p => p.ContrAgenViewtId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.WorkerView)
                      .WithMany(w => w.ProductViews)
                      .HasForeignKey(p => p.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductImageView>(entity =>
            {
                entity.HasOne(pi => pi.ProductView)
                      .WithMany(p => p.ProductImageViews)
                      .HasForeignKey(p => p.ProductViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductSaleView>(entity =>
            {
                entity.HasOne(ps => ps.ProductView)
                      .WithMany(p => p.ProductSaleViews)
                      .HasForeignKey(ps => ps.ProductViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ps => ps.PayDeskView)
                     .WithMany(pd => pd.ProductSaleViews)
                     .HasForeignKey(ps => ps.PayDeskViewId)
                     .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ps => ps.WorkerView)
                      .WithMany(w => w.ProductSaleViews)
                      .HasForeignKey(ps => ps.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ps => ps.TransactionView)
                      .WithMany(t => t.ProductSaleViews)
                      .HasForeignKey(ps => ps.TransactionViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReplaceProductView>(entity =>
            {
                entity.HasOne(rp => rp.ProductSaleView)
                      .WithMany(ps => ps.ReplaceProductViews)
                      .HasForeignKey(rp => rp.ProductSaleViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(rp => rp.WorkerView)
                      .WithMany(w => w.ReplaceProductViews)
                      .HasForeignKey(rp => rp.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalaryCheckView>(entity =>
            {
                entity.HasOne(sc => sc.WorkerView)
                      .WithMany(w => w.SalaryCheckViews)
                      .HasForeignKey(sc => sc.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SalaryWorkerView>(entity =>
            {
                entity.HasOne(sw => sw.WorkerView)
                      .WithMany(w => w.SalaryWorkerViews)
                      .HasForeignKey(sw => sw.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sw => sw.SalaryView)
                      .WithMany(s => s.SalaryWorkerViews)
                      .HasForeignKey(sw => sw.SalaryViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WorkerView>(entity =>
            {
                entity.HasOne(w => w.PositionView)
                      .WithMany(p => p.WorkerViews)
                      .HasForeignKey(w => w.PositionViewId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(w => w.WorkerRoleView)
                      .WithMany(wr => wr.WorkerViews)
                      .HasForeignKey(w => w.WorkerRoleViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WorkerDebtView>(entity =>
            {
                entity.HasOne(wd => wd.WorkerView)
                      .WithMany(w => w.WorkerDebtViews)
                      .HasForeignKey(wd => wd.WorkerViewId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        DbSet<CategoryView> CategoryViews { get; set; }
        DbSet<CustomerView> CustomerViews { get; set; }
        DbSet<ExpenseView> ExpenseViews { get; set; }
        DbSet<OrderView> OrderViews { get; set; }
        DbSet<PartnerView> PartnerViews { get; set; }
        DbSet<PartnerCompanyView> PartnerCompanyViews { get; set; }
        DbSet<ContrAgentView> ContrAgentViews { get; set; }
        DbSet<ContrAgentPaymentView> ContrAgentPaymentViews { get; set; }
        DbSet<PayDeskView> PayDeskViews { get; set; }
        DbSet<DebttorsView> DebttorsViews { get; set; }
        DbSet<DebtPaymentView> DebtPaymentViews { get; set; }
        DbSet<InValidProductView> InValidProductViews { get; set; }
        DbSet<LoadReportView> LoadReportViews { get; set; }
        DbSet<ProductView> ProductViews { get; set; }
        DbSet<ProductImageView> ProductImageViews { get; set; }
        DbSet<ProductSaleView> ProductSaleViews { get; set; }
        DbSet<ReplaceProductView> ReplaceProductViews { get; set; }
        DbSet<TransactionView> TransactionViews { get; set; }
        DbSet<PositionView> PositionViews { get; set; }
        DbSet<SalaryView> SalaryViews { get; set; }
        DbSet<SalaryCheckView> SalaryCheckViews { get; set; }
        DbSet<SalaryWorkerView> SalaryWorkerViews { get; set; }
        DbSet<WorkerView> WorkerViews { get; set; }
        DbSet<WorkerDebtView> WorkerDebtViews { get; set; }
        DbSet<WorkerRoleView> WorkerRoleViews { get; set; }
    }
}
