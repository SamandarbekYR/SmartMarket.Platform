﻿using SmartMarket.Desktop.Tablet.Components;
using SmartMarket.Desktop.Tablet.Windows;
using SmartMarket.Desktop.Tablet.Windows.Partners;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
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

    public static MainWindow GetMainWindow()
    {
        MainWindow mainWindow = null!;

        foreach (Window window in Application.Current.Windows)
        {
            Type type = typeof(MainWindow);
            if (window != null && window.DependencyObjectType.Name == type.Name)
            {
                mainWindow = (MainWindow)window;
                if (mainWindow != null)
                {
                    break;
                }
            }
        }
        return mainWindow!;
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
                partnersComponent.Tag = partner;
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

    private async void tb_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string search = tb_search.Text;

        await Task.Run(async () =>
        {
            if (search.Length >= 3)
            {
                var partner = await _partnerService.GetByName(search);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SetPartner(partner);
                });
            }
        });
    }

    private void SetPartner(PartnerDto dto)
    {
        St_partners.Children.Clear();
        if (dto != null)
        {
            Partner partner = new Partner()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                LastPayment = dto.LastPayment,
                TotalDebt = dto.TotalDebt
            };
            PartnersComponent partnersComponent = new PartnersComponent();
            partnersComponent.Tag = partner;
            partnersComponent.SetData(partner);
            St_partners.Children.Add(partnersComponent);
        }
    }

    private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        MainWindow mainWindow = GetMainWindow();
        mainWindow.Close();
        loginWindow.ShowDialog();
    }
}