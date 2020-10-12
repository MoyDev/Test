<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="GenerateGRN.aspx.vb" Inherits="ADITransfer.GenerateGRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .width-div { width: 50%;}
    </style>

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
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblbarcode" runat="server" Text="Scan Barcode: "></asp:Label>
            </div>
            <div class="col-sm-6 width-div">
                <asp:TextBox ID="txtbarcode" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off"
                    placeholder="Please , Scan Barcode" AutoPostBack="True" ></asp:TextBox>
            </div>

        </div>


        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="Label1" runat="server" Text="Date: "></asp:Label>
            </div>
            <div class="col-sm-6 width-div">
                <asp:TextBox ID="txtDate" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="true"
                    ></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblGrnType" runat="server" Text="GRN Type: "></asp:Label>
            </div>
            <div class="col-sm-3 width-div">
            <asp:DropDownList ID="ddlGrnType" runat="server" class="form-control form-control-sm txt-margin" 
                    AutoPostBack="true" BackColor="#FFFFCC" ></asp:DropDownList>
            </div>
            <div class="col-sm-1 text-right width-div">
                <asp:Label ID="lblGRN" runat="server" Text="GRN NO: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txtGRN" runat="server" class="form-control form-control-sm txt-margin" 
                    placeholder="AUTO GENERATE" ReadOnly="True"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblvendor" runat="server" Text="Vendor: "></asp:Label>
            </div>

            <div class="col-sm-6 width-div">
                <asp:TextBox ID="tetVendor" runat="server" class="form-control form-control-sm txt-margin"   
                    ReadOnly="True"></asp:TextBox>
            </div>
        </div>



         <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblvendReceive" runat="server" Text="Vendor Receive: "></asp:Label>
            </div>
            <div class="col-sm-6 width-div">
                <asp:TextBox ID="txtvendReceive" runat="server" class="form-control form-control-sm txt-margin" placeholder="Input Data" AutoPostBack="true"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblwhse" runat="server" Text="Whse: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                   <asp:TextBox ID="txtwhse" runat="server" class="form-control form-control-sm txt-margin"   
                    ReadOnly="True"></asp:TextBox>
            </div>

            <div class="col-sm-2 text-right width-div">
                <asp:Label ID="lblLocation" runat="server" Text="Location: " Visible="False"></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddlLocation" runat="server" 
                    class="form-control form-control-sm txt-margin" BackColor="#FFFFCC" Visible="False"></asp:DropDownList>
            </div>
       
        </div>

            

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-6">
                <asp:Button ID="btnprocess" runat="server" class="btn btn-outline-success btn-block btn-sm" 
                    Text="Process" Font-Bold="True" />&nbsp;&nbsp;
            </div>
        </div>

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-6 text-center">
                <asp:Button ID="btnReset" runat="server" class="btn btn-outline-danger btn-sm"  Text="Reset" Font-Bold="True" Width="100" />
                <asp:Button ID="btnDetail" runat="server" class="btn btn-outline-secondary btn-sm"  Text="Detail" Font-Bold="True" Width="100" />
                <asp:Button ID="btnVendorLot" runat="server" class="btn btn-outline-secondary btn-sm"  Text="VendorLot" Font-Bold="True" Width="100" />
            </div>
        </div>

        <div class="row align-items-center">
        <div class="col-sm-3"></div>
            <div class="col-sm-6">
                <asp:ListView ID="PanelList" runat="server" EnableTheming="True" >
                    <ItemTemplate>
                    <asp:Panel ID="Panel1" class="card card-body bg-light mb-3" style="max-width: 100%;" runat="server" BorderColor="#0066FF" BorderStyle="Inset">
                        <div class="card-text card-body-font" >
                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>PO: </strong><asp:Label ID="lblListTagID" runat="server" Text='<%#Eval("DerPOLineRelease")%>'></asp:Label>
                            </div>  
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("item")%>'></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Qty Order: </strong> <asp:Label ID="lblListLot" runat="server" Text='<%#Eval("qty_ordered", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>Qty Received: </strong> <asp:Label ID="lblListSerNum" runat="server" Text='<%#Eval("qty_received", "{0:N2}")%>'></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>Qty Require: </strong> <asp:Label ID="lblListQty" runat="server" Text='<%#Eval("qty_require", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Sum Qty: </strong> <asp:Label ID="lblListType" runat="server"  Text='<%#Eval("sum_qty", "{0:N2}")%>'></asp:Label>
                            </div>
                        </div>

                         <div class="row">                        
                            <div class="col-sm-6">
                                <strong>Location: </strong> <asp:Label ID="Label2" runat="server" Text='<%#Eval("location")%>'></asp:Label>
                            </div>
                        
                        </div>

                    </div>
                    </asp:Panel>
                </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="col-sm-3"></div>

        </div>
        
    </div>

    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>



