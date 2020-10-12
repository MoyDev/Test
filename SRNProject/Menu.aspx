<%@ Page Title="Menu" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="Menu.aspx.vb" Inherits="SRNProject.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="se-pre-con"></div>
      <div class="container pt-5">
        <div class="row pl-3">
              <%--<div class="col-6">
                
                <asp:TextBox ID="txtemp" AutoPostBack="true"  CssClass="form-control" runat="server" placeholder="Employee" AutoComplete="off"></asp:TextBox>
              </div>
            <div class="col-6">
                <h3><asp:Label ID="lblempname" runat="server" Visible="false"></asp:Label></h3>
                 
            </div>--%>

        </div>

    <!-- Three columns of text below the carousel -->
    
    <div class="row pt-4">

      <div id="divMove" class="col-4 text-center chover rounded" runat="server">
      <asp:HyperLink ID="LinkMove" runat="server" NavigateUrl="~/QtyMove.aspx">
          
             <img src="asset/image/QtyMove.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">
             <h6>Quantity Move</h6>         
      </asp:HyperLink>
      </div>
        

      <div id="divCycleCount" class="col-4 text-center chover rounded" runat="server">
      <asp:HyperLink ID="LinkCycleCount" runat="server" NavigateUrl="~/CycleCount.aspx">
         
            <img src="asset/image/CycleCount.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">
            <h6>Cycle Count</h6>           
       </asp:HyperLink>
    </div>
      
     
      <div class="col-4 text-center">
      </div><!-- /.col-lg-4 -->
    </div><!-- /.row -->


    <%--<hr>--%>

    <div class="row pt-4">
       <div id="divJobMatlTran" class="col-4 text-center chover rounded" runat="server">
        <asp:HyperLink ID="LinkJobMatlTran" runat="server" NavigateUrl="~/JobMatlTran.aspx">
            <img src="asset/image/JobMatl.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">              
            <h6>Job Material</h6>  
         
        </asp:HyperLink>
        </div>
        
        

        <div id="divUnposted" class="col-4 text-center chover rounded" runat="server">
        <asp:HyperLink ID="LinkUnposted" runat="server" NavigateUrl="~/Unposted.aspx">        
            <img src="asset/image/UnpostTransactiob.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">
          
            <h6>Unposted Job Transaction</h6>
        
        </asp:HyperLink>
        </div>
        

        <div id="divJobReceipt" class="col-4 text-center chover rounded" runat="server">
        <asp:HyperLink ID="LinkJobReceipt" runat="server" NavigateUrl="~/JobReceipt.aspx">        
          <img src="asset/image/JobRecipt.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">
          <h6>Job Receipt</h6>
                   
        </asp:HyperLink>
      </div><!-- /.row -->
    </div><!-- /.col-lg-4 -->
   
    

    <%--<hr>--%>

    <div class="row pt-4">
        <div id="divOrderShipping" class="col-4 text-center chover rounded" runat="server">
          <asp:HyperLink ID="LinkOrderShipping" runat="server" NavigateUrl="~/OrderShipping.aspx">
            
                <img src="asset/image/OrderShipping.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">         
                <h6>Order Shipping</h6>
            
        </asp:HyperLink>
        </div>

        <div id="divGenerateGRN" class="col-4 text-center chover rounded" runat="server">
        <asp:HyperLink ID="LinkGenerateGRN" runat="server" NavigateUrl="~/GenerateGRN.aspx">        
            
                <img src="asset/image/GRN.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">        
                <h6>Generate GRN</h6>
            
        </asp:HyperLink>
        </div>

        <div id="divMissOper" class="col-4 text-center chover rounded" runat="server">
        <asp:HyperLink ID="LinkMissOper" runat="server" NavigateUrl="~/MissOper.aspx">        
            
                <img src="asset/image/MissOper.png" class="bd-placeholder-img rounded img-fluid" width="120" height="120" alt="...">        
                <h6>Miss Operation</h6>
            
        </asp:HyperLink>
        </div>

       <div class="col-4 text-center">
      </div><!-- /.col-lg-4 -->
     </div><!-- /.row -->

    

      

    <hr class="featurette-divider">

  </div><!-- /.container -->

</asp:Content>
