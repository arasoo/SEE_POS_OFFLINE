Imports System.Drawing.Drawing2D
Imports connlib.DBConnection
Imports genLib.General
Imports prolib.Process
Imports saveLib.Save
Imports System.IO
Imports mainLib

Public Class frmBrowse

    Private table As DataTable
    Private first As Boolean = False
    Private mEmp As String

    'Private Sub frmBrowse_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
    '    Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
    '    Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
    '    Dim colors As Color() = {Color.SteelBlue, Color.SteelBlue, Color.White, Color.White}
    '    Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
    '    Dim blend As New ColorBlend
    '    blend.Colors = colors
    '    blend.Positions = positions
    '    Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
    '        lgb.InterpolationColors = blend
    '        e.Graphics.FillRectangle(lgb, bounds)
    '    End Using
    'End Sub

    Public WriteOnly Property EmployeeID As String
        Set(value As String)
            mEmp = value
        End Set
    End Property

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        Try
            Cursor = Cursors.WaitCursor
            first = False
            RefreshData()
            If GridInvoice.RowCount > 0 Then

                GetDetailInvoice(GridInvoice.SelectedCells(0).Value)
                first = True
            Else
                first = False
            End If
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub RefreshData()
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            table = New DataTable


            If logOn = "00-IT" Then
                query = "SELECT tpayrech.salesorderno Invoice,tpayrech.documentdate Date,tpayrech.salesamount Sales" & _
                                               ",tpayrech.returnamount Change,tpayrech.cashamount Cash,tpayrech.cardamount Card" & _
                                               ",ISNULL((SELECT SUM(tpayrecd.BankChargeAmt) " & _
                                               "FROM tpayrecd WHERE tpayrecd.receiptno=tpayrech.receiptno),0) Bank_Charge," & _
                                               "tpayrech.roundingamount RoundingAmt FROM " & DB & ".dbo.tpayrech " & _
                                               "INNER JOIN " & DB & ".dbo.tslsh on tpayrech.salesorderno=hs_invoice " & _
                                               "WHERE tpayrech.documentdate BETWEEN '" & Format(dtFrom.Value, "yyyy-MM-dd") & "' " & _
                                               "AND '" & Format(dtTo.Value, "yyyy-MM-dd") & "' "
            Else
                query = "SELECT tpayrech.salesorderno Invoice,tpayrech.documentdate Date,tpayrech.salesamount Sales" & _
                                               ",tpayrech.returnamount Change,tpayrech.cashamount Cash,tpayrech.cardamount Card" & _
                                               ",ISNULL((SELECT SUM(tpayrecd.BankChargeAmt) " & _
                                               "FROM tpayrecd WHERE tpayrecd.receiptno=tpayrech.receiptno),0) Bank_Charge," & _
                                               "tpayrech.roundingamount RoundingAmt FROM " & DB & ".dbo.tpayrech " & _
                                               "INNER JOIN " & DB & ".dbo.tslsh on tpayrech.salesorderno=hs_invoice " & _
                                               "WHERE tpayrech.documentdate BETWEEN '" & Format(dtFrom.Value, "yyyy-MM-dd") & "' " & _
                                               "AND '" & Format(dtTo.Value, "yyyy-MM-dd") & "' " & _
                                               "AND tpayrech.employeeid='" & mEmp & "'"
            End If


            With cm
                .Connection = cn
                .CommandText = query
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            If table.Rows.Count > 0 Then
                GridInvoice.Rows.Clear()
                For i As Integer = 0 To table.Rows.Count - 1
                    GridInvoice.Rows.Add(New Object() {Trim(table.Rows(i).Item("Invoice")) _
                                                       , table.Rows(i).Item("Date") _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("Sales")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("Change")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("Cash")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("Card")) _
                                                       , String.Format("{0:#,##0}", IIf(IsDBNull(table.Rows(i).Item("Bank_Charge")), 0, table.Rows(i).Item("Bank_Charge"))) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("RoundingAmt"))})
                Next

            End If



            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub GetDetailInvoice(ByVal doc As String)
        Try
            Dim seqnum As Integer = 0

            If cn.State = ConnectionState.Closed Then cn.Open()
            table = New DataTable
            With cm
                .Connection = cn
                .CommandText = "SELECT ds_partnumber Item,type_description Judul," & _
                                "ds_uom UOM,ds_qty Qty,ds_dpp+ds_ppn Amount " & _
                                "FROM " & DB & ".dbo.tslsd " & _
                                "INNER JOIN " & DB & ".dbo.mtipe on type_partnumber=ds_partnumber " & _
                                "WHERE ds_invoice = '" & doc & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            If table.Rows.Count > 0 Then
                gridDetail.Rows.Clear()
                For i As Integer = 0 To table.Rows.Count - 1
                    seqnum += 1

                    gridDetail.Rows.Add(New Object() {seqnum _
                                                       , Trim(table.Rows(i).Item("Item")) _
                                                       , Trim(table.Rows(i).Item("Judul")) _
                                                       , table.Rows(i).Item("UOM") _
                                                       , table.Rows(i).Item("Qty") _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("Amount"))})
                Next

            End If


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub GridInvoice_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInvoice.DoubleClick

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    
    Private Sub GridInvoice_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInvoice.SelectionChanged
        If first = True Then
            If GridInvoice.RowCount > 0 Then
                GetDetailInvoice(GridInvoice.SelectedCells(0).Value)

            End If

        End If
    End Sub

    Private Sub frmBrowse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        dtFrom.Value = Format(Now, "MM") & "/01/" & Format(Now, "yyyy")


    End Sub

    Private Sub LoadImage()
     
        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh
  
        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub
End Class