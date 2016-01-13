<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMasterEvent
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.gridEventDetail = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDisc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GridEventHeader = New System.Windows.Forms.DataGridView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.picTitle = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAbort = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbSalesOrg = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gridAll = New System.Windows.Forms.DataGridView()
        Me.colPromoID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiscType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAssign = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPeriodFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPeriodTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFilter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colValidFlag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCloseFlag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAutoGenerate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMinPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCreateUser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCreateDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.gridEventDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEventHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gridEventDetail
        '
        Me.gridEventDetail.AllowUserToAddRows = False
        Me.gridEventDetail.AllowUserToOrderColumns = True
        Me.gridEventDetail.AllowUserToResizeColumns = False
        Me.gridEventDetail.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridEventDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.gridEventDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridEventDetail.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.gridEventDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.gridEventDetail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridEventDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.gridEventDetail.ColumnHeadersHeight = 30
        Me.gridEventDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.gridEventDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colItem, Me.DataGridViewTextBoxColumn1, Me.colDisc})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridEventDetail.DefaultCellStyle = DataGridViewCellStyle4
        Me.gridEventDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridEventDetail.EnableHeadersVisualStyles = False
        Me.gridEventDetail.GridColor = System.Drawing.Color.Silver
        Me.gridEventDetail.Location = New System.Drawing.Point(12, 320)
        Me.gridEventDetail.MultiSelect = False
        Me.gridEventDetail.Name = "gridEventDetail"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridEventDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.gridEventDetail.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridEventDetail.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.gridEventDetail.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.gridEventDetail.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridEventDetail.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.gridEventDetail.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.gridEventDetail.RowTemplate.Height = 25
        Me.gridEventDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridEventDetail.Size = New System.Drawing.Size(825, 223)
        Me.gridEventDetail.TabIndex = 356
        '
        'colNo
        '
        Me.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colNo.HeaderText = "No."
        Me.colNo.Name = "colNo"
        Me.colNo.Width = 40
        '
        'colItem
        '
        Me.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colItem.HeaderText = "Item"
        Me.colItem.Name = "colItem"
        Me.colItem.Width = 140
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn1.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 400
        '
        'colDisc
        '
        Me.colDisc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.colDisc.DefaultCellStyle = DataGridViewCellStyle3
        Me.colDisc.HeaderText = "Disc"
        Me.colDisc.Name = "colDisc"
        Me.colDisc.Width = 50
        '
        'GridEventHeader
        '
        Me.GridEventHeader.AllowUserToAddRows = False
        Me.GridEventHeader.AllowUserToOrderColumns = True
        Me.GridEventHeader.AllowUserToResizeColumns = False
        Me.GridEventHeader.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEventHeader.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GridEventHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEventHeader.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.GridEventHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GridEventHeader.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridEventHeader.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.GridEventHeader.ColumnHeadersHeight = 30
        Me.GridEventHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridEventHeader.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPromoID, Me.colDescription, Me.colNote, Me.DiscType, Me.colAssign, Me.colPeriodFrom, Me.colPeriodTo, Me.colFilter, Me.colValidFlag, Me.colCloseFlag, Me.colAutoGenerate, Me.colMinPayment, Me.colCreateUser, Me.colCreateDate})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridEventHeader.DefaultCellStyle = DataGridViewCellStyle10
        Me.GridEventHeader.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.GridEventHeader.EnableHeadersVisualStyles = False
        Me.GridEventHeader.GridColor = System.Drawing.Color.Silver
        Me.GridEventHeader.Location = New System.Drawing.Point(12, 74)
        Me.GridEventHeader.MultiSelect = False
        Me.GridEventHeader.Name = "GridEventHeader"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridEventHeader.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.GridEventHeader.RowHeadersVisible = False
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEventHeader.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.GridEventHeader.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.GridEventHeader.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEventHeader.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.GridEventHeader.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.GridEventHeader.RowTemplate.Height = 25
        Me.GridEventHeader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridEventHeader.Size = New System.Drawing.Size(825, 240)
        Me.GridEventHeader.TabIndex = 355
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.SkyBlue
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(843, 306)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 58)
        Me.btnClose.TabIndex = 358
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'picTitle
        '
        Me.picTitle.Location = New System.Drawing.Point(24, 9)
        Me.picTitle.Name = "picTitle"
        Me.picTitle.Size = New System.Drawing.Size(24, 24)
        Me.picTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTitle.TabIndex = 405
        Me.picTitle.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(51, 9)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(262, 23)
        Me.Label16.TabIndex = 404
        Me.Label16.Text = "EVENT PROMO && BEST PRICE"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbStatus
        '
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Open", "Close"})
        Me.cmbStatus.Location = New System.Drawing.Point(412, 47)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(56, 21)
        Me.cmbStatus.TabIndex = 406
        Me.cmbStatus.Text = "Open"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(355, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 23)
        Me.Label10.TabIndex = 407
        Me.Label10.Text = "Status :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.UseCompatibleTextRendering = True
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.BackColor = System.Drawing.Color.White
        Me.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnValidate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.ForeColor = System.Drawing.Color.Black
        Me.btnValidate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnValidate.Location = New System.Drawing.Point(843, 247)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(67, 58)
        Me.btnValidate.TabIndex = 411
        Me.btnValidate.Text = "Validate"
        Me.btnValidate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(847, 192)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(15, 10)
        Me.Label11.TabIndex = 417
        Me.Label11.Text = "F5"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(847, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 10)
        Me.Label1.TabIndex = 415
        Me.Label1.Text = "F1"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(847, 133)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 10)
        Me.Label8.TabIndex = 416
        Me.Label8.Text = "F2"
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.SkyBlue
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNew.Location = New System.Drawing.Point(843, 70)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(67, 58)
        Me.btnNew.TabIndex = 412
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.BackColor = System.Drawing.Color.SkyBlue
        Me.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEdit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEdit.Location = New System.Drawing.Point(843, 129)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 58)
        Me.btnEdit.TabIndex = 413
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnAbort
        '
        Me.btnAbort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAbort.BackColor = System.Drawing.Color.SkyBlue
        Me.btnAbort.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAbort.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnAbort.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbort.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAbort.Location = New System.Drawing.Point(843, 188)
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Size = New System.Drawing.Size(67, 58)
        Me.btnAbort.TabIndex = 414
        Me.btnAbort.Text = "Abort"
        Me.btnAbort.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAbort.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(845, 309)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 10)
        Me.Label2.TabIndex = 418
        Me.Label2.Text = "Esc"
        '
        'cmbSalesOrg
        '
        Me.cmbSalesOrg.DropDownHeight = 1
        Me.cmbSalesOrg.DropDownWidth = 1
        Me.cmbSalesOrg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesOrg.FormattingEnabled = True
        Me.cmbSalesOrg.IntegralHeight = False
        Me.cmbSalesOrg.ItemHeight = 13
        Me.cmbSalesOrg.Location = New System.Drawing.Point(80, 47)
        Me.cmbSalesOrg.MaxDropDownItems = 1
        Me.cmbSalesOrg.Name = "cmbSalesOrg"
        Me.cmbSalesOrg.Size = New System.Drawing.Size(251, 21)
        Me.cmbSalesOrg.TabIndex = 420
        Me.cmbSalesOrg.Tag = "POS SLSORG"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(5, 47)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 18)
        Me.Label14.TabIndex = 419
        Me.Label14.Text = "Sales Org :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gridAll
        '
        Me.gridAll.AllowUserToAddRows = False
        Me.gridAll.AllowUserToOrderColumns = True
        Me.gridAll.AllowUserToResizeColumns = False
        Me.gridAll.AllowUserToResizeRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.gridAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridAll.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.gridAll.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.gridAll.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridAll.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.gridAll.ColumnHeadersHeight = 25
        Me.gridAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridAll.DefaultCellStyle = DataGridViewCellStyle15
        Me.gridAll.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.gridAll.EnableHeadersVisualStyles = False
        Me.gridAll.GridColor = System.Drawing.Color.Silver
        Me.gridAll.Location = New System.Drawing.Point(531, 167)
        Me.gridAll.MultiSelect = False
        Me.gridAll.Name = "gridAll"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Khaki
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridAll.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.gridAll.RowHeadersVisible = False
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.RowsDefaultCellStyle = DataGridViewCellStyle17
        Me.gridAll.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.gridAll.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridAll.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue
        Me.gridAll.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.gridAll.RowTemplate.Height = 20
        Me.gridAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridAll.Size = New System.Drawing.Size(285, 111)
        Me.gridAll.TabIndex = 421
        Me.gridAll.Visible = False
        '
        'colPromoID
        '
        Me.colPromoID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colPromoID.Frozen = True
        Me.colPromoID.HeaderText = "Promo Id"
        Me.colPromoID.Name = "colPromoID"
        Me.colPromoID.Width = 120
        '
        'colDescription
        '
        Me.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colDescription.Frozen = True
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.Width = 300
        '
        'colNote
        '
        Me.colNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colNote.Frozen = True
        Me.colNote.HeaderText = "Note"
        Me.colNote.Name = "colNote"
        Me.colNote.Width = 200
        '
        'DiscType
        '
        Me.DiscType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DiscType.HeaderText = "Disc Type"
        Me.DiscType.Name = "DiscType"
        Me.DiscType.Width = 50
        '
        'colAssign
        '
        Me.colAssign.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAssign.HeaderText = "Assign To"
        Me.colAssign.Name = "colAssign"
        Me.colAssign.Width = 50
        '
        'colPeriodFrom
        '
        Me.colPeriodFrom.HeaderText = "Period From"
        Me.colPeriodFrom.Name = "colPeriodFrom"
        '
        'colPeriodTo
        '
        Me.colPeriodTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colPeriodTo.HeaderText = "Period To"
        Me.colPeriodTo.Name = "colPeriodTo"
        '
        'colFilter
        '
        Me.colFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFilter.HeaderText = "Filter"
        Me.colFilter.Name = "colFilter"
        Me.colFilter.Width = 50
        '
        'colValidFlag
        '
        Me.colValidFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colValidFlag.HeaderText = "Valid"
        Me.colValidFlag.Name = "colValidFlag"
        Me.colValidFlag.Width = 50
        '
        'colCloseFlag
        '
        Me.colCloseFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colCloseFlag.HeaderText = "Close"
        Me.colCloseFlag.Name = "colCloseFlag"
        Me.colCloseFlag.Width = 50
        '
        'colAutoGenerate
        '
        Me.colAutoGenerate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAutoGenerate.HeaderText = "Auto Generate"
        Me.colAutoGenerate.Name = "colAutoGenerate"
        Me.colAutoGenerate.Width = 60
        '
        'colMinPayment
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = "0"
        Me.colMinPayment.DefaultCellStyle = DataGridViewCellStyle9
        Me.colMinPayment.HeaderText = "Min. Payment"
        Me.colMinPayment.Name = "colMinPayment"
        '
        'colCreateUser
        '
        Me.colCreateUser.HeaderText = "Created by"
        Me.colCreateUser.Name = "colCreateUser"
        '
        'colCreateDate
        '
        Me.colCreateDate.HeaderText = "Created at"
        Me.colCreateDate.Name = "colCreateDate"
        '
        'frmMasterEvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(922, 555)
        Me.ControlBox = False
        Me.Controls.Add(Me.gridAll)
        Me.Controls.Add(Me.cmbSalesOrg)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAbort)
        Me.Controls.Add(Me.btnValidate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.picTitle)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gridEventDetail)
        Me.Controls.Add(Me.GridEventHeader)
        Me.KeyPreview = True
        Me.Name = "frmMasterEvent"
        Me.ShowInTaskbar = False
        Me.Text = "Event Promo & BEST PRICE"
        CType(Me.gridEventDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEventHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gridEventDetail As System.Windows.Forms.DataGridView
    Friend WithEvents GridEventHeader As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents picTitle As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnAbort As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOrg As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gridAll As System.Windows.Forms.DataGridView
    Friend WithEvents colNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDisc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPromoID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNote As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiscType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAssign As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPeriodFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPeriodTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFilter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colValidFlag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCloseFlag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAutoGenerate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMinPayment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCreateUser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCreateDate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
