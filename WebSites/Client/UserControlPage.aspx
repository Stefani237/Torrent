<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserControlPage.aspx.cs" Inherits="UserControlPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Edit User List</h1>
    <form id="form1" runat="server">
    <div>
    <div style="height:300px; overflow:scroll">
    
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="SqlDataSource1" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="Password" HeaderText="Password" 
                    SortExpression="Password" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
                <asp:BoundField DataField="IP_Field" HeaderText="IP" 
                SortExpression="IP_Field" />
                <asp:BoundField DataField="Port_Field" HeaderText="Port" 
                SortExpression="Port_Field" />
                <asp:BoundField DataField="Path_Files" HeaderText="Path Files" 
                SortExpression="Path_Files" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            
            ConnectionString="<%$ ConnectionStrings:ClientsDBConnectionString %>" 
            SelectCommand="SELECT * FROM [Clients]"></asp:SqlDataSource>
            </div>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" style="color: #0033CC"></asp:Label>
        <br />
        <br />
        <asp:Button ID="delete_btn" runat="server" onclick="delete_btn_Click" 
            Text="Delete" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="update_btn" runat="server" Text="Update" 
            onclick="update_btn_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;
        <asp:Button ID="able_btn" runat="server" Text="Disable / Enable" 
            onclientclick="able_btn_Click" onclick="able_btn_Click1" />
        <br />
        <br />
        <asp:Button ID="signOut" runat="server" onclick="signOut_Click" 
            Text="Sign Out" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="homePage" runat="server" onclick="homePage_Click" 
            Text="Home Page" />
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="136px">
                <asp:ListItem>UserName</asp:ListItem>
                <asp:ListItem>Password</asp:ListItem>
                <asp:ListItem>Type</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Please enter new value:"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 2px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="OK" />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
