using D.Net.EmailClient;
using HtmlParser;
using Models;
using Notification_PI.CustomControl;
using Notification_PI.ViewModels;
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
            
            ItemGrid grid = new ItemGrid();
            ItemGridViewModel itemModel = new ItemGridViewModel();
            itemModel.FillCollection();
            grid.DataContext = itemModel;
            mainContentControl.Content = grid;
            //mainContentControl.Content = new SignIn();
            //mainContentControl.Content = new Loading();

        }
    }
}
