<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagementLogin.aspx.cs" Inherits="ManagementLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>请先登录再进行相关操作</p>
        <asp:TextBox ID="AccountTextBox" MaxLength="20" runat="server" />
        <br /><br />
        <asp:TextBox ID="PasswordTextBox" TextMode="Password" MaxLength="20" runat="server" />
        <br /><br />
        <asp:Button ID="LoginButton" OnClick="LoginButton_Click" Text="登录" runat="server" />
        <br /><br />
        <asp:Label ID="ResultLable" runat="server" />
    </form>
</body>
</html>
