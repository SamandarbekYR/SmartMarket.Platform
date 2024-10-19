using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.Windows.Partners;
using SmartMarketDeskop.Integrated.Services.Partners;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Tablet.Pages;

/// <summary>
/// Interaction logic for PartnersPage.xaml
/// </summary>
public partial class PartnersPage : Page
{

    private readonly IPartnerService _partnerService;

    public PartnersPage()
    {
        InitializeComponent();
        this._partnerService = new PartnerService();
    }


    public async void GetAllDebtor()
    {
        St_partners.Children.Clear();

        var partners = await _partnerService.GetAll();

        int count = 1;

        if (partners != null)
        {
            foreach (var partner in partners)
            {
                PartnersComponent partnersComponent = new PartnersComponent();
                partnersComponent.lb_Count.Content = count;
                partnersComponent.SetData(partner);
                St_partners.Children.Add(partnersComponent);
                count++;
            }
        }
        else
        {
            // loader o'chirilib malumot topilmadi degan yozuv chiqarib qo'yiladi
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        GetAllDebtor();
    }

    private void Partner_Create_Button_Click(object sender, RoutedEventArgs e)
    {
        PartnerCreateWindow partnerCreateWindow = new PartnerCreateWindow();
        partnerCreateWindow.ShowDialog();
    }
}
