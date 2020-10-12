Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection


Public Class CycleCountDetail
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
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
            LenPointQty = UnitQtyFormat()
            BindGridview()

        End If



    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Dim PostURL As String = ""
        Dim strLoc As String = Request.QueryString("Loc").ToString
        Dim strWhse As String = Request.QueryString("Whse").ToString
        Dim strItem As String = Request.QueryString("Item").ToString
        Dim strLot As String = Request.QueryString("Lot").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&Item=" & strItem & "&Lot=" & strLot & ""
        PostURL = PostURL & "&Loc=" & strLoc & "&Whse=" & strWhse & ""

        Response.Redirect("CycleCount.aspx" & PostURL)

    End Sub

    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click

        Dim PostURL As String = ""
        Dim strLoc As String = Request.QueryString("Loc").ToString
        Dim strWhse As String = Request.QueryString("Whse").ToString
        Dim strItem As String = Request.QueryString("Item").ToString
        Dim strLot As String = Request.QueryString("Lot").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&Item=" & strItem & "&Lot=" & strLot & ""
        PostURL = PostURL & "&Loc=" & strLoc & "&Whse=" & strWhse & ""

        Response.Redirect("CycleCount.aspx" & PostURL)

    End Sub

    Protected Sub PanelList_RowDataBound(sender As Object, e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblqty As Label = CType(e.Item.FindControl("lblqty"), Label)

            Dim lblstatus As Label = CType(e.Item.FindControl("lblstatus"), Label)

            Dim Qty As Decimal = CDec(lblqty.Text)

            lblqty.Text = FormatNumber(lblqty.Text, LenPointQty)

            If lblstatus.Text = "N" Then
                lblstatus.Text = "Not Counted"
                lblstatus.ForeColor = Drawing.Color.Red
            ElseIf lblstatus.Text = "E" Then
                lblstatus.Text = "Exception"
            ElseIf lblstatus.Text = "C" Then
                lblstatus.Text = "Counted"
                lblstatus.ForeColor = Drawing.Color.Green
            ElseIf lblstatus.Text = "P" Then
                lblstatus.Text = "Posted"
            End If


        End If

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        Dim Qty As Integer = CDec(e.Row.Cells(3).Text)

    '        e.Row.Cells(3).Text = FormatNumber(Qty, LenPointQty)

    '        If e.Row.Cells(4).Text = "N" Then
    '            e.Row.Cells(4).Text = "Not Counted"
    '            e.Row.Cells(4).ForeColor = Drawing.Color.Red
    '        ElseIf e.Row.Cells(4).Text = "E" Then
    '            e.Row.Cells(4).Text = "Exception"
    '        ElseIf e.Row.Cells(4).Text = "C" Then
    '            e.Row.Cells(4).Text = "Counted"
    '            e.Row.Cells(4).ForeColor = Drawing.Color.Green
    '        ElseIf e.Row.Cells(4).Text = "P" Then
    '            e.Row.Cells(4).Text = "Posted"
    '        End If

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

#Region "Bind Data To Gridview"

    Sub BindGridview()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "item = '" & Request.QueryString("Item").ToString & "' and loc = '" & Request.QueryString("Loc").ToString & "' and whse = '" & Request.QueryString("Whse").ToString & "' and lot = '" & Request.QueryString("Lot").ToString & "' and stat = 'C'"
        Propertie = "tag_id, item, loc, lot, count_qty, item_description, stat"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_CycleCntDetail", Propertie, Filter, "CreateDate Desc", "", 0)

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()

        'GridView1.DataSource = ds.Tables(0)
        'GridView1.DataBind()


    End Sub
#End Region

End Class