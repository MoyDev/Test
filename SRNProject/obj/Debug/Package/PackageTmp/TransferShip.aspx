<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="TransferShip.aspx.vb" Inherits="ADITransfer.TransferShip" %>
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
            var dp = $('#<%=txtrecdate.ClientID%>');
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

<script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtshipdate.ClientID%>');
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

<script type = "text/javascript">
    function MsgConfirm() {

        var ListViewRowCount = document.getElementById('<%= HiddenField1.ClientID %>').value;

        if (ListViewRowCount > 0) {

            var confirm_value = document.createElement("input");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class=" container">
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
                <asp:Panel ID="WarningNotifyPanel" runat="server" CssClass= "alert alert-warning alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="WarningText" runat="server"></asp:Literal>
                </asp:Panel>

            </div>
        </div>

            <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblbarcode" runat="server"></asp:Label>
            </div>
            <div class="col-sm-6 width-div">
                <asp:TextBox ID="txtbarcode" runat="server" AutoPostBack="true" AutoComplete="off" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblshipdate" runat="server" Text="Ship Date: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txtshipdate" runat="server" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
            <div class="col-sm-2 text-right width-div">
                <asp:Label ID="lblrecdate" runat="server" Text="Received Date: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txtrecdate" AutoPostBack="true" AutoComplete="off" runat="server" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblformwhse" runat="server" Text="Form Warehouse: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddlformwhse" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-2 text-right width-div">
                <asp:Label ID="lbltowhse" runat="server" Text="To Warehouse: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddltowhse" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblformloc" runat="server" Text="Form Location: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddlformloc" runat="server" AutoPostBack="true" class="form-control form-control-sm txt-margin" onchange="MsgConfirm()"></asp:DropDownList>
            </div>
            <div class="col-sm-2 text-right width-div">
                <asp:Label ID="lbltoloc" runat="server" Text="To Location: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddltoloc" runat="server" AutoPostBack="true" class="form-control form-control-sm txt-margin"></asp:DropDownList>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lbltranferNo" runat="server" Text="Transfer NO: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txttransferNo" runat="server" ReadOnly="true" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
            <div class="col-sm-2 text-right width-div">
                <asp:Label ID="Label2" runat="server" Text="QC Pass: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:DropDownList ID="ddlQCemp" runat="server" AutoPostBack="true" disabled = "true" class="form-control form-control-sm txt-margin"></asp:DropDownList>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                
            </div>
            <div class="col-sm-2 width-div">
                <asp:CheckBox ID="chkCancelTag" AutoPostBack="true" class="form-check-input txt-margin" runat="server" />
                <asp:Label ID="Label1" runat="server" Text="Cancel Tag"></asp:Label>
            </div>
        </div>

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-6 text-center">
                <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm" Text="Process" />&nbsp;&nbsp;
                <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />
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
                                <strong>Tag ID: </strong><asp:Label ID="lblListTagID" runat="server" Text='<%#Eval("TagID")%>'></asp:Label>
                            </div>  
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Lot: </strong> <asp:Label ID="lblListLot" runat="server" Text='<%#Eval("Lot")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>S/N: </strong> <asp:Label ID="lblListSerNum" runat="server" Text='<%#Eval("SerNum")%>'></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>Qty: </strong> <asp:Label ID="lblListQty" runat="server" Text='<%#Eval("TagQty", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Type: </strong> <asp:Label ID="lblListType" runat="server"  Text='<%#Eval("Type")%>'></asp:Label>
                            </div>
                        </div>


                    </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:ListView>

            <asp:HiddenField ID="HiddenField1" runat="server" />

        </div>

            <%--<div class="col-sm-6">
                <asp:Panel ID="Panel1" class="card card-body bg-light mb-3" style="max-width: 100%;" runat="server">
                    <div class="card-text card-body-font">
                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>Tag ID: </strong><asp:Label ID="lblTagID" runat="server" Text="10003"></asp:Label>
                            </div>  
                            <div class="col-sm-6">
                                 <strong>Status: </strong> <asp:Label ID="Label2" runat="server" Text="Plan"></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblItem" runat="server" Text="RMA120"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>Lot: </strong> <asp:Label ID="lblLot" runat="server" Text="LOT1234"></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                                <strong>Qty: </strong> <asp:Label ID="lblQty" runat="server" Text="100"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Type: </strong> <asp:Label ID="lblType" runat="server" Text="Item"></asp:Label>
                            </div>
                        </div>


                    </div>
                </asp:Panel>
            </div>--%>
        </div>

        
    </div>

    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>

</asp:Content>
