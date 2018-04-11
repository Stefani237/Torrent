using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using WPFClient;
using System.Net.Sockets;
using System.Net;

namespace ClientTry
{
    class Program
    {
        public static string SerializeAnObject(object AnObject)
        {
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }

        public static Object DeSerializeAnObject(string XmlOfAnObject, Type ObjectType)
        {
            StringReader StrReader = new StringReader(XmlOfAnObject);
            XmlSerializer Xml_Serializer = new XmlSerializer(ObjectType);
            XmlTextReader XmlReader = new XmlTextReader(StrReader);
            try
            {
                Object AnObject = Xml_Serializer.Deserialize(XmlReader);
                return AnObject;
            }
            finally
            {
                XmlReader.Close();
                StrReader.Close();
            }
        }

        public static void ServerMessageHandler(Information message)
        {
            TcpClient tcpclnt = null;
            try
            {
                tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");
                // IPAddress ipAd=IPAddress.Parse("2001:0:9d38:6abd:109f:29f:c124:8c2e");
               // tcpclnt.Connect("172.20.16.136", 8005); // use the ipaddress as in the server program
                tcpclnt.Connect("192.168.56.1", 8005);
                Console.WriteLine("Connected");

                String str = SerializeAnObject(message);
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                ////////////////////////////////////

                //byte[] bb = new byte[100];
                //int k = stm.Read(bb, 0, 100);

                // for (int i = 0; i < k; i++)
                //     Console.Write(Convert.ToChar(bb[i]));

                
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            finally
            {
                tcpclnt.Close();
            }
        }
        static void Main(string[] args)
        {
            Information info = new Information();
            info.UserName = "keren";
            info.Password = "1234";
            info.Path="C:\\Users\\mpi\\Desktop\\folder_TorrentSK";
            info.Port=8006;
            info.IpAddress = "172.20.16.136";
            ServerMessageHandler(info);
        }
    }
}
