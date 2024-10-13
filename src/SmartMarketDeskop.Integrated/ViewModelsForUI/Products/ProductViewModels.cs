namespace SmartMarketDeskop.Integrated.ViewModelsForUI.Products;

public class ProductViewModels
{
    public Guid Id { get; set; }
    public string BarCode { get; set; }=string.Empty;
    public string P_Code { get; set; }=string.Empty ;
    public string ProductName { get; set; } = string.Empty;
    public string CateogoryName { get; set; } = string.Empty;
    public string WorkerName { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Count { get; set; }
    public double TotalPrice { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public double SellPrice { get; set; }
}
