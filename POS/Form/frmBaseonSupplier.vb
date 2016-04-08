Imports mainlib
Imports genLib.General
Imports saveLib.Save
Imports sqlLib.Sql
Imports POS.StackedHeader

Public Class frmBaseonSupplier

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim objREnderer As New StackedHeaderDecorator(GridBaseOnSupplier)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub frmBaseonSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim SFD As New SaveFileDialog
        Dim strFileName As String = ""
        Dim mDataset As DataSet

        Try
            SFD.InitialDirectory = "C:\"
            SFD.Title = "Save Your File Spreadsheet"
            SFD.Filter = "Microsoft Excel(*.xls)|*.xls|Comma Delimited File(*.csv)|*.Csv"
            SFD.OverwritePrompt = True
            SFD.ShowDialog()
            strFileName = SFD.FileName
            table = New DataTable
            table = GridBaseOnSupplier.DataSource

            ' If SFD.ShowDialog() = DialogResult.OK Then
            If SFD.FilterIndex = 1 Then
                mDataset = New DataSet("Data")

                mDataset.Tables.Add(table.Copy)
                If WriteXLSFile(strFileName, mDataset) Then
                    MsgBox("Export Finish", MsgBoxStyle.Information, Title)

                End If
            Else
                Call ExporttoCSV(table, strFileName, vbTab)
                MsgBox("Export Finish", MsgBoxStyle.Information, Title)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            table = New DataTable

            table = ReportBaseOnSupplier(dtFrom.Value, dtTo.Value)

            If table.Rows.Count > 0 Then

                With GridBaseOnSupplier
                    .AutoGenerateColumns = False
                    .Columns(0).DataPropertyName = "code"
                    .Columns(1).DataPropertyName = "name"
                    .Columns(2).DataPropertyName = "consi"
                    .Columns(3).DataPropertyName = "credit"
      
                End With

                GridBaseOnSupplier.DataSource = table

            Else
                GridBaseOnSupplier.DataSource = Nothing


            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        Finally

            Me.Cursor = Cursors.Default

        End Try
    End Sub
End Class