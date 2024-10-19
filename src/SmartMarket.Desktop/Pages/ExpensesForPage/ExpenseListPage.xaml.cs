using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Services.Expenses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if(filterTextBox.Text.Length == 3)
            {
                if (e.Key == Key.Enter)
                {
                    FilterExpenses();
                }
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllExpence();
        }
    }
}
