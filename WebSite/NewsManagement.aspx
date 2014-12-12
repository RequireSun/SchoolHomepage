<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsManagement.aspx.cs" Inherits="NewsManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" >
        </asp:DropDownList>
        &nbsp;
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>新闻</asp:ListItem>
            <asp:ListItem>通知</asp:ListItem>
        </asp:RadioButtonList>
    
        <br />
&nbsp;<asp:Label ID="Label1" runat="server" Text="标题"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="titleTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        &nbsp;<asp:Label ID="Label2" runat="server" Text="内容"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="articleTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="确定" />
&nbsp;&nbsp;
        <asp:Button ID="cancelButton" runat="server" OnClick="cancelButton_Click" Text="取消" />
    
    &nbsp;&nbsp;&nbsp;
        <asp:Label ID="promptLabel" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
