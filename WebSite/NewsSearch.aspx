<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="NewsSearch.aspx.cs" Inherits="NewsSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server">
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>仅标题</asp:ListItem>
                <asp:ListItem>仅内容</asp:ListItem>
                <asp:ListItem>标题和内容</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="TextBox1" runat="server" Width="321px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click" />
    </div>
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <div id="overflow_div" visible="false" runat="server"></div>
        <div id="news_div" runat="server">
            <ul id="news_list" runat="server"></ul>
        </div>
        <div id="page_select" runat="server"></div>
    </div>
</asp:Content>
