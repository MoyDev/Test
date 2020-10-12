Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class GenerateGRN
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


        If Not Page.IsPostBack Then

            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow

            PSite = GetSite()
            PWhse = GetDefWhse()

            txtWhse.Text = PWhse.ToString

            GetListGRNType()

            If Request.QueryString("SessionID") <> "" Or Not String.IsNullOrEmpty(Request.QueryString("SessionID")) Then

                Dim strGRNType As String = ""
                strGRNType = Request.QueryString("GRNType")

                txtvendname.Text = Request.QueryString("Vendor")
                txtvendVendInv.Text = Request.QueryString("VendorInvoice")
                txtWhse.Text = Request.QueryString("Whse")
                ddlGrnType.SelectedIndex = ddlGrnType.Items.IndexOf(ddlGrnType.Items.FindByValue(strGRNType))
                txtGRN.Text = Request.QueryString("GRN")
                txtTotalLine.Text = Request.QueryString("TotalLine")

                BindGRN()

            Else

                Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "D" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & DBNull.Value & "</Parameter>" &
                            "<Parameter>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

                oWS = New SRNService.DOWebServiceSoapClient
                oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)

                lblbarcode.Text = "Scan Delivery Sheet: "

            End If



        End If

        '   Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('PPCC','" & lblbarcode.Text & "', 'error');", True)

        'If lblbarcode.Text <> "Scan Delivery Sheet: " Then
        'lblbarcode.Text = "Scan Tag: "
        'Else
        '    lblbarcode.Text = "Scan Tag: "
        'End If

        'If lblbarcode.Text = "Scan Delivery Sheet: " Then
        '    lblbarcode.Text = "Scan Delivery Sheet: "
        'Else
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('PPCC','" & lblbarcode.Text & "', 'error');", True)
        'End If

        LenPointQty = UnitQtyFormat()
        txtbarcode.Focus()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sBarcode, Stat, MsgErr, MsgType, InvNum, InvDate, PoNum, PoLine, PoRelease, QtyDelivery, TotalDsLine As String

        sBarcode = txtbarcode.Text
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""
        InvNum = ""
        InvDate = ""
        PoNum = ""
        PoLine = ""
        PoRelease = ""
        QtyDelivery = ""

        If txtbarcode.Text <> "" Then

            If lblbarcode.Text = "Scan Delivery Sheet: " Then


                Dim CountPipe As Integer = 0
                CountPipe = Len(sBarcode) - Len(Replace(sBarcode, "|", ""))

                If CountPipe = 7 Then

                    Dim arrBarcode As String()
                    arrBarcode = sBarcode.Split(New Char() {"|"c})

                    If arrBarcode.Length > 0 Then

                        InvNum = arrBarcode(0)
                        InvDate = arrBarcode(1)
                        PoNum = arrBarcode(2)
                        PoLine = arrBarcode(3)
                        PoRelease = arrBarcode(4)
                        QtyDelivery = arrBarcode(5)
                        TotalDsLine = arrBarcode(6)

                        lblBarcodeInvNum.Text = InvNum
                        lblBarcodeInvDate.Text = InvDate
                        lblBarcodePoNum.Text = PoNum
                        lblBarcodePoLine.Text = PoLine
                        lblBarcodePoRelease.Text = PoRelease
                        lblBarcodeQtyDelivery.Text = QtyDelivery
                        txtTotalLine.Text = TotalDsLine


                    End If



                    Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & "I" & "</Parameter>" &
                            "<Parameter>" & "D" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & lblBarcodeInvNum.Text & "</Parameter>" &
                            "<Parameter>" & lblBarcodePoNum.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

                    oWS = New SRNService.DOWebServiceSoapClient
                    oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)

                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(Parms)

                    Dim i As Integer = 1

                    For Each node As XmlNode In doc.DocumentElement

                        If i = 9 Then
                            Stat = node.InnerText

                        ElseIf i = 10 Then
                            MsgType = node.InnerText

                        ElseIf i = 11 Then
                            MsgErr = node.InnerText

                        End If

                        i += 1

                    Next

                    If Stat = "FALSE" Then

                        MsgErr = MsgErr.Replace("'", "\'")

                        MsgType = "Error [" & MsgType & "]"

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                        txtbarcode.Text = String.Empty

                    Else
                        txtvendname.Text = GetVendName(lblBarcodePoNum.Text)
                        txtvendVendInv.Text = lblBarcodeInvNum.Text

                        BindGRN()
                        'lblbarcode.Text = "Scan Tag: "

                    End If



                Else

                    MsgErr = "#610 : Delivery sheet does not match"

                    MsgType = "Error [ PPCC ]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    txtbarcode.Text = String.Empty
                End If



            ElseIf lblbarcode.Text = "Scan Tag: " Then

                Dim CountPipe As Integer = 0
                CountPipe = Len(sBarcode) - Len(Replace(sBarcode, "|", ""))

                If CountPipe = 9 Then

                    Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & sBarcode & "</Parameter>" &
                            "<Parameter>" & "I" & "</Parameter>" &
                            "<Parameter>" & "T" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & lblBarcodeInvNum.Text & "</Parameter>" &
                            "<Parameter>" & lblBarcodePoNum.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

                    oWS = New SRNService.DOWebServiceSoapClient
                    oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)

                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(Parms)

                    Dim i As Integer = 1

                    For Each node As XmlNode In doc.DocumentElement

                        If i = 9 Then
                            Stat = node.InnerText

                        ElseIf i = 10 Then
                            MsgType = node.InnerText

                        ElseIf i = 11 Then
                            MsgErr = node.InnerText

                        End If

                        i += 1

                    Next

                    If Stat = "FALSE" Then

                        MsgErr = MsgErr.Replace("'", "\'")

                        MsgType = "Error [" & MsgType & "]"

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                        txtbarcode.Text = String.Empty

                    Else

                        BindGRN()


                    End If
                Else

                    MsgErr = "#611 : Tag does not match"

                    MsgType = "Error [ PPCC ]"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                    txtbarcode.Text = String.Empty

                End If



            End If

            End If

        txtbarcode.Text = String.Empty
        txtbarcode.Focus()

    End Sub

    Protected Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click

        Dim Stat, MsgErr, MsgType As String
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                            "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & "R" & "</Parameter>" &
                            "<Parameter>" & "" & "</Parameter>" &
                            "<Parameter>" & PSite.ToString & "</Parameter>" &
                            "<Parameter>" & lblBarcodeInvNum.Text & "</Parameter>" &
                            "<Parameter>" & lblBarcodePoNum.Text & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                            "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_EX_InsertGRNTempSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 9 Then
                Stat = node.InnerText

            ElseIf i = 10 Then
                MsgType = node.InnerText

            ElseIf i = 11 Then
                MsgErr = node.InnerText

            End If

            i += 1

        Next

        If Stat = "TRUE" Then

            Response.Redirect("GenerateGRN.aspx")

        Else

            MsgErr = MsgErr.Replace("'", "\'")
            MsgType = "Error [" & MsgType & "]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

            'NotPassNotifyPanel.Visible = True
            'NotPassText.Text = MsgErr

        End If

    End Sub

    Protected Sub btnprocess_Click(sender As Object, e As EventArgs) Handles btnprocess.Click

        Dim sBarcode, Stat, MsgErr, MsgType, GrnNum As String
        sBarcode = txtbarcode.Text
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""
        GrnNum = ""

        Parms = "<Parameters><Parameter>" & ddlGrnType.SelectedItem.Value & "</Parameter>" &
                "<Parameter>" & txtdate.Text & "</Parameter>" &
                "<Parameter>" & txtvendVendInv.Text & "</Parameter>" &
                "<Parameter>" & PWhse.ToString & "</Parameter>" &
                "<Parameter>" & Session("PSession").ToString & "</Parameter>" &
                "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                "<Parameter>" & PSite.ToString & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                "</Parameters>"



        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", "PPCC_Ex_GenerateGrnSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 8 Then
                GrnNum = node.InnerText

            ElseIf i = 9 Then
                Stat = node.InnerText

            ElseIf i = 10 Then
                MsgType = node.InnerText

            ElseIf i = 11 Then
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

    Sub Clear()

        DateNow = Date.Now.ToString("dd/MM/yyyy")
        txtdate.Text = DateNow

        lblbarcode.Text = "Scan Delivery Sheet: "

        txtbarcode.Text = String.Empty
        ddlGrnType.SelectedIndex = ddlGrnType.Items.IndexOf(ddlGrnType.Items.FindByValue(Request.QueryString("")))
        txtGRN.Text = "AUTO GENERATE"
        txtvendname.Text = String.Empty
        txtvendVendInv.Text = String.Empty

        PanelList.DataSource = Nothing
        PanelList.DataBind()

        SGUID = System.Guid.NewGuid.ToString()
        Session("PSession") = SGUID

    End Sub



    Protected Sub ListView1_ItemDataBound(ByVal sender As Object, ByVal e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        Dim QtyOrder As Decimal = 0
        Dim QtyReceived As Decimal = 0
        Dim QtyRequire As Decimal = 0
        Dim QtySumQty As Decimal = 0

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblListQtyOrder As Label = CType(e.Item.FindControl("lblListQtyOrder"), Label)
            Dim lblListQtyReceived As Label = CType(e.Item.FindControl("lblListQtyReceived"), Label)
            Dim lblListRequire As Label = CType(e.Item.FindControl("lblListRequire"), Label)
            Dim lblListSumQty As Label = CType(e.Item.FindControl("lblListSumQty"), Label)

            QtyOrder = CDec(lblListQtyOrder.Text)
            QtyReceived = CDec(lblListQtyReceived.Text)
            QtyRequire = CDec(lblListRequire.Text)
            QtySumQty = CDec(lblListSumQty.Text)

            lblListQtyOrder.Text = FormatNumber(QtyOrder.ToString, LenPointQty)
            lblListQtyReceived.Text = FormatNumber(QtyReceived.ToString, LenPointQty)
            lblListRequire.Text = FormatNumber(QtyRequire.ToString, LenPointQty)
            lblListSumQty.Text = FormatNumber(QtySumQty.ToString, LenPointQty)

        End If

    End Sub

    Protected Sub ddlGrnType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGrnType.SelectedIndexChanged
        txtGRN.Text = ddlGrnType.SelectedItem.Value.ToString

        If txtbarcode.Text <> String.Empty Then
            txtbarcode.Text = String.Empty
        End If
    End Sub

    Sub BindGRN()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "' and UserID = '" & Session("UserName").ToString & "'"
        Propertie = "DerPo ,Item, QtyOrdered, QtyReceived, QtyRequire, SumQty, Loc"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_Tmp_Grn_Hdrs", Propertie, Filter, "DerPo ASC", "", 0)

        PanelList.DataSource = ds.Tables(0)
        PanelList.DataBind()

        If ds.Tables(0).Rows.Count.ToString = txtTotalLine.Text Then
            lblbarcode.Text = "Scan Tag: "
        Else
            lblbarcode.Text = "Scan Delivery Sheet: "
        End If


    End Sub

    Sub GetListGRNType()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "UserDefinedTypeValues", "Value, Description", "TypeName = 'Goods Receiving Note'", "", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlGrnType.Items.Add(New ListItem(dRow("Description"), dRow("Value")))
        Next

        ddlGrnType.Items.Insert(0, New ListItem("", ""))
    End Sub

    Protected Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btndetail.Click

        Dim PostURL As String = ""

        PostURL = "?SessionID=" & Session("PSession").ToString _
                                & "&Vendor=" & txtvendname.Text _
                                & "&VendorInvoice=" & txtvendVendInv.Text _
                                & "&Whse=" & txtWhse.Text _
                                & "&GRNType=" & ddlGrnType.SelectedItem.Value _
                                & "&GRN=" & txtGRN.Text _
                                & "&TotalLine=" & txtTotalLine.Text

        Response.Redirect("GRNDetail.aspx" & PostURL)

    End Sub

    Protected Sub btnvendorlot_Click(sender As Object, e As EventArgs) Handles btnvendorlot.Click

        Dim PostURL As String = ""

        PostURL = "?SessionID=" & Session("PSession").ToString _
                                & "&Vendor=" & txtvendname.Text _
                                & "&VendorInvoice=" & txtvendVendInv.Text _
                                & "&Whse=" & txtWhse.Text _
                                & "&GRNType=" & ddlGrnType.SelectedItem.Value _
                                & "&GRN=" & txtGRN.Text _
                                & "&TotalLine=" & txtTotalLine.Text

        Response.Redirect("GRNVendorLot.aspx" & PostURL)

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

    Function GetVendName(PoNum As String) As String

        GetVendName = ""
        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Dim Filter As String
        Filter = "PoNum = '" & PoNum & "'"


        ds = oWS.LoadDataSet(Session("Token"), "SLPos", "VendorName", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetVendName = ds.Tables(0).Rows(0)("VendorName").ToString
        End If

        Return GetVendName

    End Function




End Class