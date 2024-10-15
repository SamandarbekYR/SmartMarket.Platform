using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using System.Collections.ObjectModel;

namespace SmartMarket.Desktop.ViewModels.Transactions;

public class TransactionViewModel
{
    public ObservableCollection<TransactionDto> Transactions = new ObservableCollection<TransactionDto>();

    internal double TransactionPrice { get; set; } = 0;
    internal double DiscountPrice { get; set; } = 0;

    public TransactionViewModel()
    {
    }

    public void Add(ProductDto product)
    {
        Transactions.Add(new TransactionDto()
        {
            Id = product.Id,
            Barcode = product.Barcode,
            Name = product.Name,
            Price = product.SellPrice,
            Quantity = 1,
            TotalPrice = product.SellPrice,
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
}
