using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Models;
using Notification_PI.CustomControl;
using MaterialDesignThemes.Wpf;
using Notification_PI.JSONHelper;
using Notification_PI.FileHelper;
using Notification_PI.NetHelper;
using HtmlParser;
using D.Net.EmailInterfaces;

namespace Notification_PI.ModelsHelper
{
    class ItemHelper
    {
        public async Task<ObservableCollection<SIT2_Item>> GetItems(User user)
        {
            try{
                List<SIT2_Item> list1 = await GetItemsFromSystem();
                list1 = list1.OrderByDescending(a => a.Id).ToList();
                int lastSeq = 0;

                DateTime lastDate = DateTime.Now.AddDays(-50);

                FileHandler fHandler = new FileHandler();
                var jsonString = await fHandler.ReadFromSystem(FileHandler.FileName.Settings);
                if (jsonString != null)
                {
                    JSONHandler jHandler = new JSONHandler();
                    Settings setting =  jHandler.Deserialize<Settings>(jsonString);
                    lastSeq = setting.LastSeq;
                    lastDate = setting.LastDate;
                }

                List<SIT2_Item> list2 = await GetItemsFromMail(lastDate, user);
                list2 = list2.Concat(list1).ToList();
                list2 = list2.GroupBy(x => x.Id).Select(x => x.First()).ToList();

                list2 = list2.Where(x =>
                    DateTime.Parse(x.DeploymentWindow).Date >= DateTime.Now.Date
                ).ToList();

                await WriteItemToSystem(list2);
                return new ObservableCollection<SIT2_Item>(list2);
            }catch(Exception ex){
                DialogHost.CloseDialogCommand.Execute(this, null);
                Message msg = new Message(ex.ToString());
                await DialogHost.Show(msg, "RootDialog", gridDialogHost_DialogOpened, gridDialogHost_DialogClosing);
            }
            return new ObservableCollection<SIT2_Item>();
        }


        private void gridDialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }

        private void gridDialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            eventArgs.Handled = true;
        }
        
        public async Task<List<SIT2_Item>> GetItemsFromMail(DateTime lastDate,User user)
        {
            List<SIT2_Item> list = new List<SIT2_Item>();
            IMAPAsync d = new IMAPAsync();
            await d.ConnectAsync("40.103.6.22", user.Email, user.Password, 993, true);
            await d.SetCurrentFolderAsync("Inbox");
            await d.LoadMessagesWithDateFilterAsync(lastDate);

            
            List<IEmail> Messages = d.Messages
                .Where(x => x.Subject.Contains("Alert Notification PI - Item ID") && x.From.Contains("noreply@maersk.com"))
                .OrderByDescending(x => x.Date).ToList();
            await Task.Run(() =>
            {
                foreach (var item in Messages)
                {

                    item.LoadInfos();
                    Parser p = new Parser();
                    if (item.TextBody == null)
                    {
                        continue;
                    }
                    SIT2_Item table = p.ParseHtml(item.TextBody);
                    if (table == null)
                        continue;
                    table.Id = item.Subject.Remove(0, item.Subject.IndexOf(" ID") + 3)
                    ;
                    list.Add(table);
                }
            }
            );
            
            
            if (d.Messages.Count > 0)
            {
                FileHandler fHandler = new FileHandler();
                Settings setting;
                JSONHandler jHandler = new JSONHandler();
                var jsonString = await fHandler.ReadFromSystem(FileHandler.FileName.Settings);
                if (jsonString != null)
                {
                    setting = jHandler.Deserialize<Settings>(jsonString);
                    setting.LastSeq = Messages.First().SequenceNumber;
                    setting.LastDate = Messages.First().Date;
                }
                else
                {
                    setting = new Settings();
                    setting.LastSeq = Messages.First().SequenceNumber;
                    setting.LastDate = Messages.First().Date;
                }
                jsonString = jHandler.Serialize<Settings>(setting);
                await fHandler.WriteOnSystem(jsonString, FileHandler.FileName.Settings);
            }
            
            await d.DisconnectAsync();
            return list;
        }

        public async Task<List<SIT2_Item>> GetItemsFromSystem()
        {
            FileHandler fHandler = new FileHandler();
            var jsonString =await fHandler.ReadFromSystem(FileHandler.FileName.SitItem);
            if(jsonString!= null)
            {
                JSONHandler jHandler = new JSONHandler();
                return jHandler.Deserialize<List<SIT2_Item>>(jsonString);
            }
            return new List<SIT2_Item>();
        }


        public async Task<bool> WriteItemToSystem(List<SIT2_Item> list)
        {
            JSONHandler jhandler = new JSONHandler();
            var jsonString = jhandler.Serialize<List<SIT2_Item>>(list); 

            FileHandler fhandler = new FileHandler();
            return await fhandler.WriteOnSystem(jsonString,FileHandler.FileName.SitItem);
        }

    }
}
