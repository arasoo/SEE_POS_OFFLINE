Imports genLib.General
Imports mainlib
Imports sqlLib.Sql
Imports proLib.Process

Public Class frmSalesConsignment

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub frmSalesConsignment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadBranch(cmbBranch, gridAll, 0)

        cmbBranch.SelectedValue = GetValueParamText("DEFAULT BRANCH")
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick


        Select Case gridAll.Tag
            Case "BRANCH"
                cmbBranch.SelectedValue = gridAll.SelectedCells(0).Value
            Case Else
                cmbSupplier.SelectedValue = gridAll.SelectedCells(0).Value
        End Select


        gridAll.Visible = False

    End Sub

    Private Sub cmbAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBranch.Click, cmbSupplier.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "BRANCH"
                    LoadBranch(senderCmb, gridAll, 1)
                Case Else
                    LoadSupplier(senderCmb, gridAll, "", 0)
            End Select


            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                                     (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, _
                                     GetRowHeight(gridAll.Rows.Count, gridAll))
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

    Private Sub cmbSupplier_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSupplier.KeyUp
        If e.KeyCode = Keys.Enter Then
            LoadSupplier(cmbSupplier, gridAll, Trim(cmbSupplier.Text), 1)
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            table = New DataTable

            table = GETSalesConsignment(GetValueParamText("DEFAULT COMPANY"), cmbBranch.SelectedValue, cmbSupplier.SelectedValue, dtPeriod.Value)

            GridSalesConsignment.DataSource = table
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub
End Class