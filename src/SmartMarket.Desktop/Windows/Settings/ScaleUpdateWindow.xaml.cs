﻿using Microsoft.Win32;

using SmartMarketDeskop.Integrated.Services.Scales;

using SmartMarketDesktop.DTOs.DTOs.Scales;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SmartMarket.Desktop.Windows.Settings
{
    /// <summary>
    /// Interaction logic for ScaleUpdateWindow.xaml
    /// </summary>
    public partial class ScaleUpdateWindow : Window
    {
        private IScaleService _scaleService;
        private ScaleDto _scaleDto;
        public Func<Task> UpdateScale {  get; set; }
        public ScaleUpdateWindow(ScaleDto scale)
        {
            _scaleDto = scale;
            _scaleService = new ScaleService();
            InitializeComponent();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 200,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });


        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Scale_Update_Button_Click(object sender, RoutedEventArgs e)
        {
            AddScaleDto scale = new AddScaleDto();
            scale.Name = tb_ScaleName.Text;
            scale.UpdateTimeInterval = int.Parse(tb_UpdateTime.Text);
            
            bool result = await _scaleService.UpdateScaleAsync(scale, _scaleDto.Id);

            if (result)
            {
                this.Close();
                notifier.ShowSuccess("Tarozi yangilandi.");
                await UpdateScale();
            }
            else
                notifierThis.ShowError("Tarozi yaratishda qandaydir xatolik bor !");
        }


        private void TimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void TimeTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextNumeric(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }
    }
}