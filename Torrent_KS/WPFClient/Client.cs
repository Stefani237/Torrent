using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net.Sockets;
using System.Data;
using System.Windows;
using System.Net;

namespace WPFClient
{
    class Client
    {
        public byte res { get; set; }
        public string answer { get; set; }
        private DataSet files { get; set; }
        private TcpClient tcpclnt;
        private Stream stm;
        private string req;

        public string SerializeAnObject(object AnObject)
        {
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }
        public Object DeSerializeAnObject(string XmlOfAnObject, Type ObjectType)
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
        public void ServerMessageHandler(Information message)
        {
            try
            {
                connectToServer();
                String str = SerializeAnObject(message); // convert information object to string
                req = "Log In";

                ASCIIEncoding asen = new ASCIIEncoding(); // encoding object
                byte[] bReq = asen.GetBytes(req); // convert the req type to bytes
                byte[] ba = asen.GetBytes(str); // convert msg to bytes

                // write to stream
                stm.Write(bReq, 0, bReq.Length);
                stm.Write(ba, 0, ba.Length);

                // get answer from server:
                byte[] bb = new byte[ba.Length];
                int k = stm.Read(bb, 0, ba.Length);
                res = bb[0]; // user exist / not exist

                tcpclnt.Close(); // close tcp connection
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        public void connectToServer()
        {
            tcpclnt = new TcpClient(); // create tcp connection
            tcpclnt.Connect("10.20.225.71", 8005); // server ip and connection port
            stm = tcpclnt.GetStream();
        }

        public void insertNewFile(MyFile myfile)
        {
            answer = "";
            try
            {
                connectToServer();
                req = "Insert";

                ASCIIEncoding asen = new ASCIIEncoding(); // encode req
                byte[] bReq = asen.GetBytes(req); // convert to bytes
                stm.Write(bReq, 0, bReq.Length); // send req type

                String str = SerializeAnObject(myfile); // convert from MyFile obj to string xml
                byte[] bNewFile = asen.GetBytes(str);
                stm.Write(bNewFile, 0, bNewFile.Length); // send file

                byte[] ans = new byte[100]; // get answer from server if the file is already exists from that ip source
                int k = stm.Read(ans, 0, ans.Length);
                for (int i = 0; i < k; i++)
                    answer += Convert.ToChar(ans[i]); // convert to string. answer is a class member and is set with the ans from server
                Array.Clear(ans, 0, ans.Length);
           
                tcpclnt.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error..... " + e.StackTrace);
            }
        }

        public DataSet GetFilesFromServer()
        {
            connectToServer();

            req = "Files";
            // get filesList buffer size
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] bReq = asen.GetBytes(req);
            stm.Write(bReq, 0, bReq.Length);
            byte[] binSize = new byte[50];
            stm.Read(binSize, 0, binSize.Length);

            //get filesList
            int size = int.Parse(Encoding.ASCII.GetString(binSize));
            byte[] binFiles = new byte[size];
            stm.Read(binFiles, 0, binFiles.Length);
            string filesStr = Encoding.ASCII.GetString(binFiles);
            Console.WriteLine("client files:");
            Console.WriteLine(filesStr);
            DataSet files = new DataSet();
            Object obj = DeSerializeAnObject(filesStr, files.GetType());
            files = (DataSet)obj;
            return files;
        }

        public DataSet getSearchResFromServer(string name, string type)
        {
            connectToServer();

            req = type;
            // get filesList buffer size
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] bReq = asen.GetBytes(req);
            stm.Write(bReq, 0, bReq.Length);
            byte[] bName = asen.GetBytes(name);
            stm.Write(bName, 0, bName.Length);
            byte[] binSize = new byte[50];
            stm.Read(binSize, 0, binSize.Length);

            //get filesList
            int size = int.Parse(Encoding.ASCII.GetString(binSize));
            byte[] binFiles = new byte[size];
            stm.Read(binFiles, 0, binFiles.Length);
            string filesStr = Encoding.ASCII.GetString(binFiles);
            Console.WriteLine("client files:");
            Console.WriteLine(filesStr);
            DataSet files = new DataSet();
            Object obj = DeSerializeAnObject(filesStr, files.GetType());
            files = (DataSet)obj;
            return files;
        }

        public void LogOut(Information info)
        {
            connectToServer();
            String str = SerializeAnObject(info);

            req = "Close";


            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] bReq = asen.GetBytes(req);
            byte[] bUser = asen.GetBytes(str);

            stm.Write(bReq, 0, bReq.Length);
            stm.Write(bUser, 0, bUser.Length);


            //tcpclnt.Close();
        }

        public ArrayList<string> DownloadFile(string name, List<string> resourcesIp)
        {
            ArrayList<string> list = new ArrayList<string>();

            // connect to client resource 
            tcpclnt = new TcpClient();
            tcpclnt.Connect(resourcesIp[0], 8006);
            stm = tcpclnt.GetStream();

            // send required file name and size
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] bName = asen.GetBytes(name);
            stm.Write(bName, 0, bName.Length);
            byte[] binSize = new byte[50];
            stm.Read(binSize, 0, binSize.Length);

            // recieve file context
            int size = int.Parse(Encoding.ASCII.GetString(binSize));
            byte[] binFiles = new byte[size];
            stm.Read(binFiles, 0, binFiles.Length);
            string filesStr = Encoding.ASCII.GetString(binFiles);

            Object obj = DeSerializeAnObject(filesStr, list.GetType());
            list = (ArrayList<string>)obj;
            return list;
        }


    }
}
