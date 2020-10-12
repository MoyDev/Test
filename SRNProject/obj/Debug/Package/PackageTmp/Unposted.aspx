<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Unposted.aspx.vb" Inherits="ADITransfer.Unposted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .width-div { width: 50%;}
        .width-div2 { width: 35%;}
        .width-div3 { width: 15%;}
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
                    <div class="col-sm-3 text-right width-div">
                        <asp:Label ID="lblbarcode" runat="server" Text="Barcode Job: "></asp:Label>
                    </div>
                    <div class="col-sm-6 width-div">
                         <asp:TextBox ID="txtbarcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" ></asp:TextBox>
                    </div>  
                             
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Date:
                    </div>
                    <div class="col-sm-3 width-div">
                         <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 width-div"></div>           
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Transaction Type:
                    </div>
                    <div class="col-sm-3 width-div">
                        <%--<asp:TextBox ID="txttranstype" runat="server" class="form-control txt-margin" ReadOnly="True"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddltrantype" runat="server" class="form-control form-control-sm txt-margin" disabled = "true" >
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Setup" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Run" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Move" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Indirect" Value="I"></asp:ListItem>
                            <asp:ListItem Text="Machine" Value="C"></asp:ListItem>
                            <asp:ListItem Text="Queue" Value="Q"></asp:ListItem>
                            <asp:ListItem Text="Direct" Value="D"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="HiddenTrantype" runat="server" />
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Job:
                    </div>
                    <div class="col-sm-3 width-div2">
                        <asp:TextBox ID="txtjob" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-1 text-left width-div3">
                        <asp:TextBox ID="txtsuffix" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Item:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:TextBox ID="txtItem" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        WC:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:TextBox ID="txtwc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Operation:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddloper" runat="server" class="form-control form-control-sm txt-margin" ></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Employee:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddlemployee" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                        <asp:HiddenField ID="Hiddenemployee" runat="server" />
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Resource:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddlresource" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Completed:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:TextBox ID="txtqtycomplete" runat="server" AutoPostBack="true" class="form-control form-control-sm txt-margin" style="text-align:right" AutoComplete="off" ></asp:TextBox>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Move to Location:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddlmovetoloc" runat="server" class="form-control form-control-sm txt-margin" ></asp:DropDownList>
                        <asp:HiddenField ID="Hiddenmovetoloc" runat="server" />
                    </div>
                    <div class="col-sm-3 width-div"></div>           
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Lot:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddllot" runat="server" class="form-control form-control-sm txt-margin" disabled="disabled" ></asp:DropDownList>
                        <asp:HiddenField ID="Hiddenlot" runat="server" />
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Total Hour:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:TextBox ID="txttotalhour" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" style="text-align:right"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Prod Shift:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddlshift" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 width-div"></div>            
                </div>

                <div class="row align-items-center">
                    <div class="col-sm-3 text-right width-div">
                        Operator:
                    </div>
                    <div class="col-sm-3 width-div">
                        <asp:DropDownList ID="ddloperator" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                        <%--<asp:TextBox ID="txtoperator" runat="server" class="form-control txt-margin"></asp:TextBox>--%>
                    </div>
                    
                </div>

                <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                    <div class="col-sm-3"> </div>
                    <div class="col-sm-3 text-center">
                        <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm" Text="Process" />&nbsp;&nbsp;

                        <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                        <asp:Button ID="btnnext" runat="server" class="btn btn-warning btn-sm"  Text="Next" />
                    </div>
                </div>

                 <asp:Label ID="txtstrJob" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="txtstrSuffix" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="txtstrOper" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="txtstrWhse" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="txtstrMaxOper" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="txtstrCoProduct" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="lblTransnum" runat="server" ForeColor="White"></asp:Label>
                 <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="White"></asp:Label>
                 <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="White"></asp:Label>
                 <asp:Label ID="Label4" runat="server" Text="Label" ForeColor="White"></asp:Label>
                 <asp:Label ID="Label5" runat="server" Text="Label" ForeColor="White"></asp:Label>
                 <asp:HiddenField ID="hidenJshSchedDrv" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtBarcode" EventName="TextChanged" />
                <asp:PostBackTrigger ControlID="btnnext" />
                <%--<asp:PostBackTrigger ControlID="txtBarcode" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlresource" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlemployee" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtqtycomplete" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txttotalhour" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlshift" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddloperator" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlemployee" EventName="SelectedIndexChanged" />
            </Triggers>
         </asp:UpdatePanel>
            
     </div>
    
</asp:Content>
