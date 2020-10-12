<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="JobMatlTran.aspx.vb" Inherits="ADITransfer.JobMatlTran" %>
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
        .width-div2 { width: 14%;}
        .width-div3 { width: 22%;}
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
                <asp:Panel ID="WarningNotifyPanel" runat="server" CssClass= "alert alert-warning alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="WarningText" runat="server"></asp:Literal>
                </asp:Panel>

            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3"></div>

            <div class="col-sm-6 text-right" style="margin-bottom:7px;"> 
                <asp:LinkButton runat="server" ID="btnstat" class="btn btn-outline-success btn-block btn-sm" aria-hidden="true" AutoPostBack="true"> <i class="fa fa-arrow-right" aria-hidden="true"></i><strong> Issue</strong> </asp:LinkButton>
            </div>
        </div>

        <div class="row align-items-center checkbox-margin">
            <div class="col-sm-3 text-right width-div">               
            </div>
            <div class="col-sm-4 width-div">
                 <asp:CheckBox ID="chkCancelTag" AutoPostBack="true" class="form-check-input txt-margin" runat="server" />
                 <asp:Label ID="Label1" runat="server" Text="Cancel Tag"></asp:Label>
            </div> 
             <div class="col-sm-3 width-div"></div>            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblbarcode" runat="server" Text="Barcode Job Order: "></asp:Label> 
            </div>
            <div class="col-sm-6 width-div">
                 <asp:TextBox ID="txtBarcode" runat="server" class="form-control form-control-sm txt-margin"  placeholder="Please , Scan Barcode" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                 
            </div>             
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lbldate" runat="server" Text="Date: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div">
                <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin"></asp:TextBox>
            </div>
            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lbljob" runat="server" Text="Job Order: "></asp:Label>
            </div>
            <div class="col-sm-2 width-div3">
            <asp:TextBox ID="tbJob" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
               <%--<asp:TextBox ID="ddljob" runat="server" class="form-control form-control-sm txt-margin"></asp:TextBox>--%>
               
            </div>
            <div class="col-sm-1 text-left width-div2">
               <asp:TextBox ID="txtsuffix" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
              
            </div>
            <div class="col-sm-1 text-lef width-div2">
                 <asp:TextBox ID="txtOperNum" runat="server" 
                    class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
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
                                <strong>Operation: </strong><asp:Label ID="lblOperNum" runat="server" Text='<%#Eval("OperNum")%>'></asp:Label>
                            </div>  
                            <div class="col-sm-6">
                                 <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                            </div>                          
                        </div>

                        <div class="row"> 
                            <div class="col-sm-6">
                                 <strong>Qty Require: </strong> <asp:Label ID="lblListQtyReq" runat="server" Text='<%#Eval("ReqQtyConv", "{0:N2}")%>'></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                 <strong>Qty Issue: </strong> <asp:Label ID="lblListQtyIssue" runat="server" Text='<%#Eval("QtyIssued", "{0:N2}")%>'></asp:Label>
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                               <strong>Qty Remain: </strong> <asp:Label ID="lblListRemain" runat="server" Text="0.00"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <strong>Location: </strong> <asp:Label ID="lblListLoc" runat="server" Text='<%#Eval("Loc")%>'></asp:Label>
                               
                            </div>
                        </div>

                        <div class="row">                        
                            <div class="col-sm-6">
                                <asp:Label ID="lblseq" runat="server" Text='<%#Eval("Sequence")%>' Visible="false"></asp:Label>
                               
                            </div>
                        </div>

                    </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:ListView>

        </div>
        </div>

    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>