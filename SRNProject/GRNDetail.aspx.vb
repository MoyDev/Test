Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection

Public Class GRNDetail
    Inherits System.Web.UI.Page

    Dim oWS As SRNService.DOWebServiceSoapClient
    Dim ds As DataSet
    Dim Filter As String
    Dim LenPointQty As Integer = 0
    Dim Parms As String

    Private Shared Whse As String
    Private Shared ParmSite As String

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

            PSite = GetSite()
            PWhse = GetDefWhse()

            BindGridview()


        End If

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        Dim PostURL As String = ""
        Dim strVendor As String = Request.QueryString("Vendor").ToString
        Dim strVendorInvoice As String = Request.QueryString("VendorInvoice").ToString
        Dim strWhse As String = Request.QueryString("Whse").ToString
        Dim strGRNType As String = Request.QueryString("GRNType").ToString
        Dim strGRN As String = Request.QueryString("GRN").ToString
        Dim strTotalLine As String = Request.QueryString("TotalLine").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&Vendor=" & strVendor & "&VendorInvoice=" & strVendorInvoice & ""
        PostURL = PostURL & "&Whse=" & strWhse & "&GRNType=" & strGRNType & "&GRN=" & strGRN & "&TotalLine=" & strTotalLine & ""

        Response.Redirect("GenerateGRN.aspx" & PostURL)

    End Sub

    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click

        Dim PostURL As String = ""
        Dim strVendor As String = Request.QueryString("Vendor").ToString
        Dim strVendorInvoice As String = Request.QueryString("VendorInvoice").ToString
        Dim strWhse As String = Request.QueryString("Whse").ToString
        Dim strGRNType As String = Request.QueryString("GRNType").ToString
        Dim strGRN As String = Request.QueryString("GRN").ToString
        Dim strTotalLine As String = Request.QueryString("TotalLine").ToString

        PostURL = "?SessionID=" & Session("PSession").ToString & "&Vendor=" & strVendor & "&VendorInvoice=" & strVendorInvoice & ""
        PostURL = PostURL & "&Whse=" & strWhse & "&GRNType=" & strGRNType & "&GRN=" & strGRN & "&TotalLine=" & strTotalLine & ""

        Response.Redirect("GenerateGRN.aspx" & PostURL)

    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim Qty As Integer = CDec(e.Row.Cells(5).Text)

            e.Row.Cells(5).Text = FormatNumber(Qty, LenPointQty)

        End If

    End Sub

    Protected Sub btndeletetag_Click(sender As Object, e As EventArgs) Handles btndeletetag.Click

        Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "C" & "</Parameter>" &
                            "<Parameter>" & "T" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)


        BindGridview()

        If GridView1.Rows.Count = 0 Then

            btnback_Click(sender, e)

        End If


    End Sub

    Protected Sub SelectCheckBox_CheckedChanged()

        For Each Row As GridViewRow In GridView1.Rows

            If Row.RowType = DataControlRowType.DataRow Then

                Dim chkSelect As CheckBox = TryCast(Row.Cells(0).FindControl("chkSelect"), CheckBox)
                Dim TagID As String = Row.Cells(2).Text

                If chkSelect.Checked Then

                    Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & TagID & "</Parameter>" &
                            "<Parameter>" & "U" & "</Parameter>" &
                            "<Parameter>" & "T" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

                    oWS = New SRNService.DOWebServiceSoapClient
                    oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)


                End If

            End If

        Next

        BindGridview()

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

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "' and UserID = '" & Session("UserName").ToString & "'"
        Propertie = "DerPo, Item, TagID, Lot, VendLot, Qty, Selected"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Details", Propertie, Filter, "CreateDate Desc", "", 0)

        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()


    End Sub
#End Region

End Class