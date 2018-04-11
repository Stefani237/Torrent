using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using WPFClient;
using DBoperations;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try {
                string msg = "";
                IPAddress ipAd = IPAddress.Parse("172.20.16.136"); //use local m/c IP address, and use the same in the client
                //IPAddress ipAd = IPAddress.Parse("192.168.56.1");
                TcpListener myList = new TcpListener(ipAd,8005);

			    myList.Start();

			    Console.WriteLine("The server is running at port 8005...");	
			    Console.WriteLine("The local End point is  :" + myList.LocalEndpoint );
			    Console.WriteLine("Waiting for a connection.....");

              
                    Socket s = myList.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                    byte[] b = new byte[s.SendBufferSize];
                    int k = s.Receive(b);
                    Console.WriteLine("Recieved...");
                    for (int i = 0; i < k; i++)
                        msg += Convert.ToChar(b[i]);

                    Console.Write(msg);
                    Information info = new Information();
                    Object obj = DeSerializeAnObject(msg, info.GetType());
                    Information inf = (Information)obj;
                    Console.WriteLine("-------------- " + inf.UserName + " " + inf.Password + " -------------");
                    
                    DBoperations.Users data = new DBoperations.Users();
                    int result = data.isUserExist(inf.UserName, inf.Password);
                    if (result > 0)
                    {
                        Console.WriteLine("User found!! :)");
                    }
                    else
                    {
                        Console.WriteLine("User Not found!! :(");
                    }

                    //  ASCIIEncoding asen = new ASCIIEncoding();
                    //  s.Send(asen.GetBytes("The string was recieved by the server."));
                    //  Console.WriteLine("\\nSent Acknowledgement");


                    s.Close();
                
			    myList.Stop();

		    }

		    catch (Exception e) {
			    Console.WriteLine("Error..... " + e.StackTrace);
		    }	
	    }
        
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
    }
}
