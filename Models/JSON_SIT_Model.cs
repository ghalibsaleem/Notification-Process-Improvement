using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JSON_SIT_Model
    {
        public JSON_SIT_Model(SIT2_Item item, bool initial, bool final)
        {
            Item = item;
            Initial = initial;
            Final = final;
        }

        public SIT2_Item Item { get; set; }

        public bool Initial { get; set; }

        public bool Final { get; set; }
    }
}
