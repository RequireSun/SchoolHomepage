<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementMaster.master" AutoEventWireup="true" CodeFile="ManagementNewsModify.aspx.cs" Inherits="ManagementNewsModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder_01" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <form id="form1" runat="server">
            <asp:DropDownList ID="CategoryDropDownList" runat="server"></asp:DropDownList>

            <asp:RadioButtonList ID="OutlineRadioButtonList" runat="server" OnSelectedIndexChanged="OutlineRadioButtonList_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="2">学院新闻</asp:ListItem>
                <asp:ListItem Value="3">通知公告</asp:ListItem>
            </asp:RadioButtonList>

            <br />
            <asp:Label ID="Label1" runat="server" Text="标题"></asp:Label>
            <asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="内容"></asp:Label>
            <asp:TextBox ID="ArticleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="确定" />
            <asp:Label ID="promptLabel" runat="server"></asp:Label>
        </form>
    </div>
</asp:Content>
