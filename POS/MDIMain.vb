Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Drawing
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Drawing.Drawing2D
Imports genLib.General
Imports secuLib.Security
Imports connlib.DBConnection
Imports iniLib.Ini
Imports proLib.Process
Imports sqlLib.Sql
Imports mainLib
Imports Microsoft.VisualBasic

Public Class MDIMain
    Private konek As Boolean = False
    Private server As String
    Private hostname As String
    Private authentication As Integer
    Private database As String
    Private user As String
    Private password As String

    Private port As Integer
    Private query As String
    Private count As Integer
    Private data As DataTable

    Dim _imgHitArea As Point = New Point(13, 2)
    Dim _imageLocation As Point = New Point(15, 3)
    Private FILE_NAME As String = ""

    ''Public Sub New()

    ''    ' This call is required by the designer.
    ''    InitializeComponent()

    ''    '    'Set the Mode of Drawing as Owner Drawn
    ''    '    TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed

    ''    '    'Add the Handler to draw the Image on Tab Pages
    ''    '    AddHandler TabControl1.DrawItem, AddressOf TabControl1_DrawItem
    ''End Sub

    Private Sub btnMnuSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSales.Click

        Try

            ClearButtonStyle()
            mnuSales.BackColor = Color.White
            mnuSales.ForeColor = Color.Black
            mnuSales.Font = New Font("Tahoma", 8, FontStyle.Bold)
            SubMenuSales(mnuStripSub)
            Me.Text = "TMBookstore - Sales"
            mnuStripSub.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)

        End Try

    End Sub

    Private Sub btnMnuInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventory.Click
        'set focus
        ClearButtonStyle()
        mnuInventory.BackColor = Color.White
        mnuInventory.ForeColor = Color.Black
        mnuInventory.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuInventory(mnuStripSub)
        Me.Text = "TMBookstore - Inventory"
        mnuStripSub.Visible = True
    End Sub

    Public Sub ClearButtonStyle()
        For Each StripButton As ToolStripItem In Me.mnuMain.Items

            If StripButton.Equals(StripButton) Then
                StripButton.BackColor = Color.SteelBlue
                StripButton.ForeColor = Color.White
                StripButton.Font = New Font("Tahoma", 8, FontStyle.Regular)
            End If

        Next
    End Sub

    Private Sub MDIMain_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        If Me.ActiveMdiChild Is Nothing Then
            TabControl1.Visible = False
        Else

            ' If no any child form, hide tabControl 
            Me.ActiveMdiChild.WindowState = FormWindowState.Maximized
            ' Child form always maximized 

            ' If child form is new and no has tabPage, 
            ' create new tabPage 
            If Not Exists(Me.ActiveMdiChild.Text) Then
                ' Add a tabPage to tabControl with child 
                ' form caption 
                Dim tp As New TabPage(Me.ActiveMdiChild.Text)
                tp.Tag = Me.ActiveMdiChild
                tp.ForeColor = Color.SteelBlue

                tp.Parent = TabControl1


                TabControl1.SelectedTab = tp

                Me.ActiveMdiChild.Tag = tp
                'Me.ActiveMdiChild.FormClosed += New FormClosedEventHandler(ActiveMdiChild_FormClosed)
            End If

            If Not TabControl1.Visible Then
                TabControl1.Visible = True

            End If
        End If
    End Sub

    Private Function Exists(ByVal frm As String) As Boolean
        For Each tb As TabPage In TabControl1.TabPages
            If tb.Text = frm Then
                Return True
            End If
        Next

        Return False
    End Function


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If (TabControl1.SelectedTab IsNot Nothing) Then
            TryCast(TabControl1.SelectedTab.Tag, Form).[Select]()
        End If
    End Sub

    Private Sub MiddleProfile()
        'Dim x As Integer = (Me.Width / 2) - (Panel2.Width / 2)
        'Dim y As Integer = (Me.Height / 2.2) - (Panel2.Height / 2)
        'Dim m As Integer = (Me.Width / 2) - (Panel1.Width / 2)
        'Dim n As Integer = (Me.Height / 2.2) - (Panel1.Height / 2)

        'Panel2.Location = New Point(x, y)
        'Panel1.Location = New Point(m, n)


    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        mnuStripSub.Visible = False

        If File.Exists(Application.StartupPath & "\Wallpaper.jpg") Then
            Me.BackgroundImage = Image.FromFile(Application.StartupPath & "\Wallpaper.jpg")
        Else
            Me.BackgroundImage = Nothing
        End If
     
        Me.Hide()



        Dim temp As String = ""

        ProjectID = Environment.MachineName.ToString

        srcPath = Application.StartupPath & "\config.ini"
        If File.Exists(srcPath) Then
            temp = INIRead(srcPath, ProjectID, "Server", "")

            If temp = "" Then
                GoTo konek
            End If

            server = temp
            hostname = decryptString(INIRead(srcPath, ProjectID, "HostName", ""))
            database = decryptString(INIRead(srcPath, ProjectID, "Database", ""))
            authentication = decryptString(INIRead(srcPath, ProjectID, "Authentication", "0"))
            port = decryptString(INIRead(srcPath, ProjectID, "Port", "0"))
            user = decryptString(INIRead(srcPath, ProjectID, "User", ""))
            password = decryptString(INIRead(srcPath, ProjectID, "Password", ""))
            posPrinter = decryptString(INIRead(srcPath, ProjectID, "PosPrinter", ""))
            Try

                If authentication = 0 Then
                    Call OpenConnectionWindows(hostname, database)
                Else
                    Call OpenConnectionSQL(hostname, database, user, password)
                End If


                Connect = True

            Catch ex As Exception
                MsgBox(Err.Description, MsgBoxStyle.Exclamation, Title)
                Connect = False
                frmSetupSQL.ShowDialog()
            End Try

        Else
