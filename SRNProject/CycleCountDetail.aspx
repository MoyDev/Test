<%@ Page Title="Cycle Count Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="CycleCountDetail.aspx.vb" Inherits="SRNProject.CycleCountDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .width-div { width: 100%;}
        .margin-head { margin-bottom:8px;}
      
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
                            <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span><asp:LinkButton ID="LinkButton1" runat="server">Cycle Count</asp:LinkButton><span class="mx-2 mb-0">/</span><strong class="text-black"><%: Page.Title %></strong>  </h6>
                        </div>
                        
                        <%--<div class="col-md-2 float-right">
                            <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                        </div>--%>
                        
                    </div>
                 </div>
                <div class="card-body">
                    <div class="row margin-head">

                        <div class="col-md-10">
                        </div>

                        <div class="col-md-2 float-right">
                            <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm float-right" Text="Back" />
                        </div>
                        
                    </div>

                    <div class="row">
                        <asp:ListView id="PanelList" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-12 mt-2">
                                    <div class="card">
                                        <div class="card-body" style="padding: 1.0rem;">
                                        <h6 class="card-title text-primary"><asp:Label ID="lblItem" runat="server" Text='<%#Eval("item")%>'></asp:Label></h6>
                                        <span style="font-size:13px;">Desc : <%#Eval("item_description")%></span><br>
                                        <span style="font-size:13px;">Lot : <asp:Label ID="lblLot" runat="server" Font-Bold="False" Text='<%#Eval("lot")%>'></asp:Label></span><br>
                                        <span style="font-size:13px;">Tag ID : <%#Eval("tag_id")%></span><br>
                                        <span style="font-size:13px;">Location : <%#Eval("loc")%></span><br>
                                        <span style="font-size:13px;">Qty : <asp:Label ID="lblQty" runat="server" Font-Bold="False" Text='<%#Eval("count_qty")%>'></asp:Label></span><br>
                                        <span style="font-size:13px;">Status : <asp:Label ID="lblstatus" runat="server" Font-Bold="False" Text='<%#Eval("stat")%>'></asp:Label></span><br>
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
                                                ShowFooter="false" ShowHeader="true" Font-Size="Small">
                                             <Columns>
                                                <asp:TemplateField HeaderText="Item">  
                              
                                                    <ItemStyle HorizontalAlign="Left" />                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Font-Bold="False" Text='<%#Eval("item")%>'></asp:Label><br />
                                                        Desc: <asp:Label ID="lblItemDesc" runat="server" Font-Bold="False" Text='<%#Eval("item_description")%>'></asp:Label><br />
                                                        Lot: <asp:Label ID="lblLot" runat="server" Font-Bold="False" Text='<%#Eval("lot")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Tag ID" DataField="tag_id" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Location" DataField="loc" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Qty" DataField="count_qty" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Status" DataField="stat" ReadOnly="true" />
                                             </Columns>
                                            </asp:GridView>
                                        
                            </td>
                        </tr>
                    </table>--%>
                  
                </div>
            </div>



        </div>

</asp:Content>
