<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayment
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
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblPaid = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.cashGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCashAmount = New System.Windows.Forms.TextBox()
        Me.cardGroupBox = New System.Windows.Forms.GroupBox()
        Me.cmbEDC = New System.Windows.Forms.ComboBox()
        Me.cmbCardType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCharge = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtApproval = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCardName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCardNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCardAmount = New System.Windows.Forms.TextBox()
        Me.btnCash = New System.Windows.Forms.Button()
        Me.btnCard = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.voucherGroupBox = New System.Windows.Forms.GroupBox()
        Me.btnViewListVoucher = New System.Windows.Forms.Button()
        Me.btnAddVoucher = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtVoucherCode = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtVoucherAmount = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnVoucher = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.cashGroupBox.SuspendLayout()
        Me.cardGroupBox.SuspendLayout()
        Me.voucherGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSubTotal
        '
        Me.lblSubTotal.BackColor = System.Drawing.Color.SteelBlue
        Me.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSubTotal.Font = New System.Drawing.Font("Tahoma", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTotal.ForeColor = System.Drawing.Color.White
        Me.lblSubTotal.Location = New System.Drawing.Point(-1, -2)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(550, 72)
        Me.lblSubTotal.TabIndex = 316
        Me.lblSubTotal.Text = "0.00"
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.SteelBlue
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 47)
        Me.Label2.TabIndex = 319
        Me.Label2.Text = "Total :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.Black
        Me.Label9.Font = New System.Drawing.Font("Comic Sans MS", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(8, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(532, 3)
        Me.Label9.TabIndex = 331
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.ClearToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 48)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        Me.ClearToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ClearToolStripMenuItem.Text = "Clear"
        '
        'lblPaid
        '
        Me.lblPaid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPaid.BackColor = System.Drawing.Color.Coral
        Me.lblPaid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaid.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaid.ForeColor = System.Drawing.Color.White
        Me.lblPaid.Location = New System.Drawing.Point(-1, 547)
        Me.lblPaid.Name = "lblPaid"
        Me.lblPaid.Size = New System.Drawing.Size(268, 47)
        Me.lblPaid.TabIndex = 352
        Me.lblPaid.Text = "0.00"
        Me.lblPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.Coral
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(3, 547)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 25)
        Me.Label8.TabIndex = 353
        Me.Label8.Text = "Paid :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Coral
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(273, 547)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 25)
        Me.Label1.TabIndex = 350
        Me.Label1.Text = "Remain :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblChange
        '
        Me.lblChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblChange.BackColor = System.Drawing.Color.Coral
        Me.lblChange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChange.Font = New System.Drawing.Font("Tahoma", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChange.ForeColor = System.Drawing.Color.White
        Me.lblChange.Location = New System.Drawing.Point(271, 547)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(277, 47)
        Me.lblChange.TabIndex = 349
        Me.lblChange.Text = "0.00"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(473, 348)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 58)
        Me.btnSave.TabIndex = 347
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(473, 408)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 58)
        Me.btnClose.TabIndex = 348
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'cashGroupBox
        '
        Me.cashGroupBox.Controls.Add(Me.Label11)
        Me.cashGroupBox.Controls.Add(Me.txtCashAmount)
        Me.cashGroupBox.Enabled = False
        Me.cashGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cashGroupBox.ForeColor = System.Drawing.Color.Black
        Me.cashGroupBox.Location = New System.Drawing.Point(17, 189)
        Me.cashGroupBox.Name = "cashGroupBox"
        Me.cashGroupBox.Size = New System.Drawing.Size(449, 53)
        Me.cashGroupBox.TabIndex = 356
        Me.cashGroupBox.TabStop = False
        Me.cashGroupBox.Text = "Cash"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(186, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 23)
        Me.Label11.TabIndex = 244
        Me.Label11.Text = "Amount :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCashAmount
        '
        Me.txtCashAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCashAmount.BackColor = System.Drawing.Color.White
        Me.txtCashAmount.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashAmount.Location = New System.Drawing.Point(276, 17)
        Me.txtCashAmount.MaxLength = 13
        Me.txtCashAmount.Name = "txtCashAmount"
        Me.txtCashAmount.Size = New System.Drawing.Size(158, 26)
        Me.txtCashAmount.TabIndex = 0
        Me.txtCashAmount.Text = "0"
        Me.txtCashAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cardGroupBox
        '
        Me.cardGroupBox.Controls.Add(Me.cmbEDC)
        Me.cardGroupBox.Controls.Add(Me.cmbCardType)
        Me.cardGroupBox.Controls.Add(Me.Label6)
        Me.cardGroupBox.Controls.Add(Me.txtCharge)
        Me.cardGroupBox.Controls.Add(Me.Label5)
        Me.cardGroupBox.Controls.Add(Me.txtApproval)
        Me.cardGroupBox.Controls.Add(Me.Label4)
        Me.cardGroupBox.Controls.Add(Me.Label3)
        Me.cardGroupBox.Controls.Add(Me.txtCardName)
        Me.cardGroupBox.Controls.Add(Me.Label7)
        Me.cardGroupBox.Controls.Add(Me.txtCardNo)
        Me.cardGroupBox.Controls.Add(Me.Label10)
        Me.cardGroupBox.Controls.Add(Me.Label12)
        Me.cardGroupBox.Controls.Add(Me.txtCardAmount)
        Me.cardGroupBox.Enabled = False
        Me.cardGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cardGroupBox.ForeColor = System.Drawing.Color.Black
        Me.cardGroupBox.Location = New System.Drawing.Point(17, 241)
        Me.cardGroupBox.Name = "cardGroupBox"
        Me.cardGroupBox.Size = New System.Drawing.Size(449, 237)
        Me.cardGroupBox.TabIndex = 357
        Me.cardGroupBox.TabStop = False
        Me.cardGroupBox.Text = "Card"
        '
        'cmbEDC
        '
        Me.cmbEDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEDC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEDC.FormattingEnabled = True
        Me.cmbEDC.Location = New System.Drawing.Point(100, 138)
        Me.cmbEDC.Name = "cmbEDC"
        Me.cmbEDC.Size = New System.Drawing.Size(210, 21)
        Me.cmbEDC.TabIndex = 4
        '
        'cmbCardType
        '
        Me.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCardType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCardType.FormattingEnabled = True
        Me.cmbCardType.Location = New System.Drawing.Point(100, 51)
        Me.cmbCardType.Name = "cmbCardType"
        Me.cmbCardType.Size = New System.Drawing.Size(210, 21)
        Me.cmbCardType.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(183, 203)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 23)
        Me.Label6.TabIndex = 256
        Me.Label6.Text = "Charge :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCharge
        '
        Me.txtCharge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCharge.BackColor = System.Drawing.Color.White
        Me.txtCharge.Enabled = False
        Me.txtCharge.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCharge.Location = New System.Drawing.Point(276, 203)
        Me.txtCharge.MaxLength = 13
        Me.txtCharge.Name = "txtCharge"
        Me.txtCharge.Size = New System.Drawing.Size(158, 26)
        Me.txtCharge.TabIndex = 255
        Me.txtCharge.Text = "0"
        Me.txtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(11, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 23)
        Me.Label5.TabIndex = 254
        Me.Label5.Text = "Approval :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtApproval
        '
        Me.txtApproval.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtApproval.BackColor = System.Drawing.Color.White
        Me.txtApproval.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApproval.Location = New System.Drawing.Point(100, 167)
        Me.txtApproval.MaxLength = 50
        Me.txtApproval.Name = "txtApproval"
        Me.txtApproval.Size = New System.Drawing.Size(210, 21)
        Me.txtApproval.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(11, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 23)
        Me.Label4.TabIndex = 252
        Me.Label4.Text = "EDC Id :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(11, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 23)
        Me.Label3.TabIndex = 250
        Me.Label3.Text = "Card Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCardName
        '
        Me.txtCardName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCardName.BackColor = System.Drawing.Color.White
        Me.txtCardName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardName.Location = New System.Drawing.Point(100, 107)
        Me.txtCardName.MaxLength = 50
        Me.txtCardName.Name = "txtCardName"
        Me.txtCardName.Size = New System.Drawing.Size(210, 21)
        Me.txtCardName.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(11, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 23)
        Me.Label7.TabIndex = 248
        Me.Label7.Text = "Card No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCardNo
        '
        Me.txtCardNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCardNo.BackColor = System.Drawing.Color.White
        Me.txtCardNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardNo.Location = New System.Drawing.Point(100, 78)
        Me.txtCardNo.MaxLength = 50
        Me.txtCardNo.Name = "txtCardNo"
        Me.txtCardNo.Size = New System.Drawing.Size(210, 21)
        Me.txtCardNo.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 23)
        Me.Label10.TabIndex = 246
        Me.Label10.Text = "Card Type :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(186, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 23)
        Me.Label12.TabIndex = 244
        Me.Label12.Text = "Amount :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCardAmount
        '
        Me.txtCardAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCardAmount.BackColor = System.Drawing.Color.White
        Me.txtCardAmount.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardAmount.Location = New System.Drawing.Point(276, 15)
        Me.txtCardAmount.MaxLength = 13
        Me.txtCardAmount.Name = "txtCardAmount"
        Me.txtCardAmount.Size = New System.Drawing.Size(158, 26)
        Me.txtCardAmount.TabIndex = 0
        Me.txtCardAmount.Text = "0"
        Me.txtCardAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCash
        '
        Me.btnCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCash.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCash.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCash.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCash.ForeColor = System.Drawing.Color.White
        Me.btnCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCash.Location = New System.Drawing.Point(473, 111)
        Me.btnCash.Name = "btnCash"
        Me.btnCash.Size = New System.Drawing.Size(67, 58)
        Me.btnCash.TabIndex = 358
        Me.btnCash.Text = "Cash"
        Me.btnCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCash.UseVisualStyleBackColor = False
        '
        'btnCard
        '
        Me.btnCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCard.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCard.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCard.ForeColor = System.Drawing.Color.White
        Me.btnCard.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCard.Location = New System.Drawing.Point(473, 170)
        Me.btnCard.Name = "btnCard"
        Me.btnCard.Size = New System.Drawing.Size(67, 58)
        Me.btnCard.TabIndex = 359
        Me.btnCard.Text = "Card"
        Me.btnCard.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCard.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(475, 114)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(15, 10)
        Me.Label13.TabIndex = 408
        Me.Label13.Text = "F1"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(476, 173)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 10)
        Me.Label14.TabIndex = 409
        Me.Label14.Text = "F2"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(476, 351)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(20, 10)
        Me.Label15.TabIndex = 410
        Me.Label15.Text = "F12"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(475, 411)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(18, 10)
        Me.Label16.TabIndex = 411
        Me.Label16.Text = "Esc"
        '
        'voucherGroupBox
        '
        Me.voucherGroupBox.Controls.Add(Me.btnViewListVoucher)
        Me.voucherGroupBox.Controls.Add(Me.btnAddVoucher)
        Me.voucherGroupBox.Controls.Add(Me.Label18)
        Me.voucherGroupBox.Controls.Add(Me.txtVoucherCode)
        Me.voucherGroupBox.Controls.Add(Me.Label17)
        Me.voucherGroupBox.Controls.Add(Me.txtVoucherAmount)
        Me.voucherGroupBox.Enabled = False
        Me.voucherGroupBox.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.voucherGroupBox.ForeColor = System.Drawing.Color.Black
        Me.voucherGroupBox.Location = New System.Drawing.Point(17, 93)
        Me.voucherGroupBox.Name = "voucherGroupBox"
        Me.voucherGroupBox.Size = New System.Drawing.Size(449, 90)
        Me.voucherGroupBox.TabIndex = 412
        Me.voucherGroupBox.TabStop = False
        Me.voucherGroupBox.Text = "Voucher"
        '
        'btnViewListVoucher
        '
        Me.btnViewListVoucher.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewListVoucher.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewListVoucher.Location = New System.Drawing.Point(361, 55)
        Me.btnViewListVoucher.Name = "btnViewListVoucher"
        Me.btnViewListVoucher.Size = New System.Drawing.Size(45, 23)
        Me.btnViewListVoucher.TabIndex = 252
        Me.btnViewListVoucher.Text = "View"
        Me.btnViewListVoucher.UseVisualStyleBackColor = True
        '
        'btnAddVoucher
        '
        Me.btnAddVoucher.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddVoucher.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddVoucher.Location = New System.Drawing.Point(316, 55)
        Me.btnAddVoucher.Name = "btnAddVoucher"
        Me.btnAddVoucher.Size = New System.Drawing.Size(45, 23)
        Me.btnAddVoucher.TabIndex = 251
        Me.btnAddVoucher.Text = "Add"
        Me.btnAddVoucher.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(28, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 23)
        Me.Label18.TabIndex = 250
        Me.Label18.Text = "Code :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVoucherCode
        '
        Me.txtVoucherCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtVoucherCode.BackColor = System.Drawing.Color.White
        Me.txtVoucherCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVoucherCode.Location = New System.Drawing.Point(85, 57)
        Me.txtVoucherCode.MaxLength = 32
        Me.txtVoucherCode.Name = "txtVoucherCode"
        Me.txtVoucherCode.Size = New System.Drawing.Size(225, 21)
        Me.txtVoucherCode.TabIndex = 249
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(186, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(83, 23)
        Me.Label17.TabIndex = 244
        Me.Label17.Text = "Amount :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVoucherAmount
        '
        Me.txtVoucherAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtVoucherAmount.BackColor = System.Drawing.Color.White
        Me.txtVoucherAmount.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVoucherAmount.Location = New System.Drawing.Point(276, 18)
        Me.txtVoucherAmount.MaxLength = 13
        Me.txtVoucherAmount.Name = "txtVoucherAmount"
        Me.txtVoucherAmount.ReadOnly = True
        Me.txtVoucherAmount.Size = New System.Drawing.Size(158, 26)
        Me.txtVoucherAmount.TabIndex = 0
        Me.txtVoucherAmount.Text = "0"
        Me.txtVoucherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(476, 232)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(15, 10)
        Me.Label19.TabIndex = 414
        Me.Label19.Text = "F3"
        '
        'btnVoucher
        '
        Me.btnVoucher.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnVoucher.BackColor = System.Drawing.Color.SteelBlue
        Me.btnVoucher.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVoucher.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVoucher.ForeColor = System.Drawing.Color.White
        Me.btnVoucher.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnVoucher.Location = New System.Drawing.Point(473, 229)
        Me.btnVoucher.Name = "btnVoucher"
        Me.btnVoucher.Size = New System.Drawing.Size(67, 58)
        Me.btnVoucher.TabIndex = 413
        Me.btnVoucher.Text = "Voucher"
        Me.btnVoucher.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnVoucher.UseVisualStyleBackColor = False
        '
        'frmPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(548, 594)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.btnVoucher)
        Me.Controls.Add(Me.voucherGroupBox)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cardGroupBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cashGroupBox)
        Me.Controls.Add(Me.btnCard)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCash)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSubTotal)
        Me.Controls.Add(Me.lblPaid)
        Me.Controls.Add(Me.lblChange)
        Me.KeyPreview = True
        Me.Name = "frmPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form Payment"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.cashGroupBox.ResumeLayout(False)
        Me.cashGroupBox.PerformLayout()
        Me.cardGroupBox.ResumeLayout(False)
        Me.cardGroupBox.PerformLayout()
        Me.voucherGroupBox.ResumeLayout(False)
        Me.voucherGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSubTotal As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPaid As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblChange As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cashGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCashAmount As System.Windows.Forms.TextBox
    Friend WithEvents cardGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEDC As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCardType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtApproval As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCardName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCardNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCardAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnCash As System.Windows.Forms.Button
    Friend WithEvents btnCard As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents voucherGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherCode As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtVoucherAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnVoucher As System.Windows.Forms.Button
    Friend WithEvents btnViewListVoucher As System.Windows.Forms.Button
    Friend WithEvents btnAddVoucher As System.Windows.Forms.Button
End Class
