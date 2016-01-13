
Imports genLib.General
Imports connLib.DBConnection
Imports secuLib.Security
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports prolib.Process
Imports saveLib.Save
Imports iniLib.Ini
Imports sqlLib.Sql
Imports System.IO
Imports mainLib

Public Class FrmParameter
    Private table As DataTable
    Private mstate As Integer

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            For t As Integer = 0 To TabControl1.TabPages.Count - 1
                For Each c As Control In TabControl1.TabPages(t).Controls
                    If TypeOf c Is ComboBox Then
                        If c.Tag <> "" Then

                            If c.Tag = "SYSTEM SQL" Then
                                SaveParam(c.Tag, CType(c, ComboBox).SelectedIndex)
                            Else
                                SaveParam(c.Tag, CType(c, ComboBox).SelectedValue)
                            End If


                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is TextBox Then
                        If c.Tag <> "" Then

                            If c.Tag = "PASS REPRINT" Then
                                SaveParam(c.Tag, encryptString(CType(c, TextBox).Text))
                            Else

                                SaveParam(c.Tag, CType(c, TextBox).Text)
                            End If




                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is CheckBox Then
                        If c.Tag <> "" Then
                            SaveParam(c.Tag, CType(c, CheckBox).CheckState)


                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is DateTimePicker Then
                        If c.Tag <> "" Then
                            SaveParam(c.Tag, Format(CType(c, DateTimePicker).Value, "yyyy-MM-dd"))
                            If c.Tag = "SYSTEM DATE" Then
                                MDIMain.statusPeriod.Text = "  " & Format(dtDNDate.Value, "dd MMMM yyyy")
                            End If

                        End If
                        GoTo lanjut
                    End If
