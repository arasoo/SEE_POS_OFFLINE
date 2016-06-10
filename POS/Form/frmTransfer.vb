Imports genLib.General
Imports connLib.DBConnection
Imports System.Drawing.Drawing2D
Imports System.IO
Imports proLib.Process
Imports sqlLib.Sql
Imports mainlib
Imports System.Data.SqlClient


Public Class frmTransfer

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Private state As Integer
    Private mTitle As String
    Private mTransID As String
    Private mfromWH As String
    Private mFlag As Integer
    Private Const mTransDoc As String = "TS"
    Private seqnum As Integer = 0
    Private dataItem As DataTable
    Private mSuppCode As String = ""
    Private firstLoad As Boolean = False

    Public WriteOnly Property TransferTitle As String
        Set(ByVal value As String)
            mTitle = value

        End Set
    End Property

    Public WriteOnly Property TransID As String
        Set(ByVal value As String)
            mTransID = value

        End Set
    End Property

    Public WriteOnly Property FromWH As String
        Set(ByVal value As String)
            mfromWH = value

        End Set
    End Property

    Public WriteOnly Property TransferFlag As Integer
        Set(ByVal value As Integer)
            mflag = value

        End Set
    End Property


    'Private Sub frmTransfer_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    Me.WindowState = FormWindowState.Maximized
    'End Sub


    Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridTransfer.RowCount > 0 Then
            EditToolStripMenuItem.Enabled = True
            DeleteToolStripMenuItem.Enabled = True
            If logOn = "00-IT" Then

                CreateXMLToolStripMenuItem.Enabled = True
            Else
                CreateXMLToolStripMenuItem.Enabled = False
            End If
        Else
            EditToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Enabled = False
            CreateXMLToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        state = 1
        DetailClear()
    End Sub

    Private Sub DetailClear()
        If state = 1 Then
            txtNote.Enabled = True
            lblDocNo.Text = GetLastTransNo(mTransDoc)
            btnSave.Enabled = True

            btnEdit.Enabled = False
            btnClose.Text = "Cancel"

            btnClose.Image = mainClass.imgList.ImgBtnCancel

            btnNew.Enabled = False

            cmbToWH.Enabled = True
            txtItem.Enabled = True
            chckScan.Enabled = True

            If chckScan.Checked = True Then
                txtQty.Enabled = False
                txtItem.Focus()
            Else
                txtQty.Enabled = True
                txtItem.Focus()
            End If
            gridTransfer.Enabled = True
            dtDate.Value = Now
            txtNote.Clear()
            txtReffDoc.Clear()
            txtReffDoc.Enabled = True

            If mFlag = 0 Then
                cmbFromWH.Enabled = False
            ElseIf mFlag = 1 Then
                cmbFromWH.Enabled = True
                cmbToWH.Enabled = False
            Else
                cmbToWH.Enabled = False
                cmbFromWH.Enabled = True
            End If

        ElseIf state = 2 Then

        Else


            txtNote.Enabled = False

            btnNew.Enabled = True

            btnSave.Enabled = False
            btnEdit.Enabled = False
            btnClose.Text = "Close"
            btnClose.Image = mainClass.imgList.ImgBtnClosing

            cmbToWH.Enabled = False
            txtItem.Enabled = False
            chckScan.Enabled = False
            txtReffDoc.Enabled = False
            cmbFromWH.Enabled = False
            dtDate.Value = Now
            txtNote.Clear()
            txtReffDoc.Clear()

            lblDocNo.Text = ""
            gridTransfer.Rows.Clear()
            gridTransfer.Enabled = False
            dataItem = Nothing

            lblTotalItem.Text = 0
            lblTotalQty.Text = 0



        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        state = 2
        DetailClear()
    End Sub

    Private Sub LoadImage()
        
        btnNew.Image = mainClass.imgList.ImgBtnNew

        btnBrowse.Image = mainClass.imgList.ImgBtnBrowse

        btnEdit.Image = mainClass.imgList.ImgBtnEdit

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelPurchase

    End Sub

    Private Sub frmTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            state = 1
            DetailClear()
        ElseIf e.KeyCode = Keys.F3 Then
            If state = 1 Then
                If frmListItem.ShowDialog = Windows.Forms.DialogResult.OK Then
                    txtItem.Text = frmListItem.GridListItem.SelectedCells(0).Value
                    frmListItem.Close()
                End If
            End If

        ElseIf e.KeyCode = Keys.F5 Then
            SaveTransaction()

        ElseIf e.KeyCode = Keys.F10 Then
            If chckScan.CheckState = 1 Then
                txtQty.Enabled = False
                chckScan.Checked = False

                txtItem.Focus()
            Else
                txtQty.Enabled = True
                chckScan.Checked = True
                txtItem.Focus()
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            ClosingProgram()

        End If

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

                If Not ItemExists(Trim(txtItem.Text)) = True Then
                    MsgBox("Item not available!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If


                If Not ItemAssignmentExists(Trim(txtItem.Text), GetValueParamText("DEFAULT WH")) = True Then
                    MsgBox("Item not found in warehouse!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                dataItem = GetDetailItem(Trim(txtItem.Text))

                If dataItem.Rows(0).Item("type_materialtype") = "520" Or
                    dataItem.Rows(0).Item("type_materialtype") = "510" Or
                    dataItem.Rows(0).Item("type_materialtype") = "610" Then

                End If

                If chckScan.Checked = True Then ' scan one by one

                    If CheckStockMinus(Trim(txtItem.Text), txtQty.Text) = True Then

                        MsgBox("Over Stock!!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub

                    End If

                    If GridTransfer.Rows.Count > 0 Then
                        If IsExists(Trim(txtItem.Text)) Then
                            Calculate(Trim(txtItem.Text))
                            Total()
                        Else
                            seqnum = GetLastSeqnum()
                            GridTransfer.Rows.Add(New Object() {seqnum, Mid(dataItem.Rows(0).Item(0), 1, 13), _
                                                                dataItem.Rows(0).Item("type_description"), _
                                                                dataItem.Rows(0).Item("type_product"), _
                                                                dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text)})

                            Total()
                        End If
                    Else
                        seqnum = GetLastSeqnum()
                        GridTransfer.Rows.Add(New Object() {seqnum, Mid(dataItem.Rows(0).Item(0), 1, 13), _
                                                                  dataItem.Rows(0).Item("type_description"), _
                                                                  dataItem.Rows(0).Item("type_product"), _
                                                                  dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text)})
                        Total()
                    End If
                    txtItem.Clear()
                Else
                    txtQty.Focus()
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        End If
    End Sub

    Private Function IsExists(ByVal kode As String) As Boolean
        Try
            For i As Integer = 0 To GridTransfer.Rows.Count - 1
                If GridTransfer.Rows(i).Cells(1).Value = kode Then
                    Return True
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Total()
        Dim jml As Integer = 0

        lblTotalItem.Text = gridTransfer.RowCount

        For i As Integer = 0 To gridTransfer.RowCount - 1
            jml += CInt(gridTransfer.Rows(i).Cells(5).Value)

        Next

        lblTotalQty.Text = jml
    End Sub

    Private Sub Calculate(ByVal kode As String)
        Try
            For i As Integer = 0 To GridTransfer.Rows.Count - 1
                If GridTransfer.Rows(i).Cells(1).Value = kode Then

                    GridTransfer.Rows(i).Cells(5).Value += CInt(txtQty.Text)
                   
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetLastSeqnum() As Integer
        Dim iden As Integer = 0
        If GridTransfer.Rows.Count > 0 Then
            For i As Integer = 0 To GridTransfer.Rows.Count - 1
                iden = GridTransfer.Rows(i).Cells(0).Value
            Next

            iden = iden + 1
        Else
            iden = 1
        End If

        Return iden
    End Function

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

    Private Sub txtQty_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.Click
        txtQty.SelectAll()
    End Sub

    Private Sub txtQty_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.GotFocus
        txtQty.SelectAll()
    End Sub

    Private Sub frmTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = mTitle
        lblTitle.Text = mTitle

        If mFlag = 0 Then 'Warehouse Stock Movement

            LoadWarehouse(cmbFromWH, gridAll, 0)
            LoadWarehouse(cmbToWH, gridAll, 0)
            cmbFromWH.SelectedValue = GetValueParamText("DEFAULT WH")

            lblFrom.Text = "From WH"
            lblTo.Text = "To"
        ElseIf mFlag = 1 Then 'Interbranch

            LoadCustomer(cmbFromWH, gridAll, 0)
            lblFrom.Text = "Customer"
            lblTo.Text = "Ship To"

        Else 'Return Supplier

        End If

        LoadImage()

        firstLoad = True

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ClosingProgram()

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick

        cmbToWH.SelectedValue = gridAll.SelectedCells(0).Value
        gridAll.Visible = False
    End Sub

    Private Sub cmbFromWHClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbToWH.Click, cmbFromWH.Click
        Try

            Dim senderCmb As ComboBox = DirectCast(sender, ComboBox)

            If senderCmb.Tag = "FROW WH" Then
                If mFlag = 0 Then
                    LoadWarehouse(senderCmb, gridAll, 1)
                ElseIf mFlag = 1 Then
                    LoadCustomer(senderCmb, gridAll, 1)
                Else

                End If
            Else
                If mFlag = 0 Then LoadWarehouse(senderCmb, gridAll, 1)




            End If


            gridAll.Location = New Point(senderCmb.Left, senderCmb.Location.Y + 22)
            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) +
                            (senderCmb.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 60, GetRowHeight(gridAll.Rows.Count, gridAll))
            senderCmb.DroppedDown = False


            If gridAll.Visible = True Then
                gridAll.Visible = False
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
            End If

            gridAll.Tag = senderCmb.Tag

            If senderCmb.Tag = "FROM WH" Then
                gridAll.Columns(0).Width = 70
            Else
                gridAll.Columns(0).Width = 50
            End If

            gridAll.Columns(1).Width = gridAll.Width - 54
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub chckScan_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckScan.CheckedChanged
        If chckScan.Checked = True Then
            txtQty.Enabled = False
            txtItem.Focus()
        Else
            txtQty.Enabled = True
            txtItem.Focus()
        End If
    End Sub

    Private Sub txtNote_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNote.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then

            Try
                If Trim(txtItem.Text) = "" Then Exit Sub

                If Not ItemExists(Trim(txtItem.Text)) = True Then
                    MsgBox("Item not available!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                dataItem = GetDetailItem(Trim(txtItem.Text))

                If Not dataItem.Rows.Count > 0 Then
                    MsgBox("Item not found in Warehouse", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                If CheckStockMinus(Trim(txtItem.Text), txtQty.Text) = True Then

                    MsgBox("Over Stock!!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub

                End If

                If gridTransfer.Rows.Count > 0 Then
                    If IsExists(Trim(txtItem.Text)) Then
                        Calculate(Trim(txtItem.Text))
                        Total()
                    Else
                        seqnum = GetLastSeqnum()
                        gridTransfer.Rows.Add(New Object() {seqnum, Mid(dataItem.Rows(0).Item(0), 1, 13), _
                                                            dataItem.Rows(0).Item("type_description"), _
                                                            dataItem.Rows(0).Item("type_product"), _
                                                            dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text)})

                        Total()
                    End If
                Else
                    seqnum = GetLastSeqnum()
                    gridTransfer.Rows.Add(New Object() {seqnum, Mid(dataItem.Rows(0).Item(0), 1, 13), _
                                                              dataItem.Rows(0).Item("type_description"), _
                                                              dataItem.Rows(0).Item("type_product"), _
                                                              dataItem.Rows(0).Item("type_uom"), CInt(txtQty.Text)})
                    Total()
                End If
              
                txtQty.Text = 1
                txtItem.Clear()
                txtItem.Focus()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try

        End If
    End Sub

    Private Sub SaveTransaction()

        If Trim(txtNote.Text) = "" Then
            MsgBox("Please Input Note!", MsgBoxStyle.Exclamation, Title)
            txtNote.Focus()
            Exit Sub
        End If

        If gridTransfer.RowCount = 0 Then
            MsgBox("No Detail!", MsgBoxStyle.Exclamation, Title)
            Exit Sub
        End If

        Try

            'cek item tidak boleh minus
            For i As Integer = 0 To gridTransfer.RowCount - 1
                If CheckStockMinus(Trim(gridTransfer.Rows(i).Cells(1).Value), gridTransfer.Rows(i).Cells(5).Value) = True Then

                    MsgBox("Item " & Trim(gridTransfer.Rows(i).Cells(1).Value) & " Over Stock!!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub

                End If
            Next

            Me.Cursor = Cursors.WaitCursor
            SaveData(state)
            UpdateHistoryPOS(Trim(lblDocNo.Text), mTransDoc)
            MsgBox("Data Saved Successfully", MsgBoxStyle.Information, Title)
            state = 0
            DetailClear()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SaveTransaction()
    End Sub

    Private Sub SaveData(ByVal kode As Integer)
        Dim temp As New DataTable
        Dim hcustSupp As String = ""
        Dim hcustSuppCode As String = ""
        Dim DNNo As String = ""

        Try


            If state = 1 Then 'Add


                With temp.Columns
                    .Add("Item", GetType(String))
                    .Add("Description", GetType(String))
                    .Add("Product", GetType(String))
                    .Add("UOM", GetType(String))
                    .Add("Qty", GetType(Integer))
                End With

                For i As Integer = 0 To gridTransfer.RowCount - 1
                    With temp
                        .Rows.Add(New Object() {gridTransfer.Rows(i).Cells(1).Value _
                                               , gridTransfer.Rows(i).Cells(2).Value _
                                               , gridTransfer.Rows(i).Cells(3).Value _
                                               , gridTransfer.Rows(i).Cells(4).Value _
                                               , gridTransfer.Rows(i).Cells(5).Value})

                    End With
                Next


                'save detail
                If mTransID = "PN102" Or mTransID = "PN101" Then
                    hcustSupp = "S"
                    hcustSuppCode = mSuppCode

                ElseIf mTransID = "MM106" Or mTransID = "MM410" Then
                    hcustSupp = "C"
                    hcustSuppCode = ""
                    mSuppCode = ""

                End If

                Dim statusItem As String = ""

                If cn.State = ConnectionState.Closed Then cn.Open()

                ct = cn.BeginTransaction("Save Movement")

                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .Transaction = ct
                    For i As Integer = 0 To temp.Rows.Count - 1
                        .CommandText = "INSERT INTO " & DB & ".dbo.ttsd " &
                                       "(dts_doi,dts_partnumber,dts_product,dts_uom," &
                                       "dts_qty,dts_cost,dts_batchno,dts_uomunit,dts_note) " &
                                       "VALUES ('" & Trim(lblDocNo.Text) & "','" & temp.Rows(i).Item(0) & "','" & temp.Rows(i).Item(2) & "'" &
                                       ",'" & temp.Rows(i).Item(3) & "','" & temp.Rows(i).Item(4) & "',0" &
                                       ",'',1,'')"

                        .ExecuteNonQuery()

                        statusItem = 4

                        Call NewTransactionStock(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Format(GetValueParamDate("SYSTEM DATE"), "yyyy-MM-dd") _
                                                     , mTransID, hcustSupp, hcustSuppCode, Trim(lblDocNo.Text), GetValueParamText("DEFAULT WH") _
                                                     , temp.Rows(i).Item(0), temp.Rows(i).Item(4), "-", temp.Rows(i).Item(2), statusItem _
                                                     , temp.Rows(i).Item(1), temp.Rows(i).Item(3))
                    Next

                End With

                'save header
                InsertHeaderTS(GetValueParamText("DEFAULT COMPANY"), GetValueParamText("DEFAULT BRANCH"), Trim(lblDocNo.Text) _
                               , dtDate.Value, GetValueParamText("DEFAULT WH"), cmbToWH.SelectedValue, mTransID, mSuppCode _
                               , DNNo, Trim(txtNote.Text))

                Dim custAffco As String = ""
                Dim JournalCode As String = ""

                If mFlag = 0 Then
                    JournalCode = "000"
                ElseIf mFlag = 1 Then 'interbranch
                    custAffco = GetCustAFFCO(cmbFromWH.SelectedValue)
                    JournalCode = GetJournalTran(mTransID, custAffco)
                Else 'supplier
                    JournalCode = "420"
                End If

                If cn.State = ConnectionState.Closed Then cn.Open()
                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .Transaction = ct
                    .CommandText = "INSERT INTO " & DB & ".dbo.ttsh " &
                                    "(hts_company,hts_branch,hts_salesorg,hts_salesoffice,hts_doi,hts_date,hts_dc," &
                                    "hts_wh,hts_trnid,hts_supplier,hts_customer,hts_qq,hts_to_wh,hts_salesman," &
                                    "hts_reffdoc,hts_alocflag,hts_pickflag,hts_dnflag,hts_postingflag,hts_tpflag," &
                                    "hts_dn,hts_note,hts_deliveryroute,hts_costcenter,hts_journal,hts_counter," &
                                    "hts_createuser,hts_createdate,hts_createtime) " &
                                    "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "'" &
                                    ",'" & GetValueParamText("DEFAULT BRANCH") & "','',''" &
                                    ",'" & Trim(lblDocNo.Text) & "','" & Format(dtDate.Value, formatDate) & "'" &
                                    ",'" & GetValueParamText("DEFAULT BRANCH") & "'" &
                                    ",'" & GetValueParamText("DEFAULT WH") & "','" & mTransID & "'" &
                                    ",'" & IIf(mFlag = 2, cmbFromWH.SelectedValue, "") & "','" & IIf(mFlag = 1, cmbFromWH.SelectedValue, "") & "'" &
                                    ",'" & IIf(mFlag = 1, cmbToWH.SelectedValue, "") & "'" &
                                    ",'" & IIf(mFlag = 0, cmbToWH.SelectedValue, "") & "','','','Y'" &
                                    ",'Y','Y','N','Y','','" & Trim(txtNote.Text) & "','','','" & JournalCode & "',0,'" & logOn & "'" &
                                    ",'" & Format(dtDate.Value, formatDate) & "','" & Format(Now, "HHmmss") & "')"
                    .ExecuteNonQuery()


                End With

                ct.Commit()
            Else 'Edit

            End If
        Catch ex As Exception
            ct.Rollback()
            Throw ex
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub

    Private Sub CreateXMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateXMLToolStripMenuItem.Click
        Dim ds As DataSet = New DataSet("Transfer Out")

        Dim dt As New DataTable

        With dt.Columns
            .Add("No", GetType(Integer))
            .Add("Item", GetType(String))
            .Add("Description", GetType(String))
            .Add("UOM", GetType(String))
            .Add("Qty", GetType(Integer))
        End With

        For i As Integer = 0 To gridTransfer.RowCount - 1
            With dt
                .Rows.Add(New Object() {gridTransfer.Rows(i).Cells(0).Value _
                                       , gridTransfer.Rows(i).Cells(1).Value _
                                       , gridTransfer.Rows(i).Cells(2).Value _
                                       , gridTransfer.Rows(i).Cells(4).Value _
                                       , gridTransfer.Rows(i).Cells(5).Value})

            End With
        Next

        ds.Tables.Add(dt)

        ds.WriteXmlSchema(Application.StartupPath & "\XML\WarehouseStockTransferOut.xsd")


    End Sub

    Private Sub cmbFromWH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFromWH.SelectedIndexChanged
        If firstLoad = True Then
            If mFlag = 1 Then
                LoadCustomerShipTo(cmbToWH, gridAll, cmbFromWH.SelectedValue)
            End If
        End If

    End Sub
End Class