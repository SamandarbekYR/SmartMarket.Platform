using SmartMarket.Desktop.Components.ExpenseForComponents;
using SmartMarket.Desktop.Components.Loader;
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

        public async Task GetAllExpence()
        {
            St_Expenses.Children.Clear();
            var expenses = await Task.Run(async () => await expenseService.GetAll());

            List<string> workerNames = expenses
                .Select(x => x.WorkerFirstName)
                .Distinct()
                .ToList();

            foreach(var worker in workerNames)
            {
                workerComboBox.Items.Add(worker);
            }

            ShowExpenses(expenses);
        }

        private async void FilterExpenses()
        {
            EmptyDataExpense.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
            St_Expenses.Children.Clear();
            FilterExpenseDto filter = new FilterExpenseDto();

            if(fromDateTime.SelectedDate != null && toDateTime.SelectedDate != null)
            {
                filter.FromDateTime = fromDateTime.SelectedDate.Value;
                filter.ToDateTime = toDateTime.SelectedDate.Value;
            }

            if (workerComboBox.SelectedItem != null)
            {
                var selectionWorkerName = workerComboBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectionWorkerName) && !selectionWorkerName.Equals("Sotuvchi"))
                {
                    filter.WorkerName = selectionWorkerName;
                }
            }

            if(filterTextBox != null)
            {
                filter.Reason = filterTextBox.Text;
            }

            var filterExpense = await Task.Run(async () => await expenseService.FilterExpense(filter));
            ShowExpenses(filterExpense);
        }

        private async void ShowExpenses(IEnumerable<FullExpenceDto> expenses)
        {
            Loader.Visibility = Visibility.Collapsed;
            int count = 1;

            if (expenses.Any())
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
                EmptyDataExpense.Visibility = Visibility.Visible;
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
            if(e.Key == Key.Enter)
            {
                FilterExpenses();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAllExpence();
        }
    }
}
