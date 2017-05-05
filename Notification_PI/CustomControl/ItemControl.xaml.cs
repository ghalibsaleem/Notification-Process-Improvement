using MaterialDesignThemes.Wpf;
using Notification_PI.ViewModels;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notification_PI.CustomControl
{
    /// <summary>
    /// Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public ItemControl()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Clicked(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(this, null);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            
            var view = new ItemConfirmation()
            {
                DataContext = (sender as Button).DataContext as ItemControlViewModel


            };
            if (btn.Content.ToString() == "Initial Mail")
            {
                view.Header = "Initial Mail";
            }
            else
            {
                view.Header = "Final Mail";
            }
            DialogHost.CloseDialogCommand.Execute(this,null);
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

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var propInfo = textBox.DataContext as PropertyInfo;
            var model = (ItemControlViewModel)DataContext;
            propInfo.SetValue(model.SitObject, textBox.Text);
        }
    }
}
