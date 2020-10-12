Imports System.Data
Imports System.Xml
Imports System.Collections
Imports System.Reflection
Imports Microsoft.VisualBasic.DateInterval

Public Class OrderShipping
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

        Dim sLock As String = "0"
        sLock = GetAccessOrderShipping()

        If sLock = "1" Then
            Response.Redirect("Menu.aspx")
        End If

        LenPointQty = UnitQtyFormat()

        If Not Page.IsPostBack Then

            PSite = GetSite()
            PWhse = GetDefWhse()

            DateNow = Date.Now.ToString("dd/MM/yyyy")

            txtdate.Text = DateNow

            GetReasonCode()

            If Request.QueryString("SessionID") <> "" Or Not String.IsNullOrEmpty(Request.QueryString("SessionID")) Then

                BindGridview()

                Dim CheckPreInvoice As String = ""
                CheckPreInvoice = GetCheckPreInvoice(txtcustnum.Text)

                If CheckPreInvoice = "1" Then
                    ChkPreInv.Checked = True
                End If

                If Session("Stat").ToString = "Return" Then
                    btnstat.CssClass = "btn btn btn-outline-danger btn-block"
                    btnstat.Text = "<i class=""fa fa-repeat"" aria-hidden=""true""></i>" & " <strong>Return</strong>"
                Else
                    btnstat.CssClass = "btn btn-outline-success btn-block"
                    btnstat.Text = "<i class=""fa fa-arrow-right"" aria-hidden=""true""></i>" & " <strong>Order Shipping</strong>"
                End If

                ddlreturncode.SelectedIndex = ddlreturncode.Items.IndexOf(ddlreturncode.Items.FindByValue(Request.QueryString("CoReturn")))

            Else
                Session("Stat") = "Ship"
            End If

        End If

        If txtpicklistno.Text = "" Then
            lblbarcode.Text = "Scan Order PickList: "
        Else
            lblbarcode.Text = Session("Scan").ToString
        End If

        'If GridView1.Rows.Count > 0 Then

        '    If ChkPreInv.Checked = True Then

        '        Dim sCheck As String = "-1"

        '        For Each row As GridViewRow In GridView1.Rows

        '            Dim chkLabelItem As CheckBox = TryCast(row.Cells(6).FindControl("chkLabelItem"), CheckBox)

        '            If Session("Stat") = "Ship" Then

        '                If chkLabelItem.Checked = True Then
        '                    sCheck = "0"
        '                Else
        '                    sCheck = "-1"
        '                    Exit For
        '                End If

        '            Else

        '                If chkLabelItem.Checked = True Then
        '                    sCheck = "0"
        '                Else
        '                    sCheck = "-1"
        '                    Exit For
        '                End If


        '            End If

        '        Next

        '        If sCheck = "0" Then
        '            btnprocess.Attributes.Remove("disabled")
        '        End If

        '    Else
        '        btnprocess.Attributes.Remove("disabled")

        '    End If

        'End If


        If Session("Stat").ToString = "Return" Then
            ddlreturncode.Attributes.Remove("disabled")
        End If

        txtbarcode.Focus()

        txtbarcode.Attributes.Add("onchange", "javascript:scanbarcode();")

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim sBarcode, Stat, MsgErr, MsgType, Prompt, CheckCustDoc, CheckScanPallet, CheckPreInvoice, CheckCustItem, CheckCustPO, CheckQty As String
        Dim StartCustItem, LenghtCustItem, StartCustPO, LenghtCustPO, StartQty, LenghtQty As Integer
        sBarcode = txtbarcode.Text
        Stat = "TRUE"
        MsgErr = ""
        MsgType = ""
        Prompt = ""
        CheckCustDoc = "0"
        CheckScanPallet = "0"
        CheckPreInvoice = "0"
        CheckCustItem = "0"
        CheckCustPO = "0"
        CheckQty = "0"
        StartCustItem = 0
        LenghtCustItem = 0
        StartCustPO = 0
        LenghtCustPO = 0
        StartQty = 0
        LenghtQty = 0

        If txtbarcode.Text <> "" Then

            If lblbarcode.Text = "Scan Order PickList: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "O" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse.ToString & "</Parameter>" &
                        "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                        "<Parameter>" & ddlreturncode.SelectedItem.Value & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Ship", "I", "W") & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

                'BindGridview()

                'If GridView1.Rows.Count > 0 Then
                '    lblbarcode.Text = "Scan Tag: "
                'End If



            ElseIf lblbarcode.Text = "Scan Tag: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "T" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse.ToString & "</Parameter>" &
                        "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Ship", "I", "W") & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

            ElseIf lblbarcode.Text = "Scan Pallet: " Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                        "<Parameter>" & "P" & "</Parameter>" &
                        "<Parameter>" & sBarcode & "</Parameter>" &
                        "<Parameter>" & txtdate.Text & "</Parameter>" &
                        "<Parameter>" & PWhse.ToString & "</Parameter>" &
                        "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                        "<Parameter>" & DBNull.Value & "</Parameter>" &
                        "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                        "<Parameter>" & IIf(Session("Stat").ToString = "Ship", "I", "W") & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

                'lblbarcode.Text = "Scan Cust Doc.: "

            ElseIf lblbarcode.Text = "Scan Cust Doc.: " Then

                'CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)

                'If CheckCustDoc = "0" Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                       "<Parameter>" & "C" & "</Parameter>" &
                       "<Parameter>" & sBarcode & "</Parameter>" &
                       "<Parameter>" & txtdate.Text & "</Parameter>" &
                       "<Parameter>" & PWhse.ToString & "</Parameter>" &
                       "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                       "<Parameter>" & DBNull.Value & "</Parameter>" &
                       "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                       "<Parameter>" & "I" & "</Parameter>" &
                       "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                       "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "</Parameters>"

                'lblbarcode.Text = "Scan Tag: "

                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error [PPCC]','Check Cust. Doc does not exits', 'warning');", True)
                'Else

                '    lblbarcode.Text = "Scan Tag: "

                'End If
            ElseIf lblbarcode.Text = "Scan Label: " Then

                'CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)

                'If CheckCustDoc = "0" Then

                Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                       "<Parameter>" & "L" & "</Parameter>" &
                       "<Parameter>" & sBarcode & "</Parameter>" &
                       "<Parameter>" & txtdate.Text & "</Parameter>" &
                       "<Parameter>" & PWhse.ToString & "</Parameter>" &
                       "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                       "<Parameter>" & DBNull.Value & "</Parameter>" &
                       "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                       "<Parameter>" & "I" & "</Parameter>" &
                       "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                       "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "</Parameters>"


            End If

            oWS = New SRNService.DOWebServiceSoapClient
            oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_OrderShipSums", "PPCC_Ex_OrderShipSp", Parms)

            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml(Parms)

            Dim i As Integer = 1

            For Each node As XmlNode In doc.DocumentElement

                If i = 12 Then
                    Stat = node.InnerText

                ElseIf i = 13 Then
                    MsgType = node.InnerText

                ElseIf i = 14 Then
                    MsgErr = node.InnerText

                End If

                i += 1

            Next

            If Stat = "TRUE" Then

                If lblbarcode.Text = "Scan Order PickList: " Then

                    BindGridview()

                    CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)
                    CheckPreInvoice = GetCheckPreInvoice(txtcustnum.Text)

                    If CheckPreInvoice = "1" Then
                        ChkPreInv.Checked = True
                    End If

                    If CheckCustDoc = "0" Or CheckCustDoc = "" Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error [PPCC]','Check Cust. Doc does not exits', 'warning');", True)

                    End If

                    If PanelList.Items.Count > 0 Then
                        lblbarcode.Text = "Scan Tag: "
                        Session("Scan") = "Scan Tag: "
                        'Exit Sub
                    End If

                ElseIf lblbarcode.Text = "Scan Tag: " Then

                    CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)
                    CheckScanPallet = GetCheckScanPallet(txtcustnum.Text)

                    BindGridview()

                    HiddenField2.Value = ""
                    HiddenField2.Value = GetScanLabelItem(sBarcode) 'GetCoProductMix(sBarcode)

                    If HiddenField2.Value = "1" Then

                        lblbarcode.Text = "Scan Label: "
                        Session("Scan") = "Scan Label: "
                    Else

                        If CheckCustDoc = "0" Or CheckCustDoc = "" Then

                            If CheckScanPallet = "1" Then
                                lblbarcode.Text = "Scan Pallet: "
                                Session("Scan") = "Scan Pallet: "
                            Else
                                lblbarcode.Text = "Scan Tag: "
                                Session("Scan") = "Scan Tag: "
                            End If

                        Else

                            lblbarcode.Text = "Scan Cust Doc.: "
                            Session("Scan") = "Scan Cust Doc.: "

                        End If

                    End If

                    'If CheckCustDoc = "0" Or CheckCustDoc = "" Then

                    'If CheckScanPallet = "1" Then
                    '    lblbarcode.Text = "Scan Pallet: "
                    '    Session("Scan") = "Scan Pallet: "
                    'Else
                    '    lblbarcode.Text = "Scan Tag: "
                    '    Session("Scan") = "Scan Tag: "
                    'End If

                    'End If

                ElseIf lblbarcode.Text = "Scan Pallet: " Then

                    lblValidate.Visible = False
                    lblValidateCustItem.Visible = False
                    txtValidateCustItem.Visible = False
                    lblValidateCustPo.Visible = False
                    txtValidateCustPo.Visible = False
                    lblValidateQty.Visible = False
                    txtValidateQty.Visible = False

                    txtValidateCustItem.Text = ""
                    txtValidateCustPo.Text = ""
                    txtValidateQty.Text = ""

                    lblbarcode.Text = "Scan Tag: "
                    Session("Scan") = "Scan Tag: "

                ElseIf lblbarcode.Text = "Scan Cust Doc.: " Then

                    ds = New DataSet
                    ds = GetCheckInformation(txtcustnum.Text)

                    If ds.Tables(0).Rows.Count > 0 Then
                        CheckCustItem = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustItem").ToString
                        CheckCustPO = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustPo").ToString
                        CheckQty = ds.Tables(0).Rows(0)("cusUf_Customer_FixQty").ToString
                        StartCustItem = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustItemStart").ToString
                        LenghtCustItem = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustItemLenght").ToString
                        StartCustPO = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustPoStart").ToString
                        LenghtCustPO = ds.Tables(0).Rows(0)("cusUf_Customer_FixCustPoLenght").ToString
                        StartQty = ds.Tables(0).Rows(0)("cusUf_Customer_FixQtyStart").ToString
                        LenghtQty = ds.Tables(0).Rows(0)("cusUf_Customer_FixQtyLenght").ToString
                    End If


                    If sBarcode.Length > 0 Then

                        sBarcode = Replace(Replace(Replace(Replace(Replace(Replace(Replace(sBarcode, "  ", ""), "-", ""), " ", ""), "/", ""), ",", ""), "|", ""), "&", "")

                        If CheckCustItem = "1" Then
                            txtValidateCustItem.Text = Mid(sBarcode, StartCustItem, LenghtCustItem)
                        End If

                        If CheckCustPO = "1" Then
                            txtValidateCustPo.Text = Mid(sBarcode, StartCustPO, LenghtCustPO)
                        End If

                        If CheckQty = "1" Then
                            txtValidateQty.Text = CInt(Mid(sBarcode, StartQty, LenghtQty))
                        End If

                    End If

                    'ds = New DataSet
                    CheckScanPallet = GetCheckScanPallet(txtcustnum.Text)


                    If CheckScanPallet = "1" Then
                        lblbarcode.Text = "Scan Pallet: "
                        Session("Scan") = "Scan Pallet: "
                    Else
                        lblbarcode.Text = "Scan Tag: "
                        Session("Scan") = "Scan Tag: "
                    End If

                    'If CheckCustDoc = "1" Then

                    'lblbarcode.Text = "Scan Tag: "


                ElseIf lblbarcode.Text = "Scan Label: " Then

                    lblValidate.Visible = False
                    lblValidateCustItem.Visible = False
                    txtValidateCustItem.Visible = False
                    lblValidateCustPo.Visible = False
                    txtValidateCustPo.Visible = False
                    lblValidateQty.Visible = False
                    txtValidateQty.Visible = False

                    BindGridview()

                    CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)
                    CheckScanPallet = GetCheckScanPallet(txtcustnum.Text)

                    If CheckCustDoc = "0" Or CheckCustDoc = "" Then

                        If CheckScanPallet = "1" Then
                            lblbarcode.Text = "Scan Pallet: "
                            Session("Scan") = "Scan Pallet: "
                        Else
                            lblbarcode.Text = "Scan Tag: "
                            Session("Scan") = "Scan Tag: "
                        End If

                    Else

                        lblbarcode.Text = "Scan Cust Doc.: "
                        Session("Scan") = "Scan Cust Doc.: "

                    End If



                    'lblbarcode.Text = "Scan Tag: "
                    'Session("Scan") = "Scan Tag: "

                    'If HiddenField2.Value = "1" Then

                    '    Dim sCheck As String = "0"

                    '    If GridView1.Rows.Count > 0 Then

                    '        For j As Integer = 0 To GridView1.Rows.Count - 1

                    '            If GridView1.Rows(j).Cells(6).Text = "1" Then
                    '                sCheck = "-1"
                    '                Exit For
                    '            End If

                    '        Next

                    '    End If

                    '    If sCheck = "0" Then
                    '        btnprocess.Attributes.Remove("disabled")
                    '    End If

                    'Else
                    '    btnprocess.Attributes.Remove("disabled")
                    'End If


                    ''btnprocess.Attributes.Remove("disabled")

                End If


                'If lblbarcode.Text = "Scan Tag: " Then

                '    If sBarcode <> "OK" Then

                '        HiddenField2.Value = ""
                '        HiddenField2.Value = GetCoProductMix(sBarcode)

                '        If CheckCustDoc = "0" Then

                '            If CheckScanPallet = "1" Then
                '                lblbarcode.Text = "Scan Pallet: "
                '                Exit Sub
                '            End If

                '        Else

                '            Dim sSumTag As String = ""
                '            sSumTag = GetSumTag()

                '            If sSumTag = "1" Then
                '                lblValidate.Visible = True
                '                lblValidateCustItem.Visible = True
                '                txtValidateCustItem.Visible = True
                '                lblValidateCustPo.Visible = True
                '                txtValidateCustPo.Visible = True
                '                lblValidateQty.Visible = True
                '                txtValidateQty.Visible = True
                '            End If

                '            lblbarcode.Text = "Scan Cust Doc.: "

                '        End If

                '            'If CheckScanPallet = "0" Then

                '            '    If CheckCustDoc = "1" Then
                '            '        lblbarcode.Text = "Scan Cust Doc.: "
                '            '    End If

                '            'Else
                '            '    lblbarcode.Text = "Scan Pallet: "
                '            'End If

                '        End If

                '    If sBarcode = "OK" Then

                '        If HiddenField2.Value = "0" Then
                '            btnprocess.Attributes.Remove("disabled")
                '        Else
                '            lblbarcode.Text = "Scan Label: "
                '            Exit Sub
                '        End If

                '    End If

                'End If

                'If lblbarcode.Text = "Scan Pallet: " Then

                '    lblbarcode.Text = "Scan Tag: "

                '    'If CheckCustDoc = "1" Then
                '    '    lblbarcode.Text = "Scan Cust Doc.: "
                '    'Else
                '    '    lblbarcode.Text = "Scan Tag: "
                '    'End If


                'End If

                'If lblbarcode.Text = "Scan Cust Doc.: " Then

                '    Dim arrBarcode As String()
                '    arrBarcode = sBarcode.Split(New Char() {","c})

                '    If arrBarcode.Length > 0 Then
                '        txtValidateCustItem.Text = arrBarcode(0)
                '        txtValidateCustPo.Text = arrBarcode(1)
                '        txtValidateQty.Text = arrBarcode(2)
                '    End If

                '    If CheckScanPallet = "1" Then
                '        lblbarcode.Text = "Scan Pallet: "
                '    Else
                '        lblbarcode.Text = "Scan Tag: "
                '    End If

                '    'If CheckCustDoc = "1" Then

                '    lblbarcode.Text = "Scan Tag: "

                '    'End If

                'End If

                'If lblbarcode.Text = "Scan Label: " Then

                '    btnprocess.Attributes.Remove("disabled")

                'End If


            End If

            If Stat = "FALSE" Then

                If lblbarcode.Text <> "Scan Cust Doc.: " Then
                    lblValidate.Visible = False
                    lblValidateCustItem.Visible = False
                    txtValidateCustItem.Visible = False
                    lblValidateCustPo.Visible = False
                    txtValidateCustPo.Visible = False
                    lblValidateQty.Visible = False
                    txtValidateQty.Visible = False

                    'txtValidateCustItem.Text = ""
                    'txtValidateCustPo.Text = ""
                    'txtValidateQty.Text = ""
                End If



                'CheckCustDoc = GetCheckCustDoc(txtcustnum.Text)

                'If CheckCustDoc = "0" Or CheckCustDoc = "" Then

                '    lblbarcode.Text = "Scan Tag: "
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Error [PPCC]','Check Cust. Doc does not exits', 'warning');", True)

                'End If

                MsgErr = MsgErr.Replace("'", "\'")
                MsgType = "Error [" & MsgType & "]"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)

                'If Left(MsgErr, 4) = "#240" Then
                '    Response.Redirect("Menu.aspx")
                'End If

            End If

        End If

        txtbarcode.Text = String.Empty


    End Sub

    Protected Sub btnprocess_Click(sender As Object, e As EventArgs) Handles btnprocess.Click

        'Clear()

        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('Success','', 'success');", True)


        Dim Stat, MsgErr, MsgType As String
        Stat = "FALSE"
        MsgErr = ""
        MsgType = ""

        Parms = "<Parameters><Parameter>" & Session("PSession").ToString & "</Parameter>" &
                       "<Parameter>" & "" & "</Parameter>" &
                       "<Parameter>" & DBNull.Value & "</Parameter>" &
                       "<Parameter>" & txtdate.Text & "</Parameter>" &
                       "<Parameter>" & PWhse.ToString & "</Parameter>" &
                       "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                       "<Parameter>" & ddlreturncode.SelectedItem.Value & "</Parameter>" &
                       "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                       "<Parameter>" & IIf(Session("Stat").ToString = "Ship", "P", "W") & "</Parameter>" &
                       "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                       "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "</Parameters>"
        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_OrderShipSums", "PPCC_Ex_OrderShipSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 12 Then
                Stat = node.InnerText

            ElseIf i = 13 Then
                MsgType = node.InnerText

            ElseIf i = 14 Then
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

        btnstat.CssClass = "btn btn-outline-success btn-block"
        btnstat.Text = "<i class=""fa fa-arrow-right"" aria-hidden=""true""></i>" & " <strong>Order Shipping</strong>"
        Session("Stat") = "Ship"
        lblbarcode.Text = "Scan Order PickList: "

        chkCancelTag.Checked = False
        txtbarcode.Text = String.Empty
        txtpicklistno.Text = String.Empty
        txtshippingno.Text = String.Empty
        txtcustnum.Text = String.Empty
        txtdescription.Text = String.Empty

        'ddlreturncode.SelectedIndex = ddlreturncode.Items.IndexOf(ddlreturncode.Items.FindByValue(""))
        ddlreturncode.SelectedIndex = 0

        PanelList.DataSource = Nothing
        PanelList.DataBind()

        SGUID = System.Guid.NewGuid.ToString()
        Session("PSession") = SGUID

        HiddenField2.Value = ""

        lblValidate.Visible = False
        lblValidateCustItem.Visible = False
        txtValidateCustItem.Visible = False
        lblValidateCustPo.Visible = False
        txtValidateCustPo.Visible = False
        lblValidateQty.Visible = False
        txtValidateQty.Visible = False

        txtValidateCustItem.Text = String.Empty
        txtValidateCustPo.Text = String.Empty
        txtValidateQty.Text = String.Empty

        Session("Scan") = "Scan Order PickList: "

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
                       "<Parameter>" & PWhse.ToString & "</Parameter>" &
                       "<Parameter>" & txtcustnum.Text & "</Parameter>" &
                       "<Parameter>" & DBNull.Value & "</Parameter>" &
                       "<Parameter>" & IIf(chkCancelTag.Checked = True, 1, 0) & "</Parameter>" &
                       "<Parameter>" & "R" & "</Parameter>" &
                       "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                       "<Parameter>" & Session("UserName").ToString & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                       "</Parameters>"
        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_OrderShipSums", "PPCC_Ex_OrderShipSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement

            If i = 12 Then
                Stat = node.InnerText

            ElseIf i = 13 Then
                MsgType = node.InnerText

            ElseIf i = 14 Then
                MsgErr = node.InnerText

            End If


            i += 1

        Next

        If Stat = "TRUE" Then

            Response.Redirect("OrderShipping.aspx")

        Else

            MsgErr = MsgErr.Replace("'", "\'")
            MsgType = "Error [" & MsgType & "]"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowSweetAlert('" & MsgType & "','" & MsgErr & "', 'error');", True)


            'NotPassNotifyPanel.Visible = True
            'NotPassText.Text = MsgErr

        End If

    End Sub

    Protected Sub btnstat_Click(sender As Object, e As EventArgs) Handles btnstat.Click

        If PanelList.Items.Count = 0 Then
            PanelList.DataSource = Nothing
            PanelList.DataBind()
        End If

        If Session("Stat").ToString = "Ship" Then
            btnstat.CssClass = "btn btn btn-outline-danger btn-block"
            btnstat.Text = "<i class=""fa fa-repeat"" aria-hidden=""true""></i>" & " <strong>Return</strong>"
            Session("Stat") = "Return"
        Else
            btnstat.CssClass = "btn btn-outline-success btn-block"
            btnstat.Text = "<i class=""fa fa-arrow-right"" aria-hidden=""true""></i>" & " <strong>Order Shipping</strong>"
            Session("Stat") = "Ship"
        End If


        If Session("Stat").ToString = "Ship" Then
            'ddlreturncode.SelectedIndex = ddlreturncode.Items.IndexOf(ddlreturncode.Items.FindByValue(""))
            ddlreturncode.SelectedIndex = 0
            ddlreturncode.Attributes.Add("disabled", "disabled")
        Else
            ddlreturncode.Attributes.Remove("disabled")
        End If

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        e.Row.Cells(3).Text = FormatNumber(e.Row.Cells(3).Text, LenPointQty)
    '        e.Row.Cells(4).Text = FormatNumber(e.Row.Cells(4).Text, LenPointQty)
    '        e.Row.Cells(5).Text = FormatNumber(e.Row.Cells(5).Text, LenPointQty)


    '    End If

    'End Sub

    Protected Sub PanelList_RowDataBound(sender As Object, e As ListViewItemEventArgs) Handles PanelList.ItemDataBound

        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim lblQtyOrdered As Label = CType(e.Item.FindControl("lblQtyOrdered"), Label)
            Dim lblSumQty As Label = CType(e.Item.FindControl("lblSumQty"), Label)
            Dim lblQtyRemain As Label = CType(e.Item.FindControl("lblQtyRemain"), Label)


            lblQtyOrdered.Text = FormatNumber(lblQtyOrdered.Text, LenPointQty)
            lblSumQty.Text = FormatNumber(lblSumQty.Text, LenPointQty)
            lblQtyRemain.Text = FormatNumber(lblQtyRemain.Text, LenPointQty)

        End If

    End Sub

    Protected Sub ddlreturncode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlreturncode.SelectedIndexChanged
        txtbarcode.Focus()
    End Sub

    Sub Display(ByVal sender As Object, ByVal e As EventArgs)

        '    Dim rowIndex As Integer = Convert.ToInt32(TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow).RowIndex)
        '    Dim row As GridViewRow = GridView1.Rows(rowIndex)

        '    Dim PostURL As String = ""

        '    PostURL = "?SessionID=" & Session("PSession").ToString & "&CoNum=" & TryCast(row.FindControl("lblconum"), Label).Text & "&CoLine=" & TryCast(row.FindControl("lblcoline"), Label).Text & ""
        '    PostURL = PostURL & "&CoRelease=" & TryCast(row.FindControl("lblcorelease"), Label).Text & "&OrderPickList=" & txtpicklistno.Text & "&CoReturn=" & ddlreturncode.SelectedItem.Value & ""

        '    Response.Redirect("OrderShippingDetail.aspx" & PostURL)

    End Sub

    Protected Sub PanelList_ItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs) Handles PanelList.ItemCommand
        'Dim lblconum As Label = CType(e.Item.FindControl("lblconum"), Label)
        'Dim lblcoline As Label = CType(e.Item.FindControl("lblcoline"), Label)
        'Dim lblcorelease As Label = CType(e.Item.FindControl("lblcorelease"), Label)

        Dim PostURL As String = ""

        PostURL = "?SessionID=" & Session("PSession").ToString & "&CoNum=" & CType(e.Item.FindControl("lblconum"), Label).Text & "&CoLine=" & CType(e.Item.FindControl("lblcoline"), Label).Text & ""
        PostURL = PostURL & "&CoRelease=" & CType(e.Item.FindControl("lblcorelease"), Label).Text & "&OrderPickList=" & txtpicklistno.Text & "&CoReturn=" & ddlreturncode.SelectedItem.Value & ""

        Response.Redirect("OrderShippingDetail.aspx" & PostURL)
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

    Function GetCheckCustDoc(CustNum As String) As String

        GetCheckCustDoc = "0"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLCustomers", "cusUf_Customer_CheckCustDoc", "CustNum = '" & CustNum & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetCheckCustDoc = ds.Tables(0).Rows(0)("cusUf_Customer_CheckCustDoc").ToString
        End If

        Return GetCheckCustDoc

    End Function

    Function GetCheckScanPallet(CustNum As String) As String

        GetCheckScanPallet = ""

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLCustomers", "cusUf_Customer_ScanPallet", "CustNum = '" & CustNum & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetCheckScanPallet = ds.Tables(0).Rows(0)("cusUf_Customer_ScanPallet").ToString
        End If

        Return GetCheckScanPallet

    End Function



    Function GetCheckPreInvoice(CustNum As String) As String

        GetCheckPreInvoice = "0"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLCustomers", "cusUf_customer_InBeforeShip", "CustNum = '" & CustNum & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetCheckPreInvoice = ds.Tables(0).Rows(0)("cusUf_customer_InBeforeShip").ToString
        End If

        Return GetCheckPreInvoice

    End Function

    Function GetCheckInformation(CustNum As String) As DataSet

        'GetCheckInformation = "0"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLCustomers", "cusUf_Customer_FixCustItem, cusUf_Customer_FixCustPo, cusUf_Customer_FixQty, cusUf_Customer_FixCustItemStart, cusUf_Customer_FixCustItemLenght, cusUf_Customer_FixCustPoStart, cusUf_Customer_FixCustPoLenght, cusUf_Customer_FixQtyStart, cusUf_Customer_FixQtyLenght", "CustNum = '" & CustNum & "'", "", "", 0)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    GetCheckInformation = ds.Tables(0).Rows(0)("cusUf_customer_InBeforeShip").ToString
        'End If

        Return ds

    End Function

    Function GetSumTag() As String

        GetSumTag = "0"

        oWS = New SRNService.DOWebServiceSoapClient

        Dim Filter As String = ""

        ds = New DataSet

        Filter = "SessionID = '" & Session("PSession").ToString & "' And QtySum > 0 And QtySum = QtyOrder"
        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_OrderShipSums", "QtySum", Filter, "RecordDate DESC", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetSumTag = "1"
        End If

        Return GetSumTag

    End Function

    Function GetCustomer(PickListNum As String) As DataSet

        Dim CustNum As String = ""

        Filter = "OrderpicklistNum = '" & PickListNum & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_OrderPickList_Lines", "CustNum", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            CustNum = ds.Tables(0).Rows(0)("CustNum").ToString
        End If

        If CustNum <> "" Then

            oWS = New SRNService.DOWebServiceSoapClient

            ds = New DataSet

            ds = oWS.LoadDataSet(Session("Token").ToString, "SLCustomers", "CustNum, Name", Filter, "", "", 0)

        End If


        Return ds

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

    Function GetScanLabelItem(sBarcode As String) As String

        GetScanLabelItem = "0"
        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token"), "PPCC_Tags", "ScanLabelItem", "TagID = '" & sBarcode & "'", "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            GetScanLabelItem = ds.Tables(0).Rows(0)("ScanLabelItem").ToString
        End If

        Return GetScanLabelItem

    End Function

    Function GetCoProductMix(TagID As String) As String

        GetCoProductMix = "0"

        Dim sItem As String = ""

        Filter = "TagID = '" & TagID & "'"

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Tags", "Item", Filter, "", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then
            sItem = ds.Tables(0).Rows(0)("Item").ToString
        End If

        If sItem <> "" Then

            Filter = "Item = '" & sItem & "'"

            oWS = New SRNService.DOWebServiceSoapClient

            ds = New DataSet

            ds = oWS.LoadDataSet(Session("Token").ToString, "SLProdMixItems", "Item", Filter, "", "", 0)

            If ds.Tables(0).Rows.Count > 0 Then
                GetCoProductMix = "1"
            End If

        End If


        Return GetCoProductMix

    End Function

    Function GetAccessOrderShipping() As String

        GetAccessOrderShipping = "0"

        'Dim sUserUnLock As String = ""

        'Filter = "UserMissOper = '" & Session("UserName").ToString & "'"

        'oWS = New SRNService.DOWebServiceSoapClient

        'ds = New DataSet

        'ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_LogMiss", "UserMissOper", Filter, "RecordDate desc", "", 0)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    sUserUnLock = ds.Tables(0).Rows(0)("UserUnLock").ToString
        'End If

        'If sUserUnLock <> "" Then

        '    GetAccessOrderShipping = "Lock"

        'End If

        oWS = New SRNService.DOWebServiceSoapClient

        Parms = "<Parameters><Parameter>" & Session("UserName").ToString & "</Parameter>" &
                        "<Parameter>" & Session("PSite").ToString & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "<Parameter ByRef='Y'>" & DBNull.Value & "</Parameter>" &
                        "</Parameters>"

        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Session("Token").ToString, "PPCC_Ex_JobTranTags", "PPCC_EX_GetAccessFormSp", Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        For Each node As XmlNode In doc.DocumentElement


            If i = 11 Then
                GetAccessOrderShipping = node.InnerText
            End If

            i += 1

        Next

        Return GetAccessOrderShipping

    End Function



