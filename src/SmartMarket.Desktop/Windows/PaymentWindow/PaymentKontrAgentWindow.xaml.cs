using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using static SmartMarket.Desktop.Windows.BlurWindow.BlurEffect;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgentPayments;
using SmartMarketDeskop.Integrated.Services.PayDesks;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;

namespace SmartMarket.Desktop.Windows.PaymentWindow;

/// <summary>
/// Interaction logic for PaymentKontrAgentWindow.xaml
/// </summary>
public partial class PaymentKontrAgentWindow : Window
{
    private readonly IContrAgentPaymentService contrAgentPaymentService;
    private readonly IPayDeskService payDeskService;
    ContrAgentViewModels _contrAgentViewModel;
    public PaymentKontrAgentWindow()
    {
        InitializeComponent();
        this.contrAgentPaymentService = new ContrAgentPaymentService();
        this.payDeskService = new PayDeskService();
    }

    List<PayDesksDto> payDesks = new List<PayDesksDto>();
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

    public async Task GetAllPaydesks()
    {
        payDesks = await Task.Run(async () => await payDeskService.GetAll());
        if (payDesks.Any())
        {
            var payDeskNames = payDesks.Select(x => x.Name)
                .Distinct()
                .ToList();

            foreach (var paydesk in payDesks)
                payDeskComboBox.Items.Add(paydesk.Name);
        }
    }

    public void GetContrAgent(ContrAgentViewModels contrAgent)
    {
        _contrAgentViewModel = contrAgent;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        EnableBlur();
        await GetAllPaydesks();
    }

    private void phone_number_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        string text = textBox.Text;
        string filteredText = Regex.Replace(text, "[^0-9]+", "");

        if (text != filteredText)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = filteredText;
            textBox.CaretIndex = caretIndex > 0 ? caretIndex - 1 : 0;
        }
    }

    private async void BtnPay_Click(object sender, RoutedEventArgs e)
    {
        if(!string.IsNullOrEmpty(tnPayAmount.Text))
        {
            AddContrAgentPaymentDto dto = new AddContrAgentPaymentDto();

            {
                PayDesksDto payDesk = payDesks.Where(x => x.Name == payDeskComboBox.SelectedValue).FirstOrDefault();
                dto.PayDeskId = payDesk.Id;
            }
            dto.ContrAgentId = _contrAgentViewModel.Id;
            dto.PaymentType = payDeskComboBox.SelectedItem?.ToString();
            dto.LastPayment = Convert.ToDouble(tnPayAmount.Text);
            dto.TotalDebt = Convert.ToDouble(_contrAgentViewModel.DebtSum);

            var res = await contrAgentPaymentService.AddAsync(dto);
            if(res == true)
            {
                this.Close();
            }
        }
    }

    private void BtnPayHistory_Click(object sender, RoutedEventArgs e)
    {
        var contrAgent = this.Tag as ContrAgentDto;
       
        PaymentContrAgentWindow paymentContrAgentWindow = new PaymentContrAgentWindow(contrAgent);
        paymentContrAgentWindow.ShowDialog();
    }
}
