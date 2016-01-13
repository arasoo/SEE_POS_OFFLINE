Imports genLib.General
Imports connLib.DBConnection
Imports sqlLib.Sql
Imports proLib.Process
Imports System.Data.SqlClient

Public Class frmListVoucher

    Private mListVoucher As DataTable

    Public WriteOnly Property ListVoucher As DataTable
        Set(ByVal value As DataTable)
            mListVoucher = value
        End Set
    End Property

    'Private Sub AddVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddVoucher.Click
    '    Dim voucher As String = ""

    '    voucher = InputBox("Please input voucher code?", "Voucher")

    '    If Trim(voucher) <> "" Then
    '        ValidateVoucher(Trim(voucher))

    '    End If
    'End Sub

    'Private Sub ValidateVoucher(ByVal text As String)

    '    Dim voucheramt As Decimal

    '    voucherAmt = ValidateVoucherAmt(Trim(text))

    '    If voucherAmt = 0 Then
    '        MsgBox("Voucher Code not found!!!", MsgBoxStyle.Exclamation, Title)
    '        Exit Sub
    '    End If

    '    If GridVoucher.Rows.Count > 0 Then
    '        If IsExistsVoucherCode(Trim(txtVoucherCode.Text), tableVoucher) Then
    '            MsgBox("Voucher Code is exists!!!", MsgBoxStyle.Exclamation, Title)
    '            Exit Sub
    '        End If
    '    End If

    '    With tableVoucher
    '        .Rows.Add(New Object() {Trim(txtVoucherCode.Text), voucherAmt})

    '    End With

    'End Sub

    Private Sub contextVoucher_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles contextVoucher.Opening
        If GridVoucher.Rows.Count > 0 Then
            contextVoucher.Enabled = True
        Else
            contextVoucher.Enabled = False
        End If
    End Sub

    Private Sub frmListVoucher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub frmListVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 0 To mListVoucher.Rows.Count - 1
            With GridVoucher
                .Rows.Add(New Object() {GetLastSeqnum(), mListVoucher.Rows(i).Item(0), mListVoucher.Rows(i).Item(1), mListVoucher.Rows(i).Item(2)})

            End With
        Next
    End Sub

    Private Function GetLastSeqnum() As Integer
        Dim iden As Integer = 0
        If GridVoucher.Rows.Count > 0 Then
            For i As Integer = 0 To GridVoucher.Rows.Count - 1
                iden = GridVoucher.Rows(i).Cells(0).Value
            Next

            iden = iden + 1
        Else
            iden = 1
        End If

        Return iden
    End Function

    Private Sub removeVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles removeVoucher.Click
        If MsgBox("Are you sure Remove Voucher Code " & GridVoucher.Rows(GridVoucher.CurrentRow.Index).Cells(2).Value & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, Title) = MsgBoxResult.No Then Exit Sub
        GridVoucher.Rows.RemoveAt(GridVoucher.CurrentRow.Index)

    End Sub
End Class