lanjut:
                Next
            Next

            INIWrite(srcPath, ProjectID, "PosPrinter", encryptString(Trim(txtPosPrinter.Text)))
            posPrinter = Trim(txtPosPrinter.Text)

            tblParam = New DataTable
            tblParam = GetParameter()
       
            Me.Cursor = Cursors.Default
            MsgBox("Parameter is up to date", MsgBoxStyle.Information, Title)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub SaveParam(ByVal kode As String, ByVal value As String)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            With cm
                .Connection = cn
                .CommandText = "UPDATE " & DB & ".dbo.mparam SET Param_Value='" & value & "'" & _
                                " WHERE Param_Description='" & kode & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FrmParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadImage()
            LoadCompany(cmbCompany, gridCompanyProfile, 0)
            LoadBranch(cmbBranch, gridCompanyProfile, 0)
            LoadWarehouse(cmbWarehouse, gridCompanyProfile, 0)
            LoadTaxOrg(cmbTaxOrg, gridCompanyProfile, 0)
            LoadCustomer(cmbCustomerPOS, gridPOS, 0)
            LoadSalesOrg(cmbSalesOrgPOS, gridPOS, 0)
            LoadSalesman(cmbSalesman, gridPOS, 0)
            LoadCostCenter(cmbCostCenter, gridPOS, 0)
            LoadSalesOffice(cmbSalesOffice, gridPOS, 0)

            LoadCustomer(cmbPamCustomer, gridPAM, 0)
            LoadSalesOrg(cmbPamSlsOrg, gridPAM, 0)
            LoadSalesman(cmbPamSalesman, gridPAM, 0)
            LoadCostCenter(cmbPamCostCenter, gridPAM, 0)
            LoadSalesOffice(cmbPamSalesOffice, gridPAM, 0)
            LoadWarehousePAM(cmbPamWarehouse, gridPAM, 0)

            LoadCurrency(cmbCurrency, gridPOS, 0)
            LoadCard(cmbVoucherCardType)
            LoadEDC(cmbVoucherEDC)
            LoadProductGroup(cmbProduct, gridPOS, 0)

            RefreshData()

            txtPosPrinter.Text = decryptString(INIRead(srcPath, ProjectID, "PosPrinter", ""))

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub LoadImage()

        btnSave.Image = mainClass.imgList.ImgBtnSave

        picTitle.Image = mainClass.imgList.ImgLabelParameter

        btnClose.Image = mainClass.imgList.ImgBtnClosing

    End Sub

    Private Sub RefreshData()
        Try
            For t As Integer = 0 To TabControl1.TabPages.Count - 1
                For Each c As Control In TabControl1.TabPages(t).Controls
                    If TypeOf c Is ComboBox Then
                        If c.Tag <> "" Then
                            For i As Integer = 0 To tblParam.Rows.Count - 1
                                If c.Tag = tblParam.Rows(i).Item("Param_Description") Then

                                    If c.Tag = "SYSTEM SQL" Then
                                        CType(c, ComboBox).SelectedIndex = tblParam.Rows(i).Item("Param_Value")
                                    Else
                                        CType(c, ComboBox).SelectedValue = tblParam.Rows(i).Item("Param_Value")
                                    End If

                                End If
                            Next

                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is TextBox Then
                        If c.Tag <> "" Then
                            For i As Integer = 0 To tblParam.Rows.Count - 1
                                If c.Tag = tblParam.Rows(i).Item("Param_Description") Then
                                    If tblParam.Rows(i).Item("Param_Value_Type") = "M" Then
                                        CType(c, TextBox).Text = String.Format("{0:#,##0}", CDec(tblParam.Rows(i).Item("Param_Value")))
                                    End If

                                    If tblParam.Rows(i).Item("Param_Value_Type") = "N" Then
                                        CType(c, TextBox).Text = CInt(tblParam.Rows(i).Item("Param_Value"))
                                    End If

                                    If tblParam.Rows(i).Item("Param_Value_Type") = "T" Then
                                        If c.Tag = "PASS REPRINT" Then
                                            CType(c, TextBox).Text = decryptString(CStr(tblParam.Rows(i).Item("Param_Value")))
                                        Else
                                            CType(c, TextBox).Text = CStr(tblParam.Rows(i).Item("Param_Value"))
                                        End If

                                    End If

                                End If
                            Next

                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is CheckBox Then
                        If c.Tag <> "" Then
                            For i As Integer = 0 To tblParam.Rows.Count - 1
                                If c.Tag = tblParam.Rows(i).Item("Param_Description") Then
                                    CType(c, CheckBox).CheckState = CInt(tblParam.Rows(i).Item("Param_Value"))
                                End If
                            Next

                        End If
                        GoTo lanjut
                    End If

                    If TypeOf c Is DateTimePicker Then
                        If c.Tag <> "" Then
                            For i As Integer = 0 To tblParam.Rows.Count - 1
                                If c.Tag = tblParam.Rows(i).Item("Param_Description") Then
                                    CType(c, DateTimePicker).Value = CDate(tblParam.Rows(i).Item("Param_Value"))
                                End If
                            Next

                        End If
                        GoTo lanjut
                    End If
