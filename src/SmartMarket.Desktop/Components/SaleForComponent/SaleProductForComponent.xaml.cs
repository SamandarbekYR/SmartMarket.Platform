﻿using SmartMarket.Desktop.Pages.SaleForPage;
using SmartMarket.Service.DTOs.Products.Product;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.SaleForComponent;

/// <summary>
/// Interaction logic for SaleProductForComponent.xaml
/// </summary>
public partial class SaleProductForComponent : UserControl
{
    public Guid Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public int AvailableCount { get; set; }

    public SaleProductForComponent()
    {
        InitializeComponent();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var page = FindParentPage(this);
        if (page is SalePage salePage)
        {
            salePage.SelectProduct(this);
        }
    }

    private Page FindParentPage(DependencyObject child)
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject is Page page)
        {
            return page;
        }
        else if (parentObject != null)
        {
            return FindParentPage(parentObject);
        }
        return null!;
    }

    public void SetData(FullProductDto product)
    {
        Id = product.Id;
        Barcode = product.Barcode;
        AvailableCount = product.Count;
    }
}
