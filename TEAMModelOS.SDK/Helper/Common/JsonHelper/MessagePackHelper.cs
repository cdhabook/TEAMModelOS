using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Helper.Common.JsonHelper
{
    public class MessagePackHelper
    {
        /// <summary>
        /// Json字符串转Byte
        /// </summary>
        /// <param name="json"></param>
        /// <returns>byte[]</returns>
        public static byte[] JsonToByte(string json)
        {
            return LZ4MessagePackSerializer.FromJson(json);
        }

        /// <summary>
        /// Json字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns>T</returns>
        public static T JsonToObject<T>(string json)
        {
            Type type = typeof(T);
            return (T)LZ4MessagePackSerializer.NonGeneric.Deserialize(type, JsonToByte(json));
        }

        /// <summary>
        /// 对象转Byte
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>byte[]</returns>
        public static byte[] ObjectToByte(Object obj)
        {
            return LZ4MessagePackSerializer.Serialize(obj);
        }

        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns>
        public static string ObjectToJson(Object obj)
        {
            return LZ4MessagePackSerializer.ToJson(ObjectToByte(obj));
        }

        /// <summary>
        /// byte转Json
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static string ByteToJson(byte[] bt)
        {
            return LZ4MessagePackSerializer.ToJson(bt);
        }

        /// <summary>
        /// byte转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static T ByteToObject<T>(byte[] bt)
        {
            Type type = typeof(T);
            return (T)LZ4MessagePackSerializer.NonGeneric.Deserialize(type, bt);
        }
    }
}
