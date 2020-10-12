<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Co-Product.aspx.vb" Inherits="ADITransfer.Co_Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="bootstrap/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js"></script>

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
        .font-small { font-size:11px;}
        .display-col { display:none;}
        .width-div { width: 50%;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="row align-items-center">
            <div class="col-sm-12">
                <asp:Panel ID="NotPassNotifyPanel" runat="server" CssClass= "alert alert-danger alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="NotPassText" runat="server"></asp:Literal>
                </asp:Panel>
                <asp:Panel ID="PassNotifyPanel" runat="server" CssClass= "alert alert-success alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="PassText" runat="server"></asp:Literal>
                </asp:Panel>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                    <%--<div class="col-sm-3"> </div>--%>
                    <div class="col-sm-3 text-left">
                        <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" Text="Back" />&nbsp;&nbsp;
                        <asp:Button ID="btnhome" runat="server" class="btn btn-primary btn-sm" Text="Home" />&nbsp;&nbsp;

                        <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                        <asp:Button ID="btnnext" runat="server" class="btn btn-warning btn-sm"  Text="Next" />
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                        <Triggers>
                           
                        </Triggers>
                        <ContentTemplate>
                            <div class="col-sm-6 text-left">
                                <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                             CssClass="table table-bordered font-small " OnRowDataBound="Gridview1_RowDataBound">
                            <Columns>

                            <asp:TemplateField HeaderText="Co-Product">  
                                <HeaderStyle Width="230px" Wrap="False" />
                                <ItemStyle Width="230px" Wrap="False" />                                  
                                <ItemTemplate>
                                    <asp:Label ID="lblcoproditem" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Completed">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtqtyComplete" ReadOnly="false" runat="server" Font-Size="10px" AutoComplete="off"
                                                Text='<%# Eval("QtyComplete") %>' AutoPostBack="true" OnTextChanged="txtqtyComplete_TextChanged" CssClass="form-control numeric" 
                                                style="width:100px; text-align:right"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Location">     
                                <HeaderStyle Width="125px" />
                                <ItemStyle HorizontalAlign="Center" Width="125px" />                            
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLoc" runat="server" class="form-control txt-margin font-small" Width="115px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblLoc" runat="server" Text='<%# Eval("Loc") %>' Visible = "false" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Lot">
                                <HeaderStyle Width="155px" />
                                <ItemStyle HorizontalAlign="Center" Width="155px" />                                  
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlLot" runat="server" class="form-control txt-margin font-small" Width="150px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblLot" runat="server" Text='<%# Eval("Lot") %>' Visible = "false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowPointer" runat="server" Text='<%# Eval("RowPointer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                      </asp:GridView>
                            </div>
                             
                        </ContentTemplate>
                    </asp:UpdatePanel>
               
            </div>
            
         </div>

    </div>

    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
