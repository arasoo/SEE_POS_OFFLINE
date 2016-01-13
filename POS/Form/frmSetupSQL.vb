Imports System
Imports System.IO
Imports System.Text
Imports iniLib.Ini
Imports secuLib.Security
Imports genLib.General
Imports connLib.DBConnection
Imports mainlib
Imports System.Data.SqlClient

Public Class frmSetupSQL

    Private message As String = ""

    'Private Function Validasi(ByRef msg As String) As Boolean

    '    If Trim(txtServer.Text) = "" Then
    '        msg = "Server Name Must be Fill"
    '        txtServer.Focus()
    '        Return False
    '    End If

    '    If Trim(cmbAuthentication.Text) = "" Then
    '        msg = "Choose Authentication"
    '        cmbAuthentication.Focus()
    '        Return False
    '    End If

    '    If cmbAuthentication.SelectedIndex = 1 Then
    '        If Trim(txtLogin.Text) = "" Then
    '            msg = "User Name Must be Fill"
    '            txtLogin.Focus()
    '            Return False
    '        End If

    '    End If
    '    Return True

    'End Function

    Private Sub frmSetupSQL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim temp As String = ""
        LoadImage()
        cmbServer.SelectedIndex = 1
        Try
            cmbDatabase.Items.Clear()
            srcPath = Application.StartupPath & "\config.ini"
            If File.Exists(srcPath) Then
                temp = INIRead(srcPath, ProjectID, "Server", "")

                If temp <> "" Then
                    cmbServer.Text = INIRead(srcPath, ProjectID, "Server", "")
                    txtHostName.Text = decryptString(INIRead(srcPath, ProjectID, "HostName", ""))
                    cmbDatabase.Text = decryptString(INIRead(srcPath, ProjectID, "Database", ""))
                    cmbAuthentication.SelectedIndex = decryptString(INIRead(srcPath, ProjectID, "Authentication", "0"))
                    numPort.Value = decryptString(INIRead(srcPath, ProjectID, "Port", "0"))
                    txtLogin.Text = decryptString(INIRead(srcPath, ProjectID, "User", ""))
                    txtPassword.Text = decryptString(INIRead(srcPath, ProjectID, "Password", ""))
                Else
                    cmbServer.Text = ""
                    txtHostName.Text = ""
                    cmbDatabase.Text = ""
                    cmbAuthentication.Text = ""
                    numPort.Value = 1433
                    txtLogin.Text = ""
                    txtPassword.Text = ""
                End If

            Else
                cmbServer.Text = ""
                txtHostName.Text = ""
                cmbDatabase.Text = ""
                cmbAuthentication.Text = ""
                numPort.Value = 1433
                txtLogin.Text = ""
                txtPassword.Text = ""

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Title)

        End Try

    End Sub

    Private Sub LoadImage()
       
        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub

    Private Sub txtServer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Enter) Then
            Inisialisasi()
        End If
    End Sub

    Private Sub Inisialisasi()
        Dim query As String
        Dim host As String
        Dim dttable As New DataTable
        host = Trim(txtHostName.Text)

        'cn = New SqlConnection

        'If Not Validasi(message) = True Then
        '    MsgBox(message, MsgBoxStyle.Exclamation, Title)
        '    Exit Sub
        'End If
        Try

          
            query = "SELECT name FROM sys.sysdatabases order by name"
            'query = "SHOW DATABASES"



            If cn.State = ConnectionState.Open Then cn.Close()

            'cn.ConnectionString = "Server='" & host & "';" & _
            '                       "Port='" & numPort.Value & "';uid='" & txtLogin.Text & "';pwd='" & txtPassword.Text & "';"
            cn.ConnectionString = "Data Source='" & host & "';" & _
                               "User Id='" & txtLogin.Text & "';" & _
                               "Password='" & txtPassword.Text & "';Persist Security Info=true;"

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandType = CommandType.Text
                .CommandText = query
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dttable)
            End With


            FillCombo(dttable)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Title)
            Exit Sub
        End Try

    End Sub

    Private Sub FillCombo(ByVal source As DataTable)
        cmbDatabase.DataSource = source
        cmbDatabase.DisplayMember = source.Columns.Item(0).ToString

    End Sub

    Private Sub txtLogin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLogin.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Inisialisasi()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Inisialisasi()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If Not Validasi(message) = True Then
            '    MsgBox(message, MsgBoxStyle.Exclamation, Title)
            '    Exit Sub
            'End If

            If Trim(cmbDatabase.Text) = "" Then
                MsgBox("Database Must be Fill", MsgBoxStyle.Exclamation, Title)
                cmbDatabase.Focus()
                Exit Sub
            End If

            srcPath = Application.StartupPath & "\config.ini"
            INIWrite(srcPath, ProjectID, "Server", cmbServer.Text)
            INIWrite(srcPath, ProjectID, "HostName", encryptString(txtHostName.Text))
            INIWrite(srcPath, ProjectID, "Database", encryptString(cmbDatabase.Text))
            INIWrite(srcPath, ProjectID, "Authentication", encryptString(cmbAuthentication.SelectedIndex))
            INIWrite(srcPath, ProjectID, "Port", encryptString(numPort.Value))
            INIWrite(srcPath, ProjectID, "User", encryptString(txtLogin.Text))
            INIWrite(srcPath, ProjectID, "Password", encryptString(txtPassword.Text))

            If decryptString(INIRead(srcPath, ProjectID, "PosPrinter", "")) = "" Then
                INIWrite(srcPath, ProjectID, "PosPrinter", encryptString(""))
            End If

            MsgBox("Restart Programs", MsgBoxStyle.Information, Title)

            Application.Exit()

            Shell(Application.StartupPath & "\SEE ME.exe")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Title)
        End Try
    End Sub

    Private Sub btnCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Connect = False Then
            End
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmbAuthentication_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAuthentication.SelectedIndexChanged
        If cmbAuthentication.SelectedIndex = 1 Then
            txtLogin.Enabled = True
            txtPassword.Enabled = True
            txtLogin.Focus()
        Else
            txtLogin.Enabled = False
            txtPassword.Enabled = False
            txtLogin.Focus()
        End If
    End Sub

   
    Private Sub cmbServer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServer.SelectedIndexChanged
        If cmbServer.SelectedIndex = 0 Or cmbServer.SelectedIndex = 1 Then
            cmbAuthentication.Enabled = False
            txtLogin.Enabled = True
            txtPassword.Enabled = True

        Else
            cmbAuthentication.Enabled = True
            txtLogin.Enabled = False
            txtPassword.Enabled = False

        End If
    End Sub

    Private Sub numPort_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numPort.ValueChanged

    End Sub
End Class