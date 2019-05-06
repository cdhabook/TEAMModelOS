using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TEAMModelOS.SDK.Extension.JsonPath
{
    public sealed class JsonNetValueSystem : IJsonPathValueSystem
    {
        public bool HasMember(object value, string member)
        {
            if (value is JObject)
            {
                return (value as JObject).Properties().Any(property => property.Name == member);
            }

            if (value is JArray)
            {
                var index = ParseInt(member, -1);
                return index >= 0 && index < (value as JArray).Count;
            }
            return false;
        }

        public object GetMemberValue(object value, string member)
        {
            if (value is JObject)
            {
                var memberValue = (value as JObject)[member];
                return memberValue;
            }
            if (value is JArray)
            {
                var index = ParseInt(member, -1);
                return (value as JArray)[index];
            }
            return null;
        }

        public IEnumerable GetMembers(object value)
        {
            var jobject = value as JObject;
            return jobject?.Properties().Select(property => property.Name);
        }

        public bool IsObject(object value) => value is JObject;

        public bool IsArray(object value) => value is JArray;

        public bool IsPrimitive(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return !(value is JObject) && !(value is JArray);
        }

        int ParseInt(string s, int defaultValue) => int.TryParse(s, out int result) ? result : defaultValue;
    }
}
