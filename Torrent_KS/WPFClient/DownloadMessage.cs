using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WPFClient
{
    [XmlRoot(ElementName = "DownloadMessage")]
    public class DownloadMessage
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
        public string index { get; set; } // index of resource ip
        public string numOfResorces { get; set; }
        public string fileSize { get; set; }
    }
}
