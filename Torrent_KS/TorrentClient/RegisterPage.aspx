<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="TorrentClient.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<h1>Register New User</h1>
    <form id="form1" runat="server">
    <div>
        
        <br />
        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="username_txt" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="password_txt" TextMode="Password" runat="server" ></asp:TextBox>
        <br />
        <br />   
        <br />
        
        &nbsp;&nbsp;&nbsp;
        
        <br />
        <asp:Label ID="error_lbl" runat="server" Text="" style="color: #FF0000"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="register_btn" runat="server" Text="Register" 
            onclick="register_btn_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="adminLog" runat="server" onclick="adminLog_Click" 
            Text="Log In As Admin" />
        <br />
        <br />
        <br />
        
    </div>
    </form>
</body>
</html>

