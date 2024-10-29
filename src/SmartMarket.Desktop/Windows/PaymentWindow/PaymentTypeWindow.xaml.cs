using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarket.Desktop.Pages.SaleForPage;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Windows.PaymentWindow
{
    /// <summary>
    /// Interaction logic for PaymentTypeWindow.xaml
    /// </summary>
    public partial class PaymentTypeWindow : Window
    {
        public PaymentTypeWindow()
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

        private void btnclose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        private void HandlePayment(string paymentType)
        {
            foreach (Window window in Application.Current.Windows)
            {
                var frame = window.FindName("PageNavigator") as Frame;
                if (frame != null && frame.Content is SalePage salePage)
                {
                    salePage.PaymentType = paymentType;
                    salePage.SaleProducts(false);
                    break;
                }
            }

            this.Close();
        }

        private void BntCash_Click(object sender, RoutedEventArgs e)
        {
            HandlePayment("cash");
        }

        private void BntCard_Click(object sender, RoutedEventArgs e)
        {
            HandlePayment("card");
        }

        private void BntTransferMoney_Click(object sender, RoutedEventArgs e)
        {
            HandlePayment("transfer");
        }

        private void BtnClik_Click(object sender, RoutedEventArgs e)
        {
            HandlePayment("click");
        }


        private void BntClikAndCash_Click(object sender, RoutedEventArgs e)
        {
            ClikAndCashWindow clikAndCashWindow = new ClikAndCashWindow();
            this.Close();
            clikAndCashWindow.ShowDialog();
        }
    }
}
