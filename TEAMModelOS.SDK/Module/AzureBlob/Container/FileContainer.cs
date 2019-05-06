namespace TEAMModelOS.SDK.Module.AzureBlob.Container
{
    public class FileContainer : IBlobContainer
    {
        public string Container { get; private set; } = "filecontainer";

        public string GetContainer()
        {
            return Container;
        }
    }
}
