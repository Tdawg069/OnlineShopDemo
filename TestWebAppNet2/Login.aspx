<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TestWebAppNet2.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div runat="server" id="errDiv" style="color: Maroon"></div>
            <asp:HiddenField ID="hfUserId" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnRegister" CssClass="btn btn-primary" Style="display: inline;" runat="server" Text="Register" PostBackUrl="~/User.aspx"/>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxLogin" runat="server"></asp:TextBox>
                        <asp:Button ID="btnLogin" CssClass="btn btn-primary" Style="display: inline" runat="server" Text="Login" OnClick="btnLogin_Click"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
