Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection

Public Class OrderShippingDetail
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
            lblconum.Text = Request.QueryString("CoNum").ToString
            lblcoline.Text = Request.QueryString("CoLine").ToString
            lblcorelease.Text = Request.QueryString("CoRelease").ToString

            LenPointQty = UnitQtyFormat()
            BindGridview()

        End If


    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Dim PostURL As String = ""
        Dim strCoNum As String = Request.QueryString("CoNum").ToString
        Dim strCoLine As String = Request.QueryString("CoLine").ToString
        Dim strCoRelease As String = Request.QueryString("CoRelease").ToString
        Dim strOrderPickList As String = Request.QueryString("OrderPickList").ToString
        Dim strCoReturn As String = Request.QueryString("CoReturn").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&CoNum=" & strCoNum & "&CoLine=" & strCoLine & ""
        PostURL = PostURL & "&CoRelease=" & strCoRelease & "&OrderPickList=" & strOrderPickList & "&CoReturn=" & strCoReturn & ""

        Response.Redirect("OrderShipping.aspx" & PostURL)

    End Sub

    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click

        Dim PostURL As String = ""
        Dim strCoNum As String = Request.QueryString("CoNum").ToString
        Dim strCoLine As String = Request.QueryString("CoLine").ToString
        Dim strCoRelease As String = Request.QueryString("CoRelease").ToString
        Dim strOrderPickList As String = Request.QueryString("OrderPickList").ToString
        Dim strCoReturn As String = Request.QueryString("CoReturn").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&CoNum=" & strCoNum & "&CoLine=" & strCoLine & ""
        PostURL = PostURL & "&CoRelease=" & strCoRelease & "&OrderPickList=" & strOrderPickList & "&CoReturn=" & strCoReturn & ""

        Response.Redirect("OrderShipping.aspx" & PostURL)

    End Sub

    Protected Sub ListView1_ItemDataBound(ByVal sender As Object, ByVal e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        Dim QtyTag As Decimal = 0

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblListQtyReq As Label = CType(e.Item.FindControl("lblListQtyReq"), Label)
            Dim chkScanLabelItem As CheckBox = CType(e.Item.FindControl("chkScanLabelItem"), CheckBox)
            Dim lbllabelitem As Label = CType(e.Item.FindControl("lbllabelitem"), Label)

            QtyTag = CDec(lblListQtyReq.Text)

            lblListQtyReq.Text = FormatNumber(QtyTag.ToString, LenPointQty)

            If lbllabelitem.Text = "0" Then
                chkScanLabelItem.Checked = False
            Else
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
        Filter = "SessionID='" & Session("PSession").ToString & "' and CoNum = '" & lblconum.Text & "' and CoLine = '" & lblcoline.Text & "' and CoRelease = '" & lblcorelease.Text & "'"
        Propertie = "DerCO, Item, InvNum, QtyTag, LocTo, Lot, PDS_DN, Loc, PalletNo, TagID, CheckCustDoc, CustItem, LabelItem, LabelID"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_OrderShipDetails", Propertie, Filter, "CreateDate Desc", "", 0)

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()


    End Sub
#End Region



End Class