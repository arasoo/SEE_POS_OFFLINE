Imports System.Drawing.Drawing2D
Imports connlib.DBConnection
Imports genLib.General
Imports prolib.Process
Imports saveLib.Save

Public Class frmListItem

    'Private Sub frmListItem_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
    '    Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
    '    Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
    '    Dim colors As Color() = {Color.White, Color.White, Color.Khaki, Color.Khaki}
    '    Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
    '    Dim blend As New ColorBlend
    '    blend.Colors = colors
    '    blend.Positions = positions
    '    Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
    '        lgb.InterpolationColors = blend
    '        e.Graphics.FillRectangle(lgb, bounds)
    '    End Using
    'End Sub

    Private Sub frmListItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearch.Focus()
        cmbGroup.SelectedIndex = 1

    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            Try
                Cursor = Cursors.WaitCursor
                RefreshData(cmbGroup.SelectedIndex)
                GridListItem.Focus()
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try
        End If
    End Sub

    Private Sub RefreshData(ByVal state As Integer)
        Try
            Dim text As String
            text = "%" & Trim(txtSearch.Text) & "%"
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            'query = "EXECUTE " & DB & ".dbo.P_GETDETAILITEM '" & GetValueParamText("DEFAULT BRANCH") & "'," & _
            '            "'" & GetValueParamText("DEFAULT WH") & "','" & Trim(text) & "','" & state & "'," & _
            '            "'" & GetValueParamText("STOCK MINUS") & "'"

            query = "SELECT TOP 50 LTRIM(RTRIM(mtipe.type_partnumber)) item,mtipe.type_description judul," & _
                    "CASE WHEN mtipe.type_materialtype in ('520','510','610') THEN 'Consignment' ELSE 'Credit' END sts," & _
                    "CAST(ISNULL(mpart.part_rfsstock ,0)AS INT)Stock,mtipe.type_materialinfo author " & _
                    "FROM dbo.mtipe INNER JOIN dbo.mpart ON mpart.part_partnumber=mtipe.type_partnumber "
                
            If state = 0 Then
                query = query + "WHERE mtipe.type_partnumber LIKE '%' + '" & Trim(text) & "' + '%' AND mtipe.type_status<>1 "
            ElseIf state = 1 Then
                query = query + "WHERE mtipe.type_description LIKE '%' + '" & Trim(text) & "' + '%' AND mtipe.type_status<>1 "
            ElseIf state = 2 Then
                query = query + "WHERE mtipe.type_spl_material2 LIKE '%' + '" & Trim(text) & "' + '%' AND mtipe.type_status<>1 "
            Else
                query = query + "WHERE mtipe.type_materialinfo LIKE '%' + '" & Trim(text) & "' + '%' AND mtipe.type_status<>1 "
            End If

            query = query + "AND EXISTS (SELECT hkstok.* FROM dbo.hkstok WHERE hkstok.stok_partnumber=mtipe.type_partnumber AND hkstok.stok_warehouse='" & GetValueParamText("DEFAULT WH") & "') "

            query = query + "AND mpart.Part_WH='" & GetValueParamText("DEFAULT WH") & "' " & _
                            "AND mpart.Part_Branch='" & GetValueParamText("DEFAULT BRANCH") & "' "

            If GetValueParamText("STOCK MINUS") = 0 Then
                query = query + "AND mpart.part_rfsstock<>0 "
            Else
                query = query + "AND mpart.part_rfsstock=0 "
            End If

            '

            With cm
                .Connection = cn
                .CommandText = query
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            'GridListItem.Rows.Clear()
            'If table.Rows.Count > 0 Then
            '    For i As Integer = 0 To table.Rows.Count - 1
            '        GridListItem.Rows.Add( _
            '                        New Object() {Trim(table.Rows(i).Item(0)), table.Rows(i).Item(1) _
            '                                     , IIf(table.Rows(i).Item(2) = "C", "Consignment", "Credit"), CInt(table.Rows(i).Item(3))})
            '    Next

            'End If

            With GridListItem
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "item"
                .Columns(1).DataPropertyName = "judul"
                .Columns(2).DataPropertyName = "sts"
                .Columns(3).DataPropertyName = "stock"
                .Columns(4).DataPropertyName = "author"
            End With

            GridListItem.DataSource = table

            lblRecords.Text = GridListItem.RowCount & " records"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
        txtSearch.Focus()
    End Sub

    Private Sub GridListItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridListItem.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    'Private Sub GridListItem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridListItem.KeyPress
    '    If e.KeyChar = ChrW(Keys.Enter) Then

    '        If GridListItem.SelectedCells(0).Value <> "" Then

    '            DialogResult = Windows.Forms.DialogResult.OK
    '        Else
    '            DialogResult = Windows.Forms.DialogResult.None
    '        End If

    '    End If

    'End Sub
End Class