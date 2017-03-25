using D.Net.EmailClient;
using HtmlParser;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.ViewModels
{
    public class ItemGridViewModel
    {
        public ItemGridViewModel()
        {
            _sit_ItemCollection = new ObservableCollection<SIT2_Item>();
        }

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
                    _sit_ItemCollection.Add(table);
                }
            }

        }

    }
}
