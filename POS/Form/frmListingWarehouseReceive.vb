﻿Imports genLib.General
Imports proLib.Process
Imports sqlLib.Sql
Imports saveLib.Save
Imports mainlib

Public Class frmListingWarehouseReceive

    Private firstLoad As Boolean = False
    Private seqnum As Integer

    Private Sub frmListingWarehouseReceive_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        cmbStatus.SelectedIndex = 0

    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            firstLoad = False

            table = New DataTable

            table = RptListingWarehouseReceive(cmbSupplier.SelectedValue, dtFrom.Value, dtTo.Value, cmbStatus.SelectedIndex)

            With GridHeader
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "docno"
                .Columns(1).DataPropertyName = "pono"
                .Columns(2).DataPropertyName = "docdate"
                .Columns(3).DataPropertyName = "trnid"
                .Columns(4).DataPropertyName = "dn"
                .Columns(5).DataPropertyName = "dndate"
                .Columns(6).DataPropertyName = "note"
                .Columns(7).DataPropertyName = "sts"

            End With

            GridHeader.DataSource = table

            If GridHeader.RowCount > 0 Then
                GetDetailReceive()
                firstLoad = True
            Else

                firstLoad = False
            End If

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub GetDetailReceive()
        table = New DataTable

        table = ListingReceiveDetail(GridHeader.SelectedCells(0).Value)

        seqnum = 0

        If table.Rows.Count > 0 Then
            gridDetail.Rows.Clear()
            For i As Integer = 0 To table.Rows.Count - 1
                seqnum += 1

                gridDetail.Rows.Add(New Object() {seqnum _
                                                   , Trim(table.Rows(i).Item("Item")) _
                                                   , Trim(table.Rows(i).Item("Judul")) _
                                                    , table.Rows(i).Item("UOM") _
                                                   , table.Rows(i).Item("Qty")})
            Next

        End If
    End Sub


    Private Sub cmbSupplier_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbSupplier.KeyPress
        If gridAll.Visible = True Then
            gridAll.Visible = False

        End If
    End Sub

    Private Sub cmbSupplier_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSupplier.KeyUp
        If e.KeyCode = Keys.Enter Then

            If Not cmbStatus.SelectedIndex = 0 Then Exit Sub

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

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        Select Case gridAll.Tag

            Case "WAREHOUSE"

            Case Else

                cmbSupplier.SelectedValue = gridAll.SelectedCells(0).Value

        End Select


        gridAll.Visible = False

    End Sub

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag

                Case "WAREHOUSE"
               
                Case Else

                    If Not cmbSupplier.Focused = True Then gridAll.Visible = False

            End Select
        End If
    End Sub

    Private Sub cmbSupplier_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)


            If cmbStatus.SelectedIndex = 0 Then
                LoadSupplier(cmbSupplier, gridAll, "", 0)
            ElseIf cmbStatus.SelectedIndex = 1 Then             ',.
                LoadSupplier(cmbSupplier, gridAll, "Tmbookstore", 1)
            Else
                LoadWarehouse(cmbSupplier, gridAll, 0)
            End If


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

            If cmbStatus.SelectedIndex = 1 Then
                gridAll.Columns(0).Width = 65
                gridAll.Columns(1).Width = gridAll.Width - 69
            Else
                gridAll.Columns(0).Width = 50
                gridAll.Columns(1).Width = gridAll.Width - 54
            End If

           
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged

        If cmbStatus.SelectedIndex = 0 Then
            lblReceive.Text = "Supplier"

        ElseIf cmbStatus.SelectedIndex = 1 Then
            lblReceive.Text = "Branch"
        Else
            lblReceive.Text = "Warehouse"

        End If
        cmbSupplier.Text = "Any"

    End Sub

    Private Sub GridHeader_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridHeader.SelectionChanged
        If firstLoad = True Then

            If GridHeader.RowCount > 0 Then

                GetDetailReceive()

            End If


        End If
    End Sub

    Private Sub cmbSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.TextChanged
        If cmbSupplier.Text = "" Or cmbSupplier.Text.Length = 0 Then
            cmbSupplier.Text = "Any"
        End If
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
End Class