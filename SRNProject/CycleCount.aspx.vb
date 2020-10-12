Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class CycleCount
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
    Dim ds2 As DataSet
    Dim Filter2 As String
    Dim res As Object
    Dim SGUID As String
    Dim Parms As String
    Dim DateNow As String
    Dim LenPointQty As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("PSession") Is Nothing Then
            Response.Redirect("signin.aspx")
        Else
            If Session("PSession").ToString = "" Then
                Response.Redirect("signin.aspx")
            End If
        End If


        If Not Page.IsPostBack Then

            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow

            GetWarehouse()
            GetLoc()

            If Request.QueryString("SessionID") <> "" Or Not String.IsNullOrEmpty(Request.QueryString("SessionID")) Then
                '    SGUID = System.Guid.NewGuid.ToString()
                '    Session("PSession") = SGUID
                'Else
                ddlwhse.SelectedIndex = ddlwhse.Items.IndexOf(ddlwhse.Items.FindByValue(Request.QueryString("Whse").ToString))
                ddlloc.SelectedIndex = ddlloc.Items.IndexOf(ddlloc.Items.FindByValue(Request.QueryString("Loc").ToString))
            End If

            BindGridview()

        End If

        LenPointQty = UnitQtyFormat()

        txtbarcode.Focus()

        txtbarcode.Attributes.Add("onchange", "javascript:scanbarcode();")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'NotPassNotifyPanel.Visible = False
        'NotPassText.Text = ""
        'PassNotifyPanel.Visible = False
        'PassText.Text = ""
        'WarningNotifyPanel.Visible = False
        'WarningText.Text = ""

        Dim sBarcode, Stat, MsgErr, MsgType, Prompt As String
        sBarcode = txtbarcode.Text
        Stat = "TRUE"
        MsgErr = ""
        MsgType = ""
        Prompt = ""

        If txtbarcode.Text <> "" And txtprocess.Text <> "I" Then

            ValidateDataBeforeProcess(sBarcode, ddlwhse.SelectedItem.Value, ddlloc.SelectedItem.Value, Stat, MsgType, MsgErr, Prompt)

            If Stat = "FALSE" Then

                If Prompt = "Error" Then

                    MsgErr = MsgErr.Replace("'", "\'")

                    MsgType = "Error [" & MsgType & "]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    txtbarcode.Text = String.Empty

                ElseIf Prompt = "Prompt" Then

                    'Prompt Insert ppcc_ex_cycelcount_tag

                    MsgErr = MsgErr.Replace("'", "\'")

                    MsgType = "Error [" & MsgType & "]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & Prompt & "','" & MsgErr & "', 'error');", True)


                End If

            Else

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & txtdate.Text & "</Parameter>" &
                            "<Parameter>" & 0 & "</Parameter>" &
                            "<Parameter>" & "P" & "</Parameter>" &
                            "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                            "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_CycleCntDetail", "PPCC_Ex_CycleCountSp", Parms)

                BindGridview()
                txtbarcode.Text = String.Empty

            End If



        End If

        If txtbarcode.Text <> "" And txtprocess.Text = "I" Then

            Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
            "<Parameter>" & sBarcode & "</Parameter>" &
            "<Parameter>" & txtdate.Text & "</Parameter>" &
            "<Parameter>" & 0 & "</Parameter>" &
            "<Parameter>" & "I" & "</Parameter>" &
            "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
            "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
            "</Parameters>"

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_CycleCntDetail", "PPCC_Ex_CycleCountSp", Parms)

            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(Parms)

            Dim i As Integer = 1

            For Each node As XmlNode In doc.DocumentElement

                If i = 8 Then
                    Stat = node.InnerText

                ElseIf i = 9 Then
                    MsgType = node.InnerText

                ElseIf i = 10 Then
                    MsgErr = node.InnerText

                End If

                i += 1

            Next

            If Stat = "FALSE" Then

                MsgErr = MsgErr.Replace("'", "\'")

                MsgType = "Error [" & MsgType & "]"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                txtbarcode.Text = String.Empty
                txtprocess.Text = String.Empty

            Else

                BindGridview()
                txtbarcode.Text = String.Empty
                txtprocess.Text = String.Empty

            End If



        End If



    End Sub

    Protected Sub ddlloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlloc.SelectedIndexChanged
        BindGridview()
    End Sub


    Sub Clear()

        DateNow = Date.Now.ToString("dd/MM/yyyy")
        txtdate.Text = DateNow

        txtbarcode.Text = String.Empty
        txtprocess.Text = String.Empty

        PanelList.DataSource = Nothing
        PanelList.DataBind()


        'GridView1.DataSource = Nothing
        'GridView1.DataBind()



    End Sub

    Protected Sub PanelList_RowDataBound(sender As Object, e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim lblRemain As Label = CType(e.Item.FindControl("lblremain"), Label)
            Dim lblqty As Label = CType(e.Item.FindControl("lblqty"), Label)
            Dim lblqtycount As Label = CType(e.Item.FindControl("lblqtycount"), Label)
            Dim strQtyCount As String = IIf(lblqtycount.Text = "&nbsp;", "0", IIf(lblqtycount.Text = "", "0", lblqtycount.Text))

            Dim lnkStatus As LinkButton = CType(e.Item.FindControl("lnkStatus"), LinkButton)

            Dim Qty As Decimal = CDec(lblqty.Text)
            Dim QtyCount As Decimal = CDec(strQtyCount.ToString)

            lblqty.Text = FormatNumber(lblqty.Text, LenPointQty)
            lblqtycount.Text = FormatNumber(QtyCount, LenPointQty)
            lblRemain.Text = FormatNumber((Qty - QtyCount), LenPointQty)

            If lnkStatus.Text = "N" Then
                lnkStatus.Text = "Not Counted"
                lnkStatus.ForeColor = Drawing.Color.Red
            ElseIf lnkStatus.Text = "E" Then
                lnkStatus.Text = "Exception"
            ElseIf lnkStatus.Text = "C" Then
                lnkStatus.Text = "Counted"
                lnkStatus.ForeColor = Drawing.Color.Green
            ElseIf lnkStatus.Text = "P" Then
                lnkStatus.Text = "Posted"
            End If


        End If

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        Dim Qty As Decimal = CDec(e.Row.Cells(1).Text)
    '        Dim strQtyCount As String = IIf(e.Row.Cells(2).Text = "&nbsp;", "0", e.Row.Cells(2).Text)
    '        Dim lblRemain As Label = CType(e.Row.FindControl("lblRemain"), Label)
    '        Dim QtyCount As Decimal = CDec(strQtyCount)
    '        Dim lnkStatus As LinkButton = CType(e.Row.FindControl("lnkStatus"), LinkButton)

    '        e.Row.Cells(1).Text = FormatNumber(e.Row.Cells(1).Text, LenPointQty)
    '        e.Row.Cells(2).Text = FormatNumber(CDec(QtyCount), LenPointQty)
    '        lblRemain.Text = FormatNumber((Qty - QtyCount), LenPointQty)

    '        If lnkStatus.Text = "N" Then
    '            lnkStatus.Text = "Not Counted"
    '            lnkStatus.ForeColor = Drawing.Color.Red
    '        ElseIf lnkStatus.Text = "E" Then
    '            lnkStatus.Text = "Exception"
    '        ElseIf lnkStatus.Text = "C" Then
    '            lnkStatus.Text = "Counted"
    '            lnkStatus.ForeColor = Drawing.Color.Green
    '        ElseIf lnkStatus.Text = "P" Then
    '            lnkStatus.Text = "Posted"
    '        End If

    '    End If

    'End Sub

    Sub Display(ByVal sender As Object, ByVal e As EventArgs)

        'Dim rowIndex As Integer = Convert.ToInt32(TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow).RowIndex)
        'Dim row As GridViewRow = GridView1.Rows(rowIndex)


        'Dim PostURL As String = ""

        'PostURL = "?SessionID=" & Session("PSession").ToString & "&Item=" & TryCast(row.FindControl("lblItem"), Label).Text & "&Lot=" & TryCast(row.FindControl("lblLot"), Label).Text & ""
        'PostURL = PostURL & "&Loc=" & ddlloc.SelectedItem.Value & "&Whse=" & ddlwhse.SelectedItem.Value & ""

        'Response.Redirect("CycleCountDetail.aspx" & PostURL)

    End Sub

    Protected Sub PanelList_ItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs) Handles PanelList.ItemCommand
        'Dim lblItem As Label = CType(e.Item.FindControl("lblItem"), Label)
        'Dim lblLot As Label = CType(e.Item.FindControl("lblLot"), Label)

        Dim PostURL As String = ""

        PostURL = "?SessionID=" & Session("PSession").ToString & "&Item=" & TryCast(e.Item.FindControl("lblItem"), Label).Text & "&Lot=" & TryCast(e.Item.FindControl("lblLot"), Label).Text & ""
        PostURL = PostURL & "&Loc=" & ddlloc.SelectedItem.Value & "&Whse=" & ddlwhse.SelectedItem.Value & ""

        Response.Redirect("CycleCountDetail.aspx" & PostURL)

    End Sub

    Sub ValidateDataBeforeProcess(ByVal TagID As String, ByVal Whse As String, ByVal Loc As String,
                                  ByRef Stat As String, ByRef MsgType As String, ByRef Msg As String, ByRef Prompt As String)

        Dim oWhse As String = ""
        Dim oLoc As String = ""
        Dim oStat As String = ""




        '#### Check Tag
        '#### Check Whse and Location
        '#### Check Tag have data within cyclecount tag and Item have in Cycle Master

        Stat = "TRUE"
        MsgType = ""
        Msg = ""
        Prompt = "Error"

        oWS = New SRNService.DOWebServiceSoapClient

        Dim Filter As String

        If Stat = "TRUE" Then

            ds = New DataSet

            Filter = "TagID = '" & TagID & "'"

            ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Tags", "TagID,Whse,Loc,Lot", Filter, "", "", 0)

            If ds.Tables(0).Rows.Count = 0 Then
                Stat = "FALSE"
                MsgType = "PPCC"
                Msg = "#200 : Doesn't Exists TagID"
                Prompt = "Error"

            ElseIf ds.Tables(0).Rows.Count > 0 Then

                oWhse = ds.Tables(0).Rows(0)("Whse").ToString
                oLoc = ds.Tables(0).Rows(0)("Loc").ToString

                If oWhse <> Whse Or oLoc <> Loc Then
                    Stat = "FALSE"
                    MsgType = "PPCC"
                    Msg = "#210 : Tag Doesn't Exists in Warehouse [" & Whse & "] And Location [" & Loc & "]"
                    Prompt = "Error"

                End If



            End If


        End If



        If Stat = "TRUE" Then

            Dim oItem As String = ""

            ds = New DataSet

            Filter = "TagID = '" & TagID & "'"

            ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Tags", "Item", Filter, "", "", 0)

            If ds.Tables(0).Rows.Count > 0 Then
                oItem = ds.Tables(0).Rows(0)("Item").ToString
            End If

            If oItem <> "" Then

                ds = New DataSet

                Filter = "tag_id = '" & TagID & "'"

                ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_CycleCntDetail", "tag_id,stat", Filter, "", "", 0)

                If ds.Tables(0).Rows.Count = 0 Then

                    ds = New DataSet

                    Filter = "Item = '" & oItem & "'"

                    ds = oWS.LoadDataSet(Session("Token").ToString, "SLCycles", "Item", Filter, "", "", 0)

                    If ds.Tables(0).Rows.Count > 0 Then

                        Stat = "FALSE" '
                        MsgType = "PPCC"
                        Msg = TagID & ": doesn't exists in cycle count tag"
                        Prompt = "Prompt"

                    Else

                        Stat = "FALSE" '
                        MsgType = "PPCC"
                        Msg = oItem & ": doesn't exists in cycle count tag"
                        Prompt = "Error"

                    End If

                Else

                    oStat = ds.Tables(0).Rows(0)("stat").ToString
                    If oStat = "C" Then
                        Stat = "FALSE"
                        MsgType = "PPCC"
                        Msg = "#220 : Tag : " & TagID & "has counted already."
                        Prompt = "Error"

                    End If

                End If

            End If



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

    Sub GetWarehouse()

        Dim DefWhse As String = ""

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLWhses", "Whse", "", "Whse", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlwhse.Items.Add(New ListItem(dRow("Whse"), dRow("Whse")))
        Next

        ddlwhse.Items.Insert(0, New ListItem("", ""))

        DefWhse = GetDefWhse()

        If DefWhse <> "" Then
            ddlwhse.SelectedValue = DefWhse.ToString
        End If

    End Sub

    Sub GetLoc()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet
        Dim crItem As ListItem

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLCycles", "Loc", "", "Loc", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            crItem = ddlloc.Items.FindByValue(dRow("Loc"))


            If ddlloc.Items.FindByValue(dRow("Loc")) Is Nothing Then
                ddlloc.Items.Add(New ListItem(dRow("Loc"), dRow("Loc")))
            End If


            ' End If
        Next

        ddlloc.Items.Insert(0, New ListItem("", ""))

    End Sub


#Region "Bind Data To Gridview"

    Sub BindGridview()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "Whse = '" & ddlwhse.SelectedItem.Value & "' And Loc = '" & ddlloc.SelectedItem.Value & "'"
        Propertie = "Item, Loc, Lot, CutOffQty, CountQty, Stat, Description"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_SLCycles", Propertie, Filter, "RecordDate Desc", "", 0)

        'GridView1.DataSource = ds.Tables(0)
        'GridView1.DataBind()

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()

    End Sub
#End Region

#Region "Function"

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

#End Region

End Class