Public Class DGV
    Inherits DataGridView
    Private Const WM_KEYDOWN As Integer = &H100
    Private Const WM_KEYUP As Integer = &H101
    Private Const WM_CHAR As Integer = &H102
    Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
        Dim keyCode As Keys = CType(msg.WParam.ToInt32(), Keys) And Keys.KeyCode
        ' for a datagrid, we need to eat the tab key oe else its done twice
        If msg.Msg = WM_KEYDOWN AndAlso keyCode = Keys.Enter Then
            Dim intRow As Integer = Me.CurrentCell.RowIndex
            Dim intCol As Integer = Me.CurrentCell.ColumnIndex
            intCol += 1
            If intCol = Me.ColumnCount Then
                intCol = 0
                intRow += 1
                If intRow = Me.RowCount Then
                    intRow = 0
                End If
            End If

            Me.CurrentCell = Me(intCol, intRow)
            Return True
        End If
        Return MyBase.PreProcessMessage(msg)
    End Function
End Class
Public Class NoEnterColumn
    Inherits DataGridViewColumn
    Public Sub New()
        MyBase.New(New NoEnterCell())
    End Sub
    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)
            ' Ensure that the cell used for the template is a CalendarCell.
            If Not (value Is Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(NoEnterCell)) _
                Then
                Throw New InvalidCastException("Must be a NoEnterCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property
End Class
Public Class NoEnterCell
    Inherits DataGridViewTextBoxCell
    Public Sub New()
        ' Use the short date format.
    End Sub
    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
        ByVal initialFormattedValue As Object, _
        ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
            dataGridViewCellStyle)

        Dim ctl As NoEnterEditingControl = _
            CType(DataGridView.EditingControl, NoEnterEditingControl)
        ctl.Text = Me.Value.ToString
    End Sub
    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that CalendarCell uses.
            Return GetType(NoEnterEditingControl)
        End Get
    End Property
    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that CalendarCell contains.
            Return GetType(String)
        End Get
    End Property
    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value.
            Return ""
        End Get
    End Property
End Class
Class NoEnterEditingControl
    Inherits NoEnterTB
    Implements IDataGridViewEditingControl
    Private dataGridViewControl As DataGridView
    Private valueIsChanged As Boolean = False
    Private rowIndexNum As Integer
    Public Property EditingControlFormattedValue() As Object _
        Implements IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return Me.Text
        End Get
        Set(ByVal value As Object)
            If TypeOf value Is [String] Then
                Me.Text = CStr(value)
            End If
        End Set
    End Property
    Public Function GetEditingControlFormattedValue(ByVal context _
        As DataGridViewDataErrorContexts) As Object _
        Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return Me.Text
    End Function
    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As  _
        DataGridViewCellStyle) _
        Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
        Me.Font = dataGridViewCellStyle.Font
        Me.ForeColor = dataGridViewCellStyle.ForeColor
        Me.BackColor = dataGridViewCellStyle.BackColor
    End Sub
    Public Property EditingControlRowIndex() As Integer _
        Implements IDataGridViewEditingControl.EditingControlRowIndex

        Get
            Return rowIndexNum
        End Get
        Set(ByVal value As Integer)
            rowIndexNum = value
        End Set
    End Property
    Public Function EditingControlWantsInputKey(ByVal key As Keys, _
        ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements IDataGridViewEditingControl.EditingControlWantsInputKey
        Return True
    End Function
    Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
        Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
        ' No preparation needs to be done.
    End Sub
    Public ReadOnly Property RepositionEditingControlOnValueChange() _
        As Boolean Implements _
        IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property
    Public Property EditingControlDataGridView() As DataGridView _
        Implements IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return dataGridViewControl
        End Get
        Set(ByVal value As DataGridView)
            dataGridViewControl = value
        End Set
    End Property
    Public Property EditingControlValueChanged() As Boolean _
        Implements IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return valueIsChanged
        End Get
        Set(ByVal value As Boolean)
            valueIsChanged = value
        End Set
    End Property
    Public ReadOnly Property EditingControlCursor() As Cursor _
        Implements IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return MyBase.Cursor
        End Get
    End Property
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        '    Notify the DataGridView that the contents of the cell have changed.
        valueIsChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnTextChanged(e)
    End Sub
End Class
Public Class NoEnterTB
    Inherits TextBox
    Private Const WM_KEYDOWN As Integer = &H100
    Private Const WM_KEYUP As Integer = &H101
    Private Const WM_CHAR As Integer = &H102
    Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
        Dim keyCode As Keys = CType(msg.WParam.ToInt32(), Keys) And Keys.KeyCode
        If msg.Msg = WM_KEYDOWN AndAlso keyCode = Keys.Enter Then
            msg.WParam = CType(Keys.Tab, IntPtr)
        End If
        Return MyBase.PreProcessMessage(msg)
    End Function
End Class