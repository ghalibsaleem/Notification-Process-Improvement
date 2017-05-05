using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace Notification_PI.CustomControl
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }
        public Message(string message)
        {
            InitializeComponent();
            MessageBox.Text = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(this, null);
        }
    }
}
