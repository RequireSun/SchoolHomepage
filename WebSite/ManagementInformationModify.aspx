<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementMaster.master" AutoEventWireup="true" CodeFile="ManagementInformationModify.aspx.cs" Inherits="ManagementInformationModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder_01" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <asp:Label ID="TitleLabel" runat="server" />
        <asp:TextBox ID="ContentTextBox" runat="server" />
        <br /><br />
        <asp:Button ID="SubmitButton" runat="server" />
    </div>
</asp:Content>

