<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Downtime.aspx.vb" Inherits="ADITransfer.Downtime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .font-small { font-size:11px;}
        .width-div { width: 50%;}
    </style>

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
                <asp:Label ID="lblbarcode" runat="server" Text="Barcode DownTime Code: "></asp:Label> 
            </div>
            <div class="col-sm-3 width-div">
                 <asp:TextBox ID="txtBarcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                 
            </div> 
             <div class="col-sm-3"></div>            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                Downtime Code:
            </div>
            <div class="col-sm-3 width-div">
                <asp:DropDownList ID="ddldowntimecode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                <%--<asp:TextBox ID="txtdowntime" runat="server" class="form-control txt-margin" ReadOnly="True"></asp:TextBox>--%>
            </div>
            <div class="col-sm-3"></div>            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                Description:
            </div>
            <div class="col-sm-3 width-div">
                <asp:TextBox ID="txtdowntimedesc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
            </div>  
             <div class="col-sm-3 width-div"></div>           
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                Start Time:
            </div>
            <div class="col-sm-3 width-div">
                <asp:TextBox ID="txtstarttime" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off" AutoPostBack="true" ></asp:TextBox>
            </div>
            <div class="col-sm-3 width-div"></div>            
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                End Time:
            </div>
            <div class="col-sm-3 width-div">
                <asp:TextBox ID="txtendtime" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off" AutoPostBack="true" ></asp:TextBox>
            </div>
            <div class="col-sm-3 width-div"></div>            
        </div>

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"></div>
            <div class="col-sm-3 text-center">
                <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" Text="Back" />&nbsp;&nbsp;
                <asp:Button ID="btnhome" runat="server" class="btn btn-primary btn-sm" Text="Home" />&nbsp;&nbsp;
                <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                <asp:Button ID="btnnext" runat="server" class="btn btn-warning btn-sm"  Text="Next" />
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-1"></div>
            <div class="col-sm-6">
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered font-small">
                    <Columns>
                        <asp:BoundField HeaderText="Downtime Code" DataField="ReasonCode" ReadOnly="true" />
                        <asp:BoundField HeaderText="Description" DataField="Description" ReadOnly="true" />
                        <asp:BoundField HeaderText="Start Time" DataField="StartTimeHrs" ReadOnly="true" />
                        <asp:BoundField HeaderText="End Time" DataField="EndTimeHrs" ReadOnly="true" />
                        <asp:BoundField HeaderText="Total Hours" DataField="a_hrs" ReadOnly="true" 
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F3}" />
                    </Columns>
                </asp:GridView>
            </div>
         </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right">
                Total Downtime:&nbsp;&nbsp;<asp:Label ID="lbltotaldowntime" runat="server" Text="0"></asp:Label>
            </div>
            <div class="col-sm-6"></div>           
        </div>
         
     </div>

     <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>

</asp:Content>
