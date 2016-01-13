<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListVoucher
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GridVoucher = New System.Windows.Forms.DataGridView()
        Me.contextVoucher = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.removeVoucher = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVoucherId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDisc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.GridVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.contextVoucher.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridVoucher
        '
        Me.GridVoucher.AllowUserToAddRows = False
        Me.GridVoucher.AllowUserToOrderColumns = True
        Me.GridVoucher.AllowUserToResizeColumns = False
        Me.GridVoucher.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridVoucher.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridVoucher.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.GridVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GridVoucher.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridVoucher.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridVoucher.ColumnHeadersHeight = 30
        Me.GridVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridVoucher.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colVoucherId, Me.colItem, Me.colDisc})
        Me.GridVoucher.ContextMenuStrip = Me.contextVoucher
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridVoucher.DefaultCellStyle = DataGridViewCellStyle5
        Me.GridVoucher.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.GridVoucher.EnableHeadersVisualStyles = False
        Me.GridVoucher.GridColor = System.Drawing.Color.Silver
        Me.GridVoucher.Location = New System.Drawing.Point(-2, -2)
        Me.GridVoucher.MultiSelect = False
        Me.GridVoucher.Name = "GridVoucher"
        Me.GridVoucher.RowHeadersVisible = False
        Me.GridVoucher.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.GridVoucher.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridVoucher.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GridVoucher.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.GridVoucher.RowTemplate.Height = 25
        Me.GridVoucher.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.GridVoucher.Size = New System.Drawing.Size(501, 268)
        Me.GridVoucher.TabIndex = 415
        '
        'contextVoucher
        '
        Me.contextVoucher.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeVoucher})
        Me.contextVoucher.Name = "contextVoucher"
        Me.contextVoucher.Size = New System.Drawing.Size(125, 26)
        '
        'removeVoucher
        '
        Me.removeVoucher.Name = "removeVoucher"
        Me.removeVoucher.Size = New System.Drawing.Size(124, 22)
        Me.removeVoucher.Text = "Remove"
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Voucher Code"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.Width = 300
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn3.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'colNo
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colNo.DefaultCellStyle = DataGridViewCellStyle3
        Me.colNo.Frozen = True
        Me.colNo.HeaderText = "No."
        Me.colNo.Name = "colNo"
        Me.colNo.ReadOnly = True
        Me.colNo.Width = 40
        '
        'colVoucherId
        '
        Me.colVoucherId.Frozen = True
        Me.colVoucherId.HeaderText = "Voucher Id"
        Me.colVoucherId.Name = "colVoucherId"
        '
        'colItem
        '
        Me.colItem.Frozen = True
        Me.colItem.HeaderText = "Voucher Code"
        Me.colItem.Name = "colItem"
        Me.colItem.ReadOnly = True
        Me.colItem.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colItem.Width = 250
        '
        'colDisc
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.colDisc.DefaultCellStyle = DataGridViewCellStyle4
        Me.colDisc.HeaderText = "Amount"
        Me.colDisc.Name = "colDisc"
        '
        'frmListVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 268)
        Me.Controls.Add(Me.GridVoucher)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmListVoucher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Voucher"
        CType(Me.GridVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.contextVoucher.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents contextVoucher As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents removeVoucher As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVoucherId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDisc As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
