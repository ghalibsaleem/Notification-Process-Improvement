using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Notification_PI.CustomControl
{
    /// <summary>
    /// Interaction logic for ItemGrid.xaml
    /// </summary>
    public partial class ItemGrid : UserControl
    {
        public ItemGrid()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var view = new ItemControl()
            {
                DataContext = new ItemControlViewModel((sender as Button).DataContext as SIT2_Item)


            };
            await DialogHost.Show(view, "RootDialog", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
        }

        private void gridDialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }

        private void gridDialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }
    }
}
