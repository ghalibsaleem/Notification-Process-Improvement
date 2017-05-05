using Models;
using Notification_PI.FileHelper;
using Notification_PI.JSONHelper;
using Notification_PI.NetHelper;
using System.Threading.Tasks;

namespace Notification_PI.ModelsHelper
{
    class UserHelper
    {
        public async Task<User> GetUserFromSystem()
        {
            
            FileHandler fHandler = new FileHandler();
            var jsonString = await fHandler.ReadFromSystem(FileHandler.FileName.UserDetails);
            if (jsonString == null)
                return null;
            JSONHandler jHandler = new JSONHandler();
            User user = jHandler.Deserialize<User>(jsonString);
            return user;
        } 

        public async Task<bool> CheckUser(User user)
        {
            IMAPAsync imap = new IMAPAsync();
            await imap.ConnectAsync("webmail.maersk.net", user.Email, user.Password, 993, true);
            return imap.IsConnected;
        }

        public async Task<bool> WriteUserToSystem(User user)
        {
            JSONHandler jHandler = new JSONHandler();
            string jsonString =  jHandler.Serialize<User>(user);
            FileHandler fHandler = new FileHandler();
            return await fHandler.WriteOnSystem(jsonString, FileHandler.FileName.UserDetails);
        }
    }
}
