using System.Windows;
using System.Windows.Media;

namespace SmartMarket.Desktop.Tablet.Windows;

/// <summary>
/// Interaction logic for MessageBoxWindow.xaml
/// </summary>
public partial class MessageBoxWindow : Window
{
    public MessageBoxWindow(string Message, MessageType Type, MessageButtons Buttons)
    {
        InitializeComponent();

        txtMessage.Content = Message;

        switch (Type)
        {

            case MessageType.Info:
                {
                    lbl_Text_Title.Content = "Ma'lumot";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                }
                break;
            case MessageType.Confirmation:
                lbl_Text_Title.Content = "Tasdiqlash";
                break;
            case MessageType.Success:
                {
                    lbl_Text_Title.Content = "Muvaffaqiyatli";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                }
                break;
            case MessageType.Warning:
                {
                    lbl_Text_Title.Content = "Ogohlantirish";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(251, 255, 0));
                }
                break;
            case MessageType.Error:
                {
                    lbl_Text_Title.Content = "Xato";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                break;
        }

        switch (Buttons)
        {
            case MessageButtons.OkCancel:
                yes_Btn.Visibility = Visibility.Collapsed;
                no_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.YesNo:
                ok_Btn.Visibility = Visibility.Collapsed;
                cancel_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.Ok:
                ok_Btn.Visibility = Visibility.Visible;
                cancel_Btn.Visibility = Visibility.Collapsed;
                yes_Btn.Visibility = Visibility.Collapsed;
                no_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.Retry:
                {
                    retry_Btn.Visibility = Visibility.Visible;
                    ok_Btn.Visibility = Visibility.Collapsed;
                    cancel_Btn.Visibility = Visibility.Visible;
                    yes_Btn.Visibility = Visibility.Collapsed;
                    no_Btn.Visibility = Visibility.Collapsed;
                }
                break;
        }
        txtMessage.Content = Message;

        switch (Type)
        {

            case MessageType.Info:
                {
                    lbl_Text_Title.Content = "Ma'lumot";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(13, 110, 253));
                }
                break;
            case MessageType.Confirmation:
                lbl_Text_Title.Content = "Tasdiqlash";
                break;
            case MessageType.Success:
                {
                    lbl_Text_Title.Content = "Muvaffaqiyatli";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                }
                break;
            case MessageType.Warning:
                {
                    lbl_Text_Title.Content = "Ogohlantirish";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(251, 255, 0));
                }
                break;
            case MessageType.Error:
                {
                    lbl_Text_Title.Content = "Xato";
                    Header_Border.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                break;
        }

        switch (Buttons)
        {
            case MessageButtons.OkCancel:
                yes_Btn.Visibility = Visibility.Collapsed;
                no_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.YesNo:
                ok_Btn.Visibility = Visibility.Collapsed;
                cancel_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.Ok:
                ok_Btn.Visibility = Visibility.Visible;
                cancel_Btn.Visibility = Visibility.Collapsed;
                yes_Btn.Visibility = Visibility.Collapsed;
                no_Btn.Visibility = Visibility.Collapsed;
                retry_Btn.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.Retry:
                {
                    retry_Btn.Visibility = Visibility.Visible;
                    ok_Btn.Visibility = Visibility.Collapsed;
                    cancel_Btn.Visibility = Visibility.Visible;
                    yes_Btn.Visibility = Visibility.Collapsed;
                    no_Btn.Visibility = Visibility.Collapsed;
                }
                break;
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void ok_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void cancel_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void yes_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void no_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void retry_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void close_Btn_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
    }
    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
        Retry
    }
}
