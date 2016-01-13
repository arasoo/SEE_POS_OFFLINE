Imports System.Drawing.Drawing2D
Imports connlib.DBConnection
Imports genLib.General
Imports prolib.Process
Imports saveLib.Save
Imports sqlLib.Sql
Imports System.IO
Imports mainlib

Public Class frmBestSeller


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RefreshData()
        Try

            Dim top As Integer = CInt(txtTOP.Text)
            Dim startDate As Date = CDate(dtFrom.Value)
            Dim endDate As Date = CDate(dtTo.Value)
            Dim type As String = cmbGroup.SelectedValue

            table = New DataTable


            table = ReportBestSeller(top, startDate, endDate, cmbGroup.SelectedValue, _
                                 cmbWarehouse.SelectedValue, GetValueParamText("DEFAULT BRANCH"), cmbSalesOrgPOS.SelectedValue)


            If table.Rows.Count > 0 Then
     
                With GridBestSeller
                    .AutoGenerateColumns = False
                    .Columns(0).DataPropertyName = "Item"
                    .Columns(1).DataPropertyName = "Judul"
                    .Columns(2).DataPropertyName = "Product"
                    .Columns(3).DataPropertyName = "Sales"
                    .Columns(4).DataPropertyName = "Stock"
              

                End With

                GridBestSeller.DataSource = table


            Else
                GridBestSeller.DataSource = Nothing


            End If


        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
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
            table = GridBestSeller.DataSource

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
        Me.Cursor = Cursors.WaitCursor
        RefreshData()
        Me.Cursor = Cursors.Default
    End Sub

    'Private Sub frmBestSeller_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
    '    Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
    '    Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
    '    Dim colors As Color() = {Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, Color.White}
    '    Dim positions As Single() = {0.0F, 0.07F, 0.85F, 1.0F}
    '    Dim blend As New ColorBlend
    '    blend.Colors = colors
    '    blend.Positions = positions
    '    Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
    '        lgb.InterpolationColors = blend
    '        e.Graphics.FillRectangle(lgb, bounds)
    '    End Using
    'End Sub

    Private Sub txtTOP_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTOP.SelectAll()
    End Sub

    Private Sub txtQty_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtTOP.SelectAll()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmbGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroup.Click, cmbWarehouse.Click, cmbSalesOrgPOS.Click
        Try


            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "GROUP"

                    LoadProductGroup(senderCmb, gridAll, 1)
                Case "WAREHOUSE"

                    LoadWarehouse(senderCmb, gridAll, 1)
                Case Else

                    LoadSalesOrg(senderCmb, gridAll, 1)
            End Select

            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)), GetRowHeight(gridAll.Rows.Count, gridAll))

            senderCmb.DroppedDown = False

            If gridAll.Visible = True Then
                gridAll.Visible = False
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
            End If


            gridAll.Tag = senderCmb.Tag

            gridAll.Columns(0).Width = 50
            gridAll.Columns(1).Width = gridAll.Width - 54
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick
        Select Case gridAll.Tag
            Case "GROUP"
                cmbGroup.SelectedValue = gridAll.SelectedCells(0).Value
            Case "WAREHOUSE"
                cmbWarehouse.SelectedValue = gridAll.SelectedCells(0).Value
            Case Else
                cmbSalesOrgPOS.SelectedValue = gridAll.SelectedCells(0).Value
        End Select

        gridAll.Visible = False
    End Sub

    Private Sub gridAll_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.MouseLeave
        gridAll.Visible = False
    End Sub

    Private Sub txtTOP_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTOP.Click
        txtTOP.SelectAll()
    End Sub

    Private Sub txtTOP_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTOP.GotFocus
        txtTOP.SelectAll()
    End Sub

    Private Sub txtTOP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTOP.TextChanged
        If Trim(txtTOP.Text) = "" Then
            txtTOP.Text = 0
        End If
    End Sub

    Private Sub frmBestSeller_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadImage()
        LoadProductGroup(cmbGroup, gridAll, 0)
        LoadWarehouse(cmbWarehouse, gridAll, 0)
        LoadSalesOrg(cmbSalesOrgPOS, gridAll, 0)
        cmbWarehouse.SelectedValue = GetValueParamText("DEFAULT WH")
        cmbSalesOrgPOS.SelectedValue = GetValueParamText("POS SLSORG")
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnExport.Image = mainClass.imgList.ImgBtnExport

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub GridBestSeller_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridBestSeller.DoubleClick
        If GridBestSeller.SelectedCells(0).Value <> "" Then
            frmStockCard.ItemCode = GridBestSeller.SelectedCells(0).Value
            frmStockCard.Description = GridBestSeller.SelectedCells(1).Value
            frmStockCard.WHCode = cmbWarehouse.SelectedValue
            frmStockCard.WHName = cmbWarehouse.Text
            frmStockCard.ShowDialog()
        End If
    End Sub

    Private Sub GridBestSeller_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridBestSeller.CellContentClick

    End Sub
End Class
      