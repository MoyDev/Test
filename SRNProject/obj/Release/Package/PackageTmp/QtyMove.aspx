<%@ Page Title="Quantity Move" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="QtyMove.aspx.vb" Inherits="SRNProject.QtyMove" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
      
    </style>

    <script type="text/javascript">

        function scanbarcode(){

            var barcode = document.getElementById('<%=txtbarcode.ClientID%>').value;
            var scantag = document.getElementById('<%=lblbarcode.ClientID%>').innerHTML;
            var f = barcode.substring(0, 1);
            var s3 = barcode.substring(0, 3);
            var s2 = barcode.substring(0, 2);
            var strim = scantag.trim();

            if (strim == 'Barcode Tag:') {

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
                <div class="card-header py-3">
                  <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
                </div>
                <div class="card-body">

                    <div class="row align-items-center">                  
<%--                        <div class="col-sm-12" id="notify">

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

                        </div>--%>
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
                                    <span>Warehouse : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:DropDownList ID="ddlwhse" runat="server" class="form-control form-control-sm txt-margin"></asp:DropDownList>
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Form Location : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:DropDownList ID="ddlformloc" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2 text-right width-div">
                                    <span>To Location : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:DropDownList ID="ddltoloc" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-3 text-right width-div"></div>
                                <div class="col-sm-2 width-div">
                                    <asp:Button ID="btnswitchloc" runat="server" class="btn btn-outline-danger btn-sm" Text="Switch Loc" />
                                </div>
                                
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="row">
                        <asp:ListView id="PanelList" runat="server">
                         <ItemTemplate>
                             <div class="col-sm-12 mt-2">
                                 <div class="card">
                                     <div class="card-body" style="padding: 1.0rem;">
                                     <h6 class="card-title text-primary"><%#Eval("TagID")%></h6>
                                     <span style="font-size:13px;">Item : <%#Eval("Item")%></span><br>
                                     <span style="font-size:13px;">From Location : <%#Eval("FromLoc")%></span><br>
                                     <span style="font-size:13px;">To Location : <%#Eval("ToLoc")%></span><br>
                                     <span style="font-size:13px;">Lot : <%#Eval("Lot")%></span><br>
                                     <span style="font-size:13px;">Qty : <asp:Label ID="lblqty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label></span><br>
                                 </div>
                                 </div>
                             </div>
                         </ItemTemplate>
                     </asp:ListView>
                    </div>
                    <%--<table>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-6 width-div">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                                CssClass="table table-striped table-bordered table-hover" 
                                                ShowFooter="false" ShowHeader="true">
                                         <Columns>
                          
                                            <asp:BoundField HeaderText="Tag ID" DataField="TagID" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true" />
                                            <asp:BoundField HeaderText="From Location" DataField="FromLoc" ReadOnly="true" />
                                            <asp:BoundField HeaderText="To Location" DataField="ToLoc" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Lot" DataField="Lot" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Vendor Lot" DataField="VendLot" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Qty" DataField="Qty" ReadOnly="true" />
                                         </Columns>
                                        </asp:GridView>
                                    </div>
                                
                                </div>
                            </td>
                        </tr>
                    </table>--%>
                  
                </div>
            </div>



        </div>
    <%--<div class="col-lg-12">
    <div class="col-lg-6">
        <div class="card">
          <div class="card-body">
            This is some text within a card body.
          </div>
        </div>
    </div>
      <div class="col-lg-6">
        <div class="card">
          <div class="card-body">
            This is some text within a card body.
          </div>
        </div>
    </div>
        </div>--%>
</asp:Content>
