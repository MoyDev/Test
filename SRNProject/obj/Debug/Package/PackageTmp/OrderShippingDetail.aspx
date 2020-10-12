<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="OrderShippingDetail.aspx.vb" Inherits="ADITransfer.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .width-div { width: 50%;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<div>
<asp:Label ID="lblFormName" runat="server" Text="Order Shipping - Detail" 
        Font-Bold="True" Font-Size="Large" Height="30px" 
        Width="300px"></asp:Label>
</div>

<asp:Button ID="btnBack" runat="server" class="btn btn-outline-success btn-sm"  
        Text="Back" Font-Bold="True" Width="150" />

<br /><br />
<asp:GridView ID="GridViewDetail" AutoGenerateColumns="false" runat="server" 
        CssClass="table table-bordered font-small" Width="800px" HeaderStyle-BackColor="#FFCC99">
    <Columns>
                        
        <asp:TemplateField HeaderText="CO">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblCO" runat="server" Text='<%# Eval("co_num") %>' Font-Bold="True" Font-Size="Medium"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Line">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblCOLine" runat="server" Text='<%# Eval("co_line") %>'  Width="30" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Release">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblCORelease" runat="server" Text='<%# Eval("co_release") %>'  Width="30" Font-Size="Medium" Font-Bold="True"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Item">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("item") %>' Width="180" Enabled="True" Font-Size="Medium" Font-Bold="True"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Name">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("description") %>' Width="200" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Tag ID">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblTagID" runat="server" Text='<%# Eval("tag_id") %>' Font-Bold="True" Font-Size="Medium"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Lot">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblLot" runat="server" Text='<%# Eval("lot_id") %>' Font-Size="Medium" Font-Bold="True"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="S/N">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSN" runat="server" Text='<%# Eval("ser_num") %>' Font-Bold="True" Font-Size="Medium"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Qty">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("qty", "{0:N2}") %>' Font-Size="Medium" Font-Bold="True"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

                                 
    </Columns>

    <FooterStyle BorderColor="Blue" CssClass="box-sizing" Font-Size="X-Small" />

</asp:GridView>

<asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>

</asp:Content>
