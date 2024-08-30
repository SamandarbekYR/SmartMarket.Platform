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

namespace SmartMarket.Desktop.Windows.DebtForWindow
{
    /// <summary>
    /// Interaction logic for DebtWindow.xaml
    /// </summary>
    public partial class DebtWindow : Window
    {
        public DebtWindow()
        {
            InitializeComponent();
        }

        

        private void btnclose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
