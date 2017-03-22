﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.All,
        AllowMultiple = true)]
    public class NameAttribute : Attribute
    {
        
        public string Name { get;private set; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
