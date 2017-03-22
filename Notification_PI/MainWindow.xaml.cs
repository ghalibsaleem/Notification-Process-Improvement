using D.Net.EmailClient;
using Notification_PI.CustomControl;
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

namespace Notification_PI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainContentControl.Content = new ItemGrid();
            /*
            IMAP_Wrapper d = new IMAP_Wrapper();
            d.Connect("webmail.maersk.net", @"rajat.sharma@maersk.com", "Mar@2017", 993, true);
            d.SetCurrentFolder("Inbox");
            d.LoadMessages();
           
            foreach (var item in d.Messages.OrderByDescending(x => x.Date).ToList())
            {
                if(item.SequenceNumber == 916)
                {
                    item.LoadInfos();
                    Parser p = new Parser();
                    Dictionary<string, string> table = p.ParseHtml(item.TextBody);
                }
            }
            */

        }
    }
}
