Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection

Public Class JobReceiptDetail
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

        If Not Page.IsPostBack Then
            LenPointQty = UnitQtyFormat()
            BindGridview()

        End If


    End Sub

    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click

        Dim PostURL As String = ""
        Dim sDate, sJob, sSuffix, sItem, sOperNum, sQty, sLoc, sLot, sType As String

        sDate = Request.QueryString("Date")
        sJob = Request.QueryString("Job")
        sSuffix = Request.QueryString("Suffix")
        sItem = Request.QueryString("Item")
        sOperNum = Request.QueryString("OperNum")
        sQty = Request.QueryString("Qty")
        sLoc = Request.QueryString("Loc")
        sLot = Request.QueryString("Lot")
        sType = Request.QueryString("Type")


        PostURL = "?SessionID=" & Session("PSession").ToString & "&Date=" & sDate & ""
        PostURL = PostURL & "&Job=" & sJob & "&Suffix=" & sSuffix & "&Item=" & sItem & "&OperNum=" & sOperNum & ""
        PostURL = PostURL & "&Qty=" & sQty & "&Loc=" & sLoc & "&Lot=" & sLot & "&Type=" & sType & ""


        Response.Redirect("JobReceipt.aspx" & PostURL)

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Dim PostURL As String = ""
        Dim sDate, sJob, sSuffix, sItem, sOperNum, sQty, sLoc, sLot, sType As String

        sDate = Request.QueryString("Date")
        sJob = Request.QueryString("Job")
        sSuffix = Request.QueryString("Suffix")
        sItem = Request.QueryString("Item")
        sOperNum = Request.QueryString("OperNum")
        sQty = Request.QueryString("Qty")
        sLoc = Request.QueryString("Loc")
        sLot = Request.QueryString("Lot")
        sType = Request.QueryString("Type")


        PostURL = "?SessionID=" & Session("PSession").ToString & "&Date=" & sDate & ""
        PostURL = PostURL & "&Job=" & sJob & "&Suffix=" & sSuffix & "&Item=" & sItem & "&OperNum=" & sOperNum & ""
        PostURL = PostURL & "&Qty=" & sQty & "&Loc=" & sLoc & "&Lot=" & sLot & "&Type=" & sType & ""


        Response.Redirect("JobReceipt.aspx" & PostURL)

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        e.Row.Cells(2).Text = FormatNumber(e.Row.Cells(2).Text, LenPointQty)

    '    End If

    'End Sub

    Protected Sub PanelList_RowDataBound(sender As Object, e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblqty As Label = CType(e.Item.FindControl("lblqty"), Label)
            Dim chkScanLabelItem As CheckBox = CType(e.Item.FindControl("chkScanLabelItem"), CheckBox)
            Dim lblScanLabelItem As Label = CType(e.Item.FindControl("lblScanLabelItem"), Label)

            Dim Qty As Decimal = CDec(lblqty.Text)

            lblqty.Text = FormatNumber(lblqty.Text, LenPointQty)

            If lblScanLabelItem.Text = "0" Then
                chkScanLabelItem.Checked = False
            ElseIf lblScanLabelItem.Text = "1" Then
                chkScanLabelItem.Checked = True
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

#Region "Bind Data To Gridview"

    Sub BindGridview()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "'"
        Propertie = "TagID, Lot, Qty, LabelID, ScanLabelItem"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_JobReceiptDetail", Propertie, Filter, "CreateDate Desc", "", 0)

        'GridView1.DataSource = ds.Tables(0)
        'GridView1.DataBind()

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()


    End Sub
#End Region

End Class