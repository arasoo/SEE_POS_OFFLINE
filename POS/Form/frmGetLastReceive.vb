Imports mainlib
Imports System.Data
Imports System.IO
Imports genLib.General
Imports saveLib.Save
Imports sqlLib.Sql

Public Class frmGetLastReceive
    Public Declare Auto Function GetOpenClipboardWindow Lib "user32" () As IntPtr
    Public Declare Auto Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As IntPtr

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmGetLastReceive_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V Then 'Paste Clipboard
            Me.Cursor = Cursors.WaitCursor

            Try
ReClipboard:

                Dim c As IDataObject = Clipboard.GetDataObject
                Dim arrSplitData As Array = Nothing
                Dim ClipToArray As Array = Nothing

                Dim text As String = ""
                Dim sFormattedData As String
                Dim d As New DataTable

                If (c.GetDataPresent(DataFormats.CommaSeparatedValue)) Then

                    Dim sr As New StreamReader(CType(c.GetData(DataFormats.CommaSeparatedValue), Stream))

                    While (sr.Peek() > 0)


                        sFormattedData = sr.ReadLine()


                        arrSplitData = sFormattedData.Split(",")

                        If d.Columns.Count <= 0 Then
                            For i As Integer = 0 To arrSplitData.GetUpperBound(0)
                                d.Columns.Add()
                            Next
                        End If

                        If d.Columns.Count > 1 Then
                            MsgBox("Data Clipboard Not Valid!!", MsgBoxStyle.Exclamation, "Superman")
                            Exit Sub

                        End If

                        Dim iLoopCounter As Integer
                        Dim rowNew As DataRow

                        rowNew = d.NewRow()

                        For iLoopCounter = 0 To arrSplitData.GetUpperBound(0)
                            rowNew(iLoopCounter) = arrSplitData.GetValue(iLoopCounter)


                        Next
                        d.Rows.Add(rowNew)
                    End While

                    If d.Rows.Count <> 0 Then
                        With GridLastReceive
                            .AutoGenerateColumns = False
                            .Columns(0).DataPropertyName = "Item"
                            .Columns(1).DataPropertyName = "Description"
                            .Columns(2).DataPropertyName = "BM_Date"
                            .Columns(3).DataPropertyName = "BM_Qty"
                        End With

                        GridLastReceive.DataSource = ClipboardItemLastReceive(d, cmbOption.SelectedIndex)
                    End If

                ElseIf (c.GetDataPresent(DataFormats.Text)) Then
                    d.Columns.Add("code", GetType(String))

                    text = c.GetData(DataFormats.Text, True).ToString
                    text = Replace(text, vbCr, "")
                    ClipToArray = Split(text, vbLf)

                    For i As Integer = 0 To ClipToArray.Length - 1

                        If Not i = 1000 Then
                            If ClipToArray(i).ToString <> "" Then
                                d.Rows.Add(ClipToArray(i))
                            End If

                        End If

                    Next

                    If d.Rows.Count <> 0 Then
                        With GridLastReceive
                            .AutoGenerateColumns = False
                            .Columns(0).DataPropertyName = "Item"
                            .Columns(1).DataPropertyName = "Description"
                            .Columns(2).DataPropertyName = "BM_Date"
                            .Columns(3).DataPropertyName = "BM_Qty"
                        End With

                        GridLastReceive.DataSource = ClipboardItemLastReceive(d, cmbOption.SelectedIndex)
                    End If


                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default

            Catch ex As Exception
                Me.Cursor = System.Windows.Forms.Cursors.Default
                If Err.Number = 5 Then GoTo ReClipboard
            End Try
            Clipboard.Clear()
        End If
    End Sub

    Private Sub frmGetLastReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadImage()

        cmbOption.SelectedIndex = 0
    End Sub

    Private Sub LoadImage()


        btnClose.Image = mainClass.imgList.ImgBtnClosing

        btnExport.Image = mainClass.imgList.ImgBtnExport

        picLabel.Image = mainClass.imgList.ImgLabelReporting

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

            table = GridLastReceive.DataSource


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
End Class