<%@ Page Title="Job Receipt Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="JobReceiptDetail.aspx.vb" Inherits="SRNProject.JobReceiptDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
      
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
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span><asp:LinkButton ID="LinkButton1" runat="server">Job Receipt</asp:LinkButton><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
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
                    </table>
                    <br />--%>
                    <%--<table style="width:100%">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                        CssClass="table table-striped table-bordered table-hover" 
                                        ShowFooter="false" ShowHeader="true">
                                    <Columns>
                          
                                    <asp:BoundField HeaderText="Tag ID" DataField="TagID" ReadOnly="true" />
                                    <asp:BoundField HeaderText="Lot" DataField="Lot" ReadOnly="true" />
                                    <asp:BoundField HeaderText="Qty" DataField="Qty" ReadOnly="true" />
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>
                    </table>--%>

                    <div class="row">
                     <asp:ListView id="PanelList" runat="server">
                         <ItemTemplate>
                             <div class="col-sm-12 mt-2">
                                 <div class="card">
                                     <div class="card-body" style="padding: 1.0rem;">
                                     <h6 class="card-title text-primary"><%#Eval("TagID")%></h6>
                                     <span style="font-size:13px;">Lot : <%#Eval("Lot")%></span><br>
                                     <span style="font-size:13px;">Qty : <asp:Label ID="lblqty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label></span><br>
                                     <span style="font-size:13px;">Label ID : <%#Eval("LabelID")%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:13px;"><asp:CheckBox ID="chkScanLabelItem" class="form-check-input txt-margin" runat="server" Enabled="false"/> <spen>Scan Label Item</spen></span>
                                     <span style="font-size:13px; display:none;"><asp:Label ID="lblScanLabelItem" runat="server" Font-Bold="False" Text='<%#Eval("ScanLabelItem")%>'></asp:Label></span>
                                 </div>
                                 </div>
                             </div>
                         </ItemTemplate>
                     </asp:ListView>
                    </div>
                  
                </div>
            </div>



        </div>
</asp:Content>
