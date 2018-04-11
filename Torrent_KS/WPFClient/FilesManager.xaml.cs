using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.ComponentModel;
using DBoperations;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


using System.Data.OleDb;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Xml.Serialization;
using System.Reflection;

namespace WPFClient
{

    /// <summary>
    /// Interaction logic for FielsManager.xaml
    /// </summary>
    public partial class FilesManager : Window
    {
        //localhost.WebService serverHandler = new localhost.WebService();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private Client MessageHendler = new Client();
        private DataSet files;
        private List<string> resourcesIp = new List<string>();
        const int port = 8006;
        public delegate void AddMessage(string message, string ip, Information info, string recvIndex, string countRes);
        public static event AddMessage event1;
        private string fName = "";
        private string size = "";
        private Information info = Information.Instance;
        private TcpListener receivingClient;
        private TcpClient sendingClient;
        private Stream strm;
        private Thread receivingThread;
        private DateTime dt;
        private TimeSpan ts;

        public FilesManager()
        {
            InitializeComponent();
            upload_txt.Focus();
            setServer();
        }

        private void setServer()
        {
            /** loading data form myConfig.xml file **/

            XmlDocument xmldoc = new XmlDocument(); // define new xml file
            XmlNodeList xmlnode; // define a node 
            FileStream fs = new FileStream("MyConfig.xml", FileMode.Open, FileAccess.Read); // create file stream with file name and permmisions
            xmldoc.Load(fs); // load file content into xmldoc
            xmlnode = xmldoc.GetElementsByTagName("Information"); // declere the node as information
            for (int i = 0; i < xmlnode.Count; i++) // read elments from information node
            {
                info.UserName = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                info.Password = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                info.IpAddress = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                info.Port = int.Parse(xmlnode[i].ChildNodes.Item(3).InnerText.Trim());
                info.Path = xmlnode[i].ChildNodes.Item(4).InnerText.Trim();

            }
            InitializeReceiver();
        }

        private int getAllFilesFromPath(string path, string fileName, string ext)
        {
            int found = 0; // no file found
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] info = dir.GetFiles("*.*"); // get all files from directory
            foreach (FileInfo f in info)
            {
                if (f.Name.Equals(fileName))
                {
                    found = 1; // file found
                    if (f.Extension.Equals(ext))
                    {
                        found = 2; // file is dll
                    }
                }

            }
            return found;
        }

        private void upload_btn_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            string answer = "";

            fileName = upload_txt.Text.ToString().Trim(); // get file name from text field

            string fileInfo = "";
            fileInfo = info.Path + "\\" + fileName; 

            int fileSize = -1;
            upload_txt.Text = "";

