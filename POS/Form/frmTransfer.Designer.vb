<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTransfer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dtDate = New System.Windows.Forms.DateTimePicker()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateXMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chckScan = New System.Windows.Forms.CheckBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.picLabel = New System.Windows.Forms.PictureBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.cmbToWH = New System.Windows.Forms.ComboBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.cmbFromWH = New System.Windows.Forms.ComboBox()
        Me.gridAll = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.lblTotalItem = New System.Windows.Forms.Label()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.gridTransfer = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProduct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtReffDoc = New System.Windows.Forms.TextBox()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.picLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtDate
        '
        Me.dtDate.CustomFormat = "dd MMMM yyyy"
        Me.dtDate.Enabled = False
        Me.dtDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDate.Location = New System.Drawing.Point(99, 81)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.Size = New System.Drawing.Size(153, 21)
        Me.dtDate.TabIndex = 0
        '
        'txtNote
        '
        Me.txtNote.BackColor = System.Drawing.Color.White
        Me.txtNote.Enabled = False
        Me.txtNote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNote.Location = New System.Drawing.Point(99, 164)
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(444, 21)
        Me.txtNote.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(29, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 23)
        Me.Label5.TabIndex = 228
        Me.Label5.Text = "Note :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(29, 81)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 23)
        Me.Label6.TabIndex = 224
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.ToolStripMenuItem1, Me.CreateXMLToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(136, 76)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(132, 6)
        '
        'CreateXMLToolStripMenuItem
        '
        Me.CreateXMLToolStripMenuItem.Name = "CreateXMLToolStripMenuItem"
        Me.CreateXMLToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.CreateXMLToolStripMenuItem.Text = "Create XML"
        '
        'lblDocNo
        '
        Me.lblDocNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblDocNo.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.ForeColor = System.Drawing.Color.Black
        Me.lblDocNo.Location = New System.Drawing.Point(99, 48)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(213, 23)
        Me.lblDocNo.TabIndex = 223
        Me.lblDocNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(14, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 30)
        Me.Label4.TabIndex = 222
        Me.Label4.Text = "Document :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.Khaki
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(333, 538)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 23)
        Me.Label10.TabIndex = 240
        Me.Label10.Text = "Qty :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtQty
        '
        Me.txtQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtQty.Enabled = False
        Me.txtQty.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(375, 537)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(47, 21)
        Me.txtQty.TabIndex = 239
        Me.txtQty.Text = "1"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Khaki
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(20, 536)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 23)
        Me.Label7.TabIndex = 238
        Me.Label7.Text = "Item Code :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtItem
        '
        Me.txtItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtItem.BackColor = System.Drawing.Color.White
        Me.txtItem.Enabled = False
        Me.txtItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.Location = New System.Drawing.Point(109, 537)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(191, 21)
        Me.txtItem.TabIndex = 237
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.Khaki
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Font = New System.Drawing.Font("Comic Sans MS", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 531)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(760, 36)
        Me.Label11.TabIndex = 242
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chckScan
        '
        Me.chckScan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chckScan.AutoSize = True
        Me.chckScan.BackColor = System.Drawing.Color.Khaki
        Me.chckScan.Enabled = False
        Me.chckScan.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckScan.ForeColor = System.Drawing.Color.Black
        Me.chckScan.Location = New System.Drawing.Point(602, 542)
        Me.chckScan.Name = "chckScan"
        Me.chckScan.Size = New System.Drawing.Size(158, 17)
        Me.chckScan.TabIndex = 247
        Me.chckScan.Text = "Scan item one by one (F10)"
        Me.chckScan.UseVisualStyleBackColor = False
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.Color.Black
        Me.lblFrom.Location = New System.Drawing.Point(17, 109)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(76, 23)
        Me.lblFrom.TabIndex = 323
        Me.lblFrom.Text = "From WH :"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picLabel
        '
        Me.picLabel.Location = New System.Drawing.Point(25, 9)
        Me.picLabel.Name = "picLabel"
        Me.picLabel.Size = New System.Drawing.Size(24, 24)
        Me.picLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLabel.TabIndex = 398
        Me.picLabel.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(52, 11)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(268, 19)
        Me.lblTitle.TabIndex = 397
        Me.lblTitle.Text = "WAREHOUSE STOCK TRANSFER"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbToWH
        '
        Me.cmbToWH.DropDownHeight = 1
        Me.cmbToWH.DropDownWidth = 1
        Me.cmbToWH.Enabled = False
        Me.cmbToWH.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbToWH.FormattingEnabled = True
        Me.cmbToWH.IntegralHeight = False
        Me.cmbToWH.Location = New System.Drawing.Point(366, 108)
        Me.cmbToWH.MaxDropDownItems = 1
        Me.cmbToWH.Name = "cmbToWH"
        Me.cmbToWH.Size = New System.Drawing.Size(197, 21)
        Me.cmbToWH.TabIndex = 405
        Me.cmbToWH.Tag = "TO WH"
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.Color.Black
        Me.lblTo.Location = New System.Drawing.Point(304, 108)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(61, 23)
        Me.lblTo.TabIndex = 406
        Me.lblTo.Text = "To :"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFromWH
        '
        Me.cmbFromWH.DropDownHeight = 1
        Me.cmbFromWH.DropDownWidth = 1
        Me.cmbFromWH.Enabled = False
        Me.cmbFromWH.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFromWH.FormattingEnabled = True
        Me.cmbFromWH.IntegralHeight = False
        Me.cmbFromWH.Location = New System.Drawing.Point(99, 108)
        Me.cmbFromWH.MaxDropDownItems = 1
        Me.cmbFromWH.Name = "cmbFromWH"
        Me.cmbFromWH.Size = New System.Drawing.Size(197, 21)
        Me.cmbFromWH.TabIndex = 404
        Me.cmbFromWH.Tag = "FROM WH"
        '
        'gridAll
        '
        Me.gridAll.AllowUserToAddRows = False
        Me.gridAll.AllowUserToOrderColumns = True
        Me.gridAll.AllowUserToResizeColumns = False
        Me.gridAll.AllowUserToResizeRows = False
        DataGridViewCellStyle44.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle44.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle44
        Me.gridAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridAll.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.gridAll.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.gridAll.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle45.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle45.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridAll.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle45
        Me.gridAll.ColumnHeadersHeight = 25
        Me.gridAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle46.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle46.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle46.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridAll.DefaultCellStyle = DataGridViewCellStyle46
        Me.gridAll.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridAll.EnableHeadersVisualStyles = False
        Me.gridAll.GridColor = System.Drawing.Color.Silver
        Me.gridAll.Location = New System.Drawing.Point(219, 247)
        Me.gridAll.MultiSelect = False
        Me.gridAll.Name = "gridAll"
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle47.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle47.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle47.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridAll.RowHeadersDefaultCellStyle = DataGridViewCellStyle47
        Me.gridAll.RowHeadersVisible = False
        DataGridViewCellStyle48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.RowsDefaultCellStyle = DataGridViewCellStyle48
        Me.gridAll.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.gridAll.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.gridAll.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.gridAll.RowTemplate.Height = 20
        Me.gridAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridAll.Size = New System.Drawing.Size(254, 111)
        Me.gridAll.TabIndex = 407
        Me.gridAll.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(782, 369)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 10)
        Me.Label2.TabIndex = 425
        Me.Label2.Text = "F5"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(782, 429)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(18, 10)
        Me.Label3.TabIndex = 424
        Me.Label3.Text = "Esc"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(782, 311)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 10)
        Me.Label8.TabIndex = 422
        Me.Label8.Text = "F3"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(782, 194)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 10)
        Me.Label14.TabIndex = 420
        Me.Label14.Text = "F1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.SkyBlue
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(778, 425)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 58)
        Me.btnClose.TabIndex = 419
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.BackColor = System.Drawing.Color.SkyBlue
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEdit.Enabled = False
        Me.btnEdit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.Black
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEdit.Location = New System.Drawing.Point(778, 308)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 58)
        Me.btnEdit.TabIndex = 415
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.SkyBlue
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(778, 366)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 58)
        Me.btnSave.TabIndex = 417
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.SkyBlue
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(778, 190)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(67, 58)
        Me.btnNew.TabIndex = 413
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'lblTotalItem
        '
        Me.lblTotalItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalItem.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItem.ForeColor = System.Drawing.Color.Black
        Me.lblTotalItem.Location = New System.Drawing.Point(709, 141)
        Me.lblTotalItem.Name = "lblTotalItem"
        Me.lblTotalItem.Size = New System.Drawing.Size(63, 23)
        Me.lblTotalItem.TabIndex = 429
        Me.lblTotalItem.Text = "0"
        Me.lblTotalItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalQty
        '
        Me.lblTotalQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalQty.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalQty.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQty.ForeColor = System.Drawing.Color.Black
        Me.lblTotalQty.Location = New System.Drawing.Point(712, 163)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(60, 23)
        Me.lblTotalQty.TabIndex = 427
        Me.lblTotalQty.Text = "0"
        Me.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(638, 139)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 23)
        Me.Label9.TabIndex = 428
        Me.Label9.Text = "Item :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(642, 162)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 23)
        Me.Label12.TabIndex = 426
        Me.Label12.Text = "Qty : "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gridTransfer
        '
        Me.gridTransfer.AllowUserToAddRows = False
        Me.gridTransfer.AllowUserToOrderColumns = True
        Me.gridTransfer.AllowUserToResizeColumns = False
        Me.gridTransfer.AllowUserToResizeRows = False
        DataGridViewCellStyle49.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridTransfer.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle49
        Me.gridTransfer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridTransfer.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.gridTransfer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridTransfer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.gridTransfer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle50.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle50.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridTransfer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle50
        Me.gridTransfer.ColumnHeadersHeight = 30
        Me.gridTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.gridTransfer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colItem, Me.colDescription, Me.colProduct, Me.colUOM, Me.colQty})
        Me.gridTransfer.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle53.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle53.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle53.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle53.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridTransfer.DefaultCellStyle = DataGridViewCellStyle53
        Me.gridTransfer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridTransfer.Enabled = False
        Me.gridTransfer.EnableHeadersVisualStyles = False
        Me.gridTransfer.GridColor = System.Drawing.Color.Silver
        Me.gridTransfer.Location = New System.Drawing.Point(12, 190)
        Me.gridTransfer.MultiSelect = False
        Me.gridTransfer.Name = "gridTransfer"
        Me.gridTransfer.RowHeadersVisible = False
        Me.gridTransfer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.gridTransfer.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridTransfer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.gridTransfer.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.gridTransfer.RowTemplate.Height = 25
        Me.gridTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridTransfer.Size = New System.Drawing.Size(760, 338)
        Me.gridTransfer.TabIndex = 430
        '
        'colNo
        '
        DataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colNo.DefaultCellStyle = DataGridViewCellStyle51
        Me.colNo.Frozen = True
        Me.colNo.HeaderText = "No."
        Me.colNo.Name = "colNo"
        Me.colNo.Width = 40
        '
        'colItem
        '
        Me.colItem.Frozen = True
        Me.colItem.HeaderText = "Item Code"
        Me.colItem.Name = "colItem"
        Me.colItem.Width = 110
        '
        'colDescription
        '
        Me.colDescription.Frozen = True
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Width = 500
        '
        'colProduct
        '
        Me.colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colProduct.HeaderText = "Product"
        Me.colProduct.Name = "colProduct"
        Me.colProduct.Width = 50
        '
        'colUOM
        '
        Me.colUOM.HeaderText = "UOM"
        Me.colUOM.Name = "colUOM"
        Me.colUOM.Width = 50
        '
        'colQty
        '
        DataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colQty.DefaultCellStyle = DataGridViewCellStyle52
        Me.colQty.HeaderText = "Qty"
        Me.colQty.Name = "colQty"
        Me.colQty.Width = 50
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(782, 253)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(15, 10)
        Me.Label13.TabIndex = 434
        Me.Label13.Text = "F2"
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.BackColor = System.Drawing.Color.SkyBlue
        Me.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBrowse.Location = New System.Drawing.Point(778, 249)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(67, 58)
        Me.btnBrowse.TabIndex = 433
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle39
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Item Code"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 110
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 470
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn4.HeaderText = "UOM"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 50
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle40
        Me.DataGridViewTextBoxColumn5.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle43
        Me.DataGridViewTextBoxColumn6.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 50
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(29, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 23)
        Me.Label1.TabIndex = 436
        Me.Label1.Text = "Reff Doc :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReffDoc
        '
        Me.txtReffDoc.BackColor = System.Drawing.Color.White
        Me.txtReffDoc.Enabled = False
        Me.txtReffDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReffDoc.Location = New System.Drawing.Point(99, 137)
        Me.txtReffDoc.Name = "txtReffDoc"
        Me.txtReffDoc.Size = New System.Drawing.Size(199, 21)
        Me.txtReffDoc.TabIndex = 435
        '
        'btnUpload
        '
        Me.btnUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpload.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpload.Enabled = False
        Me.btnUpload.Location = New System.Drawing.Point(575, 162)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(71, 22)
        Me.btnUpload.TabIndex = 437
        Me.btnUpload.Text = "Upload"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'frmTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(857, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.gridAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtReffDoc)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.cmbToWH)
        Me.Controls.Add(Me.lblTotalItem)
        Me.Controls.Add(Me.lblTotalQty)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.cmbFromWH)
        Me.Controls.Add(Me.picLabel)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chckScan)
        Me.Controls.Add(Me.lblDocNo)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.dtDate)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtItem)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.gridTransfer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmTransfer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Warehouse Stock Transfer"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.picLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblDocNo As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chckScan As System.Windows.Forms.CheckBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents picLabel As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents cmbToWH As System.Windows.Forms.ComboBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents cmbFromWH As System.Windows.Forms.ComboBox
    Friend WithEvents gridAll As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblTotalItem As System.Windows.Forms.Label
    Friend WithEvents lblTotalQty As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents gridTransfer As System.Windows.Forms.DataGridView
    Friend WithEvents colNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProduct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateXMLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtReffDoc As TextBox
    Friend WithEvents btnUpload As Button
End Class
