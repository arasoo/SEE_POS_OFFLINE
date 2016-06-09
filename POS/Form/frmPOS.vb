Imports genLib.General
Imports connLib.DBConnection
Imports sqlLib.Sql
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic
Imports System.IO
Imports System
Imports System.IO.Ports
Imports proLib.Process
Imports mainlib
Imports iniLib.Ini
Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Text


Public Class frmPOS

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

#Region "Variable "

    'Sales org Transaction
    Private mCustomer As String = ""
    Private mWh As String = ""
    Private mSlsOrg As String = ""
    Private mSalesOffice As String = ""
    Private mSalesman As String = ""
    Private mCostCenter As String = ""

    Private state As Integer
    Private dataItem As DataTable
    Private dataCust As DataTable
    Private seqnum As Integer
    Private jml As Decimal
    Private mstock As Integer = 0
    Private data As DataTable
    Private discount As Decimal
    Private disc1Rate As Decimal = 0
    Private disc1Amt As Decimal = 0
    Private disc2Rate As Decimal = 0
    Private disc2Amt As Decimal = 0
    Private disc3Rate As Decimal = 0
    Private disc3Amt As Decimal = 0
    Private product As String = ""
    Private promoid As String = ""
    Private table As DataTable
    Private mrounding As Decimal = 0
    Private mdppAmt As Decimal = 0
    Private FILE_NAME As String
    Private mppnAmt As Decimal = 0
    Private Const mTransId As String = "FP"
    Private Const mTransIdCode As String = "FP1"
    Private mstrReceiptPrinterName = posPrinter
    Private mstrOpenDrawerCode = Chr(27) & Chr(112) & Chr(48) & Chr(55) & Chr(121)
    Private mstrPartialCutCode = Chr(27) & Chr(105)
    Private mstrFullCutCode = Chr(27) & Chr(109)
    Private mstrFullCutCode2 = Chr(27) & Chr(105)
    Private mstrFullCutCode3 = Chr(27) & Chr(112) & Chr(0) & Chr(75) & Chr(25)
    Private mstrStringToPrint As String
    Private mEmployeeID As String
    Private mMemberCode As String = ""
    Private mMemberDisc As Decimal = 0
    Private mMemberMinPayment As Decimal = 0
    Private mcqq As String

    Private branchName As String
    Private branchAddress1 As String
    Private branchAddress2 As String
    Private npwp As String
    Private tblevent As DataTable
    Private tbleventBestPrice As DataTable

    Private browseInvoice As String
    Private browseGrandTotal As Decimal
    Private browseChange As Decimal
    Private browseCash As Decimal
    Private browseCard As Decimal
    Private browseCharge As Decimal
    Private browseRounding As Decimal
    Private printstate As Integer

    Private kasir As String
    Private fPayment As frmPayment

    Private documentno As String

    Private backupPOSItem As DataTable

#End Region

    Public WriteOnly Property EmployeeID As String
        Set(ByVal value As String)
            mEmployeeID = value

        End Set
    End Property

#Region "POS Sales type Settings"

    Public WriteOnly Property Default_Customer As String
        Set(ByVal value As String)
            mCustomer = value
        End Set
    End Property

    Public WriteOnly Property Default_SlsOrg As String
        Set(ByVal value As String)
            mSlsOrg = value
        End Set
    End Property

    Public WriteOnly Property Default_SalesOffice As String
        Set(ByVal value As String)
            mSalesOffice = value
        End Set
    End Property

    Public WriteOnly Property Default_Salesman As String
        Set(ByVal value As String)
            mSalesman = value
        End Set
    End Property

    Public WriteOnly Property Default_CostCenter As String
        Set(ByVal value As String)
            mCostCenter = value
        End Set
    End Property

    Public WriteOnly Property Default_Warehouse As String
        Set(ByVal value As String)
            mWh = value
        End Set
    End Property

