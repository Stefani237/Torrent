using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DBoperations
{
    public class Users
    {
        private SqlConnection sqlcon;

        public Users()
        {
            string config = "Data Source=localhost;Initial Catalog=ClientsDB;Integrated Security=True"; // connection string
            sqlcon = new SqlConnection(config); // create connection to DB with the connection string
        }

        public int isAdmin(string UserName, string Password)
        {
            // checks if user is admin
            int isExist;
            SqlCommand sqlcmd = new SqlCommand("Select *from Clients where Username='" + UserName + "' and Password='" + Password + "' and Type='Admin'", sqlcon);
            sqlcmd.Connection.Open();
            isExist = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();

            if (isExist > 0) 
                return 1;
            else
                return 0;
        }

        public void addNewUser(string UserName, string Password)
        {
            // new user registreation
            SqlCommand sqlcmd = new SqlCommand("INSERT INTO Clients VALUES ('" + UserName + "', '" + Password + "', 'Client', 'disable', '',8006, '')", sqlcon);
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }

        public int isUserExist(string UserName, string Password)
        {
            // checks if user exists (client or admin)
            int isExist;
            SqlCommand sqlcmd = new SqlCommand("Select *from Clients where Username='" + UserName + "' and Password='" + Password + "'", sqlcon);
            sqlcmd.Connection.Open();
            isExist = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();

            if (isExist > 0) // if client exists
                return 1;
            else
                return 0;
        }
        public void deleteUser(int id)
        {
            // delete clients from DB (can't delete admin - implement in portal)
            SqlCommand sqlcmd = new SqlCommand("DELETE FROM Clients WHERE ID='" + id + "'", sqlcon);
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }

        public void updateUser(int id, string val, string type)
        {
            // update client name / password / type (can't update admin - implement in portal)
            SqlCommand sqlcmd;
            if (type.Equals("UserName"))
            {
                sqlcmd = new SqlCommand("UPDATE Clients SET UserName ='" + val + "'" + " WHERE ID='" + id + "'", sqlcon);
            }
            else if (type.Equals("Password"))
            {
                sqlcmd = new SqlCommand("UPDATE Clients SET Password ='" + val + "'" + " WHERE ID='" + id + "'", sqlcon);

            }
            else // Type
            {
                sqlcmd = new SqlCommand("UPDATE Clients SET Type ='" + val + "'" + " WHERE ID='" + id + "'", sqlcon);
            }
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }

        public void ChangeStatus(int id, string val)
        {
            // enable or disable clients (can't change for admin - implement in portal)
            SqlCommand sqlcmd;
            if (val.Equals("disable"))
            {
                sqlcmd = new SqlCommand("UPDATE Clients SET Status = 'enable' WHERE ID='" + id + "'", sqlcon);
            }
            else // "enable"
            {
                sqlcmd = new SqlCommand("UPDATE Clients SET Status = 'disable' WHERE ID='" + id + "'", sqlcon);
            }
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();

        }
        public void UpdateDetailsSignIn(string userName, string password, string IP, int port, string path)
        {
            // update ip, port and path for users who sign in
            SqlCommand sqlcmd;
            sqlcmd = new SqlCommand("UPDATE Clients SET IP_field='" + IP + "' , Port_field='" + port + "' , Path_Files='" + path + "' WHERE UserName='" + userName + "' and Password='" + password + "'", sqlcon);
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }

        public void insertFile(string fileName, int size, string IP, int port, string path)
        {
            // upload and download files transactions are insert to DB
            SqlCommand sqlcmd = new SqlCommand("INSERT INTO Files VALUES ('" + fileName + "','" + size + "','" + IP + "','" + port + "','" + path + "')", sqlcon);

            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }


        public void SetStatus(string userName, string password, string status)
        {
            // changes enable / disable when user is log in
            SqlCommand sqlcmd = new SqlCommand("UPDATE Clients SET Status ='" + status + "' WHERE UserName = '" + userName + "' AND Password ='" + password + "'", sqlcon);
            sqlcmd.Connection.Open();
            sqlcmd.ExecuteScalar();
            sqlcmd.Connection.Close();
        }

        public DataSet getFiles()
        {
            // show all files that are available for download
            string str = "SELECT FileName, max(Size) as Size From Files WHERE IP_Field IN(SELECT distinct IP_Field FROM Clients WHERE Status = 'enable') group by FileName";

            SqlDataAdapter adapter = new SqlDataAdapter(str, sqlcon);

            DataSet files = new DataSet();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(files, "Files");

            return files;
        }

        public DataSet searchFile(string name)
        {
            // search for a file according to file name
            string str = "SELECT distinct FileName, Size  From Files "
                                                            + "WHERE IP_Field IN (SELECT distinct IP_Field FROM Clients WHERE Status = 'enable')"
                                                            + "AND FileName ='" + name + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(str, sqlcon);

            DataSet files = new DataSet();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(files, "Files");

            return files;
        }

        public DataSet searchFileResources(string name)
        {
            // returns the users that contains the file name
            string str = "SELECT FileName, Size, IP_Field From Files "
                                                            + "WHERE IP_Field IN (SELECT distinct IP_Field FROM Clients WHERE Status = 'enable')"
                                                            + "AND FileName ='" + name + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(str, sqlcon);

            DataSet files = new DataSet();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(files, "Files");

            return files;
        }

        public int clientCount()
        {
            // counts total users register to system
            int count = 0;
            SqlCommand sqlcmd = new SqlCommand("SELECT count(*) FROM Clients", sqlcon);
            sqlcmd.Connection.Open();
            count = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();

            return count;
        }

        public int fileCount()
        {
            // counts total file in the system
            int count = 0;
            SqlCommand sqlcmd = new SqlCommand("SELECT count(*) FROM FILES", sqlcon);
            sqlcmd.Connection.Open();
            count = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();

            return count;
        }

        public int connectedClients()
        {
            // counts number of connected users in the system (available users to download from)
            int count = 0;
            SqlCommand sqlcmd = new SqlCommand("SELECT count(*) FROM Clients WHERE Status = 'enable'", sqlcon);
            sqlcmd.Connection.Open();
            count = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();

            return count;
        }

        public int isFileExist(string fileName, string IP)
        {
            // checks if this ip already upload that file (cant upload it again)
            int count = 0;
            SqlCommand sqlcmd = new SqlCommand("SELECT count(*) FROM Files WHERE FileName ='" + fileName + "' and IP_Field ='" + IP + "'", sqlcon);
            sqlcmd.Connection.Open();
            count = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();


            return count;
        }

        public DataSet getConnectedUsers()
        {
            // not used
            string str = "SELECT IP_Field FROM Clients WHERE Status = 'enable'";

            SqlDataAdapter adapter = new SqlDataAdapter(str, sqlcon);

            DataSet connect = new DataSet();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(connect, "Clients");

            return connect;
        }

        public int countConnectedUsers()
        {
            // not used
            int count = 0;

            SqlCommand sqlcmd = new SqlCommand("SELECT count(*) FROM Clients WHERE Status = 'enable'", sqlcon);
            sqlcmd.Connection.Open();
            count = Convert.ToInt32(sqlcmd.ExecuteScalar());
            sqlcmd.Connection.Close();


            return count;
        }

    }
}
