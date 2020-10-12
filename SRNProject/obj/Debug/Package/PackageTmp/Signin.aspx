<%@ Page Title="Sign in" Language="vb" AutoEventWireup="false" CodeBehind="Signin.aspx.vb" Inherits="ADITransfer.Signin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <meta name="veiwport" content="width=device-width" />
    <meta name="HandheldFriendly" content="true" />
    <meta name="MobileOptimized" content="width" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        body {padding-top:70px;
              background-color:#F3F3F3;
              }
    </style>


</head>
<body>
    <form id="form1" runat="server">
    <div class="container">

        <asp:Panel ID="NotificationPanel" runat="server" CssClass= "alert alert-danger alert-dismissable" Visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </asp:Panel>

        <div class="row">

            <div class="col-sm-3"></div>

            <div class="col-sm-6">
                <div class="shadow-sm text-center rounded-lg border border-primary-light p-5 bg-white">

                    <p class="h4 mb-4">Sign In</p>
                    <%--<p><img src="images/$RBJ5XRH.jpg" /></p>--%>

                        <div class="input-group mb-4">

                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                            </div>

                            <asp:TextBox ID="txtusername" runat="server" class="form-control" placeholder="Username"></asp:TextBox>
                        </div>

                        
                    <div class="input-group mb-4">

                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fa fa-lock"></i></span>
                            </div>

                            <asp:TextBox ID="txtpassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>

                    </div>

                    <asp:DropDownList ID="ddlconfig" runat="server" class="form-control"></asp:DropDownList>

                    <asp:Button ID="btnlogin" runat="server" Text="Login" class="btn btn-info btn-block my-4" />

                </div>
		   </div>

             <div class="col-sm-3"></div>

        </div>
        
    </div>
    </form>
</body>
</html>
