Imports mainlib
Imports System.IO
Imports sqlLib.Sql
Imports genLib.General

Public Class frmInterfacing

    Private countTime As Integer = 0

    Private Sub frmInterfacing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadInterfacing()
    End Sub

    Private Sub LoadImage()
     
        picTitle.Image = mainClass.imgList.ImgLabelInterfacing
        btnClose.Image = mainClass.imgList.ImgBtnClosing

    End Sub


    Private Sub LoadInterfacing()
        Dim dtTabel As New DataTable

        Try
            dtTabel = GetInterfacing()

            With GridInterfacing
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "urutan_proses"
                .Columns(1).DataPropertyName = "nama_tabel"
                .Columns(2).DataPropertyName = "last_doc"

            End With

            GridInterfacing.DataSource = dtTabel

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        countTime += 1000

        If countTime = 5000 Then
            LoadInterfacing()
            countTime = 0
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class