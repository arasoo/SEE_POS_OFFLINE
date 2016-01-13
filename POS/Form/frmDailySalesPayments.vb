Imports System.Drawing.Drawing2D
Imports connLib.DBConnection
Imports genLib.General
Imports proLib.Process
Imports saveLib.Save
Imports System.IO
Imports mainlib
Imports sqlLib.Sql

Public Class frmDailySalesPayments

    Private mNow As Date
    Private mLast As Date

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor


            mNow = Format(dtFrom.Value, "yyyy") & "-" & Format(dtFrom.Value, "MM") & "-01"
            mLast = LastDayOfMonth(mNow)

            table = New DataTable

            table = DailySalesPayments(cmbSalesOrgPOS.SelectedValue, IIf(cmbEmployeeID.Text <> "Any", cmbEmployeeID.SelectedValue, ""), _
                                       mNow, mLast)

            With GridDailySalesPayments
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "Date"
                .Columns(1).DataPropertyName = "Sales"
                .Columns(2).DataPropertyName = "Voucher"
                .Columns(3).DataPropertyName = "Cash"
                .Columns(4).DataPropertyName = "Credit"
                .Columns(5).DataPropertyName = "Debit"
                .Columns(6).DataPropertyName = "Charges"
            End With

            GridDailySalesPayments.DataSource = table



            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub frmDailySalesPayments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
     
        LoadSalesOrg(cmbSalesOrgPOS, gridAll, 0)
        cmbEmployeeID.Text = "Any"

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        If gridAll.Tag = "SLSORG" Then
            cmbSalesOrgPOS.SelectedValue = gridAll.SelectedCells(0).Value
        Else
            cmbEmployeeID.SelectedValue = gridAll.SelectedCells(0).Value
        End If

        gridAll.Visible = False

    End Sub

    Private Sub cmbEmployeeID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployeeID.TextChanged
        If cmbEmployeeID.Text = "" Then
            cmbEmployeeID.Text = "Any"
        End If
    End Sub

    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function

    Private Sub cmbEmployee_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployeeID.DropDown, cmbSalesOrgPOS.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)
            mNow = Format(dtFrom.Value, "yyyy") & "-" & Format(dtFrom.Value, "MM") & "-01"
            mLast = LastDayOfMonth(mNow)


            Select Case senderCmb.Tag
                Case "SLSORG"
                    LoadSalesOrg(cmbSalesOrgPOS, gridAll, 1)
                Case Else
                    LoadCashier(senderCmb, mNow, mLast, gridAll)

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

            gridAll.Columns(0).Width = 60
            gridAll.Columns(1).Width = gridAll.Width - 64

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub


    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag

                Case "SLSORG"
                    If Not cmbSalesOrgPOS.Focused = True Then gridAll.Visible = False

                Case Else

                    If Not cmbEmployeeID.Focused = True Then gridAll.Visible = False

            End Select
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
            table = GridDailySalesPayments.DataSource

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

End Class