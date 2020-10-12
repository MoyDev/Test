<%@ Page Title="Job Material Transaction Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="JobMatlTranDetail.aspx.vb" Inherits="SRNProject.JobMatlTranDetail" %>
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
                             <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span><asp:LinkButton ID="LinkButton1" runat="server">Job Material Transaction</asp:LinkButton><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
                         </div>

                         <%--<div class="col-md-2 float-right">
                             <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                         </div>--%>

                     </div>
                  
                </div>
                <div class="card-body">
                        <div class="row">
                         <div class="col-md-10"></div>

                         <div class="col-md-2 float-right">
                             <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                         </div>

                     </div>
                    <br />
                    <div class="row">

                        <asp:ListView id="PanelList" runat="server">
                         <ItemTemplate>
                             <div class="col-sm-12 mt-2">
                                 <div class="card">
                                     <div class="card-body" style="padding: 1.0rem;">
                                     <h6 class="card-title text-primary"><%#Eval("TagID")%></h6>
                                     <span style="font-size:13px;">Item : <%#Eval("Item")%></span><br>
                                     <span style="font-size:13px;">Lot : <%#Eval("Lot")%></span><br>
                                     <span style="font-size:13px;">Qty : <asp:Label ID="lblqty" runat="server" Text='<%#Eval("QtyIssue")%>'></asp:Label></span><br>
                                     
                                 </div>
                                 </div>
                             </div>
                         </ItemTemplate>
                     </asp:ListView>

                    </div>
                    <%--<table style="width:100%">
                        <tr>
                            <td>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                                CssClass="table table-striped table-bordered table-hover" 
                                                ShowFooter="false" ShowHeader="true">
                                         <Columns>
                          
                                            <asp:BoundField HeaderText="Tag ID" DataField="TagID" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Lot" DataField="Lot" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Qty" DataField="QtyIssue" ReadOnly="true" />
                                         </Columns>
                                        </asp:GridView>

                            </td>
                        </tr>
                    </table>--%>
                  
                </div>
            </div>



        </div>
</asp:Content>