#Region "Get Data Bind To Dropdownlist"
    Sub GetReasonCode()

        oWS = New SRNService.DOWebServiceSoapClient

        ds = New DataSet

        ds = oWS.LoadDataSet(Session("Token").ToString, "SLReasons", "ReasonCode, Description", "ReasonClass = 'CO RETURN'", "ReasonCode", "", 0)

        For Each dRow As DataRow In ds.Tables(0).Rows
            ddlreturncode.Items.Add(New ListItem(dRow("ReasonCode") & IIf(IsDBNull(dRow("Description")), "", " : " & dRow("Description")), dRow("ReasonCode")))

        Next

        'ddlreturncode.Items.Insert(0, New ListItem("", ""))

    End Sub
#End Region

#Region "Bind Data To Gridview"

    Sub BindGridview()

        Dim Filter As String
        Dim Propertie As String

        oWS = New SRNService.DOWebServiceSoapClient
        ds = New DataSet
        Filter = "SessionID = '" & Session("PSession").ToString & "'"
        Propertie = "DerCO, CoNum, CoLine, CoRelease, Item, CustPO, QtyOrder, QtySum, QtyRemain, OrderPickListNum, CustNum, CustName, TransDate, CoReturn, CustItem, LabelItem"

        ds = oWS.LoadDataSet(Session("Token").ToString, "PPCC_Ex_OrderShipSums", Propertie, Filter, "CoNum, CoLine, CoRelease  Desc", "", 0)

        If ds.Tables(0).Rows.Count > 0 Then

            txtcustnum.Text = ds.Tables(0).Rows(0)("CustNum").ToString
            txtpicklistno.Text = ds.Tables(0).Rows(0)("OrderPickListNum").ToString
            txtdescription.Text = ds.Tables(0).Rows(0)("CustName").ToString
            'ddlreturncode.SelectedItem.Value = ddlreturncode.Items.IndexOf(ddlreturncode.Items.FindByValue(ds.Tables(0).Rows(0)("CoReturn").ToString))

            'GridView1.DataSource = ds.Tables(0)
            'GridView1.DataBind()

            PanelList.DataSource = ds.Tables(0)
            PanelList.DataBind()

        End If


    End Sub

#End Region


End Class