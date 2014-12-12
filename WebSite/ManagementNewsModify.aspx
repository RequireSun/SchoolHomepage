<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementMaster.master" AutoEventWireup="true" CodeFile="ManagementNewsModify.aspx.cs" Inherits="ManagementNewsModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder_01" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <form id="form1" runat="server">
            <asp:DropDownList ID="CategoryDropDownList" runat="server" />

            <asp:RadioButtonList ID="OutlineRadioButtonList" runat="server" OnSelectedIndexChanged="OutlineRadioButtonList_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="2" Text="学院新闻" />
                <asp:ListItem Value="3" Text="通知公告" />
            </asp:RadioButtonList>

            <br />
            <span>标题</span>
            <asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <span>内容</span>
            <asp:TextBox ID="ArticleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="确定" />
            <asp:Button ID="deleteButton" runat="server" OnClick="deleteButton_Click" Text="删除" />
        </form>
    </div>
</asp:Content>
