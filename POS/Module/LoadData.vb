Imports connLib.DBConnection
Imports genLib.General
Imports proLib.Process
Imports sqlLib.Sql
Imports System.Data.SqlClient

Module LoadData

    Private dtTable As DataTable

    Public Sub LoadEDC(ByVal cmb As ComboBox)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "SELECT EDCID EDC_ID,[Description] EDC_Description FROM " & DB & ".dbo.medc"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
            FillComboEDC(dtTable, cmb)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboEDC(ByVal Data As DataTable, ByVal cmbBox As ComboBox)
        cmbBox.DataSource = Data
        cmbBox.ValueMember = Data.Columns.Item(0).ColumnName
        cmbBox.DisplayMember = Data.Columns.Item(1).ColumnName

    End Sub

    Public Sub LoadCard(ByVal cmb As ComboBox)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT cardtype Card_type,[description] Card_Description FROM " & DB & ".dbo.mcardtype"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
            FillComboCard(dtTable, cmb)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub FillComboCard(ByVal Data As DataTable, ByVal cmbBox As ComboBox)
        cmbBox.DataSource = Data
        cmbBox.ValueMember = Data.Columns.Item(0).ColumnName
        cmbBox.DisplayMember = Data.Columns.Item(1).ColumnName

    End Sub

    Public Sub LoadBranch(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT coy_branch Code,coy_description Name FROM " & DB & ".dbo.mbranch"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadTaxOrg(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT tax_organization Code,tax_description Name FROM " & DB & ".dbo.mtaxorg"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadTransaction(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT TrnCode Code,Description Name FROM " & DB & ".dbo.mktrd "

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub loadUserGroup(ByVal cmb As DataGridViewComboBoxColumn)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT usergroup_id Code,usergroup_description Name FROM " & DB & ".dbo.musergroup"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            cmb.DataSource = dtTable
            cmb.DisplayMember = "Name"
            cmb.ValueMember = "Code"


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCompany(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Coy_CompanyCode Code,Coy_Description Name FROM " & DB & ".dbo.mcoy"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCurrency(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Currency Code,Description Name FROM " & DB & ".dbo.mcurrency"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadGroup(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Mat_Tipe 'Type',Mat_Description 'Description' " & _
                                " FROM " & DB & ".dbo.mmca"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProductGroup(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT productgroup 'Group',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mgprod"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProdhier1(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal text As String, ByVal state As Integer)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If state = 0 Then
                query = "SELECT prodhier1 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mprodhier1"
            Else
                query = "SELECT prodhier1 'Code',[Description] 'Description' " & _
                             " FROM " & DB & ".dbo.mprodhier1 WHERE [Description] LIKE '%" & Trim(text) & "%'"
            End If
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query

            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            cmbBox.DataSource = dtTable
            cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProdhier2(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT prodhier2 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mprodhier2"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProdhier3(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT prodhier3 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mprodhier3"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProdhier4(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT prodhier4 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mprodhier4"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProdhier5(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT prodhier5 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mprodhier5"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadMember(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT cust_kode 'Code',cust_nama 'Description'" & _
                               " FROM " & DB & ".dbo.mcust WHERE cust_type='02'"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub


    Public Sub LoadDiscGroup(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT product_disc 'Code',[Description] 'Description' " & _
                               " FROM " & DB & ".dbo.mproddisc"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadSalesOrg(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT SO_Salesorg Code,SO_Name Name FROM " & DB & ".dbo.mslsorg"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadSalesman(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Sales_Code Code,Sales_Name Name FROM " & DB & ".dbo.mslsmn"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadEmployee(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal name As String, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If state = 0 Then
                query = "SELECT EmployeID Code,Emp_Name Name FROM " & DB & ".dbo.memp_employeid"
            Else
                query = "SELECT EmployeID Code,Emp_Name Name FROM " & DB & ".dbo.memp_employeid" & _
                            " WHERE Emp_Name LIKE '%" & name & "%'"
            End If

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            cmbBox.DataSource = dtTable
            cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCustInterbranch(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Cust_Kode Code,Cust_Nama Name from " & DB & ".dbo.mcust " & _
                               "WHERE cust_branch='" & GetValueParamText("DEFAULT BRANCH") & "' " & _
                               "AND cust_type='04'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCashier(ByVal cmbBox As ComboBox, ByVal date1 As Date, ByVal date2 As Date, ByVal gridView As DataGridView)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT EmployeID Code,Emp_Name Name FROM " & DB & ".dbo.memp_employeid with (nolock) " & _
                                "WHERE EXISTS (SELECT * FROM " & DB & ".dbo.tslsh WHERE employeID=hs_employeeID " & _
                                "AND hs_invoicedate BETWEEN '" & Format(date1, formatDate) & "' AND '" & Format(date2, formatDate) & "' )"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            cmbBox.DataSource = dtTable
            cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
            cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName


            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCostCenter(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT CostCenter Code,[Description] Name FROM " & DB & ".dbo.mcostcenter " & _
                                "WHERE Company='" & GetValueParamText("DEFAULT COMPANY") & "' " & _
                                "AND Branch='" & GetValueParamText("DEFAULT BRANCH") & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If
            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadSalesOffice(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT SalesOffice Code,Name FROM " & DB & ".dbo.mso " & _
                                "WHERE branch='" & GetValueParamText("DEFAULT BRANCH") & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If
            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadProducts(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT product_code Code,Product_description Name FROM " & DB & ".dbo.mctprod "
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If
            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadCustomer(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Cust_Kode Code,Cust_Nama Customer FROM " & DB & ".dbo.mcust " & _
                                "WHERE Cust_CompanyCode='" & GetValueParamText("DEFAULT COMPANY") & "' " & _
                                "AND Cust_Branch='" & GetValueParamText("DEFAULT BRANCH") & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub GETProducts(ByVal gridView As DataGridView, ByVal group As Integer, ByVal sts As String, ByVal product As String)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand

            query = "BEGIN TRANSACTION SELECT SUBSTRING(type_partnumber,1,13) item,type_description judul,type_materialtype tipe," & _
                    "type_spl_material1 vendor,type_spl_material2 isbn,isnull(mp_nextprice,0) price," & _
                    "disc1, discpurch FROM " & DB & ".dbo.MTIPE WITH(NOLOCK) " & _
                    "INNER JOIN " & DB & ".dbo.MCTPROD WITH(NOLOCK) ON product_code=type_product AND product_group='" & group & "' " & _
                    "INNER JOIN " & DB & ".dbo.MMCA WITH(NOLOCK) ON mat_tipe=type_materialtype AND mat_status='" & sts & "' " & _
                    "INNER JOIN " & DB & ".dbo.MPRICE WITH(NOLOCK) on mp_partnumber=type_partnumber " & _
                    "INNER JOIN " & DB & ".dbo.MPDISC WITH(NOLOCK) on product=type_discgroup " & _
                    "WHERE MP_PriceGroup='" & GetValueParamText("HET PRICE") & "' " & _
                    "AND mp_effectivedate <= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                    "AND mp_expdate >= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                    "AND exists (select * from " & DB & ".dbo.hkstok WITH(NOLOCK) where stok_partnumber=TYPE_PartNumber) " & _
                    "AND salesorg='" & GetValueParamText("POS SLSORG") & "'and branch='" & GetValueParamText("DEFAULT BRANCH") & "' " & _
                    "AND salesoffice='" & GetValueParamText("POS SALESOFFICE") & "' and discgroup='01' "




            If product <> "" Then
                query = query + " AND type_product='" & product & "'"
            End If

            query = query + " COMMIT TRANSACTION"

            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            If dtTable.Rows.Count > 0 Then
                'gridView.Rows.Clear()

                'For i As Integer = 0 To dtTable.Rows.Count - 1
                '    gridView.Rows.Add( _
                '                    New Object() {dtTable.Rows(i).Item(0), dtTable.Rows(i).Item(1), _
                '                    dtTable.Rows(i).Item(2), dtTable.Rows(i).Item(3),
                '                    dtTable.Rows(i).Item(4), String.Format("{0:#,##0}", dtTable.Rows(i).Item(5))})
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
                    .Columns(6).DataPropertyName = "disc1"
                    .Columns(7).DataPropertyName = "discpurch"

                End With

                gridView.DataSource = dtTable


            Else
                gridView.DataSource = Nothing

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
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Pricegroup_Code,Pricegroup_Description" & _
                                " FROM " & DB & ".dbo.pricegroup"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            Dim col1 As DataGridViewComboBoxColumn = DirectCast(gridView.Columns(0), DataGridViewComboBoxColumn)
            col1.DataSource = dtTable
            col1.ValueMember = dtTable.Columns.Item(0).ColumnName
            col1.DisplayMember = dtTable.Columns.Item(1).ColumnName

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadUOM(ByVal gridView As DataGridView)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT UOM_Code,UOM_Description" & _
                                " FROM " & DB & ".dbo.uom"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            Dim col1 As DataGridViewComboBoxColumn = DirectCast(gridView.Columns(3), DataGridViewComboBoxColumn)
            col1.DataSource = dtTable
            col1.ValueMember = dtTable.Columns.Item(0).ColumnName
            col1.DisplayMember = dtTable.Columns.Item(0).ColumnName

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadWarehouse(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT WH_Kode Code,WH_Description Name FROM " & DB & ".dbo.mwh " & _
                                "WHERE wh_branch='" & GetValueParamText("DEFAULT BRANCH") & "'"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadWarehousePAM(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT WH_Kode Code,WH_Description Name FROM " & DB & ".dbo.mwh " & _
                                "WHERE wh_branch='" & GetValueParamText("DEFAULT BRANCH") & "' " & _
                                "AND wh_kode <> '" & GetValueParamText("DEFAULT WH") & "'"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadSupplier(ByVal cmb As ComboBox, ByVal gridView As DataGridView, ByVal name As String, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            If state = 0 Then
                query = "SELECT SUP_supplier Code,SUP_Name Name FROM " & DB & ".dbo.mspl WHERE sup_sts=0"
            Else
                query = "SELECT SUP_supplier Code,SUP_Name Name FROM " & DB & ".dbo.mspl" & _
                            " WHERE SUP_Name LIKE '%" & name & "%' AND sup_sts=0"
            End If

            With cm
                .Connection = cn
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            cmb.DataSource = dtTable
            cmb.ValueMember = dtTable.Columns.Item(0).ColumnName
            cmb.DisplayMember = dtTable.Columns.Item(1).ColumnName

            gridView.DataSource = dtTable


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadMaterialType(ByVal cmbBox As ComboBox, ByVal gridView As DataGridView, ByVal state As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT mat_tipe Type,mat_Description [Description]" & _
                                " FROM " & DB & ".dbo.mmca WHERE mat_status in ('C','G')"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If state = 0 Then
                cmbBox.DataSource = dtTable
                cmbBox.ValueMember = dtTable.Columns.Item(0).ColumnName
                cmbBox.DisplayMember = dtTable.Columns.Item(1).ColumnName
            End If

            gridView.DataSource = dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Function GetProdhier1Item(ByVal prodhier1 As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT type_partnumber FROM " & DB & ".dbo.mtipe " & _
                                "WHERE type_prodhier1='" & prodhier1 & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            Return dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Function GetProdhier5Item(ByVal prodhier5 As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT type_partnumber FROM " & DB & ".dbo.mtipe " & _
                                "WHERE type_prodhier5='" & prodhier5 & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            Return dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Sub LoadUsers(ByVal gridview As DataGridView)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT uid_user,uid_name,uid_password,uid_information," & _
                                "uid_blocksts,iud_usergroup,uid_createuser,uid_createdate,uid_createtime " & _
                                "FROM " & DB & ".dbo.musers "
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            'With gridview
            '    .AutoGenerateColumns = False
            '    .Columns(0).DataPropertyName = "uid_user"
            '    .Columns(1).DataPropertyName = "uid_name"
            '    .Columns(2).DataPropertyName = "uid_information"
            '    .Columns(3).DataPropertyName = "uid_blocksts"
            '    .Columns(4).DataPropertyName = "usergroup_description"




            'End With


            'gridview.DataSource = dtTable

            If dtTable.Rows.Count > 0 Then
                For i As Integer = 0 To dtTable.Rows.Count - 1
                    gridview.Rows.Add(New Object() {dtTable.Rows(i).Item("uid_user") _
                                                       , dtTable.Rows(i).Item("uid_name") _
                                                       , dtTable.Rows(i).Item("uid_password") _
                                                       , dtTable.Rows(i).Item("uid_information") _
                                                       , dtTable.Rows(i).Item("uid_blocksts") _
                                                       , dtTable.Rows(i).Item("iud_usergroup") _
                                                       , dtTable.Rows(i).Item("uid_createuser") _
                                                       , dtTable.Rows(i).Item("uid_createdate") _
                                                       , dtTable.Rows(i).Item("uid_createtime")})
                Next

            End If




            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Sub LoadUserInformation(ByVal gridview As DataGridView, ByVal active As Integer)
        Try
            dtTable = New datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT user_id,user_fullname,user_groupid," & _
                                "user_email,user_locked,user_active " & _
                                "FROM " & DB & ".dbo.musers " & _
                                "WHERE user_active='" & active & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            With gridview
                .AutoGenerateColumns = False
                .Columns(0).DataPropertyName = "user_id"
                .Columns(1).DataPropertyName = "user_fullname"
                .Columns(2).DataPropertyName = "user_groupid"
                .Columns(3).DataPropertyName = "user_email"
                .Columns(4).DataPropertyName = "user_locked"
                .Columns(5).DataPropertyName = "user_active"



            End With


            gridview.DataSource = dtTable




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
            total = total + gridfilter.Rows(i).Height
        Next


        If total > 150 Then
            Return 150
        Else
            Return total + 40
        End If

    End Function

    Public Function GetLastTransNo(ByVal transid As String) As String

        Dim doc As String = ""
        Dim number As String = ""
        Dim data As New DataTable
        Dim last As Integer = 0

        Try
            ''If transid <> "RC" Then
            ''    doc = GetTempPOS(transid)
            ''Else
            ''    doc = ""
            ''End If
            doc = GetTempPOS(transid)

createLastDoc:
            If doc = "" Then
                Dim mdigit As String = GetValueParamText("DIGIT")
                data = GetCounterDetail(transid)

                If data.Rows.Count = 0 Then
                    CreateNewCounter(transid)

                    data = GetCounterDetail(transid)

                End If

                last = data.Rows(0).Item(1) + 1

                If Len(CStr(last)) = 1 Then
                    number = Mid(mdigit, 1, 5) & last
                ElseIf Len(CStr(last)) = 2 Then
                    number = Mid(mdigit, 1, 4) & last
                ElseIf Len(CStr(last)) = 3 Then
                    number = Mid(mdigit, 1, 3) & last
                ElseIf Len(CStr(last)) = 4 Then
                    number = Mid(mdigit, 1, 2) & last
                ElseIf Len(CStr(last)) = 5 Then
                    number = Mid(mdigit, 1, 1) & last
                Else
                    number = last
                End If
                doc = GetValueParamText("DEFAULT COMPANY") & GetValueParamText("DEFAULT BRANCH") & Right(data.Rows(0).Item(0), 2) & number & transid


                If Not CheckHistoryPOS(doc, transid) = True Then
                    InsertHistoryPOS(doc, transid)
                    UpdateCounter(last, data.Rows(0).Item(0), transid)
                Else
                    'create ulang last doc
                    doc = ""
                    GoTo createLastDoc
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try



        Return doc


    End Function

End Module
