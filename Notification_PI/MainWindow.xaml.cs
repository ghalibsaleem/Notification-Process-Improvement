using D.Net.EmailClient;
using HtmlParser;
using Models;
using Notification_PI.CustomControl;
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

namespace Notification_PI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MaterialDesignThemes.Wpf.DialogHost.OpenDialogCommand.Execute(new SignIn(), rootDialog);
            ItemGrid grid = new ItemGrid();
            ItemGridViewModel itemModel = new ItemGridViewModel();
            itemModel.FillCollection();
            grid.DataContext = itemModel;
            mainContentControl.Content = grid;
            //ItemControl c = new ItemControl();
            
            //c.DataContext = new ItemControlViewModel() { SitObject = itemModel.Sit_ItemsCollection.First(),
            //ItemProperties =new ObservableCollection<PropertyInfo>( itemModel.Sit_ItemsCollection.First().GetType().GetProperties().ToList())};
            //mainContentControl.Content = c;
            //mainContentControl.Content = new SignIn();
            //mainContentControl.Content = new Loading();

        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
