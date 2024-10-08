namespace SmartMarketDesktop.DTOs.DTOs.Product
{
    public class ProductSaleDto
    {
        public int Id { get; set; }

        public long TransactionNumber { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public string CategoryName { get; set; }
        public string WorkerName { get; set; }
        public string Discount { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
        public string Kasa { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
    }
}
