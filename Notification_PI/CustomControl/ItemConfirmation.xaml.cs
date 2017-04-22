﻿using MaterialDesignThemes.Wpf;
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
            string[] toMail,ccMail,bccMail;
            ItemControlViewModel model = this.DataContext as ItemControlViewModel;
            toMail = toMailBox.Text.Split(new char[] { ';' }).Where(x => x!="").ToArray();
            ccMail = ccMailBox.Text.Split(new char[] { ';' }).Where(x => x != "").ToArray();
            bccMail = bccMailBox.Text.Split(new char[] { ';' }).Where(x => x != "").ToArray();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Notification_PI." + "MailTemplate.txt";

            string resource = null;
            var ds = assembly.GetManifestResourceNames();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    resource = reader.ReadToEnd();
                }
            }
            SMTPAsync smtpObj = new SMTPAsync();
            bool result = await smtpObj.SendMessage(toMail,ccMail,bccMail,"","",new System.Net.NetworkCredential("rajat.sharma@maersk.com","Mar@2017"));
        }
    }
}
