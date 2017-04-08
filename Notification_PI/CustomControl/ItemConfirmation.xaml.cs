using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notification_PI.CustomControl
{
    /// <summary>
    /// Interaction logic for ItemConfirmation.xaml
    /// </summary>
    public partial class ItemConfirmation : UserControl
    {
        public ItemConfirmation()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return groupBox.Header.ToString(); }
            set { groupBox.Header = value; }
        }
        private void Cancel_Button_Clicked(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(this, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
