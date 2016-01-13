Imports genLib.General
Imports connlib.DBConnection
Imports prolib.Process
Imports System.Drawing.Drawing2D
Imports System.IO
Imports mainlib
Imports sqlLib.Sql

Public Class frmCloseCashier

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Private data As DataTable
    Private mstrReceiptPrinterName = posPrinter
    Private mstrOpenDrawerCode = Chr(27) & Chr(112) & Chr(48) & Chr(55) & Chr(121)
    Private mstrPartialCutCode = Chr(27) & Chr(105)
    Private mstrFullCutCode = Chr(27) & Chr(109)
    Private mstrFullCutCode2 = Chr(27) & Chr(105)
    Private mstrFullCutCode3 = Chr(27) & Chr(112) & Chr(0) & Chr(75) & Chr(25)
    Private mstrStringToPrint As String
    Private FILE_NAME As String

    Private Sub frmCloseCashier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        txtUserId.Text = logOn

        GetDataCashier()
        CalculateTotal()
    End Sub

    Private Sub LoadImage()

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub

    Private Sub CalculateTotal()
        txtTotal.Text = String.Format("{0:#,##0}", CDec(txtBeginning.Text) _
                        + CDec(txtCash.Text) + CDec(txtCard.Text) + CDec(txtCharge.Text) + CDec(txtVoucher.Text))
    End Sub

    'Private Sub frmCloseCashier_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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

    Private Sub GetDataCashier()
        Dim temp As New DataTable
        Dim totalcash As Decimal = 0
        Dim totalcard As Decimal = 0
        Dim totalcharge As Decimal = 0

        
        data = New DataTable
        data = GetOpenDetail(logOn)

        temp = GetOmset(data.Rows(0).Item(4), data.Rows(0).Item(0))

        If data.Rows.Count > 0 Then
            txtBeginning.Text = String.Format("{0:#,##0}", data.Rows(0).Item(1))
            numShift.Value = data.Rows(0).Item(2)
            txtEmpID.Text = Trim(data.Rows(0).Item(3))
        Else
            txtBeginning.Text = 0
        End If
        If temp.Rows.Count > 0 Then

            txtCash.Text = String.Format("{0:#,##0}", IIf(IsDBNull(temp.Rows(0).Item(0)), 0, temp.Rows(0).Item(0)))
            txtCard.Text = String.Format("{0:#,##0}", IIf(IsDBNull(temp.Rows(0).Item(1)), 0, temp.Rows(0).Item(1)))
            txtCharge.Text = String.Format("{0:#,##0}", IIf(IsDBNull(temp.Rows(0).Item(2)), 0, temp.Rows(0).Item(2)))
            txtVoucher.Text = String.Format("{0:#,##0}", IIf(IsDBNull(temp.Rows(0).Item(3)), 0, temp.Rows(0).Item(3)))
        Else
            txtCash.Text = 0
            txtCard.Text = 0
            txtCharge.Text = 0
            txtVoucher.Text = 0
        End If

        dtDate.Value = data.Rows(0).Item(0)
    End Sub
    

    Private Sub PrintCloseCashier(ByVal openAmt As Decimal, ByVal tunai As String, ByVal card As String, charge As String, voucher As String)
        Dim tanggal As String = "Tanggal : " & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd " & GetTimeNow())


        Dim text As String = ""
        Dim empName As String = ""

        table = New DataTable
        table = GETDetailEmployee(txtEmpID.Text)

        If table.Rows.Count > 0 Then
            empName = Trim(table.Rows(0).Item(1))
        Else
            empName = "SYSTEM"
        End If

        text = Space(Spacing("Close Cashier")) & "Close Cashier" & vbCrLf & vbCrLf

        text = text & "Tanggal   : " & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd ") & GetTimeNow() & vbCrLf & vbCrLf
        text = text & "Kasir     : " & txtEmpID.Text & vbCrLf & vbCrLf
        text = text & "======================================" & vbCrLf
        text = text & "Beginning : " & vbTab & vbTab & Space(2) & KONVERSI(openAmt) & vbCrLf & vbCrLf
        text = text & "Tunai     : " & vbTab & vbTab & Space(2) & KONVERSI(tunai) & vbCrLf & vbCrLf
        text = text & "Card      : " & vbTab & vbTab & Space(2) & KONVERSI(card) & vbCrLf & vbCrLf
        text = text & "Charge    : " & vbTab & vbTab & Space(2) & KONVERSI(charge) & vbCrLf & vbCrLf
        text = text & "Voucher   : " & vbTab & vbTab & Space(2) & KONVERSI(voucher) & vbCrLf & vbCrLf
        text = text & "======================================" & vbCrLf
        text = text & "Total     : " & vbTab & vbTab & Space(2) & KONVERSI(openAmt + tunai + card + charge + voucher) & vbCrLf & vbCrLf & vbCrLf
        text &= ControlChars.NewLine

        ' experiment with how many lines to advance before doing the cut
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= mstrFullCutCode2
        FILE_NAME = Application.StartupPath & "\closecashier.txt"

        If File.Exists(FILE_NAME) Then
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
            objWriter.Write(text)
            objWriter.Close()
        End If


        RawPrinterHelper.SendStringToPrinter(mstrReceiptPrinterName, text)
        'If File.Exists(Application.StartupPath & "\print.bat") Then
        '    Shell(Application.StartupPath & "\print.bat")

        'End If
        MsgBox("Close Cashier Success", MsgBoxStyle.Information, Title)

    End Sub

    Private Function KONVERSI(ByVal amount As Decimal) As String
        Dim grand As String = ""

        If Len(CStr(amount)) = 8 Then
            grand = "    " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 7 Then
            grand = "    " & " " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 6 Then
            grand = "    " & "   " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 5 Then
            grand = "    " & "    " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 4 Then
            grand = "    " & "     " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 3 Then
            grand = "    " & "       " & Trim(String.Format("{0:#,##0}", amount))
        ElseIf Len(CStr(amount)) = 2 Then
            grand = "    " & "        " & Trim(String.Format("{0:#,##0}", amount))
        Else
            grand = "    " & "         " & Trim(String.Format("{0:#,##0}", amount))
        End If

        Return grand
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If MsgBox("Are you sure?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn

                .CommandText = "UPDATE " & DB & ".dbo.cashierbalance " & _
                                " SET Cashierbal_TotalCash='" & CDec(txtCash.Text) & "'" & _
                                ",Cashierbal_TotalCard='" & CDec(txtCard.Text) & "'" & _
                                ",Cashierbal_TotalCharge='" & CDec(txtCharge.Text) & "'" & _
                                ",Cashierbal_TotalVoucher='" & CDec(txtVoucher.Text) & "'" & _
                                ",CashierBal_CloseDate='" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd " & GetTimeNow()) & "'" & _
                                " WHERE Cashierbal_UserId='" & logOn & "'" & _
                                " AND Cashierbal_OpenDate='" & Format(dtDate.Value, "yyyy-MM-dd HH:mm:ss") & "'"
                .ExecuteNonQuery()
            End With
            PrintCloseCashier(CDec(txtBeginning.Text), CDec(txtCash.Text), CDec(txtCard.Text), CDec(txtCharge.Text), CDec(txtVoucher.Text))
            Me.Close()
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Function Spacing(ByVal txt As String) As Integer
        Dim temp As String = ""
        Dim def As Integer = 40
        Dim sisa As Integer = 0

        temp = txt
        sisa = def - Len(temp)

        Return sisa / 2
    End Function

End Class