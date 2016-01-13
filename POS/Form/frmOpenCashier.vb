Imports genLib.General
Imports connLib.DBConnection
Imports prolib.Process
Imports System.Drawing.Drawing2D
Imports System.IO
Imports mainlib
Imports sqlLib.Sql

Public Class frmOpenCashier

    Private mstrReceiptPrinterName = posPrinter
    Private mstrOpenDrawerCode = Chr(27) & Chr(112) & Chr(48) & Chr(55) & Chr(121)
    Private mstrPartialCutCode = Chr(27) & Chr(105)
    Private mstrFullCutCode = Chr(27) & Chr(109)
    Private mstrFullCutCode2 = Chr(27) & Chr(105)
    Private mstrFullCutCode3 = Chr(27) & Chr(112) & Chr(0) & Chr(75) & Chr(25)
    Private mstrStringToPrint As String
    Private FILE_NAME As String
    Private branchName As String
    Private branchAddress1 As String
    Private branchAddress2 As String
    Private npwp As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            If cmbEmployeeID.Text = "" Then
                MsgBox("Please choose employee ID", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If Not txtAmount.Text > 0 Then
                MsgBox("Open cashier amount must greater than zero", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If ValidateOpenCashierEmpID(cmbEmployeeID.SelectedValue) = True Then
                MsgBox("Employee ID have been open cashier", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If cn.State = ConnectionState.Closed Then cn.Open()
            With cm
                .Connection = cn
                .CommandText = "INSERT INTO " & DB & ".dbo.cashierbalance " & _
                             " (cashierbal_userid,cashierbal_opendate,cashierbal_amount," & _
                             " cashierbal_totalcash,cashierbal_totalcard,cashierbal_totalcharge,cashierbal_totalvoucher," & _
                             " cashierbal_shift,cashierbal_closedate,cashierbal_note,cashierbal_employeeid) " & _
                             " VALUES ('" & txtUserId.Text & "'," & _
                             "'" & Format(dtDate.Value, "yyyy-MM-dd " & GetTimeNow()) & "'," & _
                             "'" & CDec(txtAmount.Text) & "',0,0,0,0,'" & numShift.Value & "'" & _
                             ",null,'" & Trim(txtNote.Text) & "','" & Trim(cmbEmployeeID.SelectedValue) & "')"


                .ExecuteNonQuery()
            End With

            cn.Close()

            PrintOpenCashier(CDec(txtAmount.Text))
            MsgBox("Open Cashier Success", MsgBoxStyle.Information, Title)
            Me.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub PrintOpenCashier(ByVal openAmt As Decimal)

        Dim empName As String = ""

        Dim text As String = ""

        table = New DataTable
        table = GETDetailEmployee(cmbEmployeeID.SelectedValue)

        If table.Rows.Count > 0 Then
            empName = Trim(table.Rows(0).Item(1))
        Else
            empName = "SYSTEM"
        End If

        Call Company()
        text = Space(Spacing(branchName)) & branchName & vbCrLf
        text = text & Space(Spacing(branchAddress1)) & branchAddress1 & vbCrLf
        text = text & Space(Spacing(branchAddress2)) & branchAddress2 & vbCrLf
        text = text & Space(Spacing(GetValueParamText("NPWP NO"))) & GetValueParamText("NPWP NO") & vbCrLf & vbCrLf
        text = text & "========================================" & vbCrLf
        text = text & Space(Spacing("Beginning Cashier")) & "Beginning Cashier" & vbCrLf
        text = text & "========================================" & vbCrLf & vbCrLf
    
        text = text & "Tanggal   : " & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd ") & GetTimeNow() & vbCrLf

        text = text & "Kasir     : " & empName & vbCrLf & vbCrLf
        text = text & "========================================" & vbCrLf
        text = text & "Beginning : " & vbTab & vbTab & Space(2) & KONVERSI(openAmt) & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf

        text &= ControlChars.NewLine

        ' experiment with how many lines to advance before doing the cut
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        FILE_NAME = Application.StartupPath & "\opencashier.txt"

        If File.Exists(FILE_NAME) Then
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
            objWriter.Write(text)
            objWriter.Close()
        End If


        RawPrinterHelper.SendStringToPrinter(mstrReceiptPrinterName, text)
        'If File.Exists(Application.StartupPath & "\print.bat") Then
        '    Shell(Application.StartupPath & "\print.bat")

        'End If


    End Sub

    Private Sub Company()
        Dim data As New DataTable
        Try

            query = "select coy_description,coy_Address01,coy_Address02,coy_NPWP from " & DB & ".dbo.mbranch" & _
                    " where coy_branch='" & Default_Branch & "'"

            If cn.State = ConnectionState.Open Then cn.Close()

            With cm
                .Connection = cn
                .CommandType = CommandType.Text
                .CommandText = query
            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With

            branchName = Trim(data.Rows(0).Item(0))
            branchAddress1 = Trim(data.Rows(0).Item(1))
            branchAddress2 = Trim(data.Rows(0).Item(2))
            npwp = Trim(data.Rows(0).Item(3))
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Superman")
        End Try

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

    Private Sub txtAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.GotFocus
        txtAmount.SelectAll()
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtAmount_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtAmount.Text = String.Format("{0:#,##0}", CDec(txtAmount.Text))
        End If
    End Sub

    Private Sub txtAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.LostFocus
        txtAmount.Text = String.Format("{0:#,##0}", CDec(txtAmount.Text))
    End Sub

    Private Sub txtAmount_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtAmount.MouseClick
        txtAmount.SelectAll()
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        If Trim(txtAmount.Text) = "" Then
            txtAmount.Text = 0
        End If
    End Sub

    Private Sub frmOpenCashier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUserId.Text = logOn
        dtDate.Value = GetValueParamDate("SYSTEM DATE")
        LoadImage()

    End Sub

    Private Sub LoadImage()

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub

    'Private Sub frmOpenCashier_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
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


    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        cmbEmployeeID.SelectedValue = gridAll.SelectedCells(0).Value

        gridAll.Visible = False
    End Sub

    Private Sub cmbEmployeeID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployeeID.Click
        Try
            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            LoadEmployee(senderCmb, gridAll, "", 0)

            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                            (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)), GetRowHeight(gridAll.Rows.Count, gridAll))
            senderCmb.DroppedDown = False


            If gridAll.Visible = True Then
                gridAll.Visible = False
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
            End If

            gridAll.Tag = senderCmb.Tag
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmbEmployeeID_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbEmployeeID.KeyUp
        If e.KeyCode = Keys.Enter Then
            LoadEmployee(cmbEmployeeID, gridAll, Trim(cmbEmployeeID.Text), 1)
        End If
    End Sub
End Class