lanjut:
                Next
            Next

        Catch ex As Exception

            Throw ex
        End Try
    End Sub

    'Private Sub FrmParameter_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
    '    Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
    '    Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
    '    Dim colors As Color() = {Color.White, Color.White, Color.Khaki, Color.Khaki}
    '    Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
    '    Dim blend As New ColorBlend
    '    blend.Colors = colors
    '    blend.Positions = positions
    '    Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
    '        lgb.InterpolationColors = blend
    '        e.Graphics.FillRectangle(lgb, bounds)
    '    End Using
    'End Sub

    Private Sub chckAutoGenerate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckAutoGenerate.CheckedChanged
        If chckAutoGenerate.Checked = True Then
            chckAutoGenerate.ForeColor = Color.Green
            chckAutoGenerate.Text = "ON"
        Else
            chckAutoGenerate.ForeColor = Color.Red
            chckAutoGenerate.Text = "OFF"
        End If
    End Sub

    Private Sub chckStockMinus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckStockMinus.CheckedChanged
        If chckStockMinus.Checked = True Then
            chckStockMinus.ForeColor = Color.Green
            chckStockMinus.Text = "ON"
        Else
            chckStockMinus.ForeColor = Color.Red
            chckStockMinus.Text = "OFF"
        End If
    End Sub

    Private Sub gridPOS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridPOS.DoubleClick

        Select Case gridPOS.Tag
            Case "POS CUSTOMER"
                cmbCustomerPOS.SelectedValue = gridPOS.SelectedCells(0).Value
            Case "POS SLSORG"
                cmbSalesOrgPOS.SelectedValue = gridPOS.SelectedCells(0).Value
            Case "POS COSTCENTER"
                cmbCostCenter.SelectedValue = gridPOS.SelectedCells(0).Value
            Case "POS SALESOFFICE"
                cmbSalesOffice.SelectedValue = gridPOS.SelectedCells(0).Value
            Case "PRODUCT BEST PRICE"
                cmbProduct.SelectedValue = gridPOS.SelectedCells(0).Value
            Case "CURRENCY"
                cmbCurrency.SelectedValue = gridPOS.SelectedCells(0).Value
            Case Else
                cmbSalesman.SelectedValue = gridPOS.SelectedCells(0).Value
        End Select

        gridPOS.Visible = False
    End Sub

    Private Sub gridPAM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridPAM.DoubleClick

        Select Case gridPAM.Tag
            Case "PAM CUSTOMER"
                cmbPamCustomer.SelectedValue = gridPAM.SelectedCells(0).Value
            Case "PAM SLSORG"
                cmbPamSlsOrg.SelectedValue = gridPAM.SelectedCells(0).Value
            Case "PAM COSTCENTER"
                cmbPamCostCenter.SelectedValue = gridPAM.SelectedCells(0).Value
            Case "PAM SALESOFFICE"
                cmbPamSalesOffice.SelectedValue = gridPAM.SelectedCells(0).Value
            Case "PAM WAREHOUSE"
                cmbPamWarehouse.SelectedValue = gridPAM.SelectedCells(0).Value
            Case Else
                cmbPamSalesman.SelectedValue = gridPAM.SelectedCells(0).Value
        End Select

        gridPAM.Visible = False
    End Sub

    Private Sub gridCompanyProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridCompanyProfile.DoubleClick

        Select Case gridCompanyProfile.Tag
            Case "DEFAULT COMPANY"
                cmbCompany.SelectedValue = gridCompanyProfile.SelectedCells(0).Value
            Case "DEFAULT BRANCH"
                cmbBranch.SelectedValue = gridCompanyProfile.SelectedCells(0).Value
            Case "DEFAULT WH"
                cmbWarehouse.SelectedValue = gridCompanyProfile.SelectedCells(0).Value
            Case Else
                cmbTaxOrg.SelectedValue = gridCompanyProfile.SelectedCells(0).Value

                table = New DataTable

                table = GETDetailTaxOrg(cmbTaxOrg.SelectedValue)

                txtNPWPNo.Text = table.Rows(0).Item(0)
                txtNPWPName.Text = table.Rows(0).Item(1)
                txtNPWPAddress.Text = table.Rows(0).Item(2)
                txtNPWPCity.Text = table.Rows(0).Item(3)

        End Select

        gridCompanyProfile.Visible = False
    End Sub

    Private Sub cmbCustomerPOS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomerPOS.Click, cmbSalesOrgPOS.Click, _
                                                                                                  cmbCostCenter.Click, cmbSalesOffice.Click, _
                                                                                                  cmbProduct.Click, cmbCurrency.Click, cmbSalesman.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "POS CUSTOMER"
                    LoadCustomer(senderCmb, gridPOS, 1)
                Case "POS SLSORG"
                    LoadSalesOrg(senderCmb, gridPOS, 1)
                Case "POS COSTCENTER"
                    LoadCostCenter(senderCmb, gridPOS, 1)
                Case "POS SALESOFFICE"
                    LoadSalesOffice(senderCmb, gridPOS, 1)
                Case "PRODUCT BEST PRICE"
                    LoadProductGroup(senderCmb, gridPOS, 1)
                Case "CURRENCY"
                    LoadCurrency(senderCmb, gridPOS, 1)
                Case Else
                    LoadSalesman(senderCmb, gridPOS, 1)
            End Select

            gridPOS.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridPOS.Size = New Point(GetColumnWidth(gridPOS.Columns.Count, gridPOS) + _
                            (senderCmb.Width - GetColumnWidth(gridPOS.Columns.Count, gridPOS)), GetRowHeight(gridPOS.Rows.Count, gridPOS))
            senderCmb.DroppedDown = False


            If gridPOS.Visible = True Then
                gridPOS.Visible = False
            Else
                If gridPOS.RowCount > 0 Then gridPOS.Visible = True
            End If

            gridPOS.Tag = senderCmb.Tag

            If senderCmb.Tag = "POS CUSTOMER" Then
                gridPOS.Columns(0).Width = 70
            Else
                gridPOS.Columns(0).Width = 50
            End If

            gridPOS.Columns(1).Width = gridPOS.Width - 54
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmbPAM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPamCustomer.Click, cmbPamSlsOrg.Click, _
                                                                                                 cmbPamCostCenter.Click, cmbPamSalesOffice.Click, _
                                                                                                 cmbPamSalesman.Click, cmbPamWarehouse.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "PAM CUSTOMER"
                    LoadCustomer(senderCmb, gridPAM, 1)
                Case "PAM SLSORG"
                    LoadSalesOrg(senderCmb, gridPAM, 1)
                Case "PAM COSTCENTER"
                    LoadCostCenter(senderCmb, gridPAM, 1)
                Case "PAM SALESOFFICE"
                    LoadSalesOffice(senderCmb, gridPAM, 1)
                Case "PAM WAREHOUSE"
                    LoadWarehousePAM(senderCmb, gridPAM, 1)
                Case Else
                    LoadSalesman(senderCmb, gridPAM, 1)
            End Select

            gridPAM.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridPAM.Size = New Point(GetColumnWidth(gridPAM.Columns.Count, gridPAM) + _
                            (senderCmb.Width - GetColumnWidth(gridPAM.Columns.Count, gridPAM)), GetRowHeight(gridPAM.Rows.Count, gridPAM))
            senderCmb.DroppedDown = False


            If gridPAM.Visible = True Then
                gridPAM.Visible = False
            Else
                If gridPAM.RowCount > 0 Then gridPAM.Visible = True
            End If

            gridPAM.Tag = senderCmb.Tag

            If senderCmb.Tag = "PAM CUSTOMER" Then
                gridPAM.Columns(0).Width = 70
            Else
                gridPAM.Columns(0).Width = 50
            End If

            gridPAM.Columns(1).Width = gridPAM.Width - 54
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmbCompanyProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbWarehouse.Click, cmbBranch.Click, cmbCompany.Click, cmbTaxOrg.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)


            Select Case senderCmb.Tag
                Case "DEFAULT WH"
                    LoadWarehouse(senderCmb, gridCompanyProfile, 1)
                Case "DEFAULT BRANCH"
                    LoadBranch(senderCmb, gridCompanyProfile, 1)
                Case "DEFAULT COMPANY"
                    LoadCompany(senderCmb, gridCompanyProfile, 1)
                Case Else
                    LoadTaxOrg(senderCmb, gridCompanyProfile, 1)
            End Select

            gridCompanyProfile.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridCompanyProfile.Size = New Point(GetColumnWidth(gridCompanyProfile.Columns.Count, gridCompanyProfile) _
                                    + (senderCmb.Width - GetColumnWidth(gridCompanyProfile.Columns.Count, gridCompanyProfile)), _
                                    GetRowHeight(gridCompanyProfile.Rows.Count, gridCompanyProfile))

            senderCmb.DroppedDown = False

            If gridCompanyProfile.Visible = True Then
                gridCompanyProfile.Visible = False
            Else
                If gridCompanyProfile.RowCount > 0 Then gridCompanyProfile.Visible = True
            End If

            gridCompanyProfile.Tag = senderCmb.Tag

            gridCompanyProfile.Columns(0).Width = 50
            gridCompanyProfile.Columns(1).Width = 230


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    'Private Sub cmbCurrency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCurrency.Click
    '    Try

    '        Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)


    '        Select Case senderCmb.Tag


    '        End Select

    '        gridGeneral.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
    '        gridGeneral.Size = New Point(GetColumnWidth(gridGeneral.Columns.Count, gridGeneral) _
    '                                + (senderCmb.Width - GetColumnWidth(gridGeneral.Columns.Count, gridGeneral)), _
    '                                GetRowHeight(gridGeneral.Rows.Count, gridGeneral))

    '        senderCmb.DroppedDown = False

    '        If gridGeneral.Visible = True Then
    '            gridGeneral.Visible = False
    '        Else
    '            If gridGeneral.RowCount > 0 Then gridGeneral.Visible = True
    '        End If

    '        gridGeneral.Tag = senderCmb.Tag

    '        gridGeneral.Columns(0).Width = 50
    '        gridGeneral.Columns(1).Width = 230


    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
    '    End Try
    'End Sub

    Private Sub chckBestPrice_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckBestPrice.CheckedChanged
        If chckBestPrice.Checked = True Then
            chckBestPrice.ForeColor = Color.Green
            chckBestPrice.Text = "ON"
            txtMinMargin.Enabled = True
            txtMaxDisc.Enabled = True
            txtMaxItem.Enabled = True

        Else
            chckBestPrice.ForeColor = Color.Red
            chckBestPrice.Text = "OFF"
            txtMinMargin.Enabled = False
            txtMaxDisc.Enabled = False
            txtMaxItem.Enabled = False
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub chckActivateVouchers_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chckActivateVouchers.CheckedChanged
        If chckActivateVouchers.Checked = True Then
            chckActivateVouchers.ForeColor = Color.Green
            chckActivateVouchers.Text = "ON"
        Else
            chckActivateVouchers.ForeColor = Color.Red
            chckActivateVouchers.Text = "OFF"
        End If
    End Sub

    Private Sub chckVoucherManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckVoucherManual.CheckedChanged
        If chckVoucherManual.Checked = True Then
            chckVoucherManual.ForeColor = Color.Green
            chckVoucherManual.Text = "ON"
        Else
            chckVoucherManual.ForeColor = Color.Red
            chckVoucherManual.Text = "OFF"
        End If
    End Sub

    Private Sub chckMemberShip_CheckedChanged(sender As Object, e As EventArgs) Handles chckMemberShip.CheckedChanged
        If chckMemberShip.Checked = True Then
            chckMemberShip.ForeColor = Color.Green
            chckMemberShip.Text = "ON"
            chckBooksObral.Enabled = True
            txtPoinAmount.Enabled = True
         

        Else
            chckMemberShip.ForeColor = Color.Red
            chckMemberShip.Text = "OFF"
            txtPoinAmount.Enabled = False
            chckBooksObral.Enabled = False
        End If
    End Sub

    Private Sub chckAutoPosting_CheckedChanged(sender As Object, e As EventArgs) Handles chckAutoPosting.CheckedChanged
        If chckAutoPosting.Checked = True Then
            chckAutoPosting.ForeColor = Color.Green
            chckAutoPosting.Text = "ON"
        Else
            chckAutoPosting.ForeColor = Color.Red
            chckAutoPosting.Text = "OFF"
        End If
    End Sub
End Class