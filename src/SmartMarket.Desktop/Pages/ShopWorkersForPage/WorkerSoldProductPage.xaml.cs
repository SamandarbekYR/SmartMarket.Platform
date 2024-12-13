﻿using SmartMarket.Desktop.Components.ShopWorkerForComponent;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.ViewModels.Products;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace SmartMarket.Desktop.Pages.ShopWorkersForPage;

/// <summary>
/// Interaction logic for WorkerSoldProductPage.xaml
/// </summary>
public partial class WorkerSoldProductPage : Page
{
    public WorkerSoldProductPage()
    {
        InitializeComponent();
    }

    private WorkerListComponent selectedControl = null!;
    public void SelectWorkerProductSold(WorkerListComponent workerComponent, WorkerDto? worker)
    {
        if (selectedControl != null)
        {
            selectedControl.brWorker.Background = Brushes.White;
        }

        workerComponent.brWorker.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));

        ShowWorkerSoldProducts(worker);

        selectedControl = workerComponent;
    }

    public void SelectWorkerSaleProduct(SalesRequestDto dto)
    {
        Loader.Visibility = Visibility.Collapsed;
        St_WorkerSoldProducts.Children.Clear();

        if(dto != null)
        {
            var products = dto.ProductSaleItems;

            int count = 1;
            foreach (var item in products)
            {
                var productSaleViewModel = new ProductSaleViewModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Count = item.Count,
                    Discount = item.Discount ?? 0,
                    ItemTotalCost = item.ItemTotalCost,
                    CreatedDate = item.CreatedDate,
                    SalesRequestId = item.SalesRequestId,
                    SalesRequest = item.SalesRequest,
                    ReplaceProducts = item.ReplaceProducts,
                    InvalidProducts = item.InvalidProducts
                };

                WorkerSoldProductComponent workerSoldProductComponent = new WorkerSoldProductComponent();
                workerSoldProductComponent.Tag = productSaleViewModel;
                workerSoldProductComponent.SetValues(
                    count,
                    item.SalesRequest.TransactionId,
                    item.Product.Name,
                    item.Product.SellPrice,
                    item.Count,
                    item.ItemTotalCost);

                workerSoldProductComponent.BorderThickness = new Thickness(3, 2, 3, 2);
                St_WorkerSoldProducts.Children.Add(workerSoldProductComponent);
                count++;
            }
        }
        else
        {
            EmptyDataWorkerSoldProduct.Visibility = Visibility.Visible;
            EmptyDataWorkerSoldProduct.Content = "Ma'lumot topilmadi.";
        }
    }

    public void ShowWorkerSoldProducts(WorkerDto? worker)
    {
        Loader.Visibility = Visibility.Collapsed;
        var productSales = worker?.ProductSales.Where(p => p.Count != 0)
            .OrderByDescending(p => p.CreatedDate)
            .ToList();

        if(productSales.Any())
        {
            St_WorkerSoldProducts.Visibility = Visibility.Visible;
            St_WorkerSoldProducts.Children.Clear();

            int rowNumber = 1;
            foreach (var workerSoldProduct in productSales)
            {
                WorkerSoldProductComponent workerSoldProductComponent = new WorkerSoldProductComponent();
                workerSoldProductComponent.Tag = workerSoldProduct;
                workerSoldProductComponent.SetValues(
                    rowNumber,
                    workerSoldProduct.SalesRequest.TransactionId,
                    workerSoldProduct.Product.Name,
                    workerSoldProduct.Product.SellPrice,
                    workerSoldProduct.Count,
                    workerSoldProduct.ItemTotalCost);

                workerSoldProductComponent.BorderThickness = new Thickness(3, 2, 3, 2);
                St_WorkerSoldProducts.Children.Add(workerSoldProductComponent);
                rowNumber++;
            }
        }
        else
        {
            EmptyDataWorkerSoldProduct.Visibility = Visibility.Visible;
            EmptyDataWorkerSoldProduct.Content = "Ma'lumot topilmadi";
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {

    }
}
