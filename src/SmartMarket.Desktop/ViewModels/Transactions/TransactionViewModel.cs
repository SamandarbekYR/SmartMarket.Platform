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
            Price = product.Price,
            Quantity = 1,
            TotalPrice = product.Price,
            AvailableCount = product.Count
        });
    }

    public void Increment(string barcode)
    {
        foreach (var m in Transactions)
        {
            if (m.Barcode == barcode)
            {
                m.Quantity++;
                m.TotalPrice = m.Quantity * m.Price;
            }
        }
    }
}
