<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Detail.aspx.vb" Inherits="ADITransfer.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

     <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .font-small { font-size:11px;}
        .checkbox-margin { margin-bottom:5px;}
        .width-div { width: 50%;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <br />
    <div class="row">
            <div class="col-sm-12">
                <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                    <%--<div class="col-sm-3"> </div>--%>
                    <div class="col-sm-3 text-left">
                        <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" 
                            Text="Back" Width="100px" />&nbsp;&nbsp;
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                        <Triggers>
                           
                        </Triggers>
                        <ContentTemplate>
                             <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                             CssClass="table table-bordered font-small " Width="650px" >
                            <Columns>

<%--                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Tag ID">  
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />                                  
                                <ItemTemplate>
                                    <asp:Label ID="lblListtagID" runat="server" Text='<%# Eval("TagID") %>' Font-Bold="False" Font-Size="Large"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Lot">  
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" Font-Size="Larger" Font-Bold="True" />                                  
                                <ItemTemplate>
                                    <asp:Label ID="lblListlot" runat="server" Text='<%# Eval("Lot") %>' Font-Bold="False" Font-Size="Large"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S/N">  
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" Font-Bold="True" />                                  
                                <ItemTemplate>
                                    <asp:Label ID="lblListSerNum" runat="server" Text='<%# Eval("SerNum") %>' Font-Size="Large" Font-Bold="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">  
                              
                                <ItemStyle HorizontalAlign="Right" Width="100px" />                                  
                                <ItemTemplate>
                                    <asp:Label ID="lblListQty" runat="server" Text='<%# Eval("Qty" , "{0:N2}") %>' Font-Bold="False" Font-Size="Large"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            </Columns>
                      </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
            </div>
            
         </div>

    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>