﻿using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.ModelsHelper;
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
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private async void SignInDialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            User user = new User();

            user.Email = Email.Text;
            user.Password = Password.Password;
            UserHelper helper = new UserHelper();
            if (await helper.CheckUser(user))
            {
                if (await helper.WriteUserToSystem(user))
                {

                    DialogHost sign = eventArgs.Source as DialogHost;
                    
                    DialogHost.CloseDialogCommand.Execute(null, sign);
                    DialogHost.CloseDialogCommand.Execute(sender,null);
                    //MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(sender, null);
                }
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, eventArgs.Source as DialogHost);
            }
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
