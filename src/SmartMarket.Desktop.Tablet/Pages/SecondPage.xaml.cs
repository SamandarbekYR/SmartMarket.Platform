using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Services.Orders;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for SecondPage.xaml
/// </summary>
public partial class SecondPage : Page
{

    private readonly IProductService _productService; 
    private readonly IOrderService _orderService;
    private OrderDto currentOrder { get; set; }

    public SecondPage()
    {
        InitializeComponent();
        this._productService = new ProductService();
        this._orderService = new OrderService();
    }


    #region Method

    public static MainWindow GetMainWindow()
    {
        MainWindow mainWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(MainWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                mainWindow = (MainWindow)window;
                if (mainWindow != null)
                {
                    break;
                }
            }
        }
        return mainWindow!;
    }

    private ProductComponent selectedProduct = null!;
    public void SelectProduct(ProductComponent product)
    {
        if (selectedProduct != null)
        {
            selectedProduct.product_Border.Background = Brushes.White;
        }

        product.product_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
        //EmptyPrice();
        //ColculateTotalPrice();
        selectedProduct = product;
    }

    private bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, @"^\d+$");
    }

    private void SetProduct(IList<FullProductDto> products)
    {
        Loader.Visibility = Visibility.Collapsed;
        st_searchproduct.Children.Clear();
        if (products.Count > 0)
        {
            foreach (var product in products)
            {
                SearchProductComponent searchProductComponent = new SearchProductComponent();
                searchProductComponent.Tag = product;
                searchProductComponent.SetData(product);
                st_searchproduct.Children.Add(searchProductComponent);
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    #endregion


    private void Exit_Button_Click(object sender, RoutedEventArgs e)
    {
        MainPage mainPage = new MainPage();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.PageNavigator.Content = mainPage;
    }

    private void SetOrderProduct(List<OrderProduct> products)
    {
        double totalPrice = 0;
        productLoader.Visibility = Visibility.Collapsed;
        st_product.Children.Clear();
        if (products.Count > 0)
        {
            foreach (var orderItem in products)
            {
                ProductComponent component = new ProductComponent();
                component.Tag = orderItem;
                component.SetValues(
                    orderItem.Id,
                    orderItem.Count,
                    orderItem.Product.Barcode,
                    orderItem.Product.Name,
                    orderItem.Product.SellPrice,
                    orderItem.Count);

                st_product.Children.Add(component);
                totalPrice += (orderItem.Product.SellPrice * orderItem.Count);
            }

            lbProductTotalPrice.Content = totalPrice;
        }
        else
        {
            lbProductTotalPrice.Content = 0;
            //empty data
        }
    }

    public async Task GetAllShipments()
    {
        var orders = await Task.Run(async () => await _orderService.GetAllAsync());

        await ShowShipments(orders);
    }

    private async Task ShowShipments(List<OrderDto> orders)
    {
        st_shipments.Children.Clear();

        if(orders.Count > 0)
        {
            foreach (var order in orders)
            {
                ShipmentComponent shipmentComponent = new ShipmentComponent();
                shipmentComponent.Tag = order;
                shipmentComponent.SetValues(order);
                st_shipments.Children.Add(shipmentComponent);
            }
        }
        else
        {
            //empty data visible
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllShipments();
        //for (int i = 0; i < 20; i++)
        //{
        //    //ShipmentComponent shipmentComponent = new ShipmentComponent();
        //    ProductComponent productComponent = new ProductComponent();

        //    st_product.Children.Add(productComponent);
        //    //st_shipments.Children.Add(shipmentComponent);
        //}
    }

    private ShipmentComponent selectedControl = null!;
    public async void SelectOrder(ShipmentComponent shipmentComponent, Guid orderId)
    {
        if(selectedControl != null)
        {
            selectedControl.brOrder.Background = Brushes.White;
        }

        if(shipmentComponent.Tag is OrderDto selectedOrder)
        {
            currentOrder = new OrderDto
            {
                Id = selectedOrder.Id,
                WorkerId = selectedOrder.WorkerId,
                PartnerId = selectedOrder.PartnerId,
                ProductOrderItems = selectedOrder.ProductOrderItems
            };

            SetOrderProduct(currentOrder.ProductOrderItems);
        }
    }

    private void UpdateOrder()
    {
        SetOrderProduct(currentOrder.ProductOrderItems);
    }

    private void Minus_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedProduct?.Tag is OrderProduct orderProduct && orderProduct.Count > 1)
        {
            orderProduct.Count -= 1;
            UpdateOrder();
        }
    }

    private void Plus_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedProduct?.Tag is OrderProduct orderProduct)
        {
            orderProduct.Count += 1;
            UpdateOrder();
        }
    }

    private void Delete_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedProduct?.Tag is OrderProduct orderProduct)
        {
            currentOrder.ProductOrderItems.Remove(orderProduct);
            UpdateOrder();
        }
    }

    private async void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var order = new AddOrderDto
            {
                WorkerId = currentOrder.WorkerId,
                PartnerId = currentOrder.PartnerId,
                ProductOrderItems = currentOrder.ProductOrderItems.Select(orderProduct => new AddOrderProductDto
                {
                    ProductId = orderProduct.Product.Id,
                    Count = orderProduct.Count,
                    AvailableCount = orderProduct.AvailableCount,
                    ItemTotalCost = orderProduct.ItemTotalCost
                }).ToList()
            };

            var res = await _orderService.UpdateAsync(currentOrder.Id, order);

            if (res is true)
                MessageBox.Show("Muvaffaqiyatli o'zgartirildi.");
            else
                MessageBox.Show("Xatolik yuz berdi.");
        }
        catch(Exception ex)
        {
            MessageBox.Show("Xatolik yuz berdi ...");
        }
    }

    private CancellationTokenSource _cancellationTokenSource = null!;

    private async void tb_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        string search = tb_search.Text;

        st_searchproduct.Children.Clear();
        EmptyData.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            Loader.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(async () =>
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested) return;

                    IList<FullProductDto> products = new List<FullProductDto>();

                    if (IsNumeric(search) && search.Length >= 5)
                    {
                        products = await _productService.GetByPCode(search);
                    }
                    else if (!IsNumeric(search) && search.Length >= 1)
                    {
                        products = await _productService.GetByProductName(search);
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (search == tb_search.Text)
                        {
                            SetProduct(products);
                        }
                    });
                },
                _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                Loader.Visibility = Visibility.Collapsed;
            }
        }
        else
        {
            st_searchproduct.Children.Clear();
        }
    }
}
