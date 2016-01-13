Imports genLib.General
Imports connLib.DBConnection
Imports sqlLib.Sql
Imports proLib.Process
Imports System.Data.SqlClient

Public Class frmEvent

    Private mTitle As String
    Private mEventNo As String
    Private mState As Integer

    Private hDiscType As Integer

    Public WriteOnly Property EventTitle As String
        Set(ByVal value As String)
            mTitle = value
        End Set
    End Property

    Public WriteOnly Property EventNo As String
        Set(value As String)
            mEventNo = value
        End Set
    End Property

    Public WriteOnly Property EventState As Integer
        Set(value As Integer)
            mState = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmEvent_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F1 Then
            AddDetail()

        End If
    End Sub

    Private Sub frmEvent_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Text = mTitle

            If mState = 1 Then
                LoadEventDetail()
            End If

            lblDocNo.Text = mEventNo
            cmbFilter.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try


    End Sub

    Private Sub LoadEventDetail()

    End Sub

    Private Sub cmbDiscType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDiscType.SelectedIndexChanged
        If cmbDiscType.SelectedIndex = 1 Or cmbDiscType.SelectedIndex = 2 Then
            cmbTransType.SelectedIndex = 1
            cmbAssignTo.SelectedIndex = 2

        Else
            cmbTransType.SelectedIndex = 0
            cmbAssignTo.SelectedIndex = 1
        End If

        If cmbDiscType.SelectedIndex = 0 Then
            hDiscType = 1
        ElseIf cmbDiscType.SelectedIndex = 1 Then
            hDiscType = 10
        Else
            hDiscType = 20
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try

            If Trim(txtEventName.Text) = "" Then
                MsgBox("Please input event name promo", MsgBoxStyle.Information, Title)
                txtEventName.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Trim(txtNote.Text) = "" Then
                MsgBox("Please input event note promo", MsgBoxStyle.Information, Title)
                txtNote.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Trim(cmbDiscType.Text) = "" Then
                MsgBox("Please select disc type promo", MsgBoxStyle.Information, Title)
                cmbDiscType.Focus()
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If Not GridEvent.Rows.Count > 0 Then
                MsgBox("No detail", MsgBoxStyle.Information, Title)
                DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            SaveDetailPromo(mState)
            SaveHeaderPromo(mState)

            UpdateHistoryPOS(lblDocNo.Text, "MP")
        Catch ex As Exception
            DialogResult = Windows.Forms.DialogResult.None
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try

    End Sub

    Private Sub SaveDetailPromo(state As Integer)
        Try
            Dim counter As Integer = 0


            If cn.State = ConnectionState.Closed Then cn.Open()
            counter += 1
            If state = 0 Then
                For i As Integer = 0 To GridEvent.RowCount - 1
                    cm = New SqlCommand

                    With cm
                        .Connection = cn


                        .CommandText = "INSERT INTO " & DB & ".dbo.mdiscd (promoid,item,prodhier,partnumber,disc,createuser,createdate,description)" & _
                                                                            " VALUES ('" & lblDocNo.Text & "','" & GridEvent.Rows(i).Cells(0).Value & "','" & cmbFilter.SelectedIndex + 1 & "'," & _
                                                                            "'" & GridEvent.Rows(i).Cells(1).Value & "','" & GridEvent.Rows(i).Cells(3).Value & "'," & _
                                                                            "'" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "','" & GridEvent.Rows(i).Cells(2).Value & "')"

                        .ExecuteNonQuery()
                    End With
                Next
            Else

                cm = New SqlCommand
                With cm
                    .Connection = cn
                    .CommandText = "DELETE FROM " & DB & ".dbo.mdiscd WHERE promoid='" & lblDocNo.Text & "'"
                    .ExecuteNonQuery()
                End With

                For i As Integer = 0 To GridEvent.RowCount - 1
                    counter += 1

                    cm = New SqlCommand
                    With cm
                        .Connection = cn
                        .CommandText = "INSERT INTO " & DB & ".dbo.mdiscd (promoid,item,prodhier,partnumber,disc,createuser,createdate,description)" & _
                                                                           " VALUES ('" & lblDocNo.Text & "','" & GridEvent.Rows(i).Cells(0).Value & "','" & cmbFilter.SelectedIndex + 1 & "'," & _
                                                                           "'" & GridEvent.Rows(i).Cells(1).Value & "','" & GridEvent.Rows(i).Cells(3).Value & "'," & _
                                                                           "'" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "','" & GridEvent.Rows(i).Cells(2).Value & "')"

                        .ExecuteNonQuery()
                    End With
                Next

            End If




            cn.Close()


        Catch ex As Exception
            cm = New SqlCommand

            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.mdiscd WHERE promoid='" & lblDocNo.Text & "'"
                .ExecuteNonQuery()
            End With
           
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub SaveHeaderPromo(state As Integer)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()

            cm = New SqlCommand
            With cm
                .Connection = cn

                If state = 0 Then
                    .CommandText = "INSERT INTO " & DB & ".dbo.mdisch (company,salesorg,salesoffice,promoid,description,note," & _
                                                               "disctype,trntype,assignto,periodfrom,periodto,materialtype,prodhier,validflag," & _
                                                               "closeflag,autogenerate,minmargin,maxdisc,maxitem,createuser,createdate,minpayment)" & _
                                                               " VALUES ('" & GetValueParamText("DEFAULT COMPANY") & "','" & frmMasterEvent.cmbSalesOrg.SelectedValue & "'," & _
                                                               "'" & GetValueParamText("POS SALESOFFICE") & "','" & lblDocNo.Text & "','" & txtEventName.Text & "'," & _
                                                               "'" & txtNote.Text & "','" & hDiscType & "','" & cmbTransType.SelectedIndex & "','" & cmbAssignTo.SelectedItem & "'," & _
                                                               "'" & Format(dtPeriodFrom.Value, formatDate) & "','" & Format(dtPeriodTo.Value, formatDate) & "','001'," & _
                                                               "'" & cmbFilter.SelectedIndex + 1 & "','N','N','" & Mid(cmbAutoGenerate.SelectedItem, 1, 1) & "',0,0,0,'" & logOn & "','" & Format(GetValueParamDate("SYSTEM DATE"), formatDate) & "','" & CDec(txtMinPayment.Text) & "')"
                Else

                End If

                .ExecuteNonQuery()
            End With

            cn.Close()

        Catch ex As Exception
            With cm
                .Connection = cn
                .CommandText = "DELETE FROM " & DB & ".dbo.mdiscd WHERE promoid='" & lblDocNo.Text & "'"
                .ExecuteNonQuery()
            End With
            
            cn.Close()
            Throw ex
        End Try
    End Sub

    Private Sub txtEventName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtEventName.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub


    Private Sub txtNote_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNote.KeyPress
        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        AddDetail()
    End Sub

    Private Sub AddDetail()

        If cmbFilter.SelectedIndex <> 0 Or cmbFilter.SelectedIndex <> 8 Then
            If Not Trim(cmbItem.Text) = "" Then

                If IsExists(cmbItem.SelectedValue) Then
                    MsgBox("Item is Exists!!!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If


                If cmbFilter.SelectedIndex = 8 Then
                    table = New DataTable
                    table = GETDetailMember(Trim(cmbItem.SelectedValue))

                    GridEvent.Rows.Add( _
                      New Object() {GetLastSeqnum(), cmbItem.SelectedValue, _
                                    CType(cmbItem.SelectedItem, DataRowView).Item(1), 0})
                Else
                    GridEvent.Rows.Add( _
                      New Object() {GetLastSeqnum(), cmbItem.SelectedValue, _
                                    CType(cmbItem.SelectedItem, DataRowView).Item(1), 0})
                End If
              

            End If

        Else

            If txtItem.Text.Length > 0 Then

                If IsExists(txtItem.Text) Then
                    MsgBox("Item is Exists!!!", MsgBoxStyle.Exclamation, Title)
                    Exit Sub
                End If

                table = New DataTable

                table = GetDetailItem(txtItem.Text)

                GridEvent.Rows.Add( _
                                   New Object() {GetLastSeqnum(), Trim(txtItem.Text), table.Rows(0).Item(1), 0})
                txtItem.Clear()


            End If

        End If

    End Sub

    Private Function IsExists(item As String) As Boolean
        For i As Integer = 0 To GridEvent.Rows.Count - 1
            If GridEvent.Rows(i).Cells(1).Value = item Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Function GetLastSeqnum() As Integer
        Dim iden As Integer = 0
        If GridEvent.Rows.Count > 0 Then
            For i As Integer = 0 To GridEvent.Rows.Count - 1
                iden = GridEvent.Rows(i).Cells(0).Value
            Next

            iden = iden + 1
        Else
            iden = 1
        End If

        Return iden
    End Function

    Private Sub ColumnAllReadonly()
        For i As Integer = 0 To GridEvent.Columns.Count - 1
            GridEvent.Columns(i).ReadOnly = True
        Next
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFilter.SelectedIndexChanged
        If cmbFilter.SelectedIndex <> 0 Then
            cmbItem.Visible = True
            txtItem.Visible = False
        Else
            cmbItem.Visible = False
            txtItem.Visible = True
        End If
    End Sub

    Private Sub txtItem_DoubleClick(sender As Object, e As System.EventArgs) Handles txtItem.DoubleClick
        If frmListItem.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtItem.Text = frmListItem.GridListItem.SelectedCells(0).Value

            frmListItem.Close()
        End If
    End Sub

    Private Sub txtItem_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        If e.KeyCode = Keys.Enter Then
            AddDetail()

        End If
    End Sub

    Private Sub cmbItem_Click(sender As Object, e As System.EventArgs) Handles cmbItem.Click

        Select Case cmbFilter.SelectedIndex
            Case 1 'Disc Group
                LoadDiscGroup(cmbItem, gridAll, 0)
            Case 2 'Products
                LoadProducts(cmbItem, gridAll, 0)
            Case 3 'Prodhier1
                LoadProdhier1(cmbItem, gridAll, "", 0)

            Case 4 'Prodhier2
                LoadProdhier2(cmbItem, gridAll, 0)

            Case 5 'Prodhier3
                LoadProdhier3(cmbItem, gridAll, 0)
            Case 6 'Prodhier4
                LoadProdhier4(cmbItem, gridAll, 0)

            Case 7 'Prodhier5
                LoadProdhier5(cmbItem, gridAll, 0)
            Case Else
                LoadMember(cmbItem, gridAll, 0)


        End Select
        If cmbFilter.SelectedIndex = 1 Then 'Disc Group

        ElseIf cmbFilter.SelectedIndex = 2 Then


        End If

        Try

            If cmbFilter.SelectedIndex = 4 Then
                gridAll.Location = New Point(cmbItem.Left, cmbItem.Location.Y - 120)

            ElseIf cmbFilter.SelectedIndex = 8 Then
                gridAll.Location = New Point(cmbItem.Left, cmbItem.Location.Y - 160)

            Else
                gridAll.Location = New Point(cmbItem.Left, cmbItem.Location.Y - 150)
            End If

            gridAll.Size = New Point(GetColumnWidth(gridAll.Columns.Count, gridAll) + _
                            (cmbItem.Width - GetColumnWidth(gridAll.Columns.Count, gridAll)) + 80, GetRowHeight(gridAll.Rows.Count, gridAll))
            cmbItem.DroppedDown = False


            If gridAll.Visible = True Then
                gridAll.Visible = False
            Else
                If gridAll.RowCount > 0 Then gridAll.Visible = True
            End If

            If cmbFilter.SelectedIndex = 8 Then
                gridAll.Columns(0).Width = 70
                gridAll.Columns(1).Width = gridAll.Width - 80
            Else
                gridAll.Columns(0).Width = 50
                gridAll.Columns(1).Width = gridAll.Width - 54
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Title)
        End Try
    End Sub

    Private Sub gridAll_DoubleClick(sender As Object, e As System.EventArgs) Handles gridAll.DoubleClick
        cmbItem.SelectedValue = gridAll.SelectedCells(0).Value
        AddDetail()
        gridAll.Visible = False
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure Remove Item " & GridEvent.Rows(GridEvent.CurrentRow.Index).Cells(1).Value & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, Title) = MsgBoxResult.No Then Exit Sub
        GridEvent.Rows.RemoveAt(GridEvent.CurrentRow.Index)
    End Sub
End Class