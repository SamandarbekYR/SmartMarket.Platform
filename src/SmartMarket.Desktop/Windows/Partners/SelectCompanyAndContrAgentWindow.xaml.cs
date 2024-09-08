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

namespace SmartMarket.Desktop.Windows.Partners
{
    /// <summary>
    /// Interaction logic for SelectCompanyAndContrAgentWindow.xaml
    /// </summary>
    public partial class SelectCompanyAndContrAgentWindow : Window
    {
        public SelectCompanyAndContrAgentWindow()
        {
            InitializeComponent();
        }

        private void btnComany_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnContrAgeny_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public void GetData()
        {

        }
    }
}
