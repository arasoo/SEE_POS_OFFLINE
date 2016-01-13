Imports Microsoft.Reporting.WinForms
Imports genLib.General
Imports sqlLib.Sql

Class frmReportViewer

    Private mReport As String
    Private mReportPath As String
    Private mReportDatasetName As String
    Private mReportName As String
    Private mDataSource As DataSet
    Private myPageSettings As New System.Drawing.Printing.PageSettings()

    Public WriteOnly Property Datasource As DataSet
        Set(ByVal value As DataSet)
            mDataSource = value
        End Set
    End Property

    Public WriteOnly Property ReportDocument As String
        Set(ByVal value As String)
            mReport = value
        End Set
    End Property

    Public WriteOnly Property ReportDatasetName As String
        Set(ByVal value As String)
            mReportDatasetName = value
        End Set
    End Property

    Public WriteOnly Property ReportPath As String
        Set(ByVal value As String)
            mReportPath = value
        End Set
    End Property

    Public WriteOnly Property ReportName As String
        Set(ByVal value As String)
            mReportName = value
        End Set
    End Property

    Private Sub frmReportViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ReportViewer1.LocalReport.ReleaseSandboxAppDomain()
    End Sub

    Private Sub frmReportViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

        table = New DataTable

            table = GetDetailBranch()

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource(mReportDatasetName, mDataSource.Tables(0)))
            ReportViewer1.LocalReport.ReportPath = mReportPath

            ReportViewer1.DocumentMapCollapsed = True

        Dim myParamBranch, myParamAddress1, myParamAddress2 As ReportParameter

        myParamBranch = New ReportParameter
        myParamBranch.Name = "param_branch"
        myParamBranch.Values.Add(Trim(table.Rows(0).Item(1)))

        myParamAddress1 = New ReportParameter
        myParamAddress1.Name = "param_address1"
        myParamAddress1.Values.Add(Trim(table.Rows(0).Item(2)))

        myParamAddress2 = New ReportParameter
        myParamAddress2.Name = "param_address2"
        myParamAddress2.Values.Add(Trim(table.Rows(0).Item(3)))

        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {myParamBranch, myParamAddress1, myParamAddress2})
            ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)

            ' tells the viewer to refresh with the currently loaded report
            Me.ReportViewer1.RefreshReport()

            ReportViewer1.LocalReport.ReleaseSandboxAppDomain()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub DefaultPaperSize()

    End Sub

    Private Sub ReportViewer1_Load(sender As System.Object, e As System.EventArgs) Handles ReportViewer1.Load

    End Sub
End Class