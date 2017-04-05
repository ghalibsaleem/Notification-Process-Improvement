using D.Net.EmailClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.NetHelper
{
    public class IMAPAsync : IMAP_Wrapper
    {

        public async Task ConnectAsync(string server,string user,string password,int port,bool useSSL)
        {
            await Task.Run(() =>
            {
                try
                {
                    Connect(server, user, password, port, useSSL);
                }
                catch (Exception)
                {

                    
                }
                
                
            });
        }

        public async Task DisconnectAsync()
        {
            await Task.Run(() =>
            {
                if (IsConnected)
                    Disconnect();

            });
        }

        public async Task LoadMessagesAsync()
        {
            await Task.Run(() =>
            {
                if (IsConnected)
                    LoadMessages();

            });
        }

        public async Task LoadMessagesAsync(string start,string end)
        {
            await Task.Run(() =>
            {
                if (IsConnected)
                    LoadMessages(start,end);

            });
        }


        public async Task LoadRecentMessagesAsync(int LastSeqNo)
        {
            await Task.Run(() =>
            {
                //if (IsConnected)
                    
                    //LoadRecentMessages(LastSeqNo);
                LoadMessagesWithFilter();
            });
        }

        public async Task SetCurrentFolderAsync(string folder)
        {
            await Task.Run(() =>
            {
                if(IsConnected)
                    SetCurrentFolder(folder);
            });
        }
    }
}
