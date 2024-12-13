﻿using SmartMarketDesktop.ViewModels.Entities.PayDesk;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarketDesktop.ViewModels.Entities.Expenses
{
    [Table("expense")]
    public class ExpenseView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskViewId { get; set; }
        public PayDeskView PayDeskView { get; set; }
        [Column("reason")]
        public string Reason { get; set; } = string.Empty;
        [Column("amount")]
        public double Amount { get; set; }
        [Column("type_of_payment")]
        public string TypeOfPayment { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }
        [Column("pay_desk_name")]
        public string PayDeskName { get; set; } = string.Empty;
        [Column("worker_firstname")]
        public string WorkerFirstname { get; set; } = string.Empty;
        [Column("worker_lastname")]
        public string WorkerLastname { get; set; } = string.Empty;
    }
}
