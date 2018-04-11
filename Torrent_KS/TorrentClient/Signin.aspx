<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="TorrentClient.Signin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            color: #000099;
            background-color: #0099CC;
        }
        .style3
        {
            width: 552px;
        }
        .style4
        {
            font-size: large;
        }
        .style5
        {
            font-size: large;
            height: 30px;
        }
        .style6
        {
            width: 552px;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1 style="font-family:Arial" class="style1" >
    WELCOME
    </h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="font-family:Arial">
            <tr>
                <td class="style5">Username:</td>
                <td class="style6">
                    <asp:TextBox ID="txtUsername" runat="server" Width="549px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">Password:</td>
                <td class="style3">
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="547px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:LinkButton ID="Button1" runat="server" Text="Log In" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblHelloWorldResponse" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
