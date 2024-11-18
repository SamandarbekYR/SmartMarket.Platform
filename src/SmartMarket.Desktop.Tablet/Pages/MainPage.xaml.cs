using Microsoft.AspNetCore.SignalR.Client;
using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.ViewModels.Transactions;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarketDeskop.Integrated.Services.Orders;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Tablet.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for MainPage.xaml
/// </summary>
public partial class MainPage : Page
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
   /// private HubConnection _connection;
    public Partner Partner { get; set; }
    TransactionViewModel tvm;
    List<AddOrderProductDto> orderProducts;

    private DispatcherTimer time;

    string barcode = "";
    string barcodes = "";
    string message = "";

    public MainPage()
    {
        InitializeComponent();
        this._productService = new ProductService();
        this._orderService = new OrderService();
        this.tvm = new TransactionViewModel();
        this.orderProducts = new List<AddOrderProductDto>();

        time = new DispatcherTimer();
        time.Interval = TimeSpan.FromMilliseconds(50);
        time.Tick += Time_Tick!;
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 20,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });



    #region  Method

    private void Time_Tick(object sender, EventArgs e)
    {
        time.Stop();
        ProcessBarcode(barcode);
        barcode = "";
        barcodes = "";
    }

    private async void ProcessBarcode(string barcode)
    {
        if (!string.IsNullOrEmpty(barcode))
        {
            var product = await _productService.GetByBarCode(barcode);

            if (product != null)
            {
                AddNewProductTvm(product, 1);
            }
            else
            {
                notifier.ShowWarning("Bunday maxsulot topilmadi.");
            }
        }
    }

    public void AddNewProductTvm(FullProductDto product, int count)
    {
        if (count > 0)
        {
            string barcode = product.Barcode;
            if (!tvm.Transactions.Any(t => t.Barcode == barcode))
            {
                tvm.Add(product, count);
                AddNewProduct(product, count);
                product.Count = count;

                AddOrderProductDto addOrderProductDto = new AddOrderProductDto()
                {
                    ProductId = product.Id,
                    Count = count,
                    ItemTotalCost = product.SellPrice * count
                };

                orderProducts.Add(addOrderProductDto);
            }
            else
            {
                double totalPrice = 0;
                double discountPrice = 0;
                foreach (ProductComponent child in st_product.Children)
                {
                    if (child.Barcode == barcode)
                    {
                        int quantity = int.Parse(child.lb_Quantity.Content.ToString()!);
                        if (quantity < child.AvailableCount)
                        {
                            quantity++;
                            (totalPrice, discountPrice) = SetPrice(double.Parse(child.lb_Price.Content.ToString()!), float.Parse(child.lb_Discount.Content.ToString()!), quantity);

                            child.lb_Total.Content = totalPrice.ToString();
                            child.lb_Quantity.Content = quantity.ToString();

                            GetPrice(product, quantity);
                        }
                    }
                }
                tvm.Increment(barcode, totalPrice, discountPrice, count);
            }
            ColculateTotalPrice();
        }
    }

    private void AddNewProduct(FullProductDto product, int quantity)
    {
        ProductComponent productComponent = new ProductComponent();

        GetPrice(product, quantity);
        ColculateTotalPrice();

        productComponent.SetData(product, quantity);
        st_product.Children.Add(productComponent);
    }


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
        EmptyPrice();
        ColculateTotalPrice();
        selectedProduct = product;
    }

    private (double totalPrice, double discountprice) SetPrice(double price, float discount, int quantity)
    {
        double totalPrice = 0;
        double discountPrice = 0;

        if (discount > 0)
        {
            discountPrice = ((price / 100) * discount) * quantity;
            totalPrice = (quantity * price) - discountPrice;
        }
        else
        {
            totalPrice = quantity * price;
        }

        return (totalPrice, discountPrice);
    }

    private void ColculateTotalPrice()
    {
        if (tvm != null)
        {
            tvm.TransactionPrice = tvm.Transactions.Sum(x => x.TotalPrice);
            tvm.DiscountPrice = tvm.Transactions.Sum(x => x.DiscountSum);
            lb_TotalSum.Content = (tvm.TransactionPrice + tvm.DiscountPrice).ToString();
        }
        else
        {
            lb_TotalSum.Content = "0";
        }
    }

    private void EmptyPrice()
    {
        Product_Count.Text = "0";
        Product_Price.Content = "0";
        Total_Price.Content = "0";
        Product_Name.Content = "";
        Product_Barcode.Content = "";
    }

    private void GetPrice(FullProductDto product, int quantity)
    {
        Product_Count.Text = quantity.ToString();
        Product_Price.Content = product.SellPrice.ToString();
        Total_Price.Content = (quantity * product.SellPrice).ToString();
        Product_Name.Content = product.Name;
        Product_Barcode.Content = product.Barcode.ToString();
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



    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        st_product.Focus();

        //_connection = new HubConnectionBuilder()
        //        .WithUrl("https://localhost:7055/ShipmentsHub", options =>
        //        {
        //            options.HttpMessageHandlerFactory = _ => new System.Net.Http.HttpClientHandler
        //            {
        //                ServerCertificateCustomValidationCallback = System.Net.Http.HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //            };
        //        })
        //        .Build();

        //try
        //{
        //    await _connection.StartAsync();
        //    notifier.ShowSuccess("SignalR ulanish o'rnatildi.");
        //}
        //catch 
        //{
        //    notifier.ShowError("SignalR ulanishda xato: ");
        //}
            
    }

    private async void Send_Button_Click(object sender, RoutedEventArgs e)
    {
        AddOrderDto addOrderDto = new AddOrderDto()
        {
            PartnerId = Partner.Id,
            WorkerId = Guid.Parse("2b9fdca0-30ff-4f8e-9644-aee79943cf26"),
            ProductOrderItems = orderProducts,
        };

        if (addOrderDto != null)
        {
            try
            {
                await _orderService.CreateAsync(addOrderDto);
                //await _connection.InvokeAsync("SendShipMents", addOrderDto);

                notifier.ShowSuccess("Mahsulotlar muvaffaqiyatli yuborildi.");
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Mahsulotlarni yuborishda xato");
            }
        }
        else
        {
            notifier.ShowWarning("Yuborish uchun mahsulot tanlanmagan.");
        }
    }

    private void Sends_Button_Click(object sender, RoutedEventArgs e)
    {
        SecondPage secondPage = new SecondPage();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.PageNavigator.Content = secondPage;
    }

    private void Partners_Click(object sender, RoutedEventArgs e)
    {
        PartnersPage partnersPage = new PartnersPage();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.PageNavigator.Content = partnersPage;
    }

    private void Delete_Button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedProduct != null)
        {
            message = selectedProduct.lb_ProductName.Content.ToString()!;
            foreach (var item in tvm.Transactions.ToList())
            {
                if (item.Barcode == selectedProduct.Barcode)
                {
                    var messageBox = new MessageBoxWindow(message + "ni o'chirmoqchimisiz?", MessageType.Confirmation, MessageButtons.OkCancel);
                    var result = messageBox.ShowDialog();
                    if (result == true)
                    {
                        tvm.Transactions.Remove(item);
                        st_product.Children.Remove(selectedProduct);
                        ColculateTotalPrice();
                        selectedProduct.product_Border.Background = Brushes.White;
                        selectedProduct = null!;
                    }
                }
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void Plus_Button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedProduct != null)
        {
            if (int.Parse(selectedProduct.lb_Quantity.Content.ToString()!) < selectedProduct.AvailableCount)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedProduct.Barcode)
                    {
                        item.Quantity++;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedProduct.lb_Quantity.Content = item.Quantity.ToString();
                        selectedProduct.lb_Total.Content = item.TotalPrice.ToString();
                        ColculateTotalPrice();
                    }
                }
            }
            else
            {
                notifier.ShowInformation("Bu maxsulot tugadi.");
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void Minus_Button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedProduct != null)
        {
            int quantity = int.Parse(selectedProduct.lb_Quantity.Content.ToString()!);

            if (quantity > 1)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedProduct.Barcode)
                    {
                        item.Quantity--;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedProduct.lb_Quantity.Content = item.Quantity.ToString();
                        selectedProduct.lb_Total.Content = item.TotalPrice.ToString();
                        ColculateTotalPrice();
                    }
                }
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void Page_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        barcodes += e.Text;

        if (e.Text == "\r")
        {
            if (barcodes.Length >= 2)
            {
                barcode = barcodes.Substring(0, barcodes.Length - 1);
            }
        }

        time.Stop();
        time.Start();
    }

    private CancellationTokenSource _cancellationTokenSource;

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
