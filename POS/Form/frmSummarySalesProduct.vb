Imports genLib.General
Imports sqlLib.Sql
Imports connLib.DBConnection
Imports saveLib.Save
Imports mainlib

Public Class frmSummarySalesProduct

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim SFD As New SaveFileDialog
        Dim strFileName As String = ""
        Dim mDataset As DataSet


        Try
            SFD.InitialDirectory = "C:\"
            SFD.Title = "Save Your File Spreadsheet"
            SFD.Filter = "Microsoft Excel(*.xls)|*.xls|Comma Delimited File(*.csv)|*.Csv"
            SFD.OverwritePrompt = True
            SFD.ShowDialog()
            strFileName = SFD.FileName
            table = New DataTable
            table = GridSalesProducts.DataSource

            ' If SFD.ShowDialog() = DialogResult.OK Then
            If SFD.FilterIndex = 1 Then
                mDataset = New DataSet("Data")

                mDataset.Tables.Add(table.Copy)
                If WriteXLSFile(strFileName, mDataset) Then
                    MsgBox("Export Finish", MsgBoxStyle.Information, Title)

                End If
            Else
                Call ExporttoCSV(table, strFileName, vbTab)
                MsgBox("Export Finish", MsgBoxStyle.Information, Title)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim dtSales As New DataTable
        Dim myds As DataSet
        Dim i, a, b As Integer
        Dim tgl As Date
        Try
            Me.Cursor = Cursors.WaitCursor

            b = DateDiff(DateInterval.Day, dtFrom.Value, dtTo.Value)

            With dtSales.Columns
                .Add("Code", GetType(String))
                .Add("Descriptions", GetType(String))
                For i = 0 To b
                    tgl = DateAdd(DateInterval.Day, i, dtFrom.Value)
                    .Add("S_" & Format(tgl, "dd") & Format(tgl, "MMM"), GetType(Decimal))
                    .Add("Q_" & Format(tgl, "dd") & Format(tgl, "MMM"), GetType(Integer))
                Next

            End With
            myds = Product()

            Dim dtNewRow As DataRow
            For d As Integer = 0 To myds.Tables("Product").Rows.Count - 1
                dtNewRow = dtSales.NewRow()
                dtNewRow.Item("Code") = myds.Tables("Product").Rows(d).Item(0)
                dtNewRow.Item("Descriptions") = myds.Tables("Product").Rows(d).Item(1)
                For a = 0 To b
                    tgl = DateAdd(DateInterval.Day, a, dtFrom.Value)
                    dtNewRow.Item("S_" & Format(tgl, "dd") & Format(tgl, "MMM")) = TotalAmount(a, myds.Tables("Product").Rows(d).Item(0))
                    dtNewRow.Item("Q_" & Format(tgl, "dd") & Format(tgl, "MMM")) = TotalQty(a, myds.Tables("Product").Rows(d).Item(0))
                Next

                dtSales.Rows.Add(dtNewRow)
            Next
            GridSalesProducts.DataSource = dtSales
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Function TotalAmount(ByVal jml As Integer, ByVal code As String) As Decimal
        Dim ds As New DataSet
        Dim tanggal As Date
        tanggal = DateAdd(DateInterval.Day, jml, dtFrom.Value)

        If cn.State = ConnectionState.Closed Then cn.Open()

        With cm
            .Connection = cn
            .CommandTimeout = 0
            .CommandText = "select SUM(ds_amount)Amt FROM " & DB & ".dbo.tslsd " & _
                            "where ds_product='" & code & "' and ds_invoiceDate='" & Format(tanggal, formatDate) & "'"

        End With

        With da
            .SelectCommand = cm
            .Fill(ds, "Amount")
        End With

        If ds.Tables("Amount").Rows(0).Item(0) Is DBNull.Value Then
            Return 0
        Else
            Return ds.Tables("Amount").Rows(0).Item(0)
        End If

        cn.Close()
    End Function

    Private Function TotalQty(ByVal jml As Integer, ByVal code As String) As Integer
        Dim ds As New DataSet
        Dim tanggal As Date
        tanggal = DateAdd(DateInterval.Day, jml, dtFrom.Value)
        If cn.State = ConnectionState.Closed Then cn.Open()

        With cm
            .Connection = cn
            .CommandTimeout = 0
            .CommandText = "select SUM(ds_qty)qty FROM " & DB & ".dbo.tslsd WHERE " & _
                            "ds_product='" & code & "' and ds_invoiceDate='" & Format(tanggal, "yyyy-MM-dd") & "'"

        End With

        With da
            .SelectCommand = cm
            .Fill(ds, "Qty")
        End With
        If ds.Tables("Qty").Rows(0).Item(0) Is DBNull.Value Then
            Return 0
        Else
            Return ds.Tables("Qty").Rows(0).Item(0)
        End If

        cn.Close()
    End Function

    Private Function Product() As DataSet
        Dim ds As New DataSet
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "SELECT PRODUCT_Code Code,PRODUCT_Description Descriptions " & _
                                "FROM " & DB & ".dbo.MCtPROD WHERE PRODUCT_Group <> 9"

            End With

            With da
                .SelectCommand = cm
                .Fill(ds, "product")
            End With
            Return ds
            cn.Close()
        Catch ex As Exception
            cn.Close()

            Throw ex
        End Try

    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub frmSummarySalesProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

End Class