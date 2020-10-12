<%@ Page Title="Unposted Job Transaction" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="Unposted.aspx.vb" Inherits="SRNProject.Unposted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .txt-margin { margin-bottom:5px;}
        .txt-margin2 { margin-bottom:10px;}
        .width-div { width: 50%;} 
        .txt-center {text-align:center;}
        .display-col { display:none;}
    </style>

    <script type="text/javascript">

        function scanbarcode() {

            var barcode = document.getElementById('<%=txtbarcode.ClientID%>').value;
            var label = document.getElementById('<%= lblbarcode.ClientID %>').innerHTML;


            if(label.value == 'Barcode Completed: '){
                document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                document.getElementById("notifyWaiting").innerHTML = "Waiting.....";
                document.getElementById('<%=Button1.ClientID%>').click();
                document.getElementById('<%=Button1.ClientID%>').disabled = true;
                
            }else{
                document.getElementById('<%=txtbarcode.ClientID%>').readOnly = true;
                document.getElementById("notifyWaiting").innerHTML = "Waiting.....";
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
                     <%--<div class="col-md-3 float-right">
                         <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                         <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false" />                                 
                     </div>--%>
                </div>
                  
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>        
           
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" >
            <ContentTemplate>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-9">
                        
                    </div>
                     <div class="col-md-3 float-right">
                         <asp:Button ID="btnreset" runat="server" class="btn btn-info btn-sm float-right mx-2 mb-0"  Text="Reset" UseSubmitBehavior="false" />
                         <asp:Button ID="btnprocess" runat="server" class="btn btn-success btn-sm float-right mx-2 mb-0" Text="Process" UseSubmitBehavior="false" />                                 
                     </div>
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
                                <div class="row align-items-right">
                                <div class="col-sm-3 text-right width-div">
                                    <asp:Label ID="lblbarcode" runat="server" Text="Barcode Job: "></asp:Label>
                                </div>
                                <div class="col-sm-5 width-div">
                                        <asp:TextBox ID="txtbarcode" runat="server" AutoComplete="off" class="form-control form-control-sm txt-margin"></asp:TextBox>
                                    
                                    </div>
                            
                                    <div style="margin-top:20px;display:none;">
                                        <asp:Button ID="Button1" runat="server" Text="Scan" UseSubmitBehavior="true" />
                                         <asp:HiddenField ID="ActiveTabTextBox" runat="server" Value= "1" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-right">
                                <div class="col-sm-3 text-right width-div">
                                </div>
                                <div class="col-sm-2 text-left width-div">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkcancel" class="form-check-input txt-margin" runat="server" /><span>Cancel Tag</span>
                                </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row align-items-right">
                                <div class="col-sm-2 text-left width-div">
                                    <span>User Scan Tag : </span>
                                </div>
                                <div class="col-sm-10 text-left width-div">
                                    <asp:Label ID="lblqtyscan" runat="server" Text="0"></asp:Label> <asp:Label ID="lblum" runat="server"></asp:Label>
                                </div>
                                </div>
                            </td>
                        </tr>
                        </table>

                    <%--<div class="row align-items-right txt-margin2"></div>
                    <div class="card" style="display:none">
                                <div class="row align-items-center">
                                    <div class="col-sm-12 text-left">
                                    <div class="card-body">
                                        <legend>Information</legend>
                                        <div class="row">
                                            <div class="col-sm-1 text-right width-div">
                                                <span>Job : </span>
                                            </div>
                                            <div class="col-sm-3 text-left width-div">
                                                <asp:Label ID="lbljob" runat="server" ></asp:Label> <asp:Label ID="Label1" runat="server" Text="-" Visible="False"></asp:Label> 
                                                <asp:Label ID="lblSuffix" runat="server" ></asp:Label> <asp:Label ID="Label2" runat="server" Text="-" Visible="False"></asp:Label>
                                                <asp:Label ID="lbloper" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-2 text-right width-div">
                                                <span>Qty Release : </span>
                                            </div>
                                            <div class="col-sm-2 text-left width-div">
                                                <asp:Label ID="lblQtyRelease" runat="server" ></asp:Label>
                                            </div>
                                            <div class="col-sm-2 text-right width-div">
                                                <span>Qty Complete : </span>
                                            </div>
                                            <div class="col-sm-2 text-left width-div">
                                                <asp:Label ID="lblQtyComplete" runat="server" ></asp:Label>
                                            </div>
                                            
                                        </div>
                                        
                                    </div>
                                    </div>
                                </div>
                    </div>--%>
                    

                    <%----------NAV---------%>
                    <div class="row align-items-right txt-margin2"></div>

                    <div class="row align-items-center">
                        <div class="col-sm-12 text-left">
                        <ul class="nav nav-pills" id="myTab">
                            <li id="l_s1" class='nav-item active'>
                                <!-- <a href="#s1" class='nav-link active' data-toggle="tab" id="1">Main</a> -->
                                <a href="#s1" style="font-size: 8.5px;" class="btn btn-primary btn-circle ml-1" data-toggle="tab" id="1">
                                  Main
                                </a>
                                <%--<asp:LinkButton ID="LinkButton1"  data-id="1" runat="server" class='nav-link active' data-toggle='tab' href='#s1'>Main</asp:LinkButton>--%>

                            </li>

                            <li id="l_s2" class='nav-item'>
                                <%--<a href="#s2" style="font-size: 8.5px;" class='nav-link' data-toggle="tab" id="2">Completed</a>--%>
                                <a href="#s2" style="font-size: 8.5px;" class="btn btn-primary btn-circle ml-1" data-toggle="tab" id="2">
                                  Completed
                                </a>
                                <%--<asp:LinkButton ID="LinkButton2"  data-id="2" runat="server" class='nav-link' data-toggle='tab' href='#s2'>Completed</asp:LinkButton>--%>

                            </li>
                            <li id="l_s3" class='nav-item'>
                                <%--<a href="#s3" style="font-size: 8.5px;" class='nav-link' data-toggle="tab" id="3">Scrapped</a>--%>
                                <a href="#s3" style="font-size: 8.5px;" class="btn btn-primary btn-circle ml-1" data-toggle="tab" id="3">
                                  Scrapped
                                </a>
                                <%--<asp:LinkButton ID="LinkButton3" data-id="3" runat="server" class='nav-link' data-toggle='tab' href='#s3'>Scrapped</asp:LinkButton>--%>
     
                            </li>
                            <li id="l_s4" class='nav-item'>
                                <%--<a href="#s4" style="font-size: 8.5px;" class='nav-link' data-toggle="tab" id="4">Backflush</a>--%>
                                <a href="#s4" style="font-size: 8.5px;" class="btn btn-primary btn-circle ml-1" data-toggle="tab" id="4">
                                  Backflush
                                </a>
                                <%--<asp:LinkButton ID="LinkButton4" data-id="4" runat="server" class='nav-link' data-toggle='tab' href='#s4'>Backflush</asp:LinkButton>--%>
                  
                            </li>
                            <li id="l_s5" class='nav-item'>
                                <%--<a href="#s5" style="font-size: 8.5px;" class='nav-link' data-toggle="tab" id="5">Downtime</a>--%>
                                <a href="#s5" style="font-size: 8.5px;" class="btn btn-primary btn-circle ml-1" data-toggle="tab" id="5">
                                  Downtime
                                </a>
                                <%--<asp:LinkButton ID="LinkButton5" data-id="5" runat="server" class='nav-link' data-toggle='tab' href='#s5'>Downtime</asp:LinkButton>--%>
                       
                            </li>
                            
                        </ul>

                        <!-- Tab panes -->
                        <div class="row align-items-right txt-margin2"></div>
                        
                        <div class="tab-content">

                            <div id="s1" class="tab-pane fade in active" >
                                <div class="card">
                                <div class="row align-items-center">
                                    <div class="col-sm-12 text-left">
                                        <div class="card-body">
                                            <legend>Input</legend>
                                            <div class="row">
                                                <div class="col-md-6 float-left width-div">
                                                    <asp:LinkButton runat="server" ID="btnPrevious" class="btn btn-link btn-sm float-left mx-2 mb-0" AutoPostBack="true" UseSubmitBehavior="false" >
                                                        <i class="fas fa-arrow-circle-left" aria-hidden="true"></i> <strong>Previous</strong>
                                                    </asp:LinkButton>
                                                </div>
                                                 <div class="col-md-6 float-right width-div">

                                                     <asp:LinkButton runat="server" ID="btnNext" class="btn btn-link btn-sm float-right mx-2 mb-0" AutoPostBack="true" UseSubmitBehavior="false" >
                                                        <strong>Next</strong> <i class="fas fa-arrow-circle-right" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                     
                                                 </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Job : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtjob" runat="server" class="form-control form-control-sm txt-margin datepicker" AutoComplete="off" ReadOnly="True"></asp:TextBox>
                                                    <asp:Label ID="lbljob" runat="server"  Visible="False"></asp:Label>
                                                    <asp:Label ID="lblSuffix" runat="server"  Visible="False"></asp:Label>
                                                    <asp:Label ID="lbloper" runat="server"  Visible="False"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Transaction Type : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddltrantype" runat="server" class="form-control form-control-sm txt-margin" disabled = "disabled" EnableViewState="true" >
                                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Setup" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Run" Value="R"></asp:ListItem>
                                                        <asp:ListItem Text="Move" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="Indirect" Value="I"></asp:ListItem>
                                                        <asp:ListItem Text="Machine" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="Queue" Value="Q"></asp:ListItem>
                                                        <asp:ListItem Text="Direct" Value="D"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Trans Date : </span>
                                                </div>
                                                <div class="col-sm-2 width-div">
                                                    <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm txt-margin datepicker" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Employee : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlemployee" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                            <div class="col-sm-5 text-right width-div">
                                                <span>WC : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtwc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Resource : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlResource" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Production Line : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtProdLine" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Scheduling Shift : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlSchedulingShift" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                    <span>Production Shift : </span>
                                                </div>
                                                <div class="col-sm-2 text-right width-div">
                                                    <asp:RadioButtonList ID="rdDayNight" runat="server" RepeatColumns="2" CellPadding="8">
                                                        <asp:ListItem Value="D"><sped>&nbsp;Day</sped></asp:ListItem>
                                                        <asp:ListItem Value="N"><sped>&nbsp;Night</sped></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Start Time : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtStartTime" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off">00:00</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>End Time : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtEndTime" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off">00:00</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Total Hour : </span>
                                                </div>
                                                <div class="col-sm-2 text-right width-div">
                                                    <asp:TextBox ID="txttotalhour" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                <span>Move To Location : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlmovetoloc" runat="server" class="form-control form-control-sm txt-margin" disabled = "true" EnableViewState="true" ></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </div>

                            <div id="s2" class="tab-pane fade" >
                                    
                                <div class="card">
                                <div class="row align-items-center">
                                    <div class="col-sm-12 text-left">
                                        <div class="card-body">
                                        <%--<h5 class="card-title txt-center">Input</h5>--%>
                                            <legend>Complete</legend>  

                                            <table style="width:100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                                            CssClass="table table-striped table-bordered table-hover" 
                                                            ShowFooter="false" ShowHeader="true" >
                                                        <Columns>                         
                                                        <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true" />
                                                         <asp:TemplateField HeaderText="Completed">
                                                            <HeaderStyle />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemComplete" runat="server" Text='<%# Eval("QtyComplete") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Lot">
                                                            <HeaderStyle />
                                                            <ItemStyle HorizontalAlign="Center" />                                  
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlLot" runat="server" class="form-control form-control-sm txt-margin">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                        </table>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            <div id="s3" class="tab-pane fade" >

                                <div class="card">
                                <div class="row align-items-center">
                                    <div class="col-sm-12 text-left">
                                        <div class="card-body">
                                              <legend>Scrapped</legend>  
                                            <div class="row">
                                                <%--<div class="col-sm-2 text-right width-div">
                                                    <span>Item : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlitem" runat="server" class="form-control form-control-sm txt-margin" ></asp:DropDownList>
                                                </div>--%>

                                                <div class="col-sm-6 text-right width-div">
                                                    <span>Scrap Code : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddlscrapcode" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true"  ></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-6 text-left width-div">
                                                    <span>Total Scrap : </span> <asp:Label ID="lbltotalscrap" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div class="col-sm-6 text-left width-div">

                                                
                                                </div>



                                                <%--<div class="col-sm-2 text-right width-div">
                                                    <span>Description : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtscrapdesc" runat="server" class="form-control form-control-sm txt-margin" ReadOnly="True"></asp:TextBox>
                                                </div>--%>

                                                <%--<div class="col-sm-2 text-right width-div">
                                                    <span>Scrap Qty : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtscrapqty" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" ></asp:TextBox>
                                                </div>--%>
                                            </div>

                                            <table style="width:100%">
                                            <tr>
                                                <td>
                                                    
                                                       <asp:GridView ID="GridView5" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-bordered table-hover">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true"  />
                                                            <asp:BoundField HeaderText="Scrap Code" DataField="ReasonCode" ReadOnly="true"  />
                                                            <asp:BoundField HeaderText="Description" DataField="DerDescription" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Scrap Qty">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                           </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            </table>
         
                                        </div>
                                    </div>
                                </div>
                                </div>

                            </div>
                            <div id="s4" class="tab-pane fade">

                                <div class="card">
                                <div class="row align-items-center">
                                    <div class="col-sm-12 text-left">
                                        <div class="card-body">
                                              <legend>Backflush</legend>  
                                            <table style="width:100%">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" Style="display:none" UseSubmitBehavior="false" />
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" 
                                                            CssClass="table table-striped table-bordered table-hover" 
                                                            ShowFooter="false" ShowHeader="true" >
                                                        <Columns>                         
                                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">  
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>  
                                                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-check-input position-static" Checked= '<%# Eval("Selected") %>' AutoPostBack="true" OnCheckedChanged="SelectCheckBox_CheckedChanged" />  
                                                            </ItemTemplate>  
                                                        </asp:TemplateField>
                        
                                                        <asp:BoundField HeaderText="Operation" DataField="OperNum" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Seq" DataField="Seq" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Lot" DataField="Lot" ReadOnly="true" />

                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtqtyReq" ReadOnly="false" runat="server" Font-Size="10px"
                                                                        AutoPostBack="true" Text='<%# Eval("Qty") %>' CssClass="form-control numeric" 
                                                                        style="width:100px; text-align:right"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField HeaderText="UM" DataField="UM" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Item" DataField="Item" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Description" DataField="ItemDesc" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="On Hand" DataField="QtyOnHand" ReadOnly="true">
                                                           <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Quantity" DataField="QtyNeeded" ReadOnly="true">
                                                             <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="TransNum" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />
                                                        <asp:BoundField HeaderText="" DataField="TransSeq" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />
                                                        <asp:BoundField HeaderText="" DataField="EmpNum" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />
                                                        <asp:BoundField HeaderText="" DataField="Loc" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />
                                                        <asp:BoundField HeaderText="" DataField="Whse" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />
                                                        <asp:BoundField HeaderText="" DataField="RowPointer" ReadOnly="true" ItemStyle-CssClass="display-col" HeaderStyle-CssClass="display-col" />

                                                        </Columns>
                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                        </table>

                                            <table style="width:100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" 
                                                            CssClass="table table-striped table-bordered table-hover" 
                                                            ShowFooter="false" ShowHeader="true" >
                                                        <Columns>                         
                                                        <asp:TemplateField HeaderText="Matched" ItemStyle-HorizontalAlign="Center">  
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>  
                                                                <asp:CheckBox ID="chkMatchSelect" Enabled="false" runat="server" CssClass="form-check-input position-static" Checked= '<%# Eval("Matched") %>' />  
                                                            </ItemTemplate>  
                                                        </asp:TemplateField>

                                                        <asp:BoundField HeaderText="Operation" DataField="OperNum" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Seq" DataField="Seq" ReadOnly="true" />
                                                        <asp:BoundField HeaderText="Target Qty" DataField="TargetQty" ReadOnly="true">
                                                             <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Selected Qty" DataField="SelectedQty" ReadOnly="true">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </div>
                                </div>
                                </div>

                            </div>
                            <div id="s5" class="tab-pane fade">

                                <div class="card">
                                <div class="row align-items-center">                                    
                                    <div class="col-sm-12 text-left">
                                        <div class="card-body">
                                              <legend>Downtime</legend>
                                                <div class="row">
                                                    <div class="col-md-6 float-left width-div">
                                                        <asp:LinkButton runat="server" ID="btnpreviousdt" class="btn btn-link btn-sm float-left mx-2 mb-0" AutoPostBack="true" UseSubmitBehavior="false" >
                                                            <i class="fas fa-arrow-circle-left" aria-hidden="true"></i> <strong>Previous</strong>
                                                        </asp:LinkButton>
                                                    </div>
                                                     <div class="col-md-6 float-right width-div">

                                                         <asp:LinkButton runat="server" ID="btnnextdt" class="btn btn-link btn-sm float-right mx-2 mb-0" AutoPostBack="true" UseSubmitBehavior="false" >
                                                            <strong>Next</strong> <i class="fas fa-arrow-circle-right" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                     
                                                     </div>
                                                </div>
                                            <div class="row">
                                                <asp:Button ID="Button2" runat="server" Style="display:none" UseSubmitBehavior="false" />
                                                <div class="col-sm-5 text-right width-div">
                                                    <span>Downtime Code : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:DropDownList ID="ddldowntime" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" ></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                    <span>Start Time : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtDTStartTime" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" >00:00</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                    <span>End Time : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txtDTEndTime" runat="server" class="form-control form-control-sm txt-margin" AutoPostBack="true" AutoComplete="off" >00:00</asp:TextBox>
                                                </div>
                                                
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 text-right width-div">
                                                    <span>Total Hours : </span>
                                                </div>
                                                <div class="col-sm-2 text-left width-div">
                                                    <asp:TextBox ID="txttotalhrsDT" runat="server" class="form-control form-control-sm txt-margin" AutoComplete="off" >0</asp:TextBox>
                                                </div>
                                            </div>

                                            <table style="width:100%">
                                            <tr>
                                                <td>
                                                        <asp:GridView ID="GridView6" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered font-small">
                                                        <Columns>
                                                                <asp:BoundField HeaderText="Downtime Code" DataField="ReasonCode" ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Description" DataField="DerDescription" ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Start Time" DataField="StartTime" ReadOnly="true" />
                                                                <asp:BoundField HeaderText="End Time" DataField="EndTime" ReadOnly="true" />
                                                                <asp:TemplateField HeaderText="Total Hours">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAhrs" runat="server" Text='<%# Eval("AHrs") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                               </asp:TemplateField>
                                                               <asp:TemplateField>
                                                                   <ItemStyle HorizontalAlign="Center" />
                                                                   <ItemTemplate>
                                                                       <asp:Button ID="btndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("RowPointer") %>' class="btn btn-danger btn-sm mx-2 mb-0"  Text="Delete" ToolTip="Delete" UseSubmitBehavior="false" />
                                                                   </ItemTemplate>
                                                               </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                </td>
                                            </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                </div>

                            </div>
                            <%--<div id='s6' class='tab-pane fade'>
                                <div class="card">
                                <div class="row align-items-center">
                                <div class="col-sm-12 text-left">
                                <div class="card-body">
                                    <legend>Tag Datail</legend>  
                                    <table style="width:100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
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
                                </div>
                                </div>
                            </div>--%>
                        </div>
                        </div>
                    </div>
                    <%----------END NAV---------%>
            </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtBarcode" EventName="TextChanged" />
                <asp:PostBackTrigger ControlID="Button1" />
                <asp:PostBackTrigger ControlID="Button2" />                
                <asp:PostBackTrigger ControlID="btnprocess" />
                <asp:PostBackTrigger ControlID="btnreset" />
                <asp:PostBackTrigger ControlID="btnPrevious" />
                <asp:PostBackTrigger ControlID="btnNext" />
                <asp:PostBackTrigger ControlID="btnpreviousdt" />
                <asp:PostBackTrigger ControlID="btnnextdt" />
                <asp:AsyncPostBackTrigger ControlID="ddltrantype" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlmovetoloc" EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="ddlemployee" />
                <asp:PostBackTrigger ControlID="ddlResource" />
                <asp:PostBackTrigger ControlID="ddlSchedulingShift" />
                <asp:PostBackTrigger ControlID="txtDTStartTime" />
                <asp:PostBackTrigger ControlID="txtDTEndTime" />
                <asp:PostBackTrigger ControlID="ddldowntime" />
                <asp:PostBackTrigger ControlID="GridView3" />
                <asp:PostBackTrigger ControlID="GridView6" />
            </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Label ID="Label3" runat="server" Text="1" ForeColor="White"></asp:Label>
    <asp:HiddenField ID="DigitStartTime" runat="server" Value="0" />
     <asp:HiddenField ID="DigitEndTime" runat="server" Value="0" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js" integrity="sha384-B0UglyR+jN6CkvvICOB2joaf5I4l3gm9GU6Hc1og6Ls7i6U/mkkaduKaBhlAXv9k" crossorigin="anonymous"></script>


    <script type="text/javascript">


        $("#l_s1").click(function () {   
                $('#<%= ActiveTabTextBox.ClientID%>').val('1');
                $('#<%= Label3.ClientID%>').val('1');
                $('#<%= Label3.ClientID%>').html('1');

                $('#<%= lblbarcode.ClientID %>').text("Barcode Job: ")                   
                document.getElementById('<%=txtbarcode.ClientID%>').focus(); 


            });

        $("#l_s2").click(function () {
                $('#<%= ActiveTabTextBox.ClientID%>').val('2');
                $('#<%= Label3.ClientID%>').val('2');
                $('#<%= Label3.ClientID%>').html('2');

                $('#<%= lblbarcode.ClientID %>').text("Barcode Completed: ")
                document.getElementById('<%=txtbarcode.ClientID%>').focus(); 


            });

        $("#l_s3").click(function () {
                $('#<%= ActiveTabTextBox.ClientID%>').val('3');
                $('#<%= Label3.ClientID%>').val('3');
                $('#<%= Label3.ClientID%>').html('3');

                var scrapcode = document.getElementById('<%=ddlscrapcode.ClientID%>').value;                   

                if (scrapcode.length > 0) {
                    $('#<%= lblbarcode.ClientID %>').text("Barcode Scrapped: ")
                } else {
                    $('#<%= lblbarcode.ClientID %>').text("Barcode Scrap Code: ")
                }

                document.getElementById('<%=txtbarcode.ClientID%>').focus(); 


            });

            $("#l_s4").click(function () {
                $('#<%= ActiveTabTextBox.ClientID%>').val('4');
                $('#<%= Label3.ClientID%>').val('4');
                $('#<%= Label3.ClientID%>').html('4');

            });

        $("#l_s5").click(function () {
                $('#<%= ActiveTabTextBox.ClientID%>').val('5');
                $('#<%= Label3.ClientID%>').val('5');
                $('#<%= Label3.ClientID%>').html('5');

                var downtimecode = document.getElementById('<%=ddldowntime.ClientID%>').value;                   

                if (downtimecode.length > 0) {
                    $('#<%= lblbarcode.ClientID %>').text("Barcode Downtime Start Time: ")
                } else {
                    $('#<%= lblbarcode.ClientID %>').text("Barcode Downtime Code: ")
                    
            }
                //var Label = document.getElementById('<%=lblbarcode.ClientID%>').value; 
                //sessionStorage.setItem("LabelScan", Label);
                document.getElementById('<%=Button2.ClientID%>').click();
                document.getElementById('<%=txtbarcode.ClientID%>').focus();
                  

            });

    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            

            if (typeof (Storage) !== "undefined") {

                $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                    localStorage.setItem('activeTab', $(e.target).attr('href'));
                });

            } else {

                localStorage.setItem('activeTab', '#s1');
            }
            
            var activeTab = localStorage.getItem('activeTab')

            if (activeTab){
                $('#myTab a[href="' + activeTab + '"]').tab('show');
 
            }           

        });

    </script>

    <script type="text/javascript">

        function LocalSet() {

            localStorage.setItem('activeTab', '#s1');

            var activeTab = localStorage.getItem('activeTab')

            //alert(activeTab)

            if (activeTab){
                $('#myTab a[href="' + activeTab + '"]').tab('show');
 
            } 
        }
    </script>

    
    <script type="text/javascript">

       function CalTotalHrs(start_time, end_time, out_text_input) {
            var d1 = new Date("Dec 21 1984 " + start_time);
            var d2 = new Date("Dec 21 1984 " + end_time);
            var tsec = parseFloat('#<%= txttotalhour.ClientID%>') * 60.00 * 60.00;

            var diff_time;

            var diff = d2 - d1;
            diff_time = diff / (60.000 * 60.000 * 1000.000);


            if ((d2 < d1) && (tsec < 0.000)) {
                d2.setDate(d2.getDate() + 1);
                diff = d2 - d1;
                diff_time = diff / (60.000 * 60.000 * 1000.000);
            }

            $(out_text_input).val(diff_time);
        }


       $(document).ready(function () {

           var start_time = $("#<%= txtStartTime.ClientID %>").val();
           var end_time = $("#<%= txtEndTime.ClientID %>").val();

           if (start_time != '00:00' || end_time != '00:00') {
               CalTotalHrs(start_time, end_time, '#<%= txttotalhour.ClientID %>');
           }


       });

        $('#<%= txtStartTime.ClientID%>').change(function () {

            var start_time = $(this).val();
            var end_time = $("#<%= txtEndTime.ClientID %>").val();

            var i;
            i = start_time.indexOf(':');

            if (i < 0) {
                var mid;
                mid = start_time.length / 2;
                start_time = [start_time.slice(0, parseInt(mid)), ":", start_time.slice(parseInt(mid))].join('');
                $(this).val(start_time);
            }

            var diff = new Date("Dec 21 1984") - new Date("Dec 21 1984 " + start_time);
            if (isNaN(diff) || start_time == '') {
                $(this).val('00:00');
                start_time = '00:00';
            }


            CalTotalHrs(start_time, end_time, '#<%= txttotalhour.ClientID %>');
        });

        $('#<%= txtEndTime.ClientID%>').change(function () {
            var start_time = $("#<%= txtStartTime.ClientID %>").val();
            var end_time = $(this).val();

            var i;
            i = end_time.indexOf(':');

            if (i < 0) {
                var mid;
                mid = end_time.length / 2;
                end_time = [end_time.slice(0, parseInt(mid)), ":", end_time.slice(parseInt(mid))].join('');
                $(this).val(end_time);
            }
            var diff = new Date("Dec 21 1984") - new Date("Dec 21 1984 " + end_time);

            if (isNaN(diff) || end_time == '') {
                $(this).val('00:00');
                end_time = '00:00';
            }

            CalTotalHrs(start_time, end_time, '#<%= txttotalhour.ClientID %>');
        });



    </script>

    <script type="text/javascript">

        function CalTotalHrsDT(start_time, end_time, out_text_input) {
            var d1 = new Date("Dec 21 1984 " + start_time);
            var d2 = new Date("Dec 21 1984 " + end_time);
            var tsec = parseFloat('#<%= txttotalhrsDT.ClientID%>') * 60.00 * 60.00;

            var diff_time;

            var diff = d2 - d1;
            diff_time = diff / (60.000 * 60.000 * 1000.000);


            if ((d2 < d1) && (tsec < 0.000)) {
                d2.setDate(d2.getDate() + 1);
                diff = d2 - d1;
                diff_time = diff / (60.000 * 60.000 * 1000.000);
            }

            $(out_text_input).val(diff_time);
       }


         $(document).ready(function () {

            var dt_start = $("#<%= txtDTStartTime.ClientID %>").val();
            var dt_end = $("#<%= txtDTEndTime.ClientID %>").val();


            if (dt_start != '00:00' || dt_end != '00:00') {
                CalTotalHrsDT(start_time, end_time, '#<%= txttotalhrsDT.ClientID %>');

                }

         });


            $('#<%= txtDTStartTime.ClientID%>').change(function () {
                var start_time = $(this).val();
                var end_time = $("#<%= txtDTEndTime.ClientID %>").val();

                var i;
                i = start_time.indexOf(':');

                if (i < 0) {
                    var mid;
                    mid = start_time.length / 2;
                    start_time = [start_time.slice(0, parseInt(mid)), ":", start_time.slice(parseInt(mid))].join('');
                    $(this).val(start_time);
                }

                var diff = new Date("Dec 21 1984") - new Date("Dec 21 1984 " + start_time);
                if (isNaN(diff) || start_time == '') {
                    $(this).val('00:00');
                    start_time = '00:00';
                }

                //alert("xxxx")
                CalTotalHrsDT(start_time, end_time, '#<%= txttotalhrsDT.ClientID %>');

            });

         

        $('#<%= txtDTEndTime.ClientID%>').change(function () {

            var start_time = $("#<%= txtDTStartTime.ClientID %>").val();
            var end_time = $(this).val();

            var i;
            i = end_time.indexOf(':');

            if (i < 0) {
                var mid;
                mid = end_time.length / 2;
                end_time = [end_time.slice(0, parseInt(mid)), ":", end_time.slice(parseInt(mid))].join('');
                $(this).val(end_time);
            }
            var diff = new Date("Dec 21 1984") - new Date("Dec 21 1984 " + end_time);

            if (isNaN(diff) || end_time == '') {
                $(this).val('00:00');
                end_time = '00:00';
            }

            CalTotalHrsDT(start_time, end_time, '#<%= txttotalhrsDT.ClientID %>');

        });

    </script>

    
</asp:Content>
