Imports System.Data.SqlClient

Public Class DBConnection

    Public Shared cn As New SqlConnection
    Public Shared cm As SqlCommand
    Public Shared ct As SqlTransaction
    Public Shared da As SqlDataAdapter

    Public Shared Sub OpenConnectionWindows(ByVal server As String, ByVal database As String)
        If cn.State = ConnectionState.Open Then cn.Close()
        With cn
            .ConnectionString = "Data Source='" & server & "';Initial Catalog='" & database & "';Integrated Security=SSPI;"
            .Open()
        End With
    End Sub

    Public Shared Sub OpenConnectionSQL(ByVal server As String _
            , ByVal database As String, ByVal user As String, ByVal password As String)
        If cn.State = ConnectionState.Open Then cn.Close()
        With cn
            .ConnectionString = "Data Source='" & server & "';Initial Catalog='" & database & "';" & _
                                "User Id='" & user & "';Password='" & password & "';Persist Security Info=true;"
            .Open()
        End With


    End Sub

    'Public Shared Sub OpenConnectionMYSQL(ByVal hostname As String _
    '        , ByVal database As String, ByVal port As Integer, ByVal user As String, ByVal password As String)
    '    If cn.State = ConnectionState.Open Then cn.Close()
    '    Try
    '        With cn
    '            .ConnectionString = "Server='" & hostname & "';Database='" & database & "';" & _
    '                                "Port='" & port & "';uid='" & user & "';pwd='" & password & "';Convert Zero Datetime=True;"
    '            .Open()
    '        End With
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        cn.Close()
    '        cn.Dispose()
    '    End Try

    'End Sub
End Class
