Imports System.IO
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Xml

Public Class CallMethod
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest


        Dim oWS As SRNService.DOWebServiceSoapClient
        Dim ds As DataSet
        Dim ser As JavaScriptSerializer = New JavaScriptSerializer()
        Dim strJson, __json, Token, Parms, Stat, MsgErr, MsgType, Ido, Method, inputParms, outputParms As String

        strJson = New StreamReader(context.Request.InputStream).ReadToEnd()
        Dim contents As Array = strJson.Split("&")
        Dim Fields As Array

        For index As Integer = 1 To contents.Length
            Fields = contents(index - 1).Split("=")

            Select Case Fields(0)
                Case "Token"
                    Token = URLDecode(Fields(1))
                Case "Ido"
                    Ido = URLDecode(Fields(1))
                Case "Method"
                    Method = URLDecode(Fields(1))
                Case "Parms"
                    inputParms = URLDecode(Fields(1))
            End Select
        Next index

        'inputParms = "5f7b6a3f-f995-43bf-9536-b99b0596eac8"
        'inputParms += ",J"
        'inputParms += ",S23BIC0010-0000-10"
        'inputParms += ",29/05/2020"
        'inputParms += ",MAIN"
        'inputParms += ","
        'inputParms += ",0"
        'inputParms += ",0"
        'inputParms += ",0"
        'inputParms += ",I"
        'inputParms += ",I"
        'inputParms += ",SRN"
        'inputParms += ",ip"
        'context.Response.Write(inputParms)

        Dim inputParmsArray As Array = inputParms.Split(",")
        'Dim outputParmsArray As Array = outputParms.Split(",")



        Parms = ""

        If inputParmsArray.Length > 0 Then

            Parms = "<Parameters>"
            For index As Integer = 1 To inputParmsArray.Length

                Parms += "<Parameter>" & inputParmsArray(index - 1) & "</Parameter>"

            Next index
            Parms += "<Parameter ByRef ='Y'></Parameter>"
            Parms += "<Parameter ByRef ='Y'></Parameter>"
            Parms += "<Parameter ByRef ='Y'></Parameter>"
            Parms += "</Parameters>"



        End If
        'context.Response.Write(Parms)




        'Token = "b/XdI6IQzCviZOGJ0E+002DoKUFOPmVDkwpQDbQjm3w/qkdxDUzmqvSYEZDCmJGWpA23OTlhFpxRHFz3WOsvay8V58XdIp/UIsr5TpCdMwtW3QXF2ahwQYp2O6GzKlJcY/gm+5fkf1BAwwrw/Ox0HpL6rt0vbAgdaP6GkWo4lf07dem45jk14lnEG/Pg0cNQtNxLI+8DFxYwrYl/mTT4cee6rBTQ4rG3fu0jWYFSNV448ta7dNUaCu2wqBwsmfDlWIpPP/SU7DTfYPLOW3/0VpQJNPEJgBHz9U7YCGUdCUe5CIMu/Xcoqhvml7t09mhZIL4F6QG/hcknhbmyGHqI3w=="
        'Ido = "PPCC_Ex_JobMatlDetail"
        'Method = "PPCC_Ex_JobMatlSp"
        'Parms = "<Parameters>" &
        '            "<Parameter>5f7b6a3f-f995-43bf-9536-b99b0596eac8</Parameter>" &
        '            "<Parameter>J</Parameter><Parameter>S23BIC0010-0000-10</Parameter>" &
        '            "<Parameter>29/05/2020</Parameter>" &
        '            "<Parameter>MAIN</Parameter>" &
        '            "<Parameter></Parameter>" &
        '            "<Parameter>0</Parameter>" &
        '            "<Parameter>0</Parameter>" &
        '            "<Parameter>0</Parameter>" &
        '            "<Parameter>I</Parameter>" &
        '            "<Parameter>I</Parameter>" &
        '            "<Parameter>SRN</Parameter>" &
        '            "<Parameter>ip</Parameter>" &
        '            "<Parameter ByRef ='Y'></Parameter>" &
        '            "<Parameter ByRef='Y'></Parameter>" &
        '            "<Parameter ByRef='Y'></Parameter>" &
        '            "</Parameters>"

        'context.Response.Write(Parms)

        'context.Response.Write(inputParmsArray.Length + 1)


        oWS = New SRNService.DOWebServiceSoapClient
        oWS.CallMethod(Token, Ido, Method, Parms)

        Dim doc As XmlDocument = New XmlDocument()
        doc.LoadXml(Parms)

        Dim i As Integer = 1

        Stat = "FALSE"
        MsgType = ""
        MsgErr = ""

        For Each node As XmlNode In doc.DocumentElement

            If i = (inputParmsArray.Length + 1) Then
                Stat = node.InnerText

            ElseIf i = (inputParmsArray.Length + 2) Then
                MsgType = node.InnerText

            ElseIf i = (inputParmsArray.Length + 3) Then
                MsgErr = node.InnerText

            End If

            i += 1

        Next

        __json = "{""Stat"":""" & Stat & """,""MsgType"":""" & MsgType & """,""MsgErr"":""" & MsgErr & """}"

        ' __json = DataTableToJSONWithJavaScriptSerializer(ds)
        context.Response.ContentType = "text/json"
        context.Response.Write(__json)


    End Sub

    Public Function URLDecode(StringToDecode As String) As String

        Dim TempAns As String
        Dim CurChr As Integer

        CurChr = 1

        Do Until CurChr - 1 = Len(StringToDecode)
            Select Case Mid(StringToDecode, CurChr, 1)
                Case "+"
                    TempAns = TempAns & " "
                Case "%"
                    TempAns = TempAns & Chr(Val("&h" &
         Mid(StringToDecode, CurChr + 1, 2)))
                    CurChr = CurChr + 2
                Case Else
                    TempAns = TempAns & Mid(StringToDecode, CurChr, 1)
            End Select

            CurChr = CurChr + 1
        Loop

        URLDecode = TempAns
    End Function


    Public Function DataTableToJSONWithJavaScriptSerializer(ByVal dataset As DataSet) As String
        Dim jsSerializer As JavaScriptSerializer = New JavaScriptSerializer()
        Dim ssvalue As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        For Each table As DataTable In dataset.Tables
            Dim parentRow As List(Of Dictionary(Of String, Object)) = New List(Of Dictionary(Of String, Object))()
            Dim childRow As Dictionary(Of String, Object)
            Dim tablename As String = table.TableName

            For Each row As DataRow In table.Rows
                childRow = New Dictionary(Of String, Object)()

                For Each col As DataColumn In table.Columns
                    childRow.Add(col.ColumnName, row(col))
                Next

                parentRow.Add(childRow)
            Next

            ssvalue.Add(tablename, parentRow)
        Next

        Return jsSerializer.Serialize(ssvalue)
    End Function

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class