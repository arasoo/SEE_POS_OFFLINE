Imports genLib.General
Imports mainlib
Imports proLib.Process
Imports sqlLib.Sql

Public Class frmPosEndDay

    Private Sub frmPosEndDay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        SetCloseDate()
    End Sub

    Private Sub SetCloseDate()

        table = New DataTable

        table = GetDatebyMonth(Format(MonthCalendar1.SelectionRange.Start, "MM"), Format(MonthCalendar1.SelectionRange.Start, "yyyy"))

        If table.Rows.Count > 0 Then
            For i As Integer = 0 To table.Rows.Count - 1
                MonthCalendar1.AddMonthlyBoldedDate(table.Rows(i).Item(0))
            Next


        End If
    End Sub

    Private Sub LoadImage()

        btnPosting.Image = mainClass.imgList.ImgBtnPosting

        btnClose.Image = mainClass.imgList.ImgBtnClosing

    End Sub

    Private Sub btnPosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPosting.Click

    End Sub
End Class