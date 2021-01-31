<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TestWebAppNet2.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfUserId" runat="server" />

    <div class="jumbotron">
        <h2><%: Title %></h2>
        <h3>Application Description Goes Here...</h3>
    </div>
    <p>I thought I'd leave this out of navigation until I find something worth putting here.<br/>
	This page can still be accessed by manually typing in the URL.</p>
</asp:Content>
