using MessagePack;

namespace TEAMModelOS.SDK.Extension.DataResult.JsonRpcResponse
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class ErrorModel<E>
    {
        public float code { get; set; }
        public string message { get; set; }
        public string devmsg { get; set; }
        public E data { get; set; }
    }
}
