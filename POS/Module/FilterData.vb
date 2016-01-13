Imports connLib.DBConnection
Imports genLib.General
Imports proLib.Process

Module FilterData

    Public Sub LoadBranch(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Branch_Code Code,Branch_Name Name FROM " & DB & ".dbo.branch"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName

            gridView.DataSource = table

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub


    Public Sub LoadCompany(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Company_Code Code,Company_Name Name FROM " & DB & ".dbo.company"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName
            gridView.DataSource = table

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub



    Public Sub LoadGroup(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Mat_Tipe 'Type',Mat_Description 'Description' " & _
                                " FROM " & DB & ".dbo.mmca"

            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName
            gridView.DataSource = table

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadSalesOrg(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT SO_Salesorg Code,SO_Name Name FROM " & DB & ".dbo.mslsorg"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName
            gridView.DataSource = table


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCustomer(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Cust_Kode Code,Cust_Nama Customer FROM " & DB & ".dbo.mcust"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName

            gridView.DataSource = table


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProducts(ByVal gridView As DataGridView, ByVal opt As Integer, ByRef bs As BindingSource)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "EXECUTE " & DB & ".dbo.P_GETPRODUCTS '" & opt & "','" & GetValueParamText("HET PRICE") & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With


            If table.Rows.Count > 0 Then
                'gridView.Rows.Clear()

                'For i As Integer = 0 To table.Rows.Count - 1
                '    gridView.Rows.Add( _
                '                    New Object() {table.Rows(i).Item(0), table.Rows(i).Item(1), _
                '                    table.Rows(i).Item(2), table.Rows(i).Item(3),
                '                    table.Rows(i).Item(4), String.Format("{0:#,##0}", table.Rows(i).Item(5))})
                'Next
                With gridView
                    .AutoGenerateColumns = False
                    .Columns(0).DataPropertyName = "item"
                    .Columns(1).DataPropertyName = "judul"
                    .Columns(2).DataPropertyName = "tipe"
                    .Columns(3).DataPropertyName = "vendor"
                    .Columns(4).DataPropertyName = "isbn"
                    .Columns(5).DataPropertyName = "price"
                    .Columns(5).DefaultCellStyle.Format = "##,0"

                End With

                gridView.DataSource = table
                bs.DataSource = table


            End If


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    'Master Item
    Public Sub LoadPriceGroup(ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Pricegroup_Code,Pricegroup_Description" & _
                                " FROM " & DB & ".dbo.pricegroup"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            Dim col1 As DataGridViewComboBoxColumn = DirectCast(gridView.Columns(0), DataGridViewComboBoxColumn)
            col1.DataSource = table
            col1.ValueMember = table.Columns.Item(0).ColumnName
            col1.DisplayMember = table.Columns.Item(1).ColumnName

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadUOM(ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT UOM_Code,UOM_Description" & _
                                " FROM " & DB & ".dbo.uom"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            Dim col1 As DataGridViewComboBoxColumn = DirectCast(gridView.Columns(3), DataGridViewComboBoxColumn)
            col1.DataSource = table
            col1.ValueMember = table.Columns.Item(0).ColumnName
            col1.DisplayMember = table.Columns.Item(0).ColumnName

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCurr(ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT Currency_Id,Currency_Description" & _
                                " FROM " & DB & ".dbo.currency"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            Dim col1 As DataGridViewComboBoxColumn = DirectCast(gridView.Columns(2), DataGridViewComboBoxColumn)
            col1.DataSource = table
            col1.ValueMember = table.Columns.Item(0).ColumnName
            col1.DisplayMember = table.Columns.Item(0).ColumnName


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadWarehouse(ByVal cmb As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT WH_Code Code,WH_Name Name FROM " & DB & ".dbo.warehouse"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With
            cmb.DataSource = table
            cmb.ValueMember = table.Columns.Item(0).ColumnName
            cmb.DisplayMember = table.Columns.Item(1).ColumnName

            gridView.DataSource = table


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadMaterialType(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView)
        Try
            table = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT mat_tipe Type,mat_Description [Description]" & _
                                " FROM " & DB & ".dbo.mmca WHERE mat_status in ('C','G')"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            cmbBox.DataSource = table
            cmbBox.ValueMember = table.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = table.Columns.Item(1).ColumnName
            gridView.DataSource = table

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub


    Public Function GetColumnWidth(ByVal c As Integer, ByVal gridFilter As DataGridView) As Integer
        Dim total As Integer
        For i As Integer = 0 To c - 1
            total = total + gridFilter.Columns(i).Width
        Next

        Return total
    End Function

    Public Function GetRowHeight(ByVal c As Integer, ByVal gridfilter As DataGridView) As Integer
        Dim total As Integer
        For i As Integer = 0 To c - 1
            total = total + gridFilter.Rows(i).Height
        Next

        If total > 150 Then
            Return 150
        Else
            Return total + 50
        End If

    End Function

End Module
