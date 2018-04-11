using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TorrentClient
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int val;
            String UserName, Password;
            TorrentService.MyTorrentServiceSoapClient serverHandler = new TorrentService.MyTorrentServiceSoapClient();

            UserName = txtUsername.Text.Trim();
            Password = txtPassword.Text.Trim();

            // clean fields:
            txtUsername.Text = "";
            txtPassword.Text = "";
            val = serverHandler.checkUser(UserName, Password, "Sign In");

            if (val > 0)
            {
                lblHelloWorldResponse.Text = "Login Succeeded";
                Response.Redirect("HomePage.html");
            }
            else
            {
                lblHelloWorldResponse.Text = "Wrong Input";
            }

        }
    }
}