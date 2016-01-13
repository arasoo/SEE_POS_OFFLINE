<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIMain))
        Me.mnuMain = New System.Windows.Forms.ToolStrip()
        Me.mnuSales = New System.Windows.Forms.ToolStripButton()
        Me.mnuCustomers = New System.Windows.Forms.ToolStripButton()
        Me.mnuProducts = New System.Windows.Forms.ToolStripButton()
        Me.mnuInventory = New System.Windows.Forms.ToolStripButton()
        Me.mnuFinance = New System.Windows.Forms.ToolStripButton()
        Me.mnuAccounting = New System.Windows.Forms.ToolStripButton()
        Me.mnuHRDGA = New System.Windows.Forms.ToolStripButton()
        Me.mnuReport = New System.Windows.Forms.ToolStripButton()
        Me.mnuSetup = New System.Windows.Forms.ToolStripButton()
        Me.mnuStripSub = New System.Windows.Forms.MenuStrip()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.statusPeriod = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusComp = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusServer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.linkLogOut = New System.Windows.Forms.LinkLabel()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.mnuMain.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.AutoSize = False
        Me.mnuMain.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuMain.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.mnuSales, Me.mnuCustomers, Me.mnuProducts, Me.mnuInventory, Me.mnuFinance, Me.mnuAccounting, Me.mnuHRDGA, Me.mnuReport, Me.mnuSetup})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(991, 30)
        Me.mnuMain.TabIndex = 3
        '
        'mnuSales
        '
        Me.mnuSales.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuSales.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuSales.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuSales.ForeColor = System.Drawing.Color.White
        Me.mnuSales.Image = CType(resources.GetObject("mnuSales.Image"), System.Drawing.Image)
        Me.mnuSales.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuSales.Name = "mnuSales"
        Me.mnuSales.Size = New System.Drawing.Size(36, 27)
        Me.mnuSales.Tag = "SLS"
        Me.mnuSales.Text = "Sales"
        '
        'mnuCustomers
        '
        Me.mnuCustomers.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuCustomers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuCustomers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuCustomers.ForeColor = System.Drawing.Color.White
        Me.mnuCustomers.Image = CType(resources.GetObject("mnuCustomers.Image"), System.Drawing.Image)
        Me.mnuCustomers.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuCustomers.Name = "mnuCustomers"
        Me.mnuCustomers.Size = New System.Drawing.Size(62, 27)
        Me.mnuCustomers.Tag = "CUS"
        Me.mnuCustomers.Text = "Customers"
        '
        'mnuProducts
        '
        Me.mnuProducts.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuProducts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuProducts.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuProducts.ForeColor = System.Drawing.Color.White
        Me.mnuProducts.Image = CType(resources.GetObject("mnuProducts.Image"), System.Drawing.Image)
        Me.mnuProducts.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuProducts.Name = "mnuProducts"
        Me.mnuProducts.Size = New System.Drawing.Size(53, 27)
        Me.mnuProducts.Tag = "PRO"
        Me.mnuProducts.Text = "Products"
        '
        'mnuInventory
        '
        Me.mnuInventory.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuInventory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuInventory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuInventory.ForeColor = System.Drawing.Color.White
        Me.mnuInventory.Image = CType(resources.GetObject("mnuInventory.Image"), System.Drawing.Image)
        Me.mnuInventory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuInventory.Name = "mnuInventory"
        Me.mnuInventory.Size = New System.Drawing.Size(59, 27)
        Me.mnuInventory.Tag = "INV"
        Me.mnuInventory.Text = "Inventory"
        '
        'mnuFinance
        '
        Me.mnuFinance.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuFinance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuFinance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuFinance.ForeColor = System.Drawing.Color.White
        Me.mnuFinance.Image = CType(resources.GetObject("mnuFinance.Image"), System.Drawing.Image)
        Me.mnuFinance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFinance.Name = "mnuFinance"
        Me.mnuFinance.Size = New System.Drawing.Size(48, 27)
        Me.mnuFinance.Tag = "FIN"
        Me.mnuFinance.Text = "Finance"
        '
        'mnuAccounting
        '
        Me.mnuAccounting.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuAccounting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuAccounting.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuAccounting.ForeColor = System.Drawing.Color.White
        Me.mnuAccounting.Image = CType(resources.GetObject("mnuAccounting.Image"), System.Drawing.Image)
        Me.mnuAccounting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuAccounting.Name = "mnuAccounting"
        Me.mnuAccounting.Size = New System.Drawing.Size(64, 27)
        Me.mnuAccounting.Tag = "ACC"
        Me.mnuAccounting.Text = "Accounting"
        '
        'mnuHRDGA
        '
        Me.mnuHRDGA.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuHRDGA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuHRDGA.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuHRDGA.ForeColor = System.Drawing.Color.White
        Me.mnuHRDGA.Image = CType(resources.GetObject("mnuHRDGA.Image"), System.Drawing.Image)
        Me.mnuHRDGA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuHRDGA.Name = "mnuHRDGA"
        Me.mnuHRDGA.Size = New System.Drawing.Size(50, 27)
        Me.mnuHRDGA.Tag = "HRD"
        Me.mnuHRDGA.Text = "HRD/GA"
        '
        'mnuReport
        '
        Me.mnuReport.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuReport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuReport.ForeColor = System.Drawing.Color.White
        Me.mnuReport.Image = CType(resources.GetObject("mnuReport.Image"), System.Drawing.Image)
        Me.mnuReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuReport.Name = "mnuReport"
        Me.mnuReport.Size = New System.Drawing.Size(44, 27)
        Me.mnuReport.Tag = "RPT"
        Me.mnuReport.Text = "Report"
        '
        'mnuSetup
        '
        Me.mnuSetup.BackColor = System.Drawing.Color.SteelBlue
        Me.mnuSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuSetup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuSetup.ForeColor = System.Drawing.Color.White
        Me.mnuSetup.Image = CType(resources.GetObject("mnuSetup.Image"), System.Drawing.Image)
        Me.mnuSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuSetup.Name = "mnuSetup"
        Me.mnuSetup.Size = New System.Drawing.Size(39, 27)
        Me.mnuSetup.Tag = "SET"
        Me.mnuSetup.Text = "Setup"
        '
        'mnuStripSub
        '
        Me.mnuStripSub.AutoSize = False
        Me.mnuStripSub.BackColor = System.Drawing.Color.White
        Me.mnuStripSub.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuStripSub.Location = New System.Drawing.Point(0, 30)
        Me.mnuStripSub.Name = "mnuStripSub"
        Me.mnuStripSub.Size = New System.Drawing.Size(991, 20)
        Me.mnuStripSub.TabIndex = 4
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.BackColor = System.Drawing.Color.OrangeRed
        Me.StatusStrip1.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusPeriod, Me.statusUser, Me.ToolStripStatusLabel1, Me.statusComp, Me.statusServer, Me.ToolStripStatusLabel4, Me.statusTime})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 598)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.Size = New System.Drawing.Size(991, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 86
        '
        'statusPeriod
        '
        Me.statusPeriod.BackColor = System.Drawing.Color.White
        Me.statusPeriod.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.statusPeriod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusPeriod.ForeColor = System.Drawing.Color.White
        Me.statusPeriod.Image = CType(resources.GetObject("statusPeriod.Image"), System.Drawing.Image)
        Me.statusPeriod.Name = "statusPeriod"
        Me.statusPeriod.Size = New System.Drawing.Size(16, 17)
        '
        'statusUser
        '
        Me.statusUser.BackColor = System.Drawing.Color.Maroon
        Me.statusUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusUser.ForeColor = System.Drawing.Color.White
        Me.statusUser.Image = CType(resources.GetObject("statusUser.Image"), System.Drawing.Image)
        Me.statusUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.statusUser.Name = "statusUser"
        Me.statusUser.Size = New System.Drawing.Size(16, 17)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.OrangeRed
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.White
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(448, 17)
        Me.ToolStripStatusLabel1.Spring = True
        '
        'statusComp
        '
        Me.statusComp.BackColor = System.Drawing.Color.White
        Me.statusComp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusComp.ForeColor = System.Drawing.Color.White
        Me.statusComp.Image = CType(resources.GetObject("statusComp.Image"), System.Drawing.Image)
        Me.statusComp.ImageTransparentColor = System.Drawing.Color.White
        Me.statusComp.Name = "statusComp"
        Me.statusComp.Size = New System.Drawing.Size(16, 17)
        '
        'statusServer
        '
        Me.statusServer.BackColor = System.Drawing.Color.White
        Me.statusServer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusServer.ForeColor = System.Drawing.Color.White
        Me.statusServer.Image = CType(resources.GetObject("statusServer.Image"), System.Drawing.Image)
        Me.statusServer.ImageTransparentColor = System.Drawing.Color.White
        Me.statusServer.Name = "statusServer"
        Me.statusServer.Size = New System.Drawing.Size(16, 17)
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.AutoSize = False
        Me.ToolStripStatusLabel4.BackColor = System.Drawing.Color.OrangeRed
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(448, 17)
        Me.ToolStripStatusLabel4.Spring = True
        '
        'statusTime
        '
        Me.statusTime.BackColor = System.Drawing.Color.White
        Me.statusTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statusTime.ForeColor = System.Drawing.Color.White
        Me.statusTime.Image = CType(resources.GetObject("statusTime.Image"), System.Drawing.Image)
        Me.statusTime.Name = "statusTime"
        Me.statusTime.Size = New System.Drawing.Size(16, 17)
        '
        'TabControl1
        '
        Me.TabControl1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 569)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(991, 29)
        Me.TabControl1.TabIndex = 87
        Me.TabControl1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.LightCoral
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(772, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 23)
        Me.Label1.TabIndex = 245
        Me.Label1.Text = "Version : 2.0.2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'linkLogOut
        '
        Me.linkLogOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.linkLogOut.AutoSize = True
        Me.linkLogOut.BackColor = System.Drawing.Color.SteelBlue
        Me.linkLogOut.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkLogOut.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.linkLogOut.LinkColor = System.Drawing.Color.White
        Me.linkLogOut.Location = New System.Drawing.Point(930, 5)
        Me.linkLogOut.Name = "linkLogOut"
        Me.linkLogOut.Size = New System.Drawing.Size(52, 14)
        Me.linkLogOut.TabIndex = 247
        Me.linkLogOut.TabStop = True
        Me.linkLogOut.Text = "Log Out"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.AutoSize = False
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(0, 27)
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 620)
        Me.Controls.Add(Me.linkLogOut)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.mnuStripSub)
        Me.Controls.Add(Me.mnuMain)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.mnuStripSub
        Me.Name = "MDIMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TMBookstore"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.ToolStrip
    Friend WithEvents mnuStripSub As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuSales As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuInventory As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuCustomers As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuProducts As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statusPeriod As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusComp As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusServer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mnuFinance As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuAccounting As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuSetup As System.Windows.Forms.ToolStripButton
    Friend WithEvents linkLogOut As System.Windows.Forms.LinkLabel
    Friend WithEvents mnuHRDGA As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel

End Class
