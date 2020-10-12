Public Class MissOper
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    'Dim da As New OleDb.OleDbDataAdapter(strSql, cn)
    'Dim dt As DataTable = New DataTable
    'da.Fill(dt)

    'Dim sData As String = GetJson(dt)
    'Public Shared Function GetJson(ByVal dt As DataTable) As String
    '    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
    '    serializer.MaxJsonLength = Integer.MaxValue

    '    Dim rows As New List(Of Dictionary(Of String, Object))()
    '    Dim row As Dictionary(Of String, Object) = Nothing
    '    For Each dr As DataRow In dt.Rows
    '        row = New Dictionary(Of String, Object)()
    '        For Each dc As DataColumn In dt.Columns

    '            row.Add(dc.ColumnName.Trim(), dr(dc))

    '        Next
    '        rows.Add(row)
    '    Next
    '    Return serializer.Serialize(rows)
    'End Function
End Class