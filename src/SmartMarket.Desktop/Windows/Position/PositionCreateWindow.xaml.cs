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

namespace SmartMarket.Desktop.Windows.Position
{
    /// <summary>
    /// Interaction logic for PositionCreateWindow.xaml
    /// </summary>
    public partial class PositionCreateWindow : Window
    {
        public PositionCreateWindow()
        {
            InitializeComponent();
        }

        private void btnPositionCreate_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        

        private void brClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
