Imports System.Data
Imports System.Xml

Public Class Menu
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
    Dim Parms As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("PSession") Is Nothing Then
            Response.Redirect("signin.aspx")
        Else
            If Session("PSession").ToString = "" Then
                Response.Redirect("signin.aspx")
            End If
        End If

        If Not Page.IsPostBack Then

            EnableLink()

            'txtemp.Focus()

            'If Not IsNothing(Session("Employee")) Then

            '    lblempname.Visible = True
            '    lblempname.Text = Session("Name").ToString
            'Else
            '    lblempname.Visible = False
            '    lblempname.Text = String.Empty
            'End If

        End If

    End Sub

    'Protected Sub txtemp_TextChanged(sender As Object, e As EventArgs) Handles txtemp.TextChanged

    '    If txtemp.Text <> String.Empty Then
    '        oWS = New SRNService.DOWebServiceSoapClient
    '        ds = New DataSet

    '        Dim empNum As String = ""
    '        Dim empName As String = ""

    '        Dim Filter As String = "EmpNum = '" & txtemp.Text.Trim & "'"
    '        ds = oWS.LoadDataSet(Session("Token"), "SLEmployees", "EmpNum, Name", Filter, "", "", 0)

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            empNum = ds.Tables(0).Rows(0)("EmpNum").ToString()
    '            empName = ds.Tables(0).Rows(0)("Name").ToString()

    '            Session("Employee") = empNum
    '            Session("Name") = empName

    '            Response.Redirect("Menu.aspx")
    '            'Else
    '            '    NotPassNotifyPanel.Visible = True
    '            '    NotPassText.Text = "Employee is Invalid."
    '            '    txtemp.Text = String.Empty
    '            '    txtemp.Focus()
    '        End If



    '    End If

    'End Sub




    Sub EnableLink()

        Dim QuantityMove As Integer = "0"
        Dim CycleCount As String = "0"
        Dim JobMaterial As String = "0"
        Dim UnpostedJob As String = "0"
        Dim JobReceipt As String = "0"
        Dim OrderShipping As String = "0"
        Dim GenerateGRN As String = "0"
        Dim MissOperation As String = "0"

        oWS = New SRNService.DOWebServiceSoapClient

        Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_EX_GetAccessFormSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 3 Then
                QuantityMove = node.InnerText

            ElseIf i = 4 Then
                CycleCount = node.InnerText

            ElseIf i = 5 Then
                JobMaterial = node.InnerText

            ElseIf i = 6 Then
                UnpostedJob = node.InnerText

            ElseIf i = 7 Then
                JobReceipt = node.InnerText

            ElseIf i = 8 Then
                OrderShipping = node.InnerText

            ElseIf i = 9 Then
                GenerateGRN = node.InnerText

            ElseIf i = 10 Then
                MissOperation = node.InnerText

            End If

            i += 1

        Next

        LinkMove.Visible = QuantityMove
        LinkCycleCount.Visible = CycleCount
        LinkJobMatlTran.Visible = JobMaterial
        LinkUnposted.Visible = UnpostedJob
        LinkJobReceipt.Visible = JobReceipt
        LinkOrderShipping.Visible = OrderShipping
        LinkGenerateGRN.Visible = GenerateGRN
        LinkMissOper.Visible = MissOperation

        If QuantityMove = "0" Then
            Me.divMove.Attributes("class") = ""
        End If

        If CycleCount = "0" Then
            Me.divCycleCount.Attributes("class") = ""
        End If

        If JobMaterial = "0" Then
            Me.divJobMatlTran.Attributes("class") = ""
        End If

        If UnpostedJob = "0" Then
            Me.divUnposted.Attributes("class") = ""
        End If

        If JobReceipt = "0" Then
            Me.divJobReceipt.Attributes("class") = ""
        End If

        If OrderShipping = "0" Then
            Me.divOrderShipping.Attributes("class") = ""
        End If

        If GenerateGRN = "0" Then
            Me.divGenerateGRN.Attributes("class") = ""
        End If

        If MissOperation = "0" Then
            Me.divMissOper.Attributes("class") = ""
        End If


        'ds = oWS.LoadDataSet(Session("Token").ToString, "SLUserNames", "Username", "Username = '" & Session("UserName").ToString & "'", "", "", 0)

        'ds = oWS.LoadDataSet(Session("Token").ToString, "SLUserNames", "UserNamesUf_UserName_QuantityMove, UserNamesUf_UserName_CycleCount, UserNamesUf_UserName_JobMaterial, UserNamesUf_UserName_UnpostedJob, UserNamesUf_UserName_JobReceipt, UserNamesUf_UserName_OrderShipping, UserNamesUf_UserName_GenerateGRN", "Username = '" & Session("UserName").ToString & "'", "", "", 0)

        'If ds.Tables(0).Rows.Count > 0 Then

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_QuantityMove") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_QuantityMove").ToString = "1" Then
        '            QuantityMove = "1"
        '        End If
        '    End If

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_CycleCount") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_CycleCount").ToString = "1" Then
        '            CycleCount = "1"
        '        End If
        '    End If

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_JobMaterial") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_JobMaterial").ToString = "1" Then
        '            JobMaterial = "1"
        '        End If
        '    End If

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_UnpostedJob") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_UnpostedJob").ToString = "1" Then
        '            UnpostedJob = "1"
        '        End If
        '    End If

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_JobReceipt") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_JobReceipt").ToString = "1" Then
        '            JobReceipt = "1"
        '        End If
        '    End If

        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_OrderShipping") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_OrderShipping").ToString = "1" Then
        '            OrderShipping = "1"
        '        End If
        '    End If


        '    If ds.Tables(0).Rows(0)("UserNamesUf_UserName_GenerateGRN") IsNot DBNull.Value Then
        '        If ds.Tables(0).Rows(0)("UserNamesUf_UserName_GenerateGRN").ToString = "1" Then
        '            GenerateGRN = "1"
        '        End If
        '    End If

        '    LinkMove.Visible = QuantityMove
        '    LinkCycleCount.Visible = CycleCount
        '    LinkJobMatlTran.Visible = JobMaterial
        '    LinkUnposted.Visible = UnpostedJob
        '    LinkJobReceipt.Visible = JobReceipt
        '    LinkOrderShipping.Visible = OrderShipping
        '    LinkGenerateGRN.Visible = GenerateGRN

        '    If QuantityMove = "0" Then
        '        Me.divMove.Attributes("class") = ""
        '    End If

        '    If CycleCount = "0" Then
        '        Me.divCycleCount.Attributes("class") = ""
        '    End If

        '    If JobMaterial = "0" Then
        '        Me.divJobMatlTran.Attributes("class") = ""
        '    End If

        '    If UnpostedJob = "0" Then
        '        Me.divUnposted.Attributes("class") = ""
        '    End If

        '    If JobReceipt = "0" Then
        '        Me.divJobReceipt.Attributes("class") = ""
        '    End If

        '    If OrderShipping = "0" Then
        '        Me.divOrderShipping.Attributes("class") = ""
        '    End If

        '    If GenerateGRN = "0" Then
        '        Me.divGenerateGRN.Attributes("class") = ""
        '    End If


        'End If


    End Sub



End Class