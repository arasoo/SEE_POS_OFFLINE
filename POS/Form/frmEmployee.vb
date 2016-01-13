Imports mainlib
Imports sqlLib.Sql
Imports genLib.General
Imports proLib.Process

Public Class frmEmployee


    Private Sub frmEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadImage()

            LoadCompany(cmbCompany, gridAll, 0)
            LoadBranch(cmbBranch, gridAll2, 0)

            Me.Cursor = Cursors.WaitCursor

            GridEmpCatalog.DataSource = GetEmployeeAll(cmbCompany.SelectedValue)
            gridEmpAssignment.DataSource = GetEmployeeAssignment(getvalueparamtext("DEFAULT COMPANY"), cmbBranch.SelectedValue)

            Me.Cursor = Cursors.Default
            GridEmpCatalog.Columns(3).Frozen = True
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub LoadImage()

        btnNew.Image = mainClass.imgList.ImgBtnNew

        btnDelete.Image = mainClass.imgList.ImgBtnVoid

        btnEdit.Image = mainClass.imgList.ImgBtnEdit

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picTitle.Image = mainClass.imgList.ImgLabelUsers
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick, gridAll2.DoubleClick
        Me.Cursor = Cursors.WaitCursor
        If gridAll.Tag = "COMPANY" Then
            cmbCompany.SelectedValue = gridAll.SelectedCells(0).Value
            GridEmpCatalog.DataSource = GetEmployeeAll(cmbCompany.SelectedValue)

            gridAll.Visible = False
        Else
            cmbBranch.SelectedValue = gridAll2.SelectedCells(0).Value
            gridEmpAssignment.DataSource = GetEmployeeAssignment(GetValueParamText("DEFAULT COMPANY"), cmbBranch.SelectedValue)

            gridAll2.Visible = False
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmbCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "COMPANY"
                    LoadCompany(senderCmb, gridAll, 1)
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

    Private Sub cmbBranch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBranch.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "BRANCH"
                    LoadBranch(senderCmb, gridAll2, 1)
            End Select


            gridAll2.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll2.Size = New Point(GetColumnWidth(gridAll2.Columns.Count, gridAll2) + _
                                     (senderCmb.Width - GetColumnWidth(gridAll2.Columns.Count, gridAll2)) + 60, _
                                     GetRowHeight(gridAll2.Rows.Count, gridAll2))
            senderCmb.DroppedDown = False

            If gridAll2.Visible = True Then
                gridAll2.Visible = False
            Else
                If gridAll2.RowCount > 0 Then gridAll2.Visible = True
            End If

            gridAll2.Tag = senderCmb.Tag

            gridAll2.Columns(0).Width = 50
            gridAll2.Columns(1).Width = gridAll2.Width - 54

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub


    Private Sub cmbBranch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBranch.SelectedIndexChanged

    End Sub

    Private Sub GridEmpCatalog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridEmpCatalog.CellContentClick

    End Sub
End Class