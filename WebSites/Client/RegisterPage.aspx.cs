using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RegisterPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void register_btn_Click(object sender, EventArgs e)
    {
        String UserName, Password;
        error_lbl.Text = "";
        TorrentRef.MyTorrentServiceSoapClient serverHandler = new TorrentRef.MyTorrentServiceSoapClient();

        UserName = username_txt.Text.Trim();
        Password = password_txt.Text.Trim();
     

        // clean fields:
        username_txt.Text = "";
        password_txt.Text = "";


        if (serverHandler.checkUser(UserName, Password, "Register") == 0)
        {
            serverHandler.manageUserList(UserName, Password, 0, "New User");
            error_lbl.Text = "Welcome to Torrent SK!";
        }
        else
            error_lbl.Text = "User allready exists";
    }


    protected void adminLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signin.aspx");
    }
    protected void logIn_Click(object sender, EventArgs e)
    {

    }
}