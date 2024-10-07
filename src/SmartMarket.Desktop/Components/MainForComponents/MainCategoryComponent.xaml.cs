using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarket.Desktop.Windows.Category;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);

        if (messageBoxResult == MessageBoxResult.Yes)
        {
            await _server.DeleteAsync(categoryView!.Id);
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
