Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace StackedHeader
    Public Class StackedHeaderGenerator

        Implements IStackedHeaderGenerator

        Private Shared ReadOnly objInstance As StackedHeaderGenerator

        Shared Sub New()
            objInstance = New StackedHeaderGenerator()
        End Sub

        Public Shared ReadOnly Property Instance() As StackedHeaderGenerator
            Get
                Return objInstance
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Function GenerateStackedHeader(ByVal objGridView As DataGridView) As Header Implements IStackedHeaderGenerator.GenerateStackedHeader
            Dim objParentHeader As New Header()
            Dim objHeaderTree As New Dictionary(Of String, Header)()
            Dim iX As Integer = 0
            For Each objColumn As DataGridViewColumn In objGridView.Columns
                Dim segments As String() = objColumn.HeaderText.Split("."c)
                If segments.Length > 0 Then
                    Dim segment As String = segments(0)
                    Dim tempHeader As Header, lastTempHeader As Header = Nothing
                    If objHeaderTree.ContainsKey(segment) Then
                        tempHeader = objHeaderTree(segment)
                    Else
                        tempHeader = New Header() With { _
                         .Name = segment, _
                         .X = iX _
                        }
                        objParentHeader.Children.Add(tempHeader)
                        objHeaderTree(segment) = tempHeader
                        tempHeader.ColumnId = objColumn.Index
                    End If
                    For i As Integer = 1 To segments.Length - 1
                        segment = segments(i)
                        Dim found As Boolean = False
                        For Each child As Header In tempHeader.Children
                            If 0 = String.Compare(child.Name, segment, StringComparison.InvariantCultureIgnoreCase) Then
                                found = True
                                lastTempHeader = tempHeader
                                tempHeader = child
                                Exit For
                            End If
                        Next
                        If Not found OrElse i = segments.Length - 1 Then
                            Dim temp As New Header() With { _
                             .Name = segment, _
                             .X = iX _
                            }
                            temp.ColumnId = objColumn.Index
                            If found AndAlso i = segments.Length - 1 AndAlso lastTempHeader IsNot Nothing Then
                                lastTempHeader.Children.Add(temp)
                            Else
                                tempHeader.Children.Add(temp)
                            End If
                            tempHeader = temp
                        End If
                    Next
                End If
                iX += objColumn.Width
            Next
            Return objParentHeader
        End Function
    End Class
End Namespace
