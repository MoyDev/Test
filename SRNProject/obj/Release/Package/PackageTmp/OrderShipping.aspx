<%@ Page Title="Order Shipping" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="OrderShipping.aspx.vb" Inherits="SRNProject.OrderShipping" %>
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

                    if ((msg.substring(0, 4) == '#240') || (msg.substring(0, 4) == '#450') || (msg.substring(0, 4) == '#310')) {
                        window.location = "Menu.aspx";
                    }

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

                       <%-- <div class="col-md-3 float-right">
                            
                            
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" disabled ="disabled" Text="Process" UseSubmitBehavior="false" />
                        </div>--%>
                    </div>
                </div>
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-9">
                           
                        </div>

                        <div class="col-md-3 float-right">
                            <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                            <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" disabled ="disabled" Text="Process" UseSubmitBehavior="false" />
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
                                        <asp:LinkButton runat="server" ID="btnstat" class="btn btn-outline-success btn-block" aria-hidden="true" AutoPostBack="true">
                                            <i class="fa fa-arrow-right" aria-hidden="true"></i> <strong>Order Shipping</strong>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-center checkbox-margin">
                                    <div class="col-sm-3 text-right width-div">               
                                    </div>
                                    <div class="col-sm-4 width-div">
                                         &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkCancelTag" AutoPostBack="true" class="form-check-input txt-margin" runat="server" /><spen>Cancel Tag</spen>
                                    </div> 
                                     <div class="col-sm-3 width-div"></div>            
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
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
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Order Pick List No : </span>
                                    </div>
                                    <div class="col-sm-2 width-div">
                                        <asp:TextBox ID="txtpicklistno" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>                       
                                    </div>
                                    <div class="col-sm-3 text-right width-div">
                                    </div>
                                    <div class="col-sm-2 width-div">
                                        <asp:CheckBox ID="ChkPreInv" runat="server" class="form-check-input txt-margin" Enabled="false" /> <spen>Pre-Invoice</spen>
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Order Shipping No : </span>
                                    </div>
                                    <div class="col-sm-2 width-div">
                                        <asp:TextBox ID="txtshippingno" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>                       
               
                                    </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>Customer : </span>
                                    </div>
                                    <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtcustnum" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>                       
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                    </div>
                                    <div class="col-sm-3 width-div">
                                    <asp:TextBox ID="txtdescription" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>                       
               
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="row">
                                <div class="col-sm-3 text-right width-div">
                                    <span>Date : </span>
                                </div>
                                <div class="col-sm-2 width-div">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin datepicker" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                                </div>
                                </div>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <span>CO Return Code : </span>
                                    </div>
                                    <div class="col-sm-2 width-div" >
                                    <asp:DropDownList ID="ddlreturncode" runat="server" disabled = "disabled" AutoPostBack="true" class="form-control form-control-sm txt-margin"></asp:DropDownList>
                          
               
                                    </div>

                                </div>
                            </td>
                        </tr>                        
                        
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">

                                    </div>
                                    <div class="col-sm-2 width-div" >
                                        <asp:Label ID="lblValidate" runat="server" Text="Validate" Visible="false"></asp:Label>
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <asp:Label ID="lblValidateCustItem" runat="server" Text="Cust Item : " Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 width-div" >
                                        <asp:TextBox ID="txtValidateCustItem" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" Visible="false"></asp:TextBox>
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <asp:Label ID="lblValidateCustPo" runat="server" Text="Cust Po : " Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 width-div" >
                                        <asp:TextBox ID="txtValidateCustPo" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" Visible="false"></asp:TextBox>
                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-sm-3 text-right width-div">
                                        <asp:Label ID="lblValidateQty" runat="server" Text="Qty : " Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 width-div" >
                                        <asp:TextBox ID="txtValidateQty" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True" Visible="false"></asp:TextBox>
                                    </div>

                                </div>
                            </td>
                        </tr>
                        
                    </table>

                        <div class="row align-items-center">
                         <div class="col-sm-3"></div>
                         <div class="col-sm-6">
                            <asp:ListView id="PanelList" runat="server">
                             <ItemTemplate>
                                 <div class="col-sm-12 mt-2">
                                     <div class="card">
                                         <div class="card-body" style="padding: 1.0rem;">
                                         <h6 class="card-title text-primary">
                                             <asp:Label ID="lblderconum" runat="server" Text='<%#Eval("DerCO")%>'></asp:Label>
                                             <asp:Label ID="lblconum" runat="server" Font-Bold="False" Text='<%#Eval("CoNum")%>' Visible="false"></asp:Label>
                                             <asp:Label ID="lblcoline" runat="server" Font-Bold="False" Text='<%#Eval("CoLine")%>' Visible="false"></asp:Label>
                                             <asp:Label ID="lblcorelease" runat="server" Font-Bold="False" Text='<%#Eval("CoRelease")%>' Visible="false"></asp:Label>
                                         </h6>          
                                         <span style="font-size:13px;">Cust Item : <%#Eval("CustItem")%></span><br>
                                         <span style="font-size:13px;">Cust PO. : <%#Eval("CustPO")%></span><br>
                                         <span style="font-size:13px;">Qty Ordered : <asp:Label ID="lblQtyOrdered" runat="server" Font-Bold="False" Text='<%#Eval("QtyOrder")%>'></asp:Label> | Sum Qty : <asp:Label ID="lblSumQty" runat="server" Font-Bold="False" Text='<%#Eval("QtySum")%>'></asp:Label> | Remain : <asp:Label ID="lblQtyRemain" runat="server" Font-Bold="False" Text='<%#Eval("QtyRemain")%>'></asp:Label></span><br>
                                         <span style="font-size:13px;"class="text-danger"><asp:LinkButton ID="lnkDetail" OnClick="Display" runat="server"  ToolTip="Click to view details" Text='Detail'></asp:LinkButton></span>
                                     </div>
                                     </div>
                                 </div>
                             </ItemTemplate>
                         </asp:ListView>
                         </div>

                        </div>


                    <%--<div class="row align-items-center">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-8 width-div">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                    CssClass="table table-striped table-bordered table-hover" 
                                    ShowFooter="false" ShowHeader="true" Font-Size="Small">
                                <Columns>
                                <asp:TemplateField HeaderText="CO">  
                              
                                    <ItemStyle HorizontalAlign="Left" />                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lblderconum" runat="server" Font-Bold="False" Text='<%#Eval("DerCO")%>'></asp:Label><br />
                                        Item : <asp:Label ID="lblItem" runat="server" Font-Bold="False" Text='<%#Eval("Item")%>'></asp:Label>
                                        <asp:Label ID="lblconum" runat="server" Font-Bold="False" Text='<%#Eval("CoNum")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblcoline" runat="server" Font-Bold="False" Text='<%#Eval("CoLine")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblcorelease" runat="server" Font-Bold="False" Text='<%#Eval("CoRelease")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Cust Item" DataField="CustItem" ReadOnly="true" ItemStyle-CssClass="text-left" />
                                <asp:BoundField HeaderText="Cust PO." DataField="CustPO" ReadOnly="true" ItemStyle-CssClass="text-left" />
                                <asp:BoundField HeaderText="Qty Ordered" DataField="QtyOrder" ReadOnly="true" ItemStyle-CssClass="text-right" />
                                <asp:BoundField HeaderText="Sum Qty" DataField="QtySum" ReadOnly="true" ItemStyle-CssClass="text-right" />
                                <asp:BoundField HeaderText="Remain" DataField="QtyRemain" ReadOnly="true" ItemStyle-CssClass="text-right" />
                                <asp:TemplateField HeaderText="Label Item">
                                    <ItemStyle HorizontalAlign="Center" />                                  
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkLabelItem" runat="server" CssClass="form-check-input position-static" Enabled="false" Checked= '<%# Eval("LabelItem") %>' />  
                                     </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">  
                              
                                    <ItemStyle HorizontalAlign="Left" />                                  
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetail" OnClick="Display" runat="server"  ToolTip="Click to view details" Text='Detail'></asp:LinkButton>
                                        
                                    </ItemTemplate>

                                </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                                
                    </div>--%>

                    </div>
            </div>
            <asp:HiddenField ID="HiddenField2" runat="server" />

        </div>
</asp:Content>
