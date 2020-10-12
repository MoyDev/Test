<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ADI.Master" CodeBehind="Default.aspx.vb" Inherits="ADITransfer._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .card-body-font { font-size:small;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <br />
        <br />

        <div class="row align-items-center">
            <div class="col-sm-9">
                <asp:Panel ID="NotPassNotifyPanel" runat="server" CssClass= "alert alert-danger alert-dismissable" Visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <asp:Literal ID="NotPassText" runat="server"></asp:Literal>
                </asp:Panel>
                <div class="col-sm-3"> </div>

                <div class="col-sm-6">
                    <asp:TextBox ID="txtemp" AutoPostBack="true"  CssClass="form-control" runat="server" placeholder="Employee" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="col-sm-5">
           <asp:ListView id="PanelList" runat="server">
                <ItemTemplate>
                    <div class="card border-dark mb-3">
                        <div class="card-header"><asp:Label ID="Label2" runat="server" Text="WELCOME" Style="font-size:xx-large; font-weight:bold;"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Style="font-size:xx-large; font-weight:bold;"><i class="fa fa-user"></i></asp:Label>
                        </div>
                        <asp:Panel ID="Panel1" class="card-body" style="max-width: 100%;" runat="server">                       
                        <div class="card-text card-body-font">

                        <div class="row">                        
                            <div class="col-sm-6">
                               <asp:Label ID="lblname" runat="server"  Style="font-size:20px; font-weight:bold;" Text='<%#Eval("Name")%>'></asp:Label>
                               
                            </div>  
                                                   
                        </div>

                    </div>
                    </asp:Panel>
                    </div>
                    
                </ItemTemplate>
            </asp:ListView>


            


        </div>


    </div>

    <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="White"></asp:Label>

</asp:Content>
