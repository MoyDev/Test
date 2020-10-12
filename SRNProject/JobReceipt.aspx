<%@ Page Title="Job Receipt" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="JobReceipt.aspx.vb" Inherits="SRNProject.JobReceipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
        .width-div3 { width: 15%;}
    </style>

    <script type="text/javascript">

        function scanbarcode(){

            var barcode = document.getElementById('<%=txtbarcode.ClientID%>').value;
            var scantag = document.getElementById('<%=lblbarcode.ClientID%>').innerHTML;
            var f = barcode.substring(0, 1);
            var s3 = barcode.substring(0, 3);
            var s2 = barcode.substring(0, 2);
            var strim = scantag.trim();

            if (strim == 'Scan Tag:') {

                
                if (f == 'J' || f == 'I') {

                    if ((s3 == 'JOB' || s3 == 'INV' || s3 == 'PUR') && (barcode.length > 10)) {
                        document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                        document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                        document.getElementById('<%=Button1.ClientID%>').click();
                        document.getElementById('<%=Button1.ClientID%>').disabled = true;

                    } else {

                        document.getElementById('<%=txtbarcode.ClientID%>').readOnly = false;
                        document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                        document.getElementById('<%=Button1.ClientID%>').disabled = false;
                    }


                } else if (f == 'P') {

                    if ((s2 == 'PO') && (barcode.length > 10)) {

                        document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                        document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                        document.getElementById('<%=Button1.ClientID%>').click();
                        document.getElementById('<%=Button1.ClientID%>').disabled = true;

                    } else {

                         document.getElementById('<%=txtbarcode.ClientID%>').readOnly = false;
                        document.getElementById("notifyWaiting").innerHTML = "Waiting....";
                        document.getElementById('<%=Button1.ClientID%>').disabled = false;
                    }

                }
            } else {

                    document.getElementById('<%=txtbarcode.ClientID%>').readOnly = false;
                    document.getElementById("notifyWaiting").innerHTML = "Please Enter....";
                    document.getElementById('<%=Button1.ClientID%>').disabled = false;

            }

        }

    </script>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
        <div class="se-pre-con"></div>
        <div class="col-lg-12">
            <div class="card border-primary">
                <div class="card-header py-12">
                    <div class="row">
                        <div class="col-md-9">
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
                        </div>

                        <%--<div class="col-md-3 float-right">
                            <asp:Button ID="btnprocess" runat="server" disabled ="disabled" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false"  />
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm float-right mx-2 mb-0"  Text="Detail" UseSubmitBehavior="false" />
                        </div>--%>
                    </div>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-9"></div>

                        <div class="col-md-3 float-right">
                            <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm float-right mx-2 mb-0"  Text="Detail" UseSubmitBehavior="false" />
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btnprocess" runat="server" disabled ="disabled" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false"  />
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
                                    <div class="col-sm-3"></div>

                                    <div class="col-sm-6 text-right" style="margin-bottom:7px;"> 
                                        <asp:LinkButton runat="server" ID="btnstat" class="btn btn-outline-success btn-block btn-sm" aria-hidden="true" AutoPostBack="true">
                                            <i class="fa fa-arrow-right" aria-hidden="true"></i> <strong>Receive</strong>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <asp:Label ID="lblbarcode" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 width-div">
                                    <asp:TextBox ID="txtbarcode" runat="server" AutoComplete="off" class="form-control form-control-sm txt-margin"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:CheckBox ID="chkLabelItemControl" class="form-check-input txt-margin" runat="server" Enabled="false" Checked="false" /> <spen>Label Item Control</spen>
                                </div>
                                <div style="margin-top:20px;display: none;">
                                    <asp:Button ID="Button1" runat="server" Text="Scan" UseSubmitBehavior="true" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
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
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin datepicker" AutoComplete="off"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Job : </span>
                                    </div>
                                    <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtjob" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>                       
               
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Suffix : </span>
                                    </div>
                                    <div class="col-sm-2 width-div" >
                                    <asp:TextBox ID="txtsuffix" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                          
               
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Item : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:DropDownList ID="ddlitem" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack ="true"></asp:DropDownList>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Operation : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtoper" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Quantity : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtqty" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Location : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtloc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Lot : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtlot" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                                    <div class="col-sm-3"> </div>
                                    <div class="col-sm-6 text-center">
                                        <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm" Text="Process" />&nbsp;&nbsp;

                                        <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                                        <asp:Button ID="btndetail" runat="server" class="btn btn-warning btn-sm"  Text="Detail" />
                                    </div>
                                </div>
                            </td>
                        </tr>--%>
                    </table>
                    <br />

                                <div class="row align-items-center">
                                <div class="col-sm-3"></div>

                                <div class="col-sm-6">
                                   <asp:ListView id="PanelList" runat="server">
                                        <ItemTemplate>
                                            <asp:Panel ID="Panel1" class="card card-body bg-light mb-3" style="max-width: 100%;" runat="server">
                                                <div class="card-text card-body-font">
                                                <%--<div class="row">  
                                                    <div class="col-sm-9 ">
                                                        
                                                    </div>
                                                    <div class="col-sm-3 float-right">
                                                         <asp:CheckBox ID="chkLabelItem" Enabled="false" class="form-check-input txt-margin" runat="server" /><spen>&nbsp;&nbsp;Label Item</spen>
                                                    </div>                          
                                                </div>--%>

                                                <div class="row">                        
                                                    <div class="col-sm-6">
                                                        <strong>Operation: </strong><asp:Label ID="lblOperNum" runat="server" Text='<%#Eval("Oper")%>'></asp:Label>
                                                    </div>  
                                                    <div class="col-sm-6">
                                                         <strong>Item: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                                                    </div>                          
                                                </div>

                                                <div class="row"> 
                                                    <div class="col-sm-6">
                                                         <strong>Qty Release: </strong> <asp:Label ID="lblListQtyRelease" runat="server" Text='<%#Eval("QtyRelease")%>'></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                         <strong>Qty Completed: </strong> <asp:Label ID="lblListQtyComplete" runat="server" Text='<%#Eval("QtyCompleted")%>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row"> 
                                                    <div class="col-sm-6">
                                                         <strong>Qty Receive: </strong> <asp:Label ID="lblListQtyReceive" runat="server" Text='<%#Eval("QtyReceive")%>'></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                         <strong>Qty Remain: </strong> <asp:Label ID="lblListRemain" runat="server" Text='<%#Eval("QtyRemain")%>'></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">                        
                                                    <div class="col-sm-6">
                                                       <strong>Sum Qty: </strong> <asp:Label ID="lblListSumQty" runat="server" Text='<%#Eval("QtySum")%>'></asp:Label>
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
</asp:Content>
