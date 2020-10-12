Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class JobMatlTran
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
    Dim res As Object
    Dim SGUID As String
    Dim Parms As String
    Dim Propertie As String
    Dim DateNow As String
    Dim LenPointQty As Integer = 0

    Private Shared ParmSite As String
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

            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow

            PWhse = GetDefWhse()

            If Request.QueryString("SessionID") <> "" Or Not String.IsNullOrEmpty(Request.QueryString("SessionID")) Then

                txtjob.Text = Request.QueryString("Job")
                txtsuffix.Text = Request.QueryString("Suffix")
                txtOperNum.Text = Request.QueryString("OperNum")
                Session("Stat") = Request.QueryString("Type")

                BindGridview()

            Else

                Session("Stat") = "Issue"

            End If


        End If

        If txtjob.Text = "" Then
            lblbarcode.Text = "Scan Job Order: "
        Else
            lblbarcode.Text = "Scan Tag: "
        End If

        txtbarcode.Focus()

        txtbarcode.Attributes.Add("onchange", "javascript:scanbarcode();")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim sBarcode, Stat, MsgErr, MsgType, strJob, strSuffix, strOperNum As String
        sBarcode = txtbarcode.Text
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""
        strJob = ""
        strSuffix = ""
        strOperNum = ""

        If txtbarcode.Text <> "" Then

            If lblbarcode.Text = "Scan Job Order: " Then


                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "J" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Issue", "I", "W") & "</Parameter>" &
                        "<Parameter>" & "I" & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"




            ElseIf lblbarcode.Text = "Scan Tag: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "T" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Issue", "I", "W") & "</Parameter>" &
                        "<Parameter>" & "I" & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"




            End If

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobMatlDetail", "PPCC_Ex_JobMatlSp", Parms)

            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(Parms)

            Dim i As Integer = 1

            For Each node As XmlNode In doc.DocumentElement

                If i = 14 Then
                    Stat = node.InnerText

                ElseIf i = 15 Then
                    MsgType = node.InnerText

                ElseIf i = 16 Then
                    MsgErr = node.InnerText

                End If

                i += 1

            Next

            If Stat = "TRUE" Then

                If lblbarcode.Text = "Scan Job Order: " Then

                    Dim Position1 As Integer
                    Dim Position2 As Integer

                    Position1 = InStr(sBarcode, "-")
                    Position2 = InStr(sBarcode.Substring(Position1), "-")

                    If Position1 <> 0 And Position2 <> 0 Then

                        Dim arrBarcode As String()
                        arrBarcode = sBarcode.Split(New Char() {"-"c})

                        If arrBarcode.Length > 0 Then

                            strJob = arrBarcode(0)
                            strSuffix = arrBarcode(1)
                            strOperNum = arrBarcode(2)

                        End If

                    End If

                    txtjob.Text = strJob
                    txtsuffix.Text = strSuffix
                    txtOperNum.Text = strOperNum

                End If

                BindGridview()

            Else

                MsgErr = MsgErr.Replace("'", "\'")
                MsgType = "Error [" & MsgType & "]"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                'NotPassNotifyPanel.Visible = True
                'NotPassText.Text = MsgErr

            End If



        End If

        If txtjob.Text = "" Then
            lblbarcode.Text = "Scan Job Order: "
        Else
            lblbarcode.Text = "Scan Tag: "
        End If

        txtbarcode.Text = String.Empty

    End Sub

    Protected Sub btnstat_Click(sender As Object, e As EventArgs) Handles btnstat.Click

        PanelList.DataSource = Nothing
        PanelList.DataBind()

        If Session("Stat").ToString = "Issue" Then
            btnstat.CssClass = "btn btn btn-outline-danger btn-block btn-sm"
            btnstat.Text = "<i class=""fa fa-redo-alt"" aria-hidden=""true""></i>" & " <strong>Return</strong>"
            Session("Stat") = "Return"
        Else
            btnstat.CssClass = "btn btn-outline-success btn-block btn-sm"
            btnstat.Text = "<i class=""fa fa-arrow-right"" aria-hidden=""true""></i>" & " <strong>Issue</strong>"
            Session("Stat") = "Issue"
        End If

    End Sub

    Protected Sub btnprocess_Click(sender As Object, e As EventArgs) Handles btnprocess.Click

        Dim sBarcode, Stat, MsgErr, MsgType As String
        sBarcode = txtbarcode.Text
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Issue", "I", "W") & "</Parameter>" &
                        "<Parameter>" & "P" & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"



        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobMatlDetail", "PPCC_Ex_JobMatlSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 14 Then
                Stat = node.InnerText

            ElseIf i = 15 Then
                MsgType = node.InnerText

            ElseIf i = 16 Then
                MsgErr = node.InnerText

            End If

            i += 1

        Next

        If Stat = "TRUE" Then

            Clear()

            MsgErr = MsgErr.Replace("'", "\'")


            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Success','" & MsgErr & "', 'success');", True)

        Else

            MsgErr = MsgErr.Replace("'", "\'")

            MsgType = "Error [" & MsgType & "]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

            'NotPassNotifyPanel.Visible = True
            'NotPassText.Text = MsgErr

        End If

    End Sub

    Protected Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click

        Dim Stat, MsgErr, MsgType As String
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse & "</Parameter>" &
                        "<Parameter>" & "" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & "0" & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Issue", "I", "W") & "</Parameter>" &
                        "<Parameter>" & "R" & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobMatlDetail", "PPCC_Ex_JobMatlSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 14 Then
                Stat = node.InnerText

            ElseIf i = 15 Then
                MsgType = node.InnerText

            ElseIf i = 16 Then
                MsgErr = node.InnerText

            End If

            i += 1

        Next

        If Stat = "TRUE" Then

            Response.Redirect("JobMatlTran.aspx")

        Else

            MsgErr = MsgErr.Replace("'", "\'")
            MsgType = "Error [" & MsgType & "]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

            'NotPassNotifyPanel.Visible = True
            'NotPassText.Text = MsgErr

        End If

    End Sub

    Sub Clear()

        DateNow = Date.Now.ToString("dd/MM/yyyy")
        txtdate.Text = DateNow

        Session("Stat") = "Issue"
        btnstat.CssClass = "btn btn-outline-success btn-block btn-sm"
        btnstat.Text = "<i class=""fa fa-arrow-right"" aria-hidden=""true""></i>" & " <strong>Issue</strong>"
        lblbarcode.Text = "Scan Job Order: "

        chkCancelTag.Checked = False
        txtbarcode.Text = String.Empty
        txtjob.Text = String.Empty
        txtsuffix.Text = String.Empty
        txtOperNum.Text = String.Empty

        PanelList.DataSource = Nothing
        PanelList.DataBind()

        SGUID = System.Guid.NewGuid.ToString()
        Session("PSession") = SGUID

    End Sub

    Protected Sub btndetail_Click(sender As Object, e As EventArgs) Handles btndetail.Click
        Dim PostURL As String = ""
        PostURL = "?Job=" & txtjob.Text & "&Suffix=" & txtsuffix.Text & "&OperNum=" & txtOperNum.Text & "&SessionID=" & Session("PSession").ToString & "&Type=" & Session("Stat").ToString & ""
        Response.Redirect("JobMatlTranDetail.aspx" & PostURL)
    End Sub

    Protected Sub ListView1_ItemDataBound(ByVal sender As Object, ByVal e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        Dim QtyReq As Double = 0
        Dim QtyIssue As Double = 0
        Dim QtyRemain As Double = 0

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblListQtyReq As Label = CType(e.Item.FindControl("lblListQtyReq"), Label)
            Dim lblListQtyIssue As Label = CType(e.Item.FindControl("lblListQtyIssue"), Label)
            Dim lblListRemain As Label = CType(e.Item.FindControl("lblListRemain"), Label)

            QtyReq = CDec(lblListQtyReq.Text)

            QtyIssue = CDec(IIf(lblListQtyIssue.Text = "", 0, lblListQtyIssue.Text))

            QtyRemain = CDec(lblListRemain.Text)


            lblListQtyReq.Text = FormatNumber(QtyReq.ToString, LenPointQty)
            lblListQtyIssue.Text = FormatNumber(QtyIssue.ToString, LenPointQty)
            lblListRemain.Text = FormatNumber(QtyRemain.ToString, LenPointQty)

        End If

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




#Region "Bind Data To Gridview"

    Sub BindGridview()

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "'"
        Propertie = "Oper, Item, QtyRequire, QtyIssue, QtyRemain"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_JobMatlSum", Propertie, Filter, "CreateDate Desc", "", 0)

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()


    End Sub

#End Region

End Class