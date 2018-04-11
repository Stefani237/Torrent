using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControlPage : System.Web.UI.Page
{
    TorrentRef.MyTorrentServiceSoapClient serverHandler = new TorrentRef.MyTorrentServiceSoapClient();
    static int selectedId;
    static string status;
    static string userName;
    static string password;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        GridViewRow row = GridView1.SelectedRow;
        Label1.Text = "ID: " + row.Cells[1].Text + ", Name: " + row.Cells[2].Text; 
        selectedId = int.Parse(row.Cells[1].Text);
        status = row.Cells[5].Text;

        userName = row.Cells[2].Text;
        password = row.Cells[3].Text;

        if (serverHandler.IsUserAdmin(userName, password) == 1)
        {
            able_btn.Visible = false;
            update_btn.Visible = false;
            delete_btn.Visible = false;
        }
        else
        {
            able_btn.Visible = true;
            update_btn.Visible = true;
            delete_btn.Visible = true;
        }
    }
    protected void delete_btn_Click(object sender, EventArgs e)
    {
        //MyServiceReference.MyServiceSoapClient serverHandler = new MyServiceReference.MyServiceSoapClient();
        serverHandler.manageUserList(" ", " ", selectedId, "Delete");
        GridView1.DataBind();
    }
    protected void update_btn_Click(object sender, EventArgs e)
    {
        if (selectedId > 0) // if a row in the table was selected
        {
            Panel1.Visible = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String selected = RadioButtonList1.SelectedValue;
        String val = TextBox1.Text.Trim();

       // MyServiceReference.MyServiceSoapClient serverHandler = new MyServiceReference.MyServiceSoapClient();
        serverHandler.manageUserList(selected, val, selectedId, "Update");
       /* if (selected.Equals("UserName"))
        {
            serverHandler.manageUserList("UserName", val, selectedId, "Update");
        }
        else if (selected.Equals("Password"))
        {
            serverHandler.manageUserList("Password", val, selectedId, "Update");
        }
        else // Type
        {
            serverHandler.manageUserList("Type", val, selectedId, "Update");
        }*/

        GridView1.DataBind();
        Panel1.Visible = false;
    }
    protected void signOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegisterPage.aspx");
    }
    protected void homePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomePage.html");
    }
    protected void able_btn_Click(object sender, EventArgs e)
    {
 
    }
    protected void able_btn_Click1(object sender, EventArgs e)
    {
        serverHandler.manageUserList(status, "", selectedId, "Status");
        GridView1.DataBind();
    }
}