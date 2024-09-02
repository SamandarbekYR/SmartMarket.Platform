using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Repositories.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Windows.Category
{
    /// <summary>
    /// Interaction logic for CategoryCreateWindow.xaml
    /// </summary>
    public partial class CategoryCreateWindow : Window
    {
        public ICategoryServer _categoryServer;
        public CategoryCreateWindow()
        {
            InitializeComponent();
            this._categoryServer = new CategoryServer();
        }
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        private async void btnCategoryCreate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CategoryDto categoryDto = new CategoryDto();
            categoryDto.Name = txtCategoryName.Text;
            categoryDto.Description = "description";
            bool result = await _categoryServer.AddAsync(categoryDto);
            
            if(result)
               notifier.ShowSuccess("Category muvafaqqiyatli qo'shildi");
            else
               notifier.ShowError("Category qo'shishda xatolik yuz berdi");
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
