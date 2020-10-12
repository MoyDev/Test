<%@ Page Title="Generate GRN" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="GenerateGRN.aspx.vb" Inherits="SRNProject.GenerateGRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
        .width-div3 { width: 15%;}
    </style>

    <script>

        function ShowSweetAlert(type, msg, icon) {
            Swal.fire(
                type,
                msg,
                icon
            ).then(function() {
                window.setTimeout(function () { 
                document.getElementById('<%=txtbarcode.ClientID%>').focus(); 
            }, 0);});

        }

       </script>


    <script type="text/javascript">

        function scanbarcode(){

            var barcode = document.getElementById('<%=txtbarcode.ClientID%>').value;
            var f = barcode.substring(0, 1);
            var s3 = barcode.substring(0, 3);
            var s2 = barcode.substring(0, 2);

            if (f == 'J' || f == 'I')  {

                if ((s3 == 'JOB' || s3 == 'INV' || s3 == 'PUR') && (barcode.length > 10)) {
                    document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                    document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                    document.getElementById('<%=Button1.ClientID%>').click();
                    document.getElementById('<%=Button1.ClientID%>').disabled = true;

                }

            } else if (f == 'P') {

                if ((s2 == 'PO') && (barcode.length > 10)) {

                    document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                    document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                    document.getElementById('<%=Button1.ClientID%>').click();
                    document.getElementById('<%=Button1.ClientID%>').disabled = true;

                } 

            } else {

                    document.getElementById('<%=txtbarcode.ClientID%>').readOnly = false;
                    document.getElementById("notifyWaiting").innerHTML = "Please Enter....";
                    document.getElementById('<%=Button1.ClientID%>').disabled = false;

            }

        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <br />
        <div class="se-pre-con"></div>
        <div class="col-lg-12">
            <div class="card border-primary">
                <div class="card-header py-12">
                  <div class="row">
                        <div class="col-md-8">
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
                        </div>

                        <%--<div class="col-md-4 float-right">
                            <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm float-right mx-2 mb-0"  Text="Detail" UseSubmitBehavior="false" />
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false" />
                            
                        </div>--%>
                    </div>
                
                </div>

                <div class="card-body">

                    <div class="row">
                        <div class="col-md-8">
                        </div>

                        <div class="col-md-4 float-right">
                            <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm float-right mx-2 mb-0"  Text="Detail" UseSubmitBehavior="false" />
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false" />
                             
                            
                        </div>
                    </div>

                    <div class="row align-items-center">                  
                    </div>

                    <table  width="100%">
                        <tr>
                            <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div"></div>
                                <div class="col-sm-6 width-div">
                                    <p id="notifyWaiting"></p>
                                </div>
                            </div>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <asp:Label ID="lblbarcode" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-6 width-div">
                                    <asp:TextBox ID="txtbarcode" runat="server" AutoComplete="off" class="form-control form-control-sm txt-margin"></asp:TextBox>
                                </div>
                                <div style="margin-top:20px;display: none;">
                                    <asp:Button ID="Button1" runat="server" Text="Scan" UseSubmitBehavior="true" />
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Date : </span>
                                    </div>
                                    <div class="col-sm-6 width-div">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" AutoComplete="off"></asp:TextBox>                       
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>GRN Type : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:DropDownList ID="ddlGrnType" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" required></asp:DropDownList>
                                </div>
                                <div class="col-sm-2 text-right width-div">
                                    <span>GRN NO : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtGRN" runat="server" class="form-control form-control-sm txt-margin" placeholder="AUTO GENERATE" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Vendor : </span>
                                    </div>
                                    <div class="col-sm-6 width-div">
                                    <asp:TextBox ID="txtvendname" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" AutoComplete="off"></asp:TextBox>                       
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Vendor Invoice : </span>
                                    </div>
                                    <div class="col-sm-6 width-div">
                                    <asp:TextBox ID="txtvendVendInv" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off"></asp:TextBox>
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Whse : </span>
                                    </div>
                                    <div class="col-sm-6 width-div">
                                    <asp:TextBox ID="txtWhse" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">

                                    </div>
                                    <div class="col-sm-6 width-div">
                                        <asp:Button ID="btnvendorlot" runat="server" class="btn btn-primary btn-sm mb-0"  Text="VendorLot" UseSubmitBehavior="false" />
               
                                    </div>

                                </div>
                                
                            </td>
                        </tr>
                    </table>
                    <br />

                    <div class="row align-items-center">

                <div class="col-sm-3"></div>

                <div class="col-sm-6">
                    <asp:ListView id="PanelList" runat="server">
                        <ItemTemplate>
                            <asp:Panel ID="Panel1" class="card card-body bg-light mb-3" style="max-width: 100%;" runat="server">
                                <div class="card-text card-body-font">
                                <div class="row">                        
                                    <div class="col-sm-6">
                                        <strong>PO: </strong><asp:Label ID="lblOperNum" runat="server" Text='<%#Eval("DerPo")%>'></asp:Label>
                                    </div>  
                                    <div class="col-sm-6">
                                        <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                                    </div>                          
                                </div>

                                <div class="row"> 
                                    <div class="col-sm-6">
                                            <strong>Qty Order: </strong> <asp:Label ID="lblListQtyOrder" runat="server" Text='<%#Eval("QtyOrdered")%>'></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                            <strong>Qty Received: </strong> <asp:Label ID="lblListQtyReceived" runat="server" Text='<%#Eval("QtyReceived")%>'></asp:Label>
                                    </div>
                                </div>

                                <div class="row">                        
                                    <div class="col-sm-6">
                                        <strong>Qty Require: </strong> <asp:Label ID="lblListRequire" runat="server" Text='<%#Eval("QtyRequire")%>'></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <strong>Sum Qty: </strong> <asp:Label ID="lblListSumQty" runat="server" Text='<%#Eval("SumQty")%>'></asp:Label>
                                    </div>
                                </div>

                                <div class="row">                        
                                    <div class="col-sm-6">
                                        <strong>Location: </strong> <asp:Label ID="lblListLot" runat="server" Text='<%#Eval("Loc")%>'></asp:Label>
                                    </div>
                                    
                                </div>

                            </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:ListView>

            </div>
            </div>



                    

                    </div>

                
            </div>



        </div>
    <asp:Label ID="lblBarcodeInvNum" runat="server" ForeColor="White"></asp:Label>
    <asp:Label ID="lblBarcodeInvDate" runat="server" ForeColor="White"></asp:Label>
    <asp:Label ID="lblBarcodePoNum" runat="server" ForeColor="White"></asp:Label>
    <asp:Label ID="lblBarcodePoLine" runat="server" ForeColor="White"></asp:Label>
    <asp:Label ID="lblBarcodePoRelease" runat="server" ForeColor="White"></asp:Label>
    <asp:Label ID="lblBarcodeQtyDelivery" runat="server" ForeColor="White"></asp:Label>
</asp:Content>
