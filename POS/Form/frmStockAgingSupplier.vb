Imports mainlib
Imports genLib.General
Imports saveLib.Save
Imports sqlLib.Sql
Imports proLib.Process
Imports POS.StackedHeader

Public Class frmStockAgingSupplier

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim objREnderer As New StackedHeaderDecorator(GridStockAgingSupplier)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub frmStockAgingSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()
        LoadProductGroup(cmbGroup, gridAll, 0)
        cmbStatus.SelectedIndex = 0
        cmbStock.SelectedIndex = 0

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim dt As New DataTable
            Dim sts As String
            Dim stk As String
            Dim amt As Decimal = 0
            Dim qty As Integer = 0
            Dim n0, n1, n2, n3, n4, n5 As Date


            If cmbStatus.SelectedIndex = 0 Then
                sts = "C"
            Else
                sts = "G"
            End If

            If cmbStock.SelectedIndex = 0 Then
                stk = "Y"
            Else
                stk = "N"
            End If

            n0 = Format(GetValueParamDate("SYSTEM DATE"), "MM") & "/01/" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy")
            n1 = Format(DateAdd(DateInterval.Month, -1, CDate(GetValueParamDate("SYSTEM DATE"))), "MM") & "/01/" & _
                 Format(DateAdd(DateInterval.Month, -1, CDate(GetValueParamDate("SYSTEM DATE"))), "yyyy")
            n2 = Format(DateAdd(DateInterval.Month, -2, CDate(GetValueParamDate("SYSTEM DATE"))), "MM") & "/01/" & _
                 Format(DateAdd(DateInterval.Month, -2, CDate(GetValueParamDate("SYSTEM DATE"))), "yyyy")
            n3 = Format(DateAdd(DateInterval.Month, -3, CDate(GetValueParamDate("SYSTEM DATE"))), "MM") & "/01/" & _
                 Format(DateAdd(DateInterval.Month, -3, CDate(GetValueParamDate("SYSTEM DATE"))), "yyyy")
            n4 = Format(DateAdd(DateInterval.Month, -4, CDate(GetValueParamDate("SYSTEM DATE"))), "MM") & "/01/" & _
                 Format(DateAdd(DateInterval.Month, -4, CDate(GetValueParamDate("SYSTEM DATE"))), "yyyy")
            n5 = Format(DateAdd(DateInterval.Month, -5, CDate(GetValueParamDate("SYSTEM DATE"))), "MM") & "/01/" & _
                 Format(DateAdd(DateInterval.Month, -5, CDate(GetValueParamDate("SYSTEM DATE"))), "yyyy")

            If Trim(cmbSupplier.Text) = "" Then Exit Sub
            Me.Cursor = Cursors.WaitCursor

            dt = ReportStockAgingSupplier(GetValueParamText("DEFAULT WH"), sts, cmbGroup.SelectedValue _
                                          , cmbSupplier.SelectedValue, stk, n0, n1, n2, n3, n4, n5)

            With GridStockAgingSupplier
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "Item"
                .Columns(1).DataPropertyName = "Judul"
                .Columns(2).DataPropertyName = "N5"
                .Columns(3).DataPropertyName = "N4"
                .Columns(4).DataPropertyName = "N3"
                .Columns(5).DataPropertyName = "N2"
                .Columns(6).DataPropertyName = "N1"
                .Columns(7).DataPropertyName = "N0"
                .Columns(8).DataPropertyName = "Stock"
                .Columns(9).DataPropertyName = "Last_BM"
                .Columns(10).DataPropertyName = "BM_Date"
                .Columns(11).DataPropertyName = "BM_Qty"
                .Columns(12).DataPropertyName = "DN"
                .Columns(13).DataPropertyName = "MR"
            End With

            GridStockAgingSupplier.DataSource = dt

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        Select Case gridAll.Tag

            Case "GROUP"
                cmbGroup.SelectedValue = gridAll.SelectedCells(0).Value

            Case Else

                cmbSupplier.SelectedValue = gridAll.SelectedCells(0).Value

        End Select


        gridAll.Visible = False

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

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag

                Case "GROUP"
                    If Not cmbGroup.Focused = True Then gridAll.Visible = False
                Case Else

                    If Not cmbSupplier.Focused = True Then gridAll.Visible = False

            End Select
        End If
    End Sub

    Private Sub LoadImage()
        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnClose.Image = mainClass.imgList.ImgBtnClosing
        btnExport.Image = mainClass.imgList.ImgBtnExport
        picTitle.Image = mainClass.imgList.ImgLabelReporting

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
            table = GridStockAgingSupplier.DataSource

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

    Private Sub cmb_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroup.DropDown, cmbSupplier.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "GROUP"
                    LoadProductGroup(senderCmb, gridAll, 1)
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
                gridAll.Focus()
            End If

            gridAll.Tag = senderCmb.Tag

            gridAll.Columns(0).Width = 50
            gridAll.Columns(1).Width = gridAll.Width - 54

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub GridStockAgingSupplier_DoubleClick(sender As Object, e As EventArgs) Handles GridStockAgingSupplier.DoubleClick
        If GridStockAgingSupplier.SelectedCells(0).Value <> "" Then
            frmStockCard.ItemCode = GridStockAgingSupplier.SelectedCells(0).Value
            frmStockCard.Description = GridStockAgingSupplier.SelectedCells(1).Value
            frmStockCard.WHCode = GetValueParamText("DEFAULT WH")
            frmStockCard.WHName = GetDetailWH(GetValueParamText("DEFAULT WH"))
            frmStockCard.ShowDialog()
        End If
    End Sub
End Class