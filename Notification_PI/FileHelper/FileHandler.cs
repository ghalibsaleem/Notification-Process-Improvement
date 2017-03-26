using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.FileHelper
{
    public class FileHandler
    {

        public enum FileName{
            SitItem,
            UserDetails,
            Settings
        }
        public async Task<bool> WriteOnSystem(string jsonString,FileName name)
        {
            string path = Environment.CurrentDirectory +"\\" +name.ToString() + ".json";
            
            using (StreamWriter writer = File.CreateText(path))
            {
                await writer.WriteAsync(jsonString);
                
                
            }
            File.Encrypt(path);
            return true;
        }

        public async Task<string> ReadFromSystem(FileName name)
        {
            String result = null;
            try
            {
                string path = Environment.CurrentDirectory +"\\" +name.ToString() + ".json";
                File.Decrypt(path);
                using (StreamReader reader = File.OpenText(path))
                {

                    result = await reader.ReadToEndAsync();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<bool> DeleteFile(FileName name)
        {
            string path = Environment.CurrentDirectory + name.ToString() + ".json";
            if (File.Exists(path))
            {
                await Task.Run(() => { File.Delete(path); });
                return true;
            }
            
            return false;
        }
    }
}
