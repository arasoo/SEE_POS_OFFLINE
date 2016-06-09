<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaseOnSupplier2
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.picLabel = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GridBaseOnSupplier = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSuppCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvConsi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colInvCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.picLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridBaseOnSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picLabel
        '
        Me.picLabel.Location = New System.Drawing.Point(28, 10)
        Me.picLabel.Name = "picLabel"
        Me.picLabel.Size = New System.Drawing.Size(24, 24)
        Me.picLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLabel.TabIndex = 515
        Me.picLabel.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(55, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(304, 23)
        Me.Label3.TabIndex = 514
        Me.Label3.Text = "BASE ON SUPPLIER - INVENTORY"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.BackColor = System.Drawing.Color.White
        Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExport.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExport.Location = New System.Drawing.Point(897, 137)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(67, 58)
        Me.btnExport.TabIndex = 511
        Me.btnExport.Text = "Export"
        Me.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.White
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRefresh.Location = New System.Drawing.Point(897, 77)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(67, 58)
        Me.btnRefresh.TabIndex = 510
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(897, 198)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 58)
        Me.btnClose.TabIndex = 509
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GridBaseOnSupplier
        '
        Me.GridBaseOnSupplier.AllowUserToAddRows = False
        Me.GridBaseOnSupplier.AllowUserToOrderColumns = True
        Me.GridBaseOnSupplier.AllowUserToResizeColumns = False
        Me.GridBaseOnSupplier.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridBaseOnSupplier.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridBaseOnSupplier.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridBaseOnSupplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.GridBaseOnSupplier.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.GridBaseOnSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GridBaseOnSupplier.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridBaseOnSupplier.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridBaseOnSupplier.ColumnHeadersHeight = 30
        Me.GridBaseOnSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridBaseOnSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSuppCode, Me.colName, Me.colInvConsi, Me.colInvCredit})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridBaseOnSupplier.DefaultCellStyle = DataGridViewCellStyle5
        Me.GridBaseOnSupplier.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridBaseOnSupplier.EnableHeadersVisualStyles = False
        Me.GridBaseOnSupplier.GridColor = System.Drawing.Color.Silver
        Me.GridBaseOnSupplier.Location = New System.Drawing.Point(16, 77)
        Me.GridBaseOnSupplier.MultiSelect = False
        Me.GridBaseOnSupplier.Name = "GridBaseOnSupplier"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridBaseOnSupplier.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.GridBaseOnSupplier.RowHeadersVisible = False
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridBaseOnSupplier.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GridBaseOnSupplier.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.GridBaseOnSupplier.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridBaseOnSupplier.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GridBaseOnSupplier.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.GridBaseOnSupplier.RowTemplate.Height = 25
        Me.GridBaseOnSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridBaseOnSupplier.Size = New System.Drawing.Size(874, 444)
        Me.GridBaseOnSupplier.TabIndex = 518
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn1.HeaderText = "Item"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 90
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn2.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 450
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        DataGridViewCellStyle8.NullValue = "0"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn3.HeaderText = "Product"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 250
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = "0"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn4.HeaderText = "Sales.Qty"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'colSuppCode
        '
        Me.colSuppCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colSuppCode.HeaderText = "Code"
        Me.colSuppCode.Name = "colSuppCode"
        Me.colSuppCode.Width = 90
        '
        'colName
        '
        Me.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.Width = 300
        '
        'colInvConsi
        '
        Me.colInvConsi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = "0"
        Me.colInvConsi.DefaultCellStyle = DataGridViewCellStyle3
        Me.colInvConsi.HeaderText = "Inventory.Consi"
        Me.colInvConsi.Name = "colInvConsi"
        '
        'colInvCredit
        '
        Me.colInvCredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.colInvCredit.DefaultCellStyle = DataGridViewCellStyle4
        Me.colInvCredit.HeaderText = "Inventory.Credit"
        Me.colInvCredit.Name = "colInvCredit"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        DataGridViewCellStyle10.NullValue = "0"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn6.HeaderText = "Stock"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        DataGridViewCellStyle11.NullValue = "0"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn5.HeaderText = "Sales.Amount"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 105
        '
        'frmBaseOnSupplier2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 531)
        Me.ControlBox = False
        Me.Controls.Add(Me.picLabel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GridBaseOnSupplier)
        Me.Name = "frmBaseOnSupplier2"
        Me.Text = "Base On Supplier - Inventory"
        CType(Me.picLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridBaseOnSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents picLabel As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents btnExport As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents GridBaseOnSupplier As DataGridView
    Friend WithEvents colSuppCode As DataGridViewTextBoxColumn
    Friend WithEvents colName As DataGridViewTextBoxColumn
    Friend WithEvents colInvConsi As DataGridViewTextBoxColumn
    Friend WithEvents colInvCredit As DataGridViewTextBoxColumn
End Class
