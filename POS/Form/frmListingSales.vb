Imports System.Drawing.Drawing2D
Imports connLib.DBConnection
Imports genLib.General
Imports prolib.Process
Imports saveLib.Save
Imports System.IO
Imports mainlib
Imports sqlLib.Sql

Public Class frmListingSales

    Private firstLoad As Boolean
    Private seqnum As Integer

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            firstLoad = False

            table = New DataTable

            table = ListingSalesHeader(IIf(cmbEmployeeID.Text <> "Any", cmbEmployeeID.SelectedValue, "Any"), _
                                       dtFrom.Value, dtTo.Value, cmbPayment.SelectedIndex)

            With GridHeader
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "Invoice"
                .Columns(1).DataPropertyName = "Date"
                .Columns(2).DataPropertyName = "Warehouse"
                .Columns(3).DataPropertyName = "Customer"
                .Columns(4).DataPropertyName = "Currency"
                .Columns(5).DataPropertyName = "Gross"
                .Columns(6).DataPropertyName = "DPP"
                .Columns(7).DataPropertyName = "PPN"
                .Columns(8).DataPropertyName = "Total"
                .Columns(9).DataPropertyName = "Rounding"
                .Columns(10).DataPropertyName = "Cash"
                .Columns(11).DataPropertyName = "Card"
                .Columns(12).DataPropertyName = "Charges"
                .Columns(13).DataPropertyName = "Emp"
            End With
            GridHeader.DataSource = table

            CalculateTotal()

            If GridHeader.RowCount > 0 Then
                GetDetailInvoice()
                firstLoad = True
            Else
                firstLoad = False

            End If
            'End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub CalculateTotal()
        Dim total, totalCash, totalCard, totalRounding As Decimal

        total = 0
        totalCash = 0
        totalCard = 0
        totalRounding = 0

        If GridHeader.RowCount > 0 Then
            For i As Integer = 0 To GridHeader.RowCount - 1
                total = total + (GridHeader.Rows(i).Cells(8).Value + GridHeader.Rows(i).Cells(9).Value)
                totalCash += GridHeader.Rows(i).Cells(10).Value
                totalCard += GridHeader.Rows(i).Cells(11).Value
                totalRounding += GridHeader.Rows(i).Cells(9).Value

            Next
        End If

        lblTotal.Text = String.Format("{0:#,##0}", Math.Round(total, 0))
        lblTotalCash.Text = String.Format("{0:#,##0}", Math.Round(totalCash, 0))
        lblTotalCard.Text = String.Format("{0:#,##0}", Math.Round(totalCard, 0))
        lblTotalRounding.Text = String.Format("{0:#,##0}", Math.Round(totalRounding, 0))

        lblTotalTransaction.Text = "Total Transaction : " & GridHeader.RowCount

        Dim empID As String = ""
        If Trim(cmbEmployeeID.Text) = "Any" Then
            empID = ""
        Else
            empID = Trim(cmbEmployeeID.SelectedValue)
        End If


        'Charges from Card
        lblTotalCharge.Text = String.Format("{0:#,##0}", CDec(GetCharge(empID, dtFrom.Value, dtTo.Value)))


    End Sub

    Private Sub CalculateTotalDetail()
        Dim total As Decimal = 0
        If GridHeader.RowCount > 0 Then
            For i As Integer = 0 To GridHeader.RowCount - 1
                total = total + GridHeader.Rows(i).Cells(5).Value
            Next
        End If

        'If chckAll.Checked = True And rdbAll.Checked = True Then
        '    lblTotal.Text = String.Format("{0:#,##0}", total + GetRoundingAmt())
        'Else
        '    lblTotal.Text = String.Format("{0:#,##0}", total)
        'End If

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
            table = GridHeader.DataSource

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

    Private Sub frmListingSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadSalesOrg(cmbSalesOrgPOS, gridAll, 0)
        cmbEmployeeID.Text = "Any"
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub cmbEmployee_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployeeID.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)



            LoadCashier(senderCmb, dtFrom.Value, dtTo.Value, gridAll)



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

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.Click
        cmbEmployeeID.SelectedValue = gridAll.SelectedCells(0).Value
        gridAll.Visible = False
    End Sub

    Private Sub cmbEmployeeID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployeeID.TextChanged
        If cmbEmployeeID.Text = "" Or cmbEmployeeID.Text.Length = 0 Then
            cmbEmployeeID.Text = "Any"
        End If
    End Sub


    Private Sub GridHeader_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridHeader.SelectionChanged
        If firstLoad = True Then

            If GridHeader.RowCount > 0 Then

                GetDetailInvoice()

            End If


        End If
    End Sub

    Private Sub GetDetailInvoice()
        table = New DataTable

        table = ListingSalesDetail(GridHeader.SelectedCells(0).Value)

        seqnum = 0

        If table.Rows.Count > 0 Then
            gridDetail.Rows.Clear()
            For i As Integer = 0 To table.Rows.Count - 1
                seqnum += 1

                gridDetail.Rows.Add(New Object() {seqnum _
                                                   , Trim(table.Rows(i).Item("Item")) _
                                                   , Trim(table.Rows(i).Item("Judul")) _
                                                   , table.Rows(i).Item("Qty") _
                                                   , table.Rows(i).Item("dpp") _
                                                   , table.Rows(i).Item("ppn") _
                                                   , table.Rows(i).Item("amount")})
            Next

        End If
    End Sub

End Class