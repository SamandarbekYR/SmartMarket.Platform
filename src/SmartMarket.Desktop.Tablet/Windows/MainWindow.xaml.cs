using System.Windows;

namespace SmartMarket.Desktop.Tablet;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Close_Button_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}