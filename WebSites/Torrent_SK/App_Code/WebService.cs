using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using DBoperations;
using System.Data;

[WebService]
public class WebService
{
    Users data = new Users();

    [WebMethod]
    public int checkUser(String UserName, String Password, String reqType)
    {
        int isExist = 0; // flag that checks if customer allready exists

        // check in database if user details are valid- if found return true, else false
        if (reqType.Equals("Sign In"))
        {
            // check if user is admin
            isExist = data.isAdmin(UserName, Password);
        }
        else // Register
        {
            // check if user is exists
            isExist = data.isUserExist(UserName, Password);
        }

      /*  if (isExist > 0)
            return 1;
        else
            return 0;*/
        return isExist;

    }


    [WebMethod]
    public void manageUserList(String str1, String str2, int id, String reqType)
    {
        if (reqType.Equals("Delete"))
        {
            data.deleteUser(id);
        }
        else if (reqType.Equals("New User"))
        {
            data.addNewUser(str1, str2);
        }
        else if (reqType.Equals("Update"))
        {
            data.updateUser(id, str2, str1);
        }
        else if (reqType.Equals("Status"))
        {
           data.ChangeStatus(id, str1);
        }
    }

    [WebMethod]
    public void SignInDetails(string userName, string password, string IP, int port, string path)
    {
        data.UpdateDetailsSignIn(userName, password, IP, port, path);
    }

    [WebMethod]
    public void InsertFilesRecord(string fileName, int size, string IP, int port, string path)
    {
           data.insertFile(fileName, size, IP, port, path);

    }

    [WebMethod]
    public void Connection(string userName, string password, string status)
    {
        if (data.isAdmin(userName, password) == 0)
        {
            data.SetStatus(userName, password, status);
        }
    }

    [WebMethod]
    public int IsUserAdmin(string userName, string password)
    {
        int answer = 0;
        answer = data.isAdmin(userName, password);
        return answer;
    }

    [WebMethod]
    public DataSet getFileList()
    {
        DataSet files = data.getFiles();
        return files;
    }

    [WebMethod]
    public DataSet getSearchResult(string name)
    {
        DataSet files = data.searchFile(name);
        return files;
    }

    [WebMethod]
    public int getCountClients()
    {
        int count = 0;
        count = data.clientCount();
        return count;
    }

    [WebMethod]
    public int getCountFiles()
    {
        int count = 0;
        count = data.fileCount();
        return count;
    }


    [WebMethod]
    public int getConnectedClients()
    {
        int count = 0;
        count = data.connectedClients();
        return count;
    }
}
