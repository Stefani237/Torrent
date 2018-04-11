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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBoperations;
using System.Net;
using System.Net.Sockets;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Information info;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void logIn_btn_Click(object sender, RoutedEventArgs e)
        {
            String UserName, Password, Path;
            string IP = "";

            error_lbl.Content = "";

            UserName = userName_txt.Text.Trim();
            Password = password_txt.Password.ToString();
            Path = path_txt.Text.ToString();
            if (Path.Equals(""))
            {
                error_lbl.Content = "please enter a path!";
            }


            else
            {
                // clean fields:
                userName_txt.Text = "";
                password_txt.Clear();
                path_txt.Text = "";

                // get ip address of this computer
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IP = ip.ToString();
                    }
                }

                Client messageHandler = new Client(); // create an instance of client that connects with the server
                save(UserName, Password, IP, Path); // save details in configuration file
                messageHandler.ServerMessageHandler(info);
                if (messageHandler.res == 1) // if user exist - get answer from server using messageHandler (of client type)
                {
                    error_lbl.Content = "Hello " + UserName + " !";
                    FilesManager fileManager = new FilesManager();
                    fileManager.ShowDialog(); // move to next screen
                }

                else // user not exist
                {
                    error_lbl.Content = "User not exists";
                }
            }
        }

        public void save(string name, string password, string ip, string path)
        {
            try
            {

                info = Information.Instance; // singelton - only one instance of this class
                info.UserName = name;
                info.Password = password;
                info.IpAddress = ip;
                info.Port = 8006;
                info.Path = path;
                SaveConfigFile.SaveData(info, "MyConfig.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void website_btn_Click(object sender, RoutedEventArgs e)
        { 
          
        }

        private void path_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
