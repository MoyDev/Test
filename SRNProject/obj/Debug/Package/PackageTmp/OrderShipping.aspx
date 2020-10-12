<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="OrderShipping.aspx.vb" Inherits="ADITransfer.OrderShipping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

     <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .font-small { font-size:11px;}
        .checkbox-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtduedate.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
            $('<%=txtBarcode.ClientID%>').focus();
        });
</script>

<script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtdate.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
            $('<%=txtBarcode.ClientID%>').focus();
        });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="row align-items-center">
            <div class="col-sm-12">
                <asp:Panel ID="NotPassNotifyPanel" runat="server" CssClass= "alert alert-danger alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="NotPassText" runat="server"></asp:Literal>
                </asp:Panel>
                <asp:Panel ID="PassNotifyPanel" runat="server" CssClass= "alert alert-success alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="PassText" runat="server"></asp:Literal>
                </asp:Panel>

            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3"></div>

            <div class="col-sm-6 text-right" style="margin-bottom:7px;"> 
                <asp:LinkButton runat="server" ID="btnstat" class="btn btn-outline-success btn-block btn-sm" aria-hidden="true" AutoPostBack="true">
                    <i class="fa fa-arrow-right" aria-hidden="true"></i> <strong>Order Shipping</strong>
                </asp:LinkButton>
            </div>
        </div>

        <div class="row align-items-center checkbox-margin">
            <div class="col-sm-3 text-right width-div">               
            </div>
            <div class="col-sm-3 width-div">
                    <asp:CheckBox ID="chkCancelTag" AutoPostBack="true" class="form-check-input txt-margin" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text="Cancel Tag"></asp:Label>
            </div> 
            <div class="col-sm-3 width-div"></div>            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblbarcode" runat="server" Text="Barcode Customer Order: "></asp:Label> 
            </div>
            <div class="col-sm-6 width-div">
                 <asp:TextBox ID="txtBarcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                 
            </div>             
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lbldate" runat="server" Text="Date: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
             <div class="col-sm-2 text-right width-div">
                 <asp:Label ID="lblQAOutgoing" runat="server" Text="Outgoing QA: "></asp:Label>
             </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddlQAOutgoing" BackColor="#FBFF8D" runat="server" AutoPostBack="true" class="form-control form-control-sm txt-margin"></asp:DropDownList>
            </div>
        </div>

         <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblco" runat="server" Text="Customer Order: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
               <%--<asp:TextBox ID="txtCo" runat="server" 
                    class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlCo" runat="server" class="form-control form-control-sm txt-margin" ></asp:DropDownList>
            </div>
            <div class="col-sm-1 text-left width-div">
               <asp:TextBox ID="txtcoline" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" Visible="false"></asp:TextBox>
               <asp:TextBox ID="txtcorelease" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" Visible="false"></asp:TextBox>
              
            </div>
            
         </div>

         <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblduedate" runat="server" Text="DueDate: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
               <asp:TextBox ID="txtduedate" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
            </div>
            
         </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="Label2" runat="server" Text="CO Return Code: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
               <asp:DropDownList ID="ddlCoreturnCode" runat="server" class="form-control form-control-sm txt-margin"></asp:DropDownList>
            </div>
            
         </div>

         <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-6 text-center">
                <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm" Text="Process" />&nbsp;&nbsp;

                <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm"  Text="Detail" />
            </div>
        </div>

        <div class="row align-items-center">
        <div class="col-sm-3"></div>
        <div class="col-sm-6">
           <asp:ListView id="PanelList" runat="server">
                <ItemTemplate>
                    <asp:Panel ID="Panel1" class="card card-body bg-light mb-3" style="max-width: 100%;" runat="server">
                        <div class="card-text card-body-font">
                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>CO: </strong><asp:Label ID="lblListCO" runat="server" Text='<%#Eval("DerCoCoLineCoRelease")%>'></asp:Label>
                            </div>  
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("item")%>'></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Qty Order: </strong> <asp:Label ID="lblListQtyOrder" runat="server" Text='<%#Eval("qty_ordered", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>Qty Shipped: </strong> <asp:Label ID="lblListQtyShip" runat="server" Text='<%#Eval("qty_shipped", "{0:N2}")%>'></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                               <strong>Qty Required: </strong> <asp:Label ID="lblListQtyReq" runat="server" Text='<%#Eval("qty_req", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Sum Qty: </strong> <asp:Label ID="lblListLoc" runat="server" Text='<%#Eval("sum_qty" , "{0:N2}")%>'></asp:Label>
                               
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                               <strong>Location: </strong> <asp:Label ID="Label3" runat="server" Text='<%#Eval("loc")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Type: </strong> <asp:Label ID="Label4" runat="server" Text='<%#Eval("type")%>'></asp:Label>
                               
                            </div>
                        </div>

                    </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:ListView>

        </div>

    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>