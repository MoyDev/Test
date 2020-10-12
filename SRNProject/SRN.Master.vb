Imports System.Data


Public Class SRN
    Inherits System.Web.UI.MasterPage

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Token") Is Nothing Or Session("Token").ToString = "" Then
            Response.Redirect("signin.aspx")
        End If

        If Not Page.IsPostBack Then

            If Session("UserName") IsNot Nothing Then
                lblwelcome.Text = "Welcome User: " & Session("UserName").ToString
            Else
                lblwelcome.Text = ""
            End If

            'Session("CanAccessShip") = "1"
            'Session("CanAccessReceive") = "1"

            'Dim ds As New Data.DataSet
            'oWS = New ADIService.DOWebServiceSoapClient

            'Dim PropertyList As String = "UserNamesUf_Users_Access_BCPallet, UserNamesUf_Users_Access_ReqMoveIn"

            'Dim Filter As String = "Username='" & Session("UserName").ToString & "'"
            'ds = oWS.LoadDataSet(Session("Token").ToString, "UserNames", PropertyList, Filter, "", "", 0)

            'If ds.Tables(0).Rows.Count > 0 Then
            '    If ds.Tables(0).Rows(0)("UserNamesUf_Users_Access_BCPallet") IsNot DBNull.Value Then
            '        If ds.Tables(0).Rows(0)("UserNamesUf_Users_Access_BCPallet").ToString = "1" Then
            '            Session("CanAccessBCPallet") = "1"
            '        End If
            '    End If

            '    If ds.Tables(0).Rows(0)("UserNamesUf_Users_Access_ReqMoveIn") IsNot DBNull.Value Then
            '        If ds.Tables(0).Rows(0)("UserNamesUf_Users_Access_ReqMoveIn").ToString = "1" Then
            '            Session("CanAccessReqMoveIn") = "1"
            '        End If
            '    End If
            'End If

            'btnTransferShip.Visible = Session("CanAccessShip")
            'btnTransferReceive.Visible = Session("CanAccessReceive")

            'Dim activepage As String = Request.RawUrl

            'If activepage.Contains("TransferShip.aspx") Then
            '    btnTransferShip.CssClass = "nav-link active"

            'ElseIf activepage.Contains("TransferReceive.aspx") Then
            '    btnTransferReceive.CssClass = "nav-link active"

            'ElseIf activepage.Contains("GenerateGRN.aspx") Then
            '    btnGenerateGRN.CssClass = "nav-link active"

            'ElseIf activepage.Contains("Unposted.aspx") Then
            '    btnUnpostedJob.CssClass = "nav-link active"

            'ElseIf activepage.Contains("JobMatlTran.aspx") Then
            '    btnJobMaterial.CssClass = "nav-link active"

            'ElseIf activepage.Contains("OrderShipping.aspx") Then
            '    btnOrderShipping.CssClass = "nav-link active"

            'End If


        End If

    End Sub


    'Protected Sub btnTransferShip_Click(sender As Object, e As System.EventArgs) Handles btnTransferShip.Click
    '    'btnTransferShip.Attributes.Add("class", "active")
    '    Response.Redirect("TransferShip.aspx")
    'End Sub

    'Protected Sub btnTransferReceive_Click(sender As Object, e As System.EventArgs) Handles btnTransferReceive.Click
    '    Response.Redirect("TransferReceive.aspx")
    'End Sub

    'Protected Sub btnGenerateGRN_Click(sender As Object, e As EventArgs) Handles btnGenerateGRN.Click
    '    Response.Redirect("GenerateGRN.aspx")
    'End Sub

    'Protected Sub btnUnpostedJob_Click(sender As Object, e As EventArgs) Handles btnUnpostedJob.Click
    '    Response.Redirect("Unposted.aspx")
    'End Sub

    'Protected Sub btnJobMaterial_Click(sender As Object, e As EventArgs) Handles btnJobMaterial.Click
    '    Response.Redirect("JobMatlTran.aspx")
    'End Sub

    'Protected Sub btnOrderShipping_Click(sender As Object, e As EventArgs) Handles btnOrderShipping.Click
    '    Response.Redirect("OrderShipping.aspx")
    'End Sub
End Class