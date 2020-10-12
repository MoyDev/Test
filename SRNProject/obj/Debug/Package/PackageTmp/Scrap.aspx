<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Scrap.aspx.vb" Inherits="ADITransfer.Scrap" %>
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

                <div class="row align-items-center font-Nav">
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
                    <asp:Label ID="lblbarcode" runat="server" Text="Barcode Scrap Code: "></asp:Label> 
                </div>
                <div class="col-sm-3 width-div">
                        <asp:TextBox ID="txtBarcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                 
                </div> 
                    <div class="col-sm-3 width-div"></div>            
            </div>

            <div class="row align-items-center">
                <div class="col-sm-3 text-right width-div">
                    Item:
                </div>
                <div class="col-sm-3 width-div">
                        <%--<asp:TextBox ID="txtItem" runat="server" class="form-control txt-margin" ReadOnly="True"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlitem" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"  ></asp:DropDownList>
                </div> 
                    <div class="col-sm-3 width-div"></div>            
            </div>

            <div class="row align-items-center">
                <div class="col-sm-3 text-right width-div">
                    Scrap Code:
                </div>
                <div class="col-sm-3 width-div">
                    <%--<asp:TextBox ID="txtscrapcode" runat="server" class="form-control txt-margin" ReadOnly="True"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlscrapcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                </div>
                <div class="col-sm-3 width-div"></div>            
            </div>

            <div class="row align-items-center">
                <div class="col-sm-3 text-right width-div">
                    Scrap Description:
                </div>
                <div class="col-sm-3 width-div">
                    <asp:TextBox ID="txtscrapdesc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                </div> 
                    <div class="col-sm-3 width-div"></div>            
            </div>

            <div class="row align-items-center">
                <div class="col-sm-3 text-right width-div">
                    Scrap Qty:
                </div>
                <div class="col-sm-3 width-div">
                    <asp:TextBox ID="txtscrapqty" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" ></asp:TextBox>
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
                        <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true"  />
                        <asp:BoundField HeaderText="Scrap Code" DataField="ReasonCode" ReadOnly="true"  />
                        <asp:BoundField HeaderText="Description" DataField="Description" ReadOnly="true" />
                        <asp:BoundField HeaderText="Scrap Qty" DataField="Qty" ReadOnly="true" 
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}"  />
                    </Columns>
                </asp:GridView>
            </div>
         </div>

         <div class="row align-items-center">
            <div class="col-sm-3 text-right">
                Total Scrap:&nbsp;&nbsp;<asp:Label ID="lbltotalscrap" runat="server" Text="0"></asp:Label>
            </div>
            <div class="col-sm-6"></div>           
        </div>
         
     </div>
     <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
