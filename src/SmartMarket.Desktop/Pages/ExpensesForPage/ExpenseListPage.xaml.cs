using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using SmartMarketDeskop.Integrated.Services.Expenses;
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
        private readonly IExpenseService expenseService;
        public ExpenseListPage()
        {
            InitializeComponent();
            this.expenseService = new ExpenseService();
        }

        public async void GetAllExpence()
        {
            var expenses = await expenseService.GetAll();
            ShowExpenses(expenses);
        }

        private async void FilterExpenses()
        {
            FilterExpenseDto filter = new FilterExpenseDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filter.FromDateTime = fromDateTime.SelectedDate.Value;
                filter.ToDateTime = toDateTime.SelectedDate.Value;
            }

            var selectionWorkerName = workerComboBox.SelectedItem?.ToString();

            if(!string.IsNullOrEmpty(selectionWorkerName) && !selectionWorkerName.Equals("Sotuvchi"))
            {
                filter.WorkerName = selectionWorkerName;
            }

            var filterReason = filterTextBox.Text.ToLower();
            if(!string.IsNullOrEmpty(filterReason))
            {
                filter.Reason = filterReason;
            }

            var filterExpense = await expenseService.FilterExpense(filter);
            ShowExpenses(filterExpense);
        }

        private async void ShowExpenses(IEnumerable<FullExpenceDto> expenses)
        {
            St_Expenses.Children.Clear();

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

        private void toDateTime_SelectadDataChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterExpenses();
        }

        private void workerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterExpenses();
        }

        private void FilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(filterTextBox == null)
            {
                MessageBox.Show("Search filter null");
            }
            else if(e.Key == Key.Enter)
            {
                FilterExpenses();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllExpence();
        }
    }
}
