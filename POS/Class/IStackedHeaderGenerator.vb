Imports System.Windows.Forms

Namespace StackedHeader

    Public Interface IStackedHeaderGenerator
        Function GenerateStackedHeader(ByVal objGridView As DataGridView) As Header
    End Interface

End Namespace