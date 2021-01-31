<%@ Page Title="Your Shopping Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TestWebAppNet2.Cart" %>
<%--@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" --%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script>
        function RemoveFromCart(componentId) {

        }

    </script>--%>

    <div id="welcomewrapper" width="100%">
        <div id="welcome" class="jumbotron">
            <h1>Your Shopping Cart</h1>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div runat="server" id="errDiv" style="color: Maroon"></div>
            <asp:HiddenField ID="hfUserId" runat="server" />
            <asp:HiddenField ID="hfTotal" runat="server" />

            <asp:GridView 
                ID="gvProducts" 
                DataKeyNames="user_basket_id"
                runat="server" 
                EmptyDataText="No Results To Display"
                AutoGenerateColumns="false">
                <RowStyle BackColor="Wheat" />
                <AlternatingRowStyle BackColor="White"/>
                <SelectedRowStyle BackColor="Green" Font-Bold="False" ForeColor="White" />
                <HeaderStyle BackColor="SeaGreen" Font-Bold="False" ForeColor="White" />

                <Columns>
                    <asp:BoundField Visible="False" DataField="user_basket_id" />
                    <asp:BoundField Visible="False" DataField="product_id" />
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument='<%# Eval("product_id")%>' ID="lbView" runat="server" OnClick="lbView_Click" CausesValidation="false"><%# Eval("title")%></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="image_path" Visible="false" HeaderText="Product Image" />
                    <asp:TemplateField HeaderText="Product Image">
                        <ItemTemplate>
                            <img src='<%# Eval("image_path")%>' width="50"></img>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="description" HeaderText="Details" ItemStyle-Width="500"/>--%>
                    <asp:BoundField DataField="price" HeaderText="Price" ItemStyle-Width="70" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="quantity" HeaderText="Quantity" ItemStyle-Width="70" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="total" HeaderText="Subtotal" ItemStyle-Width="70" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:TemplateField HeaderText="    ">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument='<%# Eval("product_id")%>' ID="lbRemoveFromCart" runat="server" OnClick="lbRemoveFromCart_Click" CausesValidation="false">Remove from Cart</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />

            <div id="divTotalCost" runat="server" class="h2" align="right"></div>
            <br />
            <div align="right">
                <asp:Button ID="btnCheckout" CssClass="btn btn-primary" Style="display: inline;" runat="server" Text="Checkout" OnClick="btnCheckout_Click"/>
                <%--<cc1:ConfirmButtonExtender ID="cbeCheckout" runat="server" ConfirmOnFormSubmit="false"
                    ConfirmText="Are you sure you want to hand me all your money?"
                    TargetControlID="btnCheckout">
                </cc1:ConfirmButtonExtender>--%>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
