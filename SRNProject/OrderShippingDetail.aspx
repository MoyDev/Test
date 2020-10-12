<%@ Page Title="Order Shipping Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="OrderShippingDetail.aspx.vb" Inherits="SRNProject.OrderShippingDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 100%;}
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
        <div class="se-pre-con"></div>
        <div class="col-lg-12">
            <div class="card border-primary">
                <div class="card-header py-12">
                    <div class="row">
                        <div class="col-md-10">
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span><asp:LinkButton ID="LinkButton1" runat="server">Order shipping</asp:LinkButton><span class="mx-2 mb-0">/</span><strong class="text-black"><%: Page.Title %></strong>  </h6>
                        </div>
                        
                        <%--<div class="col-md-2 float-right">
                            <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                        </div>--%>
                        
                    </div>
                 </div>
                
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-10">                           
                        </div>
                        
                        <div class="col-md-2 float-right">
                            <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                        </div>
                        
                    </div>
                    <%--<table>
                        <tr>
                            <td>
                                <div class="row align-items-center">
                                <div class="col-sm-2 width-div">
                                    <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" Text="Back" />
                                </div>
                                
                                </div>
                            </td>
                        </tr>
                    </table>--%>
                    <table style="width:100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblderco" runat="server" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblconum" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                <asp:Label ID="lblcoline" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                <asp:Label ID="lblcorelease" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                       <asp:ListView id="PanelList" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-12 mt-2">
                                <div class="card">
                                    <div class="card-body" style="padding: 1.0rem;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>SRN Item: </strong><asp:Label ID="lblOperNum" runat="server" Text='<%#Eval("Item")%>'></asp:Label></span>
                                            </div>
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Pre-Invoice No.: </strong> <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("InvNum")%>'></asp:Label></span>
                                            </div>
                                        </div>

                                        <div class="row"> 
                                            
                                            <div class="col-sm-6">
                                                 <span style="font-size:13px;"><strong>Cust Item: </strong> <asp:Label ID="lblcustitem" runat="server" Text='<%#Eval("CustItem")%>'></asp:Label></span>
                                            </div>
                                            <div class="col-sm-6">
                                                 <span style="font-size:13px;"><strong>Location To/Plant: </strong> <asp:Label ID="lblListQtyIssue" runat="server" Text='<%#Eval("LocTo")%>'></asp:Label></span>
                                            </div>
                                        </div>

                                        <div class="row"> 
                                            <div class="col-sm-6">
                                                 <span style="font-size:13px;"><strong>Scan Label Item: </strong> &nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:13px;"><asp:CheckBox ID="chkScanLabelItem" class="form-check-input txt-margin" runat="server" Enabled="false"/> <spen>Scan Label Item</spen></span>
                                                 <span style="font-size:13px; display:none;"><asp:Label ID="lbllabelitem" runat="server" Text='<%#Eval("LabelItem")%>'></asp:Label></span>
                                            </div>
                                            
                                        </div>

                                        <div class="row"> 
                                            <div class="col-sm-6">
                                                 <span style="font-size:13px;"><strong>Label Item ID: </strong> <asp:Label ID="lblLabelID" runat="server" Text='<%#Eval("LabelID")%>'></asp:Label></span>
                                            </div>
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>PSD/DN: </strong> <asp:Label ID="Label1" runat="server" Text='<%#Eval("PDS_DN")%>'></asp:Label></span>
                                            </div>
                                        </div>

                                        <div class="row">   
                                            <div class="col-sm-6">
                                                 <span style="font-size:13px;"><strong>Qty Tag: </strong> <asp:Label ID="lblListQtyReq" runat="server" Text='<%#Eval("QtyTag")%>'></asp:Label></span>
                                            </div>
                                            
                                            
                                        </div>

                                        <div class="row">                        
                                            <%--<div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Location: </strong> <asp:Label ID="Label2" runat="server" Text='<%#Eval("Loc")%>'></asp:Label></span>
                                            </div>--%>
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Lot: </strong> <asp:Label ID="lblListRemain" runat="server" Text='<%#Eval("Lot")%>'></asp:Label></span>
                                            </div>
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Pallet No.: </strong> <asp:Label ID="Label3" runat="server" Text='<%#Eval("PalletNo")%>'></asp:Label></span>
                                            </div>
                                        </div>

                                        <div class="row">                        
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Location: </strong> <asp:Label ID="Label2" runat="server" Text='<%#Eval("Loc")%>'></asp:Label></span>
                                            </div>
                                            
                                        </div>

                                        <div class="row">                        
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Tag ID: </strong> <asp:Label ID="Label6" runat="server" Text='<%#Eval("TagID")%>'></asp:Label></span>
                                            </div>
                                            <div class="col-sm-6">
                                                <span style="font-size:13px;"><strong>Check Cust. Doc: </strong> <asp:Label ID="Label7" runat="server" Text='<%#Eval("CheckCustDoc")%>'></asp:Label></span>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            
                        </ItemTemplate>
                    </asp:ListView>
                                            
                                        
                            </td>
                        </tr>
                    </table>
                  
                </div>
            </div>



        </div>

</asp:Content>