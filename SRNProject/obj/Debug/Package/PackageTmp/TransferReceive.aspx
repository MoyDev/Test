<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="TransferReceive.aspx.vb" Inherits="ADITransfer.WebForm1" %>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>

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
                        <asp:LinkButton runat="server" ID="btnstat" class="btn btn-outline-success btn-block" aria-hidden="true" AutoPostBack="true">
                            <i class="fa fa-arrow-right" aria-hidden="true"></i> <strong>Receive</strong>
                        </asp:LinkButton>
                    </div>
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        <asp:Label ID="lblbarcode" runat="server" ></asp:Label>
                    </div>
                    <div class="col-sm-6 width-div">
                        <asp:TextBox ID="txtbarcode" runat="server" AutoPostBack="true" AutoComplete="off" class="form-control form-control-sm txt-margin"></asp:TextBox>
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
                        <asp:Label ID="Label2" runat="server" Text="QC Accept: "></asp:Label>
                    </div>
                    <div class="col-sm-2 width-div">
                        <asp:DropDownList ID="ddlQCemp" runat="server" AutoPostBack="true" disabled = "true" class="form-control form-control-sm txt-margin"></asp:DropDownList>
                    </div>
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        <asp:Label ID="lblformloc" runat="server" Text="Form Location: "></asp:Label>
                    </div>
                    <div class="col-sm-2 width-div">
                        <asp:TextBox ID="txtFromloc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 text-right width-div">
                        <asp:Label ID="lbltoloc" runat="server" Text="To Location: "></asp:Label>
                    </div>
                    <div class="col-sm-2 width-div">
                        <asp:TextBox ID="txttoloc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        <asp:Label ID="lblTransferNo" runat="server" Text="Transfer NO: "></asp:Label>
                    </div>
                    <div class="col-sm-2 width-div">
                        <asp:DropDownList ID="ddlTransferNo" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2 text-right width-div">
                        <asp:Label ID="lbltranscode" runat="server" Text="TR Return Code: "></asp:Label>
                    </div>
                    <div class="col-sm-2 width-div">
                        <asp:DropDownList ID="ddlTransCode" runat="server" class="form-control form-control-sm txt-margin"></asp:DropDownList>
                    </div>
                </div>

                <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                    <div class="col-sm-3"> </div>
                    <div class="col-sm-6 text-center">
                        <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm" Text="Process" />&nbsp;&nbsp;
                        <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />
                    </div>
                </div>
            
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnstat" />
                <asp:PostBackTrigger ControlID="txtbarcode" />
                
            </Triggers>
        </asp:UpdatePanel>

        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
            
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
                                 <strong>Status: </strong> <asp:Label ID="lblListStat" runat="server" Text='<%#Eval("Stat")%>'></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>Qty: </strong> <asp:Label ID="lblListQty" runat="server" Text='<%#Eval("TagQty", "{0:N2}")%>'></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                               <strong>Lot: </strong> <asp:Label ID="lblListLot" runat="server" Text='<%#Eval("Lot")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>S/N: </strong> <asp:Label ID="lblListSurNum" runat="server" Text='<%#Eval("SerNum")%>'></asp:Label>
                               
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                               <strong>Type: </strong> <asp:Label ID="lblListType" runat="server"  Text='<%#Eval("Type")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6"></div>
                        </div>


                    </div>
                    </asp:Panel>
                </ItemTemplate>
      
                </asp:ListView>

                <asp:HiddenField ID="HiddenField1" runat="server" />

            </div>

            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="PanelList" />
            </Triggers>

        </asp:UpdatePanel>

            
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

        <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
