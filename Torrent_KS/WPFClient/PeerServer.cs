using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;

namespace WPFClient
{
    public class PeerServer
    {
        public string IP { get; set; }
        public int port { get; set; }
        public string Path { get; set; }
        public Thread t1 { get; set; }
        public bool endServer { get; set; }


        // Thread is needed for prevent congestion.
        public void handelFileReq(Object arg)
        {
            Socket s = (Socket)arg;
            //string path = (string)arg2;
            string msg;
            int i;
            ArrayList<string> list = new ArrayList<string>();

            byte[] bName = new byte[s.SendBufferSize];
            int reqSize = s.Receive(bName);

            string name = "";

            for (i = 0; i < reqSize; i++)
                name += Convert.ToChar(bName[i]);

            XmlReader xReader = XmlReader.Create(new StringReader(Path) + "//" + name);
            while (xReader.Read())
            {
                switch (xReader.NodeType)
                {
                    case XmlNodeType.Element:
                        list.Add("<" + xReader.Name + ">");
                        break;
                    case XmlNodeType.Text:
                        list.Add(xReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        list.Add("");
                        break;
                }
            }
            string msgFile = SerializeAnObject(list);
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] filesb = asen.GetBytes(msgFile);
            int size = filesb.Length;
            byte[] msgSize = asen.GetBytes("" + size);
            s.Send(msgSize);
            s.Send(filesb);
            s.Close();
        }

        // Listen to request from other clients for download my files.
        // Thread is needed for prevent the UI from being stuck.
        public void listenToRequest(Object arg)
        {

            TcpListener myList = (TcpListener)arg;
            //string path = (string)arg2;
            myList.Start();
            try
            {
                while (true)
                {
                    Socket s = myList.AcceptSocket();
                    Thread t2 = new Thread(handelFileReq);
                    t2.Start(s);
                }
            }
            catch (ThreadAbortException e)
            {
                MessageBox.Show("Error..... " + e.StackTrace);
            }
            finally
            {
                myList.Stop();
                MessageBox.Show("Bye Bye!") ;
            }
        }

        // Constractor.
        /* public PeerServer(string ipAddress, int port, string path)
         {
             this.IP = ipAddress;
             this.port = port;
             Path = path;
             endServer = false;

             IPAddress ipAd = IPAddress.Parse(IP); //use local m/c IP address, and use the same in the client
                                                   //IPAddress ipAd = IPAddress.Parse("192.168.56.1");
             TcpListener myList = new TcpListener(ipAd, port);
             t1 = new Thread(listenToRequest);
             t1.Start(myList);
         }*/

        public void initServer()
        {
            endServer = false;

            IPAddress ipAd = IPAddress.Parse(IP); //use local m/c IP address, and use the same in the client
                                                  //IPAddress ipAd = IPAddress.Parse("192.168.56.1");
            TcpListener myList = new TcpListener(ipAd, port);
            t1 = new Thread(listenToRequest);
            t1.Start(myList);
        }

        public static string SerializeAnObject(object AnObject)
        {
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }

        public void stopServer()
        {
            endServer = true;
        }


    }
}
