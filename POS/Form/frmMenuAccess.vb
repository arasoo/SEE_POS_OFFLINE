Imports mainlib
Imports genLib.General
Imports sqlLib.Sql
Imports System.IO

Public Class frmMenuAccess


    Private Sub LoadImage()

        picTitle.Image = mainClass.imgList.ImgLabelParameter
     
    End Sub

    Private Sub frmMenuAccess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        MenuAccess()
    End Sub

    Private Sub MenuAccess()

        menuTreeView.Nodes.Clear()

        For i As Integer = 0 To tblMenuAccess.Rows.Count - 1
            Dim ndRoot As New TreeNode

            With ndRoot
                .Name = tblMenuAccess.Rows(i).Item(1)
                .Text = tblMenuAccess.Rows(i).Item(6)
                .ToolTipText = tblMenuAccess.Rows(i).Item(6)

            End With
            menuTreeView.Nodes.Add(ndRoot)
        Next
    End Sub

    Private Sub GridMenuAccess_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles GridMenuAccess.CellPainting
        Dim IconImg As Image = mainClass.imgList.ImgBtnSearch2S
        If e.RowIndex = -1 AndAlso e.ColumnIndex = 2 Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.ContentForeground)
            e.Graphics.DrawImage(IconImg, e.CellBounds.Left + 5, e.CellBounds.Top + 6, 16, 16)
            e.Handled = True
        End If

        IconImg = mainClass.imgList.ImgBtnEdit2S
        If e.RowIndex = -1 AndAlso e.ColumnIndex = 4 Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.ContentForeground)
            e.Graphics.DrawImage(IconImg, e.CellBounds.Left + 5, e.CellBounds.Top + 6, 16, 16)
            e.Handled = True
        End If

    End Sub

    Private Sub menuTreeView_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles menuTreeView.AfterSelect
        table = New DataTable

        table = CreateMenuAccess(menuTreeView.SelectedNode.Name)

        If table.Rows.Count > 0 Then
      
            With GridMenuAccess
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "userid"
                .Columns(1).DataPropertyName = "usergroup_description"
                DirectCast(.Columns(2), DataGridViewCheckBoxColumn).DataPropertyName = "isselect"
                DirectCast(.Columns(3), DataGridViewCheckBoxColumn).DataPropertyName = "isinsert"
                DirectCast(.Columns(4), DataGridViewCheckBoxColumn).DataPropertyName = "isedit"
                DirectCast(.Columns(5), DataGridViewCheckBoxColumn).DataPropertyName = "isdelete"
                DirectCast(.Columns(6), DataGridViewCheckBoxColumn).DataPropertyName = "isprint"

            End With
            GridMenuAccess.DataSource = table


            table = New DataTable

            table = GetChildMenu(menuTreeView.SelectedNode.Name)

            Dim ndChild As TreeNode

            If table.Rows.Count > 0 Then

                For c As Integer = 0 To table.Rows.Count - 1
                    ndChild = New TreeNode

                    With ndChild
                        .Name = tblMenuAccess.Rows(c).Item(0)
                        .Text = tblMenuAccess.Rows(c).Item(2)
                        .ToolTipText = tblMenuAccess.Rows(c).Item(2)
                    End With



                Next

            End If

        End If
    End Sub



    Private Sub GridMenuAccess_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridMenuAccess.CellContentClick
        GridMenuAccess.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub GridMenuAccess_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridMenuAccess.CellValueChanged
        If e.ColumnIndex = 2 Or e.ColumnIndex = 3 Or e.ColumnIndex = 4 Or e.ColumnIndex = 5 Or e.ColumnIndex = 6 Then
            UpdateMenuAccess(menuTreeView.SelectedNode.Name, _
                             GridMenuAccess.Item(0, e.RowIndex).Value, e.ColumnIndex, _
                             GridMenuAccess.Item(e.ColumnIndex, e.RowIndex).Value)

        End If
    End Sub
End Class