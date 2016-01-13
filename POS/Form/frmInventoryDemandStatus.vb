Imports genLib.General
Imports proLib.Process
Imports sqlLib.Sql
Imports saveLib.Save
Imports mainlib

Public Class frmInventoryDemandStatus

    Private n12, n0, n1, n2, n3, n4, n5 As Date
    Private firstLoad As Boolean = False

    Private Sub frmInventoryDemandStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadProductGroup(cmbGroup, gridAll, 0)

        LoadProducts(cmbProducts, gridAll, 0)
        LoadWarehouse(cmbWarehouse, gridAll, 0)
        cmbWarehouse.SelectedValue = GetValueParamText("DEFAULT WH")

        cmbProducts.Text = "Any"

        cmbFilter.SelectedIndex = 0

        firstLoad = True

    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnExport.Image = mainClass.imgList.ImgBtnExport

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub cmbProduct_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProducts.TextChanged
        If cmbProducts.Text = "" Then
            If cmbFilter.SelectedIndex <> 5 And cmbFilter.SelectedIndex <> 1 Then
                cmbProducts.Text = "Any"
            End If

        End If
    End Sub

    Private Sub cmbSupplier_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbProducts.KeyPress
        If gridAll.Visible = True Then
            gridAll.Visible = False

        End If
    End Sub

    Private Sub cmbSupplier_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbProducts.KeyUp
        If e.KeyCode = Keys.Enter Then

            If cmbFilter.SelectedIndex <> 5 And cmbFilter.SelectedIndex <> 1 Then Exit Sub

            If Trim(cmbProducts.Text) <> "" Then

                If cmbFilter.SelectedIndex = 1 Then
                    LoadProdhier1(cmbProducts, gridAll, Trim(cmbProducts.Text), 1)
                Else

                    LoadSupplier(cmbProducts, gridAll, Trim(cmbProducts.Text), 1)
                End If


                gridAll.Location = New Point(cmbProducts.Left, cmbProducts.Location.Y + 22)
                gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                                         (cmbProducts.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, _
                                         GetRowHeight(gridAll.Rows.Count, gridAll))
                cmbProducts.DroppedDown = False

                If gridAll.Visible = True Then
                    gridAll.Visible = False
                    cmbProducts.Focus()
                Else
                    If gridAll.RowCount > 0 Then gridAll.Visible = True
                    cmbProducts.DroppedDown = False
                    gridAll.Focus()

                End If

                gridAll.Tag = cmbProducts.Tag

                gridAll.Columns(0).Width = 50
                gridAll.Columns(1).Width = gridAll.Width - 54

            End If
        End If
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick
        Select Case gridAll.Tag
            Case "GROUP"
                cmbGroup.SelectedValue = gridAll.SelectedCells(0).Value
            Case "WAREHOUSE"
                cmbWarehouse.SelectedValue = gridAll.SelectedCells(0).Value
            Case "PRODUCTS"
                cmbProducts.SelectedValue = gridAll.SelectedCells(0).Value
            Case Else

        End Select

        gridAll.Visible = False
    End Sub

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag
                Case "GROUP"
                    If Not cmbGroup.Focused = True Then gridAll.Visible = False
                Case "WAREHOUSE"
                    If Not cmbWarehouse.Focused = True Then gridAll.Visible = False
                Case "PRODUCTS"
                    If Not cmbProducts.Focused = True Then gridAll.Visible = False
                Case Else



            End Select
        End If
    End Sub

    Private Sub cmbAll_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroup.DropDown, cmbProducts.DropDown, cmbWarehouse.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "GROUP"
                    LoadProductGroup(senderCmb, gridAll, 1)
                Case "PRODUCTS"
                    Select Case cmbFilter.SelectedIndex
                        Case 0 'Products
                            LoadProducts(cmbProducts, gridAll, 1)
                        Case 1 'Publisher
                            LoadProdhier1(cmbProducts, gridAll, "", 1)
                        Case 2 'Prodhier2
                            LoadProdhier2(cmbProducts, gridAll, 1)
                        Case 3 'Prodhier3
                            LoadProdhier3(cmbProducts, gridAll, 1)
                        Case 4
                            LoadProdhier4(cmbProducts, gridAll, 1)
                        Case Else
                            LoadSupplier(cmbProducts, gridAll, "", 0)
                    End Select
                Case "WAREHOUSE"
                    LoadWarehouse(senderCmb, gridAll, 1)
                Case Else
                    LoadSalesOrg(senderCmb, gridAll, 1)

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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim sts As String = ""
            Dim pro As String = ""

            Dim mnow As Date = Format(dtFrom.Value, "yyyy") & "-" & Format(dtFrom.Value, "MM") & "-01"


            If cmbStatus.SelectedIndex = 0 Then
                sts = ""

            Else
                If cmbStatus.SelectedIndex = 1 Then
                    sts = "C"
                Else
                    sts = "G"
                End If


            End If

            If cmbProducts.Text = "Any" Then
                pro = ""

            Else
                pro = cmbProducts.SelectedValue


            End If
            Dim totalPage As Integer = 0

            table = New DataTable

            table = ReportInventoryDemand(mnow, cmbGroup.SelectedValue, _
                    sts, pro, cmbWarehouse.SelectedValue, cmbFilter.SelectedIndex + 1, chckStock.CheckState)

            With GridInventoryDemand
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "PRODUCT_Description"
                .Columns(1).DataPropertyName = "Item"
                .Columns(2).DataPropertyName = "Item_Description"
                .Columns(3).DataPropertyName = "N12"
                .Columns(4).DataPropertyName = "N5"
                .Columns(5).DataPropertyName = "N4"
                .Columns(6).DataPropertyName = "N3"
                .Columns(7).DataPropertyName = "N2"
                .Columns(8).DataPropertyName = "N1"
                .Columns(9).DataPropertyName = "N0"
                .Columns(10).DataPropertyName = "Stock"
            End With

            GridInventoryDemand.DataSource = table

            lblRecords.Text = GridInventoryDemand.RowCount & " records"

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default

            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub GridInventoryDemand_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles GridInventoryDemand.CellFormatting
        GridInventoryDemand.Rows(e.RowIndex).Cells(0).Style.Font = New Font("Tahoma", 8, FontStyle.Bold)

        If e.RowIndex > 0 And e.ColumnIndex = 0 Then

            If GridInventoryDemand.Item(0, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""


            ElseIf e.RowIndex < GridInventoryDemand.Rows.Count - 1 Then
                GridInventoryDemand.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White

            End If
        End If
    End Sub

    Private Sub GridInventoryDemand_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInventoryDemand.DoubleClick
        If GridInventoryDemand.SelectedCells(0).Value <> "" Then
            frmStockCard.ItemCode = GridInventoryDemand.SelectedCells(1).Value
            frmStockCard.Description = GridInventoryDemand.SelectedCells(2).Value
            frmStockCard.WHCode = cmbWarehouse.SelectedValue
            frmStockCard.WHName = cmbWarehouse.Text
            frmStockCard.ShowDialog()
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
            table = GridInventoryDemand.DataSource

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

  
    Private Sub cmbFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFilter.SelectedIndexChanged

        If Not firstLoad = True Then Exit Sub

        Select Case cmbFilter.SelectedIndex
            Case 0 'Products
                lblStatus.Text = "Products"
                LoadProducts(cmbProducts, gridAll, 0)
            Case 1 'Publisher
                lblStatus.Text = "Publisher"
                cmbProducts.Text = ""
                cmbProducts.Focus()
            Case 2 'Prodhier2
                lblStatus.Text = "Prodhier2"
                LoadProdhier2(cmbProducts, gridAll, 0)
            Case 3 'Prodhier3
                lblStatus.Text = "Prodhier3"
                LoadProdhier3(cmbProducts, gridAll, 0)
            Case 4
                lblStatus.Text = "Prodhier4"
                LoadProdhier4(cmbProducts, gridAll, 0)
            Case 5
                lblStatus.Text = "Suppliers"
                cmbProducts.Text = ""
                cmbProducts.Focus()
            Case Else


        End Select

        If cmbFilter.SelectedIndex <> 6 Then
            btnPasteClipboard.Visible = False
        Else
            btnPasteClipboard.Visible = True
        End If

    End Sub
End Class