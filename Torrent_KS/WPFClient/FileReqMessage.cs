using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WPFClient
{
    [XmlRoot(ElementName = "FileReqMessage")]
    public class FileReqMessage
    {
        public string fileName { get; set; }
        public string myIP { get; set; }
        public string resourceIndex { get; set; }
        public string numOfResources { get; set; }
    }
}
