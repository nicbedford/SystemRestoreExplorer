using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SystemRestoreExplorer
{
    class UpdateInfo
    {
        public UpdateInfo(XElement element)
        {
            XElement versionElement = element.Descendants().First();
            XElement urlElement = element.Descendants().Last();
            Version = new Version(versionElement.Value);
            Url = urlElement.Value;
        }

        public Version Version { get; private set; }
        public string Url { get; private set; }
    }
}
