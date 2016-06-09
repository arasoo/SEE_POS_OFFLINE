Imports mainlib
Imports genLib.General
Imports saveLib.Save
Imports sqlLib.Sql
Imports System.IO

Public Class frmClosingCashier
    Private mNow As Date
    Private mLast As Date
    Private mstrReceiptPrinterName = posPrinter
    Private mstrOpenDrawerCode = Chr(27) & Chr(112) & Chr(48) & Chr(55) & Chr(121)
    Private mstrPartialCutCode = Chr(27) & Chr(105)
    Private mstrFullCutCode = Chr(27) & Chr(109)
    Private mstrFullCutCode2 = Chr(27) & Chr(105)
    Private mstrFullCutCode3 = Chr(27) & Chr(112) & Chr(0) & Chr(75) & Chr(25)
    Private mstrStringToPrint As String
    Private FILE_NAME As String

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadImage()
        btnPrint.Image = mainClass.imgList.ImgBtnPrint
        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub frmClosingCashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub

    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim SFD As New SaveFileDialog
        Dim strFileName As String = ""
        Dim mDataset As DataSet


        Try
            SFD.InitialDirectory = "C:\"
            SFD.Title = "Save Your File Spreadsheet"
            SFD.Filter = "Microsoft Excel(*.xls)|*.xls|Comma Delimited File(*.csv)|*.Csv"
            SFD.OverwritePrompt = True
            SFD.ShowDialog()
            strFileName = SFD.FileName
            table = New DataTable
            table = GridClosingCashier.DataSource

            ' If SFD.ShowDialog() = DialogResult.OK Then
            If SFD.FilterIndex = 1 Then
                mDataset = New DataSet("Data")

                mDataset.Tables.Add(table.Copy)
                If WriteXLSFile(strFileName, mDataset) Then
                    MsgBox("Export Finish", MsgBoxStyle.Information, Title)

                End If
            Else
                Call ExporttoCSV(table, strFileName, vbTab)
                MsgBox("Export Finish", MsgBoxStyle.Information, Title)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor


            mNow = Format(dtFrom.Value, "yyyy") & "-" & Format(dtFrom.Value, "MM") & "-01"
            mLast = LastDayOfMonth(mNow)

            table = New DataTable

            table = ClosingCashier(mNow, mLast)

            With GridClosingCashier
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "cashierbal_id"
                .Columns(1).DataPropertyName = "cashierbal_userid"
                .Columns(2).DataPropertyName = "cashierbal_opendate"
                .Columns(3).DataPropertyName = "cashierbal_amount"
                .Columns(4).DataPropertyName = "cashierbal_totalcash"
                .Columns(5).DataPropertyName = "cashierbal_totalcard"
                .Columns(6).DataPropertyName = "cashierbal_totalcharge"
                .Columns(7).DataPropertyName = "cashierbal_totalvoucher"
                .Columns(8).DataPropertyName = "cashierbal_closedate"
                .Columns(9).DataPropertyName = "cashierbal_note"
                .Columns(10).DataPropertyName = "cashierbal_employeeId"

            End With

            GridClosingCashier.DataSource = table



            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintCloseCashier(CDec(CInt(GridClosingCashier.SelectedCells(3).Value)),
                          CDec(CInt(GridClosingCashier.SelectedCells(4).Value)),
                          CDec(CInt(GridClosingCashier.SelectedCells(5).Value)),
                          CDec(CInt(GridClosingCashier.SelectedCells(6).Value)),
                          CDec(CInt(GridClosingCashier.SelectedCells(7).Value)))
    End Sub


    Private Sub PrintCloseCashier(ByVal openAmt As Decimal, ByVal tunai As String, ByVal card As String, charge As String, voucher As String)
        Dim tanggal As String = "Tanggal : " & Format(GridClosingCashier.SelectedCells(8).Value, "yyyy-MM-dd " & Format(GridClosingCashier.SelectedCells(8).Value, "HH:mm:ss"))


        Dim text As String = ""
        Dim empName As String = ""

        table = New DataTable
        table = GETDetailEmployee(GridClosingCashier.SelectedCells(10).Value)

        If table.Rows.Count > 0 Then
            empName = Trim(table.Rows(0).Item(1))
        Else
            empName = "SYSTEM"
        End If

        text = Space(Spacing("Close Cashier")) & "Close Cashier" & vbCrLf & vbCrLf

        text = text & "Tanggal   : " & tanggal & vbCrLf & vbCrLf
        text = text & "Kasir     : " & empName & vbCrLf & vbCrLf
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

    Private Function Spacing(ByVal txt As String) As Integer
        Dim temp As String = ""
        Dim def As Integer = 40
        Dim sisa As Integer = 0

        temp = txt
        sisa = def - Len(temp)

        Return sisa / 2
    End Function


End Class