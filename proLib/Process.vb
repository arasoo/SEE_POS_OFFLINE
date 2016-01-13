Imports genLib.General
Imports connlib.DBConnection
Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic.Strings

Public Class Process
    Private Shared val As Boolean = False
    Private Shared num As Integer
    Private Shared data As DataTable
    Private Shared TempDate As DateTime
    Private Shared TempStr As String
    Private Shared TempNum As Integer
    Private Shared TempMon As Decimal

    Public Shared Function CheckStockMinus(ByVal item As String, ByVal qty As Integer) As Boolean
        Dim result As Boolean = False
        Dim mstock As Integer

        If Not GetValueParamNumber("STOCK MINUS") = 1 Then
            mstock = GetStock(item, GetValueParamText("DEFAULT BRANCH"), GetValueParamText("DEFAULT WH"))
            If CInt(qty) > mstock Then
                result = True
            Else
                result = False
            End If
        Else
            result = False
        End If

        Return result
    End Function

    Public Shared Function GetValueParamDate(ByVal kode As String) As Date

        For i As Integer = 0 To tblParam.Rows.Count - 1
            If tblParam.Rows(i).Item(1) = kode Then
                TempDate = tblParam.Rows(i).Item("Param_Value")
                Exit For
            End If
        Next

        Return TempDate
    End Function

    Public Shared Function GetValueParamText(ByVal kode As String) As String

        For i As Integer = 0 To tblParam.Rows.Count - 1
            If tblParam.Rows(i).Item(1) = kode Then
                TempStr = Trim(tblParam.Rows(i).Item("Param_Value"))
                Exit For
            End If
        Next

        Return TempStr
    End Function

    Public Shared Function GetComputerName() As String
        Dim ComputerName As String
        ComputerName = System.Net.Dns.GetHostName
        Return ComputerName
    End Function

    Public Shared Function GetValueParamFlag(ByVal kode As String) As String

        For i As Integer = 0 To tblParam.Rows.Count - 1
            If tblParam.Rows(i).Item(1) = kode Then
                TempStr = tblParam.Rows(i).Item("Param_Flag")
                Exit For
            End If
        Next

        Return TempStr
    End Function

    Public Shared Function GetValueParamNumber(ByVal kode As String) As Integer

        For i As Integer = 0 To tblParam.Rows.Count - 1
            If tblParam.Rows(i).Item(1) = kode Then
                TempNum = tblParam.Rows(i).Item("Param_Value")
                Exit For
            End If
        Next

        Return TempNum
    End Function

    Public Shared Function GetValueParamMoney(ByVal kode As String) As Decimal

        For i As Integer = 0 To tblParam.Rows.Count - 1
            If tblParam.Rows(i).Item(1) = kode Then
                TempMon = tblParam.Rows(i).Item("Param_Value")
                Exit For
            End If
        Next

        Return TempMon
    End Function


    Public Shared Function GetTimeNow() As String
        Dim time As String = ""

        time = Format(Now, "HH:mm:ss")

        Return time
    End Function

    Public Shared Sub NewTransactionStock(ByVal company As String, ByVal branch As String, ByVal tgl As String _
                                         , ByVal transid As String, ByVal custsupp As String, ByVal custsuppcode As String, _
                                         ByVal doc As String, ByVal wh As String, ByVal item As String, _
                                         ByVal qty As Integer, ByVal transtype As String, ByVal product As String, ByVal sts As Integer, _
                                         ByVal judul As String, ByVal uom As String)


        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 1000
                .CommandText = "INSERT INTO " & DB & ".dbo.hkstok " & _
                                               " VALUES ('" & company & "','" & branch & "','" & tgl & "'" & _
                                               ",'" & item & "','" & qty & "'" & _
                                               ",'" & transid & "','" & transtype & "',0,'" & doc & "','" & custsupp & "'" & _
                                               ",'" & custsuppcode & "','" & product & "','" & wh & "','" & sts & "')"
                .ExecuteNonQuery()


            End With

            With cm
                .Connection = cn
                .CommandTimeout = 1000
                If PartExitst(item, branch, wh) = True Then
                    If transtype = "-" Then
                        .CommandText = "UPDATE " & DB & ".dbo.mpart " & _
                                            " SET part_consigmentstock=part_consigmentstock " & transtype & " " & qty & _
                                            ",part_rfsstock=part_rfsstock " & transtype & " " & qty & _
                                            ",part_description='" & Replace(judul, "'", "''") & "'" & _
                                            " WHERE part_partnumber='" & item & "'" & _
                                            " AND Part_Branch='" & branch & "'" & _
                                            " AND Part_WH = '" & wh & "'"
                    Else
                        '.CommandText = "UPDATE " & DB & ".dbo.mpart " & _
                        '                    " SET part_rfsstock=part_rfsstock " & transtype & " " & qty & _
                        '                    ",part_totalstock=part_totalstock " & transtype & " " & qty & _
                        '                    ",part_description='" & Replace(judul, "'", "''") & "'" & _
                        '                    " WHERE part_partnumber='" & item & "'" & _
                        '                    " AND Part_Branch='" & branch & "'" & _
                        '                    " AND Part_WH = '" & wh & "'"
                    End If

                Else
                    If transtype = "-" Then
                        .CommandText = "INSERT INTO " & DB & ".dbo.mpart " & _
                                                                          " VALUES ('" & company & "','" & branch & "'," & _
                                                                          "'" & item & "','" & product & "','" & branch & "','" & wh & "'," & _
                                                                          "'" & Replace(judul, "'", "''") & "','" & uom & "',0,0,0,0,0,0,0,'" & 0 - qty & "',0,0,0,0,0,0,0,0," & _
                                                                          "'" & 0 - qty & "',0,0,0,0,0,0,'','',0,0)"
                    Else
                        .CommandText = "INSERT INTO " & DB & ".dbo.mpart " & _
                                                                           " VALUES ('" & company & "','" & branch & "'," & _
                                                                           "'" & item & "','" & product & "','" & branch & "','" & wh & "'," & _
                                                                           "'" & Replace(judul, "'", "''") & "','" & uom & "',0,0,0,0,0,0,0," & qty & ",0,0,0,0,0,0,0,0," & _
                                                                           "" & qty & ",0,0,0," & qty & ",0,0,'','',0,0)"
                    End If

                End If


                .ExecuteNonQuery()

            End With


        Catch ex As Exception

            Throw ex
        End Try
    End Sub

    Private Shared Function PartExitst(ByVal kode As String, ByVal b As String, ByVal w As String) As Boolean
        Dim result As Boolean = False
        data = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "SELECT part_partnumber FROM " & DB & ".dbo.mpart" & _
                                " WHERE part_partnumber='" & kode & "' " & _
                                " AND Part_Branch='" & b & "'" & _
                                " AND Part_WH='" & w & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With

            If data.Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return result
    End Function

    Public Shared Function GetStock(ByVal kode As String, ByVal b As String, ByVal w As String) As Integer
        Dim totalqty As Integer = 0

        Try
            data = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()
            With cm
                .Connection = cn
                .CommandText = "SELECT part_rfsstock FROM " & DB & ".dbo.mpart" & _
                                " WHERE Part_partnumber='" & kode & "'" & _
                                " AND Part_Branch='" & b & "'" & _
                                " AND Part_WH='" & w & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With

            If data.Rows.Count > 0 Then
                If IsDBNull(data.Rows(0).Item(0)) Then
                    totalqty = 0
                Else
                    totalqty = data.Rows(0).Item(0)
                End If


            Else

                If IsDBNull(data.Rows(0).Item(0)) Then

                    totalqty = 0
                Else
                    totalqty = 0
                End If


            End If
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return totalqty
    End Function

    Public Shared Function GetOmset(ByVal userid As String, ByVal tgl As DateTime) As DataTable
        Try
            data = New DataTable
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandTimeout = 180
                .CommandText = "WITH temp (cash_amount,card_amount,bank_charge,voucher_amount) AS (select case when tpayrecd.Paytype='01' then SUM(tpayrecd.Amount+tpayrecd.ChargeAmount) end Cash_Amount," & _
                                "case when tpayrecd.Paytype='02' then SUM(isnull(tpayrecd.Amount,0)) end Card_Amount," & _
                                "case when tpayrecd.Paytype='02' then SUM(isnull(tpayrecd.BankChargeAmt,0)) end Bank_Charge," & _
                                "case when tpayrecd.Paytype='04' then SUM(isnull(tpayrecd.Amount,0)) end Voucher_Amount from tpayrecd " & _
                                "inner join tpayrech on tpayrecd.ReceiptNo=tpayrech.ReceiptNo " & _
                                "WHERE tpayrech.EmployeeID='" & userid & "' and " & _
                                "tpayrech.createdate >= '" & Format(tgl, "yyyy-MM-dd HH:mm:ss") & "' group by tpayrecd.Paytype union all " & _
                                "select case when tpayrecd.Paytype='01' then SUM(tpayrecd.Amount+tpayrecd.ChargeAmount) end Cash_Amount," & _
                                "case when tpayrecd.Paytype='02' then SUM(isnull(tpayrecd.Amount,0)) end Card_Amount," & _
                                "case when tpayrecd.Paytype='02' then SUM(isnull(tpayrecd.BankChargeAmt,0)) end Bank_Charge," & _
                                "case when tpayrecd.Paytype='04' then SUM(isnull(tpayrecd.Amount,0)) end Voucher_Amount from tpayrecd " & _
                                "inner join tpayrech on tpayrecd.ReceiptNo=tpayrech.ReceiptNo " & _
                                "inner join torderh on tpayrech.salesorderno=hs_orderno " & _
                                "WHERE tpayrech.EmployeeID='" & userid & "' and " & _
                                "tpayrech.createdate >= '" & Format(tgl, "yyyy-MM-dd HH:mm:ss") & "' group by tpayrecd.Paytype) " & _
                                "SELECT SUM(cash_amount),SUM(card_amount),SUM(bank_charge),SUM(voucher_amount) FROM temp"

            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With

            cm.CommandTimeout = 30
            cn.Close()
        Catch ex As Exception

            cm.CommandTimeout = 30
            cn.Close()
            Throw ex
        End Try

        Return data
    End Function

    Public Shared Function GenerateVoucherCode() As String
        Dim r As New Random
        Dim key As String

        key = RandomString(r)

        Dim Key1 As String = key.Substring(0, 4) + "-" + _
                             key.Substring(4, 4) + "-" + _
                             key.Substring(8, 4) + "-" + _
                             key.Substring(12, 4)

        Return Key1

    End Function
   
    Private Shared Function RandomString(ByVal r As Random)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim sb As New StringBuilder
        Dim cnt As Integer = r.Next(15, 33)
        For i As Integer = 1 To cnt
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next


        Return sb.ToString()
    End Function

    Public Shared Function GetDiscBestPrice(discPurch As Decimal, disc1 As Decimal)
        Dim maxdisc As Decimal = GetValueParamMoney("MAX DISC")
        Dim minMargin As Decimal = GetValueParamMoney("MIN MARGIN")
        Dim bestprice As Decimal = 0
        Dim discDiff As Decimal = 0


        discDiff = discPurch - minMargin

        If discDiff > maxdisc Then
            bestprice = maxdisc
        Else
            bestprice = discDiff

        End If

        Return bestprice

    End Function


End Class
