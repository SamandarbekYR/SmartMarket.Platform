using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
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

namespace SmartMarket.Desktop.Pages.ExpensesForPage
{
    /// <summary>
    /// Interaction logic for ExpenseListPage.xaml
    /// </summary>
    public partial class ExpenseListPage : Page
    {
        private readonly IExpensesServer server;
        public ExpenseListPage()
        {
            InitializeComponent();
            this.server = new ExpensesServer();
            Loaded += Page_Loaded;
        }

        public async void GetAllExpence()
        {
            St_Expenses.Children.Clear();

            var expenses = await server.GetExpensesFullInformationAsync();

            int count = 1;

            if (expenses != null)
            {
                foreach (var expense in expenses)
                {
                    ExpenseComponent expenseComponent = new ExpenseComponent();
                    expenseComponent.tbNumber.Text = count.ToString();
                    expenseComponent.SetData(expense);
                    St_Expenses.Children.Add(expenseComponent);
                    count++;
                }
            }
            else
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllExpence();
        }
    }
}
