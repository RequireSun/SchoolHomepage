<%@ Page Title="" Language="C#" MasterPageFile="~/ManagementMaster.master" AutoEventWireup="true" CodeFile="ManagementNewsModify.aspx.cs" Inherits="ManagementNewsModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder_01" Runat="Server">
    <div id="failure_div" visible="false" runat="server"></div>
    <div id="success_div" runat="server">
        <form id="form1" runat="server">
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <asp:RadioButtonList ID="OutlineRadioButtonList" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>新闻</asp:ListItem>
                <asp:ListItem>通知</asp:ListItem>
            </asp:RadioButtonList>

            <br />
            <asp:Label ID="Label1" runat="server" Text="标题"></asp:Label>
            <asp:TextBox ID="titleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="内容"></asp:Label>
            <asp:TextBox ID="articleTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="确定" />
            <asp:Button ID="cancelButton" runat="server" OnClick="cancelButton_Click" Text="取消" />
            <asp:Label ID="promptLabel" runat="server"></asp:Label>
        </form>
    </div>
</asp:Content>
