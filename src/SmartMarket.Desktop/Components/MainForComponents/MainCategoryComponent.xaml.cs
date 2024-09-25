using SmartMarket.Desktop.Windows.Category;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartMarket.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainCategoryComponent.xaml
    /// </summary>
    public partial class MainCategoryComponent : UserControl
    {
        

        private ICategoryServer _server;


        public MainCategoryComponent()
        {
            InitializeComponent();
            _server=new SmartMarketDeskop.Integrated.Server.Repositories.Categories.CategoryServer();
           
        }


        public void SetValues(CategoryView category)
        {
            tbName.Text = category.Name;
        }

        private void BtnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryView=this.Tag as CategoryView;

            CategoryUpdateWindow categoryUpdateWindow = new CategoryUpdateWindow(); 
            categoryUpdateWindow.GetData(categoryView);
            categoryUpdateWindow.ShowDialog();
        }

        private async void BntDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryView=this.Tag as CategoryView;

            var messageBoxResult = MessageBox.Show("O'chirishni hohlaysizmi!", "Ogohlantirish!", MessageBoxButton.YesNo);

            if(messageBoxResult == MessageBoxResult.Yes)
            {
              await  _server.DeleteAsync(categoryView.Id);
            }
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(selected)
            {
                brCategory.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#988F8E"));
                selected = false;
            }
            else
            {
                brCategory.Background = Brushes.White;
                selected = true;
            }
        }

        public bool selected { get; set; } = true;

    }
}
