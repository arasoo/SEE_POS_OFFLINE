Imports genLib.General
Imports prolib.Process
Imports System.Drawing.Drawing2D
'Imports impLib.Import
Imports System.IO
Imports mainlib
Imports sqlLib.Sql
Imports connLib.DBConnection
Imports System.Data.SqlClient

Public Class FrmWR
    Private state As Integer
    Private no As String
    Private mtransID As String
    Private Const mTransDoc As String = "BM"
    Private docWR As String
    Private postWR As String
    Private mfromWH As String
    Private mflag As Integer
    Private mWrTitle As String = ""
    Private mSuppCode As String = ""
    Private mReffno As String = ""
    Private tempStock As Integer = 0
    Private temp As DataTable

    Sub New()

        ' This call is required by the Windows Form Designer '
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. '
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()

    End Sub

    Public WriteOnly Property WRTitle As String
        Set(ByVal value As String)
            mWrTitle = value

        End Set
    End Property

    Public WriteOnly Property WRTransID As String
        Set(ByVal value As String)
            mtransID = value

        End Set
    End Property

    Public WriteOnly Property FromWH As String
        Set(ByVal value As String)
            mfromWH = value

        End Set
    End Property

    Public WriteOnly Property WRFlag As Integer
        Set(ByVal value As Integer)
            mflag = value

        End Set
    End Property


    Public WriteOnly Property DocumentWR() As String
        Set(ByVal value As String)
            docWR = value
        End Set
    End Property

    Public WriteOnly Property PostingWR() As String
        Set(ByVal value As String)
            postWR = value
        End Set
    End Property

    'Private Sub FrmWR_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    Me.WindowState = FormWindowState.Maximized
    'End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If GridWR.RowCount > 0 Then
            EditToolStripMenuItem.Enabled = True
            DeleteToolStripMenuItem.Enabled = True
        Else
            EditToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        state = 2
        DetailClear()
    End Sub

    'Private Sub CreateColumn()
    '    Dim col1, col2, col3, col4, col5 As DataGridViewTextBoxColumn

    '    col1 = New DataGridViewTextBoxColumn
    '    With col1
    '        .HeaderText = "No."
    '        .Width = 40
    '    End With

    '    col2 = New DataGridViewTextBoxColumn
    '    With col2
    '        .HeaderText = "Item Code"
    '        .Width = 110
    '    End With

    '    col3 = New DataGridViewTextBoxColumn
    '    With col3
    '        .HeaderText = "Description"
    '        .Width = 470
    '    End With

    '    col4 = New DataGridViewTextBoxColumn
    '    With col4
    '        .HeaderText = "UOM"
    '        .Width = 50
    '    End With

    '    col5 = New DataGridViewTextBoxColumn
    '    With col5
    '        .HeaderText = "Qty"
    '        .Width = 50
    '    End With

    '    GridWR.Columns.Add(col1)
    '    GridWR.Columns.Add(col2)
    '    GridWR.Columns.Add(col3)
    '    GridWR.Columns.Add(col4)
    '    GridWR.Columns.Add(col5)



    'End Sub

    Private Sub NewTransaction()
        Try
            If mflag = 0 Then
                cmbFromWH.Enabled = False
                cmbToWH.Enabled = False

            Else
                txtDNNo.Enabled = False
                dtDNDate.Enabled = False
            End If

            btnClose.Text = "Cancel"

            btnClose.Image = mainClass.imgList.ImgBtnCancel

            txtDNNo.Enabled = True
            dtDNDate.Enabled = True
            txtReffNo.Enabled = True
            txtNote.Enabled = True
            lblDocNo.Text = GetLastTransNo(mTransDoc)
            btnSave.Enabled = True
            btnEdit.Enabled = False
            btnNew.Enabled = False
            txtReffNo.Text = ""
            txtReffNo.Enabled = True
            txtDNNo.Clear()
            dtDNDate.Value = Now
            txtNote.Clear()
            GridWR.Enabled = True

            txtReffNo.Focus()

        Catch ex As Exception
            Throw ex
        End Try
     

    End Sub

    Private Sub DetailClear()
        If state = 1 Then

            NewTransaction()
           
        ElseIf state = 2 Then

        Else

            If mflag = 0 Then
                cmbFromWH.Enabled = False
                cmbToWH.Enabled = False

            Else
                txtDNNo.Enabled = False
                dtDNDate.Enabled = False
            End If

          
            txtNote.Enabled = False
            btnClose.Text = "Close"
           
            btnClose.Image = mainClass.imgList.ImgBtnClosing

            btnNew.Enabled = True
            btnSave.Enabled = False
            btnEdit.Enabled = False
            txtReffNo.Text = ""
            txtDNNo.Clear()
            dtDNDate.Value = Now
            txtNote.Clear()
            txtReffNo.Enabled = False
            lblSupplier.Text = ""
            lblDocNo.Text = ""
            GridWR.Rows.Clear()
            GridWR.Enabled = False
            lblTotalReceiving.Text = "0 Qty"
            lblTotalItem.Text = 0


        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        state = 0
        DetailClear()
    End Sub

    Private Sub txtDNNo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDNNo.DoubleClick
        Try
            Dim OPD As New OpenFileDialog
            Dim data As New DataTable
            Dim a As Integer = 0
            table = New DataTable
            Dim list As String = ""
            'Navigate to the text file. 
            OPD.Filter = "CSV Files|*.csv|All files (*.*)|*.*"
            OPD.Title = "Select a CSV File"
            OPD.FileName = ""


            If OPD.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                txtDNNo.Text = Mid(Path.GetFileName(OPD.FileName), 1, 14)
                If DNExists(Trim(txtDNNo.Text)) = True Then
                    MsgBox("DN exists!", MsgBoxStyle.Critical, Title)
                    Exit Sub
                End If

                'table = GetImportData(OPD.FileName, "Receiving")

                For i As Integer = 1 To table.Rows.Count - 1
                    If Trim(table.Rows(i).Item(0)) = "" Then GoTo block
                    If Not ItemExists(table.Rows(i).Item(0)) = True Then
                        'MsgBox("Item not exists!", MsgBoxStyle.Exclamation, Title)
                        'Exit Sub

                        list = list & Mid(table.Rows(i).Item(0), 1, 13) & vbCrLf
                        GoTo lanjut
                    End If

                    data = GetDetailItem(table.Rows(i).Item(0))

                    If data.Rows(0).Item("type_status") = 1 Then
                        MsgBox("Item blocked!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If

                    a = a + 1
                    GridWR.Rows.Add(New Object() {a, Mid(table.Rows(i).Item(0), 1, 13), data.Rows(0).Item("type_description"), data.Rows(0).Item("type_product"), data.Rows(0).Item("type_uom"), table.Rows(i).Item(1)})

lanjut:

                Next
block:


                'GridWR.Columns(0).Width = 40
                GridWR.Columns(0).ReadOnly = True
                'GridWR.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'GridWR.Columns(1).Width = 110
                GridWR.Columns(1).ReadOnly = True
                'GridWR.Columns(2).Width = 470
                GridWR.Columns(2).ReadOnly = True
                'GridWR.Columns(3).Width = 50
                GridWR.Columns(3).ReadOnly = True
                'GridWR.Columns(4).Width = 50
                GridWR.Columns(4).ReadOnly = True

                Total()
                If Trim(Text) <> "" Then
                    MsgBox(list, MsgBoxStyle.Exclamation, "Item Not Exists")

                End If

                MsgBox("Import Finish", MsgBoxStyle.Information, Title)

            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub txtReffNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReffNo.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

        If e.KeyChar = ChrW(Keys.Enter) Then
            Dim doc As String = Trim(txtReffNo.Text)

            Dim list As String = ""
            Dim a As Integer = 0
            Dim data As New DataTable

            Try
                'If GridWR.Rows.Count > 0 Then
                '    GridWR.DataSource = Nothing
                'End If

                If GridWR.RowCount > 0 Then
                    GridWR.Rows.Clear()
                End If

                temp = New DataTable

                If mflag = 0 Then

                    If TSExists(doc) = True Then
                        temp = GetTS(doc, 1)

                        If temp.Rows.Count = 0 Then
                            MsgBox("Reff Document TS have been receive!!", MsgBoxStyle.Exclamation, Title)
                            Exit Sub
                        End If

                    Else
                        temp = GetTS(doc, 0)
                    End If



                    If temp.Rows.Count > 0 Then
                        cmbFromWH.SelectedValue = temp.Rows(0).Item(2)
                        cmbToWH.SelectedValue = temp.Rows(0).Item(3)
                        mfromWH = cmbFromWH.SelectedValue

                    Else
                        MsgBox("Reff Document not found!!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If

                Else

                    If Not ValidatePOStatus(doc, IIf(mtransID = "GR101", "PO101", "PO102")) = True Then
                        MsgBox("PO not found!!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If
                    temp = GETPO(doc)
                    If temp.Rows.Count = 0 Then
                        MsgBox("PO have been receive all!!", MsgBoxStyle.Exclamation, Title)
                        Exit Sub
                    End If
                    mSuppCode = temp.Rows(0).Item(2)
                    lblSupplier.Text = temp.Rows(0).Item(3) & " (" & mSuppCode & ")"
                End If


                For i As Integer = 0 To temp.Rows.Count - 1
                    If Trim(temp.Rows(i).Item(0)) = "" Then GoTo block
                    If Not ItemExists(temp.Rows(i).Item(0)) = True Then
                        'MsgBox("Item not exists!", MsgBoxStyle.Exclamation, Title)
                        'Exit Sub

                        list = list & Mid(temp.Rows(i).Item(0), 1, 13) & vbCrLf
                        GoTo lanjut
                    End If

                    data = GetDetailItem(temp.Rows(i).Item(0))

                    If data.Rows.Count > 0 Then
                        If data.Rows(0).Item("type_status") = 1 Then
                            MsgBox("Item blocked!", MsgBoxStyle.Exclamation, Title)
                            Exit Sub
                        End If
                    End If


                    a = a + 1
                    GridWR.Rows.Add(New Object() {a, Mid(temp.Rows(i).Item(0), 1, 13), data.Rows(0).Item("type_description"), data.Rows(0).Item("type_product"), data.Rows(0).Item("type_uom"), CInt(temp.Rows(i).Item(1))})

lanjut:

                Next
block:


                'GridWR.Columns(0).Width = 40
                GridWR.Columns(0).ReadOnly = True
                'GridWR.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'GridWR.Columns(1).Width = 110
                GridWR.Columns(1).ReadOnly = True
                'GridWR.Columns(2).Width = 470
                GridWR.Columns(2).ReadOnly = True
                'GridWR.Columns(3).Width = 50
                GridWR.Columns(3).ReadOnly = True
                'GridWR.Columns(4).Width = 50
                GridWR.Columns(4).ReadOnly = True

                Total()
                If Trim(list) <> "" Then
                    MsgBox(list, MsgBoxStyle.Exclamation, "Item Not Exists")

                End If



                mReffno = txtReffNo.Text
                MsgBox("Import Finish", MsgBoxStyle.Information, Title)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
            End Try


        End If
    End Sub

    Private Sub Total()
        Dim jml As Integer = 0
        For b As Integer = 0 To GridWR.Rows.Count - 1
            jml = jml + GridWR.Rows(b).Cells(5).Value
        Next
        lblTotalReceiving.Text = jml & " Qty"
        lblTotalItem.Text = GridWR.RowCount
    End Sub

    Private Sub FrmWR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        gridAll.Visible = False
    End Sub

    Private Sub FrmWR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            state = 1
            DetailClear()

        ElseIf e.KeyCode = Keys.F5 Then
            SaveTransaction()

        ElseIf e.KeyCode = Keys.Escape Then
            ClosingProgram()

        End If

    End Sub

    Private Sub FrmWR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadWarehouse(cmbFromWH, gridAll, 0)
        LoadWarehouse(cmbToWH, gridAll, 0)
        dtDate.Value = GetValueParamDate("SYSTEM DATE")
        LoadImage()

        If mflag = 0 Then

            lblDNNo.Visible = False
            txtDNNo.Visible = False
            lblDNDate.Visible = False
            dtDNDate.Visible = False
            cmbFromWH.Visible = True
            lblFromWH.Visible = True
            cmbToWH.Visible = True
            lblToWH.Visible = True

            cmbFromWH.SelectedValue = GetValueParamText("DEFAULT WH")
        Else
            lblDNNo.Visible = True
            txtDNNo.Visible = True
            lblDNDate.Visible = True
            dtDNDate.Visible = True
            cmbFromWH.Visible = False
            lblFromWH.Visible = False
            cmbToWH.Visible = False
            lblToWH.Visible = False
        End If

        lblTitle.Text = mWrTitle
        Me.Text = mWrTitle


    End Sub

    Private Sub LoadImage()



        btnNew.Image = mainClass.imgList.ImgBtnNew

        btnBrowse.Image = mainClass.imgList.ImgBtnBrowse
        
        btnEdit.Image = mainClass.imgList.ImgBtnEdit

        btnSave.Image = mainClass.imgList.ImgBtnSave

        btnClose.Image = mainClass.imgList.ImgBtnClosing

        picLabel.Image = mainClass.imgList.ImgLabelPurchase


    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click

        For i As Integer = 0 To temp.Rows.Count - 1
            If GridWR.Rows(GridWR.CurrentRow.Index).Cells(1).Value = Trim(temp.Rows(i).Item(0)) Then
                tempStock = temp.Rows(i).Item(1)
                Exit For
            End If
        Next

        GridWR.Rows(GridWR.CurrentRow.Index).Cells(5).Selected = True
        GridWR.Rows(GridWR.CurrentRow.Index).Cells(5).ReadOnly = False
        GridWR.EditMode = DataGridViewEditMode.EditOnEnter


    End Sub

    Private Sub GridWR_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridWR.CellEndEdit
        If GridWR.Rows(GridWR.CurrentRow.Index).Cells(5).Value > tempStock Then
            MsgBox("Qty over than BM qty!!", MsgBoxStyle.Exclamation, Title)
            GridWR.Rows(GridWR.CurrentRow.Index).Cells(5).Value = tempStock
        End If
        GridWR.Columns(5).ReadOnly = True
        Total()
        tempStock = 0

    End Sub

    Private Sub SaveTransaction()
        If Trim(txtReffNo.Text) = "" Then

            If mflag = 1 Then
                MsgBox("Please Choose PO No!", MsgBoxStyle.Exclamation, Title)
                txtReffNo.Focus()

            Else
                MsgBox("Please Choose TS No!", MsgBoxStyle.Exclamation, Title)
                txtReffNo.Focus()
            End If
            Exit Sub
        End If

        If mflag = 1 Then
            If Trim(txtDNNo.Text) = "" Then
                MsgBox("Please Input Reff Doc!", MsgBoxStyle.Exclamation, Title)
                txtDNNo.Focus()
                Exit Sub
            End If
        End If

        If Trim(txtNote.Text) = "" Then
            MsgBox("Please Input Note!", MsgBoxStyle.Exclamation, Title)
            txtNote.Focus()
            Exit Sub
        End If

        If GridWR.RowCount = 0 Then
            MsgBox("No Detail!", MsgBoxStyle.Exclamation, Title)
            Exit Sub
        End If

        If mReffno <> Trim(txtReffNo.Text) Then
            MsgBox("Detail item not belongs to document TS", MsgBoxStyle.Exclamation, Title)
            Exit Sub
        End If

        Try
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

                For i As Integer = 0 To GridWR.RowCount - 1
                    With temp
                        .Rows.Add(New Object() {GridWR.Rows(i).Cells(1).Value _
                                               , GridWR.Rows(i).Cells(2).Value _
                                               , GridWR.Rows(i).Cells(3).Value _
                                               , GridWR.Rows(i).Cells(4).Value _
                                               , GridWR.Rows(i).Cells(5).Value})

                    End With
                Next


                'save detail
                If mtransID = "GR102" Or mtransID = "GR101" Then
                    hcustSupp = "S"
                    hcustSuppCode = mSuppCode
                    DNNo = Trim(txtDNNo.Text)
                ElseIf mtransID = "TR100" Then
                    hcustSupp = "W"
                    hcustSuppCode = GetValueParamText("DEFAULT BRANCH")
                    DNNo = mReffno
                End If

                ct = Nothing

                If cn.State = ConnectionState.Closed Then cn.Open()

                ct = cn.BeginTransaction("Save Receive")

                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .Transaction = ct
                    For i As Integer = 0 To temp.Rows.Count - 1
                        .CommandText = "INSERT INTO " & DB & ".dbo.twrsd " &
                                        "(dlbm_wrs,dlbm_refftype,dlbm_reffdoc,dlbm_partnumber,dlbm_product,dlbm_uom,dlbm_storeuom," &
                                        "dlbm_entryqty,dlbm_stockqty,dlbm_payqty,dlbm_binqty,dlbm_cost,dlbm_batcno,dlbm_description," &
                                        "dlbm_costcenter,dlbm_freightcost,dlbm_costunit,dlbm_exchrate) " &
                                        "VALUES ('" & Trim(lblDocNo.Text) & "','PO','" & mReffno & "','" & temp.Rows(i).Item(0) & "','" & temp.Rows(i).Item(2) & "'" &
                                        ",'" & temp.Rows(i).Item(3) & "','" & temp.Rows(i).Item(3) & "','" & temp.Rows(i).Item(4) & "'" &
                                        ",'" & temp.Rows(i).Item(4) & "',0,'" & temp.Rows(i).Item(4) & "',0,'','" & Replace(temp.Rows(i).Item(1), "'", "''") & "'" &
                                        ",'',0,0,1)"
                        .ExecuteNonQuery()

                    Next

                End With


                'save header
                If cn.State = ConnectionState.Closed Then cn.Open()
                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .Transaction = ct
                    .CommandText = "INSERT INTO " & DB & ".dbo.twrsh " &
                                    "(hlbm_company,hlbm_branch,hlbm_wrs,hlbm_date,hlbm_dc,hlbm_wh,hlbm_trnid,hlbm_type," &
                                    "hlbm_supplier,hlbm_customer,hlbm_fwh,hlbm_dn,hlbm_dndate,hlbm_note,hlbm_flag_aloc," &
                                    "hlbm_flag_rls,hlbm_flag_posting,hlbm_flag_validate,hlbm_journal,hlbm_creator,hlbm_createdate," &
                                    "hlbm_createtime,hlbm_last_modifier,hlbm_modifytime) " &
                                    "VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "'," &
                                    "'" & GetValueParamText("DEFAULT BRANCH") & "','" & Trim(lblDocNo.Text) & "'," &
                                    "'" & Format(dtDate.Value, formatDate) & "'" &
                                    ",'" & GetValueParamText("DEFAULT BRANCH") & "'," &
                                    "'" & GetValueParamText("DEFAULT WH") & "','" & mtransID & "','" & IIf(mflag = 0, 5, 1) & "','" & mSuppCode & "'" &
                                    ",'','" & IIf(mflag = 0, mfromWH, "") & "','" & DNNo & "','" & Format(dtDNDate.Value, formatDate) & "','" & Trim(txtNote.Text) & "','Y','Y','N','N','101','" & logOn & "'" &
                                    ",'" & Format(dtDate.Value, formatDate) & "','" & Format(Now, "HHmmss") & "'" &
                                    ",'','')"
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

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If MsgBox("Are you sure Remove Item " & GridWR.Rows(GridWR.CurrentRow.Index).Cells(1).Value & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, Title) = MsgBoxResult.No Then Exit Sub
        GridWR.Rows.RemoveAt(GridWR.CurrentRow.Index)
        Total()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ClosingProgram()
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

    Private Sub btnNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            state = 1
            DetailClear()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub gridAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAll.DoubleClick


        Select Case gridAll.Tag
            Case "FROM WH"
                cmbFromWH.SelectedValue = gridAll.SelectedCells(0).Value

            Case Else
                cmbToWH.SelectedValue = gridAll.SelectedCells(0).Value
        End Select

        gridAll.Visible = False
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click

    End Sub
End Class