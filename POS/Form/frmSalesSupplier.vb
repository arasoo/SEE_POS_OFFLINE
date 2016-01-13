Imports genLib.General
Imports mainlib
Imports proLib.Process
Imports sqlLib.Sql
Imports saveLib.Save

Public Class frmSalesSupplier

    Private tb As TextBox
    Private lb As New Label()

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub cmbSupplier_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.DropDown, cmbWarehouse.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "WAREHOUSE"
                    LoadWarehouse(senderCmb, gridAll, 1)
                Case Else
                    LoadSupplier(cmbSupplier, gridAll, "", 0)
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

    Private Sub cmbSupplier_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbSupplier.KeyPress
        If gridAll.Visible = True Then
            gridAll.Visible = False

        End If
    End Sub

    Private Sub cmbSupplier_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSupplier.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Trim(cmbSupplier.Text) <> "" Then
                LoadSupplier(cmbSupplier, gridAll, Trim(cmbSupplier.Text), 1)

                gridAll.Location = New Point(cmbSupplier.Left, cmbSupplier.Location.Y + 22)
                gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                                         (cmbSupplier.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, _
                                         GetRowHeight(gridAll.Rows.Count, gridAll))
                cmbSupplier.DroppedDown = False

                If gridAll.Visible = True Then
                    gridAll.Visible = False
                    cmbSupplier.Focus()
                Else
                    If gridAll.RowCount > 0 Then gridAll.Visible = True
                    cmbSupplier.DroppedDown = False
                    gridAll.Focus()

                End If

                gridAll.Tag = cmbSupplier.Tag

                gridAll.Columns(0).Width = 50
                gridAll.Columns(1).Width = gridAll.Width - 54

            End If
        End If
    End Sub

    Private Sub LoadImage()
        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnClose.Image = mainClass.imgList.ImgBtnClosing
        btnExport.Image = mainClass.imgList.ImgBtnExport
        picTitle.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub frmSalesSupplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadWarehouse(cmbWarehouse, gridAll, 0)
        cmbWarehouse.SelectedValue = GetValueParamText("DEFAULT WH")
    End Sub

    'Private Sub GridFooter()
    '    lb.Font = New Font("Tahoma", 10, FontStyle.Bold)

    '    lb.Text = "Summary"

    '    lb.Height = 20

    '    lb.AutoSize = False

    '    lb.TextAlign = ContentAlignment.MiddleCenter

    '    Dim X As Integer = GridSalesSupplier.GetCellDisplayRectangle(0, -1, True).Location.X

    '    lb.Width = GridSalesSupplier.Columns(0).Width + X

    '    lb.Location = New Point(0, GridSalesSupplier.Height - 20)

    '    GridSalesSupplier.Controls.Add(lb)


    '    For c As Integer = 1 To GridSalesSupplier.Columns.Count - 1
    '        tb = New TextBox

    '        tb.Font = New Font("Tahoma", 10, FontStyle.Bold)

    '        If c = 6 Then
    '            tb.TextAlign = HorizontalAlignment.Right
    '        End If
    '        tb.Width = GridSalesSupplier.Columns(c).Width

    '        X = GridSalesSupplier.GetCellDisplayRectangle(c, -1, True).Location.X

    '        tb.Location = New Point(X, GridSalesSupplier.Height - tb.Height)

    '        GridSalesSupplier.Controls.Add(tb)


    '    Next


    '    AddHandler GridSalesSupplier.CellPainting, AddressOf DataGridView1_CellPainting

    '    AddHandler GridSalesSupplier.CellEndEdit, AddressOf DataGridView1_CellEndEdit

    'End Sub


    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        Select Case gridAll.Tag

            Case "WAREHOUSE"
                cmbWarehouse.SelectedValue = gridAll.SelectedCells(0).Value

            Case Else

                cmbSupplier.SelectedValue = gridAll.SelectedCells(0).Value

        End Select


        gridAll.Visible = False

    End Sub

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag
        
                Case "WAREHOUSE"
                    If Not cmbWarehouse.Focused = True Then gridAll.Visible = False

                Case Else

                    If Not cmbSupplier.Focused = True Then gridAll.Visible = False

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
            table = GridSalesSupplier.DataSource

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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim dt As New DataTable
            Dim sts As String
            Dim amt As Decimal = 0
            Dim qty As Integer = 0


            If Trim(cmbSupplier.Text) = "" Then Exit Sub
            Me.Cursor = Cursors.WaitCursor

            If cmbStatus.SelectedIndex = 0 Then
                sts = ""
                GridSalesSupplier.Columns(8).Visible = True
            Else
                If cmbStatus.SelectedIndex = 1 Then
                    sts = "C"
                Else
                    sts = "G"
                End If

                GridSalesSupplier.Columns(8).Visible = False
            End If
            dt = ReportSalesSupplier(cmbSupplier.SelectedValue, dtFrom.Value, dtTo.Value, sts, cmbWarehouse.SelectedValue)

            With GridSalesSupplier
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "iden"
                .Columns(1).DataPropertyName = "vendor"
                .Columns(2).DataPropertyName = "item"
                .Columns(3).DataPropertyName = "judul"
                .Columns(4).DataPropertyName = "product"
                .Columns(5).DataPropertyName = "purchase"
                .Columns(5).DefaultCellStyle.Format = "##,0"
                .Columns(6).DataPropertyName = "qty"
                .Columns(7).DataPropertyName = "amount"
                .Columns(7).DefaultCellStyle.Format = "##,0"
                .Columns(8).DataPropertyName = "sts"
                .Columns(9).DataPropertyName = "stock"
            End With

            GridSalesSupplier.DataSource = dt
            'GridFooter()
            'Dim sum As Integer = 0

            'For i As Integer = 0 To GridSalesSupplier.Rows.Count - 1

            '    sum += Convert.ToInt32(GridSalesSupplier(7, i).Value)

            'Next

            'tb.Text = sum.ToString()

            For i As Integer = 0 To GridSalesSupplier.RowCount - 1
                amt += GridSalesSupplier.Rows(i).Cells(7).Value


            Next

            lblTotal.Text = String.Format("{0:#,##0}", amt)

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If e.ColumnIndex = 1 Then

            'calculate the sum total

            Dim sum As Integer = 0

            For i As Integer = 0 To gridSalesSupplier.Rows.Count - 1

                sum += Convert.ToInt32(GridSalesSupplier(7, i).Value)

            Next

            tb.Text = sum.ToString()

        End If

    End Sub



    Private Sub DataGridView1_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs)

        Dim X As Integer = gridSalesSupplier.GetCellDisplayRectangle(0, -1, True).Location.X

        lb.Width = GridSalesSupplier.Columns(0).Width + X

        lb.Location = New Point(0, gridSalesSupplier.Height - tb.Height)


        For c As Integer = 1 To GridSalesSupplier.Columns.Count - 1
            tb.Width = GridSalesSupplier.Columns(c).Width

            X = GridSalesSupplier.GetCellDisplayRectangle(c, -1, True).Location.X

            tb.Location = New Point(X, GridSalesSupplier.Height - tb.Height)

        Next



    End Sub

    Private Sub GridSalesSupplier_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridSalesSupplier.DoubleClick
        If GridSalesSupplier.SelectedCells(2).Value <> "" Then
            frmStockCard.ItemCode = GridSalesSupplier.SelectedCells(2).Value
            frmStockCard.Description = GridSalesSupplier.SelectedCells(3).Value
            frmStockCard.WHCode = GetValueParamText("DEFAULT WH")
            frmStockCard.WHName = GetDetailWH(GetValueParamText("DEFAULT WH"))
            frmStockCard.ShowDialog()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim ds As DataSet = New DataSet("SalesSupplier_Table")
        Dim temp As New DataTable

        table = New DataTable

        Me.Cursor = Cursors.WaitCursor
        With temp.Columns
            .Add("Item", GetType(String))
            .Add("Description", GetType(String))
            .Add("Qty", GetType(Integer))
            .Add("Purchase", GetType(Decimal))
            .Add("Amount", GetType(Decimal))
        End With

        For i As Integer = 0 To GridSalesSupplier.RowCount - 1
            With temp
                .Rows.Add(New Object() {GridSalesSupplier.Rows(i).Cells(2).Value _
                                       , GridSalesSupplier.Rows(i).Cells(3).Value _
                                       , CInt(GridSalesSupplier.Rows(i).Cells(6).Value) _
                                       , GridSalesSupplier.Rows(i).Cells(5).Value _
                                       , GridSalesSupplier.Rows(i).Cells(7).Value})

            End With
        Next


        table = temp.Copy

        ds.Reset()
        ds.Tables.Add(table)

        Me.Cursor = Cursors.Default
        frmReportViewer.Datasource = ds
        frmReportViewer.ReportDocument = "SalesSupplier.rdlc"
        frmReportViewer.ReportPath = ".\Report\SalesSupplier.rdlc"
        frmReportViewer.ReportDatasetName = "SalesSupplier_Table"
        frmReportViewer.ShowDialog()
    End Sub
End Class