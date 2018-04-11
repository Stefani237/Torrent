using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WPFClient
{
    class SaveConfigFile
    {
        public static void SaveData(object obj, string filename)
        {
            // write to configuration file
            XmlSerializer sr = new XmlSerializer(obj.GetType()); // convert object to string
            TextWriter writer = new StreamWriter(filename); // create new stream writer
            sr.Serialize(writer, obj); // write it to file
            writer.Close();
        }
    }
}
