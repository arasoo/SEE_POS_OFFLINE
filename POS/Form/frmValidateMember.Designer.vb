﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmValidateMember
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
        Me.txtMember = New System.Windows.Forms.TextBox()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.memberPic = New System.Windows.Forms.PictureBox()
        CType(Me.memberPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMember
        '
        Me.txtMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMember.BackColor = System.Drawing.Color.White
        Me.txtMember.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMember.Location = New System.Drawing.Point(92, 40)
        Me.txtMember.MaxLength = 20
        Me.txtMember.Name = "txtMember"
        Me.txtMember.Size = New System.Drawing.Size(285, 27)
        Me.txtMember.TabIndex = 398
        '
        'lblNote
        '
        Me.lblNote.BackColor = System.Drawing.Color.Transparent
        Me.lblNote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNote.ForeColor = System.Drawing.Color.White
        Me.lblNote.Location = New System.Drawing.Point(92, 9)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(166, 28)
        Me.lblNote.TabIndex = 399
        Me.lblNote.Text = "Please input member code"
        Me.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'memberPic
        '
        Me.memberPic.Location = New System.Drawing.Point(5, 9)
        Me.memberPic.Name = "memberPic"
        Me.memberPic.Size = New System.Drawing.Size(81, 68)
        Me.memberPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.memberPic.TabIndex = 400
        Me.memberPic.TabStop = False
        '
        'frmValidateMember
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(398, 86)
        Me.ControlBox = False
        Me.Controls.Add(Me.memberPic)
        Me.Controls.Add(Me.lblNote)
        Me.Controls.Add(Me.txtMember)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmValidateMember"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Check Member"
        CType(Me.memberPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMember As System.Windows.Forms.TextBox
    Friend WithEvents lblNote As System.Windows.Forms.Label
    Friend WithEvents memberPic As System.Windows.Forms.PictureBox
End Class
