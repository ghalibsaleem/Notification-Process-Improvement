using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.CustomControl;
using Notification_PI.FileHelper;
using Notification_PI.JSONHelper;
using Notification_PI.ModelsHelper;
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
        public  MainWindow()
        {
            InitializeComponent();
            DialogHost.OpenDialogCommand.Execute(new Loading(), rootDialog);
            MainWindowViewModel mainModel = new MainWindowViewModel();
            DataContext = mainModel;
            
            ItemGrid grid = new ItemGrid();
            ItemGridViewModel itemModel = new ItemGridViewModel();
            
            grid.DataContext = itemModel;
            mainContentControl.Content = grid;
            

        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private async void RootDialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            DialogHost host = sender as DialogHost;
            if(host.DialogContent.GetType() == typeof(Loading)){
                UserHelper helper = new UserHelper();
                User user = await helper.GetUserFromSystem();

                if (user != null)
                {
                    bool connected = await helper.CheckUser(user);
                    if (connected)
                    {
                        (DataContext as MainWindowViewModel).User = user;
                        await ((mainContentControl.Content as ItemGrid).DataContext as ItemGridViewModel).FillCollection(user);
                        DialogHost.CloseDialogCommand.Execute(null, rootDialog);
                    }
                    else
                    {
                       DialogHost.OpenDialogCommand.Execute(new SignIn(), rootDialog);
                    }
                }
                else
                    DialogHost.OpenDialogCommand.Execute(new SignIn(), rootDialog);
            }
        }

        private async void RootDialogClosed(object sender, DialogClosingEventArgs eventArgs)
        {
            DialogHost host = sender as DialogHost;
            if (host.DialogContent.GetType() == typeof(SignIn))
            {
                (DataContext as MainWindowViewModel).User = await (new UserHelper()).GetUserFromSystem();
                if ((DataContext as MainWindowViewModel).User != null)
                {
                    DialogHost.OpenDialogCommand.Execute(new Loading(), rootDialog);
                    //await ((mainContentControl.Content as ItemGrid).DataContext as ItemGridViewModel)
                    //    .FillCollection((DataContext as MainWindowViewModel).User);
                    //DialogHost.CloseDialogCommand.Execute(null, rootDialog);
                }
                else
                {
                    DialogHost.OpenDialogCommand.Execute(host.DialogContent, rootDialog);
                }
                    
                
                
                        
               
            }
        }

        private async void SignOut_Clicked(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).User = null;
            FileHandler handler = new FileHandler();
            await handler.DeleteFile(FileHandler.FileName.SitItem);
            await handler.DeleteFile(FileHandler.FileName.UserDetails);
            ((mainContentControl.Content as ItemGrid).DataContext as ItemGridViewModel).Sit_ItemsCollection = new ObservableCollection<SIT2_Item>();
            DialogHost.OpenDialogCommand.Execute(new Loading(), rootDialog);
        }
    }
}
