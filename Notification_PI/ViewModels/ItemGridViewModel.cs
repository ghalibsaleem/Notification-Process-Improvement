using HtmlParser;
using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.Commands;
using Notification_PI.CustomControl;
using Notification_PI.FileHelper;
using Notification_PI.JSONHelper;
using Notification_PI.NetHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notification_PI.ViewModels
{
    public class ItemGridViewModel
    {
        public ItemGridViewModel()
        {
            _sit_ItemCollection = new ObservableCollection<SIT2_Item>();
        }



        public ICommand RunDialogCommand => new CommandHandler(ExecuteRunDialog);

        private ObservableCollection<SIT2_Item> _sit_ItemCollection;

        public ObservableCollection<SIT2_Item> Sit_ItemsCollection
        {
            get { return _sit_ItemCollection; }
            set { _sit_ItemCollection = value; }
        }

        public async Task FillCollection(User user)
        {
            IMAPAsync d = new IMAPAsync();
            await d.ConnectAsync("webmail.maersk.net", user.Email, user.Password, 993, true);
            await d.SetCurrentFolderAsync("Inbox");
            await d.LoadRecentMessagesAsync(915);
            
            foreach (var item in d.Messages
                .Where(x => x.Subject.Contains("SIM Application Deployment Management Dashboard"))
                .OrderByDescending(x => x.Date).ToList())
            {
                if (item.SequenceNumber == 916)
                {
                    item.LoadInfos();
                    Parser p = new Parser();
                    SIT2_Item table = p.ParseHtml(item.TextBody);
                    table.Id = item.Subject.Remove(0, item.Subject.IndexOf(" ID") + 3)
                    ;
                    _sit_ItemCollection.Add(table);
                }
            }
            await d.DisconnectAsync();
            //JSONHandler ass = new JSONHandler();
            //FileHandler hand = new FileHandler();
            
            //var a = ass.Serialize(new JSON_SIT_Model(Sit_ItemsCollection.First(), true, true));
            //await hand.WriteOnSystem(a, FileHandler.FileName.SitItem);
            //var str =await hand.ReadFromSystem(FileHandler.FileName.SitItem);
            //var asw = ass.Deserialize<JSON_SIT_Model>(str);
        }


        private async void ExecuteRunDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            
            var view = new ItemControl()
            {
                DataContext = new ItemControlViewModel()
                {
                    SitObject = o as SIT2_Item,
                    ItemProperties =new ObservableCollection<PropertyInfo>( typeof(SIT2_Item).GetProperties())
                }
            };
            
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog");

        }
    }
}
