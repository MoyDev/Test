Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class Signin
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim SGUID As String

    Private Shared PSite As String

    Private Shared Property ParmSite() As String
        Get
            Return PSite
        End Get
        Set(value As String)
            PSite = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            oWS = New SRNService.DOWebServiceSoapClient
            For Each str As String In oWS.GetConfigurationNames
                ddlconfig.Items.Add(New ListItem(str, str))
            Next

            Dim SLConfig As String = System.Configuration.ConfigurationManager.AppSettings("Configuration")
            Dim lstConfig As ListItem = ddlconfig.Items.FindByValue(Convert.ToString(SLConfig))

            If SLConfig Is Nothing Or lstConfig Is Nothing Then
                ddlconfig.Enabled = True
            Else
                ddlconfig.SelectedValue = SLConfig.ToString
            End If

        End If

    End Sub

    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click

        Dim MsgErr As String = ""

        Try
            Dim Token As String = ""
            Dim UserName As String = Trim(txtusername.Text)
            Dim Password As String = txtpassword.Text
            Dim Config As String = Convert.ToString(ddlconfig.SelectedValue)
            Dim Parms As String = ""
            Dim UserCodes As String = ""

            oWS = New SRNService.DOWebServiceSoapClient
            Token = oWS.CreateSessionToken(UserName, Password, Config)

            Session("Token") = Token
            Session("UserName") = UserName
            Session("Config") = Replace(Config, "_", " ")
            'Session("Config") = Replace(Config, "SRN_", "")

            If Not IsNothing(Session("Token")) Then

                ParmSite = GetSite()

                SGUID = System.Guid.NewGuid.ToString()
                Session("PSession") = SGUID

            End If

            Dim time As String = System.Configuration.ConfigurationManager.AppSettings("PageTimeOut")

            If time Is Nothing Then
                time = "30"
            End If

            Session("Token") = Token
            Session.Timeout = time
            Session("PSite") = ParmSite

            Response.Redirect("Menu.aspx")
            'Response.Redirect("CycleCount.aspx")

        Catch ex As Exception

            Dim end_pos As Integer = InStr(ex.Message, "at IDOWebService.ThrowSoapException")
            If end_pos > 0 And InStr(ex.Message, "System.Web.Services.Protocols.SoapException:") > 0 Then
                MsgErr = Left(ex.Message, IIf(end_pos = 0, 0, end_pos - 1)).Replace("System.Web.Services.Protocols.SoapException:", "")
            Else
                MsgErr = ex.Message
            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','" & MsgErr & "', 'error');", True)

            'NotificationPanel.Visible = True

        End Try

    End Sub

    Function GetSite() As String

        GetSite = ""

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLParms", "Site", "", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetSite = ds.Tables(0).Rows(0)("Site").ToString
        End If

        Return GetSite

    End Function

End Class