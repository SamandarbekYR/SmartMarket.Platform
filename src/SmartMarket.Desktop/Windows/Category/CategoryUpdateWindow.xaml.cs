using SmartMarket.Desktop.Pages.MainForPage;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
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
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.Category
{
    /// <summary>
    /// Interaction logic for CategoryUpdateWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        private ICategoryServer categoryServer;
       
        CategoryView categoryView;
        public CategoryUpdateWindow()
        {
            InitializeComponent();
            
            this.categoryServer = new SmartMarketDeskop.Integrated.Server.Repositories.Categories.CategoryServer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void EditBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {

            //txtName.Text = categoryView.Name;
            //txtDescribtion.Text=categoryView.Description;

            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Name=txtName.Text;
            categoryDto.Description=txtDescribtion.Text;

            await categoryServer.UpdateAsync(categoryDto, categoryView.Id);

         //   if (_mainPage != null) _mainPage.GetAllCategory();
            Clear();
            this.Close();
        }


        public void GetData(CategoryView _category)
        {
            categoryView = _category;
        }


        public void Clear()
        {
            txtDescribtion.Text=txtName.Text=string.Empty;
        }
    }
}
