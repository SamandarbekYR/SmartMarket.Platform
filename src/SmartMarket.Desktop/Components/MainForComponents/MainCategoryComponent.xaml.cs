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

namespace SmartMarket.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainCategoryComponent.xaml
    /// </summary>
    public partial class MainCategoryComponent : UserControl
    {
        public MainCategoryComponent()
        {
            InitializeComponent();
        }


        public void SetValues(int id,string name)
        {
            tbNumber.Text = id.ToString();
            tbName.Text = name;
        }
    }
}
