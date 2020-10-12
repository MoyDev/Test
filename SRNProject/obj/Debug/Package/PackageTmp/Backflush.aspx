<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Backflush.aspx.vb" Inherits="ADITransfer.Backflush" %>
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
                <asp:Panel ID="WarningNotifyPanel" runat="server" CssClass= "alert alert-warning alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="WarningText" runat="server"></asp:Literal>
                </asp:Panel>

            </div>
        </div>
        <br />
        <div class="row">
                <div class="col-sm-12">
                    <div class="row align-items-center" style="margin-top:10px; margin-bottom:10px;">
                    <div class="col-sm-3 text-left">
                        <asp:Button ID="btnback" runat="server" class="btn btn-danger btn-sm" Text="Back" />&nbsp;&nbsp;
                        <asp:Button ID="btnhome" runat="server" class="btn btn-primary btn-sm" Text="Home" />&nbsp;&nbsp;

                        <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm"  Text="Reset" />&nbsp;&nbsp;
                        <asp:Button ID="btnnext" runat="server" class="btn btn-warning btn-sm"  Text="Next" />
                    </div>
                </div>
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>--%>
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
                        <Triggers>--%>
                           <%--<asp:PostBackTrigger ControlID="chkSelect" />--%>
                        <%--</Triggers>
                        <ContentTemplate>--%>
                            <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered font-small" Width="800px">
                            <Columns>

                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">  
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>  
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-check-input position-static" Checked= '<%# Eval("Selected") %>' AutoPostBack="true" OnCheckedChanged="SelectCheckBox_CheckedChanged" />  
                                    </ItemTemplate>  
                                </asp:TemplateField>
                        
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOperNum" runat="server" Text='<%# Eval("OperNum") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Seq">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeq" runat="server" Text='<%# Eval("Seq") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lot">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLot" runat="server" Text='<%# Eval("Lot") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtqtyReq" ReadOnly="false" runat="server" Font-Size="10px"
                                                AutoPostBack="true" OnTextChanged="txtqtyReq_TextChanged" Text='<%# Eval("Qty") %>' CssClass="form-control numeric" 
                                                style="width:100px; text-align:right"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="U/M">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUM" runat="server" Text='<%# Eval("UM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="On Hand">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyOnHand" runat="server" Text='<%# Eval("QtyOnHand", "{0:N2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyNeeded" runat="server" Text='<%# Eval("QtyNeeded") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransNum" runat="server" Text='<%# Eval("TransNum") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbltransSeq" runat="server" Text='<%# Eval("TransSeq") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpNum" runat="server" Text='<%# Eval("EmpNum") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWhse" runat="server" Text='<%# Eval("Whse") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" >
                                    <ItemStyle HorizontalAlign="Left" CssClass="display-col" />
                                    <HeaderStyle CssClass="display-col" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLoc" runat="server" Text='<%# Eval("Loc") %>'></asp:Label>
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
                        <%--</ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="chkSelect" />
                            <asp:PostBackTrigger ControlID="txtqtyReq" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
               
            </div>
            
         </div>

        <div class="row">
            <div class="col-sm-12">
                
                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                        <asp:GridView ID="Gridview2" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered font-small " Width="500px">

                            <Columns>
                            <asp:TemplateField HeaderText="Matched" ItemStyle-HorizontalAlign="Center">  
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkMatchSelect" Enabled="false" runat="server" CssClass="form-check-input position-static" Checked= '<%# Eval("Matched") %>' />  
                                </ItemTemplate>  
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operation">                                    
                                <ItemTemplate>
                                    <asp:Label ID="lblMatchOperNum" runat="server" Text='<%# Eval("OperNum") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                 
                            <asp:TemplateField HeaderText="Seq">                                    
                                <ItemTemplate>
                                    <asp:Label ID="lblMatchSeq" runat="server" Text='<%# Eval("Seq") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target Qty">
                                <ItemStyle HorizontalAlign="Right" />                                    
                                <ItemTemplate>
                                    <asp:Label ID="lblMatchTargetQty" runat="server" Text='<%# Eval("TargetQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Selected Qty">
                                <ItemStyle HorizontalAlign="Right" />                                    
                                <ItemTemplate>
                                    <asp:Label ID="lblMatchSelctQty" runat="server" Text='<%# Eval("SelectedQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            </Columns>
                      </asp:GridView>
                   <%-- </ContentTemplate>

                </asp:UpdatePanel>--%>

                
            </div>

         </div>

    </div>
    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>
</asp:Content>
