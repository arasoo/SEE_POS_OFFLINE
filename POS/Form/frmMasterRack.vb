Imports genLib.General
Imports proLib.Process
Imports sqlLib.Sql
Imports mainlib

Public Class frmMasterRack

    Private mstate As Integer
    Private positionSelected As Integer

    Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If gridRack.RowCount > 0 Then

            If mstate = 1 Then
                newTool.Enabled = False
                editTool.Enabled = False
                deleteTool.Enabled = False
                saveTool.Enabled = True
                cancelTool.Enabled = True
            ElseIf mstate = 2 Then
                newTool.Enabled = False
                editTool.Enabled = False
                deleteTool.Enabled = False
                saveTool.Enabled = True
                cancelTool.Enabled = True
            Else
                newTool.Enabled = True
                editTool.Enabled = True
                deleteTool.Enabled = True
                saveTool.Enabled = False
                cancelTool.Enabled = False
            End If
        Else
            newTool.Enabled = True
            editTool.Enabled = False
            deleteTool.Enabled = False

        End If
    End Sub

    Private Sub frmMasterRack_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub newTool_Click(sender As System.Object, e As System.EventArgs) Handles newTool.Click
        Dim iden As Integer = 0

        If gridRack.RowCount > 0 Then
            iden += 1

        Else
            iden = 1
        End If
        gridRack.Rows.Add()
        mstate = 1

        gridRack.Rows(gridRack.Rows.Count - 1).Cells(0).Value = iden

        gridRack.Refresh()
        gridRack.Rows(gridRack.Rows.Count - 1).Selected = True

        For i As Integer = 1 To 3
            gridRack.Rows(gridRack.Rows.Count - 1).Cells(i).ReadOnly = False
        Next


    End Sub

    Private Sub gridRack_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles gridRack.EditingControlShowing
        If (CType(sender, DataGridView).CurrentCell.ColumnIndex <> 0) Then

            If TypeOf e.Control Is TextBox Then
                DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
            End If

        End If
    End Sub

    Private Sub saveTool_Click(sender As System.Object, e As System.EventArgs) Handles saveTool.Click

    End Sub

    Private Sub cancelTool_Click(sender As System.Object, e As System.EventArgs) Handles cancelTool.Click

    End Sub

    Private Sub gridRack_SelectionChanged(sender As Object, e As EventArgs) Handles gridRack.SelectionChanged
        If mstate = 1 Then

        ElseIf mstate = 2 Then

            If positionSelected <> gridRack.CurrentRow.Index Then
                Dim result As String = MsgBox("Are you sure you want to save your changes?", MsgBoxStyle.YesNo)
                If result = vbYes Then
                    mstate = 0
                    gridRack.ClearSelection()
                    gridRack.Rows.Clear()

                    'LoadRack(gridRack)

                    ColumnAllReadonly()


                Else
                    mstate = 0
                    gridRack.Rows(positionSelected).Selected = True
                    ColumnAllReadonly()
                End If
            End If

        End If
    End Sub

    Private Sub ColumnAllReadonly()
        For i As Integer = 0 To gridRack.Columns.Count - 1
            gridRack.Columns(i).ReadOnly = True
        Next
    End Sub
End Class