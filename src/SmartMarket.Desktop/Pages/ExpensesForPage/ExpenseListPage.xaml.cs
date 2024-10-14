using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Service.DTOs.Expence;
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

        public async void FilterExpensesAsync()
        {
            FilterExpenseDto filter = new FilterExpenseDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filter.FromDateTime = fromDateTime.SelectedDate.Value;
                filter.ToDateTime = toDateTime.SelectedDate.Value;
            }

            var workerFirstName = workerNameComboBox.SelectedItem?.ToString();
            if(!string.IsNullOrEmpty(workerFirstName) && !workerNameComboBox.Equals("Sotuvchi"))
            {
                filter.WorkerName = workerFirstName;
            }

            var searchReason = reasonTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(searchReason))
            {
                filter.Reason = searchReason;
            }

            var filterResult = await server.FilterExpensesAsync(filter);

            ShowExpense(filterResult);
        }

        private void ShowExpense(IEnumerable<FullExpenceDto> expences)
        {
            St_Expenses.Visibility = Visibility.Visible;
            St_Expenses.Children.Clear();
            int count = 1;

            foreach(var expense in expences)
            {
                ExpenseComponent expenseComponent = new ExpenseComponent();
                expenseComponent.Tag = expense;
                expenseComponent.SetData(expense);
                St_Expenses.Children.Add(expenseComponent);
                count++;
            }
        }

        private void DataPicter_SelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {
            //FilterExpensesAsync();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FilterExpensesAsync();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //FilterExpensesAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllExpence();
        }
    }
}
