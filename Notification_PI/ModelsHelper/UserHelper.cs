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
            var ObjEWSClient = System.Windows.Application.Current.Properties["ObjEWSClient"] as EWSClient;
            if(ObjEWSClient == null)
                ObjEWSClient = new EWSClient();
            
            if (ObjEWSClient.IsConnected)
                return true;
            
            bool result = await ObjEWSClient.ConnectAsync(user.Email, user.Password);
            System.Windows.Application.Current.Properties["ObjEWSClient"] = ObjEWSClient;
            return result;
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
