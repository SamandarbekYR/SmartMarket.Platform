using SmartMarket.Desktop.Components.SettingsForComponent;

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket.Desktop.Pages.SettingsForPage;

/// <summary>
/// Interaction logic for SettingsScalesPage.xaml
/// </summary>
public partial class SettingsScalesPage : Page
{
    public SettingsScalesPage()
    {
        InitializeComponent();
        GetScales();
    }

    private void bntAddScales_Click(object sender, RoutedEventArgs e)
    {

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

    public void GetScales()
    {
        St_Scales.Visibility = Visibility.Visible;
        St_Scales.Children.Clear();

        int timeValue = int.TryParse(timeTextBox.Text, out var parsedValue) ? parsedValue : 50;

        SettingsScalesComponent settingsScalesComponent = new SettingsScalesComponent();
        settingsScalesComponent.Tag = 1;
        settingsScalesComponent.SetData("Tarozi 1:", timeValue);
        settingsScalesComponent.BorderThickness = new Thickness(0, 0, 0, 5);
        St_Scales.Children.Add(settingsScalesComponent);

    }
}
