using SmartMarket.Desktop.Windows.Expenses;
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

namespace SmartMarket.Desktop.Components.ExpenseForComponents
{
    /// <summary>
    /// Interaction logic for ExpenseComponent.xaml
    /// </summary>
    public partial class ExpenseComponent : UserControl
    {
        private readonly IExpensesServer server;
        public ExpenseComponent()
        {
            InitializeComponent();
            this.server = new ExpensesServer();
        }
        public Guid ExpenseId { get; set; }

        public void SetData(FullExpenceDto expense)
        {
            tbReason.Text = expense.Reason;
            tbAmount.Text = expense.Amount.ToString();
            tbTypeOfPayment.Text = expense.TypeOfPayment;
            tbWorker.Text = expense.WorkerFirstName;
            tbPayDesk.Text = expense.PayDeskName;

            this.DataContext = expense;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is Border border && border.Tag is FullExpenceDto dto)
            {
                ExpenseDetailWindow expenseDetailWindow = new ExpenseDetailWindow();
                expenseDetailWindow.SetData(dto);
                expenseDetailWindow.ShowDialog();
            }
        }
    }
}