#End Region

    Private Sub chckScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckScan.Click
        If chckScan.Checked = True Then
            txtQty.Enabled = False
            txtQty.Text = 1
            txtItem.Focus()
        Else
            txtQty.Enabled = True
            txtQty.Text = 1
            txtItem.Focus()
        End If
    End Sub

    Private Sub frmPOS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        txtItem.Focus()
    End Sub

    Private Sub frmPOS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then 'New Transaction
            Try
                state = 1
                DetailClear()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try
        ElseIf e.KeyCode = Keys.F3 Then 'Browse List Item
            If state = 1 Then
                If frmListItem.ShowDialog = Windows.Forms.DialogResult.OK Then
                    txtItem.Text = frmListItem.GridListItem.SelectedCells(0).Value
                    frmListItem.Close()
                End If
            End If

        ElseIf e.KeyCode = Keys.F4 Then 'Validate Member
            If state = 1 Then

                If GetValueParamNumber("MEMBERSHIP") = 0 Then
                    MsgBox("Membership not active yet!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                Dim fValidateMember As frmValidateMember = New frmValidateMember

                If fValidateMember.ShowDialog = Windows.Forms.DialogResult.OK Then

                    backupPOSItem = New DataTable

                    With backupPOSItem.Columns
                        .Add("No", GetType(Integer))
                        .Add("Item", GetType(String))
                        .Add("Description", GetType(String))
                        .Add("UOM", GetType(String))
                        .Add("Qty", GetType(Integer))
                        .Add("UnitPrice", GetType(Decimal))
                        .Add("Discount", GetType(Decimal))
                        .Add("Amount", GetType(Decimal))
                        .Add("Current", GetType(Decimal))
                        .Add("TaxRate", GetType(Decimal))
                        .Add("TaxAmt", GetType(Decimal))
                        .Add("Disc1Rate", GetType(Decimal))
                        .Add("Disc1Amt", GetType(Decimal))
                        .Add("Disc2Rate", GetType(Decimal))
                        .Add("Disc2Amt", GetType(Decimal))
                        .Add("Disc3Rate", GetType(Decimal))
                        .Add("Disc3Amt", GetType(Decimal))
                        .Add("Product", GetType(String))

                    End With

                    For i As Integer = 0 To GridSales.RowCount - 1
                        With backupPOSItem
                            .Rows.Add(New Object() {GridSales.Rows(i).Cells(0).Value _
                                                   , GridSales.Rows(i).Cells(1).Value _
                                                   , GridSales.Rows(i).Cells(2).Value _
                                                   , GridSales.Rows(i).Cells(3).Value _
                                                   , GridSales.Rows(i).Cells(4).Value _
                                                   , GridSales.Rows(i).Cells(5).Value _
                                                   , GridSales.Rows(i).Cells(6).Value _
                                                   , GridSales.Rows(i).Cells(7).Value _
                                                   , GridSales.Rows(i).Cells(8).Value _
                                                   , GridSales.Rows(i).Cells(9).Value _
                                                   , GridSales.Rows(i).Cells(10).Value _
                                                   , GridSales.Rows(i).Cells(11).Value _
                                                   , GridSales.Rows(i).Cells(12).Value _
                                                   , GridSales.Rows(i).Cells(13).Value _
                                                   , GridSales.Rows(i).Cells(14).Value _
                                                   , GridSales.Rows(i).Cells(15).Value _
                                                   , GridSales.Rows(i).Cells(16).Value _
                                                   , GridSales.Rows(i).Cells(17).Value})

                        End With
                    Next

                    table = New DataTable

                    mMemberCode = Trim(fValidateMember.GetMemberCode)
                    mMemberDisc = CDec(fValidateMember.GetMemberDisc)
                    mMemberMinPayment = CDec(fValidateMember.GetMemberMinPayment)



                    If CDec(lblTotal.Text) < mMemberMinPayment Then
                        MsgBox("Member min payment must more than Rp. " & String.Format("{0:#,##0}", mMemberMinPayment), MsgBoxStyle.Information, Title)
                        mMemberCode = ""
                        lblMember.Text = "-"
                        mMemberMinPayment = 0
                    Else

                        GridSales.Rows.Clear()
                        lblTotal.Text = 0
                        lblTotalQty.Text = 0

                        For m As Integer = 0 To backupPOSItem.Rows.Count - 1
                            ReCalculateDiscItem(backupPOSItem.Rows(m).Item(1), mMemberDisc)
                      
                        Next
                        lblMember.Text = Trim(fValidateMember.GetMemberName)
                        btnCancelMember.Visible = True
                  
                    End If


                Else
                    mMemberCode = ""
                    lblMember.Text = "-"
                    mMemberMinPayment = 0
                    btnCancelMember.Visible = False
                End If
                Total()
                lblTotalQty.Text = TotalQty()

                fValidateMember.Close()
            End If

        ElseIf e.KeyCode = Keys.F5 Then 'Payment
            Payment()
        ElseIf e.KeyCode = Keys.F8 Then 'Browse Document
            Try
                If frmBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then

                    browseInvoice = frmBrowse.GridInvoice.SelectedCells(0).Value
                    browseGrandTotal = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(2).Value))
                    browseChange = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(3).Value))
                    browseCash = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(4).Value))
                    browseCard = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(5).Value))
                    browseCharge = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(6).Value))
                    browseRounding = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(7).Value))
                    lblTotal.Text = frmBrowse.GridInvoice.SelectedCells(2).Value
                    state = 3
                    DetailClear()
                    InsertDetailInvoice(browseInvoice)
                    GridSales.Enabled = False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try
        ElseIf e.KeyCode = Keys.F9 Then 'Void Transaction
            VoidTransaction()

        ElseIf e.KeyCode = Keys.F10 Then 'Auto Scan
            If chckScan.Checked = True Then
                txtQty.Enabled = True
                txtQty.Text = 1
                chckScan.Checked = False
                txtItem.Focus()
            Else
                chckScan.Checked = True
                txtQty.Enabled = False
                txtQty.Text = 1
                txtItem.Focus()
            End If

        ElseIf e.KeyCode = Keys.F11 Then 'Print
            Try
                If Not btnPrint.Enabled = False Then
                    printstate = 2
                    Call PrintInvoice("----RE-PRINT TRANSACTION----")
                    UpdatePrintNum(browseInvoice)
                    state = 0
                    DetailClear()
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        ElseIf e.KeyCode = Keys.Escape Then
            ClosingProgram()

        End If
    End Sub

    Private Sub Payment()
        Dim decTemp As Decimal
        Dim step_process As Integer = 0

        fPayment = New frmPayment

        Try
            Me.Cursor = Cursors.WaitCursor

            If GridSales.Rows.Count = 0 Then
                MsgBox("No Transaction!!", MsgBoxStyle.Exclamation, Title)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            'cek item tidak boleh minus
            For i As Integer = 0 To GridSales.RowCount - 1
                If CheckStockMinus(Trim(GridSales.Rows(i).Cells(1).Value), GridSales.Rows(i).Cells(4).Value) = True Then

                    MsgBox("Item " & Trim(GridSales.Rows(i).Cells(1).Value) & " Over Stock!!", MsgBoxStyle.Exclamation, Title)
                    Me.Cursor = Cursors.Default
                    Exit Sub

                End If
            Next

            Decimal.TryParse(lblTotal.Text, decTemp)
            Dim strTemp As String = Str(decTemp)
            strTemp = Microsoft.VisualBasic.Right(strTemp, 2)

            If CInt(strTemp) >= 50 Then
                mrounding = 100 - CInt(strTemp)
                fPayment.SubTotal = decTemp + mrounding

            Else
                mrounding = CInt(strTemp)
                fPayment.SubTotal = decTemp - mrounding
                mrounding = mrounding * -1

            End If

            If fPayment.ShowDialog = Windows.Forms.DialogResult.OK Then

                SavePayment()

                printstate = 1
                PrintInvoice("----TRANSACTION----")

                state = 0
                DetailClear()

                MsgBox("Change : " & fPayment.lblChange.Text, MsgBoxStyle.Information, "UANG KEMBALIAN")
                fPayment.Close()


            Else

                fPayment.Close()

            End If

            If GetValueParamNumber("AUTO POS") = 1 Then
                state = 1
                DetailClear()
            End If

        Catch ex As Exception


            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub SavePayment()
        Try

            Dim discPercItem As Decimal = 0
            Dim statusItem As String = ""
            Dim detailamount As Decimal = 0
            Dim detailppn As Decimal = 0

            Dim totalAmount As Decimal = 0
            Dim totalppn As Decimal = 0

            Dim mtotaldisc1 As Decimal = TotalDisc1()
            Dim mtotaldisc2 As Decimal = TotalDisc2()
            Dim mtotaldisc3 As Decimal = TotalDisc3()
            Dim mTotalPajak As Decimal = Math.Round(TotalPajak(), 2)
            Dim mPPN As Decimal = IIf(mTotalPajak = 0, 0, Default_PPN)
            Dim mDPP As Decimal = Math.Round(TotalDPP(), 2)
            Dim mGrossAmount As Decimal = Math.Round(GrossAmount(), 2)
            Dim mAfterDiscount As Decimal = Math.Round(AfterDiscount(), 2)
            Dim mTotalAmount As Decimal = mAfterDiscount + Math.Round(CDec(mTotalPajak), 2)

            Dim mchargerate As Decimal = 0
            Dim counter As Integer = 0
            Dim voucherAmt As Decimal = 0


            ct = Nothing

            If cn.State = ConnectionState.Closed Then cn.Open()

            ct = cn.BeginTransaction("Save Payment")

            'Save Detail POS
            With cm
                .Connection = cn
                .Transaction = ct
                For i As Integer = 0 To GridSales.Rows.Count - 1

                    detailamount += GridSales.Rows(i).Cells(8).Value - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(11).Value / 100)

                    If GridSales.Rows(i).Cells(13).Value > 0 Then
                        detailamount += detailamount - (CDec(detailamount) * GridSales.Rows(i).Cells(13).Value / 100)
                    End If

                    If GridSales.Rows(i).Cells(15).Value > 0 Then
                        detailamount = 0
                        detailamount += GridSales.Rows(i).Cells(8).Value - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(15).Value / 100)
                    End If

                    discPercItem = (GridSales.Rows(i).Cells(6).Value * 100) / GridSales.Rows(i).Cells(5).Value
                    detailppn = Math.Round(CDec(GridSales.Rows(i).Cells(10).Value), 2) * GridSales.Rows(i).Cells(4).Value

                    query = "INSERT INTO " & DB & ".dbo.tslsd (ds_invoice,ds_trntype,ds_partnumber,ds_orderdate,ds_invoicedate," &
                                    "ds_product,ds_batchno,ds_dn,ds_orderno,ds_price,ds_pricedisp,ds_qty,ds_uom,ds_uomunit," &
                                    "ds_disc1,ds_disc2,ds_disc3,ds_amount,ds_cogs,ds_ordered_qty,ds_pct_D4,ds_disc4,ds_disc5," &
                                    "ds_dpp,ds_pctppn,ds_pctpph,ds_pctppnbm,ds_ppn,ds_pph,ds_ppnbm,ds_internalcost,ds_internalcost2," &
                                    "ds_internalcost3,ds_internalcost4) VALUES (" &
                                    "'" & Trim(lblInvoice.Text) & "','" & mTransId & "','" & GridSales.Rows(i).Cells(1).Value & "'," &
                                    "'" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," &
                                    "'" & GridSales.Rows(i).Cells(17).Value & "','" & GridSales.Rows(i).Cells(18).Value & "','','','" & Math.Round(CDec(GridSales.Rows(i).Cells(8).Value), 2) & "'," &
                                    "'" & Math.Round(CDec(GridSales.Rows(i).Cells(5).Value), 2) & "','" & GridSales.Rows(i).Cells(4).Value & "'," &
                                    "'" & GridSales.Rows(i).Cells(3).Value & "','1','" & Math.Round(CDec(GridSales.Rows(i).Cells(11).Value), 2) & "'," &
                                    "'" & Math.Round(CDec(GridSales.Rows(i).Cells(13).Value), 2) & "','" & CDec(Math.Round(GridSales.Rows(i).Cells(15).Value, 2)) & "'," &
                                    "'" & Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value & "'," &
                                    "'0','" & GridSales.Rows(i).Cells(4).Value & "','0','0','0','" & Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value & "'," &
                                    "'" & GridSales.Rows(i).Cells(9).Value & "','0','0','" & detailppn & "'," &
                                    "'0','0','0','0','0','0')"
                    .CommandText = query
                    .ExecuteNonQuery()

                    Dim dataTemp As New DataTable

                    If cn.State = ConnectionState.Closed Then cn.Open()
                    cm = New SqlCommand
                    With cm
                        .Connection = cn
                        .Transaction = ct
                        .CommandText = "SELECT Mat_Status FROM " & DB & ".dbo.mtipe " &
                                        "INNER JOIN " & DB & ".dbo.mmca on type_materialtype=mat_tipe " &
                                        "WHERE type_partnumber='" & GridSales.Rows(i).Cells(1).Value & "'"
                    End With

                    da = New SqlDataAdapter
                    With da
                        .SelectCommand = cm
                        .Fill(dataTemp)
                    End With

                    statusItem = dataTemp.Rows(0).Item(0)

                    'Call NewTransactionStock(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") _
                    '                         , mTransIdCode, "C", mCustomer, Trim(lblInvoice.Text), mWh _
                    '                         , GridSales.Rows(i).Cells(1).Value, GridSales.Rows(i).Cells(4).Value, "-" _
                    '                         , GridSales.Rows(i).Cells(17).Value, IIf(statusItem = "G", 4, 6) _
                    '                         , GridSales.Rows(i).Cells(2).Value, GridSales.Rows(i).Cells(3).Value)

                    If cn.State = ConnectionState.Closed Then cn.Open()

                    With cm
                        .Connection = cn
                        .Transaction = ct
                        .CommandText = "INSERT INTO " & DB & ".dbo.hkstok " &
                                               " VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "'," &
                                               "'" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                               " '" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'" &
                                               ",'" & GridSales.Rows(i).Cells(1).Value & "','" & GridSales.Rows(i).Cells(4).Value & "'" &
                                               ",'" & mTransIdCode & "','-',0,'" & Trim(lblInvoice.Text) & "','C'" &
                                               ",'" & mCustomer & "','" & GridSales.Rows(i).Cells(17).Value & "','" & mWh & "'," &
                                               "'" & IIf(statusItem = "G", 4, 6) & "')"
                        .ExecuteNonQuery()


                    End With

                    If cn.State = ConnectionState.Closed Then cn.Open()

                    With cm
                        .Connection = cn
                        .Transaction = ct
                        If PartExitst(GridSales.Rows(i).Cells(1).Value, GetValueParamText("DEFAULT BRANCH"), mWh) = True Then

                            .CommandText = "UPDATE " & DB & ".dbo.mpart " &
                                            " SET part_consigmentstock=part_consigmentstock - " & GridSales.Rows(i).Cells(4).Value &
                                            ",part_rfsstock=part_rfsstock - " & GridSales.Rows(i).Cells(4).Value &
                                            ",part_description='" & Replace(GridSales.Rows(i).Cells(2).Value, "'", "''") & "'" &
                                            " WHERE part_partnumber='" & GridSales.Rows(i).Cells(1).Value & "'" &
                                            " AND Part_Branch='" & GetValueParamText("DEFAULT BRANCH") & "'" &
                                            " AND Part_WH = '" & mWh & "'"

                        Else

                            .CommandText = "INSERT INTO " & DB & ".dbo.mpart " &
                                            " VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "'," &
                                            "'" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                            "'" & GridSales.Rows(i).Cells(1).Value & "'," &
                                            "'" & GridSales.Rows(i).Cells(17).Value & "'," &
                                            "'" & GetValueParamText("DEFAULT BRANCH") & "','" & mWh & "'," &
                                            "'" & Replace(GridSales.Rows(i).Cells(2).Value, "'", "''") & "'," &
                                            "'" & GridSales.Rows(i).Cells(3).Value & "',0,0,0,0,0,0,0,'" & 0 - GridSales.Rows(i).Cells(4).Value & "',0,0,0,0,0,0,0,0," &
                                            "'" & 0 - GridSales.Rows(i).Cells(4).Value & "',0,0,0,0,0,0,'','',0,0)"


                        End If

                        .ExecuteNonQuery()

                    End With



                    totalAmount += Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value
                    totalppn += Math.Round(CDec(detailppn), 2)

                    detailamount = 0
                Next

            End With

            'Save Header POS

            If mAfterDiscount <> totalAmount Or mTotalPajak <> totalppn Then
                mTotalAmount = totalAmount + totalppn
            End If

            If mrounding = 0 Then

                mrounding = Math.Round(mTotalAmount, 0) - mTotalAmount
            End If

            If cn.State = ConnectionState.Closed Then cn.Open()

            query = "INSERT INTO " & DB & ".dbo.tslsh (hs_company,hs_branch,hs_salesorg,hs_salesoffice,hs_taxorg," &
                                "hs_invoice,hs_invoicedate,hs_deliverydate,hs_warehouse,hs_flag_kanvas,hs_customer,hs_qq," &
                                "hs_payer,hs_salesman,hs_trnid,hs_paytype,hs_currency,hs_top,hs_terms_of_payment,hs_due_date," &
                                "hs_due_date_delivery,hs_product,hs_note,hs_projectid,hs_grossamount,hs_disc3_afteramt," &
                                "hs_disc4_afteramt,hs_total_disc1,hs_total_disc2,hs_total_disc3,hs_disc4amt,hs_pct_disc_4," &
                                "hs_disc5,hs_nsudah_disc,hs_pct_ppn,hs_ppn,hs_pph,hs_ppnbm,hs_totalamount,hs_dppayamount," &
                                "hs_payamount,hs_spbno,hs_flag_gateway,hs_flag_spb,hs_flag_posting,hs_flag_boleh_prn," &
                                "hs_flag_pra_posting,hs_nomor_kanvas,hs_taxno,hs_delivery,hs_journalcode,hs_costcenter," &
                                "hs_counter_print,hs_nomor_vouch,hs_counter_dth,hs_item,hs_reffnumber,hs_createuser," &
                                "hs_createdate,hs_createtime,hs_ledgerno,hs_exchrate,hs_fiscalrate,hs_roundingamt,hs_vatno,hs_employeeid)" &
                                "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                "'" & mSlsOrg & "','" & mSalesOffice & "','01','" & Trim(lblInvoice.Text) & "'," &
                                "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'," &
                                "'" & Default_WH & "','N','" & mCustomer & "','" & mcqq & "','" & mCustomer & "'," &
                                "'" & mSalesman & "','" & mTransIdCode & "','T','IDR','T0',0," &
                                "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'," &
                                "'','" & mMemberCode & "','" & GetComputerName() & "','" & mGrossAmount & "','" & mAfterDiscount & "','" & mAfterDiscount & "',0,0,0,0,0,0," &
                                "'" & mAfterDiscount & "','" & mPPN & "','" & Math.Round(CDec(mTotalPajak), 2) & "',0,0,'" & mTotalAmount & "'," &
                                "0,'" & mTotalAmount & "','','N','Y','N','Y','N','" & IIf(mMemberCode <> "", mMemberCode, "") & "',0,'','300','" & mCostCenter & "',1,'',0,'" & GridSales.RowCount & "',0,'" & logOn & "'," &
                                "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(Now(), "HHmmss") & "'," &
                                "'',1,1,'" & mrounding & "','','" & mEmployeeID & "')"
            cm = New SqlCommand
            With cm
                .Connection = cn
                .Transaction = ct
                .CommandText = query
                .ExecuteNonQuery()
            End With

            'Update POS

            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .Transaction = ct
                .CommandText = "UPDATE " & DB & ".dbo.hdoc SET Pos_Completed=9" &
                                " WHERE Pos_Document='" & Trim(lblInvoice.Text) & "' AND Pos_TransDoc='" & mTransId & "'"
                .ExecuteNonQuery()
            End With

            'Save Header Payment

            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "INSERT INTO " & DB & ".dbo.tpayrech (Company,Branch,Receiptno,DocumentDate,Note,Currency," &
                            "SalesOrderNo,SalesAmount,Charges,CashAmount,CardAmount,ReturnAmount,PayAmount,ValidFlag,CloseFlag," &
                            "ReffDocument,CreateUser,CreateDate,CloseUser,EmployeeID,RoundingAmount) " &
                            "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & GetValueParamText("DEFAULT BRANCH") & "'," &
                            "'" & documentno & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," &
                            "'','IDR','" & lblInvoice.Text & "','" & CDec(fPayment.lblSubTotal.Text) - CDec(fPayment.txtCharge.Text) & "'," &
                            "'" & CDec(fPayment.txtCharge.Text) & "','" & CDec(fPayment.txtCashAmount.Text) & "','" & CDec(fPayment.txtCardAmount.Text) + CDec(fPayment.txtVoucherAmount.Text) & "'," &
                            "'" & CDec(fPayment.lblChange.Text) & "','" & CDec(fPayment.lblPaid.Text) & "'," &
                            "'N','N','','" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd " & GetTimeNow()) & "'," &
                            "'','" & mEmployeeID & "','" & CDec(mrounding) & "')"

            cm = New SqlCommand
            With cm
                .Connection = cn
                .Transaction = ct
                .CommandText = query
                .ExecuteNonQuery()
            End With

            If fPayment.tableVoucher.Rows.Count > 0 Then
                If fPayment.voucherState = 1 Then 'lock voucher code
                    If fPayment.tableVoucher.Rows.Count > 0 Then
                        For i As Integer = 0 To fPayment.tableVoucher.Rows.Count - 1
                            If cn.State = ConnectionState.Closed Then cn.Open()

                            query = "UPDATE " & DB & ".dbo.mvoucherd SET invoice='" & Trim(lblInvoice.Text) & "'," &
                                                    "used_at='" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," &
                                                    "employeeid='" & mEmployeeID & "' " &
                                                    "WHERE voucherid='" & fPayment.tableVoucher.Rows(i).Item(0) & "' " &
                                                    "AND vouchercode='" & fPayment.tableVoucher.Rows(i).Item(1) & "'"

                            cm = New SqlCommand
                            With cm
                                .Connection = cn
                                .Transaction = ct
                                .CommandText = query
                                .ExecuteNonQuery()
                            End With
                        Next
                    End If
                End If
            End If

            'Save Detail Payment

            seqnum = 0

            If CDec(fPayment.txtCharge.Text) = 0 Then
                mchargerate = 0
            Else
                mchargerate = (CDec(fPayment.txtCharge.Text) * 100) / CDec(fPayment.txtCardAmount.Text)
            End If


            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn

                If fPayment.voucherState = 1 Then

                    voucherAmt = CDec(fPayment.txtVoucherAmount.Text)

                    seqnum += 1
                    query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
                                               "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
                                               " VALUES ('" & documentno & "','" & seqnum & "','04','" & voucherAmt & "'," &
                                               "'" & voucherAmt & "','" & GetValueParamText("VOUCHER CARDTYPE") & "'" &
                                               ",'VOUCHER','" & GetValueParamText("VOUCHER EDC") & "','','',0,0,2,0)"

                    .Transaction = ct
                    .CommandText = query
                    .ExecuteNonQuery()

                End If


                If cn.State = ConnectionState.Closed Then cn.Open()
                If fPayment.cashState = 1 Then

                    seqnum += 1
                    query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
                                                  "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
                                                  " VALUES ('" & documentno & "','" & seqnum & "','01','" & CDec(fPayment.txtCashAmount.Text) & "'," &
                                                  "'" & CDec(fPayment.txtCashAmount.Text) & "','','','','','',0,0,1," &
                                                  "'" & CDec(fPayment.lblChange.Text) & "' )"
                    .Transaction = ct
                    .CommandText = query
                    .ExecuteNonQuery()
                End If

                If cn.State = ConnectionState.Closed Then cn.Open()

                If fPayment.cardState = 1 Then


                    seqnum += 1

                    query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
                                                  "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
                                                  " VALUES ('" & documentno & "','" & seqnum & "','02','" & CDec(fPayment.txtCardAmount.Text) & "'," &
                                                  "'" & CDec(fPayment.txtCardAmount.Text) + CDec(fPayment.txtCharge.Text) & "','" & fPayment.cmbCardType.SelectedValue & "'," &
                                                  "'" & Trim(fPayment.txtCardNo.Text) & "','" & fPayment.cmbEDC.SelectedValue & "'," &
                                                  "'" & Trim(fPayment.txtApproval.Text) & "','" & Trim(fPayment.txtCardName.Text) & "'," &
                                                  "'" & CDec(fPayment.txtCharge.Text) & "','" & (CDec(fPayment.txtCharge.Text) / CDec(fPayment.txtCardAmount.Text)) * 100 & "'," &
                                                  "2,0)"
                    .Transaction = ct
                    .CommandText = query
                    .ExecuteNonQuery()
                End If


            End With


            'Update Payment

            If cn.State = ConnectionState.Closed Then cn.Open()
            cm = New SqlCommand
            With cm
                .Connection = cn
                .Transaction = ct
                .CommandText = "UPDATE " & DB & ".dbo.hdoc SET Pos_Completed=9" &
                                " WHERE Pos_Document='" & documentno & "' AND Pos_TransDoc='RC'"
                .ExecuteNonQuery()
            End With

            'AUTO POSTING

            If GetValueParamNumber("POSTING INVOICE") = 1 Then

                If cn.State = ConnectionState.Closed Then cn.Open()

                query = "EXECUTE " & DB & ".dbo.p_posting_sales_pos '" & GetValueParamText("DEFAULT COMPANY") & "'," &
                                        "'" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                        "'" & Trim(lblInvoice.Text) & "','SYSTEM','N'"

                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .Transaction = ct
                    .CommandText = query
                    .ExecuteNonQuery()
                End With
            End If

            ct.Commit()

        Catch ex As Exception
            ct.Rollback()
            Throw ex
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Sub txtItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItem.DoubleClick
        If frmListItem.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtItem.Text = frmListItem.GridListItem.SelectedCells(0).Value
            frmListItem.Close()
        End If
    End Sub

    Private Sub txtItem_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItem.GotFocus
        txtItem.SelectAll()
    End Sub

    Private Sub txtItem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then

            Try

                If Trim(txtItem.Text) = "" Then Exit Sub
                dataCust = New DataTable
                Cursor = Cursors.WaitCursor

                dataCust = GetDetailCust(mCustomer)

                If dataCust.Rows.Count = 0 Then
                    MsgBox("Customer not found", MsgBoxStyle.Exclamation, Title)
                    GoTo finish

                End If

                If dataCust.Rows.Count > 0 Then mcqq = dataCust.Rows(0).Item(2)


                If Not ItemExists(Trim(txtItem.Text)) = True Then
                    MsgBox("Item not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If

             
                If Not ItemAssignmentExists(Trim(txtItem.Text), mWh) = True Then
                    MsgBox("Item not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If


                If Not PriceExists(Trim(txtItem.Text), dataCust.Rows(0).Item(1)) = True Then
                    MsgBox("Price not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If

                If Not DiscRoleExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc role not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If

                If Not DiscPolicyExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc policy not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If

                If Not DiscRateExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc Rate not available!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish
                End If


                Call CheckDetailItem()

                If chckScan.Checked = True Then ' scan one by one


                    If CheckStockMinus(Trim(txtItem.Text), txtQty.Text) = True Then

                        MsgBox("Over Stock!!", MsgBoxStyle.Exclamation, Title)
                        GoTo finish

                    End If

                    If GridSales.Rows.Count > 0 Then
                        If IsExists(Trim(txtItem.Text)) Then
                            Calculate(Trim(txtItem.Text))
                            Total()
                        Else
                            seqnum = GetLastSeqnum()
                            GridSales.Rows.Add( _
                                New Object() {seqnum, Trim(txtItem.Text), dataItem.Rows(0).Item("type_description") _
                                            , dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text), _
                                            String.Format("{0:#,##0}", dataItem.Rows(0).Item("mp_nextprice")), _
                                            String.Format("{0:#,##0}", discount), _
                                            String.Format("{0:#,##0}", CInt(txtQty.Text) * CDec((dataItem.Rows(0).Item("mp_nextprice")) _
                                            - discount)), mdppAmt, IIf(Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0), _
                                              mppnAmt, disc1Rate, disc1Amt, disc2Rate, disc2Amt, disc3Rate, disc3Amt, product, promoid})
                            Total()
                        End If
                    Else
                        seqnum = GetLastSeqnum()
                        GridSales.Rows.Add( _
                                     New Object() {seqnum, Trim(txtItem.Text), dataItem.Rows(0).Item("type_description") _
                                            , dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text), _
                                            String.Format("{0:#,##0}", dataItem.Rows(0).Item("mp_nextprice")), _
                                            String.Format("{0:#,##0}", discount), _
                                            String.Format("{0:#,##0}", CInt(txtQty.Text) * CDec((dataItem.Rows(0).Item("mp_nextprice")) _
                                            - discount)), mdppAmt, IIf(Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0), _
                                              mppnAmt, disc1Rate, disc1Amt, disc2Rate, disc2Amt, disc3Rate, disc3Amt, product, promoid})
                        Total()
                    End If
                    lblEventNote.Text = ""

                    txtItem.Clear()
                    lblTotalQty.Text = TotalQty()

                Else
                    txtQty.Focus()
                End If

finish:

                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        End If
    End Sub

    Private Sub CheckDetailItem()

        Try
            Dim ExistsBestPrice As Boolean = False
            Dim DiscBestPrice As Decimal = 0
            Dim DetailBestPrice As New DataTable
            Dim totalItem As Integer = 0
            promoid = ""

            disc1Rate = 0
            disc2Rate = 0
            disc3Rate = 0
            disc1Amt = 0
            disc2Amt = 0
            disc3Amt = 0
            discount = 0

            dataItem = New DataTable

            dataItem = GetDetailItemPOS(Trim(txtItem.Text), dataCust.Rows(0).Item("Cust_Discgroup"), dataCust.Rows(0).Item("Cust_Pricegroup"))


            If Not dataItem.Rows.Count > 0 Then
                MsgBox("No data for this item!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If dataItem.Rows(0).Item("type_Status") = 1 Then
                MsgBox("Item is blocked!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If Not dataItem.Rows(0).Item("Disc_Role") = 1 Then
                GoTo finish
            End If

            If dataItem.Rows(0).Item("mp_nextprice") <= GetValueParamMoney("PRICE NO DISCOUNT") Then
                GoTo finish
            End If

            'CHECK BEST PRICE EVENT

            If GetValueParamNumber("BEST PRICE") = 0 Then 'PARAMETER BEST PRICE ACTIVE OR NOT
                GoTo OtherPromo
            End If

            'Must BOOKS
            If Trim(dataItem.Rows(0).Item("product_group")) <> GetValueParamText("PRODUCT BEST PRICE") Then
                GoTo OtherPromo
            End If

            'Not MAGAZINE & OBRAL
            If Trim(dataItem.Rows(0).Item("type_product")) = "120" Or Trim(dataItem.Rows(0).Item("type_product")) = "109" Then
                GoTo OtherPromo
            End If

            If Not dataItem.Rows(0).Item("Param_D3") = "Y" Then
                GoTo otherpromo
            End If

            If Not BestPriceExists() = True Then
                ExistsBestPrice = False
                DiscBestPrice = 0
                GoTo OtherPromo
            Else
                ExistsBestPrice = True
            End If

            If ExistsBestPrice = True Then
                'CHECK ITEM BEST PRICE EXISTS
                tbleventBestPrice = New DataTable
                tbleventBestPrice = GetEventBestPrice(mSalesOffice, Trim(txtItem.Text), dataItem.Rows(0).Item("type_product") _
                              , GetValueParamDate("SYSTEM DATE"), dataItem.Rows(0).Item("type_discgroup") _
                              , dataItem.Rows(0).Item("type_prodhier1"), dataItem.Rows(0).Item("type_prodhier2") _
                              , dataItem.Rows(0).Item("type_prodhier3"), dataItem.Rows(0).Item("type_prodhier4") _
                              , dataItem.Rows(0).Item("type_prodhier5"))

                'GET DISC BEST PRICE
                If tbleventBestPrice.Rows.Count > 0 Then
                    DiscBestPrice = tbleventBestPrice.Rows(0).Item(5)
                Else
                    'BEST PRICE OVERLOAD MAX ITEM
                    totalItem = BestPriceOverload()
                    DetailBestPrice = BestPriceDetail()
                    If totalItem >= DetailBestPrice.Rows(0).Item(2) Then
                        ExistsBestPrice = False
                        DiscBestPrice = 0
                        GoTo otherpromo
                    End If

                    DiscBestPrice = GetDiscBestPrice(dataItem.Rows(0).Item("DiscPurch"), dataItem.Rows(0).Item("Disc1_Rate"))

                    'best price < 20% goto other promo
                    If DiscBestPrice < DetailBestPrice.Rows(0).Item(0) Then
                        GoTo otherpromo
                    End If

                    'cek event promo vs best price
                    tblevent = New DataTable

                    tblevent = GetPromo(mSalesOffice, Trim(txtItem.Text), dataItem.Rows(0).Item("type_product") _
                              , GetValueParamDate("SYSTEM DATE"), dataItem.Rows(0).Item("type_discgroup") _
                              , dataItem.Rows(0).Item("type_prodhier1"), dataItem.Rows(0).Item("type_prodhier2") _
                              , dataItem.Rows(0).Item("type_prodhier3"), dataItem.Rows(0).Item("type_prodhier4") _
                              , dataItem.Rows(0).Item("type_prodhier5"))

                    If tblevent.Rows.Count > 0 Then
                        If tblevent.Rows(0).Item(2) = 20 Then 'if final discount
                            If tblevent.Rows(0).Item(5) > DiscBestPrice Then
                                ExistsBestPrice = False
                                DiscBestPrice = 0
                                GoTo OtherPromo
                            Else
                                GoTo bestprice
                            End If
                        Else
                            GoTo bestprice
                        End If
                    End If

bestprice:
                    InsertDetailBestPrice(Trim(txtItem.Text), DiscBestPrice, Trim(mEmployeeID))

                End If

                disc3Rate = DiscBestPrice
                disc3Amt = dataItem.Rows(0).Item("mp_nextprice") * DiscBestPrice / 100

                discount = disc3Amt
                lblEventNote.Text = "BEST PRICE"
                promoid = "BEST PRICE"
                Dim bestPriceState As Integer = 0
                GoTo finish

            End If

otherpromo:
            'cek event promo
            tblevent = New DataTable

            tblevent = GetPromo(mSalesOffice, Trim(txtItem.Text), dataItem.Rows(0).Item("type_product") _
                                , GetValueParamDate("SYSTEM DATE"), dataItem.Rows(0).Item("type_discgroup") _
                                , dataItem.Rows(0).Item("type_prodhier1"), dataItem.Rows(0).Item("type_prodhier2") _
                                , dataItem.Rows(0).Item("type_prodhier3"), dataItem.Rows(0).Item("type_prodhier4") _
                                , dataItem.Rows(0).Item("type_prodhier5"))

            If tblevent.Rows.Count > 0 Then
                If tblevent.Rows(0).Item(2) = 1 Then 'additional promo type
                    GoTo additionaldiscount
                ElseIf tblevent.Rows(0).Item(2) = 20 Then 'final promo type
                    GoTo finaldiscount
                End If
            Else
                GoTo discnormal
            End If


additionaldiscount:
            Dim mprice As Decimal = 0

            If Not dataItem.Rows(0).Item("Param_D2") = "Y" Then
                GoTo discnormal
            End If

            disc1Rate = dataItem.Rows(0).Item("Disc1_Rate")
            disc1Amt = (dataItem.Rows(0).Item("mp_nextprice") * dataItem.Rows(0).Item("Disc1_Rate") / 100)
            mprice = dataItem.Rows(0).Item("mp_nextprice") - disc1Amt
            disc2Rate = tblevent.Rows(0).Item(5)
            disc2Amt = mprice * disc2Rate / 100
            discount = disc1Amt + disc2Amt
            promoid = Trim(tblevent.Rows(0).Item(0))
            GoTo finish

finaldiscount:

            If Not dataItem.Rows(0).Item("Param_D3") = "Y" Then
                GoTo discnormal
            End If

            disc3Rate = tblevent.Rows(0).Item(5)
            disc3Amt = dataItem.Rows(0).Item("mp_nextprice") * disc3Rate / 100

            discount = disc3Amt

            lblEventNote.Text = Trim(tblevent.Rows(0).Item(1))
            promoid = Trim(tblevent.Rows(0).Item(0))
            GoTo finish

discnormal:
            If Not dataItem.Rows(0).Item("Param_D1") = "Y" Then
                GoTo finish
            End If

            disc1Rate = dataItem.Rows(0).Item("Disc1_Rate")
            disc1Amt = (dataItem.Rows(0).Item("mp_nextprice") * dataItem.Rows(0).Item("Disc1_Rate") / 100)
            discount = disc1Amt

            lblEventNote.Text = ""
            GoTo finish
finish:
            Dim amountAfterDiscount As Decimal = 0
            If Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01" Then
                mdppAmt = dataItem.Rows(0).Item("mp_currentprice")

                amountAfterDiscount = mdppAmt - (mdppAmt * disc1Rate / 100)

                If disc2Rate > 0 Then 'Disc2
                    amountAfterDiscount = amountAfterDiscount - (amountAfterDiscount * disc2Rate / 100)
                End If

                If disc3Rate > 0 Then 'Disc3
                    amountAfterDiscount = 0
                    amountAfterDiscount = mdppAmt - (mdppAmt * disc3Rate / 100)
                End If

                mppnAmt = amountAfterDiscount * GetValueParamMoney("DEFAULT PPN") / 100
            Else

                mdppAmt = dataItem.Rows(0).Item("mp_currentprice")
                'mdppAmt = dataItem.Rows(0).Item("mp_nextprice") _
                '        - dataItem.Rows(0).Item("mp_nextprice") * dataItem.Rows(0).Item("Disc1_Rate") / 100
                mppnAmt = 0
            End If

            'item product code
            product = dataItem.Rows(0).Item("type_product")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ReCalculateDiscItem(partnumber As String, discMember As Decimal)
        Dim ExistsBestPrice As Boolean = False
        Dim DiscBestPrice As Decimal = 0

        Try
            disc1Rate = 0
            disc2Rate = 0
            disc3Rate = 0
            disc1Amt = 0
            disc2Amt = 0
            disc3Amt = 0
            discount = 0

            dataItem = New DataTable

            dataItem = GetDetailItemPOS(Trim(partnumber), dataCust.Rows(0).Item("Cust_Discgroup"), dataCust.Rows(0).Item("Cust_Pricegroup"))

            If Not dataItem.Rows.Count > 0 Then
                MsgBox("No data for this item!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If dataItem.Rows(0).Item("type_Status") = 1 Then
                MsgBox("Item is blocked!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            End If

            If Not dataItem.Rows(0).Item("Disc_Role") = 1 Then
                GoTo finalDiscMember
            End If

            'If dataItem.Rows(0).Item("mp_nextprice") <= GetValueParamMoney("PRICE NO DISCOUNT") Then
            '    GoTo finish
            'End If

            'cek event promo
            tblevent = New DataTable

            tblevent = GetPromo(mSalesOffice, Trim(txtItem.Text), dataItem.Rows(0).Item("type_product") _
                              , GetValueParamDate("SYSTEM DATE"), dataItem.Rows(0).Item("type_discgroup") _
                              , dataItem.Rows(0).Item("type_prodhier1"), dataItem.Rows(0).Item("type_prodhier2") _
                              , dataItem.Rows(0).Item("type_prodhier3"), dataItem.Rows(0).Item("type_prodhier4") _
                              , dataItem.Rows(0).Item("type_prodhier5"))



            If tblevent.Rows.Count > 0 Then

                'Priority 1 BEST PRICE
                'Check BEST PRICE EXISTS

                If Not dataItem.Rows(0).Item("Param_D3") = "Y" Then
                    GoTo finalDiscMember
                End If

                If GetValueParamNumber("BEST PRICE") = 0 Then 'PARAMETER BEST PRICE ACTIVE OR NOT
                    GoTo finalDiscMember
                End If

                'Must BOOKS
                If Trim(dataItem.Rows(0).Item("product_group")) <> GetValueParamText("PRODUCT BEST PRICE") Then
                    GoTo finalDiscMember
                End If

                'Not MAGAZINE & OBRAL
                If Trim(dataItem.Rows(0).Item("type_product")) = "120" Or Trim(dataItem.Rows(0).Item("type_product")) = "109" Then
                    GoTo finalDiscMember
                End If

                'BEST PRICE EXISTS
                If BestPriceExists() = True Then
                    ExistsBestPrice = True
                    DiscBestPrice = GetDiscBestPrice(dataItem.Rows(0).Item("DiscPurch"), dataItem.Rows(0).Item("Disc1_Rate"))
                Else
                    ExistsBestPrice = False
                    DiscBestPrice = 0
                    GoTo finalDiscMember
                End If

                'best price < 20% goto other promo
                If DiscBestPrice < GetValueParamNumber("MIN MARGIN") Then
                    GoTo finalDiscMember
                End If

            End If

finalDiscMember:

            'Final Discount Member
                If discMember >= DiscBestPrice Then
                    disc3Rate = discMember
                Else
                    disc3Rate = DiscBestPrice
                End If

                disc3Amt = dataItem.Rows(0).Item("mp_nextprice") * disc3Rate / 100

                discount = disc3Amt

finish:
            Dim amountAfterDiscount As Decimal = 0
            If Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01" Then
                mdppAmt = dataItem.Rows(0).Item("mp_currentprice")

                amountAfterDiscount = mdppAmt - (mdppAmt * disc1Rate / 100)

                If disc2Rate > 0 Then 'Disc2
                    amountAfterDiscount = amountAfterDiscount - (amountAfterDiscount * disc2Rate / 100)
                End If

                If disc3Rate > 0 Then 'Disc3
                    amountAfterDiscount = 0
                    amountAfterDiscount = mdppAmt - (mdppAmt * disc3Rate / 100)
                End If

                mppnAmt = amountAfterDiscount * GetValueParamMoney("DEFAULT PPN") / 100
            Else

                mdppAmt = dataItem.Rows(0).Item("mp_currentprice")
                'mdppAmt = dataItem.Rows(0).Item("mp_nextprice") _
                '        - dataItem.Rows(0).Item("mp_nextprice") * dataItem.Rows(0).Item("Disc1_Rate") / 100
                mppnAmt = 0
            End If

            'item product code
            product = dataItem.Rows(0).Item("type_product")

            GridSales.Rows.Add( _
                        New Object() {GetLastSeqnum(), Trim(partnumber), dataItem.Rows(0).Item("type_description") _
                                            , dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text), _
                                            String.Format("{0:#,##0}", dataItem.Rows(0).Item("mp_nextprice")), _
                                            String.Format("{0:#,##0}", discount), _
                                            String.Format("{0:#,##0}", CInt(txtQty.Text) * CDec((dataItem.Rows(0).Item("mp_nextprice")) _
                                            - discount)), mdppAmt, IIf(Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0), _
                                              mppnAmt, disc1Rate, disc1Amt, disc2Rate, disc2Amt, disc3Rate, disc3Amt, product})
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function GetDetailCust(ByVal kode As String) As DataTable
        data = New DataTable
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            query = "SELECT mcust.Cust_DiscGroup,mcust.Cust_Pricegroup,mcqq.Cust_qq from " & DB & ".dbo.mcust " & _
                    "INNER JOIN " & DB & ".dbo.mcqq ON mcust.Cust_Kode=mcqq.Cust_Kode WHERE mcust.Cust_Kode='" & kode & "'"
            With cm
                .Connection = cn
                .CommandText = query
            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With
            cn.Close()
            query = ""

        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try

        Return data
    End Function

    Private Sub Calculate(ByVal kode As String)
        Try
            For i As Integer = 0 To GridSales.Rows.Count - 1
                If GridSales.Rows(i).Cells(1).Value = kode Then

                    GridSales.Rows(i).Cells(4).Value += CInt(txtQty.Text)
                    GridSales.Rows(i).Cells(7).Value = String.Format("{0:#,##0}", GridSales.Rows(i).Cells(4).Value * _
                                    (GridSales.Rows(i).Cells(5).Value - GridSales.Rows(i).Cells(6).Value))
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Total()
        If GridSales.RowCount > 0 Then
            jml = 0
            For i As Integer = 0 To GridSales.RowCount - 1
                jml = jml + CDec(GridSales.Rows(i).Cells(7).Value)
            Next

        Else
            jml = 0
        End If

        lblTotal.Text = String.Format("{0:#,##0}", jml)
    End Sub

    Private Function IsExists(ByVal kode As String) As Boolean
        Try
            For i As Integer = 0 To GridSales.Rows.Count - 1
                If GridSales.Rows(i).Cells(1).Value = kode Then
                    Return True
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtQty_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Click
        txtQty.SelectAll()
    End Sub

    Private Sub txtQty_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.GotFocus
        txtQty.SelectAll()
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then
            Try
                Cursor = Cursors.WaitCursor
                If Trim(txtItem.Text) = "" Then txtItem.Focus()

                dataCust = New DataTable

                dataCust = GetDetailCust(mCustomer)

                If dataCust.Rows.Count > 0 Then mcqq = dataCust.Rows(0).Item(2)

                If Not ItemExists(Trim(txtItem.Text)) = True Then
                    MsgBox("Item not available!", MsgBoxStyle.Exclamation, Title)
                    txtQty.Text = 1
                    GoTo finish
                End If

                If Not PriceExists(Trim(txtItem.Text), dataCust.Rows(0).Item(1)) = True Then
                    MsgBox("Price not available!", MsgBoxStyle.Exclamation, Title)
                    txtQty.Text = 1
                    GoTo finish
                End If


                If Not DiscRoleExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc role not available!", MsgBoxStyle.Exclamation, Title)
                    txtQty.Text = 1
                    GoTo finish
                End If

                If Not DiscPolicyExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc Policy not available!", MsgBoxStyle.Exclamation, Title)
                    txtQty.Text = 1
                    GoTo finish
                End If

                If Not DiscRateExists(Trim(txtItem.Text), dataCust.Rows(0).Item(0)) = True Then
                    MsgBox("Disc Rate not available!", MsgBoxStyle.Exclamation, Title)
                    txtQty.Text = 1
                    GoTo finish
                End If


                Call CheckDetailItem()

                If CheckStockMinus(Trim(txtItem.Text), txtQty.Text) = True Then

                    MsgBox("Over Stock!!", MsgBoxStyle.Exclamation, Title)
                    GoTo finish

                End If

                If GridSales.Rows.Count > 0 Then
                    If IsExists(Trim(txtItem.Text)) Then
                        Calculate(Trim(txtItem.Text))
                        Total()
                    Else
                        seqnum = GetLastSeqnum()
                        GridSales.Rows.Add( _
                                        New Object() {seqnum, Trim(txtItem.Text), dataItem.Rows(0).Item("type_description") _
                                            , dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text), _
                                            String.Format("{0:#,##0}", dataItem.Rows(0).Item("mp_nextprice")), _
                                            String.Format("{0:#,##0}", discount), _
                                            String.Format("{0:#,##0}", CInt(txtQty.Text) * CDec((dataItem.Rows(0).Item("mp_nextprice")) _
                                            - discount)), mdppAmt, IIf(Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0), _
                                              mppnAmt, disc1Rate, disc1Amt, disc2Rate, disc2Amt, disc3Rate, disc3Amt, product, promoid})
                        Total()

                    End If
                Else
                    seqnum = GetLastSeqnum()
                    GridSales.Rows.Add( _
                                     New Object() {seqnum, Trim(txtItem.Text), dataItem.Rows(0).Item("type_description") _
                                            , dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text), _
                                            String.Format("{0:#,##0}", dataItem.Rows(0).Item("mp_nextprice")), _
                                            String.Format("{0:#,##0}", discount), _
                                            String.Format("{0:#,##0}", CInt(txtQty.Text) * CDec((dataItem.Rows(0).Item("mp_nextprice")) _
                                            - discount)), mdppAmt, IIf(Trim(dataItem.Rows(0).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0), _
                                              mppnAmt, disc1Rate, disc1Amt, disc2Rate, disc2Amt, disc3Rate, disc3Amt, product, promoid})
                    Total()

                End If
                txtQty.Text = 1
                txtItem.Clear()
                lblEventNote.Text = ""
                txtItem.Focus()

                lblTotalQty.Text = TotalQty()

finish:
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        End If
    End Sub

    Private Function GetLastSeqnum() As Integer
        Dim iden As Integer = 0
        If GridSales.Rows.Count > 0 Then
            For i As Integer = 0 To GridSales.Rows.Count - 1
                iden = GridSales.Rows(i).Cells(0).Value
            Next

            iden = iden + 1
        Else
            iden = 1
        End If

        Return iden
    End Function

    Private Sub DetailClear()
        If state = 1 Then 'new transaction

            seqnum = 0

            lblInvoice.Text = GetLastTransNo(mTransId)
            documentno = GetLastTransNo("RC")
            GridSales.Enabled = True


            txtItem.Clear()
            txtQty.Text = 1
            'cmbCustomer.Enabled = True
            chckScan.Enabled = True

            btnClose.Text = "Cancel"

            btnClose.Image = mainClass.imgList.ImgBtnCancel

            lblTotalQty.Text = 0

            btnNew.Enabled = False

            btnVoid.Enabled = False
            btnPayment.Enabled = True
            btnBrowse.Enabled = False
            btnClose.Enabled = True
            If chckScan.Checked = True Then
                txtQty.Enabled = False
            Else
                txtQty.Enabled = True
            End If
            txtItem.Enabled = True

            txtItem.Focus()
        ElseIf state = 2 Then 'browse

        ElseIf state = 3 Then 'Browse
            GridSales.Enabled = True

            btnNew.Enabled = False
            btnPayment.Enabled = False
            btnBrowse.Enabled = True
            btnPrint.Enabled = True


            btnClose.Text = "Cancel"
            btnClose.Enabled = True
       
            btnClose.Image = mainClass.imgList.ImgBtnCancel


            btnVoid.Enabled = True
            'lblKasir.Text = ""
            lblInvoice.Text = browseInvoice
            chckScan.Enabled = False

            txtItem.Enabled = False
            txtQty.Enabled = False

            btnPrint.Focus()
        Else
            GridSales.Enabled = False

            GridSales.Rows.Clear()

            btnClose.Text = "Close"


            btnClose.Image = mainClass.imgList.ImgBtnClosing

            lblEventNote.Text = ""
            btnNew.Enabled = True

            btnPayment.Enabled = False
            btnPrint.Enabled = False
            btnBrowse.Enabled = True
            'cmbCustomer.Enabled = True
            'cmbSalesOrg.Enabled = True
            lblTotal.Text = 0
            seqnum = 0
            btnVoid.Enabled = False
            'lblKasir.Text = ""
            lblInvoice.Text = "-"
            chckScan.Enabled = False
            btnClose.Enabled = True
            txtItem.Enabled = False
            txtQty.Enabled = False
            lblTotalQty.Text = 0
            If backupPOSItem.Rows.Count > 0 Then backupPOSItem.Rows.Clear()
            mMemberCode = ""
            mMemberDisc = 0
            mMemberMinPayment = 0
            lblMember.Text = "-"

            btnCancelMember.Visible = False
            btnNew.Focus()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            state = 1
            DetailClear()

            
        Catch ex As Exception
            state = 0
            DetailClear()
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click

        Payment()

    End Sub

    'Private Sub SaveDetailPOS(ByRef mAmount As Decimal, ByRef mPPN As Decimal, ByVal receipt As String)
    '    Try
    '        Dim discPercItem As Decimal = 0
    '        Dim statusItem As String = ""
    '        Dim detailamount As Decimal = 0
    '        Dim detailppn As Decimal = 0

    '        Dim totalAmount As Decimal = 0
    '        Dim totalppn As Decimal = 0

    '        If cn.State = ConnectionState.Closed Then cn.Open()

    '        With cm
    '            .Connection = cn

    '            For i As Integer = 0 To GridSales.Rows.Count - 1


    '                detailamount += GridSales.Rows(i).Cells(8).Value - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(11).Value / 100)

    '                If GridSales.Rows(i).Cells(13).Value > 0 Then
    '                    detailamount += detailamount - (CDec(detailamount) * GridSales.Rows(i).Cells(13).Value / 100)
    '                End If

    '                If GridSales.Rows(i).Cells(15).Value > 0 Then
    '                    detailamount = 0
    '                    detailamount += GridSales.Rows(i).Cells(8).Value - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(15).Value / 100)
    '                End If

    '                discPercItem = (GridSales.Rows(i).Cells(6).Value * 100) / GridSales.Rows(i).Cells(5).Value
    '                detailppn = Math.Round(CDec(GridSales.Rows(i).Cells(10).Value), 2) * GridSales.Rows(i).Cells(4).Value

    '                query = "INSERT INTO " & DB & ".dbo.tslsd (ds_invoice,ds_trntype,ds_partnumber,ds_orderdate,ds_invoicedate," &
    '                                "ds_product,ds_batchno,ds_dn,ds_orderno,ds_price,ds_pricedisp,ds_qty,ds_uom,ds_uomunit," &
    '                                "ds_disc1,ds_disc2,ds_disc3,ds_amount,ds_cogs,ds_ordered_qty,ds_pct_D4,ds_disc4,ds_disc5," &
    '                                "ds_dpp,ds_pctppn,ds_pctpph,ds_pctppnbm,ds_ppn,ds_pph,ds_ppnbm,ds_internalcost,ds_internalcost2," &
    '                                "ds_internalcost3,ds_internalcost4) VALUES (" &
    '                                "'" & Trim(lblInvoice.Text) & "','" & mTransId & "','" & GridSales.Rows(i).Cells(1).Value & "'," &
    '                                "'" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," &
    '                                "'" & GridSales.Rows(i).Cells(17).Value & "','" & GridSales.Rows(i).Cells(18).Value & "','','','" & Math.Round(CDec(GridSales.Rows(i).Cells(8).Value), 2) & "'," &
    '                                "'" & Math.Round(CDec(GridSales.Rows(i).Cells(5).Value), 2) & "','" & GridSales.Rows(i).Cells(4).Value & "'," &
    '                                "'" & GridSales.Rows(i).Cells(3).Value & "','1','" & Math.Round(CDec(GridSales.Rows(i).Cells(11).Value), 2) & "'," &
    '                                "'" & Math.Round(CDec(GridSales.Rows(i).Cells(13).Value), 2) & "','" & CDec(Math.Round(GridSales.Rows(i).Cells(15).Value, 2)) & "'," &
    '                                "'" & Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value & "'," &
    '                                "'0','" & GridSales.Rows(i).Cells(4).Value & "','0','0','0','" & Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value & "'," &
    '                                "'" & GridSales.Rows(i).Cells(9).Value & "','0','0','" & detailppn & "'," &
    '                                "'0','0','0','0','0','0')"
    '                .CommandTimeout = 1000
    '                .CommandText = query
    '                .ExecuteNonQuery()

    '                statusItem = GetStatusItem(GridSales.Rows(i).Cells(1).Value)

    '                Call NewTransactionStock(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") _
    '                                         , mTransIdCode, "C", mCustomer, Trim(lblInvoice.Text), mWh _
    '                                         , GridSales.Rows(i).Cells(1).Value, GridSales.Rows(i).Cells(4).Value, "-", GridSales.Rows(i).Cells(17).Value, IIf(statusItem = "G", 4, 6) _
    '                                         , GridSales.Rows(i).Cells(2).Value, GridSales.Rows(i).Cells(3).Value)

    '                totalAmount += Math.Round(CDec(detailamount), 2) * GridSales.Rows(i).Cells(4).Value
    '                totalppn += Math.Round(CDec(detailppn), 2)

    '                detailamount = 0
    '            Next
    '            cn.Close()
    '        End With

    '        mAmount = totalAmount
    '        mPPN = totalppn

    '    Catch ex As Exception

    '        cn.Close()
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub SaveHeaderPOS(ByVal mHeaderAmt As Decimal, ByVal mHeaderPPN As Decimal, receipt As String)
    '    Try
    '        Dim mtotaldisc1 As Decimal = TotalDisc1()
    '        Dim mtotaldisc2 As Decimal = TotalDisc2()
    '        Dim mtotaldisc3 As Decimal = TotalDisc3()
    '        Dim mTotalPajak As Decimal = Math.Round(TotalPajak(), 2)
    '        Dim mPPN As Decimal = IIf(mTotalPajak = 0, 0, Default_PPN)
    '        Dim mDPP As Decimal = Math.Round(TotalDPP(), 2)
    '        Dim mGrossAmount As Decimal = Math.Round(GrossAmount(), 2)
    '        Dim mAfterDiscount As Decimal = Math.Round(AfterDiscount(), 2)
    '        Dim mTotalAmount As Decimal = mAfterDiscount + Math.Round(CDec(mTotalPajak), 2)

    '        If mAfterDiscount <> mHeaderAmt Or mTotalPajak <> mHeaderPPN Then
    '            mTotalAmount = mHeaderAmt + mHeaderPPN
    '        End If

    '        If mrounding = 0 Then
    '            mrounding = Math.Round(mTotalAmount, 0) - mTotalAmount
    '        End If

    '        If cn.State = ConnectionState.Closed Then cn.Open()
    '        query = "INSERT INTO " & DB & ".dbo.tslsh (hs_company,hs_branch,hs_salesorg,hs_salesoffice,hs_taxorg," &
    '                            "hs_invoice,hs_invoicedate,hs_deliverydate,hs_warehouse,hs_flag_kanvas,hs_customer,hs_qq," &
    '                            "hs_payer,hs_salesman,hs_trnid,hs_paytype,hs_currency,hs_top,hs_terms_of_payment,hs_due_date," &
    '                            "hs_due_date_delivery,hs_product,hs_note,hs_projectid,hs_grossamount,hs_disc3_afteramt," &
    '                            "hs_disc4_afteramt,hs_total_disc1,hs_total_disc2,hs_total_disc3,hs_disc4amt,hs_pct_disc_4," &
    '                            "hs_disc5,hs_nsudah_disc,hs_pct_ppn,hs_ppn,hs_pph,hs_ppnbm,hs_totalamount,hs_dppayamount," &
    '                            "hs_payamount,hs_spbno,hs_flag_gateway,hs_flag_spb,hs_flag_posting,hs_flag_boleh_prn," &
    '                            "hs_flag_pra_posting,hs_nomor_kanvas,hs_taxno,hs_delivery,hs_journalcode,hs_costcenter," &
    '                            "hs_counter_print,hs_nomor_vouch,hs_counter_dth,hs_item,hs_reffnumber,hs_createuser," &
    '                            "hs_createdate,hs_createtime,hs_ledgerno,hs_exchrate,hs_fiscalrate,hs_roundingamt,hs_vatno,hs_employeeid)" &
    '                            "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & GetValueParamText("DEFAULT BRANCH") & "'," &
    '                            "'" & mSlsOrg & "','" & mSalesOffice & "','01','" & Trim(lblInvoice.Text) & "'," &
    '                            "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'," &
    '                            "'" & Default_WH & "','N','" & mCustomer & "','" & mcqq & "','" & mCustomer & "'," &
    '                            "'" & mSalesman & "','" & mTransIdCode & "','T','IDR','T0',0," &
    '                            "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "'," &
    '                            "'','" & mMemberCode & "','" & GetComputerName() & "','" & mGrossAmount & "','" & mAfterDiscount & "','" & mAfterDiscount & "',0,0,0,0,0,0," &
    '                            "'" & mAfterDiscount & "','" & mPPN & "','" & Math.Round(CDec(mTotalPajak), 2) & "',0,0,'" & mTotalAmount & "'," &
    '                            "0,'" & mTotalAmount & "','','N','Y','N','Y','N','" & IIf(mMemberCode <> "", mMemberCode, "") & "',0,'','" & GETPOSJournalCode(mTransIdCode) & "','" & mCostCenter & "',1,'',0,'" & GridSales.RowCount & "',0,'" & logOn & "'," &
    '                            "'" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") & "','" & Format(Now(), "HHmmss") & "'," &
    '                            "'',1,1,'" & mrounding & "','','" & mEmployeeID & "')"
    '        cm = New SqlCommand
    '        With cm
    '            .Connection = cn
    '            .CommandTimeout = 1000
    '            .CommandText = query
    '            .ExecuteNonQuery()
    '        End With

    '        cn.Close()
    '    Catch ex As Exception

    '        cn.Close()
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub SaveHeaderPayment(ByVal no As String)

    '    Try

    '        If cn.State = ConnectionState.Closed Then cn.Open()
    '        query = "INSERT INTO " & DB & ".dbo.tpayrech (Company,Branch,Receiptno,DocumentDate,Note,Currency," &
    '                        "SalesOrderNo,SalesAmount,Charges,CashAmount,CardAmount,ReturnAmount,PayAmount,ValidFlag,CloseFlag," &
    '                        "ReffDocument,CreateUser,CreateDate,CloseUser,EmployeeID,RoundingAmount) " &
    '                        "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & GetValueParamText("DEFAULT BRANCH") & "'," &
    '                        "'" & no & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "'," &
    '                        "'','IDR','" & lblInvoice.Text & "','" & CDec(fPayment.lblSubTotal.Text) - CDec(fPayment.txtCharge.Text) & "'," &
    '                        "'" & CDec(fPayment.txtCharge.Text) & "','" & CDec(fPayment.txtCashAmount.Text) & "','" & CDec(fPayment.txtCardAmount.Text) + CDec(fPayment.txtVoucherAmount.Text) & "'," &
    '                        "'" & CDec(fPayment.lblChange.Text) & "','" & CDec(fPayment.lblPaid.Text) & "'," &
    '                        "'N','N','','" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd " & GetTimeNow()) & "'," &
    '                        "'','" & mEmployeeID & "','" & CDec(mrounding) & "')"

    '        cm = New SqlCommand
    '        With cm
    '            .Connection = cn
    '            .CommandTimeout = 1000
    '            .CommandText = query
    '            .ExecuteNonQuery()
    '        End With

    '        If fPayment.tableVoucher.Rows.Count > 0 Then
    '            LockVoucherPayment()
    '        End If

    '        cn.Close()
    '    Catch ex As Exception
    '        cn.Close()
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub SaveDetailPayment(ByVal no As String)

    '    Dim mchargerate As Decimal = 0
    '    Dim counter As Integer = 0
    '    Dim voucherAmt As Decimal = 0

    '    seqnum = 0
    '    Try
    '        If CDec(fPayment.txtCharge.Text) = 0 Then
    '            mchargerate = 0
    '        Else
    '            mchargerate = (CDec(fPayment.txtCharge.Text) * 100) / CDec(fPayment.txtCardAmount.Text)
    '        End If


    '        If cn.State = ConnectionState.Closed Then cn.Open()

    '        cm = New SqlCommand
    '        With cm
    '            .Connection = cn

    '            If fPayment.voucherState = 1 Then

    '                'If fPayment.cashState = 1 Or fPayment.cardState = 1 Then
    '                voucherAmt = CDec(fPayment.txtVoucherAmount.Text)
    '                'Else
    '                '    If CDec(fPayment.lblSubTotal.Text) < CDec(fPayment.txtVoucherAmount.Text) Then
    '                '        voucherAmt = CDec(fPayment.lblSubTotal.Text)
    '                '    End If

    '                'End If

    '                seqnum += 1
    '                query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
    '                                           "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
    '                                           " VALUES ('" & no & "','" & seqnum & "','04','" & voucherAmt & "'," &
    '                                           "'" & voucherAmt & "','" & GetValueParamText("VOUCHER CARDTYPE") & "'" &
    '                                           ",'VOUCHER','" & GetValueParamText("VOUCHER EDC") & "','','',0,0,2,0)"

    '                .CommandTimeout = 0
    '                .CommandText = query
    '                .ExecuteNonQuery()

    '            End If


    '            If cn.State = ConnectionState.Closed Then cn.Open()
    '            If fPayment.cashState = 1 Then

    '                seqnum += 1
    '                query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
    '                                              "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
    '                                              " VALUES ('" & no & "','" & seqnum & "','01','" & CDec(fPayment.txtCashAmount.Text) & "'," &
    '                                              "'" & CDec(fPayment.txtCashAmount.Text) & "','','','','','',0,0,1," &
    '                                              "'" & CDec(fPayment.lblChange.Text) & "' )"
    '                .CommandTimeout = 0
    '                .CommandText = query
    '                .ExecuteNonQuery()
    '            End If

    '            If cn.State = ConnectionState.Closed Then cn.Open()

    '            If fPayment.cardState = 1 Then


    '                seqnum += 1

    '                query = "INSERT INTO " & DB & ".dbo.tpayrecd (receiptno,item,paytype,amount,fullamount,cardtype,cardno," &
    '                                              "edcid,approvalcode,cardname,bankchargeamt,bankchargepct,type,chargeamount)" &
    '                                              " VALUES ('" & no & "','" & seqnum & "','02','" & CDec(fPayment.txtCardAmount.Text) & "'," &
    '                                              "'" & CDec(fPayment.txtCardAmount.Text) + CDec(fPayment.txtCharge.Text) & "','" & fPayment.cmbCardType.SelectedValue & "'," &
    '                                              "'" & Trim(fPayment.txtCardNo.Text) & "','" & fPayment.cmbEDC.SelectedValue & "'," &
    '                                              "'" & Trim(fPayment.txtApproval.Text) & "','" & Trim(fPayment.txtCardName.Text) & "'," &
    '                                              "'" & CDec(fPayment.txtCharge.Text) & "','" & (CDec(fPayment.txtCharge.Text) / CDec(fPayment.txtCardAmount.Text)) * 100 & "'," &
    '                                              "2,0)"
    '                .CommandTimeout = 0
    '                .CommandText = query
    '                .ExecuteNonQuery()
    '            End If


    '        End With

    '        cn.Close()
    '    Catch ex As Exception

    '        cn.Close()
    '        Throw ex
    '    End Try


    'End Sub

    Private Function SubTotal() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (CDec(GridSales.Rows(i).Cells(5).Value) * CInt(GridSales.Rows(i).Cells(4).Value))
        Next

        Return total
    End Function

    Private Function GrossAmount() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (Math.Round(CDec(GridSales.Rows(i).Cells(8).Value), 2) * CInt(GridSales.Rows(i).Cells(4).Value))
        Next

        Return total
    End Function

    Private Function AfterDiscount() As Decimal
        Dim total As Decimal = 0
        Dim amount As Decimal = 0
        Dim discount1, discount2, discount3 As Decimal

        For i As Integer = 0 To GridSales.RowCount - 1
            amount = 0
            'disc1
            discount1 = Math.Round(CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(11).Value / 100, 2)
            amount = amount + ((Math.Round(CDec(GridSales.Rows(i).Cells(8).Value), 2) - discount1) * GridSales.Rows(i).Cells(4).Value)

            'disc2
            If GridSales.Rows(i).Cells(13).Value > 0 Then
                discount2 = Math.Round(CDec(amount) * GridSales.Rows(i).Cells(13).Value / 100, 2)
                amount = amount + ((Math.Round(CDec(amount), 2) - discount2) * CInt(GridSales.Rows(i).Cells(4).Value))

            End If

            'disc3
            If GridSales.Rows(i).Cells(15).Value > 0 Then
                amount = 0
                discount3 = Math.Round(CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(15).Value / 100, 2)
                amount = ((Math.Round(CDec(GridSales.Rows(i).Cells(8).Value), 2) - discount3) * CInt(GridSales.Rows(i).Cells(4).Value))
            End If

            total += amount

        Next

        Return total
    End Function

    Private Function TotalDisc1() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (CDec(GridSales.Rows(i).Cells(12).Value) * CDec(GridSales.Rows(i).Cells(4).Value))
        Next

        Return total
    End Function

    Private Function TotalDisc2() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (CDec(GridSales.Rows(i).Cells(14).Value) * CDec(GridSales.Rows(i).Cells(4).Value))
        Next

        Return total
    End Function

    Private Function TotalDisc3() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (CDec(GridSales.Rows(i).Cells(16).Value) * CDec(GridSales.Rows(i).Cells(4).Value))
        Next

        Return total
    End Function

    Private Function TotalPajak() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (Math.Round(CDec(GridSales.Rows(i).Cells(10).Value), 2) * GridSales.Rows(i).Cells(4).Value)
        Next

        Return total
    End Function

    Private Function TotalDPP() As Decimal
        Dim total As Decimal = 0
        For i As Integer = 0 To GridSales.RowCount - 1
            total = total + (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(4).Value)
        Next

        Return total
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            'Dim pass As String = ""

            'pass = InputBox("Please input password for RE-PRINT CASHIER?", Title)


            'If Trim(pass) = "" Then Exit Sub

            'If Trim(pass) <> GetValueParamText("PASS REPRINT") Then
            '    MsgBox("Password Wrong!", MsgBoxStyle.Information, Title)
            '    Exit Sub
            'End If

            printstate = 2
            Call PrintInvoice("----RE-PRINT TRANSACTION----")
          
            UpdatePrintNum(browseInvoice)
            state = 0
            DetailClear()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub UpdatePrintNum(ByVal doc As String)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            With cm
                .Connection = cn
                .CommandText = "UPDATE " & DB & ".dbo.tslsh SET hs_counter_print=hs_counter_print+1" & _
                                " WHERE hs_invoice='" & doc & "'"
                .ExecuteNonQuery()
            End With
            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub PrintInvoice(ByVal judul As String)
        Dim tinggi As Integer = 2
        Dim text As String = ""
        Dim grandtotal As Integer
        Dim grand As String
        Dim disc As Integer

        text = ""

        Call Company()

        text &= Space(Spacing(branchName)) & branchName & vbCrLf
        text &= Space(Spacing(branchAddress1)) & branchAddress1 & vbCrLf
        text &= Space(Spacing(branchAddress2)) & branchAddress2 & vbCrLf
        text &= Space(Spacing(GetValueParamText("NPWP NO"))) & GetValueParamText("NPWP NO") & vbCrLf
        text &= "========================================" & vbCrLf
        text &= Space(Spacing(judul)) & judul & vbCrLf
        text &= "========================================" & vbCrLf

        For i As Integer = 0 To GridSales.Rows.Count - 1

            text &= Mid(GridSales.Rows(i).Cells(1).Value & " " & GridSales.Rows(i).Cells(2).Value, 1, 40) & vbCrLf
            Dim total As Integer = (GridSales.Rows(i).Cells(4).Value)
            Dim total2 As Integer = total * GridSales.Rows(i).Cells(5).Value

            Dim total3 As Integer = (GridSales.Rows(i).Cells(7).Value)
            Dim total4 As Integer = total3 / GridSales.Rows(i).Cells(4).Value

            If Len(CStr(total2)) = 8 Then
                grand = "    " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 7 Then
                grand = "    " & " " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 6 Then
                grand = "    " & "   " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 5 Then
                grand = "    " & "    " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 4 Then
                grand = "    " & "     " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 3 Then
                grand = vbTab & "             " & Trim(String.Format("{0:#,##0}", total2))
            ElseIf Len(CStr(total2)) = 2 Then
                grand = vbTab & "              " & Trim(String.Format("{0:#,##0}", total2))
            Else
                grand = vbTab & "               " & Trim(String.Format("{0:#,##0}", total2))
            End If
            'grandtotal += total3
            text &= CType(GridSales.Rows(i).Cells(4).Value, Integer) & " X " & Trim(String.Format("{0:#,##0}", GridSales.Rows(i).Cells(5).Value)) & vbTab & vbTab & Space(2) & grand & vbCrLf
            text &= "Discount" & vbTab & vbTab & Space(5) & GETKONVERSIDISCOUNT(GridSales.Rows(i).Cells(4).Value * (GridSales.Rows(i).Cells(5).Value - total4)) & "-" & vbCrLf
            disc = disc + (CInt(GridSales.Rows(i).Cells(4).Value) * GridSales.Rows(i).Cells(6).Value)
        Next

        If printstate = 1 Then
            grandtotal = CDec(fPayment.lblSubTotal.Text)
        Else
            grandtotal = CDec(browseGrandTotal)
        End If

        If Len(CStr(grandtotal)) = 8 Then
            grand = "    " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 7 Then
            grand = "    " & " " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 6 Then
            grand = "    " & "   " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 5 Then
            grand = "    " & "    " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 4 Then
            grand = "    " & "     " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 3 Then
            grand = "    " & "       " & Trim(String.Format("{0:#,##0}", grandtotal))
        ElseIf Len(CStr(grandtotal)) = 2 Then
            grand = "    " & "        " & Trim(String.Format("{0:#,##0}", grandtotal))
        Else
            grand = "    " & "         " & Trim(String.Format("{0:#,##0}", grandtotal))
        End If

        text &= "----------------------------------------" & vbCrLf
        text &= "Grand Total" & vbTab & vbTab & Space(2) & grand & vbCrLf
        text &= "----------------------------------------" & vbCrLf
        text &= vbTab & Space(7) & "DPP       " & Space(5) & GETDPP() & vbCrLf
        text &= vbTab & Space(7) & "PAJAK     " & Space(5) & GETPAJAK() & vbCrLf
        text &= vbTab & Space(7) & "ROUNDING  " & Space(5) & GETROUNDING() & vbCrLf
        text &= vbTab & Space(7) & "CHARGE    " & Space(5) & GETCHARGE() & vbCrLf
        text &= vbTab & Space(7) & "TOTAL     " & Space(5) & GETTOTAL() & vbCrLf
        text &= "----------------------------------------" & vbCrLf
        'Dim val As String
        'Dim totalAmt As Integer
        'dtpayment = Payment(GridInvoice.SelectedCells(0).Value)
        'For a As Integer = 0 To dtpayment.Rows.Count - 1
        '    val = GETKONVERSI(dtpayment.Rows(a).Item(1))
        '    totalAmt = totalAmt + val
        '    text = text & vbTab & Space(7) & dtpayment.Rows(a).Item(0) & vbTab & Space(6) & val & vbCrLf
        'Next

        If printstate = 1 Then
            If fPayment.txtCashAmount.Text <> 0 Then
                text = text & vbTab & Space(7) & "Tunai" & vbTab & Space(6) & GETKONVERSI(CDec(fPayment.txtCashAmount.Text)) & vbCrLf
            End If

            If fPayment.txtCardAmount.Text <> 0 Then
                text = text & vbTab & Space(7) & "Card" & vbTab & Space(6) & GETKONVERSI(CDec(fPayment.txtCardAmount.Text) + CDec(fPayment.txtCharge.Text)) & vbCrLf
            End If
        Else
            If browseCash <> 0 Then
                text = text & vbTab & Space(7) & "Tunai" & vbTab & Space(6) & GETKONVERSI(CDec(browseCash)) & vbCrLf
            End If

            If browseCard <> 0 Then
                text = text & vbTab & Space(7) & "Card" & vbTab & Space(6) & GETKONVERSI(CDec(browseCard) + CDec(browseCharge)) & vbCrLf
            End If
        End If

        text &= vbTab & Space(7) & "Change     " & Space(4) & KEMBALIAN() & vbCrLf
        text &= "========================================" & vbCrLf
        text &= "SELAMAT, ANDA HEMAT : " & Trim(String.Format("{0:#,##0}", disc)) & vbCrLf
        text &= "ITEM PURCHASE : " & GridSales.RowCount & ", QTY : " & TotalQty() & vbCrLf
        text &= "INVOICE: " & lblInvoice.Text & vbCrLf


        text &= "CASHIER: " & Trim(lblEmpName.Text) & vbCrLf

        text &= Format(GetValueParamDate("SYSTEM DATE"), "dd MMM yyyy " & GetTimeNow()) & vbCrLf
        text &= "----------------------------------------" & vbCrLf
        text &= Space(Spacing("***TERIMA KASIH ATAS KUNJUNGAN ANDA***")) & "***TERIMA KASIH ATAS KUNJUNGAN ANDA***" & vbCrLf
        text &= Space(Spacing("HRG BRG KENA PAJAK SUDAH TERMASUK PPN")) & "HRG BRG KENA PAJAK SUDAH TERMASUK PPN" & vbCrLf
        text &= "----------------------------------------" & vbCrLf
        text &= "Barang yang telah dibeli tidak dapat" & vbCrLf
        text &= "ditukarkan kecuali cacat produksi" & vbCrLf
        text &= "dengan batas penukaran 7 hari" & vbCrLf
        text &= "setelah tanggal pembelian dengan" & vbCrLf
        text &= "melampirkan struk belanja" & vbCrLf & vbCrLf & vbCrLf
        text &= ControlChars.NewLine

        '' experiment with how many lines to advance before doing the cut
        'text &= mstrOpenDrawerCode()
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= ControlChars.NewLine
        text &= mstrFullCutCode2

        ' experiment with how many lines to advance after doing the cut (to feed the paper)
        'text &= ControlChars.NewLine
        'text &= ControlChars.NewLine
        'text &= ControlChars.NewLine
        'text &= ControlChars.NewLine
        'text = text & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf

        'Create Directory Struk

        Dim dir As String = "Invoice List" & "\" & GetValueParamText("SYSTEM DATE")
        Dim dir_emp As String = Trim(lblEmpName.Text)

        If Not Directory.Exists(Application.StartupPath & "\" & dir) Then
            Directory.CreateDirectory(Application.StartupPath & "\" & dir)
        End If

        If Not Directory.Exists(Application.StartupPath & "\" & dir & "\" & dir_emp) Then
            Directory.CreateDirectory(Application.StartupPath & "\" & dir & "\" & dir_emp)
        End If

        FILE_NAME = Application.StartupPath & "\" & dir & "\" & dir_emp & "\" & lblInvoice.Text & ".txt"

        If Not File.Exists(FILE_NAME) Then
            File.Create(FILE_NAME).Dispose()
        End If

        Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
        objWriter.Write(text)
        objWriter.Close()


        If File.Exists(Application.StartupPath & "\CashDrawer.exe") Then
            Shell(Application.StartupPath & "\CashDrawer.exe")
        End If

        'If GridSales.RowCount > 35 Then
        '    Dim struk1 As String = ""
        '    Dim struk2 As String = ""

        '    struk1 = text.Substring(1, 3000)

        '    If RawPrinterHelper.SendBytesToPrinter(mstrReceiptPrinterName, Marshal.StringToCoTaskMemAnsi(struk1), struk1.Length()) = True Then

        '        struk2 = text.Substring(3001)

        '        MsgBox("Please wait...", MsgBoxStyle.Information, "Printing...")

        '        RawPrinterHelper.SendBytesToPrinter(mstrReceiptPrinterName, Marshal.StringToCoTaskMemAnsi(struk2), struk2.Length())
        '    End If


        'Else
        RawPrinterHelper.SendStringToPrinter(mstrReceiptPrinterName, text)
        'End If

        'RawPrinterHelper.SendBytesToPrinter(mstrReceiptPrinterName, Marshal.StringToCoTaskMemAnsi(text), text.Length())

        'RawPrinterHelper.SendFileToPrinter(mstrReceiptPrinterName, FILE_NAME)

    End Sub

    Private Function TotalQty() As Integer
        Dim tot As Integer = 0

        For i As Integer = 0 To GridSales.RowCount - 1
            tot = tot + GridSales.Rows(i).Cells(4).Value
        Next

        Return tot
    End Function

    Private Function GETPAJAK() As String

        Dim grand As String
        Dim tot As Integer = 0

        For i As Integer = 0 To GridSales.RowCount - 1
            tot = tot + (CDec(GridSales.Rows(i).Cells(10).Value) * GridSales.Rows(i).Cells(4).Value)
        Next
        If Len(CStr(tot)) = 8 Then
            grand = Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 7 Then
            grand = " " & Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 6 Then
            grand = "   " & Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 5 Then
            grand = "    " & Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 4 Then
            grand = "     " & Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 3 Then
            grand = "       " & Trim(String.Format("{0:#,##0}", tot))
        ElseIf Len(CStr(tot)) = 2 Then
            grand = "        " & Trim(String.Format("{0:#,##0}", tot))
        Else
            grand = "         " & Trim(String.Format("{0:#,##0}", tot))
        End If

        Return grand
    End Function

    Private Function GETKONVERSI(ByVal value As Integer) As String

        Dim grand As String

        If Len(CStr(value)) = 8 Then
            grand = Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 7 Then
            grand = " " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 6 Then
            grand = "   " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 5 Then
            grand = "    " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 4 Then
            grand = "     " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 3 Then
            grand = "       " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 2 Then
            grand = "        " & Trim(String.Format("{0:#,##0}", value))
        Else
            grand = "          " & Trim(String.Format("{0:#,##0}", value))
        End If

        Return grand
    End Function

    Private Function GETKONVERSIDISCOUNT(ByVal value As Integer) As String

        Dim grand As String

        If Len(CStr(value)) = 8 Then
            grand = Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 7 Then
            grand = " " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 6 Then
            grand = "   " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 5 Then
            grand = "    " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 4 Then
            grand = "     " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 3 Then
            grand = "       " & Trim(String.Format("{0:#,##0}", value))
        ElseIf Len(CStr(value)) = 2 Then
            grand = "        " & Trim(String.Format("{0:#,##0}", value))
        Else
            grand = "         " & Trim(String.Format("{0:#,##0}", value))
        End If

        Return grand
    End Function

    Private Function KEMBALIAN() As String

        Dim grand As String
        Dim mchange As Decimal
        If printstate = 1 Then
            mchange = CDec(fPayment.lblChange.Text)
        Else
            mchange = CDec(browseChange)
        End If

        If Len(CStr(mchange)) = 8 Then

            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "-" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = Trim(String.Format("{0:#,##0}", mchange * -1))
            End If

        ElseIf Len(CStr(mchange)) = 7 Then
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = " -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = " " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        ElseIf Len(CStr(mchange)) = 6 Then
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "   -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "   " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        ElseIf Len(CStr(mchange)) = 5 Then
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "    -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "    " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        ElseIf Len(CStr(mchange)) = 4 Then
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "      -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "     " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        ElseIf Len(CStr(mchange)) = 3 Then
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "       -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "      " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        ElseIf Len(CStr(mchange)) = 2 Then

            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "        -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "       " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If

        Else
            If Mid(CStr(mchange), 1, 1) = "-" Then
                grand = "          -" & Trim(String.Format("{0:#,##0}", mchange * -1))
            Else
                grand = "         " & Trim(String.Format("{0:#,##0}", mchange * -1))
            End If
        End If

        Return grand
    End Function

    Private Function GETROUNDING() As String

        Dim grand As String = ""
        Dim mtotal As Decimal = 0

        If printstate = 1 Then

            If CDec(mrounding) < 0 Then
                mtotal = 0
            Else
                mtotal = CDec(mrounding)
            End If

        Else
            mtotal = CDec(browseRounding)
        End If

        If Len(CStr(mtotal)) = 8 Then
            grand = String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 7 Then
            grand = " " & String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 6 Then
            grand = "   " & String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 5 Then
            grand = "    " & String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 4 Then
            grand = "     " & String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 3 Then
            grand = "       " & String.Format("{0:#,##0}", mtotal)
        ElseIf Len(CStr(mtotal)) = 2 Then
            grand = "        " & String.Format("{0:#,##0}", mtotal)
        Else
            grand = "         " & String.Format("{0:#,##0}", mtotal)
        End If

        Return grand
    End Function

    Private Function GETCHARGE() As String

        Dim grand As String
        Dim mTotalDec As Decimal

        If printstate = 1 Then
            mTotalDec = CDec(fPayment.txtCharge.Text)
        Else
            mTotalDec = CDec(browseCharge)
        End If


        If Len(CStr(mTotalDec)) = 8 Then
            grand = String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 7 Then
            grand = " " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 6 Then
            grand = "   " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 5 Then
            grand = "    " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 4 Then
            grand = "     " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 3 Then
            grand = "       " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 2 Then
            grand = "        " & String.Format("{0:#,##0}", mTotalDec)
        Else
            grand = "         " & String.Format("{0:#,##0}", mTotalDec)
        End If

        Return grand
    End Function

    Private Function GETTOTAL() As String

        Dim grand As String
        Dim mTotalDec As Decimal

        If printstate = 1 Then
            mTotalDec = CDec(fPayment.lblSubTotal.Text)
        Else
            mTotalDec = CDec(browseGrandTotal)
        End If


        If Len(CStr(mTotalDec)) = 8 Then
            grand = String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 7 Then
            grand = " " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 6 Then
            grand = "   " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 5 Then
            grand = "    " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 4 Then
            grand = "     " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 3 Then
            grand = "       " & String.Format("{0:#,##0}", mTotalDec)
        ElseIf Len(CStr(mTotalDec)) = 2 Then
            grand = "        " & String.Format("{0:#,##0}", mTotalDec)
        Else
            grand = "          " & String.Format("{0:#,##0}", mTotalDec)
        End If

        Return grand
    End Function

    Private Function GETDPP() As String

        Dim grand As String
        Dim tot As Integer = 0
        Dim amount As Decimal = 0

        For i As Integer = 0 To GridSales.RowCount - 1
            amount = 0
            amount = (CDec(GridSales.Rows(i).Cells(8).Value) - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(11).Value / 100)) * GridSales.Rows(i).Cells(4).Value

            If GridSales.Rows(i).Cells(13).Value > 0 Then
                amount = amount + (CDec(amount) - (CDec(amount) * GridSales.Rows(i).Cells(13).Value / 100)) * GridSales.Rows(i).Cells(4).Value

            End If

            If GridSales.Rows(i).Cells(15).Value > 0 Then
                amount = 0
                amount = (CDec(GridSales.Rows(i).Cells(8).Value) - (CDec(GridSales.Rows(i).Cells(8).Value) * GridSales.Rows(i).Cells(15).Value / 100)) * GridSales.Rows(i).Cells(4).Value

            End If

            tot += amount
        Next



        If Len(CStr(tot)) = 8 Then
            grand = String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 7 Then
            grand = " " & String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 6 Then
            grand = "   " & String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 5 Then
            grand = "    " & String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 4 Then
            grand = "     " & String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 3 Then
            grand = "       " & String.Format("{0:#,##0}", tot)
        ElseIf Len(CStr(tot)) = 2 Then
            grand = "        " & String.Format("{0:#,##0}", tot)
        Else
            grand = "        " & String.Format("{0:#,##0}", tot)
        End If

        Return grand
    End Function

    Private Sub Company()
        Dim data As New DataTable
        Try

            query = "select coy_description,coy_Address01,coy_Address02,coy_NPWP from " & DB & ".dbo.mbranch" & _
                    " where coy_branch='" & Default_Branch & "'"

            If cn.State = ConnectionState.Open Then cn.Close()

            With cm
                .Connection = cn
                .CommandType = CommandType.Text
                .CommandText = query
            End With

            With da
                .SelectCommand = cm
                .Fill(data)
            End With

            branchName = Trim(data.Rows(0).Item(0))
            branchAddress1 = Trim(data.Rows(0).Item(1))
            branchAddress2 = Trim(data.Rows(0).Item(2))
            npwp = Trim(data.Rows(0).Item(3))
            cn.Close()
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Superman")
        End Try

    End Sub

    Private Function Spacing(ByVal txt As String) As Integer
        Dim temp As String = ""
        Dim def As Integer = 40
        Dim sisa As Integer = 0

        temp = txt
        sisa = def - Len(temp)

        Return sisa / 2
    End Function

    Private Sub frmPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadImage()
        'LoadSalesOrg(cmbSalesOrg, gridAll)
        'LoadCustomer(cmbCustomer, gridAll)
        'lblKasir.Text = UserName

        table = New DataTable
        table = GETDetailEmployee(mEmployeeID)

        If table.Rows.Count > 0 Then
            lblEmpName.Text = Trim(table.Rows(0).Item(1))
        Else
            lblEmpName.Text = "SYSTEM"
        End If

        backupPOSItem = New DataTable

    End Sub

    Private Sub LoadImage()

        btnNew.Image = mainClass.imgList.ImgBtnNew

        btnPayment.Image = mainClass.imgList.ImgBtnPayment

        btnVoid.Image = mainClass.imgList.ImgBtnVoid

        btnPrint.Image = mainClass.imgList.ImgBtnPrint

        btnBrowse.Image = mainClass.imgList.ImgBtnBrowse

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelShoppingCart

    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            frmBrowse.EmployeeID = mEmployeeID

            If frmBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then

                browseInvoice = frmBrowse.GridInvoice.SelectedCells(0).Value
                browseGrandTotal = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(2).Value))
                browseChange = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(3).Value))
                browseCash = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(4).Value))
                browseCard = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(5).Value))
                browseCharge = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(6).Value))
                browseRounding = String.Format("{0:#,##0}", CDec(frmBrowse.GridInvoice.SelectedCells(7).Value))
                lblTotal.Text = frmBrowse.GridInvoice.SelectedCells(2).Value
                state = 3
                DetailClear()
                InsertDetailInvoice(browseInvoice)
                GridSales.Enabled = True
                GridSales.ReadOnly = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub InsertDetailInvoice(ByVal doc As String)
        Try
            seqnum = 0

            If cn.State = ConnectionState.Closed Then cn.Open()
            table = New DataTable
            With cm
                .Connection = cn
                .CommandText = "SELECT ds_partnumber,type_description," & _
                                "ds_uom,ds_qty,ds_pricedisp,ds_dpp+ds_ppn ds_amount," & _
                                "ds_price,ds_pctppn,ds_ppn/ds_qty ds_ppn," & _
                                "ds_disc1,ds_pricedisp*ds_disc1/100 ds_disc1amt," & _
                                "ds_disc2,ds_pricedisp*ds_disc2/100 ds_disc2amt," & _
                                "ds_disc3,ds_pricedisp*ds_disc3/100 ds_disc3amt,type_taxgroup,ds_product " & _
                                "FROM " & DB & ".dbo.tslsd " & _
                                "INNER JOIN " & DB & ".dbo.mtipe ON type_partnumber=ds_partnumber " & _
                                "WHERE ds_invoice = '" & doc & "'"
            End With

            With da
                .SelectCommand = cm
                .Fill(table)
            End With

            If table.Rows.Count > 0 Then
                GridSales.Rows.Clear()
                For i As Integer = 0 To table.Rows.Count - 1
                    seqnum += 1

                    GridSales.Rows.Add(New Object() {seqnum _
                                                       , table.Rows(i).Item("ds_partnumber") _
                                                       , table.Rows(i).Item("type_description") _
                                                       , table.Rows(i).Item("ds_uom") _
                                                       , table.Rows(i).Item("ds_qty") _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_pricedisp")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc1amt") + table.Rows(i).Item("ds_disc2amt") + table.Rows(i).Item("ds_disc3amt")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_amount")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_price")) _
                                                       , String.Format("{0:#,##0}", IIf(Trim(table.Rows(i).Item("type_taxgroup")) = "01", GetValueParamMoney("DEFAULT PPN"), 0)) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_ppn")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc1")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc1amt")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc2")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc2amt")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc3")) _
                                                       , String.Format("{0:#,##0}", table.Rows(i).Item("ds_disc3amt")) _
                                                        , String.Format("{0:#,##0}", table.Rows(i).Item("ds_product"))})
                Next

            End If


            cn.Close()
        Catch ex As Exception
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub GridSales_DoubleClick(sender As Object, e As System.EventArgs) Handles GridSales.DoubleClick
        'If GridSales.Rows.Count > 0 Then
        '    table = New DataTable

        '    table = GetPromo(mSalesOffice, GridSales.SelectedCells(1).Value)

        '    'Priority Promo Best Price
        '    For i As Integer = 0 To table.Rows.Count - 1
        '        If table.Rows(i).Item(1) = "BEST PRICE" Then
        '            lblEventNote.Text = Trim(table.Rows(i).Item(1))
        '            Exit Sub
        '        End If
        '    Next
        '    If table.Rows.Count > 0 Then
        '        lblEventNote.Text = Trim(table.Rows(0).Item(1))
        '    Else
        '        lblEventNote.Text = ""
        '    End If
        'End If
    End Sub

    Private Sub GridSales_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridSales.KeyUp
        If e.KeyCode = Keys.Delete Then
            Total()
        End If
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        If Trim(txtQty.Text) = "" Then txtQty.Text = 1
        If Trim(txtQty.Text) = 0 Then txtQty.Text = 1
    End Sub

    'Private Sub btnVoid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoid.Click

    '    VoidTransaction()

    'End Sub

    Private Sub VoidTransaction()
        If MsgBox("Are you sure void this invoice!!?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Void Invoice") = MsgBoxResult.No Then Exit Sub
        Dim reason As String
        Try
