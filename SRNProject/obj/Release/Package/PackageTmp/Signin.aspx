<%@ Page Title="Sign in" Language="vb" AutoEventWireup="false" CodeBehind="Signin.aspx.vb" Inherits="SRNProject.Signin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="~/asset/image/favicon-32x32.png" sizes="32x32" type="image/png" />
     <link rel="icon" href="~/asset/image/favicon-16x16.png" sizes="16x16" type="image/png" />
    <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="bootstrap/css/font-awesome.css" rel="stylesheet" type="text/css" />
        <link href="bootstrap/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

 <link href="https://getbootstrap.com/docs/4.4/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />

    <!-- Favicons -->
<link rel="apple-touch-icon" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/apple-touch-icon.png" sizes="180x180" />
<%--<link rel="icon" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/favicon-32x32.png" sizes="32x32" type="image/png" />
<link rel="icon" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/favicon-16x16.png" sizes="16x16" type="image/png" />--%>
<link rel="manifest" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/manifest.json" />
<link rel="mask-icon" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/safari-pinned-tab.svg" color="#563d7c" />
<%--<link rel="icon" href="https://getbootstrap.com/docs/4.4/assets/img/favicons/favicon.ico" />--%>
<meta name="msapplication-config" content="https://getbootstrap.com/docs/4.4/assets/img/favicons/browserconfig.xml" />
<meta name="theme-color" content="#563d7c" />

<script src="https://cdn.jsdelivr.net/npm/sweetalert2@9/dist/sweetalert2.min.js"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@9/dist/sweetalert2.min.css" />
<script>

        function ShowSweetAlert(type, msg, icon) {
            Swal.fire(
                type,
                msg,
                icon
            )

        }

</script>



</head>
<body>
    <form id="form1" runat="server">
    <div class="container">

        <br />

        <asp:Panel ID="NotificationPanel" runat="server" CssClass= "alert alert-danger alert-dismissable" Visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </asp:Panel>

        <div class="row">

            <div class="col-sm-3"></div>

            <div class="col-sm-6 pt-5">
                <div class="shadow-sm text-center rounded-lg border border-success p-5 bg-white">
                    <span>
                        <img src="asset/image/logo.png" alt="SRN">
                        </span>
                    <%--<p class="h4 mb-4">Sign In</p>--%>
            

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

                    <asp:Button ID="btnlogin" runat="server" Text="Login" class="btn btn-success btn-block my-4" />

                </div>
		   </div>

             <div class="col-sm-3"></div>

        </div>
        
    </div>
    </form>
</body>
</html>
