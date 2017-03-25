using D.Net.EmailClient;
using HtmlParser;
using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.Commands;
using Notification_PI.CustomControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public void FillCollection()
        {
            IMAP_Wrapper d = new IMAP_Wrapper();
            d.Connect("webmail.maersk.net", @"rajat.sharma@maersk.com", "Mar@2017", 993, true);
            d.SetCurrentFolder("Inbox");
            d.LoadRecentMessages(915);
            
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
            //var result = await DialogHost.Show(view, "RootDialog");

        }
    }
}
