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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for SecondPage.xaml
/// </summary>
public partial class SecondPage : Page
{

    private readonly IProductService _productService; 
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private OrderDto currentOrder { get; set; }
    public int i = 0;

    public SecondPage()
    {
        InitializeComponent();
        this._productService = new ProductService();
        this._orderService = new OrderService();
        this._orderItemService = new OrderItemService();
    }

    Notifier notifierThis = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });


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

    public void AddNewProduct(FullProductDto dto, int quantity)
    {
        if (currentOrder != null)
        {
            OrderProduct product = new OrderProduct()
            {
                ProductId = dto.Id,
                Count = quantity,
                AvailableCount = dto.Count,
                ItemTotalCost = dto.SellPrice * quantity,
            };
            currentOrder.ProductOrderItems.Add(product);
            AddNewProductComponent(dto, quantity);
        }
        else
            notifierThis.ShowInformation("Jo'natma tanlanmagan.");
    }

    public void AddNewProductComponent(FullProductDto dto, int quantity)
    {
        ProductComponent productComponent = new ProductComponent();
        productComponent.SetData(dto, quantity);
        st_product.Children.Add(productComponent);
        ColculatePrice();
    }

    private void ColculatePrice()
    {
        lbProductTotalPrice.Content = (currentOrder.ProductOrderItems.Sum(x => x.ItemTotalCost)).ToString();
    }

    #endregion

    private void Exit_Button_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = GetMainWindow();
        if(i == 1)
        {
            MainPage mainPage = new MainPage();
            mainWindow.PageNavigator.Content = mainPage;
        }
        else if(i == 2)
        {
            PartnersPage partnersPage = new PartnersPage();
            mainWindow.PageNavigator.Content = partnersPage;
        }
        
    }

    private void SetOrderProduct(List<OrderProduct> products)
    {
        double totalPrice = 0;
        productLoader.Visibility = Visibility.Collapsed;
        st_product.Children.Clear();
        if (products.Count > 0)
        {
            productEmptyData.Visibility = Visibility.Collapsed;

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
            lbPartnerName.Content = currentOrder.Partner?.FirstName ?? "";
            lbworkerName.Content = currentOrder.Worker?.FirstName ?? "";
        }
        else
        {
            lbProductTotalPrice.Content = 0;
            productEmptyData.Visibility = Visibility.Visible;
        }
    }

    public async Task GetAllShipments()
    {
        st_shipments.Children.Clear();
        shipmentLoader.Visibility = Visibility.Visible;
        var orders = await Task.Run(async () => await _orderService.GetAllAsync());

        await ShowShipments(orders);
    }

    private async Task ShowShipments(List<OrderDto> orders)
    {
        shipmentLoader.Visibility = Visibility.Collapsed;
        st_shipments.Children.Clear();

        if(orders.Count > 0)
        {
            ShipmentEmptyData.Visibility = Visibility.Collapsed;

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
            ShipmentEmptyData.Visibility = Visibility.Visible;
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllShipments();
    }

    private ShipmentComponent selectedControl = null!;
    public async void SelectOrder(ShipmentComponent shipmentComponent, OrderDto dto)
        {
        if(selectedControl != null)
        {
            selectedControl.CancelButton.Visibility = Visibility.Collapsed;
            selectedControl.btnEditShipment.Visibility = Visibility.Visible;    
        }

        selectedControl = shipmentComponent;
        currentOrder = dto;
        SetOrderProduct(dto.ProductOrderItems);
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

    private async void Delete_Button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedProduct?.Tag is OrderProduct orderProduct)
        {
            try
            {
                await _orderItemService.DeleteAsync(orderProduct.Id);

                currentOrder.ProductOrderItems.Remove(orderProduct);
                st_product.Children.Remove(selectedProduct);
                selectedProduct = null!;

                notifierThis.ShowSuccess("Mahsulot muvaffaqiyatli o'chirildi.");
                double totalPrice = (double)lbProductTotalPrice.Content - (orderProduct.Count * orderProduct.Product.SellPrice);
                lbProductTotalPrice.Content = totalPrice;
                await GetAllShipments();

                UpdateOrder();
            }
            catch(Exception ex)
            {
                notifierThis.ShowError("Mahsulotni o'chirishda xatolik yuz berdi.");
            }
        }
        else
        {
            notifierThis.ShowWarning("Mahsulot tanlanmagan.");
        }
    }

    private async void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var order = new UpdateOrderDto
            {
                WorkerId = currentOrder.WorkerId,
                PartnerId = currentOrder.PartnerId,
                ProductOrderItems = currentOrder.ProductOrderItems.Select(orderProduct => new UpdateOrderProductDto
                {
                    Id = orderProduct.Id,
                    ProductId = orderProduct.Product.Id,
                    Count = orderProduct.Count,
                    AvailableCount = orderProduct.AvailableCount,
                    ItemTotalCost = orderProduct.ItemTotalCost,
                }).ToList()
            };

            var res = await _orderService.UpdateAsync(currentOrder.Id, order);

            if (res is true)
            {
                notifierThis.ShowSuccess("Mahsulotlar muvaffaqiyatli yangilandi.");
                StopSale();
                await GetAllShipments();
            }
            else
            {
                notifierThis.ShowError("Mahsulotni yangilashda xatolik yuz berdi.");
            }
        }
        catch(Exception ex)
        {
            notifierThis.ShowError("Xatolik yuz berdi.");
        }
    }

    private void EmptyOrderDetails()
    {
        lbProductTotalPrice.Content = "0";
        lbPartnerName.Content = "";
        lbworkerName.Content = "";
    }

    public void StopSale()
    {
        selectedControl = null!;
        st_product.Children.Clear();
        st_searchproduct.Children.Clear();
        tb_search.Text = "";
        EmptyOrderDetails();
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
