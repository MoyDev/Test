<%@ Page Title="Tag Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="TagDetail.aspx.vb" Inherits="SRNProject.TagDetail" %>
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
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span><asp:LinkButton ID="LinkButton1" runat="server">Unposted Job Transaction</asp:LinkButton><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
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
                    <table style="width:100%">
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
                    </table>
                  
                </div>
            </div>
</asp:Content>
