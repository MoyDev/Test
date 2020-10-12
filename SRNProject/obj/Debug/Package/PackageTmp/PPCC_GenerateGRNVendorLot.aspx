<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="PPCC_GenerateGRNVendorLot.aspx.vb" Inherits="ADITransfer.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();

            $('#txtExpDate').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        });
    </script>

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .display-col { display:none;}
        .width-div { width: 50%;}
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br />
<div>
<asp:Label ID="lblFormName" runat="server" Text="Generate GRN - Vendor Lot" 
        Font-Bold="True" Font-Size="Large" Height="30px" 
        Width="500px"></asp:Label>
</div>

<asp:Button ID="btnBack" runat="server" class="btn btn-outline-success btn-sm"  
        Text="Back" Font-Bold="True" Width="150" />

<br /><br />

             <asp:GridView ID="GridViewVendorLot" AutoGenerateColumns="false" runat="server" 
                CssClass="table table-bordered font-small" Width="1000px">
                    <Columns>
             
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
                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("item") %>' Width="120" Enabled="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lot">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblLot" runat="server" Text='<%# Eval("lot") %>' Width="120" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vendor Lot">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtvendor_lot" ReadOnly="false" runat="server" Font-Size="15px" AutoComplete="off"
                                                                Text='<%# Eval("vendor_lot") %>' AutoPostBack="true"  CssClass="form-control numeric" 
                                                                style="width:100px; text-align:left" Height="30" OnTextChanged="txtvendor_lot_TextChanged" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                      <asp:TemplateField HeaderText="Expiration Date">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="center"/>
                            <ItemTemplate>
                                <%--<asp:Label ID="lblExpDate" runat="server" Text='<%# Eval("expire_date") %>' Width="150"></asp:Label>--%>
                                <asp:TextBox ID="txtExpDate" ReadOnly="True" runat="server" Font-Size="15px"
                                                                Text='<%# Eval("expire_date") %>' AutoPostBack="true"  CssClass="form-control" 
                                                                style="width:106px; text-align:left" Height="30" OnTextChanged="txtExpDate_TextChanged"></asp:TextBox>
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                              <asp:ImageButton ID="btncalendar" runat="server" OnClick="btncalendar_OnClick" CommandArgument='<%# Eval("RowPointer") %>' ImageUrl="images/calendar-512.png" Width="30px" Height="30px" ToolTip="Add Expiration Date" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RowPointer">
                            <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                            <HeaderStyle CssClass="display-col" />
                            <ItemTemplate>
                                <asp:Label ID="lblRowPointer" runat="server" Text='<%# Eval("RowPointer") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                 
                    </Columns>

                    <FooterStyle BorderColor="Blue" Font-Size="X-Small" />

                </asp:GridView>

                <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>

</asp:Content>

