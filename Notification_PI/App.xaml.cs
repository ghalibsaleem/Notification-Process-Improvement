using Notification_PI.NetHelper;
using System.Windows;

namespace Notification_PI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private EWSClient _ewsClient;

        public EWSClient ObjEWSClient
        {
            get { return _ewsClient; }
            set { _ewsClient = value; }
        }

    }
}
