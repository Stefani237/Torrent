using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileControlPage : System.Web.UI.Page
{
    TorrentRef.MyTorrentServiceSoapClient serverHandler = new TorrentRef.MyTorrentServiceSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataSource = serverHandler.getFileList();
        GridView1.DataBind();
        int filesCount = serverHandler.getCountFiles();
        int clientsCount = serverHandler.getCountClients();
        int conClientsCount = serverHandler.getConnectedClients();

        files_lbl.Text = "" + filesCount;
        con_client.Text = "" + conClientsCount;
        tot_client.Text = "" + clientsCount;
    }
    protected void signOut_btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegisterPage.aspx");
    }
    protected void homePage_btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomePage.htm");
    }
    protected void find_btn_Click(object sender, EventArgs e)
    {
        string searchName = search_txt.Text.ToString();
        GridView1.DataSource = serverHandler.getSearchResult(searchName);
        GridView1.DataBind();

    }
    protected void showAll_btn_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = serverHandler.getFileList();
        GridView1.DataBind();
    }
}