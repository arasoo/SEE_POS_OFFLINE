Imports genLib.General
Imports mainlib
Imports sqlLib.Sql
Imports proLib.Process

Public Class frmVoucher

    Private firstLoad As Boolean = False

    Private Sub frmMasterEvent_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F1 Then
            NewVoucher()

        ElseIf e.KeyCode = Keys.Escape Then
            Close()
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        NewVoucher()
    End Sub

    Private Sub NewVoucher()
        Dim documentno As String = GetLastTransNo("VC")
        Dim fVoucher As frmVoucherProperties = New frmVoucherProperties

        fVoucher.VoucherTitle = "New Voucher"
        fVoucher.VoucherID = documentno
        fVoucher.SalesOrg = cmbSalesOrg.SelectedValue
        fVoucher.VoucherState = 0

        If fVoucher.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData(cmbStatus.SelectedIndex)
            fVoucher.Close()
        End If
    End Sub

    Private Sub RefreshData(ByVal state As Integer)
        Try
            table = New DataTable

            table = GetVoucherHeader(state, cmbSalesOrg.SelectedValue)

            With GridVoucherHeader
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "voucherid"
                .Columns(1).DataPropertyName = "description"
                .Columns(2).DataPropertyName = "note"
                .Columns(3).DataPropertyName = "periodfrom"
                .Columns(4).DataPropertyName = "periodto"
                .Columns(5).DataPropertyName = "type"
                .Columns(6).DataPropertyName = "validflag"
                .Columns(7).DataPropertyName = "closeflag"
                .Columns(8).DataPropertyName = "createuser"
                .Columns(9).DataPropertyName = "createdate"
            End With

            GridVoucherHeader.DataSource = table

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        cmbSalesOrg.SelectedValue = gridAll.SelectedCells(0).Value

        RefreshData(cmbStatus.SelectedIndex)
        If GridVoucherHeader.Rows.Count > 0 Then
            RefresDataDetail()
        Else
            gridVoucherDetail.DataSource = Nothing
        End If

        gridAll.Visible = False
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click


        frmEvent.EventTitle = "Modify Voucher"
        frmEvent.EventNo = Trim(GridVoucherHeader.SelectedCells(0).Value)
        frmEvent.EventState = 1

        If frmEvent.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData(cmbStatus.SelectedIndex)
        End If
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        If Not firstLoad = False Then
            RefreshData(cmbStatus.SelectedIndex)
            If GridVoucherHeader.Rows.Count > 0 Then
                RefresDataDetail()
            Else
                gridVoucherDetail.DataSource = Nothing
            End If

        End If
    End Sub

    Private Sub cmbSalesOrg_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSalesOrg.DropDown
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

    Private Sub GridVoucherHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridVoucherHeader.Click

        If GridVoucherHeader.RowCount > 0 Then
            RefresDataDetail()
        End If

        If GridVoucherHeader.Rows.Count > 0 Then
            If GridVoucherHeader.SelectedCells(6).Value = "Y" Then
                btnValidate.Enabled = False
            Else
                btnValidate.Enabled = True
            End If

            If GridVoucherHeader.SelectedCells(7).Value = "Y" Then
                btnAbort.Enabled = False
            Else
                btnAbort.Enabled = True
            End If
        End If

    End Sub

    Private Sub RefresDataDetail()
        Try

            table = New DataTable

            table = GetVoucherDetail(GridVoucherHeader.SelectedCells(0).Value)

            With gridVoucherDetail
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "iden"
                .Columns(1).DataPropertyName = "vouchercode"
                .Columns(2).DataPropertyName = "invoice"
                .Columns(3).DataPropertyName = "amount"
                .Columns(4).DataPropertyName = "used_at"
                .Columns(5).DataPropertyName = "employeeid"
            End With

            gridVoucherDetail.DataSource = table

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If Not GridVoucherHeader.Rows.Count > 0 Then
            MsgBox("No Data", MsgBoxStyle.Information, Title)
        Else
            If MsgBox("Are you sure validate this voucher id " & Trim(GridVoucherHeader.SelectedCells(0).Value) & " ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            ValidateVoucher(Trim(GridVoucherHeader.SelectedCells(0).Value))
            RefreshData(cmbStatus.SelectedIndex)
            RefresDataDetail()

        End If
    End Sub

    Private Sub btnAbort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbort.Click
        If Not GridVoucherHeader.Rows.Count > 0 Then
            MsgBox("No Data", MsgBoxStyle.Information, Title)
        Else
            If MsgBox("Are you sure close this voucher id " & Trim(GridVoucherHeader.SelectedCells(0).Value) & " ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            CloseVoucher(Trim(GridVoucherHeader.SelectedCells(0).Value))
            RefreshData(cmbStatus.SelectedIndex)
            RefresDataDetail()

        End If
    End Sub

    Private Sub frmVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadImage()
        LoadSalesOrg(cmbSalesOrg, gridAll, 0)

        RefreshData(cmbStatus.SelectedIndex)

        If GridVoucherHeader.Rows.Count > 0 Then
            RefresDataDetail()
            firstLoad = True
        End If
    End Sub

End Class