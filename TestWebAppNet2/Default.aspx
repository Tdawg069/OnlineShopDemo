<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestWebAppNet2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfUserId" runat="server" />

    <div class="jumbotron">
        <h1>Awesome Title Goes Here!</h1>
        <p id=feedback runat="server" class="lead">This website uses cookies... Deal with it!</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Product Catalogue</h2>
            <p>
                Weird Stuff For Sale
            </p>
            <p>
                <a class="btn btn-default" href="/Product.aspx?uid=<%: hfUserId.Value %>">To Catalogue</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>User Management</h2>
            <p>
                Mess around with other people's profiles just for the fun of it
            </p>
            <p>
                <a class="btn btn-default" href="/User.aspx?uid=<%: hfUserId.Value %>">To User Pages</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Contact Me</h2>
            <p>
                Stuff about me, the developer, in case you're curious :)
            </p>
            <p>
                <a class="btn btn-default" href="/Contact.aspx?uid=<%: hfUserId.Value %>">Learn more</a>
            </p>
        </div>
    </div>

</asp:Content>
