using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.JsonPath
{
    public interface IJsonPathValueSystem
    {
        bool HasMember(object value, string member);
        object GetMemberValue(object value, string member);
        IEnumerable GetMembers(object value);
        bool IsObject(object value);
        bool IsArray(object value);
        bool IsPrimitive(object value);
    }
}
