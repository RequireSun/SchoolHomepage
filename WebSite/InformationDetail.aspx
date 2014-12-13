<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InformationDetail.aspx.cs" Inherits="InformationDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <form id="Form1" runat="server">
        <asp:Label ID="TitleLabel" runat="server" />
        <asp:Label ID="ContentLabel" runat="server" />
        </form>
    </div>
</asp:Content>

