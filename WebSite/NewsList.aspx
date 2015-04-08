<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <div id="overflow_div" visible="false" runat="server"></div>
        <div id="news_div" runat="server">
            <ul id="news_list" runat="server"></ul>
        </div>
        <div id="page_select" runat="server"></div>
    </div>
</asp:Content>

