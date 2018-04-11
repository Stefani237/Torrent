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
using System.Data;

using System.Threading;

namespace Server
{
    class Program
    {
        private static DBoperations.Users data = new DBoperations.Users(); // get instance of DB


        private static void handelRequest(Object arg)
        {
            // get requests and handels them
            Socket s = (Socket)arg;
            string msg;
            byte[] bReq = new byte[s.SendBufferSize]; // declere byte array to recieve a massage
            int reqSize = s.Receive(bReq); // recieve the req type and get it's size

            string typeMsg = "";

            for (int i = 0; i < reqSize; i++) // convert the message  from byte to string
                typeMsg += Convert.ToChar(bReq[i]);

            Console.WriteLine("^^^^^^^^^^ " + typeMsg + " ^^^^^^^^^^");

            if (typeMsg.Equals("Log In"))
            {
                byte[] b = new byte[s.SendBufferSize]; // recieve the user details in bytes
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");
                msg = "";
                for (int i = 0; i < k; i++) // convert to string
                    msg += Convert.ToChar(b[i]);
                Array.Clear(b, 0, b.Length);
                Console.Write(msg);
                Information info = new Information(); // create an instance of client information
                Object obj = DeSerializeAnObject(msg, info.GetType()); // convert from string to information object (2 rows)
                Information inf = (Information)obj;
                Console.WriteLine("-------------- " + inf.UserName + " " + inf.Password + " -------------");
                
                int result = data.isUserExist(inf.UserName, inf.Password);
                 
                if (result > 0) // user exists
                {
                    data.UpdateDetailsSignIn(inf.UserName, inf.Password, inf.IpAddress, inf.Port, inf.Path); // update user's ip, port and path in DB
                    data.SetStatus(inf.UserName, inf.Password, "enable"); // set status to enable - available to download from
                    Console.WriteLine("User found!! :)");
                    b[0] = 1; // return answer from server to client. 1 = found.

                    s.Send(b);
                }
                else  // user not exists
                {
                    Console.WriteLine("User Not found!! :(");
                    b[0] = 0; // return answer from server to client. 0 = not found.
                    s.Send(b);
                }
                s.Close();
            }

            else if (typeMsg.Equals("Files")) // show files in FileManager's table
            {
                Console.WriteLine("searching for files");
                sendFilesList(s);
                s.Close();
            }

            else if (typeMsg.Equals("Search")) // search for a file in FileManager's table
            {

                SearchFile(s, "Search"); // call func with search req
                s.Close();
            }

            else if (typeMsg.Equals("Resources"))
            {

                SearchFile(s, "Resources"); // call func with Resources req
                s.Close();
            }

            else if (typeMsg.Equals("Insert"))
            {
                Console.WriteLine("---------- insert request -----------");
                insertFileToDB(s);
                s.Close();
            }

            else if (typeMsg.Equals("Close"))
            {
                Console.WriteLine("User Log out");
                logOutUser(s);
                s.Close();
            }
        }

        static void Main(string[] args)
        {

            try
            {
                string msg = "";
                string ipStr = "";

                // get the IP of the server computer
                var host = Dns.GetHostEntry(Dns.GetHostName()); 
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipStr = "" + ip;
                        Console.WriteLine(ipStr);
                    }
                }

                IPAddress ipAd = IPAddress.Parse(ipStr); // convert from String to IP address
                TcpListener myList = new TcpListener(ipAd, 8005); // listen to request in 8005 port

                myList.Start(); // start listening

                Console.WriteLine("The server is running at port 8005...");
                Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");


                while (true)
                {
                    Socket s = myList.AcceptSocket(); // accept socket in the port
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                    Thread t = new Thread(handelRequest); // new thread for each connection request, handelRequest is the func that tell the thread what to do
                    t.Start(s); // start the thread with socket
                }

                myList.Stop(); // stop listening

            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }



        public static void sendFilesList(Socket socket)
        {
            DataSet filesList;

            filesList = data.getFiles(); // get files from DB


            String strFiles = SerializeAnObject(filesList); // convert from DataSet object to string
            ASCIIEncoding asen = new ASCIIEncoding(); 
            byte[] filesb = asen.GetBytes(strFiles);  // convert from String to bytes
            int size = filesb.Length; // get byes size
            byte[] msgSize = asen.GetBytes("" + size); // encode the byte array size
            socket.Send(msgSize); // send size
            socket.Send(filesb); // send files
        }

        public static void SearchFile(Socket socket, string searchType)
        {
            DataSet filesList;
            string nameMsg = "";
            byte[] bname = new byte[socket.SendBufferSize]; 
            int nameSize = socket.Receive(bname); // get file name in bytes
            for (int i = 0; i < nameSize; i++) // convert from bytes to string 
                nameMsg += Convert.ToChar(bname[i]);

            if (searchType.Equals("Search"))
            {

                filesList = data.searchFile(nameMsg);
            }

            else // searchType.Equals("resources")
            {

                filesList = data.searchFileResources(nameMsg);
            }



            String strFiles = SerializeAnObject(filesList);
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] filesb = asen.GetBytes(strFiles);
            int size = filesb.Length;
            byte[] msgSize = asen.GetBytes("" + size);
            socket.Send(msgSize);
            socket.Send(filesb);
        }

        public static void insertFileToDB(Socket socket)
        {

            byte[] b = new byte[socket.SendBufferSize];
            int k = socket.Receive(b); // get myFile object in bytes
            Console.WriteLine("Recieved...");
            string msg = "";
            for (int i = 0; i < k; i++) // convert to string 
                msg += Convert.ToChar(b[i]);
            Array.Clear(b, 0, b.Length);
            Console.Write(msg);
            MyFile file = new MyFile();
            Object obj = DeSerializeAnObject(msg, file.GetType()); // convert from string to myFile object (2 rows)
            MyFile newFile = (MyFile)obj;
            int ans = data.isFileExist(newFile.fileName, newFile.IP); // checks if the file exist in system
            string str = "";
            if (ans < 1) // no rows found
            {
                str = "Not Exists";
                data.insertFile(newFile.fileName, newFile.size, newFile.IP, newFile.port, newFile.path);
            }
            else
            {
                str = "Exists";

            }

            ASCIIEncoding asen = new ASCIIEncoding();

            byte[] flag = asen.GetBytes(str); // return answer from server to client
            socket.Send(flag);
        }

        public static string SerializeAnObject(object AnObject)
        {
            // convert from an object to xml string 
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }
        public static Object DeSerializeAnObject(string XmlOfAnObject, Type ObjectType)
        {
            // convert from an xml string to object
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

        public static void logOutUser(Socket socket)
        {
            string msg = "";
            try
            {
                Console.WriteLine("---------- login out! -------------");
                byte[] b = new byte[socket.SendBufferSize]; 
                int k = socket.Receive(b); // recieve user's details in bytes
                Console.WriteLine("Recieved...");

                for (int i = 0; i < k; i++) // convert to string from bytes
                    msg += Convert.ToChar(b[i]);
                Console.WriteLine("server str -> " + msg);
                Array.Clear(b, 0, b.Length);
                Console.Write(msg);
                Information info = new Information();
                Object obj = DeSerializeAnObject(msg, info.GetType()); // convert from string to information object (2 rows)
                Information inf = (Information)obj;
                data.SetStatus(inf.UserName, inf.Password, "disable"); // update user to disable - not available to download from
                Console.WriteLine("server succieded -> yes");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
