Imports sqlLib.Sql
Imports genLib.General

Public Class frmStockCard

    Private mItem As String
    Private mJudul As String
    Private mWHKode As String
    Private mWHName As String


    Public WriteOnly Property ItemCode As String
        Set(ByVal value As String)
            mItem = value
        End Set
    End Property

    Public WriteOnly Property Description As String
        Set(ByVal value As String)
            mJudul = value
        End Set
    End Property

    Public WriteOnly Property WHCode As String
        Set(ByVal value As String)
            mWHKode = value
        End Set
    End Property

    Public WriteOnly Property WHName As String
        Set(ByVal value As String)
            mWHName = value
        End Set
    End Property

    Private Sub frmStockCard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblItem.Text = mItem
        lblDescription.Text = mJudul
        lblWarehouse.Text = mWHName

        LoadStockCard(Format(Now, "yyyy") & "-" & Format(Now, "MM") & "-01")
        lblPeriod.Text = "01-" & Format(Now, "MMM") & "-" & Format(Now, "yyyy") & "   " & Format(Now, "dd-MMM-yyyy")
        chckAllPeriod.Checked = False
    End Sub

    Private Sub LoadStockCard(ByVal fperiod As Date)
        Try
            Dim temp As New DataTable

            temp = GETStockCard(Trim(mItem), mWHKode, fperiod, Now)

            If temp.Rows.Count > 0 Then
                With GridStockCard
                    .AutoGenerateColumns = False
                    .Columns(0).DataPropertyName = "Iden"
                    .Columns(1).DataPropertyName = "Date"
                    .Columns(2).DataPropertyName = "DocumentNo"
                    .Columns(3).DataPropertyName = "dn"
                    .Columns(4).DataPropertyName = "Transid"
                    .Columns(5).DataPropertyName = "TrnType"
                    .Columns(6).DataPropertyName = "Conterpart"
                    .Columns(7).DataPropertyName = "CounterpartName"
                    .Columns(8).DataPropertyName = "Qty"
                    .Columns(9).DataPropertyName = "Saldo"

                End With

                GridStockCard.DataSource = temp

            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub chckAllPeriod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckAllPeriod.CheckedChanged

        If chckAllPeriod.Checked = True Then
            LoadStockCard("2012-06-01")
            lblPeriod.Text = "...   " & Format(Now, "dd-MMM-yyyy")

        Else
            LoadStockCard(Format(Now, "yyyy") & "-" & Format(Now, "MM") & "-01")
            lblPeriod.Text = "01-" & Format(Now, "MMM") & "-" & Format(Now, "yyyy") & "   " & Format(Now, "dd-MMM-yyyy")

        End If


    End Sub

End Class