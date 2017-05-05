using MaterialDesignThemes.Wpf;
using Notification_PI.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Notification_PI.CustomControl
{
    /// <summary>
    /// Interaction logic for ItemFlipView.xaml
    /// </summary>
    public partial class ItemFlipView : UserControl
    {
        public ItemFlipView()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Clicked(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(this, null);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "Initial Mail")
            {
                groupBox.Header = "Initial Mail";
                (DataContext as ItemControlViewModel).OnPropertyChanged(new PropertyChangedEventArgs("ItemProperties"));
            }
            else
            {
                groupBox.Header = "Final Mail";
                groupBox.DataContext = DataContext;
            }
        }
    }
}