            if (getAllFilesFromPath(info.Path, fileName, "") == 0) // file not found
            {
                error_lbl.Content = "No such file exists";
                error_lbl.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    long length = new System.IO.FileInfo(fileInfo).Length;
                    fileSize = Convert.ToInt32(length);

                    MyFile myFile = new MyFile();
                    myFile.fileName = fileName;
                    myFile.size = fileSize;
                    myFile.IP = info.IpAddress;
                    myFile.port = info.Port;
                    myFile.path = info.Path;
                    MessageHendler.insertNewFile(myFile); // checks if file is already exist from that ip and insert the file if not
                    answer = MessageHendler.answer; // get answer from server (using client class) 
                    error_lbl.Visibility = Visibility.Hidden;

                    if (answer.Equals("Exists"))
                    {
                        error_lbl.Content = "File Already Exist";
                        error_lbl.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        dataGrid1_Loaded(sender, e); // update table view
                        error_lbl.Content = "DONE!";
                        error_lbl.Visibility = Visibility.Visible;
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("" + ex);

                }
            }

        }
        void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Information info = Information.Instance;
                InitializeSender(info.IpAddress);
                string message = info.IpAddress;

                MessageHendler.LogOut(info);
                byte[] myIpMsg = Encoding.ASCII.GetBytes(message);
                strm.Write(myIpMsg, 0, myIpMsg.Length);
                strm.Close();
                sendingClient.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("" + ex);
            }
        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {

            files = MessageHendler.GetFilesFromServer();
            DataTable filesData = new DataTable();
            this.dataGrid1.DataContext = files.Tables[0].DefaultView;


        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            size = "";
            MyFile file = new MyFile();
            DataRowView row = dataGrid1.SelectedItem as DataRowView;
            if (row != null)
            {
                resourcesIp.Clear();
                string name = row["FileName"].ToString();
                string type = "Resources";
                files = MessageHendler.getSearchResFromServer(name, type);
                int records = files.Tables[0].Rows.Count;
                string ipAddresses = "";
                for (int i = 0; i < records; i++)
                {
                    fName = files.Tables[0].Rows[i]["FileName"].ToString();
                    size = files.Tables[0].Rows[i]["Size"].ToString();
                    string ip = files.Tables[0].Rows[i]["IP_Field"].ToString();

                    /** adds item to the resources list **/
                    resourcesIp.Add(ip);
                    ipAddresses += ip + " , ";
                }
                string res = "Result: file name = " + fName + " size = " + size + " resources = " + records;
                selected_row_lbl.Content = res;
                dataGrid1_Loaded(sender, e);
            }

        }

        private void file_btn_Click(object sender, RoutedEventArgs e)
        {
            string searchName = search_txt.Text.ToString().Trim();
            string type = "Search";
            search_error_lbl.Visibility = Visibility.Hidden;
            if (searchName == "")
            {
                search_error_lbl.Content = "No file name was entered";
                search_error_lbl.Visibility = Visibility.Visible;
            }
            else
            {
                search_error_lbl.Visibility = Visibility.Hidden;
                files = MessageHendler.getSearchResFromServer(searchName, type);
                DataTable filesData = new DataTable();
                this.dataGrid1.DataContext = files.Tables[0].DefaultView;
                int records = files.Tables[0].Rows.Count; // number of rows in table 
                if (records > 0) // results found
                {
                    search_error_lbl.Visibility = Visibility.Hidden;
                }
                else // no results - table is empty
                {
                    search_error_lbl.Content = "No results matched your search";
                    search_error_lbl.Visibility = Visibility.Visible;
                }

            }
        }

        private void showAllFiles_btn_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1_Loaded(sender, e);
        }

        private void upload_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void download_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ArrayList<string> fileData = new ArrayList<string>();
                Information info = Information.Instance;
                if (resourcesIp.Contains(info.IpAddress))
                {
                    System.Windows.MessageBox.Show("file is allready exists!");
                }
                else
                {
                    System.Windows.MessageBox.Show("file name to downlaod: " + fName);
                    if (fName.Equals(""))
                    {
                        System.Windows.MessageBox.Show("No file was selected!");
                    }
                    else
                    {
                        int index = 0;
                        foreach (string ip in resourcesIp)
                        {
                            dt = DateTime.Now;
                            InitializeSender(ip);
                            string type = "download";
                            byte[] typeB = Encoding.ASCII.GetBytes(type);
                            Thread.Sleep(4000);
                            strm.Write(typeB, 0, typeB.Length);
                            FileReqMessage fileReq = new FileReqMessage();
                            fileReq.fileName = fName;
                            fileReq.myIP = info.IpAddress;
                            fileReq.resourceIndex = "" + index;
                            fileReq.numOfResources = "" + resourcesIp.Count;
                            string message = MessageHendler.SerializeAnObject(fileReq);
                            byte[] contentBytes = Encoding.ASCII.GetBytes(message);
                            Thread.Sleep(4000);
                            strm.Write(contentBytes, 0, contentBytes.Length);
                            strm.Close();
                            sendingClient.Close();
                            index++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("In download progress, please try later");
            }
        }

        private void InitializeSender(string ip)
        {
            try
            {
                /** creating socket to send message **/
                sendingClient = new TcpClient();

                sendingClient.Connect(ip, info.Port);
                strm = sendingClient.GetStream();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("" + ex);
            }
        }

        /** init server details and start a thread that listen to clients requests **/
        private void InitializeReceiver()
        {
            IPAddress ipAd = IPAddress.Parse(info.IpAddress); // convert string to ip
            receivingClient = new TcpListener(ipAd, 8006); // create tcp listener with port 8006 
            receivingThread = new Thread(Receiver); // create new thread with func that handles requests
            receivingThread.Start(info);
        }

        /** this is the thread function which recieves messages and listen to clients - server **/
        private void Receiver(Object obj)

        {
            Information info = (Information)obj;
            receivingClient.Start(); // strat tcpListener
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port); // get messages from any ip
            event1 += new AddMessage(MessageReceived); // add action listener to delegete
            string message;

            while (true)
            {
                try
                {
                    Socket s = receivingClient.AcceptSocket();
                    /** recieving request type **/
                    byte[] bReq = new byte[s.SendBufferSize];
                    int reqSize = s.Receive(bReq);
                    message = "";
                    for (int i = 0; i < reqSize; i++) // convert from byte to string
                        message += Convert.ToChar(bReq[i]);

                    // check request type:
                    if (info.IpAddress.Equals(message)) // log out
                    {
                        // closing window request - if recieved ip is equal to my ip - stop listening.
                        break;
                    }

                    else
                    {
                        if (message.Equals("file")) // recieve file that i downloaded
                        {
                            byte[] recvFileInfoB = new byte[s.SendBufferSize]; // get file info in bytes
                            int k = s.Receive(recvFileInfoB);
                            string recvFileInfo = "";
                            for (int i = 0; i < k; i++) // convert from byte to string
                                recvFileInfo += Convert.ToChar(recvFileInfoB[i]);
                            Array.Clear(recvFileInfoB, 0, recvFileInfoB.Length);
                            DownloadMessage DownloadMessage = new DownloadMessage(); // class of downloaded message details
                            Object ob = MessageHendler.DeSerializeAnObject(recvFileInfo, DownloadMessage.GetType()); // convert from string to DownloadMessage object (2 rows)
                            DownloadMessage myDownloadMessage = (DownloadMessage)ob;

                            int resourceIndex = int.Parse(myDownloadMessage.index); // get idx from DownloadMessage obj
                            int countResources = int.Parse(myDownloadMessage.numOfResorces);// get num of resources from DownloadMessage obj
                            int fileSize = int.Parse(myDownloadMessage.fileSize); // get file size from DownloadMessage obj

                            string fileInfo = "";
                            fileInfo = info.Path + "\\" + myDownloadMessage.fileName;
                            if (resourceIndex == 0) // create new file only if this is the first source index 
                            {
                                File.WriteAllBytes(fileInfo, myDownloadMessage.fileContent);
                            }
                            else // not first source index
                            {
                                using (var stream = new FileStream(fileInfo, FileMode.Append))
                                {
                                    // concat to last part of file
                                    stream.Write(myDownloadMessage.fileContent, 0, myDownloadMessage.fileContent.Length);
                                }
                            }

                            ts = DateTime.Now - dt; // calc dowmload time
                            string downloadedMsg = "";
                            int idx = resourceIndex + 1;
                            downloadedMsg += "" + idx + "  / " + countResources + " arrived! \n";
                            downloadedMsg += "total time: " + ts.Seconds + " seconds. \n";
                            downloadedMsg += "total length: " + myDownloadMessage.fileContent.Length + " kb. \n";
                            downloadedMsg += "bit rate: " + myDownloadMessage.fileContent.Length / ts.Seconds + " kb/s. \n";
                            System.Windows.MessageBox.Show(downloadedMsg);
                            if (resourceIndex == countResources - 1) // last resource
                            {
                                // create new MyFile and add it to DB
                                long length = new System.IO.FileInfo(fileInfo).Length;
                                MyFile myFile = new MyFile();
                                myFile.fileName = myDownloadMessage.fileName;
                                myFile.size = fileSize;
                                myFile.IP = info.IpAddress;
                                myFile.port = info.Port;
                                myFile.path = info.Path;
                                MessageHendler.insertNewFile(myFile);
                            }
                        }
                        else // another client download file from me
                        {
                            byte[] recvFileInfoB = new byte[s.SendBufferSize]; // get wanted file info
                            int k = s.Receive(recvFileInfoB);
                            string recvFileInfo = "";
                            for (int i = 0; i < k; i++)
                                recvFileInfo += Convert.ToChar(recvFileInfoB[i]);
                            Array.Clear(recvFileInfoB, 0, recvFileInfoB.Length);
                            FileReqMessage fileReq = new FileReqMessage(); 
                            Object ob = MessageHendler.DeSerializeAnObject(recvFileInfo, fileReq.GetType()); // convert from string to FileReqMessage obj (2 rows)
                            FileReqMessage myfileReq = (FileReqMessage)ob;
                            event1(myfileReq.fileName, myfileReq.myIP, info, myfileReq.resourceIndex, myfileReq.numOfResources); // call delegete func
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("In download progress, please try later");
                }
            }

            receivingClient.Stop(); // stop listening
            System.Windows.MessageBox.Show("bye bye");
            return;

        }

        private void MessageReceived(string message, string ip, Information info, string recvIndex, string countRes)
            // the clients who wants to download send me my index and how many resources there are
        {
            try
            {
                int start, end;
                string name = "";
                name = message; // file name
                int resourceIndx = int.Parse(recvIndex);
                int countResources = int.Parse(countRes);
                
                /** sending file content to client**/
                TcpClient fileClnt = new TcpClient(); // create tcp listener
                fileClnt.Connect(ip, info.Port); // connect with port 8006 to endpoint
                strm = fileClnt.GetStream();

                string type = "file";
                byte[] typeB = Encoding.ASCII.GetBytes(type); 
                Thread.Sleep(4000);
                strm.Write(typeB, 0, typeB.Length); // send msg type

                DownloadMessage downloadMsg = new DownloadMessage();
                downloadMsg.fileName = name;

                /** reading file content **/
                byte[] fileContentMsg = File.ReadAllBytes(info.Path + "\\" + name); 
                start = resourceIndx * (fileContentMsg.Length / countResources);
                if (resourceIndx == countResources - 1) // last index needs to add modolu
                {
                    end = (resourceIndx + 1) * (fileContentMsg.Length / countResources) + (fileContentMsg.Length % countResources);
                }

                else 
                {
                    end = (resourceIndx + 1) * (fileContentMsg.Length / countResources);
                }
                byte[] sendFileB = new byte[end - start]; // create bytes array with section size (2 rows)
                Array.Copy(fileContentMsg, start, sendFileB, 0, end - start);

                downloadMsg.fileContent = sendFileB;
                downloadMsg.index = "" + resourceIndx;
                downloadMsg.numOfResorces = "" + countResources;
                downloadMsg.fileSize = "" + fileContentMsg.Length;

                string downloadSendMsg = MessageHendler.SerializeAnObject(downloadMsg); // convert from object to xml string
                byte[] contentBytes = Encoding.ASCII.GetBytes(downloadSendMsg); // convert to bytes
                Thread.Sleep(4000);
                strm.Write(contentBytes, 0, contentBytes.Length); // send 

                strm.Close();
                fileClnt.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("In download progress, please try later");
            }
        }

        private void dll_btn_Click(object sender, RoutedEventArgs e)
        {
            if (fName == "") // no row was selected
            {
                dll_error_lbl.Content = "No file was selected!";
                dll_error_lbl.Visibility = Visibility.Visible;
            }
            else if (fName != "" && getAllFilesFromPath(info.Path, fName, ".dll") == 2) // the user have dll file
            {
                dll_error_lbl.Visibility = Visibility.Hidden;
                // call reflection func:
                Assembly myassembly = Assembly.LoadFrom(info.Path + "\\FileDLL.dll");
                getReflectionDLL(myassembly, "FileDLL.Dog");
                getReflectionDLL(myassembly, "FileDLL.IceCream");

            }
            else if (fName != "" && getAllFilesFromPath(info.Path, fName, ".dll") == 1) // not dll file
            {
                dll_error_lbl.Content = "Selected file is not dll";
                dll_error_lbl.Visibility = Visibility.Visible;
            }
            else // the user dont have dll file
            {
                dll_error_lbl.Content = "You don't have the file";
                dll_error_lbl.Visibility = Visibility.Visible;
            }
        }

        public void getReflectionDLL(Assembly myassembly, string classType)
        {
            Type type = myassembly.GetType(classType);

            string className = type.Name;
            string nameSpace = type.Namespace;
            string classMethods = "";
            string classProperties = "";

            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                classMethods += method.ReturnType.Name + " " + method.Name + "\n";
            }

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                classProperties += property.PropertyType.Name + " " + property.Name + "\n";
            }

            System.Windows.MessageBox.Show("Namespace: " + nameSpace + "\n" + "Class Name: " + className + "\n" + "Properties: " + classProperties + "\n" + "Methods: " + classMethods);
        }
    }
}
