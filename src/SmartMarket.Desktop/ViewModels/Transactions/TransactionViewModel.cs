using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDesktop.DTOs.DTOs.Transactions;

namespace SmartMarket.Desktop.ViewModels.Transactions;

public class TransactionViewModel
{
    public List<TransactionDto> Transactions = new List<TransactionDto>();

    internal double TransactionPrice { get; set; } = 0;
    internal double DiscountPrice { get; set; } = 0;

    public TransactionViewModel()
    {
    }

    public void Add(FullProductDto product, int quantity)
    {
        Transactions.Add(new TransactionDto()
        {
            Id = product.Id,
            Barcode = product.Barcode,
            Name = product.Name,
            Price = product.SellPrice,
            Quantity = quantity,
            TotalPrice = product.SellPrice * quantity,
            AvailableCount = product.Count,
            Discount = 0
        });
    }

    public void Increment(string barcode, double price, double discountSum)
    {
        foreach (var m in Transactions)
        {
            if (m.Barcode == barcode)
            {
                m.Quantity++;
                m.TotalPrice = price;
                m.DiscountSum = discountSum;
            }
        }
    }

    public void ClearTransaction()
    {
        Transactions.Clear();
        TransactionPrice = 0;
        DiscountPrice = 0;
    }
}
