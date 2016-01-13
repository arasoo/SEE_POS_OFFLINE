Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace StackedHeader
    Public Class Header

        Public Property Children() As List(Of Header)
            Get
                Return m_Children
            End Get
            Set(ByVal value As List(Of Header))
                m_Children = value
            End Set
        End Property

        Private m_Children As List(Of Header)

        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(ByVal value As String)
                m_Name = value
            End Set
        End Property

        Private m_Name As String

        Public Property X() As Integer
            Get
                Return m_X
            End Get
            Set(ByVal value As Integer)
                m_X = value
            End Set
        End Property
        Private m_X As Integer

        Public Property Y() As Integer
            Get
                Return m_Y
            End Get
            Set(ByVal value As Integer)
                m_Y = value
            End Set
        End Property
        Private m_Y As Integer

        Public Property Width() As Integer
            Get
                Return m_Width
            End Get
            Set(ByVal value As Integer)
                m_Width = value
            End Set
        End Property
        Private m_Width As Integer

        Public Property Height() As Integer
            Get
                Return m_Height
            End Get
            Set(ByVal value As Integer)
                m_Height = value
            End Set
        End Property
        Private m_Height As Integer

        Public Property ColumnId() As Integer
            Get
                Return m_ColumnId
            End Get
            Set(ByVal value As Integer)
                m_ColumnId = value
            End Set
        End Property
        Private m_ColumnId As Integer

        Public Sub New()
            Name = String.Empty
            Children = New List(Of Header)()
            ColumnId = -1
        End Sub

        Public Sub Measure(ByVal objGrid As DataGridView, ByVal iY As Integer, ByVal iHeight As Integer)
            Width = 0
            If Children.Count > 0 Then
                Dim tempY As Integer = If(String.IsNullOrEmpty(Name.Trim()), iY, iY + iHeight)
                Dim columnWidthSet As Boolean = False
                For Each child As Header In Children
                    child.Measure(objGrid, tempY, iHeight)
                    Width += child.Width
                    If Not columnWidthSet AndAlso Width > 0 Then
                        ColumnId = child.ColumnId
                        columnWidthSet = True
                    End If
                Next
            ElseIf -1 <> ColumnId AndAlso objGrid.Columns(ColumnId).Visible Then
                Width = objGrid.Columns(ColumnId).Width
            End If
            Y = iY
            If Children.Count = 0 Then
                Height = objGrid.ColumnHeadersHeight - iY
            Else
                Height = iHeight
            End If
        End Sub

        'public void AcceptRenderer(StackedHeaderDecorator objRenderer, DataGridView objGrid, int iY)
        '{
        '    foreach (Header children in Children)
        '    {
        '        children.AcceptRenderer(objRenderer, objGrid, iY);
        '    }
        '    if (-1 != ColumnId && !string.IsNullOrEmpty(Name.Trim()))
        '    {
        '        objRenderer.Render(this);
        '    }

        '}

        Public Sub AcceptRenderer(ByVal objRenderer As StackedHeaderDecorator)
            For Each objChild As Header In Children
                objChild.AcceptRenderer(objRenderer)
            Next
            If -1 <> ColumnId AndAlso Not String.IsNullOrEmpty(Name.Trim()) Then
                objRenderer.Render(Me)
            End If

        End Sub
    End Class
End Namespace
