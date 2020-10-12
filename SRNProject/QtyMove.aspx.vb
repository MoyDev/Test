Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class QtyMove
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
    Dim res As Object
    Dim SGUID As String
    Dim Parms As String
    Dim LenPointQty As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("PSession") Is Nothing Then
            Response.Redirect("signin.aspx")
        Else
            If Session("PSession").ToString = "" Then
                Response.Redirect("signin.aspx")
            End If
        End If

        'If Session("Employee") Is Nothing Then
        '    Response.Redirect("Menu.aspx")
        'Else
        '    If Session("Employee").ToString = "" Then
        '        Response.Redirect("Menu.aspx")
        '    End If

        'End If

        If Not Page.IsPostBack Then

            Dim DateNow As String
            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow


            SGUID = System.Guid.NewGuid.ToString()
            Session("PSession") = SGUID

            '------Bind DropdownList------'
            GetWarehouse()
            GetLocFrom()
            GetLocTo()
            '------Bind DropdownList------'

            'LenPointQty = UnitQtyFormat()

            'MsgBox(Session("PSession").ToString)


        End If

        LenPointQty = UnitQtyFormat()

        txtbarcode.Focus()

        If ddlformloc.SelectedItem.Value = "" Then
            lblbarcode.Text = "Scan From Location: "
        ElseIf ddltoloc.SelectedItem.Value = "" Then
            lblbarcode.Text = "Scan To Location: "
        Else
            lblbarcode.Text = "Scan Tag: "
        End If

        txtbarcode.Attributes.Add("onchange", "javascript:scanbarcode();")

    End Sub

    Protected Sub btnswitchloc_Click(sender As Object, e As EventArgs) Handles btnswitchloc.Click

        Dim strFromloc, strToLoc As String
        strFromloc = ddlformloc.SelectedItem.Value
        strToLoc = ddltoloc.SelectedItem.Value

        ddlformloc.SelectedIndex = ddlformloc.Items.IndexOf(ddlformloc.Items.FindByValue(strToLoc))
        ddltoloc.SelectedIndex = ddltoloc.Items.IndexOf(ddltoloc.Items.FindByValue(strFromloc))

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'NotPassNotifyPanel.Visible = False
        'NotPassText.Text = ""
        'PassNotifyPanel.Visible = False
        'PassText.Text = ""
        'WarningNotifyPanel.Visible = False
        'WarningText.Text = ""

        Dim sBarcode, Stat, MsgErr, MsgType As String
        sBarcode = txtbarcode.Text
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        If txtbarcode.Text <> "" Then

            If lblbarcode.Text = "Scan From Location: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & "F" & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & ddlwhse.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddlformloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddltoloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_QtyMove", "PPCC_Ex_QtyMoveSp", Parms)

                Dim doc As XmlDocument = New XmlDocument()
                doc.LoadXml(Parms)

                Dim i As Integer = 1

                For Each node As XmlNode In doc.DocumentElement

                    If i = 10 Then
                        Stat = node.InnerText

                    ElseIf i = 11 Then
                        MsgType = node.InnerText

                    ElseIf i = 12 Then
                        MsgErr = node.InnerText

                    End If

                    i += 1

                Next

                If Stat = "TRUE" Then

                    ddlformloc.SelectedIndex = ddlformloc.Items.IndexOf(ddlformloc.Items.FindByValue(sBarcode))

                    If ddltoloc.SelectedItem.Value = "" Then
                        lblbarcode.Text = "Scan To Location: "
                    Else
                        lblbarcode.Text = "Scan Tag: "
                    End If

                Else

                    MsgErr = MsgErr.Replace("'", "\'")

                    MsgType = "Error [" & MsgType & "]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    'NotPassNotifyPanel.Visible = True
                    'NotPassText.Text = MsgErr

                End If

            ElseIf lblbarcode.Text = "Scan To Location: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & "O" & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & ddlwhse.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddlformloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddltoloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_QtyMove", "PPCC_Ex_QtyMoveSp", Parms)

                Dim doc As XmlDocument = New XmlDocument()
                doc.LoadXml(Parms)

                Dim i As Integer = 1

                For Each node As XmlNode In doc.DocumentElement

                    If i = 10 Then
                        Stat = node.InnerText

                    ElseIf i = 11 Then
                        MsgType = node.InnerText

                    ElseIf i = 12 Then
                        MsgErr = node.InnerText

                    End If

                    i += 1

                Next

                If Stat = "TRUE" Then

                    ddltoloc.SelectedIndex = ddltoloc.Items.IndexOf(ddltoloc.Items.FindByValue(sBarcode))
                    txtbarcode.Text = String.Empty

                    If ddlformloc.SelectedItem.Value = "" Then
                        lblbarcode.Text = "Scan Form Location: "
                    Else
                        lblbarcode.Text = "Scan Tag: "
                    End If

                Else

                    MsgErr = MsgErr.Replace("'", "\'")

                    MsgType = "Error [" & MsgType & "]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    'NotPassNotifyPanel.Visible = True
                    'NotPassText.Text = MsgErr

                End If

            ElseIf lblbarcode.Text = "Scan Tag: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & "T" & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & ddlwhse.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddlformloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & ddltoloc.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_QtyMove", "PPCC_Ex_QtyMoveSp", Parms)

                Dim doc As XmlDocument = New XmlDocument()
                doc.LoadXml(Parms)

                Dim i As Integer = 1

                For Each node As XmlNode In doc.DocumentElement

                    If i = 10 Then
                        Stat = node.InnerText

                    ElseIf i = 11 Then
                        MsgType = node.InnerText

                    ElseIf i = 12 Then
                        MsgErr = node.InnerText

                    End If

                    i += 1

                Next

                If Stat = "TRUE" Then

                    BindGridview()

                Else

                    MsgErr = MsgErr.Replace("'", "\'")
                    MsgErr = MsgErr.Replace(vbLf, " ")

                    MsgType = "Error [" & MsgType & "]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    'NotPassNotifyPanel.Visible = True
                    'NotPassText.Text = MsgErr

                End If



            End If


        End If

        txtbarcode.Text = String.Empty

    End Sub

    Protected Sub PanelList_RowDataBound(sender As Object, e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblqty As Label = CType(e.Item.FindControl("lblqty"), Label)

            lblqty.Text = FormatNumber(lblqty.Text, LenPointQty)

        End If

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        Dim Qty As Integer = CDec(e.Row.Cells(6).Text)

    '        Qty = FormatNumber(Qty, LenPointQty)

    '    End If

    'End Sub

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



#Region "Get Data Bind To Dropdownlist"

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

    Sub GetLocFrom()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLLocations", "Loc", "", "Loc", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlformloc.Items.Add(New ListItem(dRow("Loc"), dRow("Loc")))
        Next

        ddlformloc.Items.Insert(0, New ListItem("", ""))

    End Sub

    Sub GetLocTo()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLLocations", "Loc", "", "Loc", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddltoloc.Items.Add(New ListItem(dRow("Loc"), dRow("Loc")))
        Next

        ddltoloc.Items.Insert(0, New ListItem("", ""))

    End Sub

#End Region

#Region "Bind Data To Gridview"

    Sub BindGridview()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "'"
        Propertie = "TagID, Item, FromLoc, ToLoc, Lot, VendLot, Qty"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_QtyMove", Propertie, Filter, "CreateDate Desc", "", 0)

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()

        'GridView1.DataSource = ds.Tables(0)
        'GridView1.DataBind()


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