Imports genLib.General
Imports proLib.Process
Imports connLib.DBConnection
Imports iniLib.Ini
Imports secuLib.Security
Imports System.Data.SqlClient

Public Class Sql

    Private Shared dtTable As DataTable

#Region "Reporting"

    Public Shared Function ClipboardItemLastReceive(ByVal data As DataTable, opt As Integer) As DataTable
        dtTable = New DataTable

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "BEGIN TRANSACTION DELETE FROM Tool.dbo.Sams COMMIT TRANSACTION"
                .ExecuteNonQuery()
            End With

            cn.Close()


            For a As Integer = 0 To data.Rows.Count - 1
                If Trim(data.Rows(a).Item(0)) <> "" Then

                    If opt = 0 Then
                        query = "BEGIN TRANSACTION " & _
                                "INSERT INTO Tool.dbo.Sams " & _
                                "SELECT TOP 1 stok_partnumber FROM " & DB & ".dbo.hkstok " & _
                                "WHERE stok_partnumber='" & data.Rows(a).Item(0) & "' " & _
                                "AND Stok_txcode IN ('GR102','GR101','GR410') " & _
                                "COMMIT TRANSACTION"

                    ElseIf opt = 1 Then
                        query = "BEGIN TRANSACTION " & _
                                "INSERT INTO Tool.dbo.Sams " & _
                                "SELECT type_partnumber FROM " & DB & ".dbo.mtipe " & _
                                "WHERE type_prodhier1='" & data.Rows(a).Item(0) & "' " & _
                                "AND EXISTS (SELECT * FROM " & DB & ".dbo.hkstok " & _
                                "WHERE stok_partnumber=type_partnumber " & _
                                "AND Stok_txcode IN ('GR102','GR101','GR410')) " & _
                                "COMMIT TRANSACTION"

                    Else
                        query = "BEGIN TRANSACTION " & _
                                "INSERT INTO Tool.dbo.Sams " & _
                                "SELECT type_partnumber FROM " & DB & ".dbo.mtipe " & _
                                "WHERE type_prodhier5='" & data.Rows(a).Item(0) & "' " & _
                                "AND EXISTS (SELECT * FROM " & DB & ".dbo.hkstok " & _
                                "WHERE stok_partnumber=type_partnumber " & _
                                "AND Stok_txcode IN ('GR102','GR101','GR410')) " & _
                                "COMMIT TRANSACTION"
                    End If


                    If cn.State = ConnectionState.Closed Then cn.Open()
                    With cm
                        .Connection = cn
                        .CommandTimeout = 0
                        .CommandText = query
                        .ExecuteNonQuery()
                    End With
                    cn.Close()
                End If

            Next

            If cn.State = ConnectionState.Closed Then cn.Open()
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "BEGIN TRANSACTION SELECT Item,type_description Description," &
                                "(SELECT TOP 1 stok_date FROM " & DB & ".dbo.hkstok " &
                                "WHERE stok_partnumber=item AND stok_txcode in ('GR102','GR101','GR410') " &
                                "ORDER BY stok_date DESC)BM_Date," &
                                "(SELECT TOP 1 stok_qty FROM " & DB & ".dbo.hkstok " &
                                "WHERE stok_partnumber=item AND stok_txcode in ('GR102','GR101','GR410')  " &
                                "ORDER BY stok_date DESC)BM_Qty " &
                                "FROM tool.dbo.sams " &
                                "INNER JOIN " & DB & ".dbo.mtipe ON type_partnumber=item " &
                                "COMMIT TRANSACTION"
            End With

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

    Public Shared Function ClipboardItemRealStock(ByVal data As DataTable, opt As Integer, search As String) As DataTable
        dtTable = New DataTable

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "BEGIN TRANSACTION DELETE FROM Tool.dbo.tbl_real COMMIT TRANSACTION"
                .ExecuteNonQuery()
            End With

            cn.Close()

            If opt = 0 Then
                For a As Integer = 0 To data.Rows.Count - 1
                    If Trim(data.Rows(a).Item(0)) <> "" Then



                        query = "BEGIN TRANSACTION " & _
                                "INSERT INTO Tool.dbo.tbl_real " & _
                                "SELECT type_partnumber,type_description,part_rfsstock,0 FROM " & DB & ".dbo.mtipe WITH(NOLOCK)" & _
                                "INNER JOIN " & DB & ".dbo.MPART WITH(NOLOCK) ON part_partnumber=type_partnumber " & _
                                        "AND part_wh='" & GetValueParamText("DEFAULT WH") & "' " & _
                                "WHERE type_Partnumber='" & data.Rows(a).Item(0) & "' " & _
                                "AND EXISTS (SELECT * FROM " & DB & ".dbo.hkstok WITH(NOLOCK) " & _
                                "WHERE stok_partnumber=type_partnumber " & _
                                "AND Stok_txcode IN ('GR102','GR101','GR410','GR501','INIT')) " & _
                                "COMMIT TRANSACTION"


                        If cn.State = ConnectionState.Closed Then cn.Open()
                        With cm
                            .Connection = cn
                            .CommandTimeout = 0
                            .CommandText = query
                            .ExecuteNonQuery()
                        End With
                        cn.Close()
                    End If

                Next
            ElseIf opt = 1 Then
                query = "BEGIN TRANSACTION " & _
                        "INSERT INTO Tool.dbo.tbl_real " & _
                        "SELECT type_partnumber,type_description,part_rfsstock,0 FROM " & DB & ".dbo.mtipe WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.MPART WITH(NOLOCK) ON part_partnumber=type_partnumber " & _
                                "AND part_wh='" & GetValueParamText("DEFAULT WH") & "' " & _
                        "WHERE type_prodhier1='" & search & "' " & _
                        "AND EXISTS (SELECT * FROM " & DB & ".dbo.hkstok WITH(NOLOCK) " & _
                        "WHERE stok_partnumber=type_partnumber " & _
                        "AND Stok_txcode IN ('GR102','GR101','GR410','GR501','INIT')) " & _
                        "COMMIT TRANSACTION"

                If cn.State = ConnectionState.Closed Then cn.Open()
                With cm
                    .Connection = cn
                    .CommandTimeout = 0
                    .CommandText = query
                    .ExecuteNonQuery()
                End With
                cn.Close()

            Else
                query = "BEGIN TRANSACTION " & _
                        "INSERT INTO Tool.dbo.tbl_real " & _
                        "SELECT type_partnumber,type_description,part_rfsstock,0 FROM " & DB & ".dbo.mtipe WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.MPART WITH(NOLOCK) ON part_partnumber=type_partnumber " & _
                                "AND part_wh='" & GetValueParamText("DEFAULT WH") & "' " & _
                        "WHERE type_prodhier5='" & search & "' " & _
                        "AND EXISTS (SELECT * FROM " & DB & ".dbo.hkstok WITH(NOLOCK) " & _
                        "WHERE stok_partnumber=type_partnumber " & _
                        "AND Stok_txcode IN ('GR102','GR101','GR410','GR501','INIT')) " & _
                        "COMMIT TRANSACTION"

                If cn.State = ConnectionState.Closed Then cn.Open()
                With cm
                    .Connection = cn
                    .CommandTimeout = 0
                    .CommandText = query
                    .ExecuteNonQuery()
                End With
                cn.Close()


            End If


            If cn.State = ConnectionState.Closed Then cn.Open()
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "BEGIN TRANSACTION " & _
                                "EXECUTE " & DB & ".dbo.P_GET_STOCK_REAL " & _
                                "COMMIT TRANSACTION"
            End With

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            cn.Close()
            Return dtTable


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try


    End Function

    Public Shared Function ReportBestSeller(ByVal top As Integer, ByVal startDate As Date, ByVal endDate As Date, _
                                  ByVal group As String, ByVal wh As String, ByVal branch As String, ByVal org As String) As DataTable

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            If GetValueParamNumber("SYSTEM SQL") = 0 Then
                query = "EXECUTE " & DB & ".dbo.P_BestSeller '" & top & "','" & Format(startDate, formatDate) & "','" & Format(endDate, formatDate) & "','" & group & "','" & wh & "'," & _
                                                  "'" & branch & "','" & org & "'"

            Else
                query = "SELECT TOP " & top & " LTRIM(RTRIM(tslsd.DS_PartNumber)) Item,mtipe.TYPE_Description Judul,mctprod.Product_Description Product," & _
                        "mtipe.type_uom UOM,mprice.mp_nextprice HET,CAST(SUM(tslsd.DS_Qty) AS INT) Sales,CAST(mpart.PART_RFSStock AS INT) Stock " & _
                        "FROM " & DB & ".dbo.tslsd WITH(NOLOCK) " & _
                        "INNER JOIN tslsh WITH(NOLOCK) on hs_invoicedate BETWEEN '" & Format(startDate, formatDate) & "' " & _
                        "AND '" & Format(endDate, formatDate) & "' AND tslsh.hs_branch='" & branch & "' " & _
                        "AND tslsh.hs_salesorg='" & org & "'  AND tslsh.hs_warehouse='" & wh & "' AND tslsh.HS_Invoice=tslsd.DS_Invoice " & _
                        "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on mtipe.type_product<>'120' and mtipe.type_status<>1 AND mtipe.TYPE_PartNumber=tslsd.DS_PartNumber " & _
                        "INNER JOIN " & DB & ".dbo.mctprod WITH(NOLOCK) on mctprod.PRODUCT_GROUP='" & group & "' AND mctprod.product_code=mtipe.TYPE_Product " & _
                        "INNER JOIN " & DB & ".dbo.mpart WITH(NOLOCK) on mpart.part_wh='" & wh & "' AND mpart.PART_PartNumber=tslsd.DS_PartNumber " & _
                        "INNER JOIN " & DB & ".dbo.mprice WITH(NOLOCK) ON mprice.mp_partnumber=tslsd.DS_PartNumber " & _
                        "WHERE tslsd.DS_InvoiceDate BETWEEN '" & Format(startDate, formatDate) & "' AND " & _
                        "'" & Format(endDate, formatDate) & "' " & _
                        "AND mprice.mp_pricegroup='" & GetValueParamText("HET PRICE") & "' " & _
                        "AND mprice.mp_effectivedate <= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                        "AND mprice.mp_expdate >= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                        "GROUP BY tslsd.DS_PartNumber,mtipe.TYPE_Description,mctprod.Product_Description,mtipe.type_uom,mpart.PART_RFSStock,mprice.mp_nextprice " & _
                        "ORDER BY SUM(tslsd.DS_Qty) DESC"
            End If




            cm = New SqlCommand
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

            cn.Close()

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try



        Return dtTable

    End Function

    Public Shared Function GetDetailItemPOS(ByVal kode As String, ByVal discgroup As String, ByVal pricegroup As String) As DataTable
        dtTable = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT TOP 1 type_partnumber,type_description,type_uom,Param_D1,Disc1 Disc1_Rate,Param_D2,Disc2 Disc2_Rate," &
                                "Param_D3,Disc3 Disc3_Rate,Param_D4,Disc4 Disc4_Rate,Param_D5,DiscPurch," &
                                "Role Disc_Role,mp_currentprice,mp_nextprice,mp_effectivedate," &
                                "mp_expdate,type_taxgroup,type_status,type_product,product_group,ISNULL(type_prodhier1,'')type_prodhier1,ISNULL(type_prodhier2,'')type_prodhier2," &
                                "ISNULL(type_prodhier3,'')type_prodhier3,ISNULL(type_prodhier4,'')type_prodhier4,ISNULL(type_prodhier5,'')type_prodhier5,type_discgroup from " & DB & ".dbo.mtipe " &
                                "inner join (select Product_Disc,[description],role,Param_D1,Param_D2,Param_D3,Param_D4,param_d5," &
                                "Disc1,Disc2,Disc3,isnull(Disc4,0)Disc4,DiscPurch from mproddisc " &
                                "inner join mdisc on product_disc=param_group_prod " &
                                "inner join mpdisc on Product_Disc=product " &
                                "where Param_SalesOrg='" & GetValueParamText("POS SLSORG") & "' and Param_Salesoffice='" & GetValueParamText("POS SALESOFFICE") & "' " &
                                "and param_discgroup='" & discgroup & "' " &
                                "and SalesOffice='" & GetValueParamText("POS SALESOFFICE") & "' and discgroup='" & discgroup & "' and Salesorg='" & GetValueParamText("POS SLSORG") & "')as discgroup on type_discgroup=product_disc" &
                                " inner join " & DB & ".dbo.mprice on type_partnumber=mp_partnumber" &
                                " inner join " & DB & ".dbo.mctprod on product_code=type_product" &
                                " WHERE type_partnumber='" & kode & "' AND mp_pricegroup='" & pricegroup & "'" &
                                " AND mp_effectivedate <= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " &
                                " AND mp_expdate >= '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " &
                                "ORDER BY mp_effectivedate DESC"

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()

            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function RptListingWarehouseReceive(ByVal kode As String, fromDt As Date, toDt As Date, ByVal state As Integer) As DataTable
        dtTable = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            Select Case state
                Case 0
                    query = "SELECT hlbm_wrs docno,(SELECT DISTINCT dlbm_reffdoc FROM " & DB & ".dbo.twrsd" & _
                            " WHERE dlbm_wrs=hlbm_wrs)AS pono,hlbm_date docdate,hlbm_trnid trnid,hlbm_dn dn," & _
                            "hlbm_dndate dndate,hlbm_note note," & _
                            "CASE WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='Y' then 'Posted' " & _
                            "WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='N' then 'Receive' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.twrsh where hlbm_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hlbm_trnid IN ('GR102','GR101')"
                Case 1
                    query = "SELECT hlbm_wrs docno,(SELECT DISTINCT dlbm_reffdoc FROM " & DB & ".dbo.twrsd" & _
                            " WHERE dlbm_wrs=hlbm_wrs)AS pono,hlbm_date docdate,hlbm_trnid trnid,hlbm_dn dn," & _
                            "hlbm_dndate dndate,hlbm_note note," & _
                            "CASE WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='Y' then 'Posted' " & _
                            "WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='N' then 'Receive' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.twrsh where hlbm_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hlbm_trnid = 'GR410'"
                Case 2
                    query = "SELECT hlbm_wrs docno,(SELECT DISTINCT dlbm_reffdoc FROM " & DB & ".dbo.twrsd" & _
                            " WHERE dlbm_wrs=hlbm_wrs)AS pono,hlbm_date docdate,hlbm_trnid trnid,hlbm_dn dn," & _
                            "hlbm_dndate dndate,hlbm_note note," & _
                            "CASE WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='Y' then 'Posted' " & _
                            "WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='N' then 'Receive' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.twrsh where hlbm_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hlbm_trnid = 'TR100'"
                Case Else

                    query = "SELECT hlbm_wrs docno,(SELECT DISTINCT dlbm_reffdoc FROM " & DB & ".dbo.twrsd" & _
                            " WHERE dlbm_wrs=hlbm_wrs)AS pono,hlbm_date docdate,hlbm_trnid trnid,hlbm_dn dn," & _
                            "hlbm_dndate dndate,hlbm_note note," & _
                            "CASE WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='Y' then 'Posted' " & _
                            "WHEN hlbm_flag_validate='Y' AND hlbm_flag_posting='N' then 'Receive' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.twrsh where hlbm_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hlbm_trnid = 'GR401'"

            End Select

            If Trim(kode) <> "" Then
                query = query & " AND hlbm_supplier = '" & kode & "'"
            End If


            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandTimeout = 60
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()

            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function RptListingWarehouseMovement(ByVal kode As String, ByVal fromDt As Date, ByVal toDt As Date, _
                                                       ByVal state As Integer) As DataTable
        dtTable = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            Select Case state
                Case 0
                    query = "SELECT hts_doi docno,hts_date docdate,hts_trnid trnid,hts_supplier supp," & _
                            "hts_customer cust,hts_to_wh towh,hts_reffdoc dn,hts_note note," & _
                            "CASE WHEN hts_pickflag='Y' AND hts_postingflag='Y' then 'Posted' " & _
                            "WHEN hts_pickflag='Y' AND hts_postingflag='N' then 'Validate' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.ttsh WITH(NOLOCK) where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hts_trnid IN ('PN102','PN101')"

                    If Trim(kode) <> "All" Then query = query & " AND hlbm_supplier = '" & kode & "'"

                Case 1
                    query = "SELECT hts_doi docno,hts_date docdate,hts_trnid trnid,hts_supplier supp," & _
                            "hts_customer cust,hts_to_wh towh,hts_reffdoc dn,hts_note note," & _
                            "CASE WHEN hts_pickflag='Y' AND hts_postingflag='Y' then 'Posted' " & _
                            "WHEN hts_pickflag='Y' AND hts_postingflag='N' then 'Validate' " & _
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.ttsh WITH(NOLOCK) where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hts_trnid = 'MM410'"
                Case Else
                    query = "SELECT hts_doi docno,hts_date docdate,hts_trnid trnid,hts_supplier supp," &
                            "hts_customer cust,hts_to_wh towh,hts_reffdoc dn,hts_note note," &
                            "CASE WHEN hts_pickflag='Y' AND hts_postingflag='Y' then 'Posted' " &
                            "WHEN hts_pickflag='Y' AND hts_postingflag='N' then 'Validate' " &
                            "ELSE 'Draft' END sts FROM " & DB & ".dbo.ttsh WITH(NOLOCK) where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " &
                            "AND hts_trnid = 'MM106'"


            End Select




            cm = New SqlCommand

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
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()

            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetPromo(ByVal salesoffice As String, ByVal kode As String, ByVal product As String, ByVal today As Date _
                                    , ByVal discgroup As String, ByVal prodhier1 As String _
                                    , ByVal prodhier2 As String, ByVal prodhier3 As String _
                                    , ByVal prodhier4 As String, ByVal prodhier5 As String) As DataTable
        Try

            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamNumber("SYSTEM SQL") = 1 Then
                query = "SELECT MDiscD.PromoID,MdiscH.Description,MdiscH.DiscType,MDiscD.PartNumber as item," & _
                        "MDiscD.Description as description,MDiscD.Disc FROM MDiscD,MdiscH " & _
                        "WHERE ( MDiscD.PromoID = MdiscH.PromoID ) and  " & _
                        "( ( MdiscH.Salesoffice = '" & salesoffice & "' ) AND  " & _
                        "(MDiscD.PartNumber = '" & kode & "' AND  " & _
                        "MDiscD.Prodhier = '1') OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(discgroup) & "' AND  " & _
                        "MDiscD.Prodhier = '2') OR  " & _
                        "(MDiscD.PartNumber = '" & product & "' AND  " & _
                        "MDiscD.Prodhier = '3') OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(prodhier1) & "' AND  " & _
                        "MDiscD.Prodhier = '4') OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(prodhier2) & "' AND  " & _
                        "MDiscD.Prodhier = '5') OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(prodhier3) & "' AND  " & _
                        "MDiscD.Prodhier = '6')  OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(prodhier4) & "' AND  " & _
                        "MDiscD.Prodhier = '7')  OR  " & _
                        "(MDiscD.PartNumber = '" & Trim(prodhier5) & "' AND  " & _
                        "MDiscD.Prodhier = '8') ) AND " & _
                        "MdiscH.PeriodFrom <= '" & Format(today, formatDate) & "' AND  " & _
                        "MdiscH.Periodto >= '" & Format(today, formatDate) & "' AND  " & _
                        "mdisch.Validflag='Y' AND " & _
                        "mdisch.Closeflag='N' AND " & _
                        "mdisch.DiscType<>10"
            Else
                query = "EXECUTE " & DB & ".dbo.P_PROMO_DISC '" & salesoffice & "','" & kode & "'"
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

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetEventBestPrice(ByVal salesoffice As String, ByVal kode As String, ByVal product As String, ByVal today As Date _
                                    , ByVal discgroup As String, ByVal prodhier1 As String _
                                    , ByVal prodhier2 As String, ByVal prodhier3 As String _
                                    , ByVal prodhier4 As String, ByVal prodhier5 As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamNumber("SYSTEM SQL") = 1 Then
                query = "SELECT MDiscD.PromoID,MdiscH.Description,MdiscH.DiscType,MDiscD.PartNumber as item," &
                        "MDiscD.Description as description,MDiscD.Disc FROM MDiscD,MdiscH " &
                        "WHERE ( MDiscD.PromoID = MdiscH.PromoID ) and  " &
                        "( ( MdiscH.Salesoffice = '" & salesoffice & "' ) AND  " &
                        "( MdiscH.PeriodFrom <= '" & Format(today, formatDate) & "' ) AND  " &
                        "( MdiscH.Periodto >= '" & Format(today, formatDate) & "' ) AND  " &
                        "(MDiscD.PartNumber = '" & kode & "' AND  " &
                        "MDiscD.Prodhier = '1') OR  " &
                        "(MDiscD.PartNumber = '" & Trim(discgroup) & "' AND  " &
                        "MDiscD.Prodhier = '2') OR  " &
                        "(MDiscD.PartNumber = '" & product & "' AND  " &
                        "MDiscD.Prodhier = '3') OR  " &
                        "(MDiscD.PartNumber = '" & Trim(prodhier1) & "' AND  " &
                        "MDiscD.Prodhier = '4') OR  " &
                        "(MDiscD.PartNumber = '" & Trim(prodhier2) & "' AND  " &
                        "MDiscD.Prodhier = '5') OR  " &
                        "(MDiscD.PartNumber = '" & Trim(prodhier3) & "' AND  " &
                        "MDiscD.Prodhier = '6')  OR  " &
                        "(MDiscD.PartNumber = '" & Trim(prodhier4) & "' AND  " &
                        "MDiscD.Prodhier = '7')  OR  " &
                        "(MDiscD.PartNumber = '" & Trim(prodhier5) & "' AND  " &
                        "MDiscD.Prodhier = '8') ) AND " &
                        "mdisch.Validflag='Y' AND " &
                        "mdisch.Closeflag='N' AND " &
                        "mdisch.DiscType=10"
            Else
                query = "EXECUTE " & DB & ".dbo.P_PROMO_BESTPRICE '" & salesoffice & "','" & kode & "'"
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

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetDiscMember(ByVal salesoffice As String, ByVal kode As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "EXECUTE " & DB & ".dbo.P_PROMO_DISC_MEMBER '" & salesoffice & "','" & kode & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)

            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function MemberExists(ByVal kode As String) As Boolean
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT cust_nama FROM " & DB & ".dbo.Mcust " & _
                                "WHERE cust_kode='" & kode & "' AND cust_type='02'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)

            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function EmpIDExists(ByVal empid As String, userLogon As String) As Boolean
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT cashierbal_userid FROM " & DB & ".dbo.cashierbalance " & _
                                "WHERE cashierbal_userid='" & logOn & "' " & _
                                "AND cashierbal_employeeid='" & Trim(empid) & "' " & _
                                "AND CashierBal_CloseDate is null"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)

            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub CreateBestPrice(ByVal doc As String)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "EXECUTE " & DB & ".dbo.P_AUTO_BESTPRICE '" & doc & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Sub

    Public Shared Function ReportSalesSupplier(ByVal supp As String, ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sts As String, ByVal wh As String) As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamNumber("SYSTEM SQL") = 1 Then
                query = "SELECT ROW_NUMBER() OVER(ORDER BY sum(tslsd.ds_qty) DESC) AS iden," &
                        "mtipe.type_spl_material1 vendor,LTRIM(RTRIM(tslsd.ds_partnumber)) item," &
                        "mtipe.type_description judul,mctprod.product_description product," &
                        "(SELECT TOP 1 mprice.mp_nextprice FROM " & DB & ".dbo.mprice With(NOLOCK) " &
                        "WHERE  mprice.mp_partnumber=tslsd.ds_partnumber " &
                        "And mprice.mp_pricegroup='01' " &
                        "AND mprice.mp_effectivedate <= '" & Format(CDate(GetValueParamText("SYSTEM DATE")), formatDate) & "' " &
                        "AND mprice.mp_expdate >= '" & Format(CDate(GetValueParamText("SYSTEM DATE")), formatDate) & "' " &
                        "ORDER BY mprice.mp_effectivedate DESC) AS purchase," &
                        "sum(tslsd.ds_qty) qty,ROUND(sum(tslsd.ds_dpp+tslsd.ds_ppn),-2) amount," &
                        "CASE WHEN mmca.mat_status='C' then 'Consignment' else 'Credit' end sts," &
                        "mpart.part_rfsstock stock FROM " & DB & ".dbo.tslsd WITH(NOLOCK) " &
                        "INNER JOIN " & DB & ".dbo.tslsh WITH(NOLOCK) on tslsh.hs_invoicedate " &
                                "BETWEEN '" & Format(dtFrom, formatDate) & "' AND '" & Format(dtTo, formatDate) & "' AND tslsh.hs_warehouse='" & wh & "' " &
                                "AND tslsd.ds_invoice=tslsh.hs_invoice " &
                        "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on mtipe.type_prodhier5='" & supp & "' " &
                                "AND mtipe.type_partnumber=tslsd.ds_partnumber " &
                        "INNER JOIN " & DB & ".dbo.mpart WITH(NOLOCK) on mpart.part_wh='" & wh & "' " &
                                "AND mpart.part_partnumber=tslsd.ds_partnumber " &
                        "INNER JOIN " & DB & ".dbo.mmca WITH(NOLOCK) ON mmca.mat_tipe=mtipe.type_materialtype " &
                        "INNER JOIN " & DB & ".dbo.mctprod WITH(NOLOCK) on type_product=product_code " &
                        "WHERE tslsd.ds_invoicedate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
                             "AND '" & Format(dtTo, formatDate) & "' "

                If sts <> "" Then
                    query = query + "AND mat_status='" & sts & "' "

                End If
                query = query + "GROUP BY tslsd.ds_partnumber,mtipe.type_description,mctprod.product_description," &
                                "mtipe.type_spl_material1,mmca.mat_status,mpart.part_rfsstock " &
                        "ORDER BY sum(tslsd.ds_qty) DESC"
            Else
                query = "EXECUTE " & DB & ".dbo.P_SALES_SUPPLIER '" & supp & "','" & Format(dtFrom, formatDate) & "'," &
                                            "'" & Format(dtTo, formatDate) & "','" & sts & "','" & wh & "' "
            End If
            cm = New SqlCommand
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

            cn.Close()
        Catch ex As Exception

            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function ReportStockAgingSupplier(ByVal wh As String, ByVal sts As String, _
                                                    ByVal group As String, ByVal supp As String, _
                                                    ByVal zero As String, n0 As Date, n1 As Date, _
                                                    n2 As Date, n3 As Date, n4 As Date, n5 As Date) As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamNumber("SYSTEM SQL") = 1 Then
                query = "SELECT LTRIM(RTRIM(type_partnumber)) Item,LTRIM(RTRIM(type_description)) Judul," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) from TSLSD WITH(NOLOCK) " & _
                        "WHERE MONTH(ds_invoicedate)=DATEPART(mm,'" & n5 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n5 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N5," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) FROM " & DB & ".dbo.TSLSD WITH(NOLOCK) " & _
                        "WHERE month(ds_invoicedate)=DATEPART(mm,'" & n4 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n4 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N4," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) FROM " & DB & ".dbo.TSLSD WITH(NOLOCK) " & _
                        "WHERE month(ds_invoicedate)=DATEPART(mm,'" & n3 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n3 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N3," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) FROM " & DB & ".dbo.TSLSD WITH(NOLOCK) " & _
                        "WHERE month(ds_invoicedate)=DATEPART(mm,'" & n2 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n2 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N2," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) FROM " & DB & ".dbo.TSLSD WITH(NOLOCK) " & _
                        "WHERE MONTH(ds_invoicedate)=DATEPART(mm,'" & n1 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n1 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N1," & _
                        "(SELECT CAST(ISNULL(SUM(ds_qty),0) AS INTEGER) FROM " & DB & ".dbo.TSLSD WITH(NOLOCK) " & _
                        "WHERE month(ds_invoicedate)=DATEPART(mm,'" & n0 & "') " & _
                        "AND YEAR(ds_invoicedate)=DATEPART(yyyy,'" & n0 & "') " & _
                        "AND DS_PartNumber=TYPE_PartNumber)N0," & _
                        "CAST(part_rfsstock AS INTEGER) Stock,(select TOP 1 hlbm_wrs FROM " & DB & ".dbo.TWRSD WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.TWRSH WITH(NOLOCK) on HLBM_WRS=DLBM_WRS " & _
                        "WHERE DLBM_PartNumber = TYPE_PartNumber " & _
                        "AND HLBM_TrnID in ('GR102','GR101','GR410') " & _
                        "ORDER BY HLBM_Date desc )Last_BM," & _
                        "(SELECT TOP 1 hlbm_date FROM " & DB & ".dbo.TWRSD WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.TWRSH on HLBM_WRS=DLBM_WRS " & _
                        "WHERE DLBM_PartNumber = TYPE_PartNumber " & _
                        "AND HLBM_TrnID in ('GR102','GR101','GR410') " & _
                        "ORDER BY HLBM_Date desc )BM_Date," & _
                        "(SELECT top 1 CAST(DLBM_StockQty AS INTEGER) FROM " & DB & ".dbo.TWRSD WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.TWRSH on HLBM_WRS=DLBM_WRS " & _
                        "WHERE DLBM_PartNumber = TYPE_PartNumber " & _
                        "AND HLBM_TrnID in ('GR102','GR101','GR410') " & _
                        "ORDER BY HLBM_Date desc )BM_Qty," & _
                        "(SELECT TOP 1 hlbm_dn FROM " & DB & ".dbo.TWRSD WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.TWRSH on HLBM_WRS=DLBM_WRS " & _
                        "WHERE DLBM_PartNumber = TYPE_PartNumber " & _
                        "AND HLBM_TrnID in ('GR102','GR101','GR410') " & _
                        "ORDER BY HLBM_Date desc )DN," & _
                        "ISNULL((SELECT TOP 1 TMatReqd.requestno FROM " & DB & ".dbo.TMatReqd WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.TMatReqH WITH(NOLOCK) ON tmatreqh.Requestno=TMatreqD.Requestno " & _
                        "WHERE Partnumber = TYPE_PartNumber AND Warehouse= '" & wh & "' " & _
                        "ORDER BY DocumentDate ),'')MR FROM " & DB & ".dbo.MTIPE WITH(NOLOCK) " & _
                        "INNER JOIN " & DB & ".dbo.MCTPROD on PRODUCT_Code=TYPE_Product " & _
                        "INNER JOIN " & DB & ".dbo.MPART on part_partnumber=type_partnumber and PART_WH='" & wh & "' "

                If zero = "Y" Then
                    query = query + "AND part_rfsstock <> 0 "
                Else
                    query = query + "AND part_rfsstock = 0 "
                End If

                query = query + "AND EXISTS (SELECT * FROM " & DB & ".dbo.HKSTOK WITH(NOLOCK) " & _
                        "WHERE stok_warehouse='" & wh & "' AND STOK_PartNumber=PART_PartNumber) " & _
                        "WHERE type_status<>1 and type_prodhier5='" & supp & "' " & _
                        "AND EXISTS (select * FROM " & DB & ".dbo.HKSTOK WITH(NOLOCK) WHERE stok_warehouse='" & wh & "' " & _
                        "AND STOK_PartNumber=TYPE_PartNumber) AND product_group='" & group & "' " & _
                        "AND EXISTS (select * FROM " & DB & ".dbo.MMCA WITH(NOLOCK) " & _
                        "WHERE mat_status='" & sts & "' AND Mat_Tipe=TYPE_MaterialType)"

            Else
                query = "EXECUTE " & DB & ".dbo.P_STOCK_AGING '" & wh & "','" & sts & "'," & _
                                            "'" & group & "','" & zero & "','" & Format(n0, formatDate) & "'," & _
                                            "'" & Format(n1, formatDate) & "','" & Format(n2, formatDate) & "'," & _
                                            "'" & Format(n3, formatDate) & "','" & Format(n4, formatDate) & "', " & _
                                            "'" & Format(n5, formatDate) & "'"
            End If

            cm = New SqlCommand
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

            cn.Close()
        Catch ex As Exception
            cm.CommandTimeout = 30
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function ReportSalesProducts(ByVal slsorg As String, ByVal dtFrom As Date, ByVal dtTo As Date, _
                                               product As String, ByVal sts As String) As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT LTRIM(RTRIM(tslsd.ds_partnumber)) AS Item,mtipe.TYPE_Description2 [Description]," & _
                    "mctprod.PRODUCT_Description Product,CAST(SUM(tslsd.ds_qty) AS integer)Qty," & _
                    "SUM(tslsd.ds_dpp+tslsd.ds_ppn) AS Amount " & _
                    "FROM " & DB & ".dbo.tslsd WITH(NOLOCK) " & _
                    "INNER JOIN " & DB & ".dbo.tslsh WITH(NOLOCK) on tslsh.hs_invoice=tslsd.ds_invoice " & _
                             "AND hs_invoicedate BETWEEN '" & Format(dtFrom, formatDate) & "' AND '" & Format(dtTo, formatDate) & "' " & _
                             "AND hs_salesorg LIKE '%" & slsorg & "%' " & _
                    "INNER JOIN " & DB & ".dbo.MTIPE WITH(NOLOCK) on mtipe.TYPE_PartNumber=tslsd.ds_partnumber " & _
                             "AND MTIPE.type_product LIKE '%" & product & "%' " & _
                    "INNER JOIN " & DB & ".dbo.MCTPROD WITH(NOLOCK) on MCTPROD.PRODUCT_Code=MTIPE.TYPE_Product " & _
                    "INNER JOIN " & DB & ".dbo.MMCA WITH(NOLOCK) on MMCA.mat_tipe=MTIPE.type_materialtype " & _
                            "AND MMCA.mat_status LIKE '%" & sts & "%' " & _
                    "WHERE ds_invoicedate BETWEEN '" & Format(dtFrom, formatDate) & "' AND '" & Format(dtTo, formatDate) & "' " & _
                    "GROUP BY tslsd.DS_PartNumber,mtipe.TYPE_Description2,MCTPROD.PRODUCT_Description"


            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandType = CommandType.Text
                .CommandText = query
                End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
                End With

        Catch ex As Exception

            cn.Close()
            Throw ex
        Finally
            cn.Close()
        End Try

        Return dtTable
    End Function

    Public Shared Function ReportBaseOnSupplier(ByVal dtFrom As Date, ByVal dtTo As Date) As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "with temp (code,name,consi,credit) AS ( " &
                    "select sup_supplier,sup_name,SUM(ds_ppn+ds_dpp),0 from tslsd WITH(NOLOCK) " &
                    "inner join MTIPE WITH(NOLOCK) on TYPE_PartNumber=DS_PartNumber " &
                            "and TYPE_MaterialType in ('510','520','610') " &
                            "and TYPE_Status<>1 " &
                    "inner join MSPL WITH(NOLOCK) on SUP_Supplier=TYPE_ProdHier5 and SUP_Sts=0 " &
                    "where ds_invoicedate between '" & Format(dtFrom, formatDate) & "' " &
                    "and '" & Format(dtTo, formatDate) & "' " &
                    "group by sup_supplier,sup_name " &
                    "union all " &
                    "select sup_supplier,sup_name,0,SUM(ds_ppn+ds_dpp) from tslsd WITH(NOLOCK) " &
                    "inner join MTIPE WITH(NOLOCK) on TYPE_PartNumber=DS_PartNumber " &
                            "and TYPE_MaterialType in ('001','002','600') " &
                            "and TYPE_Status<>1 " &
                    "inner join MSPL WITH(NOLOCK) on SUP_Supplier=TYPE_ProdHier5 and SUP_Sts=0 " &
                    "where ds_invoicedate between '" & Format(dtFrom, formatDate) & "' " &
                    "and '" & Format(dtTo, formatDate) & "' " &
                    "group by sup_supplier,sup_name " &
                    ") SELECT code,name,sum(consi)consi,sum(credit)credit from temp WITH(NOLOCK) " &
                    "group by code,name "

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandType = CommandType.Text
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

        Catch ex As Exception
            cn.Close()
            Throw ex
        Finally
            cn.Close()
        End Try

        Return dtTable
    End Function

    Public Shared Function ReportBaseOnSupplier2() As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "with temp (code,name,consi,credit) AS( " &
                  "select type_prodhier5,sup_name,SUM(part_rfsstock * mp_nextprice),0 " &
                  "FROM " & DB & ".dbo.MTIPE WITH (NOLOCK) " &
                  "inner join MPRICE on MP_PartNumber=TYPE_PartNumber and MP_PriceGroup='01' " &
                  "left join " & DB & ".dbo.MSPL With (NOLOCK) On SUP_Supplier=TYPE_ProdHier5 " &
                  "inner join " & DB & ".dbo.MPART WITH (NOLOCK) on PART_PartNumber=TYPE_PartNumber " &
                  "AND PART_WH='" & GetValueParamText("DEFAULT WH") & "' " &
                  "where TYPE_MaterialType in ('510','520','610') " &
                  "AND MP_EffectiveDate <= GETDATE() and MP_ExpDate >= GETDATE() " &
                  "group by type_prodhier5,sup_name " &
                  "having sum(part_rfsstock)<>0 " &
                  "union all " &
                  "select type_prodhier5,sup_name,0,SUM(part_rfsstock*mp_nextprice) " &
                  "FROM " & DB & ".dbo.MTIPE WITH (NOLOCK) " &
                  "inner join MPRICE on MP_PartNumber=TYPE_PartNumber and MP_PriceGroup='01' " &
                  "left join " & DB & ".dbo.MSPL WITH (NOLOCK) on SUP_Supplier=TYPE_ProdHier5 " &
                  "inner join " & DB & ".dbo.MPART WITH (NOLOCK) on PART_PartNumber=TYPE_PartNumber " &
                  "AND PART_WH='" & GetValueParamText("DEFAULT WH") & "' " &
                  "where TYPE_MaterialType in ('001','002','600') " &
                  "AND MP_EffectiveDate <= GETDATE() and MP_ExpDate >= GETDATE() " &
                  "group by type_prodhier5,sup_name " &
                  "having sum(part_rfsstock)<>0 " &
                  ")SELECT code,name,sum(consi)consi,sum(credit)credit from temp " &
                   "group by code,name " &
                   "order by name "

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandType = CommandType.Text
                .CommandText = query
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

        Catch ex As Exception
            cn.Close()
            Throw ex
        Finally
            cn.Close()
        End Try

        Return dtTable
    End Function

    Public Shared Function ReportInventoryDemand(ByVal period As Date, ByVal group As String, ByVal sts As String, _
                                                 ByVal product As String, ByVal wh As String, ByVal opt As String, _
                                                 ByVal optStock As String) As DataTable
        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand()


            With cm

                .CommandTimeout = 180
                .Connection = cn

                .CommandText = "EXECUTE " & DB & ".dbo.P_INVENTORY_DEMAND '" & Format(period, formatDate) & "','" & Trim(group) & "'," & _
                                            "'" & sts

            query = "" & wh & "','" & opt & "','" & optStock + 1 & "'"


            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            cm.CommandTimeout = 30
            cn.Close()
        Catch ex As Exception
            cm.CommandTimeout = 30
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetWarehouseStockLevel(ByVal group As String, ByVal sts As String, _
                                                  ByVal product As String, ByVal wh As String, _
                                                  ByVal state As Integer, ByVal text As String, _
                                                  ByVal OPTSTOCK As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamNumber("SYSTEM SQL") = 0 Then
                query = "EXECUTE " & DB & ".dbo.P_GETWAREHOUSESTOCKLEVEL '" & group & "'," & _
                                    "'" & sts & "','" & product & "','" & wh & "','" & state & "'," & _
                                    "'" & Trim(text) & "','" & OPTSTOCK & "'"
            Else
                query = "SELECT LTRIM(RTRIM(PART_PartNumber)) AS item,TYPE_Description name," & _
                   "type_materialtype type,product_description product,type_prodhier1 prodhier1," & _
                   "type_prodhier2 prodhier2,type_prodhier3 prodhier3,type_prodhier4 prodhier4," & _
                   "type_prodhier5 prodhier5,type_materialinfo author,PART_RFSStock stock " & _
                   "FROM " & DB & ".dbo.mpart WITH(NOLOCK) " & _
                   "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on TYPE_PartNumber=PART_PartNumber AND type_status<>1 " & _
                   "INNER JOIN " & DB & ".dbo.mctprod WITH(NOLOCK) on TYPE_Product=PRODUCT_code  AND product_group LIKE '%" & group & "%' " & _
                   "INNER JOIN " & DB & ".dbo.mmca WITH(NOLOCK) on mat_tipe=type_materialtype AND mat_status LIKE '%" & sts & "%' " & _
                   "WHERE PART_WH='" & wh & "' " & _
                   "AND EXISTS(SELECT * FROM " & DB & ".dbo.hkstok WITH(NOLOCK) WHERE stok_partnumber=part_partnumber " & _
                   "AND stok_warehouse='" & wh & "') "

                If state = 0 Then
                    If OPTSTOCK = "0" Then
                        query = query + "AND part_rfsstock>0 "
                    ElseIf OPTSTOCK = "1" Then
                        query = query + "AND part_rfsstock=0 "
                    Else
                        query = query + "AND part_rfsstock<0 "
                    End If

                End If

                If state = 0 Then
                    If product = "" Then
                        query = query + "AND TYPE_Product LIKE '%" & product & "%'"
                    Else
                        query = query + "AND TYPE_Product = '" & product & "'"
                    End If

                ElseIf state = 1 Then
                    query = query + "AND type_partnumber LIKE '%" & Trim(text) & "%'"
                ElseIf state = 2 Then
                    query = query + "AND type_description LIKE '%" & Trim(text) & "%'"
                ElseIf state = 3 Then
                    query = query + "AND type_spl_material2 LIKE '%" & Trim(text) & "%'"
                ElseIf state = 4 Then
                    query = query + "AND type_prodhier1 = '" & Trim(text) & "'"
                ElseIf state = 5 Then
                    query = query + "AND type_prodhier4 = '" & Trim(text) & "'"
                Else
                    query = query + "AND type_prodhier5 = '" & Trim(text) & "'"
                End If
            End If


            cm = New SqlCommand
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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Private Shared Function LastDayOfMonth(ByVal aDate As DateTime) As Date

        Return New DateTime(aDate.Year, _
           aDate.Month, _
                            DateTime.DaysInMonth(aDate.Year, aDate.Month))
    End Function

    Public Shared Function ReportInventorySupplier(ByVal supp As String, ByVal sts As String, ByVal wh As String) As DataTable
        Try

            Dim today As Date = CDate(GetValueParamText("SYSTEM DATE"))
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            If GetValueParamText("SYSTEM SQL") = 1 Then
                query = "DECLARE @tmp table(vendor varchar(50),item varchar(13),judul varchar(255)," &
                        "product varchar(200),purchase money,discpurch money,stock int," &
                        "sts varchar(30)) "

                query = query + "INSERT INTO @tmp " &
                    "SELECT " &
                     "type_spl_material1 vendor,LTRIM(RTRIM(part_partnumber)) item," &
                     "type_description judul,product_description product," &
                     "(SELECT TOP 1 mp_nextprice FROM mprice WHERE mp_partnumber=part_partnumber " &
                     "AND mp_pricegroup='01' AND mp_effectivedate <= '" & Format(today, formatDate) & "' " &
                     "AND mp_expdate >= '" & Format(today, formatDate) & "') purchase,discpurch," &
                     "part_rfsstock stock,'' FROM mpart " &
                     "INNER JOIN " & DB & ".dbo.mtipe on type_partnumber=part_partnumber " &
                     "INNER JOIN " & DB & ".dbo.mmca on type_materialtype=mat_tipe " &
                     "INNER JOIN " & DB & ".dbo.mctprod on type_product=product_code " &
                     "INNER JOIN " & DB & ".dbo.mpdisc on type_discgroup=product WHERE type_prodhier5='" & supp & "' "

                If sts <> "" Then
                    query = query + " AND mat_status='" & sts & "' "
                End If

                query = query + "and part_wh='" & wh & "' and exists(select * FROM " & DB & ".dbo.hkstok " &
                     "where stok_partnumber=part_partnumber " &
                     "and stok_txcode IN ('GR102','GR101','GR410','GR501')) and discgroup='01' " &
                     "and salesorg='" & GetValueParamText("POS SLSORG") & "' " &
                     "And salesoffice='" & GetValueParamText("POS SALESOFFICE") & "' And part_rfsstock <> 0 " &
                     "GROUP BY part_partnumber,type_description,product_description," &
                         "type_spl_material1,part_rfsstock,discpurch " &
                     "ORDER BY part_rfsstock DESC "

                query = query + "SELECT vendor,item,judul,product,purchase,discpurch" &
                        ",stock,ISNULL((purchase - (purchase * discpurch/100)) * stock,0) As amount,sts " &
                        "FROM @tmp"
            Else
                query = "EXECUTE " & DB & ".dbo.P_INVENTORY_SUPPLIER '" & supp & "'," &
                                            "'" & sts & "','" & wh & "' "
            End If

            cm = New SqlCommand
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

            cn.Close()
        Catch ex As Exception

            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function ListingSalesHeader(ByVal emp As String, ByVal dtFrom As Date, _
                                             ByVal dtTo As Date, ByVal OPT As String) As DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            'If GetValueParamText("SYSTEM SQL") = 1 Then
            '    query = "DECLARE @temp table(invoice varchar(14),receipt varchar(14),pay1 char(1),pay2 char(1),pay3 char(1)) "
            '    query = query + " ;with temp (invoice,receipt,pay1,pay2,pay3) AS ( " &
            '            "select tpayrech.salesorderno,tpayrech.receiptno,'01',0,0 FROM " & DB & ".dbo.tpayrecd WITH(NOLOCK)" &
            '            "INNER JOIN " & DB & ".dbo.tpayrech WITH(NOLOCK) On " &
            '            "tpayrech.documentdate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
            '            "And '" & Format(dtTo, formatDate) & "' " &
            '            "And EmployeeID Like '%" & IIf(emp = "Any", "", Trim(emp)) & "%' and tpayrech.receiptno=tpayrecd.receiptno " &
            '            "WHERE tpayrecd.paytype='01' " &
            '            "union all " &
            '            "select tpayrech.salesorderno,tpayrech.receiptno,0,'02',0 from tpayrecd WITH(NOLOCK) " &
            '            "INNER JOIN " & DB & ".dbo.tpayrech WITH(NOLOCK) on " &
            '             "tpayrech.documentdate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
            '            "And '" & Format(dtTo, formatDate) & "' " &
            '            "And EmployeeID Like '%" & IIf(emp = "Any", "", Trim(emp)) & "%' and tpayrech.receiptno=tpayrecd.receiptno " &
            '            "WHERE tpayrecd.paytype='02' " &
            '            "union all " &
            '            "select tpayrech.salesorderno,tpayrech.receiptno,0,0,'04' from tpayrecd WITH(NOLOCK) " &
            '            "INNER JOIN " & DB & ".dbo.tpayrech WITH(NOLOCK) on " &
            '             "tpayrech.documentdate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
            '            "And '" & Format(dtTo, formatDate) & "' " &
            '            "And EmployeeID Like '%" & IIf(emp = "Any", "", Trim(emp)) & "%' and tpayrech.receiptno=tpayrecd.receiptno " &
            '            "WHERE tpayrecd.paytype='04') " &
            '            "INSERT INTO @temp " &
            '            "SELECT invoice,receipt,SUM(pay1) pay1,SUM(pay2) pay2,SUM(pay3) pay3 from temp WITH(NOLOCK) " &
            '            "GROUP BY invoice,receipt " &
            '            "Select hs_invoice Invoice,hs_invoicedate [Date],hs_warehouse Warehouse," &
            '            "hs_customer Customer, hs_currency Currency, hs_grossamount Gross," &
            '            "hs_disc3_afteramt DPP, hs_ppn PPN, hs_totalAmount Total," &
            '            "cashamount+returnamount Cash, cardamount Card, Charges," &
            '            "hs_roundingamt Rounding, hs_employeeid Emp " &
            '            "FROM " & DB & ".dbo.tslsh WITH(NOLOCK) " &
            '            "INNER Join @temp WITH(NOLOCK) ON invoice=hs_invoice " &
            '            "INNER JOIN " & DB & ".dbo.tpayrech WITH(NOLOCK) ON " &
            '            "tpayrech.documentdate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
            '            "AND '" & Format(dtTo, formatDate) & "' AND receiptno = receipt " &
            '            "WHERE hs_invoicedate BETWEEN '" & Format(dtFrom, formatDate) & "' " &
            '            "AND '" & Format(dtTo, formatDate) & "' "

            '    Select Case OPT
            '        Case "1"
            '            query = query + "AND pay2=0 AND pay1=1 AND pay3=0 "
            '        Case "2"
            '            query = query + "AND pay2=2 And pay1=0 And pay3=0 "
            '        Case "3"
            '            query = query + "AND pay2=0 And pay1=0 And pay3=4 "
            '        Case "4"
            '            query = query + "AND pay2=2 And pay1=1 And pay3=0 "
            '        Case "5"
            '            query = query + "AND pay2=0 And pay1=1 And pay3=4 "
            '        Case "6"
            '            query = query + "AND pay2=2 And pay1=0 And pay3=4 "
            '        Case "7"
            '            query = query + "AND pay2=2 And pay1=1 And pay3=4 "
            '        Case Else '0'
            '            'do nothing'
            '    End Select

            '    query = query + "AND hs_branch='" & Default_Branch & "' And EmployeeID Like '%" & IIf(emp = "Any", "", Trim(emp)) & "%'"

            query = "EXECUTE " & DB & ".dbo.P_LISTING_SALES '" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                                 "'" & Format(dtFrom, formatDate) & "','" & Format(dtTo, "yyyy-MM-dd") & "'," &
                                                 "'" & IIf(emp = "Any", "", Trim(emp)) & "','" & OPT & "'"


            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 180
                .CommandText = query


            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With



            Return dtTable
            cm.CommandTimeout = 30
            cn.Close()
        Catch ex As Exception
            cn.Close()
            cm.CommandTimeout = 30
            Throw ex
        End Try
    End Function

    Public Shared Function DailySalesPayments(ByVal slsorg As String, ByVal emp As String, ByVal dtFrom As Date,
                                             ByVal dtTo As Date) As DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable


            query = "EXECUTE " & DB & ".dbo.P_DAILY_SALES_PAYMENTS '" & Format(dtFrom, formatDate) & "','" & Format(dtTo, "yyyy-MM-dd") & "'," &
                                "'" & slsorg & "','" & emp & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 180
                .CommandText = query


            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With



            Return dtTable
            cm.CommandTimeout = 30
            cn.Close()
        Catch ex As Exception
            cn.Close()
            cm.CommandTimeout = 30
            Throw ex
        End Try
    End Function

    Public Shared Function ClosingCashier(ByVal dtFrom As Date,
                                             ByVal dtTo As Date) As DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable


            query = "SELECT cashierbal_id,cashierbal_userid,cashierbal_opendate" &
                            ",cashierbal_amount,cashierbal_totalcash" &
                            ",cashierbal_totalcard,cashierbal_totalcharge," &
                            "cashierbal_totalvoucher,cashierbal_closedate," &
                            "cashierbal_note,cashierbal_employeeId " &
                            "FROM " & DB & ".dbo.cashierbalance " &
                    "WHERE YEAR(cashierbal_opendate)='" & Format(dtFrom, "yyyy") & "' " &
                    "AND MONTH(cashierbal_opendate)='" & Format(dtFrom, "MM") & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 180
                .CommandText = query


            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With



            Return dtTable
            cm.CommandTimeout = 30
            cn.Close()
        Catch ex As Exception
            cn.Close()
            cm.CommandTimeout = 30
            Throw ex
        End Try
    End Function

    Public Shared Function ListingReceiveDetail(ByVal doc As String) As DataTable
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "Select RTRIM(LTRIM(dlbm_partnumber)) Item,type_description Judul," & _
                                "type_uom UOM,dlbm_stockqty Qty " & _
                                "FROM " & DB & ".dbo.twrsd " & _
                                "INNER JOIN " & DB & ".dbo.mtipe On type_partnumber=dlbm_partnumber " & _
                                "WHERE dlbm_wrs = '" & doc & "' ORDER BY type_description ASC"
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

    Public Shared Function ListingSalesDetail(ByVal doc As String) As DataTable
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "SELECT RTRIM(LTRIM(ds_partnumber)) Item,type_description Judul," & _
                                "ds_qty Qty,ds_dpp dpp,ds_ppn ppn,ds_dpp+ds_ppn amount " & _
                                "FROM " & DB & ".dbo.tslsd " & _
                                "INNER JOIN " & DB & ".dbo.mtipe on type_partnumber=ds_partnumber " & _
                                "WHERE ds_invoice = '" & doc & "' ORDER BY type_description ASC"
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

    Public Shared Function ListingMovementDetail(ByVal kode As String, ByVal fromDt As Date, ByVal toDt As Date, _
                                                       ByVal state As Integer) As DataTable
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            cm = New SqlCommand
            Select Case state
                Case 0
                    query = "SELECT hts_doi docno,hts_date docdate,type_spl_material1 vendor," & _
                            "LTRIM(RTRIM(dts_partnumber)) item,type_description judul,type_uom uom,dts_qty qty " & _
                            "FROM " & DB & ".dbo.ttsh INNER JOIN " & DB & ".dbo.ttsd WITH(NOLOCK) ON dts_doi=hts_doi " & _
                            "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on type_partnumber=dts_partnumber " & _
                            "where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " & _
                            "AND hts_trnid IN ('PN102','PN101') "

                    If kode <> "All" Then query = query & "AND hts_supplier='" & kode & "'"
                Case 1
                    query = "SELECT hts_doi docno,hts_date docdate,type_spl_material1 vendor," &
                            "LTRIM(RTRIM(dts_partnumber)) item,type_description judul,type_uom uom,dts_qty qty " &
                            "FROM " & DB & ".dbo.ttsh INNER JOIN " & DB & ".dbo.ttsd WITH(NOLOCK) ON dts_doi=hts_doi " &
                            "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on type_partnumber=dts_partnumber " &
                            "where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " &
                            "AND hts_trnid = 'MM410' "

                    If kode <> "All" Then query = query & "AND hts_wh='" & kode & "'"
                Case Else
                    query = "SELECT hts_doi docno,hts_date docdate,type_spl_material1 vendor," &
                            "LTRIM(RTRIM(dts_partnumber)) item,type_description judul,type_uom uom,dts_qty qty " &
                            "FROM " & DB & ".dbo.ttsh INNER JOIN " & DB & ".dbo.ttsd WITH(NOLOCK) ON dts_doi=hts_doi " &
                            "INNER JOIN " & DB & ".dbo.mtipe WITH(NOLOCK) on type_partnumber=dts_partnumber " &
                            "where hts_date BETWEEN '" & Format(fromDt, formatDate) & "' AND '" & Format(toDt, formatDate) & "' " &
                            "AND hts_trnid = 'MM106' "

                    If kode <> "All" Then query = query & "AND hts_wh='" & kode & "'"


            End Select



            With cm
                .Connection = cn
                .CommandText = query
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

    Public Shared Function GetRoundingAmt(ByVal dtFrom As Date, ByVal dtTo As Date) As Decimal
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT SUM(Hs_RoundingAmt) FROM " & DB & ".dbo.tslsh" & _
                                " WHERE hs_Invoicedate BETWEEN '" & Format(dtFrom, "yyyy-MM-dd") & "'" & _
                                " AND '" & Format(dtTo, "yyyy-MM-dd") & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
        Catch ex As Exception
            Throw ex
        End Try

        If Not IsDBNull(dtTable.Rows(0).Item(0)) Then
            Return dtTable.Rows(0).Item(0)
        Else
            Return 0
        End If
    End Function

    Public Shared Function GetRoundingAmtbyEmp(ByVal dtFrom As Date, ByVal dtTo As Date, empid As String) As Decimal
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT SUM(Hs_RoundingAmt) FROM " & DB & ".dbo.tslsh" & _
                                " WHERE hs_Invoicedate BETWEEN '" & Format(dtFrom, "yyyy-MM-dd") & "'" & _
                                " AND '" & Format(dtTo, "yyyy-MM-dd") & "' AND hs_employeeid LIKE '%" & Trim(empid) & "%' "
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
        Catch ex As Exception
            Throw ex
        End Try

        If Not IsDBNull(dtTable.Rows(0).Item(0)) Then
            Return dtTable.Rows(0).Item(0)
        Else
            Return 0
        End If
    End Function

    Public Shared Function ReportSummarySalesItem(ByVal group As String, ByVal sts As String,
                                                  ByVal dtFrom As Date, ByVal dtTo As Date,
                                                  ByVal OPTPAYTYPE As String) As DataTable
        Try


            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable

            query = "EXECUTE " & DB & ".dbo.P_SUMMARY_SALES_ITEM '" & Format(dtFrom, formatDate) & "'," & _
                                    "'" & Format(dtTo, formatDate) & "','" & group & "','" & sts & "'," & _
                                    "'" & OPTPAYTYPE & "'"
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = query


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

    Public Shared Function GETStockCard(ByVal item As String, ByVal wh As String, ByVal fperiod As Date, ByVal tperiod As Date) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.P_STOCKCARD '" & wh & "','" & item & "','" & Format(fperiod, formatDate) & "','" & Format(tperiod, formatDate) & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GETDetailTaxOrg(ByVal code As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT tax_NPWP,tax_name,tax_address,tax_city FROM " & DB & ".dbo.mtaxorg " & _
                    "WHERE tax_organization='" & code & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GetDetailWH(ByVal code As String) As String

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT wh_description FROM " & DB & ".dbo.mwh " & _
                    "WHERE wh_kode='" & code & "'"

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

            Return dtTable.Rows(0).Item(0)
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GETDetailEmployee(ByVal code As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT employeid,emp_name,emp_title FROM " & DB & ".dbo.memp_employeid " & _
                    "WHERE employeid='" & code & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GETDetailMember(ByVal code As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT cust_nama,cust_type,cust_address_1,cust_phone," & _
                    "cust_paytype,cust_discgroup,cust_pricegroup FROM " & DB & ".dbo.mcust " & _
                    "WHERE cust_kode='" & code & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function ValidateVoucherAmt(ByVal code As String, ByRef id As String) As Decimal

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT mvoucherd.voucherid,mvoucherd.amount FROM " & DB & ".dbo.mvoucherd " & _
                    "INNER JOIN " & DB & ".dbo.mvoucherh ON mvoucherh.voucherid=mvoucherd.voucherid " & _
                    "WHERE mvoucherd.vouchercode='" & code & "' AND mvoucherd.invoice is null " & _
                    "AND validflag='Y' AND closeflag='N'"

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

            If dtTable.Rows.Count > 0 Then
                id = dtTable.Rows(0).Item(0)
                Return dtTable.Rows(0).Item(1)
            Else
                Return 0
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Sub GETTaxInvoice(ByVal company As String, ByVal branch As String, _
                                        ByVal taxorg As String, ByVal invoice As String, _
                                        ByVal period As Date, ByVal customer As String, ByVal empid As String)

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.P_PGentaxinvoice '" & company & "','" & branch & "'," & _
                                "'" & Trim(taxorg) & "','" & Trim(invoice) & "'," & _
                                "'" & Format(period, formatDate) & "','" & Trim(customer) & "','" & Trim(empid) & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Sub

    Public Shared Sub PostingSales(ByVal company As String, ByVal branch As String, _
                                       ByVal invoice As String, _
                                       Optional ByVal userPost As String = "SYSTEM", Optional ByVal sts As String = "N")

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.p_posting_sales_pos '" & company & "','" & branch & "'," & _
                                "'" & Trim(invoice) & "','" & userPost & "','" & sts & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Sub

    Public Shared Sub LockVoucherCode(ByVal id As String, ByVal code As String, ByVal invoice As String, ByVal empId As String)

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "UPDATE " & DB & ".dbo.mvoucherd SET invoice='" & invoice & "'," & _
                                    "used_at='" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," & _
                                    "employeeid='" & empId & "' " & _
                                    "WHERE voucherid='" & id & "' AND vouchercode='" & code & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub GETSystemDate()

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.P_SystemDate"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .CommandTimeout = 180
                .ExecuteNonQuery()
            End With
            cm.CommandTimeout = 30

            cn.Close()
        Catch ex As Exception
            cm.CommandTimeout = 30
            cn.Close()
            Throw ex
        End Try

    End Sub

    Public Shared Function GETSalesConsignment(ByVal company As String, ByVal branch As String, ByVal supplier As String, ByVal period As Date) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.P_SALES_CONSIGNMENT '" & company & "','" & branch & "','" & supplier & "','" & Format(period, formatDate) & "','" & Format(period, formatDate) & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function GETDOCUMENTSTATUS(ByVal branch As String, ByVal kode As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "EXECUTE " & DB & ".dbo.P_GETDOCUMENTSTATUS '" & branch & "','" & kode & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetDetailBranch() As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT coy_branch,coy_description,coy_address01,coy_address02 " & _
                               "FROM " & DB & ".dbo.mbranch WHERE coy_branch='" & GetValueParamText("DEFAULT BRANCH") & "'"

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

    Public Shared Sub RepairRFS(ByVal item As String, ByVal wh As String, ByVal saldo As Integer)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "UPDATE " & DB & ".dbo.mpart set part_consigmentstock='" & saldo & "',part_rfsstock='" & saldo & "',part_totalstock='" & saldo & "' " & _
                    "WHERE part_partnumber='" & item & "' AND part_wh='" & wh & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub


    'Master Item
    Public Shared Function GetMasterItem(ByVal opt As Integer, ByVal text As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            Select Case opt
                Case 0
                    query = "SELECT Item_Code,Item_Description,Mat_Status" & _
                       ",Item_Vendor,Item_ISBN FROM " & DB & ".dbo.items " & _
                       " INNER JOIN " & DB & ".dbo.materialtype ON Item_Type=Mat_type" & _
                       " WHERE Item_Code LIKE '" & text & "' "
                Case 1
                    query = "SELECT Item_Code,Item_Description,Mat_Status" & _
                         ",Item_Vendor,Item_ISBN FROM " & DB & ".dbo.items " & _
                         " INNER JOIN " & DB & ".dbo.materialtype ON Item_Type=Mat_type" & _
                         " WHERE Item_Description LIKE '" & text & "' "
                Case Else

            End Select

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetInterfacing() As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT urutan_proses,nama_tabel,last_doc " & _
                    "FROM interfacing_dest.dbo.tbl_temp_doc " & _
                    "ORDER BY urutan_proses ASC"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function


#End Region

#Region "Sales"
    'Point Of Sales

    Public Shared Function ValidateOpenCashier() As DataTable

        Try
            dtTable = New DataTable
            query = "SELECT CashierBal_UserId,cashierbal_employeeid FROM " & DB & ".dbo.cashierbalance" & _
                    " WHERE CashierBal_UserId='" & logOn & "'" & _
                    " AND CashierBal_CLoseDate is null"

            If cn.State = ConnectionState.Closed Then cn.Open()

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

            Return dtTable

        Catch ex As Exception

            Throw ex

        End Try


    End Function

    Public Shared Function ValidateOpenCashierEmpID(empid As String) As Boolean

        Try
            dtTable = New DataTable
            query = "SELECT CashierBal_UserId,cashierbal_employeeid FROM " & DB & ".dbo.cashierbalance" & _
                    " WHERE cashierbal_employeeid='" & empid & "'" & _
                    " AND CashierBal_CLoseDate is null"

            If cn.State = ConnectionState.Closed Then cn.Open()

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

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception

            Throw ex

        End Try


    End Function

    Public Shared Function GetOpenDetail(ByVal empId As String) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT CashierBal_OpenDate,CashierBal_Amount,Cashierbal_Shift,Emp_Name,cashierbal_employeeid" & _
                                " FROM " & DB & ".dbo.cashierbalance" & _
                                " INNER JOIN " & DB & ".dbo.memp_employeid ON cashierbal_employeeid=employeid" & _
                                " WHERE CashierBal_userid='" & empId & "'" & _
                                " AND CashierBal_CloseDate is null"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
        Catch ex As Exception
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetDatebyMonth(ByVal month As Integer, year As Integer) As DataTable
        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "select period from mendofday " & _
                                "where Month(period) = '" & month & "' " & _
                                "and year(period)='" & year & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
        Catch ex As Exception
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Sub PromoDiscount(ByVal item As String)
        query = "select event_disctype,Devent_disc,event_active from " & DB & ".dbo.eventdetail" & _
                " inner join event on devent_id=event_id" & _
                " where devent_item='" & item & "' " & _
                " and event_startdate <= '" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "' " & _
                " and event_expdate >= '" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'"
    End Sub

    Public Shared Sub VoidInvoice(ByVal doc As String, ByVal note As String)

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "EXECUTE " & DB & ".dbo.P_VOIDINVOICE '" & doc & "','" & note & "'"
                .ExecuteNonQuery()

            End With

            cn.Close()


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function GETPOSJournalCode(ByVal doc As String) As String
        Dim kode As String
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New Datatable

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT JournalCode FROM " & DB & ".dbo.mktrn_sls " & _
                                "WHERE TxCode='" & doc & "'"

            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                kode = dtTable.Rows(0).Item(0)
            Else
                kode = "000"
            End If


        Catch ex As Exception
            Throw ex
        End Try

        Return kode

    End Function

#End Region

#Region "Inventory"

    Public Shared Function GetTS(ByVal no As String, ByVal state As Integer) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If state = 0 Then
                query = "SELECT dts_partnumber,dts_qty,hts_wh,hts_to_wh FROM " & DB & ".dbo.ttsd " & _
                                  "INNER JOIN " & DB & ".dbo.ttsh ON hts_doi=dts_doi where dts_doi='" & no & "'"
            Else
                query = "EXECUTE " & DB & ".dbo.P_GETDETAILREMAIN '" & no & "'"
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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetPromoHeader(ByVal state As Integer, ByVal salesOrg As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If state = 0 Then
                query = "SELECT promoid,description,note,disctype,assignto,periodfrom,periodto," & _
                        "prodhier,validflag,closeflag,autogenerate,minpayment,createuser,createdate FROM " & DB & ".dbo.mdisch " & _
                        "WHERE closeflag='N' AND salesorg='" & salesOrg & "'"
            Else
                query = "SELECT promoid,description,note,disctype,assignto,periodfrom,periodto," & _
                       "prodhier,validflag,closeflag,autogenerate,minpayment,createuser,createdate FROM " & DB & ".dbo.mdisch " & _
                       "WHERE closeflag='Y' AND salesorg='" & salesOrg & "'"
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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetVoucherHeader(ByVal state As Integer, ByVal salesOrg As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            If state = 0 Then
                query = "SELECT voucherid,description,note,periodfrom,periodto," & _
                       "vouchertype,validflag,closeflag,createuser,createdate FROM " & DB & ".dbo.mvoucherh " & _
                        "WHERE closeflag='N' AND salesorg='" & salesOrg & "'"
            Else
                query = "SELECT voucherid,description,note,periodfrom,periodto," & _
                       "vouchertype,validflag,closeflag,createuser,createdate FROM " & DB & ".dbo.mvoucherh " & _
                       "WHERE closeflag='Y' AND salesorg='" & salesOrg & "'"
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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetPromoDetail(ByVal no As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT item,partnumber,description,disc FROM " & DB & ".dbo.mdiscd " & _
                    "WHERE promoid='" & no & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetVoucherDetail(ByVal no As String) As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT ROW_NUMBER() OVER(ORDER BY vouchercode ASC)iden,vouchercode,invoice,amount,used_at,employeeid FROM " & DB & ".dbo.mvoucherd " & _
                    "WHERE voucherid='" & no & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function TSExists(ByVal no As String) As Boolean

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT hlbm_dn FROM " & DB & ".dbo.twrsh " & _
                    "where hlbm_dn='" & no & "'"

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

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub ValidatePromo(ByVal no As String)

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "UPDATE " & DB & ".dbo.mdisch SET validflag='Y' " & _
                    "where promoid='" & no & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub ValidateVoucher(ByVal no As String)

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "UPDATE " & DB & ".dbo.mvoucherh SET validflag='Y' " & _
                    "where voucherid='" & no & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub ClosePromo(ByVal no As String)

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "UPDATE " & DB & ".dbo.mdisch SET closeFlag='Y' " & _
                    "where promoid='" & no & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub CloseVoucher(ByVal no As String)

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "UPDATE " & DB & ".dbo.mvoucherh SET closeFlag='Y' " & _
                    "where voucherid='" & no & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function GETPO(ByVal no As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT dpo_partnumber,dpo_qty,hpo_supplier,sup_name FROM " & DB & ".dbo.tpod " & _
                    "INNER JOIN " & DB & ".dbo.tpoh ON dpo_po=hpo_ponumber " & _
                    "INNER JOIN " & DB & ".dbo.mspl ON hpo_supplier=sup_supplier " & _
                    "WHERE dpo_po='" & no & "' AND dpo_supply=0"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function ValidatePOStatus(ByVal no As String, ByVal transid As String) As Boolean

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()


            query = "SELECT hpo_ponumber FROM " & DB & ".dbo.tpoh " & _
                    "WHERE hpo_ponumber='" & no & "' AND hpo_tranid='" & transid & "'"

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

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub InsertDetailBM(ByVal transid As String, ByVal doc As String, ByVal reffType As String, _
                                     ByVal reffdoc As String, ByVal dt As DataTable, ByVal custsupp As String, ByVal custsuppcode As String)
        Dim statusItem As String = ""

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn

                For i As Integer = 0 To dt.Rows.Count - 1
                    .CommandText = "INSERT INTO " & DB & ".dbo.twrsd " & _
                                    "(dlbm_wrs,dlbm_refftype,dlbm_reffdoc,dlbm_partnumber,dlbm_product,dlbm_uom,dlbm_storeuom," & _
                                    "dlbm_entryqty,dlbm_stockqty,dlbm_payqty,dlbm_binqty,dlbm_cost,dlbm_batcno,dlbm_description," & _
                                    "dlbm_costcenter,dlbm_freightcost,dlbm_costunit,dlbm_exchrate) " & _
                                    "VALUES ('" & doc & "','" & reffType & "','" & reffdoc & "','" & dt.Rows(i).Item(0) & "','" & dt.Rows(i).Item(2) & "'" & _
                                    ",'" & dt.Rows(i).Item(3) & "','" & dt.Rows(i).Item(3) & "','" & dt.Rows(i).Item(4) & "'" & _
                                    ",'" & dt.Rows(i).Item(4) & "',0,'" & dt.Rows(i).Item(4) & "',0,'','" & Replace(dt.Rows(i).Item(1), "'", "''") & "'" & _
                                    ",'',0,0,1)"
                    .ExecuteNonQuery()

                    'statusItem = GetStatusItem(dt.Rows(i).Item(0))

                    'Call NewTransactionStock(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") _
                    '                         , transid, custsupp, custsuppcode, doc, GetValueParamText("DEFAULT WH") _
                    '                         , dt.Rows(i).Item(0), dt.Rows(i).Item(4), "+", dt.Rows(i).Item(2), IIf(statusItem = "C", 5, 0) _
                    '                         , dt.Rows(i).Item(1), dt.Rows(i).Item(3))
                Next

            End With
        Catch ex As Exception
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.twrsd WHERE dlbm_wrs='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cm.CommandText = "DELETE FROM " & DB & ".dbo.hkstok WHERE stok_document='" & doc & "'"
            cm.ExecuteNonQuery()

            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function GetStatusItem(ByVal item As String) As String
        Dim sts As String = ""
        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT Mat_Status FROM " & DB & ".dbo.mtipe " & _
                                "INNER JOIN " & DB & ".dbo.mmca on type_materialtype=mat_tipe " & _
                                "WHERE type_partnumber='" & item & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            sts = dtTable.Rows(0).Item(0)

        Catch ex As Exception
            Throw ex
        End Try

        Return sts
    End Function

    Public Shared Sub InsertHeaderBM(ByVal company As String, ByVal branch As String, ByVal doc As String, ByVal tgl As DateTime _
                                    , ByVal wh As String, ByVal fwh As String, ByVal transid As String, ByVal supplier As String, ByVal dn As String _
                                    , ByVal dnDate As DateTime, ByVal note As String, ByVal type As Integer)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn


                .CommandText = "INSERT INTO " & DB & ".dbo.twrsh " & _
                                "(hlbm_company,hlbm_branch,hlbm_wrs,hlbm_date,hlbm_dc,hlbm_wh,hlbm_trnid,hlbm_type," & _
                                "hlbm_supplier,hlbm_customer,hlbm_fwh,hlbm_dn,hlbm_dndate,hlbm_note,hlbm_flag_aloc," & _
                                "hlbm_flag_rls,hlbm_flag_posting,hlbm_flag_validate,hlbm_journal,hlbm_creator,hlbm_createdate," & _
                                "hlbm_createtime,hlbm_last_modifier,hlbm_modifytime) " & _
                                "VALUES ('" & company & "','" & branch & "','" & doc & "','" & Format(tgl, formatDate) & "'" & _
                                ",'" & branch & "','" & wh & "','" & transid & "','" & type & "','" & supplier & "'" & _
                                ",'','" & fwh & "','" & dn & "','" & Format(dnDate, formatDate) & "','" & note & "','Y','Y','N','N','101','" & logOn & "'" & _
                                ",'" & Format(tgl, formatDate) & "','" & Format(Now, "HHmmss") & "'" & _
                                ",'','')"
                .ExecuteNonQuery()


            End With

            cn.Close()

        Catch ex As Exception

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.twrsd WHERE dlbm_wrs='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cm = New SqlCommand
            With cm
                .Connection = cn
                cm.CommandText = "DELETE FROM " & DB & ".dbo.hkstok WHERE stok_document='" & doc & "'"
                cm.ExecuteNonQuery()

            End With

            cn.Close()

            Throw ex
        End Try
    End Sub

    Public Shared Sub ValidateBM(ByVal doc As String, ByVal state As Integer)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            query = ""


            query = "EXECUTE " & DB & ".dbo.P_VALIDATE_WRS '" & Trim(doc) & "','" & state & "'"
            cm = New SqlCommand
            With cm
                .Connection = cn

                .CommandText = query
                .ExecuteNonQuery()


            End With
        Catch ex As Exception

            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub PostingBM(ByVal doc As String, ByVal wh As String, ByVal user As String)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            query = ""


            query = "EXECUTE " & DB & ".dbo.P_Posting_WRS_POS '" & GetValueParamText("DEFAULT BRANCH") & "'" & _
                            ",'" & Trim(wh) & "','" & Trim(doc) & "','" & Trim(user) & "'"
            cm = New SqlCommand
            With cm
                .Connection = cn

                .CommandText = query
                .ExecuteNonQuery()


            End With



        Catch ex As Exception

            cn.Close()
            Throw ex

        End Try
    End Sub

    Public Shared Sub InsertDetailTS(ByVal transid As String, ByVal doc As String, ByVal reffType As String, _
                                     ByVal reffdoc As String, ByVal dt As DataTable, ByVal custsupp As String, ByVal custsuppcode As String)
        Dim statusItem As String = ""

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn

                For i As Integer = 0 To dt.Rows.Count - 1
                    .CommandText = "INSERT INTO " & DB & ".dbo.ttsd " & _
                                    "(dts_doi,dts_partnumber,dts_product,dts_uom,dts_qty,dts_cost,dts_batchno," & _
                                    "dts_uomunit,dts_note) " & _
                                    "VALUES ('" & doc & "','" & dt.Rows(i).Item(0) & "','" & dt.Rows(i).Item(2) & "'" & _
                                    ",'" & dt.Rows(i).Item(3) & "','" & dt.Rows(i).Item(4) & "',0" & _
                                    ",'',1,'')"

                    .ExecuteNonQuery()

                    statusItem = 4

                    Call NewTransactionStock(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") _
                                             , transid, custsupp, custsuppcode, doc, GetValueParamText("DEFAULT WH") _
                                             , dt.Rows(i).Item(0), dt.Rows(i).Item(4), "-", dt.Rows(i).Item(2), statusItem _
                                             , dt.Rows(i).Item(1), dt.Rows(i).Item(3))
                Next

            End With
        Catch ex As Exception
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.ttsd WHERE dts_doi='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.hkstok WHERE stok_document='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub InsertHeaderTS(ByVal company As String, ByVal branch As String, ByVal doc As String, ByVal tgl As DateTime _
                                    , ByVal wh As String, ByVal towh As String, ByVal transid As String, ByVal supplier As String, ByVal dn As String _
                                    , ByVal note As String)
        Try

            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn


                .CommandText = "INSERT INTO " & DB & ".dbo.ttsh " & _
                                "(hts_company,hts_branch,hts_salesorg,hts_salesoffice,hts_doi,hts_date,hts_dc," & _
                                "hts_wh,hts_trnid,hts_supplier,hts_customer,hts_qq,hts_to_wh,hts_salesman," & _
                                "hts_reffdoc,hts_alocflag,hts_pickflag,hts_dnflag,hts_postingflag,hts_tpflag," & _
                                "hts_dn,hts_note,hts_deliveryroute,hts_costcenter,hts_journal,hts_counter," & _
                                "hts_createuser,hts_createdate,hts_createtime) " & _
                                "VALUES ('" & company & "','" & branch & "','','','" & doc & "','" & Format(tgl, formatDate) & "'" & _
                                ",'" & branch & "','" & wh & "','" & transid & "','','',''" & _
                                ",'" & towh & "','','','Y','Y','Y','N','Y','','" & note & "','','','000',0,'" & logOn & "'" & _
                                ",'" & Format(tgl, formatDate) & "','" & Format(Now, "HHmmss") & "')"
                .ExecuteNonQuery()


            End With
        Catch ex As Exception

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.ttsd WHERE dts_doi='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.hkstok WHERE stok_document='" & doc & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function GetCustAFFCO(ByVal kode As String) As String
        dtTable = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT CUST_AFFCO FROM " & DB & ".dbo.mcust WHERE Cust_Kode='" & kode & "' "
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
            cn.Close()
            query = ""

            If dtTable.Rows.Count > 0 Then
                Return dtTable.Rows(0).Item(0)
            Else
                Return "000"
            End If

        Catch ex As Exception

            Throw ex
        Finally
            cn.Close()
        End Try

    End Function

    Public Shared Function GetJournalTran(ByVal transid As String, affco As String) As String
        dtTable = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT JournalCode FROM " & DB & ".dbo.MKTRNAFCO " &
                    "WHERE AFFCO='" & affco & "' " &
                    "AND txCode='" & transid & "'"
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
            cn.Close()
            query = ""

            If dtTable.Rows.Count > 0 Then
                Return dtTable.Rows(0).Item(0)
            Else
                Return "000"
            End If

        Catch ex As Exception

            Throw ex
        Finally
            cn.Close()
        End Try
    End Function

    Public Shared Function GetDetailItem(ByVal kode As String) As DataTable

        dtTable = New Datatable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT type_partnumber,type_description,type_product,type_uom," &
                                "type_taxgroup,type_status,type_materialtype " &
                                "FROM " & DB & ".dbo.mtipe WHERE type_partnumber='" & kode & "' "
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
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return dtTable
    End Function

#End Region

#Region "Products"

#End Region

#Region "Customers"

#End Region

#Region "Accounting"

#End Region

#Region "Finance"

#End Region

#Region "Login User"

    Public Shared Function ValidateUserExists(ByVal uid As String) As Boolean
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New Datatable

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT uid_user,uid_name,iud_usergroup FROM " & DB & ".dbo.musers " & _
                                " WHERE uid_user='" & uid & "' AND uid_blocksts=0"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                logOn = dtTable.Rows(0).Item(0)
                UserName = dtTable.Rows(0).Item(1)
                UserGroup = dtTable.Rows(0).Item(2)
                result = True
            Else
                result = False
            End If

            cn.Close()

            Return result


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function PasswordCorrect(ByVal id As String, ByVal pass As String) As Boolean
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New Datatable

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = "SELECT UID_Password FROM " & DB & ".dbo.musers WHERE uid_user='" & id & "'"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If decryptString(dtTable.Rows(0).Item(0)) = pass Then

                result = True
            Else
                result = False

            End If
            cn.Close()

            Return result
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub AddUser(ByVal id As String, ByVal name As String, ByVal password As String _
                                    , ByVal information As String, ByVal block As Integer, ByVal group As String)
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New Datatable

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "INSERT INTO " & DB & ".dbo.musers (uid_user,uid_name,uid_password,uid_information" & _
                                ",uid_numberofattemp,uid_blocksts,uid_lastpwdlog,uid_forcechangepwd,iud_usergroup" & _
                                ",uid_createuser,uid_createdate,uid_createtime)" & _
                                " VALUES ('" & id & "','" & name & "','" & password & "','" & information & "'" & _
                                ",0,'" & block & "',0,'N','" & Trim(group) & "','" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'" & _
                                ",'" & Format(Now, "HHmmss") & "')"
                .ExecuteNonQuery()
            End With


            cn.Close()


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateUser(ByVal id As String, ByVal name As String _
                                    , ByVal information As String, ByVal block As Integer, ByVal group As String)
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New Datatable
            query = "UPDATE " & DB & ".dbo.musers SET uid_name='" & name & "'," & _
                                "uid_information='" & information & "',uid_blocksts='" & block & "'," & _
                                "iud_usergroup='" & Trim(group) & "' WHERE uid_user='" & id & "'"

            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With


            cn.Close()


        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub SetUserOnline(ByVal userid As String, ByVal status As Integer)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "UPDATE " & DB & ".dbo.musers SET User_Online='" & status & "'" & _
                                " WHERE User_Id='" & userid & "'"
                .ExecuteNonQuery()
            End With

            cn.Close()

        Catch ex As Exception

            cn.Close()
            Throw ex
        End Try
    End Sub
#End Region

#Region "HRD/GA"

    Public Shared Function GetEmployeeAll(ByVal company As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "select EmployeID,Emp_Name,Emp_Title,Emp_Birthdate,Emp_BirthPlace,Emp_BirthCOuntry,Emp_Gender," & _
                        "EMP_Bloodtype,EMP_BloodRhesus,Emp_CorrespondanceAddress1,Emp_CorrespondanceAddress2,Emp_CorrespondanceCity," & _
                        "Emp_CorrespondanceCountry,EMP_CorrespondancePhone,EMP_CorrespondanceEmail,Emp_DateofHire,Emp_Nationality," & _
                        "Emp_IDType,Emp_IDno,EMP_IDAddress,EMP_IDCity,EMP_ExpireDate,EMP_Issuedby,Emp_TaxNumber,Emp_Religion," & _
                        "Emp_Status from " & DB & ".dbo.memp_employeid WHERE emp_company='" & Trim(company) & "'"
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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetEmployeeAssignment(ByVal company As String, ByVal branch As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "select Temp_EmployeID,Emp_Name,Temp_NPK,Temp_CurrentAddress1 from memp_coy " & _
                        "inner join MEMP_EmployeID on EmployeID=Temp_EmployeID WHERE temp_company='" & Trim(company) & "' " & _
                        "AND temp_branch = '" & branch & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

#End Region

#Region "Menus"

    Public Shared Function GetMenuAccess(ByVal userid As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT userid,s.menudata,menuparent,menuform,menulevel,menuseq,menulabel," &
                    "haschild,menulock,lockmessage,isselect,isinsert,isedit,isdelete,isprint FROM " & DB & ".dbo.MENUACCESS s " &
                    "INNER JOIN menus m ON s.menudata=m.menudata " &
                    "WHERE userid='" & userid & "' ORDER BY menuparent,menuseq"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetAllMenuAccess() As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT menudata,menuparent,menuform,menulevel,menuseq,menulabel," &
                    "haschild,menulock,lockmessage FROM " & DB & ".dbo.menus ORDER BY menuparent,menuseq"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub UpdateMenuAccess(ByVal mnu As String, ByVal userid As String, ByVal col As Integer, ByVal flag As Boolean)

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            Select Case col
                Case 2 'IsSelect
                    query = "UPDATE " & DB & ".dbo.menuaccess SET isselect='" & flag & "' " & _
                            "WHERE userid='" & userid & "' AND menudata='" & mnu & "'"
                Case 3 'IsInsert
                    query = "UPDATE " & DB & ".dbo.menuaccess SET isinsert='" & flag & "' " & _
                            "WHERE userid='" & userid & "' AND menudata='" & mnu & "'"
                Case 4 'IsEdit
                    query = "UPDATE " & DB & ".dbo.menuaccess SET isedit='" & flag & "' " & _
                            "WHERE userid='" & userid & "' AND menudata='" & mnu & "'"
                Case 5 'IsDelete
                    query = "UPDATE " & DB & ".dbo.menuaccess SET isdelete='" & flag & "' " & _
                            "WHERE userid='" & userid & "' AND menudata='" & mnu & "'"
                Case Else
                    query = "UPDATE " & DB & ".dbo.menuaccess SET isprint='" & flag & "' " & _
                            "WHERE userid='" & userid & "' AND menudata='" & mnu & "'"

            End Select

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function CreateMenuAccess(ByVal menu As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "EXECUTE " & DB & ".dbo.P_CREATE_MENU_ACCESS '" & menu.ToUpper & "'"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetChildMenu(ByVal menu As String) As DataTable

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT MenuData,menuForm,menulabel,menuname FROM " & DB & ".dbo.menus " & _
                    "WHERE menuparent='" & menu & "' AND haschild=1 ORDER BY menuseq ASC"

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

            Return dtTable
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function GetCharge(ByVal empId As String, ByVal dtFrom As Date, ByVal dtTo As Date) As Decimal

        Try
            Dim total As Decimal = 0
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT SUM(charges) FROM " & DB & ".dbo.tpayrech WHERE employeeid LIKE '%" & empId & "%' " & _
                    "AND documentdate BETWEEN '" & Format(dtFrom, formatDate) & "' AND '" & Format(dtTo, formatDate) & "' "

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

            If dtTable.Rows.Count > 0 Then
                If IsDBNull(dtTable.Rows(0).Item(0)) Then
                    total = 0
                Else
                    total = dtTable.Rows(0).Item(0)
                End If
            End If

            Return total
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Sub InsertDetailBestPrice(ByVal item As String, ByVal disc As Decimal, ByVal empid As String)

        Try
            dtTable = New DataTable

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "EXECUTE " & DB & ".dbo.P_DETAIL_BESTPRICE '" & item & "','" & disc & "','" & empid & "'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub CloseEvent()

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "UPDATE " & DB & ".dbo.mdisch SET closeflag='Y' " & _
                    "WHERE periodto < '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                    "AND closeflag <> 'Y'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Sub CloseVoucher()

        Try

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "UPDATE " & DB & ".dbo.mvoucherh SET closeflag='Y' " & _
                    "WHERE periodto < '" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' " & _
                    "AND closeflag <> 'Y'"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandText = query
                .ExecuteNonQuery()
            End With


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Public Shared Function BestPriceExists() As Boolean

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT promoid FROM " & DB & ".dbo.mdisch " & _
                    "WHERE description='BEST PRICE' " & _
                    "AND periodfrom='" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'"

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

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function BestPriceOverload() As Integer

        Try
            dtTable = New Datatable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT count(mdiscd.partnumber) FROM " & DB & ".dbo.mdiscd " &
                    "INNER JOIN " & DB & ".dbo.mdisch on mdisch.promoid=mdiscd.promoid " &
                    "WHERE mdisch.description='BEST PRICE' " &
                    "AND mdisch.periodfrom='" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'"

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

            Return dtTable.Rows(0).Item(0)

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function

    Public Shared Function BestPriceDetail() As DataTable

        Try
            dtTable = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "SELECT minMargin,MaxDisc,MaxItem FROM " & DB & ".dbo.mdisch " &
                    "WHERE mdisch.description='BEST PRICE' AND mdisch.disctype=10  " &
                    "AND mdisch.periodfrom='" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "' "

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

            Return dtTable

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Function



    Public Shared Function GetParameter() As DataTable

        Try
            dtTable = New DataTable
            query = "SELECT Param_Code,Param_Description,Param_Flag,Param_Value,Param_Value_Type" & _
                    " FROM " & DB & ".dbo.mparam with (nolock)"

            If cn.State = ConnectionState.Closed Then cn.Open()

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

            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex

        End Try

        Return dtTable
    End Function


#End Region

#Region "System"

    Public Shared Sub UpdateCounter(ByVal lastdoc As Integer, ByVal year As String, ByVal kode As String)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "UPDATE " & DB & ".dbo.mcountr SET CT_Last_Number='" & lastdoc & "'" & _
                                " WHERE CT_Branch='" & Default_Branch & "'" & _
                                " AND CT_Year='" & year & "' AND CT_DocCode='" & kode & "'"
                .ExecuteNonQuery()
            End With
        Catch ex As Exception





            Throw ex
        End Try
    End Sub

    Public Shared Sub CreateNewCounter(ByVal kode As String)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "INSERT INTO " & DB & ".dbo.mcountr " & _
                               "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & GetValueParamText("DEFAULT BRANCH") & "'," & _
                               "'" & kode & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy") & "',0,'') "
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateHistoryPOS(ByVal invoice As String, ByVal kode As String)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "UPDATE " & DB & ".dbo.hdoc SET Pos_Completed=9" & _
                                " WHERE Pos_Document='" & invoice & "' AND Pos_TransDoc='" & kode & "'"
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub InsertHistoryPOS(ByVal doc As String, ByVal kode As String)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "INSERT INTO " & DB & ".dbo.hdoc " & _
                                " VALUES ('" & doc & "','" & kode & "','" & logOn & "',0," & _
                                "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "')"

                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function CheckHistoryPOS(ByVal doc As String, ByVal kode As String) As Boolean
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "SELECT pos_document FROM " & DB & ".dbo.hdoc " & _
                                " WHERE pos_document='" & doc & "' AND pos_transdoc='" & kode & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function InvoiceExists(ByVal doc As String) As Boolean
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "SELECT hs_invoice FROM " & DB & ".dbo.tslsh " &
                                " WHERE hs_invoice='" & doc & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function InvoiceDetailExists(ByVal doc As String) As Boolean
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "SELECT ds_invoice FROM " & DB & ".dbo.tslsd " &
                                " WHERE ds_invoice='" & doc & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function PaymentExists(ByVal doc As String, receipt As String) As Boolean
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "SELECT salesorderno FROM " & DB & ".dbo.tpayrech " &
                                "WHERE salesorderno='" & doc & "' " &
                                "AND receiptno='" & receipt & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function PaymentDetailExists(ByVal doc As String) As Boolean
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            dtTable = New DataTable
            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 0
                .CommandText = "SELECT receiptno FROM " & DB & ".dbo.tpayrecd " &
                                " WHERE receiptno='" & doc & "'"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With

            If dtTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function GetCounterDetail(ByVal kode As String) As DataTable
        dtTable = New DataTable

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "SELECT CT_Year,CT_Last_Number FROM " & DB & ".dbo.mcountr" & _
                                " WHERE CT_Branch='" & GetValueParamText("DEFAULT BRANCH") & "'" & _
                                " AND CT_DocCode='" & kode & "'" & _
                                " AND CT_Year='" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy") & "'"
            End With

            da = New SqlDataAdapter

            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With
        Catch ex As Exception
            Throw ex
        End Try

        Return dtTable
    End Function

    Public Shared Function GetTempPOS(ByVal kode As String) As String
        dtTable = New DataTable
        Dim no As String

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "SELECT Pos_Document FROM " & DB & ".dbo.hdoc" & _
                                " WHERE Pos_UserId='" & logOn & "'" & _
                                " AND Pos_TransDoc='" & kode & "' AND Pos_Completed=0"
            End With

            da = New SqlDataAdapter
            With da
                .SelectCommand = cm
                .Fill(dtTable)
            End With


            If dtTable.Rows.Count = 0 Then
                no = ""

            Else
                no = dtTable.Rows(0).Item(0)
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return no
    End Function

    Public Shared Sub UpdateSystemDate(ByVal d As Date, ByVal kode As String)

        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "UPDATE " & DB & ".dbo.mparam SET Param_Value='" & Format(d, formatDate) & "'" & _
                                " WHERE Param_Description='" & kode & "'"
                .ExecuteNonQuery()
            End With

            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "UPDATE " & DB & ".dbo.msystem1 SET Param_today='" & Format(d, formatDate) & "'" & _
                                " WHERE Paramsys=1"
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function DNExists(ByVal kode As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT dlbm_partnumber FROM " & DB & ".dbo.twrsd WHERE dlbm_ReffDoc='" & kode & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function ItemExists(ByVal kode As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT type_partnumber FROM " & DB & ".dbo.mtipe WHERE type_partnumber='" & kode & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function ItemAssignmentExists(ByVal kode As String, wh As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT part_partnumber FROM " & DB & ".dbo.mpart " & _
                    "WHERE part_partnumber='" & kode & "' " & _
                    "AND part_wh='" & wh & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function PriceExists(ByVal kode As String, ByVal pricegroup As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT mp_partnumber FROM " & DB & ".dbo.mprice with (nolock) " & _
                    "WHERE mp_pricegroup='" & pricegroup & "' AND mp_effectivedate <= '" & Format(Now, formatDate) & "' " & _
                    " AND mp_expdate >= '" & Format(Now, formatDate) & "' AND mp_partnumber='" & kode & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function DiscRoleExists(ByVal kode As String, ByVal discgroup As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT product_disc FROM " & DB & ".dbo.mtipe " & _
                    "INNER JOIN " & DB & ".dbo.mproddisc ON product_disc=type_discgroup " & _
                    "WHERE type_partnumber='" & kode & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function DiscPolicyExists(ByVal kode As String, ByVal discgroup As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT param_group_prod FROM " & DB & ".dbo.mtipe " & _
                    "INNER JOIN " & DB & ".dbo.mdisc ON param_group_prod=type_discgroup " & _
                    "WHERE type_partnumber='" & kode & "' AND param_salesorg='" & GetValueParamText("POS SLSORG") & "' " & _
                    "AND param_salesoffice='" & GetValueParamText("POS SALESOFFICE") & "' " & _
                    "AND param_discgroup='" & discgroup & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function DiscRateExists(ByVal kode As String, ByVal discgroup As String) As Boolean
        dtTable = New DataTable
        Dim result As Boolean = False
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT param_group_prod FROM " & DB & ".dbo.mtipe " & _
                    "INNER JOIN " & DB & ".dbo.mdisc ON param_group_prod=type_discgroup " & _
                    "WHERE type_partnumber='" & kode & "' AND param_salesorg='" & GetValueParamText("POS SLSORG") & "' " & _
                    "AND param_salesoffice='" & GetValueParamText("POS SALESOFFICE") & "' " & _
                    "AND param_discgroup='" & discgroup & "'"

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

            If dtTable.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return result
    End Function


#End Region


End Class
