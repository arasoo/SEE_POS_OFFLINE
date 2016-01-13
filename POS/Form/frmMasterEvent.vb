Imports genLib.General
Imports mainlib
Imports sqlLib.Sql
Imports proLib.Process

Public Class frmMasterEvent

    Private firstLoad As Boolean = False

    Private Sub frmMasterEvent_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F1 Then
            NewPromo()

        ElseIf e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub frmEvent_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadImage()
        LoadSalesOrg(cmbSalesOrg, gridAll, 0)

        RefreshData(cmbStatus.SelectedIndex)

        If GridEventHeader.Rows.Count > 0 Then
            RefresDataDetail()
            firstLoad = True
        End If

    End Sub

    Private Sub loadImage()
        btnValidate.Image = mainClass.imgList.ImgBtnValidate
        btnEdit.Image = mainClass.imgList.ImgBtnEdit
        btnNew.Image = mainClass.imgList.ImgBtnNew
        btnAbort.Image = mainClass.imgList.ImgBtnVoid
        btnClose.Image = mainClass.imgList.ImgBtnClosing
        picTitle.Image = mainClass.imgList.ImgLabelReporting
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        NewPromo()
    End Sub

    Private Sub NewPromo()
        Dim documentno As String = GetLastTransNo("MP")
        Dim fEvent As frmEvent = New frmEvent

        fEvent.EventTitle = "New Event Promo"
        fEvent.EventNo = documentno
        fEvent.EventState = 0

        If fEvent.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData(cmbStatus.SelectedIndex)
            fEvent.Close()
        End If
    End Sub

    Private Sub RefreshData(state As Integer)
        Try


            table = New DataTable

            table = GetPromoHeader(state, cmbSalesOrg.SelectedValue)

            With GridEventHeader
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "promoid"
                .Columns(1).DataPropertyName = "description"
                .Columns(2).DataPropertyName = "note"
                .Columns(3).DataPropertyName = "disctype"
                .Columns(4).DataPropertyName = "assignto"
                .Columns(5).DataPropertyName = "periodfrom"
                .Columns(6).DataPropertyName = "periodto"
                .Columns(7).DataPropertyName = "prodhier"
                .Columns(8).DataPropertyName = "validflag"
                .Columns(9).DataPropertyName = "closeflag"
                .Columns(10).DataPropertyName = "autogenerate"
                .Columns(11).DataPropertyName = "minpayment"
                .Columns(12).DataPropertyName = "createuser"
                .Columns(13).DataPropertyName = "createdate"
            End With

            GridEventHeader.DataSource = table

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        cmbSalesOrg.SelectedValue = gridAll.SelectedCells(0).Value

        RefreshData(cmbStatus.SelectedIndex)
        If GridEventHeader.Rows.Count > 0 Then
            RefresDataDetail()
        Else
            gridEventDetail.DataSource = Nothing
        End If

        gridAll.Visible = False
    End Sub

    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click


        frmEvent.EventTitle = "Modify Event Promo"
        frmEvent.EventNo = Trim(GridEventHeader.SelectedCells(0).Value)
        frmEvent.EventState = 1

        If frmEvent.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData(cmbStatus.SelectedIndex)
        End If
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        If Not firstLoad = False Then
            RefreshData(cmbStatus.SelectedIndex)
            If GridEventHeader.Rows.Count > 0 Then
                RefresDataDetail()
            Else
                gridEventDetail.DataSource = Nothing
            End If

        End If
    End Sub

    Private Sub cmbSalesOrg_DropDown(sender As Object, e As System.EventArgs) Handles cmbSalesOrg.DropDown
        Try


            LoadSalesOrg(cmbSalesOrg, gridAll, 1)


            gridAll.Location = New Point(cmbSalesOrg.Left, cmbSalesOrg.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                            (cmbSalesOrg.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)), GetRowHeight(gridAll.Rows.Count, gridAll))
            cmbSalesOrg.DroppedDown = False


            If gridAll.Visible = True Then
                gridAll.Visible = False
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
            End If

            gridAll.Tag = cmbSalesOrg.Tag

            gridAll.Columns(0).Width = 50


            gridAll.Columns(1).Width = gridAll.Width - 54
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub GridEventHeader_Click(sender As Object, e As System.EventArgs) Handles GridEventHeader.Click

        If GridEventHeader.RowCount > 0 Then
            RefresDataDetail()
        End If

        If GridEventHeader.Rows.Count > 0 Then
            If GridEventHeader.SelectedCells(8).Value = "Y" Then
                btnValidate.Enabled = False
            Else
                btnValidate.Enabled = True
            End If

            If GridEventHeader.SelectedCells(9).Value = "Y" Then
                btnAbort.Enabled = False
            Else
                btnAbort.Enabled = True
            End If
        End If

    End Sub

    Private Sub RefresDataDetail()
        Try

            table = New DataTable

            table = GetPromoDetail(GridEventHeader.SelectedCells(0).Value)

            With gridEventDetail
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "item"
                .Columns(1).DataPropertyName = "partnumber"
                .Columns(2).DataPropertyName = "description"
                .Columns(3).DataPropertyName = "disc"
            End With

            gridEventDetail.DataSource = table

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnValidate_Click(sender As System.Object, e As System.EventArgs) Handles btnValidate.Click
        If Not GridEventHeader.Rows.Count > 0 Then
            MsgBox("No Data", MsgBoxStyle.Information, Title)
        Else
            If MsgBox("Are you sure validate this promo id " & GridEventHeader.SelectedCells(0).Value & " ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            ValidatePromo(GridEventHeader.SelectedCells(0).Value)
            RefreshData(cmbStatus.SelectedIndex)
            RefresDataDetail()

        End If
    End Sub

    Private Sub btnAbort_Click(sender As System.Object, e As System.EventArgs) Handles btnAbort.Click
        If Not GridEventHeader.Rows.Count > 0 Then
            MsgBox("No Data", MsgBoxStyle.Information, Title)
        Else
            If MsgBox("Are you sure close this promo id " & GridEventHeader.SelectedCells(0).Value & " ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            ClosePromo(GridEventHeader.SelectedCells(0).Value)
            RefreshData(cmbStatus.SelectedIndex)
            RefresDataDetail()

        End If
    End Sub
End Class