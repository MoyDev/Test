<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="PPCC_GenerateGRNDetail.aspx.vb" Inherits="ADITransfer.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .width-div { width: 50%;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<div>
<asp:Label ID="lblFormName" runat="server" Text="Generate GRN - Detail" 
        Font-Bold="True" Font-Size="Large" Height="30px" 
        Width="300px"></asp:Label>
</div>

<asp:Button ID="btnBack" runat="server" class="btn btn-outline-success btn-sm"  
        Text="Back" Font-Bold="True" Width="150" />

<asp:Button ID="btnDelete" runat="server" class="btn btn-outline-danger btn-sm"  
        Text="Delete" Font-Bold="True" Width="150" />


<br /><br />
<asp:GridView ID="GridViewDetail" AutoGenerateColumns="false" runat="server" 
        CssClass="table table-bordered font-small" Width="800px">
    <Columns>

        <asp:TemplateField HeaderText="^" ItemStyle-HorizontalAlign="Center">  
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>  
                <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-check-input position-static" Checked= '<%# Eval("Selected") %>' AutoPostBack="true" OnCheckedChanged="SelectCheckBox_CheckedChanged" />  
            </ItemTemplate>  
        </asp:TemplateField>
                        
        <asp:TemplateField HeaderText="PO">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblPO" runat="server" Text='<%# Eval("po_num") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="PO Line">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblPOLine" runat="server" Text='<%# Eval("po_line") %>'  Width="70"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="PO Release">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblPORelease" runat="server" Text='<%# Eval("po_release") %>'  Width="90"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Item">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("item") %>' Width="150" Enabled="True"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Tag ID">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblTagID" runat="server" Text='<%# Eval("Tag_Id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Lot">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblLot" runat="server" Text='<%# Eval("lot") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Qty">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("qty", "{0:N2}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       
        <%--<asp:TemplateField HeaderText="" >
            <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
            <HeaderStyle CssClass="display-col" />
            <ItemTemplate>
                <asp:Label ID="lblRowPointer" runat="server" Text='<%# Eval("RowPointer") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
                                 
    </Columns>

    <FooterStyle BorderColor="Blue" CssClass="box-sizing" Font-Size="X-Small" />

</asp:GridView>



<asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
