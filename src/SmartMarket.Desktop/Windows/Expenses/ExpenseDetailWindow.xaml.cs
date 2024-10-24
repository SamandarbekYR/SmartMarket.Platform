using SmartMarket.Service.DTOs.Expence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ExpenseDetailWindow.xaml
    /// </summary>
    public partial class ExpenseDetailWindow : Window
    {
        public ExpenseDetailWindow()
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

        public void SetData(FullExpenceDto dto)
        {
            reason_TextBlock.Text = dto.Reason;
            amount_TextBlock.Text = dto.Amount.ToString();
            typeOfPayment_TextBlock.Text = dto.TypeOfPayment;
            payDeskName_TextBlock.Text = dto.PayDeskName;
            workerFirstname_TextBlock.Text = dto.WorkerFirstName;
            workerLastname_TextBlock.Text = dto.WorkerLastName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
