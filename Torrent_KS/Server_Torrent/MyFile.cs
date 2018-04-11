using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFClient
{
    public class MyFile
    {
        // file data class - to save in DB.
        public string fileName { get; set; }
        public int size { get; set; }
        public string IP { get; set; }
        public int port { get; set; }
        public string path { get; set; } 
    }

}
