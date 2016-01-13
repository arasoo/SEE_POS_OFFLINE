Imports genLib.General
Imports sqlLib.Sql
Imports proLib.Process
Imports mainlib

Public Class frmDisplay

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub frmDisplay_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub

    Private Sub LoadImage()
        btnClose.Image = mainClass.imgList.ImgBtnClosing

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        picTitle.Image = mainClass.imgList.ImgLabelSearch
    End Sub

    Private Sub btnRack_Click(sender As System.Object, e As System.EventArgs) Handles btnRack.Click
        If frmMasterRack.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshRack()
        End If
    End Sub

    Private Sub RefreshData()
        Try

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub
    '
    Private Sub RefreshRack()
        Try

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub
End Class