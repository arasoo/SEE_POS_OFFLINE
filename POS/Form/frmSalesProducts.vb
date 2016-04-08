Imports mainlib
Imports genLib.General
Imports saveLib.Save
Imports sqlLib.Sql
Imports POS.StackedHeader

Public Class frmSalesProducts

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim objREnderer As New StackedHeaderDecorator(GridSalesProducts)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub frmSalesProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()
        LoadSalesOrg(cmbSalesOrgPOS, gridAll, 0)
        LoadProducts(cmbProductGroup, gridAll, 0)

        cmbStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadImage()

        btnRefresh.Image = mainClass.imgList.ImgBtnRefresh

        btnExport.Image = mainClass.imgList.ImgBtnExport

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelReporting

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.Click

        If gridAll.Tag = "SLSORG" Then
            cmbSalesOrgPOS.SelectedValue = gridAll.SelectedCells(0).Value
        ElseIf gridAll.Tag = "PRODUCT_GROUP" Then
            cmbProductGroup.SelectedValue = gridAll.SelectedCells(0).Value

        Else

        End If

        gridAll.Visible = False

    End Sub

    Private Sub cmb_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSalesOrgPOS.Click, cmbProductGroup.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            Select Case senderCmb.Tag
                Case "SLSORG"
                    LoadSalesOrg(senderCmb, gridAll, 1)
                Case "PRODUCT_GROUP"
                    LoadProducts(senderCmb, gridAll, 1)
                Case Else

            End Select


            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                                     (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, _
                                     GetRowHeight(gridAll.Rows.Count, gridAll))
            senderCmb.DroppedDown = False

            If gridAll.Visible = True Then
                gridAll.Visible = False
                senderCmb.Focus()
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
                gridAll.Focus()
            End If

            gridAll.Tag = senderCmb.Tag

            gridAll.Columns(0).Width = 50
            gridAll.Columns(1).Width = gridAll.Width - 54

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub cmb_TextChanged(sender As Object, e As EventArgs) Handles cmbSalesOrgPOS.TextChanged, cmbProductGroup.TextChanged
        Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

        Select Case senderCmb.Tag
            Case "SLSORG"

                If cmbSalesOrgPOS.Text = "" Then
                    cmbSalesOrgPOS.Text = "Any"
                End If
            Case "PRODUCT_GROUP"

                If cmbProductGroup.Text = "" Then
                    cmbProductGroup.Text = "Any"
                End If
            Case Else

        End Select


    End Sub

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
        Try

            Dim slsorg As String
            Dim status As String
            Dim productcode As String

            lblTotal.Text = 0
            lblTotalQty.Text = 0

            Me.Cursor = Cursors.WaitCursor

            If cmbSalesOrgPOS.Text = "Any" Then
                slsorg = ""
            Else
                slsorg = cmbSalesOrgPOS.SelectedValue
            End If

            If cmbProductGroup.Text = "Any" Then
                productcode = ""
            Else
                productcode = cmbProductGroup.SelectedValue
            End If


            If cmbStatus.SelectedIndex = 0 Then
                status = ""
            ElseIf cmbStatus.SelectedIndex = 1 Then
                status = "C"
            Else
                status = "G"
            End If

            table = New DataTable

            table = ReportSalesProducts(slsorg, dtFrom.Value, dtTo.Value, productcode, status)

            If table.Rows.Count > 0 Then

                With GridSalesProducts
                    .AutoGenerateColumns = False
                    .Columns(0).DataPropertyName = "Item"
                    .Columns(1).DataPropertyName = "Description"
                    .Columns(2).DataPropertyName = "Product"
                    .Columns(3).DataPropertyName = "Qty"
                    .Columns(4).DataPropertyName = "Amount"


                End With

                GridSalesProducts.DataSource = table

            Else
                GridSalesProducts.DataSource = Nothing


            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        Finally
            CalculateTotal()
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub CalculateTotal()
        Dim totalAmt As Decimal = 0
        Dim totalQty As Integer = 0

        'Total Amount
        For i As Integer = 0 To GridSalesProducts.RowCount - 1
            totalAmt += GridSalesProducts.Rows(i).Cells(4).Value
            totalQty += GridSalesProducts.Rows(i).Cells(3).Value
        Next

        lblTotal.Text = String.Format("{0:#,##0}", CDec(totalAmt))
        lblTotalQty.Text = totalQty

    End Sub
End Class