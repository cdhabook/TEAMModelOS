using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Context.Attributes.Azure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableSpaceAttribute :Attribute
    {
        public string Name { get; set; }
    }
}