reason:
            reason = InputBox("Please input your reason", "Void Reason")

            If Trim(reason) <> "" Then

                VoidInvoice(lblInvoice.Text, Trim(reason))
                printstate = 2
                Call PrintInvoice("----VOID TRANSACTION----")
                UpdatePrintNum(browseInvoice)

                state = 0
                DetailClear()
            Else
                MsgBox("Please give your reason!!", MsgBoxStyle.Exclamation, "Reason Reminder")
                GoTo reason

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub ClosingProgram()

        If btnClose.Text = "Cancel" Then
            state = 0
            DetailClear()
        Else
            If state = 1 Or state = 2 Then
                MsgBox("Please Finish Editing!!", MsgBoxStyle.Exclamation, Title)
                Exit Sub
            Else
                Me.Close()
            End If
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ClosingProgram()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If MsgBox("Are you sure Remove Item " & GridSales.Rows(GridSales.CurrentRow.Index).Cells(1).Value & " - " & GridSales.Rows(GridSales.CurrentRow.Index).Cells(2).Value & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, Title) = MsgBoxResult.No Then Exit Sub
        GridSales.Rows.RemoveAt(GridSales.CurrentRow.Index)
        Total()
        lblTotalQty.Text = TotalQty()

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridSales.RowCount > 0 Then
            ContextMenuStrip1.Enabled = True
        Else
            ContextMenuStrip1.Enabled = False

        End If
    End Sub

    Private Sub btnCancelMember_Click(sender As Object, e As EventArgs) Handles btnCancelMember.Click
        Try
            If mMemberCode <> "" Then
                GridSales.Rows.Clear()

                For i As Integer = 0 To backupPOSItem.Rows.Count - 1
                    With GridSales
                        .Rows.Add(New Object() {backupPOSItem.Rows(i).Item(0) _
                                   , backupPOSItem.Rows(i).Item(1) _
                                   , backupPOSItem.Rows(i).Item(2) _
                                   , backupPOSItem.Rows(i).Item(3) _
                                   , backupPOSItem.Rows(i).Item(4) _
                                   , backupPOSItem.Rows(i).Item(5) _
                                   , backupPOSItem.Rows(i).Item(6) _
                                   , backupPOSItem.Rows(i).Item(7) _
                                   , backupPOSItem.Rows(i).Item(8) _
                                   , backupPOSItem.Rows(i).Item(9) _
                                   , backupPOSItem.Rows(i).Item(10) _
                                   , backupPOSItem.Rows(i).Item(11) _
                                   , backupPOSItem.Rows(i).Item(12) _
                                   , backupPOSItem.Rows(i).Item(13) _
                                   , backupPOSItem.Rows(i).Item(14) _
                                   , backupPOSItem.Rows(i).Item(15) _
                                   , backupPOSItem.Rows(i).Item(16) _
                                   , backupPOSItem.Rows(i).Item(17)})
                    End With
                Next

                'clear backupPOSITEM
                backupPOSItem.Rows.Clear()
                mMemberCode = ""
                mMemberDisc = 0
                mMemberMinPayment = 0
                lblMember.Text = "-"
                btnCancelMember.Visible = False

            Else

            End If

            Total()
            lblTotalQty.Text = TotalQty()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnVoid_Click(sender As Object, e As EventArgs) Handles btnVoid.Click
       CheckPrinter

    End Sub

    Private Sub CheckPrinter()
        'Dim Printers As Object
        'Dim Printer As Object
        'Dim WMIObj As String
        'Dim lItem As ListViewItem

        'WMIObj = "winmgmts://localhost"
        'Printers = GetObject(WMIObj).InstancesOf("win32_Printer")
        ''clear list
        'Me.lvwPrinter.Items.Clear()
        'For Each Printer In Printers
        '    lItem = Me.lvwPrinter.Items.Add(Printer.name)
        '    Select Case Printer.PrinterStatus
        '        Case 3
        '            lItem.SubItems.Add("Idle")
        '        Case 4
        '            lItem.SubItems.Add("Printing")
        '        Case Else
        '            lItem.SubItems.Add("Unknown")
        '    End Select
        'Next
    End Sub

End Class