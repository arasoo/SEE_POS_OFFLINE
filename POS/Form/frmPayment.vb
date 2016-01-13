Imports System.IO
Imports System.Drawing.Drawing2D
Imports connLib.DBConnection
Imports genLib.General
Imports mainlib
Imports proLib.Process
Imports sqlLib.Sql

Public Class frmPayment

    Private mSubTotal As Decimal
    Private card(10) As ArrayList
    Private mpayAmt As Decimal
    Private mcharge As Decimal = 0
    Private table As DataTable
    Public cardState As Integer
    Public cashState As Integer
    Public voucherState As Integer
    Public tableVoucher As DataTable
    Private firstLoad As Boolean = False
    Private voucherAmt As Decimal = 0

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Public WriteOnly Property SubTotal As Decimal
        Set(ByVal value As Decimal)
            mSubTotal = value
        End Set
    End Property

    Public WriteOnly Property PaymentAmount As Decimal
        Set(ByVal value As Decimal)
            mpayAmt = value
        End Set
    End Property

    Private Sub Change()

        If CDec(lblChange.Text) < 0 Then
            lblChange.ForeColor = Color.White
        Else
            lblChange.ForeColor = Color.White
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub

    Private Sub frmPayment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                PaymentCash()
            ElseIf e.KeyCode = Keys.F2 Then
                PaymentCard()

            ElseIf e.KeyCode = Keys.F3 Then
                If GetValueParamNumber("ACTIVATE VOUCHER COUPON") = 0 Then
                    txtVoucherCode.Enabled = False

                    MsgBox("Please activate voucher coupon in parameter first!", MsgBoxStyle.Exclamation, Title)
                    voucherGroupBox.Enabled = False
                    btnVoucher.BackColor = Color.SteelBlue
                    btnVoucher.ForeColor = Color.White
                    voucherState = 0
                    txtVoucherAmount.Text = String.Format("{0:#,##0}", 0)

                    txtVoucherCode.Text = ""

                    Exit Sub

                Else

                    If GetValueParamNumber("VOUCHER MANUAL") = 0 Then
                        txtVoucherAmount.ReadOnly = True
                    Else
                        txtVoucherAmount.ReadOnly = False
                    End If

                    PaymentVoucher()

                End If
            ElseIf e.KeyCode = Keys.F12 Then
                SavePayment()

            ElseIf e.KeyCode = Keys.Escape Then

                Close()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal)
        LoadCard(cmbCardType)
        LoadEDC(cmbEDC)
        Change()
        cardState = 0
        cashState = 0
        voucherState = 0
        firstLoad = True
        CreateTableVoucher()
        CalculatePaid()
        CalculateChange()
    End Sub

    Private Sub CreateTableVoucher()
        tableVoucher = New DataTable

        With tableVoucher.Columns
            .Add("Id", GetType(String))
            .Add("Voucher", GetType(String))
            .Add("Amount", GetType(Decimal))
        End With

    End Sub

    Private Sub LoadImage()

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        btnCash.Image = mainClass.imgList.ImgBtnCash

        btnCard.Image = mainClass.imgList.ImgBtnCard

        btnVoucher.Image = mainClass.imgList.ImgBtnEdit

    End Sub

    Private Sub CalculatePaid()
        Dim total As Decimal = CDec(txtVoucherAmount.Text) + CDec(txtCardAmount.Text) + CDec(txtCashAmount.Text) + CDec(txtCharge.Text)
        'lblPaid.Text = FormatCurrency(CDec(txtCardAmount.Text) + CDec(txtCashAmount.Text) + CDec(txtCharge.Text), 0)
        lblPaid.Text = String.Format("{0:#,##0}", total)

    End Sub

    Private Sub CalculateChange()
        Dim total As Decimal = CDec(lblSubTotal.Text) - CDec(lblPaid.Text)
        lblChange.Text = String.Format("{0:#,##0}", total)

        'lblChange.Text = FormatCurrency(total, 0, , TriState.False)
        Change()
    End Sub

    Private Sub btnCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCard.Click
        Try
            PaymentCard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub btnCash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCash.Click
        Try
            PaymentCash()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub PaymentCash()
        If cashState = 0 Then
            If cardState = 1 And Trim(txtCardAmount.Text) = 0 Then
                cardGroupBox.Enabled = False
                btnCard.BackColor = Color.steelblue
                btnCard.ForeColor = Color.White
                cardState = 0
            End If

            If voucherState = 1 And Trim(txtVoucherAmount.Text) = 0 Then
                voucherGroupBox.Enabled = False
                btnVoucher.BackColor = Color.steelblue
                btnVoucher.ForeColor = Color.White
                voucherState = 0
            End If

            cashGroupBox.Enabled = True
            btnCash.BackColor = Color.Red
            btnCash.ForeColor = Color.White
            cashState = 1
            txtCashAmount.Focus()
        Else
            cashGroupBox.Enabled = False
            btnCash.BackColor = Color.steelblue
            btnCash.ForeColor = Color.White
            cashState = 0
            txtCashAmount.Text = String.Format("{0:#,##0}", 0)
            CalculatePaid()
            CalculateChange()
        End If
    End Sub

    Private Sub PaymentCard()
        If cardState = 0 Then
            If cashState = 1 And Trim(txtCashAmount.Text) = 0 Then
                cashGroupBox.Enabled = False
                btnCash.BackColor = Color.steelblue
                btnCash.ForeColor = Color.White
                cashState = 0
            End If

            If voucherState = 1 And Trim(txtVoucherAmount.Text) = 0 Then
                voucherGroupBox.Enabled = False
                btnVoucher.BackColor = Color.steelblue
                btnVoucher.ForeColor = Color.White
                voucherState = 0
            End If

            txtCardAmount.Text = String.Format("{0:#,##0}", CDec(lblSubTotal.Text) - CDec(lblPaid.Text))

            If CDec(txtCardAmount.Text) < 0 Then
                MsgBox("Payment card negative", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            cardGroupBox.Enabled = True
            btnCard.BackColor = Color.Red
            btnCard.ForeColor = Color.White

            cardState = 1

            Try
                Dim data As New DataTable
                If Trim(cmbEDC.Text) = "" Then
                    MsgBox("Fill EDC!", MsgBoxStyle.Exclamation, Title)
                    cmbCardType.Focus()
                    Exit Sub

                End If
                data = IsChargeCC(cmbCardType.SelectedValue, cmbEDC.SelectedValue)

                If data.Rows.Count > 0 Then
                    If Not CDec(txtCardAmount.Text) >= data.Rows(0).Item("Charge_MinPayment") Then
                        mcharge = CDec(txtCardAmount.Text) * data.Rows(0).Item("Charge_Rate") / 100
                        txtCharge.Text = String.Format("{0:#,##0}", mcharge)

                        lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                        CalculatePaid()
                        CalculateChange()
                    End If
                Else
                    mcharge = 0
                    txtCharge.Text = String.Format("{0:#,##0}", mcharge)
                    lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                    CalculatePaid()
                    CalculateChange()
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

            cmbCardType.Focus()
        Else
            cardGroupBox.Enabled = False
            btnCard.BackColor = Color.steelblue
            btnCard.ForeColor = Color.White
            cardState = 0
            txtCardAmount.Text = String.Format("{0:#,##0}", 0)
            lblSubTotal.Text = String.Format("{0:#,##0}", CDec(lblSubTotal.Text) - CDec(txtCharge.Text))
            txtCharge.Text = 0

        End If
        CalculatePaid()
        CalculateChange()
    End Sub

    Private Sub txtCashAmount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashAmount.Click
        txtCashAmount.SelectAll()
    End Sub

    Private Sub txtCashAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashAmount.GotFocus
        txtCashAmount.SelectAll()
    End Sub

    Private Sub txtCardAmount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCardAmount.Click
        txtCardAmount.SelectAll()
    End Sub

    Private Sub txtCardAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCardAmount.GotFocus
        txtCardAmount.SelectAll()
    End Sub

    Private Sub txtCashAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCashAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub txtCashAmount_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCashAmount.KeyUp
        If e.KeyCode = Keys.Enter Then
            CalculatePaid()
            CalculateChange()
            txtCashAmount.Text = String.Format("{0:#,##0}", CDec(txtCashAmount.Text))
        End If
    End Sub

    Private Sub txtCashAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashAmount.LostFocus
        If firstLoad = True Then
            CalculatePaid()
            CalculateChange()
            txtCashAmount.Text = String.Format("{0:#,##0}", CDec(txtCashAmount.Text))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SavePayment()

    End Sub

    Private Sub SavePayment()
        If voucherState = 1 Then
            If txtVoucherAmount.Text = 0 Then
                MsgBox("Voucher amount must greather than 0!!", MsgBoxStyle.Exclamation)
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If

        If cashState = 1 Then
            If txtCashAmount.Text = 0 Then
                MsgBox("Cash amount must greather than 0!!", MsgBoxStyle.Exclamation)
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If


        CalculatePaid()
        CalculateChange()
        txtCashAmount.Text = String.Format("{0:#,##0}", CDec(txtCashAmount.Text))


        If txtCardAmount.Text = 0 And txtCashAmount.Text = 0 And txtVoucherAmount.Text = 0 Then
            MsgBox("Choose customer payment first!!!", MsgBoxStyle.Exclamation, Title)
            DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        If CDec(lblPaid.Text) < CDec(lblSubTotal.Text) Then
            MsgBox("Paid must more than total!!!", MsgBoxStyle.Exclamation, Title)
            DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If


        If cardState = 1 Then

            If txtCardAmount.Text <> 0 Then
                If cmbCardType.Text = "" Then

                    MsgBox("Choose card type!", MsgBoxStyle.Exclamation, Title)
                    DialogResult = Windows.Forms.DialogResult.None
                    cmbCardType.Focus()
                    Exit Sub
                End If

                If Trim(txtCardNo.Text) = "" Then

                    MsgBox("Fill card no!", MsgBoxStyle.Exclamation, Title)
                    DialogResult = Windows.Forms.DialogResult.None
                    txtCardNo.Focus()
                    Exit Sub
                End If

                If Trim(txtCardName.Text) = "" Then

                    MsgBox("Fill card name!", MsgBoxStyle.Exclamation, Title)
                    DialogResult = Windows.Forms.DialogResult.None
                    txtCardName.Focus()
                    Exit Sub
                End If


                If Trim(cmbEDC.Text) = "" Then

                    MsgBox("Choose EDC!", MsgBoxStyle.Exclamation, Title)
                    DialogResult = Windows.Forms.DialogResult.None
                    cmbEDC.Focus()
                    Exit Sub
                End If

                If Trim(txtApproval.Text) = "" Then

                    MsgBox("Fill approval!", MsgBoxStyle.Exclamation, Title)
                    DialogResult = Windows.Forms.DialogResult.None
                    txtApproval.Focus()
                    Exit Sub
                End If

                'If voucherState = 1 And cashState = 1 Then
                '    If CDec(txtCardAmount.Text) > (CDec(lblSubTotal.Text) - CDec(lblPaid.Text)) Then

                '        MsgBox("Card Amount can't more than sub total!", MsgBoxStyle.Exclamation, Title)
                '        DialogResult = Windows.Forms.DialogResult.None

                '        Exit Sub
                '    End If
                'End If

            Else
                If txtCardAmount.Text = 0 Then
                    MsgBox("Card amount must greather than 0!!", MsgBoxStyle.Exclamation)
                    DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            End If
        End If


        DialogResult = Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub cmbEDC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEDC.SelectedIndexChanged
        If firstLoad = True Then

            Try
                Dim data As New DataTable
                If Trim(cmbCardType.Text) = "" Then
                    MsgBox("Fill card type!", MsgBoxStyle.Exclamation, Title)
                    cmbCardType.Focus()
                    Exit Sub

                End If
                data = IsChargeCC(cmbCardType.SelectedValue, cmbEDC.SelectedValue)

                If data.Rows.Count > 0 Then
                    If Not CDec(txtCardAmount.Text) >= data.Rows(0).Item("Charge_MinPayment") Then
                        mcharge = CDec(txtCardAmount.Text) * data.Rows(0).Item("Charge_Rate") / 100
                        txtCharge.Text = String.Format("{0:#,##0}", mcharge)

                        lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                        CalculatePaid()
                        CalculateChange()
                    End If
                Else
                    mcharge = 0
                    txtCharge.Text = String.Format("{0:#,##0}", mcharge)
                    lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                    CalculatePaid()
                    CalculateChange()
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try
        End If
    End Sub

    Private Function IsChargeCC(ByVal cardtype As String, ByVal edcid As String) As DataTable
        Try
            table = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT minpayment Charge_MinPayment,bankcharge Charge_Rate" & _
                                " FROM " & DB & ".dbo.mcard_charge" & _
                                " WHERE cardtype='" & cardtype & "' AND EDCID='" & edcid & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return table
    End Function

    Private Sub txtCardAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCardAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmbCardType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCardType.SelectedIndexChanged
        If firstLoad = True Then

            Try
                Dim data As New DataTable
                If Trim(cmbEDC.Text) = "" Then
                    MsgBox("Fill EDC!", MsgBoxStyle.Exclamation, Title)
                    cmbCardType.Focus()
                    Exit Sub

                End If
                data = IsChargeCC(cmbCardType.SelectedValue, cmbEDC.SelectedValue)

                If data.Rows.Count > 0 Then
                    If Not CDec(txtCardAmount.Text) >= data.Rows(0).Item("Charge_MinPayment") Then
                        mcharge = CDec(txtCardAmount.Text) * data.Rows(0).Item("Charge_Rate") / 100
                        txtCharge.Text = String.Format("{0:#,##0}", mcharge)

                        lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                        CalculatePaid()
                        CalculateChange()
                    End If
                Else
                    mcharge = 0
                    txtCharge.Text = String.Format("{0:#,##0}", mcharge)
                    lblSubTotal.Text = String.Format("{0:#,##0}", mSubTotal + CDec(txtCharge.Text))

                    CalculatePaid()
                    CalculateChange()
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try
        End If
    End Sub

    Private Sub btnVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoucher.Click
        Try
            If GetValueParamNumber("ACTIVATE VOUCHER COUPON") = 0 Then
                txtVoucherCode.Enabled = False

                MsgBox("Please activate voucher coupon in parameter first!", MsgBoxStyle.Exclamation, Title)
                voucherGroupBox.Enabled = False
                btnVoucher.BackColor = Color.steelblue
                btnVoucher.ForeColor = Color.White
                voucherState = 0
                txtVoucherAmount.Text = String.Format("{0:#,##0}", 0)

                txtVoucherCode.Text = ""

                Exit Sub

            Else

                If GetValueParamNumber("VOUCHER MANUAL") = 0 Then
                    txtVoucherAmount.ReadOnly = True
                Else
                    txtVoucherAmount.ReadOnly = False
                End If

                PaymentVoucher()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try


    End Sub

    Private Sub PaymentVoucher()
        If voucherState = 0 Then
            If cashState = 1 And Trim(txtCashAmount.Text) = 0 Then
                cashGroupBox.Enabled = False
                btnCash.BackColor = Color.steelblue
                btnCash.ForeColor = Color.White
                cashState = 0
            End If

            If cardState = 1 And Trim(txtCardAmount.Text) = 0 Then
                cardGroupBox.Enabled = False
                btnCard.BackColor = Color.steelblue
                btnCard.ForeColor = Color.White
                cardState = 0
            End If

            voucherGroupBox.Enabled = True
            btnVoucher.BackColor = Color.Red
            btnVoucher.ForeColor = Color.White
            voucherState = 1

            txtVoucherCode.Enabled = True

            txtVoucherCode.Focus()



        Else
            voucherGroupBox.Enabled = False
            btnVoucher.BackColor = Color.steelblue
            btnVoucher.ForeColor = Color.White
            voucherState = 0
            txtVoucherAmount.Text = String.Format("{0:#,##0}", 0)

            txtVoucherCode.Text = ""
            tableVoucher.Rows.Clear()


        End If
        CalculatePaid()
        CalculateChange()
    End Sub

    Private Sub txtVoucherAmount_Click(sender As Object, e As System.EventArgs) Handles txtVoucherAmount.Click
        txtVoucherAmount.SelectAll()
    End Sub

    Private Sub txtVoucherAmount_GotFocus(sender As Object, e As System.EventArgs) Handles txtVoucherAmount.GotFocus
        txtVoucherAmount.SelectAll()
    End Sub

    Private Sub txtVoucherAmount_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If


    End Sub

    Private Sub txtVoucherAmount_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtVoucherAmount.KeyUp
        If e.KeyCode = Keys.Enter Then
            If tableVoucher.Rows.Count > 0 Then
                CalculateVoucherAmt(tableVoucher)
            Else
                If CDec(txtVoucherAmount.Text) > CDec(lblSubTotal.Text) Then
                    txtVoucherAmount.Text = String.Format("{0:#,##0}", lblSubTotal.Text)
                Else
                    txtVoucherAmount.Text = String.Format("{0:#,##0}", CDec(txtVoucherAmount.Text))
                End If
            End If

            CalculatePaid()
            CalculateChange()
            txtVoucherAmount.Text = String.Format("{0:#,##0}", CDec(txtVoucherAmount.Text))

        End If
    End Sub

    Private Sub txtVoucherAmount_LostFocus(sender As Object, e As System.EventArgs) Handles txtVoucherAmount.LostFocus
        If firstLoad = True Then
            If tableVoucher.Rows.Count > 0 Then
                CalculateVoucherAmt(tableVoucher)
            Else
                If CDec(txtVoucherAmount.Text) > CDec(lblSubTotal.Text) Then
                    txtVoucherAmount.Text = String.Format("{0:#,##0}", lblSubTotal.Text)
                Else
                    txtVoucherAmount.Text = String.Format("{0:#,##0}", CDec(txtVoucherAmount.Text))
                End If
            End If

            CalculatePaid()
            CalculateChange()
            txtVoucherAmount.Text = String.Format("{0:#,##0}", CDec(txtVoucherAmount.Text))
        End If
    End Sub

    Private Sub btnViewListVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewListVoucher.Click
        Dim fListVoucher As frmListVoucher = New frmListVoucher

        fListVoucher.ListVoucher = tableVoucher
        If fListVoucher.ShowDialog = Windows.Forms.DialogResult.OK Then
            tableVoucher.Rows.Clear()

            For i As Integer = 0 To fListVoucher.GridVoucher.RowCount - 1
                With tableVoucher
                    .Rows.Add(New Object() {fListVoucher.GridVoucher.Rows(i).Cells(1).Value, fListVoucher.GridVoucher.Rows(i).Cells(2).Value, fListVoucher.GridVoucher.Rows(i).Cells(3).Value})

                End With
            Next

            CalculateVoucherAmt(tableVoucher)

        End If

    End Sub

    Private Sub txtVoucherCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherCode.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then
            AddVoucher()
        End If
    End Sub

    Private Sub btnAddVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVoucher.Click
        AddVoucher()
    End Sub

    Private Function IsExistsVoucherCode(ByVal voucher As String, ByVal data As DataTable) As Boolean
        For i As Integer = 0 To data.Rows.Count - 1
            If data.Rows(i).Item(1) = voucher Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Sub CalculateVoucherAmt(ByVal data As DataTable)
        Dim amt As Decimal = 0

        For i As Integer = 0 To Data.Rows.Count - 1
            amt += data.Rows(i).Item(2)
        Next

        If amt > CDec(lblSubTotal.Text) Then
            txtVoucherAmount.Text = String.Format("{0:#,##0}", lblSubTotal.Text)
        Else
            txtVoucherAmount.Text = String.Format("{0:#,##0}", amt)
        End If

    End Sub

    Private Sub AddVoucher()
        If Trim(txtVoucherCode.Text) <> "" Then
            Dim voucherid As String = ""

            voucherAmt = ValidateVoucherAmt(Trim(txtVoucherCode.Text), voucherid)

            If voucherAmt = 0 Then
                MsgBox("Voucher Code not found!!!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If tableVoucher.Rows.Count > 0 Then
                If IsExistsVoucherCode(Trim(txtVoucherCode.Text), tableVoucher) Then
                    MsgBox("Voucher Code is exists!!!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If
            End If

            With tableVoucher
                .Rows.Add(New Object() {voucherid, Trim(txtVoucherCode.Text), voucherAmt})

            End With

            CalculateVoucherAmt(tableVoucher)
            txtVoucherCode.Clear()

            CalculatePaid()
            CalculateChange()
        End If
    End Sub

    Private Sub txtCardAmount_KeyUp(sender As Object, e As KeyEventArgs) Handles txtCardAmount.KeyUp
        If e.KeyCode = Keys.Enter Then
            CalculatePaid()
            CalculateChange()
            txtCardAmount.Text = String.Format("{0:#,##0}", CDec(txtCardAmount.Text))
        End If
    End Sub

    Private Sub txtCardAmount_LostFocus(sender As Object, e As EventArgs) Handles txtCardAmount.LostFocus
        If firstLoad = True Then
            CalculatePaid()
            CalculateChange()
            txtCardAmount.Text = String.Format("{0:#,##0}", CDec(txtCardAmount.Text))
        End If
    End Sub
End Class