Imports System.IO
Imports mainlib
Imports genLib.General

Public Class frmProducts

    Private ds As DataSet

    Private Sub frmProducts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadImage()
        LoadMaterialType(cmbType, gridAll, 0)
        LoadProducts(cmbProduct, gridAll, 0)

        cmbType.Text = "Any"
        cmbProduct.Text = "Any"
        cmbFilter.SelectedIndex = 1

    End Sub

    Private Sub LoadImage()

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnClose.Image = mainClass.imgList.ImgBtnClosing


        picProducts.Image = mainClass.imgList.ImgLabelTag

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        picSearch.Image = mainClass.imgList.ImgLabelSearch
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick
        If gridAll.Tag = "TYPE" Then
            cmbType.SelectedValue = gridAll.SelectedCells(0).Value

        Else
            cmbProduct.SelectedValue = gridAll.SelectedCells(0).Value
        End If

        gridAll.Visible = False
    End Sub

    Private Sub cmbType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.Click, cmbProduct.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "TYPE"
                    LoadMaterialType(senderCmb, gridAll, 1)
                Case Else
                    LoadProducts(senderCmb, gridAll, 1)
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
    Private Sub GridProducts_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridProducts.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            MsgBox(GridProducts.SelectedCells(0).Value)
        End If

    End Sub

    Private Sub GridProducts_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridProducts.CellMouseEnter

        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
              e.RowIndex >= 0 Then
            GridProducts.Cursor = Cursors.Hand
        Else
            GridProducts.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub cmbType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.TextChanged
        If cmbType.Text = "" Then
            cmbType.Text = "Any"
        End If
    End Sub

    Private Sub cmbProduct_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProduct.TextChanged
        If cmbProduct.Text = "" Then
            cmbProduct.Text = "Any"
        End If
    End Sub

    Private Sub GridProducts_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles GridProducts.CellPainting
        If e.ColumnIndex = 9 AndAlso e.RowIndex >= 0 Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)

            Dim img As Image = mainClass.imgList.ImgBtnEdit2S
            e.Graphics.DrawImage(img, e.CellBounds.Left + 10, e.CellBounds.Top + 5, 16, 16)
            e.Handled = True

        End If

        If e.ColumnIndex = 8 AndAlso e.RowIndex >= 0 Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)

            Dim img As Image = mainClass.imgList.ImgBtnSearch2S
            e.Graphics.DrawImage(img, e.CellBounds.Left + 10, e.CellBounds.Top + 5, 16, 16)
            e.Handled = True


        End If
    End Sub

    'Private Sub txtDescription_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        Me.Cursor = Cursors.WaitCursor
    '        'Dim someText As String = Trim(txtDescription.Text)
    '        'Dim gridRow As Integer = 0
    '        'Dim gridColumn As Integer = 1
    '        'For Each Row As DataGridViewRow In GridProducts.Rows

    '        '    Dim cell As DataGridViewCell = (GridProducts.Rows(Row.Index).Cells(gridColumn))
    '        '    If cell.Value.ToString.ToUpper.Contains(someText.ToUpper) Then
    '        '        cell.Style.BackColor = Color.Khaki
    '        '    Else
    '        '        cell.Style.BackColor = Color.White
    '        '    End If


    '        '    GridProducts.Refresh()


    '        'Next Row
    '        'gridColumn = 0

    '        bs.Filter = String.Format("{0} LIKE '%{1}%'", "judul", Trim(txtDescription.Text))

    '        bs.Sort = "judul ASC"
    '    End If

    '    Me.Cursor = Cursors.Default
    'End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
          

        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim typecode As String
            Dim productcode As String

            Me.Cursor = Cursors.WaitCursor
            If cmbType.Text = "Any" Then
                typecode = ""
            Else
                typecode = cmbType.SelectedValue

            End If

            If cmbProduct.Text = "Any" Then
                productcode = ""
            Else
                productcode = cmbProduct.SelectedValue
            End If

            'GETProducts(GridProducts, TabStatusItem.SelectedIndex, productcode, typecode, Text)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Title)

        End Try
    End Sub

End Class