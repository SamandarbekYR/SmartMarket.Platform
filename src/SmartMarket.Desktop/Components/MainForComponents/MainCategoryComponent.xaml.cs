using SmartMarket.Desktop.Windows.Category;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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


        public void SetValues(long number,string Name)
        {
            tbNumber.Text=number.ToString();
            tbName.Text =Name;
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

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
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
