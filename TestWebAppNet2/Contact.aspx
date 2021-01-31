<%@ Page Title="Contact Me" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TestWebAppNet2.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfUserId" runat="server" />

    <div class="jumbotron">
        <h2><%: Title %></h2>
        <h3>My contact details:</h3>
    </div>
    <address>
        Somewhere in Durban<br />
        KZN<br />
        South Africa<br />
        Planet Earth<br />
        <abbr title="Phone">Ph:</abbr>
        072 298 6438
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:trinesan.govender@live.co.za">trinesan.govender@live.co.za</a>
    </address>
</asp:Content>
