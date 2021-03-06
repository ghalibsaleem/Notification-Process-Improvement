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

namespace Notification_PI.ModelsHelper
{
    class ItemHelper
    {
        public EWSClient ObjEWSClient { get; set; }
        public async Task<ObservableCollection<SIT2_Item>> GetItems(User user)
        {
            try{
                if (ObjEWSClient == null)
                    ObjEWSClient = System.Windows.Application.Current.Properties["ObjEWSClient"] as EWSClient;

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

                //list2 = list2.Where(x =>
                //{
                //    DateTime date;
                //    if(DateTime.TryParse(x.DeploymentWindow,out date))
                //    {
                //        if (date.Date >= DateTime.Now.Date)
                //            return true;
                //        else
                //            return false;
                //    }
                //    return false;
                //}).ToList();

                
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
        
        private async Task<List<SIT2_Item>> GetItemsFromMail(DateTime lastDate,User user)
        {
            List<SIT2_Item> list = new List<SIT2_Item>();
            
            List <EmailMessageEntity> messages =await ObjEWSClient.ReadMailAsync(lastDate);
            await Task.Run(() =>
            {
                foreach (var item in messages)
                {
                    Parser parser = new Parser();
                    SIT2_Item table = parser.ParseHtml(item.Body);
                    if (table == null)
                        continue;
                    table.Id = item.Subject.Remove(0, item.Subject.IndexOf(" ID") + 3);
                    list.Add(table);
                }
            });
            if (messages.Count > 0)
            {
                FileHandler fHandler = new FileHandler();
                Settings setting;
                JSONHandler jHandler = new JSONHandler();
                var jsonString = await fHandler.ReadFromSystem(FileHandler.FileName.Settings);
                if (jsonString != null)
                {
                    setting = jHandler.Deserialize<Settings>(jsonString);

                }
                else
                {
                    setting = new Settings();
                }
                setting.LastDate = DateTime.UtcNow;
                jsonString = jHandler.Serialize<Settings>(setting);
                await fHandler.WriteOnSystem(jsonString, FileHandler.FileName.Settings);
            }

            return list;
        }

        private async Task<List<SIT2_Item>> GetItemsFromSystem()
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
