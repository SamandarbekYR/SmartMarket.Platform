using SmartMarket.Service.DTOs.Products.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;

namespace SmartMarket.Desktop.Windows.Expenses
{
    /// <summary>
    /// Interaction logic for RunningOutOfProductDetailWindow.xaml
    /// </summary>
    public partial class RunningOutOfProductDetailWindow : Window
    {
        public RunningOutOfProductDetailWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        public void SetData(FullProductDto dto)
        {
            pCode_TextBlock.Text = dto.PCode.ToString();
            barcode_TextBlock.Text = dto.Barcode.ToString();
            productName_TextBlock.Text = dto.Name;
            productSellPrice_TextBlock.Text = dto.SellPrice.ToString();
            productCount_TextBlock.Text = dto.Count.ToString();
            productUnitOfMeasure_TextBlock.Text = dto.UnitOfMeasure;
            categoryName_TextBlock.Text = dto.CategoryName;
            worker_TextBlock.Text = dto.WorkerFirstName + " " + dto.WorkerLastName;
            productNoteAmount_TextBlock.Text = dto.NoteAmount.ToString();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
    }
}
