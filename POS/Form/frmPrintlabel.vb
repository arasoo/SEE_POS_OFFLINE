Imports System.Text
Imports Microsoft.VisualBasic.Strings
Imports proLib.Process

Public Class frmPrintlabel

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        MsgBox(GenerateVoucherCode())

    End Sub

    
End Class