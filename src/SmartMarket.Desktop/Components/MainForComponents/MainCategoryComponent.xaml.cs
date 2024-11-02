using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Windows;
using SmartMarket.Desktop.Windows.Category;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static SmartMarket.Desktop.Windows.MessageBoxWindow;

namespace SmartMarket.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainCategoryComponent.xaml
/// </summary>
public partial class MainCategoryComponent : UserControl
{

    private ICategoryServer _server;

    public bool selected { get; set; } = true;


    public MainCategoryComponent()
    {
        InitializeComponent();
        _server = new SmartMarketDeskop.Integrated.Server.Repositories.Categories.CategoryServer();

    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });

    public void SetValues(CategoryView category, int count)
    {
        tbNumber.Text = count.ToString();
        tbName.Text = category.Name;
    }

    private void BtnEditCategory_Click(object sender, RoutedEventArgs e)
    {
        var categoryView = this.Tag as CategoryView;

        CategoryUpdateWindow categoryUpdateWindow = new CategoryUpdateWindow();
        categoryUpdateWindow.GetData(categoryView!);
        categoryUpdateWindow.ShowDialog();
    }

    private async void BntDeleteCategory_Click(object sender, RoutedEventArgs e)
    {
        var categoryView = this.Tag as CategoryView;
        var message = tbName.Text;

        var messageBox = new MessageBoxWindow(message + "ni o'chirilsinmi?", MessageType.Confirmation, MessageButtons.OkCancel);
        var result = messageBox.ShowDialog();

        if (result == true)
        {
            var res = await _server.DeleteAsync(categoryView!.Id);

            if (res)
                notifier.ShowSuccess($"{tbName.Text} muvaffaqiyatli o'chirildi.");
            else
                notifier.ShowError("O'chirishda xato yuz berdi.");
        }

    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var categoryView = this.Tag as CategoryView;

        var page = FindParentPage(this);
        if (page is MainPage mainPage)
        {
            mainPage.SelectCategory(this, categoryView!.Id);
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
}
