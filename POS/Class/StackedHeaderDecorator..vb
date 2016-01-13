﻿
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Namespace StackedHeader
    Public Class StackedHeaderDecorator
        Private ReadOnly objStackedHeaderGenerator As IStackedHeaderGenerator = StackedHeaderGenerator.Instance
        Private objGraphics As Graphics
        Private ReadOnly objDataGrid As DataGridView
        Private objHeaderTree As Header
        Private iNoOfLevels As Integer
        Private ReadOnly objFormat As StringFormat

        Public Sub New(ByVal objDataGrid As DataGridView)
            Me.objDataGrid = objDataGrid
            objFormat = New StringFormat()
            objFormat.Alignment = StringAlignment.Center
            objFormat.LineAlignment = StringAlignment.Center

            Dim dgvType As Type = objDataGrid.[GetType]()
            Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
            pi.SetValue(objDataGrid, True, Nothing)

            AddHandler objDataGrid.Scroll, AddressOf objDataGrid_Scroll
            AddHandler objDataGrid.Paint, AddressOf objDataGrid_Paint
            AddHandler objDataGrid.ColumnRemoved, AddressOf objDataGrid_ColumnRemoved
            AddHandler objDataGrid.ColumnAdded, AddressOf objDataGrid_ColumnAdded
            AddHandler objDataGrid.ColumnWidthChanged, AddressOf objDataGrid_ColumnWidthChanged
            objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid)
        End Sub

        Public Sub New(ByVal objStackedHeaderGenerator As IStackedHeaderGenerator, ByVal objDataGrid As DataGridView)
            Me.New(objDataGrid)
            Me.objStackedHeaderGenerator = objStackedHeaderGenerator
        End Sub

        Private Sub objDataGrid_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            Refresh()
        End Sub

        Private Sub objDataGrid_ColumnAdded(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            RegenerateHeaders()
            Refresh()
        End Sub

        Private Sub objDataGrid_ColumnRemoved(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
            RegenerateHeaders()
            Refresh()
        End Sub

        Private Sub objDataGrid_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
            iNoOfLevels = NoOfLevels(objHeaderTree)
            objGraphics = e.Graphics
            objDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            objDataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
            objDataGrid.ColumnHeadersHeight = iNoOfLevels * 17
            If objHeaderTree IsNot Nothing Then
                RenderColumnHeaders()
            End If
        End Sub

        Private Sub objDataGrid_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs)
            Dim Brush As New SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.BackColor)
            e.Graphics.FillRectangle(Brush, e.CellBounds)
            Brush.Dispose()
            e.Paint(e.CellBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.ContentBackground)

            ControlPaint.DrawBorder(e.Graphics, e.CellBounds, objDataGrid.GridColor, 1, ButtonBorderStyle.Solid, objDataGrid.GridColor, 1, ButtonBorderStyle.Solid, objDataGrid.GridColor, 1, ButtonBorderStyle.Solid, objDataGrid.GridColor, 1, ButtonBorderStyle.Solid)

            e.Handled = True
        End Sub

        Private Sub objDataGrid_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)
            Refresh()
        End Sub

        Private Sub Refresh()
            Dim rtHeader As Rectangle = objDataGrid.DisplayRectangle
            objDataGrid.Invalidate(rtHeader)
        End Sub

        Private Sub RegenerateHeaders()
            objHeaderTree = objStackedHeaderGenerator.GenerateStackedHeader(objDataGrid)
        End Sub

        Private Sub RenderColumnHeaders()
            objGraphics.FillRectangle(New SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.BackColor), New Rectangle(objDataGrid.DisplayRectangle.X, objDataGrid.DisplayRectangle.Y, objDataGrid.DisplayRectangle.Width, objDataGrid.ColumnHeadersHeight))

            For Each objChild As Header In objHeaderTree.Children
                objChild.Measure(objDataGrid, 0, objDataGrid.ColumnHeadersHeight / iNoOfLevels)

                objChild.AcceptRenderer(Me)
            Next
        End Sub

        Public Sub Render(ByVal objHeader As Header)
            If objHeader.Children.Count = 0 Then
                Dim r1 As Rectangle = objDataGrid.GetColumnDisplayRectangle(objHeader.ColumnId, True)
                If r1.Width = 0 Then
                    Return
                End If
                r1.Y = objHeader.Y
                r1.Width += 1
                r1.X -= 1
                r1.Height = objHeader.Height
                objGraphics.SetClip(r1)

                If r1.X + objDataGrid.Columns(objHeader.ColumnId).Width < objDataGrid.DisplayRectangle.Width Then
                    r1.X -= (objDataGrid.Columns(objHeader.ColumnId).Width - r1.Width)
                End If
                r1.X -= 1
                r1.Width = objDataGrid.Columns(objHeader.ColumnId).Width
                objGraphics.DrawRectangle(Pens.White, r1)
                objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font, New SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor), r1, objFormat)
                objGraphics.ResetClip()
            Else
                Dim x As Integer = objDataGrid.RowHeadersWidth - 41
                For i As Integer = 0 To objHeader.Children(0).ColumnId - 1
                    If objDataGrid.Columns(i).Visible Then
                        x += objDataGrid.Columns(i).Width
                    End If
                Next
                If x > (objDataGrid.HorizontalScrollingOffset + objDataGrid.DisplayRectangle.Width - 5) Then
                    Return
                End If

                'Rectangle r1 = objDataGrid.GetCellDisplayRectangle(objHeader.Children[0].ColumnId, -1, true);
                Dim r1 As Rectangle = objDataGrid.GetCellDisplayRectangle(objHeader.ColumnId, -1, True)
                r1.Y = objHeader.Y
                r1.Height = objHeader.Height
                r1.Width = objHeader.Width + 1
                If r1.X < objDataGrid.RowHeadersWidth Then
                    r1.X = objDataGrid.RowHeadersWidth
                End If
                r1.X -= 1
                objGraphics.SetClip(r1)
                r1.X = x - objDataGrid.HorizontalScrollingOffset
                r1.Width -= 1
                objGraphics.DrawRectangle(Pens.White, r1)
                r1.X -= 1
                objGraphics.DrawString(objHeader.Name, objDataGrid.ColumnHeadersDefaultCellStyle.Font, New SolidBrush(objDataGrid.ColumnHeadersDefaultCellStyle.ForeColor), r1, objFormat)
                objGraphics.ResetClip()
            End If
        End Sub

        Private Function NoOfLevels(ByVal header As Header) As Integer

            Dim level As Integer = 0
            For Each child As Header In header.Children
                Dim temp As Integer = NoOfLevels(child)
                level = If(temp > level, temp, level)
            Next
            Return level + 1
        End Function
    End Class
End Namespace
