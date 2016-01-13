<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMasterDisc
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.picTitle = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gridMasterDisc = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDiscGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRole = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbSalesOrgPOS = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbSalesOffice = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMasterDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picTitle
        '
        Me.picTitle.Location = New System.Drawing.Point(16, 9)
        Me.picTitle.Name = "picTitle"
        Me.picTitle.Size = New System.Drawing.Size(24, 24)
        Me.picTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTitle.TabIndex = 407
        Me.picTitle.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(43, 9)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(185, 23)
        Me.Label16.TabIndex = 406
        Me.Label16.Text = "MASTER DISCOUNT"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gridMasterDisc
        '
        Me.gridMasterDisc.AllowUserToAddRows = False
        Me.gridMasterDisc.AllowUserToOrderColumns = True
        Me.gridMasterDisc.AllowUserToResizeColumns = False
        Me.gridMasterDisc.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridMasterDisc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.gridMasterDisc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gridMasterDisc.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.gridMasterDisc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridMasterDisc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridMasterDisc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.gridMasterDisc.ColumnHeadersHeight = 30
        Me.gridMasterDisc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.gridMasterDisc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colDiscGroup, Me.colDescription, Me.colRole})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridMasterDisc.DefaultCellStyle = DataGridViewCellStyle8
        Me.gridMasterDisc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridMasterDisc.EnableHeadersVisualStyles = False
        Me.gridMasterDisc.GridColor = System.Drawing.Color.Silver
        Me.gridMasterDisc.Location = New System.Drawing.Point(12, 92)
        Me.gridMasterDisc.MultiSelect = False
        Me.gridMasterDisc.Name = "gridMasterDisc"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridMasterDisc.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.gridMasterDisc.RowHeadersVisible = False
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridMasterDisc.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.gridMasterDisc.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.gridMasterDisc.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridMasterDisc.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.gridMasterDisc.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.gridMasterDisc.RowTemplate.Height = 25
        Me.gridMasterDisc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridMasterDisc.Size = New System.Drawing.Size(372, 423)
        Me.gridMasterDisc.TabIndex = 408
        '
        'colNo
        '
        Me.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colNo.HeaderText = "No"
        Me.colNo.Name = "colNo"
        Me.colNo.Width = 40
        '
        'colDiscGroup
        '
        Me.colDiscGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colDiscGroup.HeaderText = "DiscGroup"
        Me.colDiscGroup.Name = "colDiscGroup"
        Me.colDiscGroup.Width = 60
        '
        'colDescription
        '
        Me.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Width = 200
        '
        'colRole
        '
        Me.colRole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colRole.HeaderText = "Role"
        Me.colRole.Name = "colRole"
        Me.colRole.Width = 50
        '
        'cmbSalesOrgPOS
        '
        Me.cmbSalesOrgPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSalesOrgPOS.DropDownHeight = 1
        Me.cmbSalesOrgPOS.DropDownWidth = 1
        Me.cmbSalesOrgPOS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrgPOS.FormattingEnabled = True
        Me.cmbSalesOrgPOS.IntegralHeight = False
        Me.cmbSalesOrgPOS.ItemHeight = 13
        Me.cmbSalesOrgPOS.Location = New System.Drawing.Point(16, 66)
        Me.cmbSalesOrgPOS.MaxDropDownItems = 1
        Me.cmbSalesOrgPOS.Name = "cmbSalesOrgPOS"
        Me.cmbSalesOrgPOS.Size = New System.Drawing.Size(212, 21)
        Me.cmbSalesOrgPOS.TabIndex = 413
        Me.cmbSalesOrgPOS.Tag = "POS SLSORG"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(13, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 18)
        Me.Label14.TabIndex = 412
        Me.Label14.Text = "Sales Org"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSalesOffice
        '
        Me.cmbSalesOffice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSalesOffice.DropDownHeight = 1
        Me.cmbSalesOffice.DropDownWidth = 1
        Me.cmbSalesOffice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOffice.FormattingEnabled = True
        Me.cmbSalesOffice.IntegralHeight = False
        Me.cmbSalesOffice.ItemHeight = 13
        Me.cmbSalesOffice.Location = New System.Drawing.Point(504, 94)
        Me.cmbSalesOffice.MaxDropDownItems = 1
        Me.cmbSalesOffice.Name = "cmbSalesOffice"
        Me.cmbSalesOffice.Size = New System.Drawing.Size(251, 21)
        Me.cmbSalesOffice.TabIndex = 415
        Me.cmbSalesOffice.Tag = "POS SALESOFFICE"
        '
        'Label25
        '
        Me.Label25.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(418, 94)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(80, 18)
        Me.Label25.TabIndex = 414
        Me.Label25.Text = "Sales Office :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmMasterDisc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(894, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbSalesOrgPOS)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cmbSalesOffice)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.gridMasterDisc)
        Me.Controls.Add(Me.picTitle)
        Me.Controls.Add(Me.Label16)
        Me.Name = "frmMasterDisc"
        Me.ShowInTaskbar = False
        Me.Text = "Master Discount"
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMasterDisc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picTitle As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents gridMasterDisc As System.Windows.Forms.DataGridView
    Friend WithEvents colNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDiscGroup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRole As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbSalesOrgPOS As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOffice As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
End Class
