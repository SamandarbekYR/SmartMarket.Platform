using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Interop;
using SmartMarketDeskop.Integrated.Services.Workers.Position;

namespace SmartMarket.Desktop.Windows.AccountSettings
{
    /// <summary>
    /// Interaction logic for AccountCreateWindow.xaml
    /// </summary>
    public partial class AccountCreateWindow : Window
    {
        private IPositionService _positionService;

        public AccountCreateWindow()
        {
            InitializeComponent();
            _positionService = new PositionService();
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

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnCreateAccount_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
            
        }
    }
}
