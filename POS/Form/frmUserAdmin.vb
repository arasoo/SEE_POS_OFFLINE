Imports genLib.General
Imports connLib.DBConnection
Imports secuLib.Security
Imports sqlLib.Sql
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports proLib.Process
Imports mainlib
Imports System.IO

Public Class frmUserAdmin

    Private mstate As Integer
    Private positionSelected As Integer

    Private Sub frmUserAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()

        loadUserGroup(colGroup)
        LoadColLocked()
        LoadUsers(GridUsers)
        RefreshPasswordChar()

    End Sub

    Private Sub LoadColLocked()
        Dim LoggingLevelsList As New Dictionary(Of String, Integer)
        For Each enumValue As Integer In [Enum].GetValues(GetType(Status))
            LoggingLevelsList.Add([Enum].GetName(GetType(Status), enumValue), enumValue)
        Next

        With colBlocked
            .DataSource = New Windows.Forms.BindingSource(LoggingLevelsList, Nothing)
            .DisplayMember = "KEY"
            .ValueMember = "VALUE"
        End With



    End Sub

    Private Sub LoadImage()

        picLabel.Image = mainClass.imgList.ImgLabelUsers

        btnNew.Image = mainClass.imgList.ImgBtnNew

        btnClose.Image = mainClass.imgList.ImgBtnClosing

    End Sub

    Private Sub GridUsers_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles GridUsers.CellFormatting
        If e.ColumnIndex = 2 Then
            If GridUsers.Rows.Count > 0 And Not e.Value Is Nothing Then

                If Not GridUsers.Rows(e.RowIndex).Cells(2).Tag Is Nothing Then
                    With GridUsers.Rows(e.RowIndex).Cells(2)
                        .Value = New String("*", TryCast(.Tag, String).Length)
                    End With
                End If

            End If
        End If



    End Sub


    'Private Sub GridUsers_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridUsers.CellContentClick
    '    Dim senderGrid = DirectCast(sender, DataGridView)

    '    If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
    '       e.RowIndex >= 0 Then

    '        If e.ColumnIndex = 5 Then
    '            GridUsers.EditMode = DataGridViewEditMode.EditOnEnter
    '            GridUsers.Rows(GridUsers.CurrentRow.Index).Cells(0).ReadOnly = True
    '        End If

    '    End If
    'End Sub

    Private Sub GridUsers_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridUsers.CellMouseEnter
        Dim senderGrid = DirectCast(sender, DataGridView)

        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
              e.RowIndex >= 0 Then
            GridUsers.Cursor = Cursors.Hand
        Else
            GridUsers.Cursor = Cursors.Default
        End If
    End Sub

    'Private Sub GridUsers_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles GridUsers.CellPainting
    '    If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
    '        e.Paint(e.CellBounds, DataGridViewPaintParts.All)

    '        If File.Exists(Application.StartupPath & "\image\button\edit2.png") Then
    '            Dim img As Image = Image.FromFile(Application.StartupPath & "\Image\button\edit2.png")
    '            e.Graphics.DrawImage(img, e.CellBounds.Left + 10, e.CellBounds.Top + 5, 16, 16)
    '            e.Handled = True

    '        End If

    '    End If

    '    'If e.ColumnIndex = 6 AndAlso e.RowIndex >= 0 Then
    '    '    e.Paint(e.CellBounds, DataGridViewPaintParts.All)

    '    '    If File.Exists(Application.StartupPath & "\image\button\search.png") Then
    '    '        Dim img As Image = Image.FromFile(Application.StartupPath & "\Image\button\search.png")
    '    '        e.Graphics.DrawImage(img, e.CellBounds.Left + 10, e.CellBounds.Top + 5, 16, 16)
    '    '        e.Handled = True

    '    '    End If

    '    'End If
    'End Sub



    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        GridUsers.Rows.Add()
        mstate = 1
        btnNew.Enabled = False

        GridUsers.Refresh()
        GridUsers.Rows(GridUsers.Rows.Count - 1).Selected = True


        For i As Integer = 0 To 5
            GridUsers.Rows(GridUsers.Rows.Count - 1).Cells(i).ReadOnly = False
        Next

        GridUsers.Rows(GridUsers.Rows.Count - 1).Cells(6).Value = logOn
        GridUsers.Rows(GridUsers.Rows.Count - 1).Cells(7).Value = Format(Now, "MM/dd/yyyy")
        GridUsers.Rows(GridUsers.Rows.Count - 1).Cells(8).Value = Format(Now, "HHmmss")




    End Sub

    Private Sub GridUsers_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GridUsers.EditingControlShowing


        If (CType(sender, DataGridView).CurrentCell.ColumnIndex = 2) Then

            CType(e.Control, TextBox).PasswordChar = CChar("*")

        End If

        If (CType(sender, DataGridView).CurrentCell.ColumnIndex = 0) Then

            If TypeOf e.Control Is TextBox Then
                DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
            End If

        Else
            If TypeOf e.Control Is TextBox Then
                DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Normal
            End If

        End If


        If TypeOf e.Control Is ComboBox Then
            Dim combo As ComboBox = CType(e.Control, ComboBox)

            If (combo IsNot Nothing) Then

                If GridUsers.CurrentCell.ColumnIndex = 5 Then
                    RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf cmbUserGroup_SelectionchangeCommitted)

                    AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf cmbUserGroup_SelectionchangeCommitted)

                End If


            End If
        End If





    End Sub

    Private Sub cmbUserGroup_SelectionchangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim combo As ComboBox = CType(sender, ComboBox)

        GridUsers.CurrentRow.Cells(5).Value = combo.SelectedValue



    End Sub


    Private Sub GridUsers_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridUsers.CellValidated

        If e.ColumnIndex = 2 Then
            Dim PasswordBox As TextBox = CType(GridUsers.EditingControl, TextBox)
            If PasswordBox IsNot Nothing Then
                GridUsers.Rows(e.RowIndex).Cells(2).Tag =
                PasswordBox.Text
                PasswordBox.PasswordChar = Nothing
            End If
        End If

    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridUsers.Rows.Count = 0 Then
            ContextMenuStrip1.Enabled = False
        Else
            ContextMenuStrip1.Enabled = True

            If mstate = 1 Then
                EditToolStripMenuItem.Enabled = False
                DeleteToolStripMenuItem.Enabled = True
                SaveToolStripMenuItem.Enabled = True
           
            ElseIf mstate = 2 Then
                EditToolStripMenuItem.Enabled = False
                DeleteToolStripMenuItem.Enabled = False
                SaveToolStripMenuItem.Enabled = True

            Else
                EditToolStripMenuItem.Enabled = True
                DeleteToolStripMenuItem.Enabled = True
                SaveToolStripMenuItem.Enabled = False

            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If mstate = 1 Then
                'cek 
                If GridUsers.CurrentRow.Cells(0).Value = "" Then
                    MsgBox("User ID must be fill!!!", MsgBoxStyle.Exclamation, Title)
                    GridUsers.CurrentRow.Cells(0).Selected = True

                    Exit Sub
                End If


                AddUser(GridUsers.CurrentRow.Cells(0).Value, GridUsers.CurrentRow.Cells(1).Value, _
                                      encryptString(GridUsers.CurrentRow.Cells(2).Tag), GridUsers.CurrentRow.Cells(3).Value, _
                                      GridUsers.CurrentRow.Cells(4).Value, GridUsers.CurrentRow.Cells(5).Value)

            Else

                UpdateUser(GridUsers.CurrentRow.Cells(0).Value, GridUsers.CurrentRow.Cells(1).Value, _
                                     GridUsers.CurrentRow.Cells(3).Value, _
                                     GridUsers.CurrentRow.Cells(4).Value, GridUsers.CurrentRow.Cells(5).Value)
            End If

            GridUsers.Rows.Clear()
            LoadUsers(GridUsers)

            ColumnAllReadonly()
            btnNew.Enabled = True
            mstate = 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub ColumnAllReadonly()
        For i As Integer = 0 To GridUsers.Columns.Count - 1
            GridUsers.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub RefreshPasswordChar()

        For i As Integer = 0 To GridUsers.Rows.Count - 1


            With GridUsers.Rows(i).Cells(2)
                .Tag = .Value
                .Value = New String("*", TryCast(.Tag, String).Length)
            End With
        Next



    End Sub


    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        GridUsers.Rows.Remove(GridUsers.CurrentRow)

        If mstate = 1 Then
            btnNew.Enabled = True
            mstate = 0
        End If

        ColumnAllReadonly()
    End Sub

  
    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click

        GridUsers.CurrentRow.Cells(1).ReadOnly = False
        GridUsers.CurrentRow.Cells(3).ReadOnly = False
        GridUsers.CurrentRow.Cells(4).ReadOnly = False
        GridUsers.CurrentRow.Cells(5).ReadOnly = False
        mstate = 2
        positionSelected = GridUsers.CurrentRow.Index


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If mstate = 1 Or mstate = 2 Then
            MsgBox("Please finish your process!!", MsgBoxStyle.Exclamation, Title)

            Exit Sub
        Else
            Close()
        End If

    End Sub


    Private Sub GridUsers_SelectionChanged(sender As Object, e As System.EventArgs) Handles GridUsers.SelectionChanged

        If mstate = 1 Then

        ElseIf mstate = 2 Then

            If positionSelected <> GridUsers.CurrentRow.Index Then
                Dim result As String = MsgBox("Are you sure you want to save your changes?", MsgBoxStyle.YesNo)
                If result = vbYes Then
                    mstate = 0
                    GridUsers.ClearSelection()
                    GridUsers.Rows.Clear()

                    LoadUsers(GridUsers)
                    RefreshPasswordChar()

                    ColumnAllReadonly()


                Else
                    mstate = 0
                    GridUsers.Rows(positionSelected).Selected = True
                    ColumnAllReadonly()
                End If
            End If

            End If
    End Sub
End Class