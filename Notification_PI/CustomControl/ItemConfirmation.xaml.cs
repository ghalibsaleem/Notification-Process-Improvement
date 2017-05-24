using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.FileHelper;
using Notification_PI.ModelsHelper;
using Notification_PI.NetHelper;
using Notification_PI.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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


        public async override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Header == "Final Mail")
            {
                greetingText.Text = "Deployment completed  for change id " + ((ItemControlViewModel)DataContext).SitObject.Id + ". Please proceed with smoke testing and confirm status once done.";
            }
            else
            {
                greetingText.Text = "Please find below the notification for Change ID-" + ((ItemControlViewModel)DataContext).SitObject.Id + ".";
            }
            toMailBox.Text = "XDSIT2ChangeConflictForum@maersk.com;";
            User requester = await getUser(((ItemControlViewModel)DataContext).SitObject.RequesterName);
            User tester = await getUser(((ItemControlViewModel)DataContext).SitObject.TestersInvolved);
            ccMailBox.Text = "XDSIT2ReleaseDeployment@MAERSK.COM;";
            if (!tester.Email.Contains(requester.Email))
            {
                ccMailBox.Text += requester.Email + ((requester.Email != "") ? ";" : "");
            }
            ccMailBox.Text += tester.Email
                    + ((tester.Email != "") ? ";" : "");

            
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
                

                DialogHost.OpenDialogCommand.Execute(view, this);
                
                SMTPAsync smtpObj = new SMTPAsync();
                
                string subject = itemModel.SitObject.Id + " Notification " + itemModel.SitObject.Project;
                
                bool result = await smtpObj.SendMessage(toMail, ccMail, bccMail, subject, str, deployer);
                DialogHost.CloseDialogCommand.Execute(this, view);
                if (result)
                {
                    Message msg = new Message("Mail Send Successfully");
                    await DialogHost.Show(msg, "RootDialog", GridDialogHost_DialogOpened, GridDialogHost_DialogClosing);
                }
                else
                {
                    Message msg = new Message("Mail Send Failed");
                    await DialogHost.Show(msg, "RootDialog", GridDialogHost_DialogOpened, GridDialogHost_DialogClosing);
                }
            }
            catch (Exception ex)
            {
                DialogHost.CloseDialogCommand.Execute(this, view);
                Message msg = new Message(ex.ToString());
                await DialogHost.Show(msg, "RootDialog", GridDialogHost_DialogOpened, GridDialogHost_DialogClosing);
            }
        }

        private async Task<User> getUser(string name)
        {
            if (name.Contains(";"))
                return await getMultipleUsers(name);

            string[] namePart;
            if (!name.Contains(','))
            {
                namePart = name.Split(' ');
            }
            else
                namePart = name.Split(',');
            namePart = namePart.Where(x => x != "").ToArray();
            namePart[0] = namePart[0].Replace(" ", "");
            if(namePart.Length > 1)
                namePart[1] = namePart[1].Replace(" ", "");

            try
            {
                FileHandler handler = new FileHandler();
                string contactsString = await handler.ReadFromInstallationSystem(FileName.Contacts, Extension.DAT);

                string[] Team = contactsString.Split(',');
                Team = Team.Where(x => x.Contains(namePart[0]) && x.Contains(namePart[1])).ToArray();
                if (Team.Length > 0)
                {
                    Team[0] = Team[0].Replace("\r\n", "");
                    return new User()
                    {
                        Name = Team[0].Split(';')[1],
                        Email = Team[0].Split(';')[0]
                    };
                }
            }
            catch (Exception)
            {
            }
            return new User()
            {
                Name = (namePart.Length > 1 ? namePart[1] + " ":"") + namePart[0],
                Email = ""
            };
        }

        private async Task<User> getMultipleUsers(string name)
        {
            FileHandler handler = new FileHandler();
            string contactsString = await handler.ReadFromInstallationSystem(FileName.Contacts, Extension.DAT);

            User user = new User()
            {
                Name = "",
                Email = ""
            };
            if (contactsString == null || contactsString == "")
                return user;
            string[] Team = contactsString.Split(',');
            string[] names = name.Split(';');
            
            foreach (var item in names)
            {
                string[] namePart;
                if (!name.Contains(','))
                {
                    namePart = name.Split(' ');
                }
                else
                    namePart = name.Split(',');
                namePart = namePart.Where(x => x != "").ToArray();
                namePart[0] = namePart[0].Replace(" ", "");
                if (namePart.Length > 1)
                    namePart[1] = namePart[1].Replace(" ", "");

                string[] tempTeam = Team.Where(x => x.Contains(namePart[0]) && x.Contains(namePart[1])).ToArray();
                if (tempTeam.Length > 0)
                {
                    tempTeam[0] = tempTeam[0].Replace("\r\n", "");
                    if(user.Name == "")
                    {
                        user.Name = tempTeam[0].Split(';')[1];
                        user.Email = tempTeam[0].Split(';')[0];
                    }
                    else
                    {
                        user.Name += ", " + tempTeam[0].Split(';')[1];
                        user.Email += "; " + tempTeam[0].Split(';')[0];
                    }
                }
                else
                {
                    if (user.Name == "")
                        user.Name += (namePart.Length > 1 ? namePart[1] + " " : "") + namePart[0];
                    else
                        user.Name += ", " + (namePart.Length > 1 ? namePart[1] + " " : "") + namePart[0];
                }
            }
            return user;
        }

        private void GridDialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }

        private void GridDialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }
    }
}
