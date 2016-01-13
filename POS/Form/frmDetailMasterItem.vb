Imports genLib.General
Imports connLib.DBConnection
Imports System.Drawing.Drawing2D
Imports System.IO
Imports mainLib

Public Class frmDetailMasterItem

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Private table As DataTable
    Private mitem As String

    Public WriteOnly Property ItemCode As String
        Set(ByVal value As String)
            mitem = value
        End Set
    End Property

    Private Sub frmMasterItem_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim bounds As New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height)
        Dim topPoint As New Point((Me.ClientSize.Width - 1) \ 2, 0)
        Dim bottomPoint As New Point((Me.ClientSize.Width - 1) \ 2, Me.ClientSize.Height - 1)
        Dim colors As Color() = {Color.White, Color.White, Color.Khaki, Color.Khaki}
        Dim positions As Single() = {0.0F, 0.15F, 0.85F, 1.0F}
        Dim blend As New ColorBlend
        blend.Colors = colors
        blend.Positions = positions
        Using lgb As New LinearGradientBrush(topPoint, bottomPoint, Color.White, Color.White)
            lgb.InterpolationColors = blend
            e.Graphics.FillRectangle(lgb, bounds)
        End Using
    End Sub

    Private Sub GetItem()
        Try

            table = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Item_Code,Item_Description,Item_Type,Item_UOM" & _
                                ",Item_Product,Item_TaxGroup,Item_DiscGroup,Item_Prodhier1 " & _
                                ",Item_Prodhier2,Item_Prodhier3,Item_Prodhier4,Item_Prodhier5 " & _
                                ",Item_Vendor,Item_ISBN,Item_Supplier,Item_Author,Item_Sinobsys " & _
                                ",Item_Status FROM " & DB & ".dbo.items" & _
                                " WHERE Item_Code = '" & mitem & "'"


            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            If table.Rows.Count > 0 Then
                txtItem.Text = table.Rows(0).Item("Item_Code")
                txtDescription.Text = table.Rows(0).Item("Item_Description")
                cmbTaxGroup.SelectedValue = table.Rows(0).Item("Item_TaxGroup")
                cmbMaterialType.SelectedValue = table.Rows(0).Item("Item_Type")
                cmbUOM.SelectedValue = table.Rows(0).Item("Item_UOM")
                cmbDiscGroup.SelectedValue = table.Rows(0).Item("Item_DiscGroup")
                cmbProdhier1.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Prodhier1")), "", table.Rows(0).Item("Item_Prodhier1"))
                cmbProdhier2.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Prodhier2")), "", table.Rows(0).Item("Item_Prodhier2"))
                cmbProdhier3.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Prodhier3")), "", table.Rows(0).Item("Item_Prodhier3"))
                cmbProdhier4.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Prodhier4")), "", table.Rows(0).Item("Item_Prodhier4"))
                cmbProdhier5.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Prodhier5")), "", table.Rows(0).Item("Item_Prodhier5"))
                cmbProduct.SelectedValue = table.Rows(0).Item("Item_Product")
                txtVendor.Text = table.Rows(0).Item("Item_Vendor")
                txtISBN.Text = table.Rows(0).Item("Item_ISBN")
                txtSinobsys.Text = IIf(IsDBNull(table.Rows(0).Item("Item_Sinobsys")), "", table.Rows(0).Item("Item_Sinobsys"))
                txtAuthor.Text = table.Rows(0).Item("Item_Author")
                cmbStatus.SelectedIndex = table.Rows(0).Item("Item_Status")
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub frmMasterItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
        LoadMaterialType()
        LoadProduct()
        LoadDiscGroup()
        LoadUOM()
        LoadTaxGroup()
        GetItem()

    End Sub

    Private Sub LoadImage()

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnCancel.Image = mainClass.imgList.ImgBtnCancel

    End Sub

    Private Sub LoadMaterialType()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Mat_Type Type,Mat_Description 'Description' FROM " & DB & ".dbo.materialtype"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillMaterialType(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillMaterialType(ByVal Data As DataTable)
        cmbMaterialType.DataSource = Data
        cmbMaterialType.ValueMember = Data.Columns.Item(0).ColumnName
        cmbMaterialType.DisplayMember = Data.Columns.Item(1).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub LoadUOM()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Uom_Code Code,Uom_Description 'Description' FROM " & DB & ".dbo.uom"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillUOM(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillUOM(ByVal Data As DataTable)
        cmbUOM.DataSource = Data
        cmbUOM.ValueMember = Data.Columns.Item(0).ColumnName
        cmbUOM.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub LoadProduct()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Product_Code Product,Product_Name 'Description' FROM " & DB & ".dbo.products"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillProduct(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillProduct(ByVal Data As DataTable)
        cmbProduct.DataSource = Data
        cmbProduct.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProduct.DisplayMember = Data.Columns.Item(1).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub LoadTaxGroup()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Tax_Type 'Group',Tax_Description 'Description' FROM " & DB & ".dbo.tax"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillTaxGroup(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillTaxGroup(ByVal Data As DataTable)
        cmbTaxGroup.DataSource = Data
        cmbTaxGroup.ValueMember = Data.Columns.Item(0).ColumnName
        cmbTaxGroup.DisplayMember = Data.Columns.Item(1).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub LoadDiscGroup()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Disc_Group 'Group',Disc_Description 'Description' FROM " & DB & ".dbo.discgroup"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillDiscGroup(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillDiscGroup(ByVal Data As DataTable)
        cmbDiscGroup.DataSource = Data
        cmbDiscGroup.ValueMember = Data.Columns.Item(0).ColumnName
        cmbDiscGroup.DisplayMember = Data.Columns.Item(1).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub LoadSupplier()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim search As String = "%" & cmbSupplier.Text & "%"
            With cm
                .Connection = cn
                .CommandText = "SELECT Sup_Code Code,Sup_Name 'Name' " & _
                                " FROM " & DB & ".dbo.suppliers" & _
                                " WHERE Sup_Name LIKE '" & search & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillSupplier(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillSupplier(ByVal Data As DataTable)
        cmbSupplier.DataSource = Data
        cmbSupplier.ValueMember = Data.Columns.Item(0).ColumnName
        cmbSupplier.DisplayMember = Data.Columns.Item(1).ColumnName
        gridAll.DataSource = Data
    End Sub

    Private Sub cmbProdhier5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProdhier5.Click, cmbProdhier4.Click _
                                                                                                , cmbProdhier1.Click, cmbProdhier2.Click _
                                                                                                , cmbProdhier3.Click, cmbProduct.Click _
                                                                                                , cmbMaterialType.Click, cmbDiscGroup.Click _
                                                                                                , cmbTaxGroup.Click, cmbUOM.Click, cmbSupplier.Click

        Dim cmb As ComboBox = CType(sender, ComboBox)

        Select Case cmb.Tag
            Case "Prodhier1"
                GetProdhier1()
            Case "Prodhier2"
                GetProdhier2()
            Case "Prodhier3"
                GetProdhier3()
            Case "Prodhier4"
                GetProdhier4()
            Case "Prodhier5"
                GetProdhier5()
            Case "Product"
                LoadProduct()
            Case "Type"
                LoadMaterialType()
            Case "Tax"
                LoadTaxGroup()
            Case "UOM"
                LoadUOM()
            Case "Supplier"
                LoadSupplier()
            Case Else
                LoadDiscGroup()

        End Select
        gridAll.Location = New Point(cmb.Left, cmb.Location.Y + 22)
        gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count) + 30, GetRowHeight(gridAll.Rows.Count))
        cmb.DroppedDown = False

        If gridAll.RowCount > 0 Then gridAll.Visible = True
        gridAll.Tag = cmb.Tag
    End Sub

    Private Function GetColumnWidth(ByVal c As Integer) As Integer
        Dim total As Integer
        For i As Integer = 0 To c - 1
            total = total + gridAll.Columns(i).Width
        Next

        Return total
    End Function

    Private Function GetRowHeight(ByVal c As Integer) As Integer
        Dim total As Integer
        For i As Integer = 0 To c - 1
            total = total + gridAll.Rows(i).Height
        Next

        If total > 150 Then
            Return 150
        Else
            Return total + 30
        End If

    End Function

    Private Sub cmbProdhier1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbProdhier1.KeyUp, cmbProdhier2.KeyUp _
                                                                                                                , cmbProdhier3.KeyUp, cmbProdhier4.KeyUp _
                                                                                                                , cmbProdhier5.KeyUp, cmbSupplier.KeyUp
        If e.KeyCode = Keys.Enter Then
            Dim cmb As ComboBox = CType(sender, ComboBox)

            Select Case cmb.Tag
                Case "Prodhier1"
                    GetProdhier1()
                Case "Prodhier2"
                    GetProdhier2()
                Case "Prodhier3"
                    GetProdhier3()
                Case "Prodhier4"
                    GetProdhier4()
                Case "Supplier"
                    LoadSupplier()
                Case Else
                    GetProdhier5()
            End Select

            gridAll.Location = New Point(cmb.Left, cmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count) + 30, GetRowHeight(gridAll.Rows.Count))
            cmb.DroppedDown = False

            If gridAll.RowCount > 0 Then gridAll.Visible = True

            gridAll.Tag = cmb.Tag
        End If

    End Sub

    Private Sub cmbProdhier_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProdhier3.LostFocus, cmbProdhier2.LostFocus _
                                                                                                    , cmbProdhier4.LostFocus, cmbProdhier5.LostFocus, cmbProdhier1.LostFocus
        gridAll.Visible = False
    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.Click

        'If gridAll.Tag = "Prodhier5" Then
        '    cmbProdhier5.SelectedValue = gridAll.SelectedCells(0).Value
        'ElseIf gridAll.Tag = "Prodhier4" Then
        '    cmbProdhier4.SelectedValue = gridAll.SelectedCells(0).Value
        'End If

        gridAll.Visible = False
    End Sub

    Private Sub gridAll_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.MouseLeave
        gridAll.Visible = False
    End Sub

    Private Sub GetProdhier5()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Prodhier5_Code Code,Prodhier5_Description 'Name' FROM " & DB & ".dbo.Prodhier5"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillComboprodhier5(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboprodhier5(ByVal Data As DataTable)
        cmbProdhier5.DataSource = Data
        cmbProdhier5.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProdhier5.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub GetProdhier1()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Prodhier1_Code Code,Prodhier1_Description 'Name' FROM " & DB & ".dbo.Prodhier1"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillComboprodhier1(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboprodhier1(ByVal Data As DataTable)
        cmbProdhier1.DataSource = Data
        cmbProdhier1.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProdhier1.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub GetProdhier2()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Prodhier2_Code Code,Prodhier2_Description 'Name' FROM " & DB & ".dbo.Prodhier2"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillComboprodhier2(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboprodhier2(ByVal Data As DataTable)
        cmbProdhier2.DataSource = Data
        cmbProdhier2.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProdhier2.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub GetProdhier3()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Prodhier3_Code Code,Prodhier3_Description 'Name' FROM " & DB & ".dbo.Prodhier3"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillComboprodhier3(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboprodhier3(ByVal Data As DataTable)
        cmbProdhier3.DataSource = Data
        cmbProdhier3.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProdhier3.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

    Private Sub GetProdhier4()
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Prodhier4_Code Code,Prodhier4_Description 'Name' FROM " & DB & ".dbo.Prodhier4"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            FillComboprodhier4(table)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboprodhier4(ByVal Data As DataTable)
        cmbProdhier4.DataSource = Data
        cmbProdhier4.ValueMember = Data.Columns.Item(0).ColumnName
        cmbProdhier4.DisplayMember = Data.Columns.Item(0).ColumnName

        gridAll.DataSource = Data
    End Sub

End Class