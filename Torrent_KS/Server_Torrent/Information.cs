using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFClient
{
    public class Information
    {
        // clients data class - to save in the config file.
        private string name;
        private string password;
        private string ipAddress;
        private int port;
        private string path;
        
        public string UserName
        {
            get { return name; }
            set { name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

    }
}
