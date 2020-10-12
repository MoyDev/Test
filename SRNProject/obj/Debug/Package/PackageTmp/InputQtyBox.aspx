<%@ Page Title="Input Qty Box" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="InputQtyBox.aspx.vb" Inherits="ADITransfer.InputQtyBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>
    
    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .width-div { width: 50%;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblbarcode" runat="server" Text="Scan Quantity: "></asp:Label>
            </div>
            <div class="col-sm-6 width-div">
                <asp:TextBox ID="txtbarcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" ></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblboxbarcode" runat="server" Text="Box Barcode: "></asp:Label>
            </div>
            <div class="col-sm-3 width-div">
                <asp:TextBox ID="txtboxbarcode" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center">
            <div class="col-sm-3 text-right width-div">
                <asp:Label ID="lblqty" runat="server" Text="Quantity: "></asp:Label>
            </div>
            <div class="col-sm-3 width-div">
                <asp:TextBox ID="txtqty" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-3 text-center">
                <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" Text="Back" />&nbsp;&nbsp;

                <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />
            </div>
        </div>
        
    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
