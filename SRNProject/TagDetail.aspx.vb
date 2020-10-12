Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection

Public Class TagDetail
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
            BindGridview()

        End If

        LenPointQty = UnitQtyFormat()

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

        ds = oWS.LoadDataSet(Session("Token").ToString, "ppcc_ex_jobtran_tags", Propertie, Filter, "CreateDate Desc", "", 0)

        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()


    End Sub
#End Region

End Class