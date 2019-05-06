using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Context.Attributes.MySQL
{
    public class TextAttribute : Attribute
    {
        public TextAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
