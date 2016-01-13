Imports genLib.General
Imports mainlib
Imports proLib.Process
Imports sqlLib.Sql
Imports POS.StackedHeader

Public Class frmApproval

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim objREnderer As New StackedHeaderDecorator(GridDocument)
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadBranch(cmbBranch, gridAll, 0)
        LoadTransaction(cmbTransaction, gridAll, 0)

        cmbBranch.SelectedValue = GetValueParamText("DEFAULT BRANCH")

    End Sub


    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnPosting.Image = mainClass.imgList.ImgBtnPosting

        btnValidate.Image = mainClass.imgList.ImgBtnValidate

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picTitle.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

 
        Select Case gridAll.Tag
            Case "BRANCH"
                cmbBranch.SelectedValue = gridAll.SelectedCells(0).Value
            Case Else
                cmbTransaction.SelectedValue = gridAll.SelectedCells(0).Value
        End Select


        gridAll.Visible = False

    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub

    Private Sub RefreshData()
        Try
            Me.Cursor = Cursors.WaitCursor
            With GridDocument
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "No"
                .Columns(1).DataPropertyName = "Document No"
                .Columns(2).DataPropertyName = "Date"
                .Columns(3).DataPropertyName = "Supp Code"
                .Columns(4).DataPropertyName = "Supp Name"
                .Columns(5).DataPropertyName = "DN"
                .Columns(6).DataPropertyName = "Note"
                .Columns(7).DataPropertyName = "Validate"
                .Columns(8).DataPropertyName = "Posting"
                .Columns(9).DataPropertyName = "TransID"
                .Columns(10).DataPropertyName = "WHCode"
            End With
            GridDocument.DataSource = GETDOCUMENTSTATUS(cmbBranch.SelectedValue, cmbTransaction.SelectedValue)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click

        Try
            If GridDocument.Rows.Count > 0 And Not GridDocument.SelectedCells(0).Value Is Nothing Then

                If MsgBox("Are you sure?", MsgBoxStyle.YesNo + MsgBoxStyle.Information, Title) = MsgBoxResult.No Then Exit Sub

                If cmbTransaction.SelectedValue = "GR102" Or cmbTransaction.SelectedValue = "GR101" Or cmbTransaction.SelectedValue = "TR100" Then
                    If btnValidate.Text = "Validate" Then
                        ValidateBM(GridDocument.SelectedCells(1).Value, 1)
                    Else
                        ValidateBM(GridDocument.SelectedCells(1).Value, 0)
                    End If

                End If

                RefreshData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub GridDocument_SelectionChanged(sender As Object, e As System.EventArgs) Handles GridDocument.SelectionChanged
        If GridDocument.RowCount > 0 Then
            If GridDocument.Rows(GridDocument.CurrentRow.Index).Cells(7).Value = "Y" Then
                btnValidate.Text = "Abort"
                btnPosting.Enabled = True
            Else
                btnValidate.Text = "Validate"
                btnPosting.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosting.Click

        Try
            If GridDocument.Rows.Count > 0 And Not GridDocument.SelectedCells(0).Value Is Nothing Then

                If MsgBox("Are you sure?", MsgBoxStyle.YesNo + MsgBoxStyle.Information, Title) = MsgBoxResult.No Then Exit Sub

                If cmbTransaction.SelectedValue = "GR102" Or cmbTransaction.SelectedValue = "GR101" Or cmbTransaction.SelectedValue = "TR100" Then

                    PostingBM(GridDocument.SelectedCells(1).Value, GridDocument.SelectedCells(10).Value, _
                               logOn)

                    MsgBox("Posting " & Trim(GridDocument.SelectedCells(1).Value) & " Finish", MsgBoxStyle.Information, Title)
                End If

                RefreshData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub cmbBranch_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBranch.DropDown, cmbTransaction.DropDown
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "BRANCH"
                    LoadBranch(senderCmb, gridAll, 1)
                Case Else
                    LoadTransaction(senderCmb, gridAll, 1)
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

    Private Sub gridAll_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.LostFocus
        If gridAll.Visible = True Then

            Select Case gridAll.Tag
                Case "BRANCH"
                    If Not cmbBranch.Focused = True Then gridAll.Visible = False

                Case Else

                    If Not cmbTransaction.Focused = True Then gridAll.Visible = False

            End Select



        End If
    End Sub

End Class