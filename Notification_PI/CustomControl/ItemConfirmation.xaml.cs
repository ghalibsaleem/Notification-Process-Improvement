﻿using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.FileHelper;
using Notification_PI.ModelsHelper;
using Notification_PI.NetHelper;
using Notification_PI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using static Notification_PI.FileHelper.FileHandler;

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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Loading view = new Loading();
            try
            {
                string[] toMail, ccMail, bccMail;
                ItemControlViewModel model = this.DataContext as ItemControlViewModel;
                toMail = toMailBox.Text.Split(new char[] { ';' }).Where(x => x != "").ToArray();
                ccMail = ccMailBox.Text.Split(new char[] { ';' }).Where(x => x != "").ToArray();
                bccMail = bccMailBox.Text.Split(new char[] { ';' }).Where(x => x != "").ToArray();

                FileHandler handler = new FileHandler();
                string str = await handler.ReadFromInstallationSystem(FileName.Template, Extension.HTML);
                ItemControlViewModel itemModel = (ItemControlViewModel)DataContext;

                User requester = await getUser(itemModel.SitObject.RequesterName);
                User tester = await getUser(itemModel.SitObject.TestersInvolved);

                UserHelper helper = new UserHelper();
                User deployer = await helper.GetUserFromSystem();

                str = String.Format(str,
                    "Hi All,",
                    greetingText.Text,
                    DateTime.Parse(itemModel.SitObject.DeploymentWindow).Day + "/",
                    DateTime.Parse(itemModel.SitObject.DeploymentWindow).Month.ToString() + "/" + DateTime.Parse(itemModel.SitObject.DeploymentWindow).Year,
                    DateTime.Parse(itemModel.SitObject.DeploymentWindow).Hour.ToString() + "00",
                    (DateTime.Now.Month >= 4 && DateTime.Now.Month <= 10) ? "CEST" : "CET",
                    (DateTime.Parse(itemModel.SitObject.DeploymentWindow).Hour + 1).ToString() + "00",
                    itemModel.SitObject.Application,
                    itemModel.SitObject.Id,
                    itemModel.SitObject.Project,
                    requester.Email,
                    requester.Name,
                    deployer.Name,
                    deployer.Email,
                    tester.Name,
                    tester.Email,
                    (Header == "Final Mail") ? "Completed successfully." : "Updates to follow soon.",
                    deployer.Name
                    );
                

                //await DialogHost.Show(view, "gridDialogHost", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
                DialogHost.OpenDialogCommand.Execute(view, this);
                SMTPAsync smtpObj = new SMTPAsync();
                bool result = await smtpObj.SendMessage(toMail, ccMail, bccMail, "", str, new System.Net.NetworkCredential(deployer.Email, deployer.Password));
                DialogHost.CloseDialogCommand.Execute(this, view);
                if (result)
                {
                    Message msg = new Message("Mail Send Successfully");
                    await DialogHost.Show(msg, "RootDialog", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
                }
                else
                {
                    Message msg = new Message("Mail Send Failed");
                    await DialogHost.Show(msg, "RootDialog", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
                }
            }
            catch (Exception ex)
            {
                DialogHost.CloseDialogCommand.Execute(this, view);
                Message msg = new Message(ex.Message);
                //DialogHost.OpenDialogCommand.Execute(msg, this);
                await DialogHost.Show(msg, "RootDialog", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
            }
        }

        private async Task<User> getUser(string name)
        {
            FileHandler handler = new FileHandler();
            string contactsString = await handler.ReadFromInstallationSystem(FileName.Contacts, Extension.DAT);
            string[] namePart = name.Split(',');
            namePart = namePart.Where(x => x != "").ToArray();
            namePart[0] = namePart[0].Replace(" ", "");
            namePart[1] = namePart[1].Replace(" ", "");
            string[] Team = contactsString.Split(',');
            Team = Team.Where(x => x.Contains(namePart[0]) && x.Contains(namePart[1])).ToArray();
            if(Team.Length > 0)
            {
                Team[0] = Team[0].Replace("\r\n", "");
                return new User()
                {
                    Name = Team[0].Split(';')[1],
                    Email = Team[0].Split(';')[0]
                };
            }
            return new User()
            {
                Name = namePart[1] + namePart[0],
                Email = ""
            };
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
