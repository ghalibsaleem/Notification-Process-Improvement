using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.JSONHelper
{
    public class JSONHandler
    {
        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        
        public T Deserialize<T>(string jsonSting)
        {
            return JsonConvert.DeserializeObject<T>(jsonSting);
        }
        
    }
}
