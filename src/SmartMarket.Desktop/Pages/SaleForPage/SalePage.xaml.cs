using Microsoft.AspNetCore.SignalR.Client;
using SmartMarket.Desktop.Components.SaleForComponent;
using SmartMarket.Desktop.ViewModels.Transactions;
using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.Auth;
using SmartMarket.Desktop.Windows.Expenses;
using SmartMarket.Desktop.Windows.Partners;
using SmartMarket.Desktop.Windows.PaymentWindow;
using SmartMarket.Desktop.Windows.ProductsForWindow;
using SmartMarket.Desktop.Windows.Sales;
using SmartMarket.Desktop.Windows.Settings;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Services.Orders;
using SmartMarketDeskop.Integrated.Services.Partners;
using SmartMarketDeskop.Integrated.Services.Products.Print;
using SmartMarketDeskop.Integrated.Services.Products.Product;
using SmartMarketDeskop.Integrated.Services.Products.SalesRequests;
using SmartMarketDesktop.DTOs.DTOs.Transactions;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Pages.SaleForPage;

/// <summary>
/// Interaction logic for SalePage.xaml
/// </summary>
public partial class SalePage : Page
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly ISalesRequestsService _salesRequestsService;
    private readonly IPartnerService _partnerService;
    private HubConnection _connection;

    private System.Timers.Timer timer = new System.Timers.Timer();
    private DispatcherTimer time;
    public TransactionViewModel tvm;

    int activeTextboxIndex = 2;
    string message = "";
    string barcode = "";
    string barcodes = "";

    public OrderDto Order { get; set; } = null!;
    public bool IsShipment { get; set; } = false;
    public string PaymentType { get; set; } = string.Empty;
    public double TotalPrice { get; set; }
    public double CashSum { get; set; }
    public double ClickSum { get; set; }

    public SalePage()
    {
        InitializeComponent();
        this.tvm = new TransactionViewModel();
        this._orderService = new OrderService();
        this._productService = new ProductService();
        this._salesRequestsService = new SalesRequestService();
        this._partnerService = new PartnerService();

        timer.Elapsed += vaqt_ketdi!;
        timer.Interval = 500;
        timer.Enabled = true;

        time = new DispatcherTimer();
        time.Interval = TimeSpan.FromMilliseconds(50);
        time.Tick += Timer_Tick!;
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.BottomCenter,
            offsetX: 40,
            offsetY: 40);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    private void Timer_Tick(object sender, EventArgs e)
    {
        time.Stop();
        ProcessBarcode(barcode);
        barcode = "";
        barcodes = "";
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

        var button = (Button)sender;
        WriteNumber(button.Content.ToString() ?? "");

    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        tbCalculator.Text = string.Empty;
    }

    private void btnBackKlav_Click(object sender, RoutedEventArgs e)
    {
        if (activeTextboxIndex == 2 && tbCalculator.Text.Length > 0)
        {
            tbCalculator.Text = tbCalculator.Text.Substring(0, tbCalculator.Text.Length - 1);
        }
        else if (activeTextboxIndex == 3)
        {
            if (selectedControl.tbDiscount.Text.Length == 1)
                selectedControl.tbDiscount.Text = "0";
            else if (selectedControl.tbDiscount.Text.Length > 1)
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text.Substring(0, selectedControl.tbDiscount.Text.Length - 1);
        }
    }

    private void btnReturnProduct_Click(object sender, RoutedEventArgs e)
    {
        ReturnProductWindow returnProductWindow = new ReturnProductWindow();
        returnProductWindow.ShowDialog();
    }

    public void GetData()
    {
        var payDeskId = Properties.Settings.Default.PayDesk;
        if (string.IsNullOrEmpty(payDeskId))
        {
            SelectPayDeskWindow selectPayDeskWindow = new SelectPayDeskWindow();
            selectPayDeskWindow.ShowDialog();
        }
        else
        {
            IdentitySingelton.GetInstance().PayDeskId = Guid.Parse(payDeskId.ToString()!);
            IdentitySingelton.GetInstance().PayDeskName = Properties.Settings.Default.PayDeskName;
        }
        tbFullName.Text = IdentitySingelton.GetInstance().FirstName + " " + IdentitySingelton.GetInstance().LastName;
        tbKassaName.Text = IdentitySingelton.GetInstance().PayDeskName;
        IdentitySingelton.GetInstance().PrinterName = Properties.Settings.Default.PrinterName;

        tbDate.Text = DateTime.UtcNow.Month + "." + DateTime.UtcNow.Day + "." + DateTime.UtcNow.Year;
        tbhour.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }

    private void vaqt_ketdi(object sender, ElapsedEventArgs e)
    {
        this.Dispatcher.Invoke(() =>
        {
            if (tbhour.Opacity == 1)
            {
                tbhour.Opacity = 1;
            }
            else
                tbhour.Opacity = 0.5;
            GetData();
        });
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        activeTextboxIndex = int.Parse(((TextBox)sender).Uid);
    }

    private void WriteNumber(string number)
    {
        if (activeTextboxIndex == 2)
        {
            tbCalculator.Text = tbCalculator.Text.ToString() + number;
        }
        else if (activeTextboxIndex == 3)
        {
            if (selectedControl.tbDiscount.Text == "0")
            {
                selectedControl.tbDiscount.Text = "";
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text + number;
            }
            else
                selectedControl.tbDiscount.Text = selectedControl.tbDiscount.Text + number;
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

    private async void ProcessBarcode(string barcode)
    {
        if (!string.IsNullOrEmpty(barcode))
        {
            var product = await Task.Run( async () => await _productService.GetByBarCode(barcode));

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (product != null)
                {
                    AddNewProductTvm(product, 0);
                }
                else 
                {
                    notifier.ShowWarning("Bunday maxsulot topilmadi.");
                }
            });

        }
    }

    public void AddNewProductTvm(FullProductDto product, int count)
    {
        if (product.Count > 0)
        {
            string barcode = product.Barcode;
            if (!tvm.Transactions.Any(t => t.Barcode == barcode))
            {
                if (count == 0)
                    tvm.Add(product, 1);
                else
                    tvm.Add(product, count);
                AddNewProduct(product, count);
            }
            else
            {
                double totalPrice = 0;
                double discountPrice = 0;
                foreach (SaleProductForComponent child in St_product.Children)
                {
                    if (child.Barcode == barcode)
                    {
                        int quantity = int.Parse(child.tbQuantity.Text);
                        if (quantity < child.AvailableCount)
                        {
                            quantity++;
                            (totalPrice, discountPrice) = SetPrice(double.Parse(child.tbPrice.Text), float.Parse(child.tbDiscount.Text), quantity);

                            child.tbTotalPrice.Text = totalPrice.ToString();
                            child.tbQuantity.Text = quantity.ToString();

                            GetPrice(product, quantity);
                        }
                    }
                }
                tvm.Increment(barcode, totalPrice, discountPrice);
            }
            ColculateTotalPrice();
        }
        else
        {
            notifier.ShowInformation("Bu maxsulot tugagan.");
        }
    }

    private void AddNewProduct(FullProductDto product, int quantity)
    {
        SaleProductForComponent saleProductForComponent = new SaleProductForComponent();
        if (quantity == 0)
        {
            if (string.IsNullOrEmpty(tbCalculator.Text))
                quantity = 1;
            else
                quantity = int.Parse(tbCalculator.Text!);
        }

        saleProductForComponent.DataContext = new TransactionDto
        {
            Name = product.Name,
            Barcode = product.Barcode,
            Price = product.SellPrice,
            TotalPrice = product.SellPrice * quantity,
            AvailableCount = product.Count,
            Discount = 0,
            Quantity = quantity,
        };

        GetPrice(product, quantity);
        ColculateTotalPrice();

        saleProductForComponent.SetData(product);
        St_product.Children.Add(saleProductForComponent);
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllOrders();
        await InitializeSignalRConnection();

        GetData();
        St_product.Focus();

        foreach (var scrollViewer in FindVisualChildren<ScrollViewer>(this))
        {
            scrollViewer.ManipulationBoundaryFeedback += (s, args) =>
            {
                args.Handled = true;
            };
        }
    }

    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T t)
            {
                yield return t;
            }
            foreach (var childOfChild in FindVisualChildren<T>(child))
            {
                yield return childOfChild;
            }
        }
    }

    private async Task InitializeSignalRConnection()
    {
        _connection = new HubConnectionBuilder()
               .WithUrl("https://localhost:7055/ShipmentsHub", options =>
               {
                   options.HttpMessageHandlerFactory = _ => new System.Net.Http.HttpClientHandler
                   {
                       ServerCertificateCustomValidationCallback = System.Net.Http.HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                   };
               })
               .Build();

        _connection.On<string>("ReceiveShipMents", (message) =>
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                await GetAllOrders();
            });
        });

        try
        {
            await _connection.StartAsync();
        }
        catch
        {
            notifier.ShowWarning("Ulanishda xotolik mavjud!");
        }
    }

    public async Task GetAllOrders()
    {
        Loader.Visibility = Visibility.Visible;
        var orders = await Task.Run(async () => await _orderService.GetAllAsync());
        if (orders.Count > 0)
        {
            DisplayOrdersInStackPanel(orders);
        }
        else
        {
            Loader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private CancellationTokenSource _cancellationTokenSource;
    private async void Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        string search = Search.Text;

        try
        {
            await Task.Delay(500, token);
        }
        catch (TaskCanceledException)
        {
            return;
        }

        if (token.IsCancellationRequested)
        {
            return;
        }

        stackPanelOrders.Children.Clear();
        EmptyData.Visibility = Visibility.Collapsed;

        if (!string.IsNullOrWhiteSpace(search))
        {
            Loader.Visibility = Visibility.Visible;
            try
            {
                await Task.Run(async () =>
                {
                    if (search.Length >= 1)
                    {
                        var orders = await _orderService.GetByPartnerNameAsync(search);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (orders.Count > 0)
                            {
                                DisplayOrdersInStackPanel(orders);
                            }
                            else
                            {
                                EmptyData.Visibility = Visibility.Visible;
                            }
                        });
                    }
                }, token);
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
            stackPanelOrders.Children.Clear();
            EmptyData.Visibility = Visibility.Collapsed;
            await GetAllOrders();
        }

    }

    private void DisplayOrdersInStackPanel(List<OrderDto> orders)
    {
        stackPanelOrders.Children.Clear();
        Loader.Visibility = Visibility.Collapsed;

        foreach (var order in orders)
        {
            ShipmentComponent shipmentComponent = new ShipmentComponent();
            shipmentComponent.SetData(order);
            shipmentComponent.Tag = order;
            stackPanelOrders.Children.Add(shipmentComponent);
        }
    }

    private bool IsNumeric(string text)
    {
        return Regex.IsMatch(text, @"^\d+$");
    }

    private void Harajat_Click(object sender, RoutedEventArgs e)
    {
        ExpensesWindow expensesWindow = new ExpensesWindow();
        expensesWindow.ShowDialog();
    }

    private void Hamkorlar_Click(object sender, RoutedEventArgs e)
    {
        PartnersWindow partnersWindow = new PartnersWindow();
        partnersWindow.ShowDialog();
    }

    private void Sozlamalar_Click(object sender, RoutedEventArgs e)
    {
        SettingPrinterWindow settingPrinterWindow = new SettingPrinterWindow();
        settingPrinterWindow.ShowDialog();
    }

    private void Sotuv_Tarixi_Click(object sender, RoutedEventArgs e)
    {
        SaleHistoryWindow saleHistoryWindow = new SaleHistoryWindow();
        saleHistoryWindow.ShowDialog();
    }

    private void Log_Out_Click(object sender, RoutedEventArgs e)
    {
        IdentitySingelton.GetInstance().Reset();

        LoginWindow login = new LoginWindow();
        login.Show();

        Window currentWindow = Window.GetWindow(this);
        if (currentWindow != null)
            currentWindow.Close();
    }

    // Jami summani hisoblash uchun
    public void ColculateTotalPrice()
    {
        if (tvm != null)
        {
            TotalPrice = tvm.TransactionPrice = tvm.Transactions.Sum(x => x.TotalPrice);
            tvm.DiscountPrice = tvm.Transactions.Sum(x => x.DiscountSum);
            tbTotalAmount.Text = tvm.TransactionPrice.ToString();
            tbDiscountAmount.Text = tvm.DiscountPrice.ToString();
            tbAmount.Text = (tvm.TransactionPrice + tvm.DiscountPrice).ToString();
        }
        else
        {
            tbAmount.Text = "0";
            tbDiscountAmount.Text = "0";
            tbTotalAmount.Text = "0";
        }
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

    private void GetPrice(FullProductDto product, int quantity)
    {
        Product_Count.Text = quantity.ToString();
        Product_Price.Text = product.SellPrice.ToString();
        Total_Price.Text = (quantity * product.SellPrice).ToString();
        Product_Name.Text = product.Name;
        Product_Barcode.Text = product.Barcode.ToString();
    }

    public void EmptyPrice()
    {
        Product_Count.Text = "0";
        Product_Price.Text = "0";
        Total_Price.Text = "0";
        Product_Name.Text = "";
        Product_Barcode.Text = "";
    }

    public void StopSale()
    {
        tvm.ClearTransaction();
        selectedControl = null!;
        St_product.Children.Clear();
        EmptyPrice();
        ColculateTotalPrice();
    }

    private SaleProductForComponent selectedControl = null!;
    public void SelectProduct(SaleProductForComponent product)
    {
        if (selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
        }

        product.product_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
        EmptyPrice();
        ColculateTotalPrice();
        selectedControl = product;
    }

    private void delete_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            message = selectedControl.tbProductName.Text;
            foreach (var item in tvm.Transactions.ToList())
            {
                if (item.Barcode == selectedControl.Barcode)
                {
                    var messageBox = new MessageBoxWindow(message + "ni o'chirmoqchimisiz?", MessageType.Confirmation, MessageButtons.OkCancel);
                    var result = messageBox.ShowDialog();
                    if (result == true)
                    {
                        tvm.Transactions.Remove(item);
                        St_product.Children.Remove(selectedControl);
                        ColculateTotalPrice();
                        selectedControl.product_Border.Background = Brushes.White;
                        selectedControl = null!;
                        break;
                    }
                }
            }
        }
        else
        {
            notifier.ShowInformation("Maxsulot tanlanmagan.");
        }
    }

    private void plus_button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedControl != null)
        {
            if (int.Parse(selectedControl.tbQuantity.Text) < selectedControl.AvailableCount)
            {
                foreach (var item in tvm.Transactions)
                {
                    if(item.Barcode == selectedControl.Barcode)
                    {
                        item.Quantity++;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedControl.tbQuantity.Text = item.Quantity.ToString();
                        selectedControl.tbTotalPrice.Text = item.TotalPrice.ToString();
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

    private void minus_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            int quantity = int.Parse(selectedControl.tbQuantity.Text);

            if (quantity > 1)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedControl.Barcode)
                    {
                        item.Quantity--;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;
                        selectedControl.tbQuantity.Text = item.Quantity.ToString();
                        selectedControl.tbTotalPrice.Text = item.TotalPrice.ToString();
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

    private void percent_button_Click(object sender, RoutedEventArgs e)
    {
        if (selectedControl != null)
        {
            activeTextboxIndex = 3;
            float discount = int.Parse(selectedControl.tbDiscount.Text);

            if (discount >= 0)
            {
                foreach (var item in tvm.Transactions)
                {
                    if (item.Barcode == selectedControl.Barcode)
                    {
                        item.Discount = discount;
                        var (totalPrice, discountPrice) = SetPrice(item.Price, item.Discount, item.Quantity);
                        item.TotalPrice = totalPrice;
                        item.DiscountSum = discountPrice;

                        selectedControl.tbDiscount.Text = item.Discount.ToString();
                        selectedControl.tbTotalPrice.Text = item.TotalPrice.ToString();

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

    private void search_button_Click(object sender, RoutedEventArgs e)
    {
        if(selectedControl != null)
        {
            selectedControl.product_Border.Background = Brushes.White;
            selectedControl = null!;
        }
        SearchProductWindow searchProductWindow = new SearchProductWindow();
        searchProductWindow.ShowDialog();
    }

    private async void save_button_Click(object sender, RoutedEventArgs e)
    {
        UpdateOrderDto dto = new UpdateOrderDto();
        dto.PartnerId = Order.PartnerId;
        dto.WorkerId = Order.WorkerId;
        
        UpdateOrderProductDto product = new UpdateOrderProductDto();
        List<UpdateOrderProductDto> products = new List<UpdateOrderProductDto>();
        foreach (var item in tvm.Transactions)
        {
            product.ProductId = item.Id;
            product.Count = item.Quantity;
            product.AvailableCount = item.AvailableCount;
            product.ItemTotalCost = item.TotalPrice;
            products.Add(product);
        }

        dto.ProductOrderItems = products;

        bool result = await _orderService.UpdateAsync(Order.Id, dto);
        if (result)
            notifier.ShowSuccess("Jo'natma yangilandi.");
        else
            notifier.ShowError("Jo'natmani yangilashda xatolik mavjud.");

    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        tvm = null!;
    }

    private void Nasiya_Button_Click(object sender, RoutedEventArgs e)
    {
        if (tvm.Transactions.Count > 0)
        {
            if (IsShipment)
            {
                ConvertTransaction(true, Order.PartnerId);
            }
            else
            {
                PartnersNationWindow nationWindow = new PartnersNationWindow();
                nationWindow.ShowDialog();
            }
        }
        else
            notifier.ShowInformation("Mahsulot xarid qilinmagan.");
    }

    private void btnPay_Click(object sender, RoutedEventArgs e)
    {
        if (tvm.Transactions.Count > 0)
        {
            PaymentTypeWindow paymentTypeWindow = new PaymentTypeWindow();
            paymentTypeWindow.SendWhere = 1;
            paymentTypeWindow.TotalCost = TotalPrice;
            paymentTypeWindow.ShowDialog();
        }
        else
            notifier.ShowInformation("Mahsulot xarid qilinmagan.");
    }

    public async void ConvertTransaction(bool isDebt, Guid id = default)
    {
        await Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(async () =>
            {
                AddSalesRequestDto dto = new AddSalesRequestDto
                {
                    TotalCost = tvm.TransactionPrice,
                    DiscountSum = tvm.DiscountPrice,
                    DebtSum = 0,
                    CardSum = 0,
                    CashSum = 0
                };
                
                switch (PaymentType)
                {
                    case "card":
                    case "click":
                    case "transfer":
                        dto.CardSum = tvm.TransactionPrice;
                        break;

                    case "cash":
                        dto.CashSum = tvm.TransactionPrice;
                        break;

                    case "clickandcash":
                        dto.CardSum = ClickSum;
                        dto.CashSum = CashSum;
                        break;
                }

                if (isDebt)
                {
                    dto.PartnerId = id;
                    dto.DebtSum = tvm.TransactionPrice;
                    dto.CardSum = 0;
                    dto.CashSum = 0;
                }

                dto.ProductSaleItems = tvm.Transactions
                    .Select(t => new AddProductSaleDto
                    {
                        ProductId = t.Id,
                        Count = t.Quantity,
                        Discount = t.Discount,
                        ItemTotalCost = t.TotalPrice
                    }).ToList();

                if (IsShipment)
                {
                    dto.WorkerId = Order.WorkerId;
                    await UpdateProductCount(Order, dto.ProductSaleItems);
                    IsShipment = false;
                }

                await ProductSale(dto, isDebt);
            })
        );
    }

    public void ConvertShipment(OrderDto dto) 
    {
        Order = dto;
        IsShipment = true;

        foreach (var product in dto.ProductOrderItems)
        {
            FullProductDto fpd = new FullProductDto
            {
                Id = product.Product.Id,
                Barcode = product.Product.Barcode,
                Name = product.Product.Name,
                Price = product.Product.Price,
                SellPrice = product.Product.SellPrice,
                Count = product.AvailableCount
            };

            AddNewProductTvm(fpd, product.Count);
        }
    }

    private async Task ProductSale(AddSalesRequestDto dto, bool isDebt)
    {
        var result = await _salesRequestsService.CreateSalesRequest(dto);
        if (result.Item2)
        {
            //PrintService printService = new PrintService();
            //printService.Print(dto, tvm.Transactions, result.Item1);

            if(Order != null && Order.Id != Guid.Empty)
                await UpdateSaleShipment(Order.Id);

            //if (isDebt)
            //    await NationSale(dto.PartnerId!.Value, dto.DebtSum!.Value);

            tvm.ClearTransaction();
            St_product.Children.Clear();
            ColculateTotalPrice();
            EmptyPrice();

            notifier.ShowSuccess("Sotuv amalga oshirildi.");
        }
        else
            notifier.ShowError("Sotuvda qandaydir muammo bor!!!");
    }

    public async Task NationSale(Guid id, double debtSum)
    {
        var result = await _partnerService.UpdatePartnerDebtSum(debtSum, id);    
    }

    public async Task UpdateSaleShipment(Guid Id)
    {
        while (true)
        {
            bool result = await _orderService.UpdateStatusAsync(Id);

            if (result)
            {
                await GetAllOrders();
                Order = null!;
                break;
            }

            await Task.Delay(1000);
        }
    }

    public async Task UpdateProductCount(OrderDto dto, List<AddProductSaleDto> tvm)
    {
        var products = new List<UpdateProductDto>();

        foreach (var item in tvm)
        {
            var product = dto.ProductOrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (product != null) 
            {
                if (product.Count != item.Count) 
                {
                    products.Add(new UpdateProductDto
                    {
                        ProductId = item.ProductId,
                        Count = Math.Abs(product.Count - item.Count),
                        IsIncrement = item.Count < product.Count 
                    });
                }
            }
            else 
            {
                products.Add(new UpdateProductDto
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    IsIncrement = false
                });
            }
        }

        foreach (var product in dto.ProductOrderItems)
        {
            var item = tvm.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (item == null) 
            {
                products.Add(new UpdateProductDto
                {
                    ProductId = product.ProductId,
                    Count = product.Count,
                    IsIncrement = true 
                });
            }
        }

        if (products.Any())
        {
            bool success = false;
            while (!success)
            {
                success = await _productService.UpdateProductCountAsync(products);
                if (!success)
                    await Task.Delay(1000);
            }
        }
    }

    private void Page_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (Keyboard.FocusedElement is TextBox textBox && textBox == Search)
            return;
        Keyboard.Focus(St_product);
    }

    private void Page_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.Source is TextBox textBox && textBox == Search)
            return;
        Keyboard.Focus(St_product);
    }

    private void Search_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        Search.Focus();
        e.Handled = true;
    }
}
