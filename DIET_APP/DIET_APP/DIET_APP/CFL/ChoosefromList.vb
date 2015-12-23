Imports System.Windows.Forms

Public Class frmCFL

#Region "Declaration"
    Dim oDt As New DataTable
    Public oRowNum As Integer
    Public oCurrentForm As String
    Public oCurrentItem As String
    Public oFormHeaderText As String
    Public oCurrentStr As String
    Dim oDr As SqlClient.SqlDataReader
#End Region

#Region "Events"

    Private Sub itemCfl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            UXUTIL.clsUtilities.setAllControlsThemes(Me)
            If oCurrentForm = "PSWIZARD" Then
                Select Case oCurrentItem
                    Case "dgv_BF_C", "dgv_LN_C", "dgv_LS_C", "dgv_SK_C", "dgv_DI_C", "dgv_DS_C"
                        oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany().CompanyDB, getQueryBasedonFuntionality("Item"))
                    Case "txtFCustomer", "txtTCustomer"
                        oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany().CompanyDB, getQueryBasedonFuntionality("Customer"))
                    Case "txtFCGroup", "txtTCGroup"
                        oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany().CompanyDB, getQueryBasedonFuntionality("CGroup"))
                    Case "txtFProgram", "txtTProgram"
                        oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany().CompanyDB, getQueryBasedonFuntionality("Program"))
                    Case "txtFIGroup", "txtTIGroup"
                        oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany().CompanyDB, getQueryBasedonFuntionality("IGroup"))
                End Select
            End If
            oDt.Load(oDr)
            oDr.Close()
            RowMatrix.AutoGenerateColumns = False
            RowMatrix.DataSource = oDt
            cflFind.Clear()
            cflFind.Focus()
            cflFind.Select()
        Catch ex As Exception

        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RowMatrix_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RowMatrix.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If RowMatrix.Rows.Count > 0 Then
                    Dim intRowIndex As Integer = DirectCast(sender, System.Windows.Forms.DataGridView).CurrentCell.RowIndex
                    Dim intColumnIndex As Integer = DirectCast(sender, System.Windows.Forms.DataGridView).CurrentCell.ColumnIndex
                    If intRowIndex > -1 And intColumnIndex > -1 Then
                        If oCurrentForm = "PSWIZARD" Then
                            Select Case oCurrentItem
                                Case "dgv_BF_C", "dgv_LN_C", "dgv_LS_C", "dgv_SK_C", "dgv_DI_C", "dgv_DS_C", "txtFCustomer", "txtTCustomer", "txtFCGroup", "txtTCGroup", "txtFProgram", "txtTProgram", "txtFIGroup", "txtTIGroup"
                                    PSWizard.FillSelection(oCurrentItem, RowMatrix.SelectedRows.Item(0).Cells.Item("VCode").Value.ToString(), RowMatrix.SelectedRows.Item(0).Cells.Item("CName").Value.ToString())
                            End Select
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cflFind_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cflFind.TextChanged
        Try
            oDt.DefaultView.RowFilter = "VName like '%" & cflFind.Text.Trim().ToString & "%' "
        Catch ex As Exception
            cflFind.Text = ""
        End Try
    End Sub

    Private Sub itemCfl_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            oCurrentForm = String.Empty
            oCurrentItem = String.Empty
            oCurrentStr = String.Empty
            cflFind.Text = String.Empty
            oDt.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RowMatrix_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles RowMatrix.CellDoubleClick
        Try
            If RowMatrix.Rows.Count > 0 Then
                If e.RowIndex > -1 And e.ColumnIndex > -1 Then
                    FillParentForm()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cflFind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cflFind.KeyDown
        Try
            If e.KeyData = Keys.Enter Then
                FillParentForm()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub itemCfl_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If RowMatrix.Rows.Count > 0 Then
                If e.KeyCode = Keys.Up Then
                    If RowMatrix.SelectedRows.Count > 0 Then
                        If RowMatrix.SelectedRows(0).Index - 1 >= 0 Then
                            RowMatrix.Rows(RowMatrix.SelectedRows(0).Index - 1).Selected = True
                            RowMatrix.FirstDisplayedScrollingRowIndex = RowMatrix.SelectedRows(0).Index
                        End If
                    End If
                ElseIf e.KeyCode = Keys.Down Then
                    If RowMatrix.SelectedRows.Count > 0 Then
                        If RowMatrix.SelectedRows(0).Index + 1 < RowMatrix.Rows.Count Then
                            RowMatrix.Rows(RowMatrix.SelectedRows(0).Index + 1).Selected = True
                            RowMatrix.FirstDisplayedScrollingRowIndex = RowMatrix.SelectedRows(0).Index
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Functions"

    Private Sub FillParentForm()
        Try
            If RowMatrix.SelectedRows.Count > 0 Then
                If oCurrentForm = "PSWIZARD" Then
                    Select Case oCurrentItem
                        Case "dgv_BF_C", "dgv_LN_C", "dgv_LS_C", "dgv_SK_C", "dgv_DI_C", "dgv_DS_C", "txtFCustomer", "txtTCustomer", "txtFCGroup", "txtTCGroup", "txtFProgram", "txtTProgram", "txtFIGroup", "txtTIGroup"
                            PSWizard.FillSelection(oCurrentItem, RowMatrix.SelectedRows.Item(0).Cells.Item("VCode").Value.ToString(), RowMatrix.SelectedRows.Item(0).Cells.Item("VName").Value.ToString())
                    End Select
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function getQueryBasedonFuntionality(ByVal strType As String) As String
        Dim _retVal As String = String.Empty
        Try
            Select Case strType
                Case "Customer"
                    _retVal = "Select C.CardCode As 'VCode',C.CardName As 'VName' From OCRD C "
                    _retVal &= " INNER JOIN "
                    _retVal &= " (SELECT U_Prefix "
                    _retVal &= " FROM [@Z_OFCI] Where U_Type = 'C' And U_Active = 'Y' "
                    _retVal &= " GROUP BY U_Prefix) P "
                    _retVal &= " ON C.CardCode LIKE (P.U_Prefix + '%') "
                    _retVal &= " Where 1 = 1 "
                    _retVal &= " And C.CardType = 'C' "
                    _retVal &= " And C.validFor = 'Y' "
                Case "Item"
                    _retVal = "Select I.ItemCode As 'VCode',I.ItemName As 'VName' From OITM I "
                    _retVal &= " INNER JOIN "
                    _retVal &= " (SELECT U_Prefix "
                    _retVal &= " FROM [@Z_OFCI] Where U_Type = 'I' And U_Active = 'Y' "
                    _retVal &= " GROUP BY U_Prefix) P "
                    _retVal &= " ON I.ItemCode LIKE (P.U_Prefix + '%') "
                    _retVal &= " Where 1 = 1 "
                    _retVal &= " And I.InvntItem = 'Y' "
                    _retVal &= " And I.SellItem = 'Y' "
                    _retVal &= " And I.validFor = 'Y' "
                    _retVal &= " And I.U_ISFOOD = 'Y' "
                Case "CGroup"
                    _retVal = "Select G.GroupCode As 'VCode',G.GroupName As 'VName' From OCRG G "
                    _retVal &= " Where 1 = 1 "
                Case "Program"
                    _retVal = "Select I.ItemCode As 'VCode',I.ItemName As 'VName' From OITM I "
                    _retVal &= " INNER JOIN "
                    _retVal &= " OITB G "
                    _retVal &= " ON I.ItmsGrpCod = G.ItmsGrpCod "
                    _retVal &= " And G.U_Program = 'Y' "
                Case "IGroup"
                    _retVal = "Select G.ItmsGrpCod As 'VCode',G.ItmsGrpNam As 'VName' From OITB G "
                    _retVal &= " Where 1 = 1 "
                    _retVal &= " And G.U_Program = 'Y' "
            End Select
        Catch ex As Exception
            Throw ex
        End Try
        Return _retVal
    End Function

#End Region

End Class
