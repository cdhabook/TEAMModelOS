﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TEAMModelOS.SDK.Module.GrpcServer.Extensions
{
    public class ProtobufExtensions
    {
        public static byte[] Serialize<T>(T input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize<T>((Stream)memoryStream, input);
                return memoryStream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
                return Serializer.Deserialize<T>((Stream)memoryStream);
        }
    }
}
