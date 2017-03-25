using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
                DataContext = new ItemControlViewModel()
                {
                    SitObject = (sender as Button).DataContext as SIT2_Item,
                    ItemProperties = new ObservableCollection<PropertyInfo>(typeof(SIT2_Item).GetProperties())
                }
                
            };
            Binding widthBinding = new Binding();
            widthBinding.ElementName = "stackDialog";
            widthBinding.Path = new PropertyPath("ActualWidth");
            widthBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(view, ItemControl.WidthProperty, widthBinding);

            Binding heightBinding = new Binding();
            heightBinding.ElementName = "stackDialog";
            heightBinding.Path = new PropertyPath("ActualHeight");
            heightBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(view, ItemControl.HeightProperty, heightBinding);

            await DialogHost.Show(view, "gridDialogHost");
        }
    }
}
