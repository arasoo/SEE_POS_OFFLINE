Imports genLib.General
Imports connlib.DBConnection
Imports secuLib.Security
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports System.IO
Imports mainLib

Public Class frmChangePassword

    Private detail As DataTable

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub frmChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        txtUserId.Text = logOn
    End Sub

    Private Sub LoadImage()

        picLabel.Image = mainClass.imgList.ImgLabelUsers

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub

    Private Function UserExists(ByVal email As String) As Boolean
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            detail = New DataTable

            With cm
                .Connection = cn
                .CommandText = "SELECT User_Id,User_FullName FROM " & DB & ".dbo.users " & _
                                " WHERE User_Email='" & email & "' AND User_Active=1"
            End With

            With da
                .SelectCommand = cm
                .Fill(detail)
            End With

            If detail.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If

            cn.Close()

            Return result


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Private Function PasswordCorrect(ByVal user As String, ByVal pass As String) As Boolean
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            detail = New DataTable

            With cm
                .Connection = cn
                .CommandText = "SELECT uid_Password FROM " & DB & ".dbo.musers WHERE uid_user='" & user & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(detail)
            End With

            If decryptString(detail.Rows(0).Item(0)) = pass Then

                result = True
            Else
                result = False

            End If
            cn.Close()

            Return result
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Private Function IsEmailValid(ByVal email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(email, pattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub txtOldPassword_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOldPassword.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                If Not PasswordCorrect(Trim(txtUserId.Text), Trim(txtOldPassword.Text)) Then
                    MsgBox("Password is wrong!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                txtNewPassword.Enabled = True
                txtConfirmPassword.Enabled = True
                txtNewPassword.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Trim(txtOldPassword.Text) = "" Then
                MsgBox("Old Password Blank!", MsgBoxStyle.Exclamation, Title)

                txtNewPassword.Clear()
                txtConfirmPassword.Clear()


                txtNewPassword.Enabled = False
                txtConfirmPassword.Enabled = False
                txtOldPassword.Focus()
                Exit Sub
            ElseIf Trim(txtNewPassword.Text) = "" Then
                MsgBox("New Password Blank!", MsgBoxStyle.Exclamation, Title)

                txtNewPassword.Focus()
                Exit Sub
            ElseIf Trim(txtConfirmPassword.Text) = "" Then
                MsgBox("Confirm Password Blank!", MsgBoxStyle.Exclamation, Title)

                txtConfirmPassword.Focus()
                Exit Sub
            ElseIf txtNewPassword.Text <> txtConfirmPassword.Text Then
                MsgBox("Validate Password not valid!", MsgBoxStyle.Exclamation, Title)

                txtNewPassword.Focus()
                Exit Sub
            End If

            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "UPDATE " & DB & ".dbo.muserid SET uid_password='" & encryptString(Trim(txtNewPassword.Text)) & "'" & _
                                " WHERE uid_user='" & Trim(txtUserId.Text) & "'"
                .ExecuteNonQuery()
            End With
            cn.Close()
            MsgBox("Password have been update", MsgBoxStyle.Information, Title)
            Me.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
           
        End Try
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserId.TextChanged
        If IsEmailValid(Trim(txtUserId.Text)) = True Then
            txtUserId.ForeColor = Color.SteelBlue
        Else
            txtUserId.ForeColor = Color.Red
        End If
    End Sub

    Private Sub txtNewPassword_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNewPassword.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtConfirmPassword.Focus()

        End If

    End Sub
End Class