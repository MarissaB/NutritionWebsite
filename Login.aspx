<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Login</h1>
    <p>
        <asp:Login ID="Login1" runat="server" OnAuthenticate="LoginButton_Click"></asp:Login></p>
</asp:Content>

