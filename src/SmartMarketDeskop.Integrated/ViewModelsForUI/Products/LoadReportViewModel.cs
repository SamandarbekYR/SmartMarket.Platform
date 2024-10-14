using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarketDeskop.Integrated.ViewModelsForUI.Products
{
    public class LoadReportViewModel
    {
        public string PCode { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string WorkerName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double SellPrice { get; set; }
        public int Count { get; set; }
        public string UnitOfMeasure { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string ManuFacture { get; set; } = string.Empty;
    }
}
