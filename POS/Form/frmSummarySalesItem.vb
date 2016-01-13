Imports mainlib
Imports genLib.General
Imports sqlLib.Sql
Imports connLib.DBConnection
Imports saveLib.Save
Imports proLib.Process
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Public Class frmSummarySalesItem

    Private Sub frmSummarySalesItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            table = New DataTable
            Dim sts As String = ""
            Dim payType As String = ""
            If cmbStatus.SelectedIndex <> 0 Then

                If cmbStatus.SelectedIndex = 1 Then
                    sts = "C"
                Else
                    sts = "G"
                End If

            End If

            table = ReportSummarySalesItem(IIf(cmbGroup.Text <> "Any", cmbGroup.SelectedValue, ""), _
                                           IIf(cmbStatus.SelectedIndex <> 0, sts, ""), dtFrom.Value, _
                                           dtTo.Value, cmbPayment.SelectedIndex)
            With GridSummarySales
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "Item"
                .Columns(1).DataPropertyName = "Description"
                .Columns(2).DataPropertyName = "UOM"
                .Columns(3).DataPropertyName = "Qty"
                .Columns(4).DataPropertyName = "Unit Price"
                .Columns(5).DataPropertyName = "Nett Price"
                .Columns(6).DataPropertyName = "Amount"
                .Columns(6).DefaultCellStyle.Format = "##,0"
            End With

            GridSummarySales.DataSource = table


            CalculateTotalSummary()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub CalculateTotalSummary()
        Dim total As Decimal = 0
        If GridSummarySales.RowCount > 0 Then
            For i As Integer = 0 To GridSummarySales.RowCount - 1
                total = total + GridSummarySales.Rows(i).Cells(6).Value
            Next
        End If

        If cmbGroup.Text = "Any" And cmbStatus.Text = "Any" And cmbPayment.Text = "Any" Then
            lblTotal.Text = String.Format("{0:#,##0}", total + GetRoundingAmt(dtFrom.Value, dtTo.Value))
        Else
            lblTotal.Text = String.Format("{0:#,##0}", total)
        End If

    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
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
            table = GridSummarySales.DataSource

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

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        cmbGroup.SelectedValue = gridAll.SelectedCells(0).Value


        gridAll.Visible = False
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ds As DataSet = New DataSet("SummarySalesbyItem_Table1")
        Dim temp As New DataTable

        table = New DataTable

        Me.Cursor = Cursors.WaitCursor
        With temp.Columns
            .Add("Item", GetType(String))
            .Add("Description", GetType(String))
            .Add("UOM", GetType(String))
            .Add("Qty", GetType(Integer))
            .Add("Nett Price", GetType(Decimal))
            .Add("Amount", GetType(Decimal))
        End With

        For i As Integer = 0 To GridSummarySales.RowCount - 1
            With temp
                .Rows.Add(New Object() {GridSummarySales.Rows(i).Cells(0).Value _
                                       , GridSummarySales.Rows(i).Cells(1).Value _
                                       , GridSummarySales.Rows(i).Cells(2).Value _
                                       , GridSummarySales.Rows(i).Cells(3).Value _
                                       , GridSummarySales.Rows(i).Cells(4).Value _
                                       , GridSummarySales.Rows(i).Cells(5).Value})

            End With
        Next


        table = temp.Copy

        ds.Reset()
        ds.Tables.Add(table)

        Me.Cursor = Cursors.Default
        frmReportViewer.Datasource = ds
        frmReportViewer.ReportDocument = "SummarySalesbyItem.rdlc"
        frmReportViewer.ReportPath = ".\Report\SummarySalesbyItem.rdlc"
        frmReportViewer.ReportDatasetName = "SummarySalesbyItem_Table1"
        frmReportViewer.ShowDialog()
    End Sub

    Private Sub CreateXMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateXMLToolStripMenuItem.Click
        Dim ds As DataSet = New DataSet("SummarySalesbyItem")

        Dim dt As New DataTable

        With dt.Columns
            .Add("Item", GetType(String))
            .Add("Description", GetType(String))
            .Add("UOM", GetType(String))
            .Add("Qty", GetType(Integer))
            .Add("Nett Price", GetType(Decimal))
            .Add("Amount", GetType(Decimal))
        End With

        For i As Integer = 0 To GridSummarySales.RowCount - 1
            With dt
                .Rows.Add(New Object() {GridSummarySales.Rows(i).Cells(0).Value _
                                       , GridSummarySales.Rows(i).Cells(1).Value _
                                       , GridSummarySales.Rows(i).Cells(2).Value _
                                       , GridSummarySales.Rows(i).Cells(3).Value _
                                       , GridSummarySales.Rows(i).Cells(4).Value _
                                       , GridSummarySales.Rows(i).Cells(5).Value})

            End With
        Next

        ds.Tables.Add(dt)

        ds.WriteXmlSchema(Application.StartupPath & "\XML\SummarySalesbyItem.xsd")


    End Sub

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then


            If Not cmbGroup.Focused = True Then gridAll.Visible = False



        End If

    End Sub

    Private Sub cmbGroup_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroup.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "GROUP"
                    LoadProductGroup(senderCmb, gridAll, 0)
                Case Else

            End Select




            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                                     (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, _
                                     GetRowHeight(gridAll.Rows.Count, gridAll))
            senderCmb.DroppedDown = False

            If gridAll.Visible = True Then
                gridAll.Visible = False
                senderCmb.Focus()
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
                senderCmb.DroppedDown = False
                gridAll.Focus()

            End If

            gridAll.Tag = senderCmb.Tag

            gridAll.Columns(0).Width = 50
            gridAll.Columns(1).Width = gridAll.Width - 54

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmbGroup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroup.TextChanged
        If cmbGroup.Text = "" Then
            cmbGroup.Text = "Any"
        End If
    End Sub

End Class