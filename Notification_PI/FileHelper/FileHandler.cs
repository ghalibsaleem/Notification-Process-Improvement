using Notification_PI.Cipher;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.FileHelper
{
    public class FileHandler
    {

        public enum FileName{
            SitItem,
            UserDetails,
            Settings,
            Template,
            DeploymentData,
            RequesterData,
            Contacts
        }

        public enum Extension
        {
            TXT,
            HTML,
            JSON,
            DAT
        }

        public async Task<bool> WriteOnSystem(string jsonString,FileName name)
        {
            string path = Environment.CurrentDirectory +"\\" +name.ToString() + ".dat";

            await Task.Run(
                () =>
                {
                    AESHelper cipher = new AESHelper();
                    byte[] bytes = cipher.Encrypt(jsonString);
                    using (BinaryWriter bwriter = new BinaryWriter(File.CreateText(path).BaseStream, Encoding.UTF8))
                    {

                        bwriter.Write(bytes);
                    }
                }
                );
            return true;
        }

        public async Task<string> ReadFromSystem(FileName name)
        {
            String result = null;
            try
            {
                string path = Environment.CurrentDirectory +"\\" +name.ToString() + ".dat";
                using (BinaryReader breader = new BinaryReader(File.OpenText(path).BaseStream))
                {
                    await Task.Run(
                        () =>
                            {
                                byte[] bytes = breader.ReadBytes((int)breader.BaseStream.Length);
                                AESHelper cipher = new AESHelper();
                                result = cipher.Decrypt(bytes);
                            }
                        );
                    
                }
                return result;
                
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<bool> DeleteFile(FileName name)
        {
            string path = Environment.CurrentDirectory + name.ToString() + ".dat";
            if (File.Exists(path))
            {
                await Task.Run(() => { File.Delete(path); });
                return true;
            }
            return false;
        }

        public async Task<string> ReadFromInstallationSystem(FileName name,Extension ext)
        {
            String result = null;
            try
            {
                string path = Environment.CurrentDirectory +"\\" +name.ToString() + "." + ext.ToString().ToLower();
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

    }
}
