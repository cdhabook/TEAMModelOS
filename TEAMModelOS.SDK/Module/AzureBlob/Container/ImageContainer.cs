using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Module.AzureBlob.Container
{
    public class ImageContainer : IBlobContainer
    {
        public string Container { get; private set; } = "imagecontainer";

        public string GetContainer()
        {
            return Container;
        }
    }
}
