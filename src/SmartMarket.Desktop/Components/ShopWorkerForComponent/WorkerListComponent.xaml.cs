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

namespace SmartMarket.Desktop.Components.ShopWorkerForComponent
{
    /// <summary>
    /// Interaction logic for WorkerListComponent.xaml
    /// </summary>
    public partial class WorkerListComponent : UserControl
    {
        public WorkerListComponent()
        {
            InitializeComponent();
        }

        public void SetValues(int id, string name, string lastName)
        {
            tbNumber.Text = id.ToString();
            tbName.Text = name + " " + lastName;
        }
    }
}
