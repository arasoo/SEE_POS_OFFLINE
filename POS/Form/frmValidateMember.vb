Imports mainlib
Imports genLib.General
Imports sqlLib.Sql
Imports proLib.Process

Public Class frmValidateMember

    Private mMemberCode As String = ""
    Private mMemberName As String = ""
    Private mMemberDisc As Decimal = 0
    Private mMemberMinPayment As Decimal = 0
    Private mEmployeeIDValid As Boolean = False
    Private mState As Integer = 0

    Public WriteOnly Property FormState As Integer
        Set(value As Integer)
            mState = value
        End Set
    End Property

    Public ReadOnly Property GetMemberCode As String
        Get
            Return mMemberCode
        End Get

    End Property

    Public ReadOnly Property GetEmpIDValid As Boolean
        Get
            Return mEmployeeIDValid
        End Get

    End Property


    Public ReadOnly Property GetMemberName As String
        Get
            Return mMemberName
        End Get

    End Property

    Public ReadOnly Property GetMemberDisc As Decimal
        Get
            Return mMemberDisc
        End Get

    End Property


    Public ReadOnly Property GetMemberMinPayment As Decimal
        Get
            Return mMemberMinPayment
        End Get

    End Property

    Private Sub frmValidateMember_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If mState = 0 Then 'Check Member
            Me.Text = "Check Member"
            lblNote.Text = "Please input member code"
            memberPic.Image = mainClass.imgList.ImgLoginUser
        Else 'Check Employee ID POS
            Me.Text = "Check Employee ID POS"
            lblNote.Text = "Please input employee id"
            memberPic.Image = mainClass.imgList.ImgLoginUser
        End If

    End Sub

    Private Sub txtMember_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMember.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then
            Try

                If mState = 0 Then 'Member exists
                    If Not MemberExists(Trim(txtMember.Text)) = True Then
                        MsgBox("Member not found!", MsgBoxStyle.Exclamation, Title)
                        DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    End If

                    table = New DataTable

                    table = GetDiscMember(GetValueParamText("POS SALESOFFICE"), Trim(txtMember.Text))

                    If table.Rows.Count > 0 Then
                        mMemberCode = table.Rows(0).Item(3)
                        mMemberName = table.Rows(0).Item(4)
                        mMemberDisc = table.Rows(0).Item(5)
                        mMemberMinPayment = table.Rows(0).Item(6)

                        MsgBox("Member Valid", MsgBoxStyle.Information, Title)
                        DialogResult = Windows.Forms.DialogResult.OK
                    Else
                        MsgBox("No event disc for this member!", MsgBoxStyle.Exclamation, Title)
                        DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    End If
                Else 'Employee ID Exists
                    If Not EmpIDExists(Trim(txtMember.Text), logOn) = True Then
                        mEmployeeIDValid = False
                    Else
                        mEmployeeIDValid = True
                    End If

                    DialogResult = Windows.Forms.DialogResult.OK
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try


        End If
    End Sub
End Class