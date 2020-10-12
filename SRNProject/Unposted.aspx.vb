Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class Unposted
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim dt_match As DataTable
    Dim Filter As String
    Dim res As Object
    Dim SGUID As String
    Dim Parms As String
    Dim LenPointQty As Integer = 0
    Dim DateNow As String

    Private Shared ParmSite As String
    Private Shared JobStockTran As String
    Private Shared Whse As String

    Private Shared Property PSite() As String
        Get
            Return ParmSite
        End Get
        Set(value As String)
            ParmSite = value
        End Set
    End Property

    Private Shared Property PWhse() As String
        Get
            Return Whse
        End Get
        Set(value As String)
            Whse = value
        End Set
    End Property

    Private Shared Property PJobStockTran() As String
        Get
            Return JobStockTran
        End Get
        Set(value As String)
            JobStockTran = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Session("PSession") Is Nothing Then
            Response.Redirect("signin.aspx")
        Else
            If Session("PSession").ToString = "" Then
                Response.Redirect("signin.aspx")
            End If
        End If

        LenPointQty = UnitQtyFormat()


        If Not Page.IsPostBack Then

            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "LocalSet();", True)

            Session.Remove("LabelScan")

            PSite = GetSite()
            PJobStockTran = GetJobStockTran()
            PWhse = GetDefWhse()

            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow

            '---------Dropdown------------'
            GetEmployee()
            GetShift()
            GetReasonCode()
            GetReasonCodeDownTime()

            '---------End Dropdown------------'

            lbltotalscrap.Text = FormatNumber(0, LenPointQty)
            lblqtyscan.Text = FormatNumber(0, LenPointQty)


        End If

        'Dim DateNow As String
        'DateNow = Date.Now.ToString("dd/MM/yyyy")

        'txtdate.Text = DateNow

        txtbarcode.Focus()

        txtbarcode.Attributes.Add("onchange", "javascript:scanbarcode();")

        If ActiveTabTextBox.Value = "3" Then

            If ddlscrapcode.SelectedItem.Value = "" Then
                lblbarcode.Text = "Barcode Scrap Code: "
            Else
                lblbarcode.Text = "Barcode Scrapped: "
            End If

        ElseIf ActiveTabTextBox.Value = "5" Then

            If ddldowntime.SelectedItem.Value = "" Then
                lblbarcode.Text = "Barcode Downtime Code: "
            Else

                If Session("LabelScan") Is Nothing Or Session("LabelScan") = "" Then
                    lblbarcode.Text = "Barcode Downtime Start Time: "
                Else
                    lblbarcode.Text = Session("LabelScan").ToString
                End If

            End If

        ElseIf ActiveTabTextBox.Value = "1" Then

            If txtjob.Text = String.Empty Then
                lblbarcode.Text = "Barcode Job: "
            End If

        ElseIf ActiveTabTextBox.Value = "2" Then

            lblbarcode.Text = "Barcode Completed: "

        End If


        'If lbljob.Text <> "" Then
        '    Label1.Visible = True
        '    Label2.Visible = True
        'Else
        '    Label1.Visible = False
        '    Label2.Visible = False
        'End If
        'Response.Write("<script language=javascript>SetTab();</script>")

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If GridView6.Rows.Count = 0 And lbljob.Text <> String.Empty Then

            Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                        "<Parameter>" & PSite.ToString() & "</Parameter>" &
                        "<Parameter>" & lbljob.Text & "</Parameter>" &
                        "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                        "<Parameter>" & lbloper.Text & "</Parameter>" &
                        "<Parameter>" & "I" & "</Parameter>" &
                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_EX_InsertDowmtimeDefaultSp", Parms)

            BindDownTime()


        End If


    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        GetBackflushLots(lbltotalscrap.Text, False)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim sBarcode, sJob, sSuffix, sOper, sSchedDriver, sWc, sProdLine, sCoProductMix, sItem, sWhse As String
        Dim sLoc, sLastOper, Stat, MsgType, MsgErr As String
        Dim sQtyReceive, sQtyComplete As Decimal
        Dim oDataset As DataSet
        Dim StartTime As DateTime
        Dim Endtime As DateTime
        Dim TotalHours As Decimal = 0
        Dim StartSec As Integer = 0
        Dim EndSac As Integer = 0
        Dim sDate As DateTime

        sBarcode = txtbarcode.Text
        sJob = ""
        sSuffix = ""
        sOper = ""
        sSchedDriver = ""
        sWc = ""
        sProdLine = ""
        sQtyReceive = 0
        sQtyComplete = 0
        sCoProductMix = "0"
        sItem = ""
        sWhse = ""
        sLastOper = "10"
        sLoc = ""
        Parms = ""
        Stat = "TRUE"
        MsgType = ""
        MsgErr = ""

        'If ActiveTabTextBox.Value = "1" Then


        If txtbarcode.Text <> "" Then

            'If ActiveTabTextBox.Value = "3" Then

            '    If ddlscrapcode.SelectedItem.Value = "" Then
            '        lblbarcode.Text = "Barcode Scrap Code: "
            '    Else
            '        lblbarcode.Text = "Barcode Scrapped: "
            '    End If

            'ElseIf ActiveTabTextBox.Value = "5" Then

            '    If ddldowntime.SelectedItem.Value = "" Then
            '        lblbarcode.Text = "Barcode Downtime Code: "
            '    Else
            '        lblbarcode.Text = "Barcode Start Time: "
            '    End If

            'End If

            'Validate Before Process

            If lblbarcode.Text = "Barcode Job: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "J" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Employee: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "E" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Resource: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "R" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Scheduling Shift: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "S" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Completed: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "C" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Start Time: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "O" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode End Time: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "I" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Total Hours: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "H" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Scrap Code: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "Z" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Scrapped: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "P" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Downtime Code: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "X" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Downtime Code: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "X" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Downtime Start Time: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "T" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            ElseIf lblbarcode.Text = "Barcode Downtime End Time: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "T" & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & lbljob.Text & "</Parameter>" &
                            "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                            "<Parameter>" & lbloper.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

            End If

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_CheckValidateUnpostedSp", Parms)

            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(Parms)

            Dim i As Integer = 1

            For Each node As XmlNode In doc.DocumentElement

                If i = 7 Then
                    Stat = node.InnerText

                ElseIf i = 8 Then
                    MsgType = node.InnerText

                ElseIf i = 9 Then
                    MsgErr = node.InnerText

                End If

                i += 1

            Next

            If Stat = "FALSE" Then

                MsgErr = MsgErr.Replace("'", "\'")

                MsgType = "Error [" & MsgType & "]"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                'NotPassNotifyPanel.Visible = True
                'NotPassText.Text = MsgErr

            End If

            If Stat = "TRUE" Then

                If lblbarcode.Text = "Barcode Job: " Then

                    Dim arrBarcode As String()
                    arrBarcode = sBarcode.Split(New Char() {"-"c})

                    If arrBarcode.Length > 0 Then
                        sJob = arrBarcode(0)
                        sSuffix = arrBarcode(1)
                        sOper = arrBarcode(2)
                    End If

                    lbljob.Text = sJob
                    lblSuffix.Text = sSuffix
                    lbloper.Text = sOper

                    txtjob.Text = lbljob.Text + "-" + lblSuffix.Text + "-" + lbloper.Text

                    'Label1.Visible = True
                    'Label2.Visible = True

                    '---------GetValue------------'

                    sItem = GetItemJob(lbljob.Text, lblSuffix.Text)
                    sWhse = GetDefWhse()
                    sLastOper = GetLastOper(lbljob.Text, lblSuffix.Text)

                    GetResource(lbljob.Text, lblSuffix.Text, lbloper.Text)
                    GetItemLoc(sItem, sWhse)
                    '---------End GetValue------------'


                    '---------SetValue------------'

                    oDataset = GetJobroute(lbljob.Text, lblSuffix.Text, lbloper.Text)

                    If oDataset.Tables(0).Rows.Count > 0 Then
                        sSchedDriver = oDataset.Tables(0).Rows(0)("JshSchedDrv").ToString
                        sWc = oDataset.Tables(0).Rows(0)("Wc").ToString
                        sProdLine = oDataset.Tables(0).Rows(0)("jbrUf_jobroute_line").ToString
                        sQtyReceive = oDataset.Tables(0).Rows(0)("QtyReceived").ToString
                        sQtyComplete = oDataset.Tables(0).Rows(0)("QtyComplete").ToString
                    End If

                    lblqtyscan.Text = FormatNumber(sQtyReceive, LenPointQty)
                    lblum.Text = GetItemUM(sItem)


                    If sSchedDriver = "M" Then
                        ddltrantype.SelectedIndex = ddltrantype.Items.IndexOf(ddltrantype.Items.FindByValue("C"))
                    Else
                        ddltrantype.SelectedIndex = ddltrantype.Items.IndexOf(ddltrantype.Items.FindByValue("R"))
                    End If

                    txtwc.Text = sWc
                    txtProdLine.Text = sProdLine
                    'lblQtyRelease.Text = FormatNumber(sQtyReceive, LenPointQty)
                    'lblQtyComplete.Text = FormatNumber(sQtyComplete, LenPointQty)

                    If sLastOper = lbloper.Text Then

                        sLoc = GetLocFromProdLine(txtProdLine.Text)

                        ddlmovetoloc.SelectedIndex = ddlmovetoloc.Items.IndexOf(ddlmovetoloc.Items.FindByValue(sLoc))
                    End If

                    BindItem()
                    GetItemScrap()

                    If sCoProductMix = "0" Then

                    End If

                    '---------End SetValue------------'

                    If ddltrantype.SelectedItem.Value = "C" Then

                        ddlemployee.Attributes.Add("disabled", "disabled")
                        lblbarcode.Text = "Barcode Resource: "

                    ElseIf ddltrantype.SelectedItem.Value = "R" Then

                        ddlemployee.Attributes.Remove("disabled")
                        lblbarcode.Text = "Barcode Employee: "

                    End If


                ElseIf lblbarcode.Text = "Barcode Employee: " Then

                    ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(sBarcode))

                    ddlemployee_SelectedIndexChanged(sender, e)

                    'lblbarcode.Text = "Barcode Resource: "

                ElseIf lblbarcode.Text = "Barcode Resource: " Then

                    ddlResource.SelectedIndex = ddlResource.Items.IndexOf(ddlResource.Items.FindByValue(sBarcode))

                    ddlResource_SelectedIndexChanged(sender, e)

                    'lblbarcode.Text = "Barcode Completed: "
                    'lblbarcode.Text = "Barcode Scheduling Shift: "

                ElseIf lblbarcode.Text = "Barcode Scheduling Shift: " Then

                    ddlSchedulingShift.SelectedIndex = ddlSchedulingShift.Items.IndexOf(ddlSchedulingShift.Items.FindByValue(sBarcode))

                    ddlSchedulingShift_SelectedIndexChanged(sender, e)


                ElseIf lblbarcode.Text = "Barcode Completed: " Then

                    If sBarcode <> "OK" Then

                        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                                "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                                "<Parameter>" & sBarcode & "</Parameter>" &
                                "<Parameter>" & lbljob.Text & "</Parameter>" &
                                "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                                "<Parameter>" & lbloper.Text & "</Parameter>" &
                                "<Parameter>" & PSite.ToString() & "</Parameter>" &
                                "<Parameter>" & "T" & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "</Parameters>"

                        oWS = New SRNService.DOWebServiceSoapClient
                        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_InsertJobTranTagSp", Parms)

                        BindGridviewTag()
                        BindItemGrid()
                        GetBackflushLots(lbltotalscrap.Text, False)
                        'Else

                        'lblbarcode.Text = "Barcode Start Time: "

                    End If

                ElseIf lblbarcode.Text = "Barcode Start Time: " Then

                    txtStartTime.Text = sBarcode

                    lblbarcode.Text = "Barcode End Time: "

                ElseIf lblbarcode.Text = "Barcode End Time: " Then

                    txtEndTime.Text = sBarcode

                    lblbarcode.Text = "Barcode Total Hours: "

                ElseIf lblbarcode.Text = "Barcode Total Hours: " Then

                    txttotalhour.Text = sBarcode

                    lblbarcode.Text = "Barcode Completed: "

                ElseIf lblbarcode.Text = "Barcode Scrap Code: " Then

                    ddlscrapcode.SelectedIndex = ddlscrapcode.Items.IndexOf(ddlscrapcode.Items.FindByValue(sBarcode))

                    lblbarcode.Text = "Barcode Scrapped: "

                ElseIf lblbarcode.Text = "Barcode Scrapped: " Then

                    Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                                "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                                "<Parameter>" & sBarcode & "</Parameter>" &
                                "<Parameter>" & lbljob.Text & "</Parameter>" &
                                "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                                "<Parameter>" & lbloper.Text & "</Parameter>" &
                                "<Parameter>" & PSite.ToString() & "</Parameter>" &
                                "<Parameter>" & "S" & "</Parameter>" &
                                "<Parameter>" & ddlscrapcode.SelectedItem.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "</Parameters>"

                    oWS = New SRNService.DOWebServiceSoapClient
                    oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_InsertJobTranTagSp", Parms)

                    BindScrap()
                    BindItemGrid()
                    GetBackflushLots(lbltotalscrap.Text, False)

                ElseIf lblbarcode.Text = "Barcode Downtime Code: " Then

                    ddldowntime.SelectedIndex = ddldowntime.Items.IndexOf(ddldowntime.Items.FindByValue(sBarcode))

                    lblbarcode.Text = "Barcode Downtime Start Time: "

                ElseIf lblbarcode.Text = "Barcode Downtime Start Time: " Then

                    DigitStartTime.Value = CInt(DigitStartTime.Value) + 1

                    If DigitStartTime.Value = 1 Then
                        txtDTStartTime.Text = sBarcode + "0:00"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitStartTime.Value = 2 Then
                        txtDTStartTime.Text = Left(txtDTStartTime.Text, 1) + sBarcode + ":00"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitStartTime.Value = 3 Then
                        txtDTStartTime.Text = Left(txtDTStartTime.Text, 2) + ":" + sBarcode + "0"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitStartTime.Value = 4 Then
                        txtDTStartTime.Text = Left(txtDTStartTime.Text, 4) + sBarcode

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                        Session("LabelScan") = "Barcode Downtime End Time: "
                        lblbarcode.Text = "Barcode Downtime End Time: "

                    End If


                ElseIf lblbarcode.Text = "Barcode Downtime End Time: " Then

                    DigitEndTime.Value = CInt(DigitEndTime.Value) + 1

                    If DigitEndTime.Value = 1 Then
                        txtDTEndTime.Text = sBarcode + "0:00"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitEndTime.Value = 2 Then

                        txtDTEndTime.Text = Left(txtDTEndTime.Text, 1) + sBarcode + ":00"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitEndTime.Value = 3 Then
                        txtDTEndTime.Text = Left(txtDTEndTime.Text, 2) + ":" + sBarcode + "0"

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                    ElseIf DigitEndTime.Value = 4 Then
                        txtDTEndTime.Text = Left(txtDTEndTime.Text, 4) + sBarcode

                        StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
                        sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                        'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                        'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

                        'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                        StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                        EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                        txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

                        InsertDownTime()

                        lblbarcode.Text = "Barcode Downtime Code: "

                    End If

                    'Button2_Click(sender, e)

                End If

            End If


        End If

        'ElseIf ActiveTabTextBox.Value = "2" Then

        '    If txtbarcode.Text <> "" Then


        '    End If

        'ElseIf ActiveTabTextBox.Value = "3" Then

        '    If txtbarcode.Text <> "" Then


        '    End If


        'End If

        txtbarcode.Text = String.Empty


    End Sub

    Protected Sub btnprocess_Click(sender As Object, e As EventArgs) Handles btnprocess.Click

        Dim StartTimeSec As String = ""
        Dim EndTimeSec As String = ""
        Dim DefWhse As String = ""
        Dim UserCode As String = ""
        Dim Stat, MsgType, MsgErr As String
        Dim doc As XmlDocument = New XmlDocument()
        Dim SelectedCount As Integer = 0
        Dim RowCount As Integer = 0

        DefWhse = GetDefWhse()
        UserCode = GetUserCode(Session("Username").ToString)
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        RowCount = GridView4.Rows.Count
        For Each Row As GridViewRow In GridView4.Rows
            Dim chkMatchSelect As CheckBox = DirectCast(Row.FindControl("chkMatchSelect"), CheckBox)
            If chkMatchSelect.Checked Then
                SelectedCount += 1
            End If
        Next

        If RowCount = SelectedCount Then

            StartTimeSec = DateDiff(DateInterval.Second, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01 " & txtStartTime.Text)).ToString
            EndTimeSec = DateDiff(DateInterval.Second, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01 " & txtEndTime.Text)).ToString


            Parms = ""
            Parms = "<Parameters><Parameter>" & Session("Username").ToString & "</Parameter>" &
                    "<Parameter>" & PSite.ToString & "</Parameter>" &
                    "<Parameter>" & lbljob.Text & "</Parameter>" &
                    "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                    "<Parameter>" & ddltrantype.SelectedItem.Value & "</Parameter>" &
                    "<Parameter>" & txtdate.Text & "</Parameter>" &
                    "<Parameter>" & lbloper.Text & "</Parameter>" &
                    "<Parameter>" & txttotalhour.Text & "</Parameter>" &
                    "<Parameter>" & StartTimeSec & "</Parameter>" &
                    "<Parameter>" & EndTimeSec & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & ddlemployee.SelectedItem.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & "R" & "</Parameter>" &
                    "<Parameter>" & DefWhse & "</Parameter>" &
                    "<Parameter>" & ddlmovetoloc.SelectedItem.Value & "</Parameter>" &
                    "<Parameter>" & UserCode & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & "J" & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & txtwc.Text & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & 0 & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & ddlSchedulingShift.SelectedItem.Value & "</Parameter>" &
                    "<Parameter>" & rdDayNight.SelectedItem.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & ddlResource.SelectedItem.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                    "<Parameter>" & txtProdLine.Text & "</Parameter>" &
                    "<Parameter>" & "P" & "</Parameter>" &
                    "</Parameters>"

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "ppcc_ex_unpostedprocessSp", Parms)


            doc.LoadXml(Parms)

            Dim i As Integer = 1

            For Each node As XmlNode In doc.DocumentElement

                If i = 43 Then
                    Stat = node.InnerText

                ElseIf i = 44 Then
                    MsgType = node.InnerText

                ElseIf i = 45 Then
                    MsgErr = node.InnerText

                End If

                i += 1

            Next


            If Stat = "TRUE" Then

                Dim PostCompleteVar As String = "0"
                Dim PostNegativeInventoryVar As String = "0"
                Dim StartJobVar As String = lbljob.Text
                Dim EndJobVar As String = lbljob.Text
                Dim StartSuffixVar As String = lblSuffix.Text
                Dim EndSuffixVar As String = lblSuffix.Text
                Dim StartTransDateVar As String = ""
                Dim EndTransDateVar As String = ""
                Dim StartEmpNumVar As String = ""
                Dim EndEmpNumVar As String = ""
                Dim StartDeptVar As String = ""
                Dim EndDeptVar As String = ""
                Dim StartShiftVar As String = ""
                Dim EndShiftVar As String = ""
                Dim StartUserCodeVar As String = ""
                Dim EndUserCodeVar As String = ""
                Dim EmployeeTypeVar As String = "H S N"
                Dim FormCurWhse As String = PWhse.ToString
                Dim BlankVar1 As String = ""
                Dim BlankVar2 As String = ""
                Dim BlankVar3 As String = ""
                Dim WCVar As String = ""

                StartUserCodeVar = GetUserInitial(Session("Username").ToString)
                EndUserCodeVar = StartUserCodeVar


                Parms = ""
                Parms = "<Parameters><Parameter>" & PostCompleteVar & "</Parameter>" &
                                        "<Parameter>" & PostNegativeInventoryVar & "</Parameter>" &
                                        "<Parameter>" & StartJobVar & "</Parameter>" &
                                        "<Parameter>" & EndJobVar & "</Parameter>" &
                                        "<Parameter>" & StartSuffixVar & "</Parameter>" &
                                        "<Parameter>" & EndSuffixVar & "</Parameter>" &
                                        "<Parameter>" & StartTransDateVar & "</Parameter>" &
                                        "<Parameter>" & EndTransDateVar & "</Parameter>" &
                                        "<Parameter>" & StartEmpNumVar & "</Parameter>" &
                                        "<Parameter>" & EndEmpNumVar & "</Parameter>" &
                                        "<Parameter>" & StartDeptVar & "</Parameter>" &
                                        "<Parameter>" & EndDeptVar & "</Parameter>" &
                                        "<Parameter>" & StartShiftVar & "</Parameter>" &
                                        "<Parameter>" & EndShiftVar & "</Parameter>" &
                                        "<Parameter>" & StartUserCodeVar & "</Parameter>" &
                                        "<Parameter>" & EndUserCodeVar & "</Parameter>" &
                                        "<Parameter>" & EmployeeTypeVar & "</Parameter>" &
                                        "<Parameter>" & FormCurWhse & "</Parameter>" &
                                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                                        "<Parameter>" & DBNull.Value & "</Parameter></Parameters>"

                'Debug.WriteLine("JobJobP" & Parms)
                Dim res As Object
                res = New Object
                oWS = New SRNService.DOWebServiceSoapClient
                res = oWS.CallMethod(Session("Token").ToString, "SLJobTrans", "JobJobP", Parms)

                If res = "0" Then

                    doc.LoadXml(Parms)

                    Dim j As Integer = 1
                    Dim MsgPost As String = ""

                    For Each node As XmlNode In doc.DocumentElement

                        If j = 21 Then
                            MsgPost = node.InnerText

                        End If

                        j += 1

                    Next

                    Parms = ""
                    Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter></Parameters>"
                    oWS = New SRNService.DOWebServiceSoapClient
                    oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_PostedSetupJobTran2Sp", Parms)

                    Clear()

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Success','" & MsgPost & "', 'success');", True)

                Else

                    doc.LoadXml(Parms)

                    Dim j As Integer = 1
                    Dim MsgPost As String = ""

                    For Each node As XmlNode In doc.DocumentElement

                        If j = 21 Then
                            MsgPost = node.InnerText

                        End If

                        j += 1

                    Next

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','" & MsgPost & "', 'error');", True)

                End If

            Else

                MsgErr = MsgErr.Replace("'", "\'")
                MsgErr = MsgErr.Replace(vbLf, " ")

                MsgType = "Error [" & MsgType & "]"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)


            End If

        Else

            MsgType = "Error [STD]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','Target Qty must match Selected Qty', 'error');", True)

        End If


    End Sub

    Protected Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click

        Dim Stat, MsgErr, MsgType As String
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        Parms = ""
        Parms = "<Parameters><Parameter>" & Session("Username").ToString & "</Parameter>" &
                "<Parameter>" & PSite.ToString & "</Parameter>" &
                "<Parameter>" & lbljob.Text & "</Parameter>" &
                "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & txtdate.Text & "</Parameter>" &
                "<Parameter>" & lbloper.Text & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & 0 & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                "<Parameter>" & DBNull.Value & "</Parameter>" &
                "<Parameter>" & "R" & "</Parameter>" &
                "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "ppcc_ex_unpostedprocessSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 43 Then
                Stat = node.InnerText

            ElseIf i = 44 Then
                MsgType = node.InnerText

            ElseIf i = 45 Then
                MsgErr = node.InnerText

            End If

            i += 1

        Next

        If Stat = "TRUE" Then

            SGUID = System.Guid.NewGuid.ToString()
            Session("PSession") = SGUID

            Response.Redirect("Unposted.aspx")

        Else

            MsgErr = MsgErr.Replace("'", "\'")
            MsgType = "Error [" & MsgType & "]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

            'NotPassNotifyPanel.Visible = True
            'NotPassText.Text = MsgErr

        End If

    End Sub

    Sub InsertDownTime()

        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                                "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & lbljob.Text & "</Parameter>" &
                                "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                                "<Parameter>" & lbloper.Text & "</Parameter>" &
                                "<Parameter>" & PSite.ToString() & "</Parameter>" &
                                "<Parameter>" & "D" & "</Parameter>" &
                                "<Parameter>" & ddldowntime.SelectedItem.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & txtDTStartTime.Text & "</Parameter>" &
                                "<Parameter>" & txtDTEndTime.Text & "</Parameter>" &
                                "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_InsertJobTranTagSp", Parms)
        BindDownTime()

        ddldowntime.SelectedIndex = ddldowntime.Items.IndexOf(ddldowntime.Items.FindByValue(""))
        txtDTStartTime.Text = "00:00"
        txtDTEndTime.Text = "00:00"
        txttotalhrsDT.Text = "0"
        DigitStartTime.Value = "0"
        DigitEndTime.Value = "0"
        Session.Remove("LabelScan")

    End Sub

    'Protected Sub txtscrapqty_TextChanged(sender As Object, e As EventArgs) Handles txtscrapqty.TextChanged

    '    If ddlscrapcode.SelectedItem.Value <> "" Then

    '        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
    '                        "<Parameter>" & Session("Username").ToString & "</Parameter>" &
    '                        "<Parameter>" & DBNull.Value & "</Parameter>" &
    '                        "<Parameter>" & lbljob.Text & "</Parameter>" &
    '                        "<Parameter>" & lblSuffix.Text & "</Parameter>" &
    '                        "<Parameter>" & lbloper.Text & "</Parameter>" &
    '                        "<Parameter>" & PSite.ToString() & "</Parameter>" &
    '                        "<Parameter>" & "S" & "</Parameter>" &
    '                        "<Parameter>" & ddlscrapcode.SelectedItem.Value & "</Parameter>" &
    '                        "<Parameter>" & ddlitem.SelectedItem.Value & "</Parameter>" &
    '                        "<Parameter>" & txtscrapqty.Text & "</Parameter>" &
    '                        "<Parameter>" & DBNull.Value & "</Parameter>" &
    '                        "<Parameter>" & DBNull.Value & "</Parameter>" &
    '                        "</Parameters>"

    '        oWS = New SRNService.DOWebServiceSoapClient
    '        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_Ex_InsertJobTranTagSp", Parms)

    '        BindScrap()

    '    End If

    'End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblitemComplete As Label = CType(e.Row.FindControl("lblitemComplete"), Label)
            Dim ddlLot As DropDownList = CType(e.Row.FindControl("ddlLot"), DropDownList)
            Dim total As Double = Convert.ToDecimal(lblqtyscan.Text)

            Dim Qty As Decimal = CDec(lblitemComplete.Text)

            lblitemComplete.Text = FormatNumber(Qty, LenPointQty)

            total = total - Convert.ToDecimal(lblitemComplete.Text)
            lblqtyscan.Text = Convert.ToString(total)

            If PJobStockTran = "0" Then
                ddlLot.Attributes.Add("disabled", "disabled")
            Else

                ddlLot.Attributes.Add("disabled", "disabled")
                ddlLot.Items.Clear()

                oWS = New SRNService.DOWebServiceSoapClient

                ds = New DataSet

                ds = oWS.LoadDataSet(Session("Token").ToString, "SLPreassignedLots", "Lot", "RefType = 'J' and RefNum = '" & lbljob.Text & "' and RefLineSuf = '" & lblSuffix.Text & "'", "Lot", "", 0)

                For Each dRow As DataRow In ds.Tables(0).Rows
                    ddlLot.Items.Add(New ListItem(dRow("Lot"), dRow("Lot")))

                Next

            End If

        End If

    End Sub

    Protected Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txtqtyReq As TextBox = CType(e.Row.FindControl("txtqtyReq"), TextBox)

            Dim QtyReq As Double = CDec(txtqtyReq.Text)
            Dim QtyOnHand As Double = CDec(e.Row.Cells(8).Text)
            Dim QtyNeeded As Double = CDec(e.Row.Cells(9).Text)

            txtqtyReq.Text = FormatNumber(QtyReq, LenPointQty)
            e.Row.Cells(8).Text = FormatNumber(QtyOnHand, LenPointQty)
            e.Row.Cells(9).Text = FormatNumber(QtyNeeded, LenPointQty)

        End If

    End Sub

    Protected Sub GridView6_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridView6.RowCommand

        If e.CommandName = "DeleteDT" Then

            Dim RowPointer As String = e.CommandArgument.ToString
            Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                        "<Parameter>" & PSite.ToString() & "</Parameter>" &
                        "<Parameter>" & lbljob.Text & "</Parameter>" &
                        "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                        "<Parameter>" & lbloper.Text & "</Parameter>" &
                        "<Parameter>" & "D" & "</Parameter>" &
                        "<Parameter>" & RowPointer & "</Parameter>" &
                        "</Parameters>"

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_EX_InsertDowmtimeDefaultSp", Parms)

            BindDownTime()

        End If

    End Sub

    Protected Sub GridView6_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView6.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblAhrs As Label = CType(e.Row.FindControl("lblAhrs"), Label)

            Dim QtyAhrs As Double = CDec(lblAhrs.Text)

            lblAhrs.Text = Decimal.Round(QtyAhrs.ToString, LenPointQty, MidpointRounding.AwayFromZero)

        End If

    End Sub

    Protected Sub GridView4_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView4.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim TargetQty As Double = CDec(e.Row.Cells(3).Text)
            Dim SelectedQty As Double = CDec(e.Row.Cells(4).Text)

            e.Row.Cells(3).Text = FormatNumber(TargetQty, LenPointQty)
            e.Row.Cells(4).Text = FormatNumber(SelectedQty, LenPointQty)

        End If

    End Sub

    Protected Sub GridView5_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView5.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblQty As Label = CType(e.Row.FindControl("lblQty"), Label)

            Dim QtyTagScrap As Double = CDec(lblQty.Text)

            lblQty.Text = FormatNumber(QtyTagScrap, LenPointQty)

        End If

    End Sub

    Sub GetBackflushLots(ByVal QtyScrapped As Decimal, ByVal Selected As Boolean)

        Dim QtyCompleted As Double = 0

        'QtyScrapped = 0

        If GridView1.Rows.Count > 0 Then

            For Each row As GridViewRow In GridView1.Rows

                Dim lblitemComplete As Label = TryCast(row.Cells(1).FindControl("lblitemComplete"), Label)

                QtyCompleted = QtyCompleted + Convert.ToDecimal(lblitemComplete.Text)

            Next

        Else

            QtyCompleted = 0

        End If

        'If GridView5.Rows.Count > 0 Then

        '    For Each row As GridViewRow In GridView5.Rows

        '        Dim lblQty As Label = TryCast(row.Cells(1).FindControl("lblQty"), Label)

        '        QtyScrapped = QtyScrapped + Convert.ToDecimal(lblQty.Text)

        '    Next

        'Else

        '    QtyScrapped = 0

        'End If

        If Not Selected Then

            Parms = "<Parameters><Parameter>" & "1" & "</Parameter>" &
                    "<Parameter>" & "J" & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & lbljob.Text & "</Parameter>" &
                    "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                    "<Parameter>" & lbloper.Text & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & txtdate.Text & "</Parameter>" &
                    "<Parameter>" & PWhse.ToString & "</Parameter>" &
                    "<Parameter>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & QtyCompleted & "</Parameter>" &
                    "<Parameter>" & QtyScrapped & "</Parameter>" &
                    "<Parameter>" & ddlemployee.SelectedItem.Value & "</Parameter>" &
                    "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                    "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                    "<Parameter>" & ddlemployee.SelectedItem.Value & "</Parameter>" &
                    "<Parameter>" & ParmSite.ToString & "</Parameter>" &
                    "<Parameter>" & Session("PSession").ToString & "</Parameter></Parameters>"

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Backflushs", "PPCC_EX_CLM_LoadBackflushSp", Parms)

            Filter = "UserID='" & Session("UserName").ToString & "' And Job= '" & lbljob.Text & "' And Suffix= '" & lblSuffix.Text & "' And QtyReq > 0 And SessionID='" & Session("PSession").ToString & "'"

            Dim List As String = "Selected, OperNum, Seq, Lot, Qty, UM, Item, ItemDesc, QtyOnHand, QtyNeeded, RowPointer, Loc, Whse, TransNum, TransSeq, EmpNum"
            oWS = New SRNService.DOWebServiceSoapClient
            ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Backflushs", List, Filter, "OperNum, Seq, Lot", "", 0)

            If ds.Tables(0).Rows.Count > 0 Then
                GridView3.DataSource = ds
                GridView3.DataBind()
            End If

            dt_match = New Data.DataTable

            With dt_match.Columns
                .Add("Matched", Type.GetType("System.Int32"))
                .Add("OperNum", Type.GetType("System.Int32"))
                .Add("Job", Type.GetType("System.String"))
                .Add("Seq", Type.GetType("System.Int32"))
                .Add("TargetQty", Type.GetType("System.Decimal"))
                .Add("SelectedQty", Type.GetType("System.Decimal"))
                .Add("IssueQty", Type.GetType("System.Decimal"))
            End With

        Else

            ds = New DataSet
            ds = GetBFLot(lbljob.Text, lblSuffix.Text, lbloper.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                GridView3.DataSource = ds
                GridView3.DataBind()
            End If

            dt_match = New Data.DataTable

            With dt_match.Columns
                .Add("Matched", Type.GetType("System.Int32"))
                .Add("OperNum", Type.GetType("System.Int32"))
                .Add("Job", Type.GetType("System.String"))
                .Add("Seq", Type.GetType("System.Int32"))
                .Add("TargetQty", Type.GetType("System.Decimal"))
                .Add("SelectedQty", Type.GetType("System.Decimal"))
                .Add("IssueQty", Type.GetType("System.Decimal"))
            End With

        End If

        Dim sJob As String = lbljob.Text
        Dim sOperNum As String = "'"
        Dim sSeq As String = ""
        Dim sNeeded As String = ""
        Dim sRequired As String = ""
        Dim sSelect As String = ""
        Dim sLot As String = ""
        Dim sWhse As String = ""
        Dim sLoc As String = ""
        Dim sItem As String = ""
        Dim bClearSum As Boolean = False

        Call RefreshBflushLotsSum("", "", 0, 0, 0, 0, bClearSum, dt_match)

        For Each Row As GridViewRow In GridView3.Rows
            Dim OperNum As String = Row.Cells(1).Text
            Dim Seq As String = Row.Cells(2).Text
            Dim Needed As String = Row.Cells(9).Text
            Dim txtqtyReq As TextBox = DirectCast(Row.FindControl("txtqtyReq"), TextBox)
            Dim Lot As String = Row.Cells(3).Text
            Dim chkSelect As CheckBox = DirectCast(Row.FindControl("chkSelect"), CheckBox)
            Dim Whse As String = Row.Cells(14).Text
            Dim Location As String = Row.Cells(13).Text
            Dim Item As String = Row.Cells(6).Text

            sOperNum = OperNum.ToString
            sSeq = Seq.ToString
            sNeeded = Needed.ToString
            sRequired = txtqtyReq.Text
            sLot = Lot.ToString
            sSelect = IIf(chkSelect.Checked, "1", "0")
            sWhse = Whse.ToString
            sLoc = Location.ToString
            sItem = Item.ToString

            If sSelect = "1" Then
                Parms = "<Parameters><Parameter>" & sWhse & "</Parameter>" &
                        "<Parameter>" & sLot & "</Parameter>" &
                        "<Parameter>" & "1" & "</Parameter>" &
                        "<Parameter>" & sItem & "</Parameter>" &
                        "<Parameter>" & sLoc & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter>" & lbljob.Text & "</Parameter>" &
                        "<Parameter>" & lblSuffix.Text & "</Parameter></Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "SLJobtMats", "BflushLotValSp", Parms)

            End If

            Call RefreshBflushLotsSum(sJob, sOperNum, sSeq, sNeeded, sRequired, sSelect, bClearSum, dt_match)
            bClearSum = False
        Next

        GridView4.DataSource = dt_match
        GridView4.DataBind()

    End Sub

    Sub RefreshBflushLotsSum(ByVal sJob As String, ByVal sOperNum As String, ByVal iSeq As Integer, ByVal dNeeded As Decimal,
                             ByVal dRequired As Decimal, ByVal iSelect As Integer, ByVal bClearSum As Boolean, ByRef dt_match As DataTable)

        Dim sSumJob As String = ""
        Dim sSumOperNum As String = ""
        Dim iSumSeq As Integer = 0
        Dim dSumNeeded As Decimal = 0D
        Dim dSumRequired As Decimal = 0D
        Dim bExists As Boolean
        Dim iRowCount As Integer = 0

        bExists = False

        iRowCount = dt_match.Rows.Count

        If sJob = "" Or sJob = String.Empty Then

            For i As Integer = 0 To iRowCount - 1
                dt_match.Rows(i)("Matched") = "0"
                dt_match.Rows(i)("SelectedQty") = "0"
            Next

        End If

        If sJob = "" Or sJob = String.Empty Then
            Exit Sub
        End If

        For i As Integer = 0 To iRowCount - 1

            sSumJob = dt_match.Rows(i)("Job")

            If sSumJob = "" Or sSumJob = String.Empty Then
                Exit Sub
            End If

            sSumOperNum = dt_match.Rows(i)("OperNum")
            iSumSeq = CInt(dt_match.Rows(i)("Seq"))
            dSumNeeded = CDec(dt_match.Rows(i)("TargetQty"))

            If dt_match.Rows(i)("SelectedQty").ToString = "" Then
                dSumRequired = 0
            Else
                dSumRequired = CDec(dt_match.Rows(i)("SelectedQty"))
            End If

            If sSumJob = sJob And sSumOperNum = sOperNum And iSumSeq = iSeq Then
                If iSelect = 1 Then
                    dSumRequired = dSumRequired + dRequired
                    dt_match.Rows(i)("SelectedQty") = dSumRequired
                End If

                If dSumNeeded = dSumRequired Then
                    dt_match.Rows(i)("Matched") = "1"
                Else
                    dt_match.Rows(i)("Matched") = "0"
                End If

                bExists = True

                Exit For

            End If
        Next

        If Not bExists Then

            Dim row As Data.DataRow
            row = dt_match.NewRow
            row("Matched") = "0"
            row("Job") = sJob
            row("OperNum") = sOperNum
            row("Seq") = CStr(iSeq)
            row("TargetQty") = CStr(dNeeded)
            row("SelectedQty") = "0"

            If iSelect = 1 Then
                row("SelectedQty") = CStr(dRequired)
                If dNeeded = dRequired Then
                    row("Matched") = "1"
                Else
                    row("Matched") = "0"
                End If
            End If

            dt_match.Rows.Add(row)

            'Session("dt_match") = dt_match

        End If

    End Sub

    Protected Sub ddlemployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlemployee.SelectedIndexChanged

        If ddlemployee.SelectedItem.Value <> "" Then
            lblbarcode.Text = "Barcode Resource: "
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Employee Before', 'error');", True)
        End If

    End Sub

    Protected Sub ddlResource_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlResource.SelectedIndexChanged

        If ddlResource.SelectedItem.Value <> "" Then
            lblbarcode.Text = "Barcode Scheduling Shift: "

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Resource Before', 'error');", True)
        End If

    End Sub

    Protected Sub ddlSchedulingShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSchedulingShift.SelectedIndexChanged

        If ddlSchedulingShift.SelectedItem.Value <> "" Then

            lblbarcode.Text = "Barcode Start Time: "

            If Left(ddlSchedulingShift.SelectedItem.Value, 1) = "D" Then
                rdDayNight.SelectedValue = "D"
            Else
                rdDayNight.SelectedValue = "N"
            End If

        Else

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Scheduling Shift Before', 'error');", True)

        End If


    End Sub

    Protected Sub txtStartTime_TextChanged(sender As Object, e As EventArgs) Handles txtStartTime.TextChanged

        If txtStartTime.Text <> "" Then
            lblbarcode.Text = "Barcode End Time: "
        End If

    End Sub

    Protected Sub txtEndTime_TextChanged(sender As Object, e As EventArgs) Handles txtEndTime.TextChanged

        If txtStartTime.Text <> "" Then
            lblbarcode.Text = "Barcode Total Hours: "
        End If

    End Sub

    Protected Sub txtDTStartTime_TextChanged(sender As Object, e As EventArgs) Handles txtDTStartTime.TextChanged

        If txtDTStartTime.Text <> "" Then
            Dim StartTime As DateTime
            Dim Endtime As DateTime
            Dim TotalHours As Decimal = 0
            Dim StartSec As Integer = 0
            Dim EndSac As Integer = 0
            Dim sDate As DateTime

            'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
            'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

            StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
            Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
            sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

            'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
            'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

            'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
            StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
            EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

            txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0

            lblbarcode.Text = "Barcode Downtime End Time: "
        End If

    End Sub

    Protected Sub txtDTEndTime_TextChanged(sender As Object, e As EventArgs) Handles txtDTEndTime.TextChanged

        If txtDTEndTime.Text <> "" Then

            Dim StartTime As DateTime
            Dim Endtime As DateTime
            Dim TotalHours As Decimal = 0
            Dim StartSec As Integer = 0
            Dim EndSac As Integer = 0
            Dim sDate As DateTime

            'StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
            'Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString

            StartTime = DateTime.ParseExact("1900-01-01 " & txtDTStartTime.Text, "yyyy-MM-dd HH:mm", Nothing)
            Endtime = DateTime.ParseExact("1900-01-01 " & txtDTEndTime.Text, "yyyy-MM-dd HH:mm", Nothing)
            sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

            StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
            EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

            'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
            txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0
            'Button2_Click(sender, e)
            InsertDownTime()
            lblbarcode.Text = "Barcode Downtime Code: "

        End If

    End Sub



    Protected Sub ddlscrapcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlscrapcode.SelectedIndexChanged

        If ddlscrapcode.SelectedItem.Value <> "" Then
            lblbarcode.Text = "Barcode Scrapped: "
        End If


    End Sub

    Protected Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click

        If lblbarcode.Text = "Barcode Completed: " Then
            lblbarcode.Text = "Barcode Total Hours: "
        ElseIf lblbarcode.Text = "Barcode Total Hours: " Then
            lblbarcode.Text = "Barcode End Time: "
        ElseIf lblbarcode.Text = "Barcode End Time: " Then
            lblbarcode.Text = "Barcode Start Time: "
        ElseIf lblbarcode.Text = "Barcode Start Time: " Then
            lblbarcode.Text = "Barcode Scheduling Shift: "
        ElseIf lblbarcode.Text = "Barcode Scheduling Shift: " Then

            'If ddlSchedulingShift.SelectedItem.Value = "" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Scheduling Shift Before', 'error');", True)
            'Else
            lblbarcode.Text = "Barcode Resource: "
            'End If


        ElseIf lblbarcode.Text = "Barcode Resource: " Then

            'If ddlResource.SelectedItem.Value = "" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Resource Before', 'error');", True)
            'Else

            If ddltrantype.SelectedItem.Value = "C" Then
                    lblbarcode.Text = "Barcode Resource: "
                ElseIf ddltrantype.SelectedItem.Value = "R" Then
                    lblbarcode.Text = "Barcode Employee: "
                End If

            'End If

            'ElseIf lblbarcode.Text = "Barcode Employee: " Then

        End If

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Dim MsgErr As String = ""
        Dim MsgType As String = ""

        If lblbarcode.Text = "Barcode Job: " Then

            If ddltrantype.SelectedItem.Value = "C" Then
                lblbarcode.Text = "Barcode Resource: "
            ElseIf ddltrantype.SelectedItem.Value = "R" Then
                lblbarcode.Text = "Barcode Employee: "
            End If

        ElseIf lblbarcode.Text = "Barcode Employee: " Then

            If ddlemployee.SelectedItem.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Employee Before', 'error');", True)
            Else
                lblbarcode.Text = "Barcode Resource: "
            End If



        ElseIf lblbarcode.Text = "Barcode Resource: " Then

            If ddlResource.SelectedItem.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Resource Before', 'error');", True)
            Else
                lblbarcode.Text = "Barcode Scheduling Shift: "
            End If


        ElseIf lblbarcode.Text = "Barcode Scheduling Shift: " Then

            If ddlSchedulingShift.SelectedItem.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Scheduling Shift Before', 'error');", True)
            Else
                lblbarcode.Text = "Barcode Start Time: "
            End If

        ElseIf lblbarcode.Text = "Barcode Start Time: " Then
            lblbarcode.Text = "Barcode End Time: "
        ElseIf lblbarcode.Text = "Barcode End Time: " Then
            lblbarcode.Text = "Barcode Total Hours: "
        ElseIf lblbarcode.Text = "Barcode Total Hours: " Then
            lblbarcode.Text = "Barcode Completed: "
        End If

    End Sub

    Protected Sub btnpreviousdt_Click(sender As Object, e As EventArgs) Handles btnpreviousdt.Click

        If lblbarcode.Text = "Barcode Downtime End Time: " Then
            lblbarcode.Text = "Barcode Downtime Start Time: "
        ElseIf lblbarcode.Text = "Barcode Downtime Start Time: " Then
            lblbarcode.Text = "Barcode Downtime Code: "
        End If

    End Sub

    Protected Sub btnnextdt_Click(sender As Object, e As EventArgs) Handles btnnextdt.Click

        Dim MsgErr As String = ""
        Dim MsgType As String = ""

        If lblbarcode.Text = "Barcode Downtime Code: " Then

            If ddldowntime.SelectedItem.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Select Downtime Code Before', 'error');", True)
            Else
                lblbarcode.Text = "Barcode Downtime Start Time: "
            End If

        ElseIf lblbarcode.Text = "Barcode Downtime Start Time: " Then

            If txtDTStartTime.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Fill Start time Before', 'error');", True)
            Else
                lblbarcode.Text = "Barcode Downtime End Time: "

                Session("LabelScan") = "Barcode Downtime End Time: "

            End If

        ElseIf lblbarcode.Text = "Barcode Downtime End Time: " Then

            If txtDTEndTime.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error','Please Scan/Fill End Time Before', 'error');", True)
            Else

                Dim StartTime As DateTime
                Dim Endtime As DateTime
                Dim TotalHours As Decimal = 0
                Dim StartSec As Integer = 0
                Dim EndSac As Integer = 0
                Dim sDate As DateTime

                StartTime = Convert.ToDateTime("1900-01-01 " & txtDTStartTime.Text).ToString
                Endtime = Convert.ToDateTime("1900-01-01 " & txtDTEndTime.Text).ToString
                sDate = DateTime.ParseExact("1900-01-01 00:00", "yyyy-MM-dd HH:mm", Nothing)

                StartSec = DateDiff(DateInterval.Second, sDate, StartTime)
                EndSac = DateDiff(DateInterval.Second, sDate, Endtime)

                'TotalHours = DateDiff(DateInterval.Hour, StartTime, Endtime)
                txttotalhrsDT.Text = ((EndSac - StartSec) / 60.0) / 60.0
                InsertDownTime()

                lblbarcode.Text = "Barcode Downtime Code: "

            End If

        End If

    End Sub

    Sub SelectCheckBox_CheckedChanged()

        Dim Selected As Integer

        For Each row As GridViewRow In GridView3.Rows

            If row.RowType = DataControlRowType.DataRow Then

                Dim chkSelect As CheckBox = TryCast(row.Cells(0).FindControl("chkSelect"), CheckBox)

                Selected = IIf(chkSelect.Checked = True, 1, 0)
                'If chkSelect.Checked Then

                Parms = "<Parameters><Parameter>" & row.Cells(15).Text & "</Parameter>" &
                "<Parameter>" & Selected & "</Parameter>" &
                "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                "<Parameter>" & ParmSite.ToString & "</Parameter></Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                res = oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Backflushs", "PPCC_EX_BackflushSelectSp", Parms)


                'End If
            End If
        Next

        'QtyScrapped = 0
        'QtyScrapped = SumScrap(strJob, strSuffix, strOperNum)
        Call GetBackflushLots(lbltotalscrap.Text, True)

    End Sub

    Sub Clear()

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "LocalSet();", True)
        Session.Remove("LabelScan")
        DateNow = Date.Now.ToString("dd/MM/yyyy")
        txtdate.Text = DateNow

        lblbarcode.Text = "Barcode Job: "
        txtbarcode.Text = String.Empty
        txtjob.Text = String.Empty
        lbljob.Text = String.Empty
        lblSuffix.Text = String.Empty
        lbloper.Text = String.Empty
        'ActiveTabTextBox.Value = "1"
        'Label3.Text = "#s1"
        ddlemployee.Attributes.Remove("disabled")
        ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(""))
        txtwc.Text = String.Empty
        ddlResource.SelectedIndex = ddlResource.Items.IndexOf(ddlResource.Items.FindByValue(""))
        txtProdLine.Text = String.Empty
        ddlSchedulingShift.SelectedIndex = ddlSchedulingShift.Items.IndexOf(ddlSchedulingShift.Items.FindByValue(""))
        rdDayNight.ClearSelection()
        txtStartTime.Text = "00:00"
        txtEndTime.Text = "00:00"
        txttotalhour.Text = "0"
        ddlmovetoloc.SelectedIndex = ddlmovetoloc.Items.IndexOf(ddlmovetoloc.Items.FindByValue(""))

        GridView1.DataSource = Nothing
        GridView1.DataBind()

        ddlscrapcode.SelectedIndex = ddlscrapcode.Items.IndexOf(ddlscrapcode.Items.FindByValue(""))
        lbltotalscrap.Text = "0"
        GridView5.DataSource = Nothing
        GridView5.DataBind()

        GridView3.DataSource = Nothing
        GridView3.DataBind()

        GridView4.DataSource = Nothing
        GridView4.DataBind()

        ddldowntime.SelectedIndex = ddldowntime.Items.IndexOf(ddldowntime.Items.FindByValue(""))
        txtDTStartTime.Text = "00:00"
        txtDTEndTime.Text = "00:00"
        txttotalhrsDT.Text = "0"
        DigitStartTime.Value = "0"
        DigitEndTime.Value = "0"

        GridView6.DataSource = Nothing
        GridView6.DataBind()

        txtbarcode.Focus()

        SGUID = System.Guid.NewGuid.ToString()
        Session("PSession") = SGUID

    End Sub

    Sub BindItem()

        Parms = ""
        Parms = "<Parameters><Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & lbljob.Text & "</Parameter>" &
                                "<Parameter>" & lblSuffix.Text & "</Parameter>" &
                                "<Parameter>" & lbloper.Text & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                                "<Parameter>" & PSite.ToString & "</Parameter>" &
                                "<Parameter>" & Session("PSession").ToString & "</Parameter></Parameters>"

        res = New Object
        oWS = New SRNService.DOWebServiceSoapClient
        res = oWS.CallMethod(Session("Token").ToString, "PPCC_EX_CoProducts", "PPCC_EX_LoadJobtranitemSp", Parms)

        Filter = "Job = '" & lbljob.Text & "' And Suffix = '" & lblSuffix.Text & "' And OperNum = '" & lbloper.Text & "' And SessionID = '" & Session("PSession").ToString & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_EX_CoProducts", "Item, QtyComplete, Lot", Filter, "Item", "", 0)

        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()

    End Sub

    Sub BindItemGrid()

        Filter = "Job = '" & lbljob.Text & "' And Suffix = '" & lblSuffix.Text & "' And OperNum = '" & lbloper.Text & "' And SessionID = '" & Session("PSession").ToString & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_EX_CoProducts", "Item, QtyComplete, Lot", Filter, "Item", "", 0)

        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()



    End Sub

    Sub BindScrap()

        Filter = "SessionID = '" & Session("PSession").ToString & "' And UserID = '" & Session("Username").ToString & "' And Type = 'S'"

        Dim List As String
        List = "ReasonCode, DerDescription, Qty, Job, Suffix, OperNum, Item"

        ds = New DataSet

        oWS = New SRNService.DOWebServiceSoapClient
        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Scrappeds", List, Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then

            GridView5.DataSource = ds
            GridView5.DataBind()

            Dim dt As DataTable = ds.Tables(0)
            Dim sum As Decimal = FormatNumber(Convert.ToDecimal(dt.Compute("SUM(Qty)", "Job = '" & lbljob.Text & "' And Suffix = '" & lblSuffix.Text & "' And OperNum = '" & lbloper.Text & "'")), LenPointQty)

            lbltotalscrap.Text = Decimal.Round(sum.ToString, LenPointQty, MidpointRounding.AwayFromZero)

        End If

    End Sub

    Sub BindDownTime()

        Filter = "SessionID = '" & Session("PSession").ToString & "' And UserID = '" & Session("Username").ToString & "' And Type = 'D'"

        Dim List As String
        List = "ReasonCode, DerDescription, StartTime, EndTime, AHrs, Job, Suffix, OperNum, RowPointer"

        ds = New DataSet

        oWS = New SRNService.DOWebServiceSoapClient
        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Scrappeds", List, Filter, "CreateDate desc", "", 0)

        'If ds.Tables(0).Rows.Count > 0 Then

        GridView6.DataSource = ds
            GridView6.DataBind()

        'Dim dt As DataTable = ds.Tables(0)
        'Dim sum As Decimal = FormatNumber(Convert.ToDecimal(dt.Compute("SUM(Qty)", "Job = '" & Session("Job").ToString & "' And Suffix = '" & Session("Suffix").ToString & "' And OperNum = '" & Session("OperNum").ToString & "'")), LenPointQty)

        'lbltotalscrap.Text = Decimal.Round(sum.ToString, LenPointQty, MidpointRounding.AwayFromZero)

        'End If

    End Sub

    Public Function UnitQtyFormat() As Integer

        Dim strUnitQtyFormat As String = ""
        Dim PointQty As String = ""

        UnitQtyFormat = 0

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLInvparms", "QtyUnitFormat", "", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            strUnitQtyFormat = ds.Tables(0).Rows(0)("QtyUnitFormat").ToString

            Dim words As String() = strUnitQtyFormat.Split(New Char() {"."c})

            For Each word As String In words
                PointQty = words(1)
                Exit For
            Next

            UnitQtyFormat = Len(PointQty)

        End If

        Return UnitQtyFormat

    End Function

    Function GetStrShift(Employee As String) As String

        GetStrShift = ""

        Filter = "EmpNum = '" & Employee & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLEmployees", "Shift", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetStrShift = ds.Tables(0).Rows(0)("Shift").ToString
        End If

        Return GetStrShift

    End Function

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

    Function GetJobStockTran() As String

        GetJobStockTran = ""

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLSfcparms", "JobStockrm", "", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetJobStockTran = ds.Tables(0).Rows(0)("JobStockrm").ToString
        End If

        Return GetJobStockTran

    End Function

    Function GetCoProductMix(Job As String, Suffix As String) As String

        GetCoProductMix = ""

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLJobs", "CoProductMix", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetCoProductMix = ds.Tables(0).Rows(0)("CoProductMix").ToString
        End If

        Return GetCoProductMix

    End Function

    Function GetItemJob(Job As String, Suffix As String) As String

        GetItemJob = ""

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLJobs", "Item", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetItemJob = ds.Tables(0).Rows(0)("Item").ToString
        End If

        Return GetItemJob

    End Function

    Function GetItemUM(Item As String) As String

        GetItemUM = ""

        Filter = "Item = '" & Item & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLItems", "UM", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetItemUM = ds.Tables(0).Rows(0)("UM").ToString
        End If

        Return GetItemUM

    End Function

    Function GetJobroute(Job As String, Suffix As String, OperNum As String) As DataSet

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "' And OperNum = '" & OperNum & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLJobRoutes", "JshSchedDrv, Wc, jbrUf_jobroute_line, QtyReceived, QtyComplete", Filter, "", "", 0)


        Return ds

    End Function

    Function GetLocFromProdLine(ProdLine As String) As String

        GetLocFromProdLine = ""
        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet

        Filter = "line_num = '" & ProdLine & "'"

        ds = oWS.LoadDataSet(Session("Token"), "ppcc_prod_lines", "Loc", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then

            'If PJobStockTran = "1" Then
            GetLocFromProdLine = ds.Tables(0).Rows(0)("Loc").ToString

            'End If

        End If

        Return GetLocFromProdLine

    End Function

    Function GetLastOper(Job As String, Suffix As String) As String

        GetLastOper = ""

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "'"

        ds = oWS.LoadDataSet(Session("Token"), "SLJobRoutes", "OperNum", Filter, "OperNum DESC", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetLastOper = ds.Tables(0).Rows(0)("OperNum").ToString
        End If

        Return GetLastOper

    End Function

    Function GetUserInitial(UserName As String) As String

        GetUserInitial = ""
        Dim UserID As String = ""

        ds = New DataSet
        oWS = New SRNService.DOWebServiceSoapClient
        ds = oWS.LoadDataSet(Session("Token").ToString, "UserNames", "UserId", "Username='" & UserName & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            UserID = ds.Tables(0).Rows(0)("UserId").ToString
        End If

        ds = New DataSet
        oWS = New SRNService.DOWebServiceSoapClient
        ds = oWS.LoadDataSet(Session("Token").ToString, "SLUserLocals", "UserCode", "UserId='" & UserID & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetUserInitial = ds.Tables(0).Rows(0)("UserCode").ToString
        End If

        Return GetUserInitial

    End Function

    Function GetFirstOper(Job As String, Suffix As String) As String

        GetFirstOper = ""
        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "'"

        ds = oWS.LoadDataSet(Session("Token"), "SLJobRoutes", "OperNum", "", "OperNum", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetFirstOper = ds.Tables(0).Rows(0)("OperNum").ToString
        End If

        Return GetFirstOper

    End Function

    Function GetDefWhse() As String

        GetDefWhse = ""
        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token"), "SLInvparms", "DefWhse", "", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetDefWhse = ds.Tables(0).Rows(0)("DefWhse").ToString
        End If

        Return GetDefWhse

    End Function

    Function GetUserCode(UserID As String) As String

        GetUserCode = ""

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "UserNames", "UserId", "Username = '" & UserID & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then

            Filter = "UserId = '" & ds.Tables(0).Rows(0)("UserId").ToString & "'"

            oWS = New SRNService.DOWebServiceSoapClient

            ds = New DataSet

            ds = oWS.LoadDataSet(Session("Token").ToString, "SLUserLocals", "UserCode", Filter, "", "", 0)

            If ds.Tables(0).Rows.Count > 0 Then
                GetUserCode = ds.Tables(0).Rows(0)("UserCode").ToString
            End If

        End If

        Return GetUserCode

    End Function

    Function GetBFLot(Job As String, Suffix As String, Oper As String) As DataSet

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        Dim sList As String

        sList = "Selected, OperNum, Seq, Lot, Qty, UM, Item, ItemDesc, QtyOnHand, QtyNeeded, RowPointer, Loc, Whse, TransNum, TransSeq, EmpNum"

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "' And UserID = '" & Session("Username").ToString & "' and QtyReq > 0 and SessionID = '" & Session("PSession").ToString & "'"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Backflushs", sList, Filter, "", "", 0)

        Return ds

    End Function

#Region "Get Data Bind To Dropdownlist"

    Sub GetEmployee()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLEmployees", "EmpNum, Name", "", "EmpNum", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlemployee.Items.Add(New ListItem(dRow("EmpNum") & IIf(IsDBNull(dRow("Name")), "", " : " & dRow("Name")), dRow("EmpNum").ToString.Trim))

        Next

        ddlemployee.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetResource(Job As String, Suffix As String, Oper As String)

        ddlResource.Items.Clear()

        Parms = ""
        Parms = "<Parameters><Parameter>" & Job & "</Parameter>" &
                                "<Parameter>" & Suffix & "</Parameter>" &
                                "<Parameter>" & Oper & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter>" & DBNull.Value & "</Parameter>" &
                                "<Parameter  ByRef='Y'></Parameter>" &
                                "<Parameter>" & Session("Username").ToString & "</Parameter>" &
                                "<Parameter>" & PSite.ToString & "</Parameter></Parameters>"

        res = New Object
        oWS = New SRNService.DOWebServiceSoapClient
        res = oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_TmpRresources", "PPCC_EX_CLM_ResourceSp", Parms)

        Filter = "Job = '" & Job & "' And Suffix = '" & Suffix & "' And OperNum = '" & Oper & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_TmpRresources", "RESID, DESCR", Filter, "SEQNO", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlResource.Items.Add(New ListItem(dRow("RESID") & IIf(IsDBNull(dRow("DESCR")), "", " : " & dRow("DESCR")), dRow("RESID")))
        Next

        ddlResource.Items.Insert(0, New ListItem("", ""))

    End Sub


    Sub GetItemLoc(Item As String, Whse As String)

        Dim sProdLine As String = ""

        sProdLine = GetLocFromProdLine(txtProdLine.Text)

        ddlmovetoloc.Items.Clear()

        Filter = "Item = '" & Item & "' And Whse = '" & Whse & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLItemLocs", "Loc, LocDescription", Filter, "Rank", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlmovetoloc.Items.Add(New ListItem(dRow("Loc") & IIf(IsDBNull(dRow("LocDescription")), "", " : " & dRow("LocDescription")), dRow("Loc")))
        Next

        ddlmovetoloc.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetShift()

        ddlSchedulingShift.Items.Clear()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLSHIFTnnns", "SHIFTID, DESCR", Filter, "SHIFTID", "", 0)

        Dim dt_distinct As DataTable = ds.Tables(0).DefaultView.ToTable(True, "SHIFTID", "DESCR")

        For Each dRow As DataRow In dt_distinct.Rows
            ddlSchedulingShift.Items.Add(New ListItem(dRow("SHIFTID") & IIf(IsDBNull(dRow("DESCR")), "", " : " & dRow("DESCR")), dRow("SHIFTID")))
        Next

        ddlSchedulingShift.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetReasonCode()

        ddlscrapcode.Items.Clear()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLReasons", "ReasonCode, Description", "ReasonClass = 'MFG SCRAP'", "ReasonCode", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlscrapcode.Items.Add(New ListItem(dRow("ReasonCode") & IIf(IsDBNull(dRow("Description")), "", " : " & dRow("Description")), dRow("ReasonCode")))

        Next

        ddlscrapcode.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetReasonCodeDownTime()

        ddldowntime.Items.Clear()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "ppcc_downtime_reasoncodes", "reason_code, description", "", "reason_code", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddldowntime.Items.Add(New ListItem(dRow("reason_code") & IIf(IsDBNull(dRow("description")), "", " : " & dRow("description")), dRow("reason_code")))

        Next

        ddldowntime.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetItemScrap()

        'ddlSchedulingShift.Items.Clear()

        'oWS = New SRNService.DOWebServiceSoapClient

        'ds = New DataSet

        'ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_CoProducts", "Item, ItemDescription", "SessionID = '" & Session("PSession").ToString & "'", "Item", "", 0)

        'For Each dRow As DataRow In ds.Tables(0).Rows
        '    ddlitem.Items.Add(New ListItem(dRow("Item") & IIf(IsDBNull(dRow("ItemDescription")), "", " : " & dRow("ItemDescription")), dRow("Item")))
        'Next

        'ddlitem.Items.Insert(0, New ListItem("", ""))

    End Sub


#End Region

#Region "Bind Data To Gridview Tag"

    Sub BindGridviewTag()

        'Dim Filter As String
        'Dim Propertie As String

        'oWS = New SRNService.DOWebServiceSoapClient
        'ds = New DataSet
        'Filter = "SessionID = '" & Session("PSession").ToString & "'"
        'Propertie = "TagID, Lot, Qty"

        'ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_JobTranTags", Propertie, Filter, "CreateDate Desc", "", 0)

        'GridView2.DataSource = ds.Tables(0)
        'GridView2.DataBind()

        'If GridView2.Rows.Count > 0 Then

        '    Filter = "Job = '" & lbljob.Text & "' And Suffix = '" & lblSuffix.Text & "' And OperNum = '" & lbloper.Text & "' And SessionID = '" & Session("PSession").ToString & "'"

        '    oWS = New SRNService.DOWebServiceSoapClient

        '    ds = New DataSet

        '    ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_EX_CoProducts", "Item, QtyComplete, Lot", Filter, "Item", "", 0)

        '    GridView1.DataSource = ds.Tables(0)
        '    GridView1.DataBind()

        'End If


    End Sub

#End Region

End Class