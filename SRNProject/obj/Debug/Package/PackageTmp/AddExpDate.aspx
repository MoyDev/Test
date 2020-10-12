<%@ Page Title="Add Expiration Date" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="AddExpDate.aspx.vb" Inherits="ADITransfer.AddExpDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>
    
    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var dp = $('#<%=txtexpdate.ClientID%>');
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
            <div class="col-sm-3 text-right">
                <asp:Label ID="lblexpdate" runat="server" Text="Expiration Date: "></asp:Label>
            </div>
            <div class="col-sm-3">
                <asp:TextBox ID="txtexpdate" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off"></asp:TextBox>
            </div>
        </div>

        <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
            <div class="col-sm-3"> </div>
            <div class="col-sm-3 text-center">
                <asp:Button ID="btnconfirm" runat="server" class="btn btn-success btn-sm" Text="Confirm" />
            </div>
        </div>
        
    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
