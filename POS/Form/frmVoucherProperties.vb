Imports genLib.General
Imports connLib.DBConnection
Imports sqlLib.Sql
Imports proLib.Process
Imports System.Data.SqlClient

Public Class frmVoucherProperties

    Private mTitle As String
    Private mVoucherID As String
    Private mState As Integer
    Private mSalesorg As String = ""

    Public WriteOnly Property VoucherTitle As String
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public WriteOnly Property SalesOrg As String
        Set(ByVal value As String)
            mSalesorg = value
        End Set
    End Property


    Public WriteOnly Property VoucherID As String
        Set(ByVal value As String)
            mVoucherID = value
        End Set
    End Property

    Public WriteOnly Property VoucherState As Integer
        Set(ByVal value As Integer)
            mState = value
        End Set
    End Property

    Private Sub frmVoucherProperties_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F1 Then
            AddDetail()

        End If
    End Sub

    Private Sub frmVoucherProperties_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.Text = mTitle

            If mState = 1 Then
                'LoadEventDetail()
            End If

            lblDocNo.Text = mVoucherID
            cmbVoucherType.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try

            If Trim(txtEventName.Text) = "" Then
                MsgBox("Please input voucher event", MsgBoxStyle.Information, Title)
                txtEventName.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Trim(txtNote.Text) = "" Then
                MsgBox("Please input voucher note", MsgBoxStyle.Information, Title)
                txtNote.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Trim(cmbVoucherType.Text) = "" Then
                MsgBox("Please select voucher type", MsgBoxStyle.Information, Title)
                cmbVoucherType.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Not GridVoucher.Rows.Count > 0 Then
                MsgBox("No detail", MsgBoxStyle.Information, Title)
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            SaveDetailVoucher(mState)
            SaveHeaderVoucher(mState)

            UpdateHistoryPOS(lblDocNo.Text, "VC")
        Catch ex As Exception
            DialogResult = Windows.Forms.DialogResult.None
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub SaveDetailVoucher(ByVal state As Integer)
        Try
            Dim counter As Integer = 0


            If cn.State = ConnectionState.Closed Then cn.Open()
            counter += 1
            If state = 0 Then
                For i As Integer = 0 To GridVoucher.RowCount - 1
                    cm = New SqlCommand

                    With cm
                        .Connection = cn


                        .CommandText = "INSERT INTO " & DB & ".dbo.mvoucherd " & _
                                        "(voucherid,vouchercode,amount)" & _
                                        " VALUES ('" & lblDocNo.Text & "','" & GridVoucher.Rows(i).Cells(1).Value & "','" & GridVoucher.Rows(i).Cells(2).Value & "')"

                        .ExecuteNonQuery()
                    End With
                Next
            Else

                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .CommandText = "DELETE FROM " & DB & ".dbo.mvoucherd WHERE voucherid='" & lblDocNo.Text & "'"
                    .ExecuteNonQuery()
                End With

                'For i As Integer = 0 To GridVoucher.RowCount - 1
                '    counter += 1

                '    cm = New SqlCommand
                '    With cm
                '        .Connection = cn
                '       .CommandText = "INSERT INTO " & DB & ".dbo.mvoucherd " & _
                '                         "(voucherid,vouchercode,amount)" & _
                '                         " VALUES ('" & lblDocNo.Text & "','" & GridVoucher.Rows(i).Cells(1).Value & "','" & GridVoucher.Rows(i).Cells(2).Value & "')"

                '        .ExecuteNonQuery()
                '    End With
                'Next

            End If




            cn.Close()


        Catch ex As Exception
            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.mvoucherd WHERE voucherid='" & lblDocNo.Text & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub SaveHeaderVoucher(ByVal state As Integer)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn

                If state = 0 Then
                    .CommandText = "INSERT INTO " & DB & ".dbo.mvoucherh " & _
                                        "(company,salesorg,salesoffice,voucherid,description,note," & _
                                        "periodfrom,periodto,vouchertype,validflag," & _
                                        "closeflag,createuser,createdate)" & _
                                        " VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & mSalesorg & "'," & _
                                        "'" & GetValueParamText("POS SALESOFFICE") & "','" & lblDocNo.Text & "','" & txtEventName.Text & "'," & _
                                        "'" & txtNote.Text & "','" & Format(dtPeriodFrom.Value, formatDate) & "','" & Format(dtPeriodTo.Value, formatDate) & "','" & cmbVoucherType.SelectedIndex & "'," & _
                                        "'N','N','" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "')"
                Else

                End If

                .ExecuteNonQuery()
            End With

            cn.Close()

        Catch ex As Exception
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.mdiscd WHERE promoid='" & lblDocNo.Text & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub txtEventName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEventName.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub txtNote_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNote.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub AddDetail()

        If txtVoucherCode.Text.Length > 0 Then

            If IsExists(txtVoucherCode.Text) Then
                MsgBox("Voucher Code is Exists!!!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If


            GridVoucher.Rows.Add( _
                               New Object() {GetLastSeqnum(), Trim(txtVoucherCode.Text), CDec(txtDefault.Text)})

            txtVoucherCode.Clear()


        End If

    End Sub

    Private Function IsExists(ByVal item As String) As Boolean
        For i As Integer = 0 To GridVoucher.Rows.Count - 1
            If GridVoucher.Rows(i).Cells(1).Value = item Then
                Return True
            End If
        Next

        Return False
    End Function

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

    Private Sub ColumnAllReadonly()
        For i As Integer = 0 To GridVoucher.Columns.Count - 1
            GridVoucher.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub txtItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVoucherCode.DoubleClick
        If frmListItem.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtVoucherCode.Text = frmListItem.GridListItem.SelectedCells(0).Value

            frmListItem.Close()
        End If
    End Sub

    Private Sub txtVoucherCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVoucherCode.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub txtVoucherCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVoucherCode.KeyUp
        If e.KeyCode = Keys.Enter Then
            AddDetail()

        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure Remove Voucher " & GridVoucher.Rows(GridVoucher.CurrentRow.Index).Cells(1).Value & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, Title) = MsgBoxResult.No Then Exit Sub
        GridVoucher.Rows.RemoveAt(GridVoucher.CurrentRow.Index)
    End Sub

    Private Sub btnAdd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddDetail()
    End Sub

End Class