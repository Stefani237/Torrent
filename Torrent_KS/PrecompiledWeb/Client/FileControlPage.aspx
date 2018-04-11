<%@ page language="C#" autoeventwireup="true" inherits="FileControlPage, App_Web_xlfphzot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Files List</h1>
    <form id="form1" runat="server">
    <p>&nbsp;</p>
    <p>
        <asp:TextBox ID="search_txt" runat="server" Width="278px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="find_btn" runat="server" onclick="find_btn_Click" Text="Find" />
    </p>
    <div>
        <div style="height:205px; overflow:scroll">
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
    </div>
    </div>
    <p>
        <asp:Button ID="showAll_btn" runat="server" onclick="showAll_btn_Click" 
            Text="Show All Files" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="files_num_lbl" runat="server" Text="Total Files:"></asp:Label>
&nbsp;<asp:Label ID="files_lbl" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="clients_num_lbl" runat="server" Text="Total Connected Clients:"></asp:Label>
&nbsp;<asp:Label ID="con_client" runat="server" Text="Label"></asp:Label>
&nbsp;
        <asp:Label ID="Label2" runat="server" Text="/"></asp:Label>
&nbsp;
        <asp:Label ID="tot_client" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Button ID="signOut_btn" runat="server" onclick="signOut_btn_Click" 
            Text="Sign Out" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="homePage_btn" runat="server" onclick="homePage_btn_Click" 
            Text="Home Page" />
    </p>
    </form>
</body>
</html>
