using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using System.Collections.ObjectModel;

namespace SmartMarket.Desktop.Tablet.ViewModels.Transactions;

public class TransactionViewModel
{
    public ObservableCollection<TransactionDto> Transactions = new ObservableCollection<TransactionDto>();

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

    public void Increment(string barcode, double price, double discountSum, int quantity)
    {
        foreach (var m in Transactions)
        {
            if (m.Barcode == barcode)
            {
                m.Quantity += quantity;
                m.TotalPrice = price;
                m.DiscountSum = discountSum;
            }
        }
    }
}
