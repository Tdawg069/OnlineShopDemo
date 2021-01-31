<%@ Page Title="Product Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="TestWebAppNet2.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script>
        function AddToCart(componentId) {

        }

    </script>--%>

    <div id="welcomewrapper" width="100%">
        <div id="welcome" class="jumbotron">
            <h1>Product Catalogue</h1>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div runat="server" id="errDiv" style="color: Maroon"></div>
            <asp:HiddenField ID="hfUserId" runat="server" />

            <asp:GridView 
                ID="gvProducts" 
                DataKeyNames="product_id"
                runat="server" 
                EmptyDataText="No Results To Display"
                AutoGenerateColumns="false">
                <RowStyle BackColor="Wheat" />
                <AlternatingRowStyle BackColor="White"/>
                <SelectedRowStyle BackColor="Green" Font-Bold="False" ForeColor="White" />
                <HeaderStyle BackColor="SeaGreen" Font-Bold="False" ForeColor="White" />

                <Columns>
                    <asp:BoundField Visible="False" DataField="product_id" />
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument='<%# Eval("product_id")%>' ID="lbView" runat="server" OnClick="lbView_Click" CausesValidation="false"><%# Eval("title")%></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="image_path" Visible="false" HeaderText="Product Image" />
                    <asp:TemplateField HeaderText="Product Image">
                        <ItemTemplate>
                            <img src='<%# Eval("image_path")%>' width="300"></img>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="description" HeaderText="Details" ItemStyle-Width="500"/>
                    <asp:BoundField DataField="price" HeaderText="Price" ItemStyle-Width="70" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:TemplateField HeaderText="    ">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument='<%# Eval("product_id") + "," + Eval("title")%>' ID="lbAddToCart" runat="server" OnClick="lbAddToCart_Click" CausesValidation="false">Add to Cart</asp:LinkButton>
                            <label id="lblAdded" = hidden="hidden" style="color:red">Added to Cart</label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
