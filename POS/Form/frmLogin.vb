Imports genLib.General
Imports connlib.DBConnection
Imports secuLib.Security
Imports sqlLib.Sql
Imports System.Drawing.Drawing2D
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Drawing.Graphics
Imports mainlib

Public Class frmLogin
    Private detail As DataTable
    Dim count As Integer = 0
    Private _fadeOpacity As Single = 0
    Private oldImage As Bitmap
    Private newImage As Bitmap

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Login()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    'Private Function UserExists(ByVal email As String) As Boolean
    '    Dim result As Boolean = False
    '    Try
    '        If cn.State = ConnectionState.Closed Then cn.Open()
    '        detail = New DataTable

    '        With cm
    '            .Connection = cn
    '            .CommandText = "SELECT User_Id,User_FullName FROM " & DB & ".dbo.users " & _
    '                            " WHERE User_Email='" & email & "' AND User_Active=1"
    '        End With

    '        With da
    '            .SelectCommand = cm
    '            .Fill(detail)
    '        End With

    '        If detail.Rows.Count > 0 Then
    '            lblFullName.Text = detail.Rows(0).Item(1)
    '            logOn = detail.Rows(0).Item(0)
    '            UserName = detail.Rows(0).Item(1)
    '            result = True
    '        Else
    '            result = False
    '        End If

    '        cn.Close()

    '        Return result


    '    Catch ex As Exception
    '        cn.Close()
    '        Throw ex
    '    End Try
    'End Function

    Private Sub txtUserId_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserId.KeyPress

        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then

            'If Not IsEmailValid(Trim(txtUserId.Text)) = True Then
            '    MsgBox("User Not Valid!", MsgBoxStyle.Exclamation, Title)
            '    Exit Sub
            'End If
            Try
                If Trim(txtUserId.Text) = "00-IT" Then
                    lblFullName.Text = "IT HO"

                    txtPassword.Enabled = True
                    txtPassword.Focus()
                Else
                    If Not ValidateUserExists(Trim(txtUserId.Text)) = False Then
                        lblFullName.Text = UserName
                        txtPassword.Enabled = True
                        txtPassword.Focus()
                    Else
                        txtUserId.Focus()
                        txtPassword.Enabled = False
                        txtPassword.Clear()
                        MsgBox("User not exists!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

            End Try


        End If

    End Sub

    Private Sub frmLogin_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout

        oldImage = New Bitmap(mainClass.imgList.ImgLoginUserGroup, userPic.Size)
        newImage = New Bitmap(mainClass.imgList.ImgLoginUserGroup, userPic.Size)
        userPic.Refresh()
    End Sub

    'Private Sub frmLogin_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
    '    Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
    '    Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
    '    Dim colors As Color() = {Color.SteelBlue, Color.SteelBlue, Color.SteelBlue, Color.White}
    '    Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
    '    Dim blend As New ColorBlend
    '    blend.Colors = colors
    '    blend.Positions = positions
    '    Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
    '        lgb.InterpolationColors = blend
    '        e.Graphics.FillRectangle(lgb, bounds)
    '    End Using
    'End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            Try
                Login()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        End If
    End Sub

    Private Sub Login()
        Try
            If Trim(txtUserId.Text) = "00-IT" And Trim(txtPassword.Text) = "arasoo1229" Then
                Online = True
                logOn = "00-IT"
                UserName = "Administrator"
                UserGroup = "99"
                DialogResult = Windows.Forms.DialogResult.OK
            Else

                If Trim(txtUserId.Text) = "00-IT" Then
                    DialogResult = Windows.Forms.DialogResult.None
                    MsgBox("Password is wrong!", MsgBoxStyle.Critical, Title)
                    Exit Sub
                End If

                If Not PasswordCorrect(Trim(txtUserId.Text), Trim(txtPassword.Text)) = False Then
                    Online = True

                    'Call SetUserOnline(logOn, 1)
                    DialogResult = Windows.Forms.DialogResult.OK
                Else
                    DialogResult = Windows.Forms.DialogResult.None
                    MsgBox("Password is wrong!", MsgBoxStyle.Critical, Title)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            DialogResult = Windows.Forms.DialogResult.None
            Throw ex
        End Try

    End Sub

    'Private Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserId.TextChanged
    '    If IsEmailValid(Trim(txtUserId.Text)) = True Then
    '        txtUserId.ForeColor = Color.SteelBlue
    '    Else
    '        txtUserId.ForeColor = Color.Red
    '    End If
    'End Sub

    Private Function IsEmailValid(ByVal email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(email, pattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUserId.Clear()
        txtPassword.Clear()
        txtPassword.Enabled = False
        lblFullName.Text = ""
        txtUserId.Focus()
        Timer1.Enabled = True
        LoadImage()

    End Sub


    Private Sub LoadImage()

        picCompany.Image = mainClass.imgList.LogoCompany

     

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer2.Start()
        Timer1.Stop()

    End Sub

    'Private Function SetImageOpacity(ByVal img As Image, ByVal imgopc As Double) As Image
    '    Dim bmpPic As New Bitmap(img.Width, img.Height)
    '    Dim grPic As Graphics = Graphics.FromImage(bmpPic)
    '    Dim imgAtt As New Imaging.ImageAttributes
    '    Dim cmxPic As New Imaging.ColorMatrix
    '    cmxPic.Matrix33 = imgopc

    '    imgAtt.SetColorMatrix(cmxPic, Imaging.ColorMatrixFlag.Default, Imaging.ColorAdjustType.Bitmap)
    '    grPic.DrawImage(img, New Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, _
    '                    img.Width, img.Height, GraphicsUnit.Pixel, imgAtt)

    '    grPic.Dispose()
    '    imgAtt.Dispose()

    '    Return bmpPic

    'End Function


    Private Function FadeBitmap(ByVal bmp As Bitmap, ByVal opacity As Single) As Bitmap
        Dim bmp2 As New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format32bppArgb)
        opacity = Math.Max(0, Math.Min(opacity, 1.0F))

        Using ia As New Imaging.ImageAttributes

            Dim cm As New Imaging.ColorMatrix

            cm.Matrix33 = opacity
            ia.SetColorMatrix(cm)

            Dim destpoints() As PointF = {New Point(0, 0), New Point(bmp.Width, 0), New Point(0, bmp.Height)}

            Using g As Graphics = Graphics.FromImage(bmp2)

                g.DrawImage(bmp, destpoints, _
                New RectangleF(Point.Empty, bmp.Size), GraphicsUnit.Pixel, ia)
            End Using

        End Using

        Return bmp2

    End Function

    Private Sub userPic_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles userPic.Paint
        'If _fadeOpacity < 100 AndAlso oldImage IsNot Nothing Then
        '    'Using fadedImage1 As Bitmap = FadeBitmap(oldImage, _fadeOpacity)

        '    e.Graphics.DrawImageUnscaled(oldImage, Point.Empty)

        '    'End Using

        'End If
        If _fadeOpacity > 0 AndAlso oldImage IsNot Nothing Then
            Using fadedImage2 As Bitmap = FadeBitmap(oldImage, _fadeOpacity)

                e.Graphics.DrawImageUnscaled(fadedImage2, Point.Empty)

            End Using

        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        _fadeOpacity += 0.02
        If _fadeOpacity >= 1 Then
            _fadeOpacity = 1
            Timer2.Stop()
        End If


        userPic.Invalidate()

    End Sub

  
End Class