konek:
            Connect = False
            frmSetupSQL.ShowDialog()
        End If


        If frmLogin.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            If Not Online = True Then
                End
            Else
                GoTo ShowMainMenu
            End If

        End If

ShowMainMenu:

        Me.Show()

        Try
            tblParam = New DataTable

            tblParam = GetParameter()

            DB = database
            Call StatusBar()
            Default_WH = GetValueParamText("DEFAULT WH")


            Default_Branch = GetValueParamText("DEFAULT BRANCH")

            Default_PPN = GetValueParamMoney("DEFAULT PPN")


            tblMenuAccess = New DataTable
            tblMenuAccess = GetMenuAccess(UserGroup)

            'LoadMenuParent First
            MenuParent(tblMenuAccess)

            'close event
            Call CloseEvent()

            'close voucher
            Call CloseVoucher()

            'check best price
            If GetValueParamNumber("BEST PRICE") = 1 Then
                If Not BestPriceExists() = True Then

                    Dim docBestPrice As String = ""

                    docBestPrice = GetLastTransNo("MP")
                    'Check Best Price Promo Exists Today
                    CreateBestPrice(docBestPrice)

                    UpdateHistoryPOS(docBestPrice, "MP")

                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub MenuParent(dt As DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item(2) = "app" Then
                For Each c As ToolStripButton In mnuMain.Items

                    If c.Name = dt.Rows(i).Item(3) Then

                        c.Visible = dt.Rows(i).Item(10)

                    End If

                Next
            End If

        Next
    End Sub

    Private Sub StatusBar()
        Dim systemdate As Date
        Dim today As Date = CDate(Format(Now, "yyyy-MM-dd"))

        systemdate = CDate(GetValueParamDate("SYSTEM DATE"))
        'GETSystemDate()

        If today > systemdate Then
            UpdateSystemDate(today, "SYSTEM DATE")
            statusPeriod.Text = "  " & Format(today, "dd MMMM yyyy")
            tblParam = GetParameter()
        Else
            statusPeriod.Text = "  " & Format(GetValueParamDate("SYSTEM DATE"), "dd MMMM yyyy")
        End If
        statusComp.Text = "  " & GetComputerName()
        statusServer.Text = "  " & hostname & " - " & DB

        statusUser.Text = "  " & UserName
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        statusTime.Text = "  " & Format(Now, "hh:mm:ss tt")
    End Sub

    Private Sub btnMnuReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReport.Click
        'set focus
        ClearButtonStyle()
        mnuReport.BackColor = Color.White
        mnuReport.ForeColor = Color.Black
        mnuReport.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuReport(mnuStripSub)
        Me.Text = "TMBookstore - Report"
        mnuStripSub.Visible = True
    End Sub

    Private Sub mnuStripSub_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles mnuStripSub.Paint
        Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
        Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
        Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
        Dim colors As Color() = {Color.White, Color.White, Color.Silver, Color.Silver}
        Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
        Dim blend As New ColorBlend
        blend.Colors = colors
        blend.Positions = positions
        Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
            lgb.InterpolationColors = blend
            e.Graphics.FillRectangle(lgb, bounds)
        End Using
    End Sub

    Private Sub btnMnuProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProducts.Click
        'set focus
        ClearButtonStyle()
        mnuProducts.BackColor = Color.White
        mnuProducts.ForeColor = Color.Black
        mnuProducts.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuProducts(mnuStripSub)
        Me.Text = "TMBookstore - Products"
        mnuStripSub.Visible = True
    End Sub

    Private Sub btnMnuSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetup.Click

        'set focus
        ClearButtonStyle()
        mnuSetup.BackColor = Color.White
        mnuSetup.ForeColor = Color.Black
        mnuSetup.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuSetup(mnuStripSub)
        Me.Text = "TMBookstore - Setup"
        mnuStripSub.Visible = True
    End Sub


    ''Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
    ''    Try
    ''        'Close Image to draw
    ''        Dim img As Image = New Bitmap(Application.StartupPath & "\image\close.gif")
    ''        Dim r As Rectangle = e.Bounds

    ''        r = Me.TabControl1.GetTabRect(e.Index)
    ''        r.Offset(2, 2)
    ''        Dim TitleBrush As Brush = New SolidBrush(Color.Black)
    ''        Dim f As Font = Me.Font
    ''        Dim title As String = Me.TabControl1.TabPages(e.Index).Text

    ''        e.Graphics.DrawString(title, f, TitleBrush, New PointF(r.X, r.Y))
    ''        e.Graphics.DrawImage(img, New Point(r.X + (Me.TabControl1.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y))

    ''    Catch ex As Exception
    ''        MsgBox(ex.Message)
    ''    End Try
    ''End Sub

    ''Private Sub TabControl1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl1.MouseClick
    ''    Dim tc As TabControl = CType(sender, TabControl)
    ''    Dim p As Point = e.Location
    ''    Dim _tabWidth As Integer
    ''    Dim frm As New Form

    ''    _tabWidth = Me.TabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X)
    ''    Dim r As Rectangle = Me.TabControl1.GetTabRect(tc.SelectedIndex)
    ''    r.Offset(_tabWidth, _imgHitArea.Y)
    ''    r.Width = 16
    ''    r.Height = 16
    ''    If r.Contains(p) Then
    ''        frm = TryCast(TabControl1.TabPages.Item(tc.SelectedIndex).Tag, Form)
    ''        Dim TabP As TabPage = DirectCast(tc.TabPages.Item(tc.SelectedIndex), TabPage)
    ''        tc.TabPages.Remove(TabP)
    ''        frm.Close()

    ''    End If


    ''End Sub

    Private Sub linkLogOut_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkLogOut.LinkClicked
        Online = False
        Me.Hide()
        If frmLogin.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            If Not Online = True Then
                End
            Else
                GoTo ShowMainMenu
            End If

        End If

ShowMainMenu:

        Me.Show()

        Try
            tblParam = New DataTable

            tblParam = GetParameter()

            DB = database
            Call StatusBar()
            Default_WH = GetValueParamText("DEFAULT WH")


            Default_Branch = GetValueParamText("DEFAULT BRANCH")

            Default_PPN = GetValueParamMoney("DEFAULT PPN")


            tblMenuAccess = New DataTable
            tblMenuAccess = GetMenuAccess(UserGroup)

            'LoadMenuParent First
            MenuParent(tblMenuAccess)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
       

    End Sub

    Private Sub mnuHRDGA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHRDGA.Click
        ClearButtonStyle()
        mnuHRDGA.BackColor = Color.White
        mnuHRDGA.ForeColor = Color.Black
        mnuHRDGA.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuHRD(mnuStripSub)
        Me.Text = "TMBookstore - HRD/GA"
        mnuStripSub.Visible = True
    End Sub

    Private Sub mnuFinance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFinance.Click
        ClearButtonStyle()
        mnuFinance.BackColor = Color.White
        mnuFinance.ForeColor = Color.Black
        mnuFinance.Font = New Font("Tahoma", 8, FontStyle.Bold)
        SubMenuFinance(mnuStripSub)
        Me.Text = "TMBookstore - Finance"
        mnuStripSub.Visible = True
    End Sub

    Private Sub MDIMain_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

    End Sub
End Class
