using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Desktop.Tablet.Windows.Partners;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for PartnersPage.xaml
/// </summary>
public partial class PartnersPage : Page
{

    private readonly IPartnerService _partnerService;

    public PartnersPage()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
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
    public async Task GetAllDebtor()
    {
        St_partners.Children.Clear();

        var partners = await Task.Run(() => _partnerService.GetAll());
        Loader.Visibility = Visibility.Collapsed;

        int count = 1;

        if (partners.Count > 0)
        {
            foreach (var partner in partners)
            {
                PartnersComponent partnersComponent = new PartnersComponent();
                partnersComponent.lb_Count.Content = count;
                partnersComponent.Tag = partner;
                partnersComponent.SetData(partner);
                St_partners.Children.Add(partnersComponent);
                count++;
            }
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllDebtor();
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

    private void Partner_Create_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateWindow partnerCreateWindow = new PartnerCreateWindow();
        partnerCreateWindow.ShowDialog();
    }

    private CancellationTokenSource _cancellationTokenSource;

    private async void tb_search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;

        string search = tb_search.Text;

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
            EmptyData.Visibility = Visibility.Collapsed;
            return;
        }

        St_partners.Children.Clear();
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
                        var partner = await _partnerService.GetByName(search);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SetPartner(partner);
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
            St_partners.Children.Clear();
            EmptyData.Visibility = Visibility.Collapsed;
            await GetAllDebtor();
        }
    }

    private void SetPartner(PartnerDto dto)
    {
        Loader.Visibility = Visibility.Collapsed;
        St_partners.Children.Clear();
        if (dto != null)
        {
            EmptyData.Visibility = Visibility.Collapsed;
            Partner partner = new Partner()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                LastPayment = dto.LastPayment,
                TotalDebt = dto.TotalDebt
            };
            PartnersComponent partnersComponent = new PartnersComponent();
            partnersComponent.Tag = partner;
            partnersComponent.SetData(partner);
            St_partners.Children.Add(partnersComponent);
        }
        else
        {
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.Close();
        loginWindow.ShowDialog();
    }

    private void Sends_Button_Click(object sender, RoutedEventArgs e)
    {
        SecondPage secondPage = new SecondPage();
        MainWindow mainWindow = GetMainWindow();
        secondPage.i = 2;
        mainWindow.PageNavigator.Content = secondPage;
    }
}
