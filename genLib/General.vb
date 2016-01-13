
Public Class General
    Public Shared ProjectID As String
    Public Shared Title As String = "System Administrator"
    Public Shared Connect As Boolean = False
    Public Shared Online As Boolean = False
    'Public formatDate As String = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern()
    Public Shared formatDate As String = "yyyy-MM-dd"
    Public Shared logOn As String = ""
    Public Shared UserName As String = ""
    Public Shared UserGroup As String = ""
    Public Shared Default_WH As String = ""
    Public Shared Default_Branch As String = ""
    Public Shared Default_PPN As Decimal = 0
    Public Shared query As String = ""
    Public Shared DB As String = ""
    Public Shared tblParam As DataTable
    Public Shared tblMenuAccess As DataTable
    Public Shared table As DataTable
    Public Shared srcPath As String
    Public Shared posPrinter As String

    Public Enum MyCrypt
        EnCrypt
        Decrypt
    End Enum

    Public Enum Status
        No
        Yes
    End Enum


End Class
