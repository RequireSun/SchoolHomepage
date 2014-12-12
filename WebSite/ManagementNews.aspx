<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementMaster.master" AutoEventWireup="true" CodeFile="ManagementNews.aspx.cs" Inherits="ManagementNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder_01" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <div id="overflow_div" visible="false" runat="server"></div>
        <div id="news_div" runat="server">
            <ul id="news_list" runat="server"></ul>
        </div>
        <div id="page_select" runat="server"></div>
    </div>
</asp:Content>

