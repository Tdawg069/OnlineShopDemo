﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TestWebAppNet2.User" MasterPageFile="~/Site.Master" Title="User Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="welcomewrapper" width="100%">
        <div id="welcome" class="jumbotron">
            <h1>User Data Page</h1>
            <h2 id="subheading" runat="server">Create New User</h2>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div runat="server" id="errDiv" style="color: Maroon"></div>
            <asp:HiddenField ID="hfUserId" runat="server" />
            <asp:HiddenField ID="hfFunction" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnAdd" CssClass="btn btn-primary" Style="display: inline;" runat="server" Text="Add New User" OnClick="btnAdd_Click"/>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxSearch" runat="server"></asp:TextBox>
                        <asp:Button ID="btnEdit" CssClass="btn btn-primary" Style="display: inline" runat="server" Text="Edit Existing User" OnClick="btnEdit_Click"/>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr runat="server" visible="false" id="trUserId">
                    <td align="right">User ID</td>
                    <td>
                        <asp:TextBox Width="250" Enabled="false" ID="tbxUserId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trUserName">
                    <td align="right">Username <span style="color:red">*</span></td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxUserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidatorIDasp:RequiredFieldValidatorID="rfvUserName" runat="server" ControlToValidate="tbxUserName" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trFirstName">
                    <td align="right">First Name(s) <span style="color:red">*</span></td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxFirstName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidatorIDasp:RequiredFieldValidatorID="rfvFirstName" runat="server" ControlToValidate="tbxFirstName" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trSurName">
                    <td align="right">Surname <span style="color:red">*</span></td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxSurName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidatorIDasp:RequiredFieldValidatorID="rfvSurName" runat="server" ControlToValidate="tbxSurName" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trAddressType">
                    <td align="right">Address Type</td>
                    <td>
                        <asp:DropDownList ID="ddlAddressType" Width="250" runat="server" Style="margin-left: 5px;">
                            <asp:ListItem Text="Please Select Item" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Residential" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Business" Value="B"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trStreetAddress">
                    <td align="right">Street Address</td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxStreetAddress" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trSuburb">
                    <td align="right">Suburb</td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxSuburb" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trCity">
                    <td align="right">City</td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxCity" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" visible="true" id="trPostalCode">
                    <td align="right">Postal Code</td>
                    <td>
                        <asp:TextBox Width="250" Enabled="true" ID="tbxPostalCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" Style="display: inline;" runat="server" Text="Save" OnClick="btnSave_Click"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
