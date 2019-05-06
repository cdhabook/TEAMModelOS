using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Extension.MessagePush.Model
{
    [MessagePackObject(keyAsPropertyName: true)]
    public  class SendCloudResponse
    {
        public string message { get; set; }
        public Info info { get; set; }
        public bool result { get; set; }
        public int  statusCode { get; set; }
    }
    [MessagePackObject(keyAsPropertyName: true)]
    public class Info
    {
        public int successCount { get; set; }
        public string[] smsIds { get; set; }
        public int failedCount { get; set; }
        public Item[] items { get; set; }
    }
    [MessagePackObject(keyAsPropertyName: true)]
    public class Item
    {
        public string phone { get; set; }
        public string message { get; set; }
    }
}
