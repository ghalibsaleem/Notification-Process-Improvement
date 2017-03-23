using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.All,
        AllowMultiple = true)]
    public class SnoAttribute : Attribute
    {
        public int Sno { get;private set; }

        public SnoAttribute(int no)
        {
            Sno = no;
        }
    }
}
