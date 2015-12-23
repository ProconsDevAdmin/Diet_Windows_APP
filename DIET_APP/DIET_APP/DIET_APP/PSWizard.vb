'Login Form. - 8 Hours
'Screen Design,Logic.
'MDI Form. - 8 Hours
'Screen Design,Logic.
'MDI Menu Design. - 8 Hours
'Screen Design,Logic.
'Missed Client Form. - 8 Hours
'Screen Design,Logic.
'Row Selection - Cancel if Something selected.
'Remove Tab Page Logic. - 1 Hours
'Initialize Function - 1 Hours 
'Header Details(CardCode,Program,Program From & To Date etc,No of Days). - 2 Hours
'Load Program Date. - 1 Hours 
'Load Foods Based on Program Dates. - 1 Hours 
'Fill Customize Items. - 1 Hours 
'Fill Dislike & Medical Logic. - 1 - Hours

'Save Temp DataTable. - 1 Hours. 
'Generate Food Save Logic,PreSale Order Document & Sale Order Creation Logic. - 2 Hours .
'Images for Buttons.
'Validation Logic.
'Testing.
'SetUp & Delivery.

Imports SAPbobsCOM

Public Class PSWizard

#Region "Declaration"
    Dim oRowPoint_C As Integer
    Dim oRowPoint As Integer
    Dim oDt_Customers As DataTable
    Dim oDt_Programs As DataTable
    Dim oDt_BF_R As DataTable
    Dim oDt_BF_C As DataTable
    Dim oDt_Lunch_R As DataTable
    Dim oDt_Lunch_C As DataTable
    Dim oDt_LunchS_R As DataTable
    Dim oDt_LunchS_C As DataTable
    Dim oDt_Snack_R As DataTable
    Dim oDt_Snack_C As DataTable
    Dim oDt_Dinner_R As DataTable
    Dim oDt_Dinner_C As DataTable
    Dim oDt_DinnerS_R As DataTable
    Dim oDt_DinnerS_C As DataTable
    Dim oDt_ProgramDates As DataTable
    Dim oDt_Summary As DataTable
    Dim strQuery As String = String.Empty
    Dim strdtFormat As String = "yyyyMMdd"
    Dim oDr As SqlClient.SqlDataReader
    Dim oRecordSet As SAPbobsCOM.Recordset
    Dim oCompany As SAPbobsCOM.Company
    Dim oGridRow As Integer
#End Region

#Region "Events"

    Private Sub PSWizard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            UXUTIL.clsUtilities.setAllControlsThemes(Me)
            initialize()
            sc_Missed_Clients_0.Enabled = False
            scFoodWizard.Visible = False
            sc_Missed_Clients_0.Dock = DockStyle.Fill

            'lblCustomer_T.Text = "CR0203-DHARMARAJ"
            'lblProgram_T.Text = "73-PR0001-Regular"
            'dtpFrmDate.Value = System.DateTime.Now.AddMonths(-1)
            'dtpToDate.Value = System.DateTime.Now
            'lblNoofDays_T.Text = "2"
            'lblRemNoofDays_T.Text = "2"

            'oCompany = Singleton.SapCompany
            'tcFood.TabPages.Remove(tpSnack)
            'fillProgramDate()

            'scWizard0.Panel1Collapsed = True
            'sc_FoodSelection.Visible = False
            'sc_Missed_Clients_0.Visible = False

            '
            'scFoodWizard.Dock = DockStyle.Fill
            'sc_Summary.Dock = DockStyle.Fill

            'fillCustomers()
            'fillSummary()

        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_BF_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_BF_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_BF_C.Columns.Item(e.ColumnIndex).Name = "ItemName_BF_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgvProgramDate_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles dgvProgramDate.SelectionChanged
        Try
            dtpSelectedDate.Value = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("PDate").Value
            txt_Program_ID.Text = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("Program_ID").Value
            txt_Program.Text = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("Program").Value
            fillMenuBasedOnDate()
            addremoveSession(txt_Program.Text)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgvProgramDate_Click(sender As System.Object, e As System.EventArgs) Handles dgvProgramDate.Click
        Try
            If dgvProgramDate.SelectedRows.Count > 0 Then
                dtpSelectedDate.Value = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("PDate").Value
                txt_Program_ID.Text = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("Program_ID").Value
                txt_Program.Text = dgvProgramDate.Rows(IIf(dgvProgramDate.SelectedRows(0).Index = 0, 0, dgvProgramDate.SelectedRows(0).Index)).Cells("Program").Value
                fillMenuBasedOnDate()
                addremoveSession(txt_Program.Text)
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgvBF_D_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgv_BF_R.DataError
        Try

        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_BF_C_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgv_BF_C.DataError
        Try

        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_Lunch_C_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgv_LN_C.DataError

    End Sub

    Private Sub dgv_Lunch_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_LN_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_LN_C.Columns.Item(e.ColumnIndex).Name = "ItemName_LN_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_LS_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_LS_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_LS_C.Columns.Item(e.ColumnIndex).Name = "ItemName_LS_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_SK_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_SK_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_SK_C.Columns.Item(e.ColumnIndex).Name = "ItemName_SK_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_DN_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_DI_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_DI_C.Columns.Item(e.ColumnIndex).Name = "ItemName_DI_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_DS_C_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_DS_C.CellDoubleClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_DS_C.Columns.Item(e.ColumnIndex).Name = "ItemName_DS_C" Then
                    oRowPoint = e.RowIndex
                    frmCFL.oCurrentForm = "PSWIZARD"
                    frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                    frmCFL.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFCustomer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFCustomer.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFCustomer_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtFCustomer.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTCustomer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTCustomer.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTCustomer_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtTCustomer.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFCGroup_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFCGroup.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFCGroup_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtFCGroup.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTCGroup_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTCGroup.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub


    Private Sub txtTCGroup_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtTCGroup.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFProgram_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtFProgram.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTProgram_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtTProgram.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFIGroup_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtFIGroup.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTIGroup_DoubleClick(sender As System.Object, e As System.EventArgs) Handles txtTIGroup.DoubleClick
        Try
            showCFL(sender)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFProgram_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFProgram.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTProgram_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTProgram.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFIGroup_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFIGroup.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtTIGroup_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTIGroup.KeyDown
        Try
            popUpCfl(sender, e)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub Run_Click(sender As System.Object, e As System.EventArgs) Handles Run.Click
        Try
            fillCustomers()
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles btnBack_MC.Click
        Try
            scWizard0.Panel1Collapsed = False
            sc_Missed_Clients_0.Enabled = False
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub btnNext_MC_Click(sender As System.Object, e As System.EventArgs) Handles btnNext_MC.Click
        Try
            If validate_ClientSelection() Then
                If Validation_Customer(getCustomerCode) Then
                    clearFoodDataTable()
                    sc_Missed_Clients_0.Visible = False
                    fillProgram()
                    fillProgramDate()
                    txtInstance.Text = System.DateTime.Now.ToString("yyyyMMddhhmmss")
                    scFoodWizard.Visible = True
                    scFoodWizard.Dock = DockStyle.Fill
                Else
                    MainForm.ErrorMsg.StatusBarMsg("Calories Or Address Not defined For Selected Customer...", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
                End If
            Else
                MainForm.ErrorMsg.StatusBarMsg("No Customer Selected...", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub btn_Back_FS_Click(sender As System.Object, e As System.EventArgs) Handles btn_Back_FS.Click
        Try
            scFoodWizard.Visible = False
            sc_Missed_Clients_0.Visible = True
            sc_Missed_Clients_0.Dock = DockStyle.Fill
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub btnAdd_FS_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_FS.Click
        Try
            UpdateCustomerFoodMenu(dgv_BF_R, "BF", "R") 'Break Fast - Regular
            UpdateCustomerFoodMenu(dgv_BF_C, "BF", "C") 'Break Fast Custom
            UpdateCustomerFoodMenu(dgv_LN_R, "LN", "R") 'Lunch- Regular
            UpdateCustomerFoodMenu(dgv_LN_C, "LN", "C") 'Lunch Custom
            UpdateCustomerFoodMenu(dgv_LS_R, "LS", "R") 'Lunch Side- Regular
            UpdateCustomerFoodMenu(dgv_LS_C, "LS", "C") 'Lunch Side Custom
            UpdateCustomerFoodMenu(dgv_SK_R, "SK", "R") 'Snack - Regular
            UpdateCustomerFoodMenu(dgv_SK_C, "SK", "C") 'Snack Custom
            UpdateCustomerFoodMenu(dgv_DI_R, "DI", "R") 'Dinner- Regular
            UpdateCustomerFoodMenu(dgv_DI_C, "DI", "C") 'Dinner Custom
            UpdateCustomerFoodMenu(dgv_DS_R, "DS", "R") 'Dinner Side- Regular
            UpdateCustomerFoodMenu(dgv_DS_C, "DS", "C") 'Dinner Side Custom
            MsgBoxNew.Shows("Food Saved Successfully...", "Food Success", MsgBoxNew.Buttons.OKCancel, MsgBoxNew.Icons.Info)
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub btn_Cancel_FS_Click(sender As System.Object, e As System.EventArgs) Handles btn_Cancel_FS.Click
        Me.Close()
    End Sub

    Private Sub btn_Generate_FS_Click(sender As System.Object, e As System.EventArgs) Handles btn_Generate_FS.Click
        Try
            Singleton.getSAPCompany.StartTransaction()

            For intRow = 0 To dgv_Programs.RowCount - 1
                Dim strCardCode As String = txtCardCode.Text
                Dim strCardName As String = getRecordSetValueString("Select CardName From OCRD Where CardCode = '" & txtCardCode.Text & "'", "CardName")
                Dim strProgram_ID As String = dgv_Programs.Rows.Item(intRow).Cells("PProgram_ID").Value
                Dim strIsCons As String = IIf(IsDBNull(dgv_Programs.Rows.Item(intRow).Cells("IsCons").Value), False, dgv_Programs.Rows.Item(intRow).Cells("IsCons").Value)
                Dim strRemDays As String = dgv_Programs.Rows.Item(intRow).Cells("Program_RD").Value
                Dim oDT_Food As New DataTable
                strQuery = " Select Distinct U_ProgramID,Min(U_PrgDate) As 'MD',Max(U_PrgDate) As 'XD' "
                strQuery += " ,(Select Count(T0.U_PrgDate) From "
                strQuery += " ( "
                strQuery += " (Select Distinct U_PrgDate From [@Z_OFSL] "
                strQuery += " Where U_ProgramID = '" & strProgram_ID & "'"
                strQuery += " And U_Session = '" & txtInstance.Text & "' )"
                strQuery += " ) T0)  As 'NoDays' "
                strQuery += " From [@Z_OFSL] "
                strQuery += " Group By U_ProgramID,U_Session "
                strQuery += " Having U_ProgramID = '" & strProgram_ID & "'"
                strQuery += " And U_Session = '" & txtInstance.Text & "' "
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDT_Food.Load(oDr)
                oDr.Close()
                If Not IsNothing(oDT_Food) Then
                    If oDT_Food.Rows.Count > 0 Then
                        Dim strPSRef As String = String.Empty
                        If (AddPreSalesOrder(strCardCode, strCardName, txt_Program.Text, strProgram_ID _
                                              , oDT_Food.Rows(0)("MD").ToString _
                                              , oDT_Food.Rows(0)("XD").ToString _
                                              , oDT_Food.Rows(0)("NoDays").ToString _
                                              , strRemDays _
                                        , strIsCons, strPSRef)) Then
                            AddOrder(strPSRef)
                        End If
                    Else
                        Continue For
                    End If
                End If
            Next
            Singleton.getSAPCompany.EndTransaction(BoWfTransOpt.wf_Commit)
            For index = 0 To dgv_MissedClients.RowCount - 1
                If index = oRowPoint_C Then
                    dgv_MissedClients.Item("IsCreated_MC", index).Value = "Yes"
                    dgv_MissedClients.Rows(index).ReadOnly = True
                    scFoodWizard.Visible = False
                    txtInstance.Text = String.Empty
                    sc_Missed_Clients_0.Visible = True
                    sc_Missed_Clients_0.Dock = DockStyle.Fill
                End If
            Next
        Catch ex As Exception
            Singleton.getSAPCompany.EndTransaction(BoWfTransOpt.wf_RollBack)
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_MissedClients_DataError(sender As System.Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgv_MissedClients.DataError
        Try

        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub FromDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FromDate.ValueChanged
        Try
            dtpFrmDate.Value = FromDate.Value
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub ToDate_ValueChanged(sender As System.Object, e As System.EventArgs) Handles ToDate.ValueChanged
        Try
            dtpToDate.Value = ToDate.Value
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_BF_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_BF_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_BF_R.Columns.Item(e.ColumnIndex).Name = "Select_BF_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_BF_R.Item("ItemCode_BF_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_BF_R.Item("Quantity_BF_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_BF_R.Item("ItemCode_BF_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_BF_R.Item("Dislike_BF_R", e.RowIndex).Value = strDisLike
                    dgv_BF_R.Item("Medical_BF_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_LN_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_LN_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_LN_R.Columns.Item(e.ColumnIndex).Name = "Select_LN_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_LN_R.Item("ItemCode_LN_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_LN_R.Item("Quantity_LN_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_LN_R.Item("ItemCode_LN_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_LN_R.Item("Dislike_LN_R", e.RowIndex).Value = strDisLike
                    dgv_LN_R.Item("Medical_LN_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_LS_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_LS_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_LS_R.Columns.Item(e.ColumnIndex).Name = "Select_LS_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_LS_R.Item("ItemCode_LS_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_LS_R.Item("Quantity_LS_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_LS_R.Item("ItemCode_LS_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_LS_R.Item("Dislike_LS_R", e.RowIndex).Value = strDisLike
                    dgv_LS_R.Item("Medical_LS_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_SK_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_SK_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_SK_R.Columns.Item(e.ColumnIndex).Name = "Select_SK_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_SK_R.Item("ItemCode_SK_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_SK_R.Item("Quantity_SK_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_SK_R.Item("ItemCode_SK_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_SK_R.Item("Dislike_SK_R", e.RowIndex).Value = strDisLike
                    dgv_SK_R.Item("Medical_SK_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_DN_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_DI_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_DI_R.Columns.Item(e.ColumnIndex).Name = "Select_DI_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_DI_R.Item("ItemCode_DI_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_DI_R.Item("Quantity_DI_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_DI_R.Item("ItemCode_DI_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_DI_R.Item("Dislike_DI_R", e.RowIndex).Value = strDisLike
                    dgv_DI_R.Item("Medical_DI_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_DS_R_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_DS_R.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If dgv_DS_R.Columns.Item(e.ColumnIndex).Name = "Select_DS_R" Then
                    Dim strItemCode As String
                    Dim strDisLike As String
                    Dim strMedical As String

                    strItemCode = (dgv_DS_R.Item("ItemCode_DS_R", e.RowIndex).Value)
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CType(sender, DataGridView).Name)
                    dgv_DS_R.Item("Quantity_DS_R", e.RowIndex).Value = dblCaloriesQty

                    If (hasBOM(dgv_DS_R.Item("ItemCode_DS_R", e.RowIndex).Value)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                        get_ChildItems(txtCardCode.Text, strItemCode, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, strItemCode)
                        strMedical = GetMedicalItem(txtCardCode.Text, strItemCode)
                    End If
                    dgv_DS_R.Item("Dislike_DS_R", e.RowIndex).Value = strDisLike
                    dgv_DS_R.Item("Medical_DS_R", e.RowIndex).Value = strMedical
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub dgv_BF_C_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgv_BF_C.KeyDown
        Try
            Dim intRowIndex As Integer = DirectCast(sender, System.Windows.Forms.DataGridView).CurrentCell.RowIndex
            Dim intColumnIndex As Integer = DirectCast(sender, System.Windows.Forms.DataGridView).CurrentCell.ColumnIndex
            If e.KeyData = Keys.Enter Then
                If intRowIndex > -1 And intColumnIndex > -1 Then
                    oRowPoint = intRowIndex
                    If dgv_BF_C.Columns.Item(intColumnIndex).Name = "ItemName_BF_C" Then
                        frmCFL.oCurrentForm = "PSWIZARD"
                        frmCFL.oCurrentItem = CType(sender, DataGridView).Name
                        frmCFL.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            MainForm.ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub txtFCustomer_Leave(sender As System.Object, e As System.EventArgs) Handles txtFCustomer.Leave
        Try
            If txtFCustomer.Text = String.Empty Then
                txtFCustomer.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFCustomer_Validated(sender As System.Object, e As System.EventArgs) Handles txtFCustomer.Validated
        Try
            If txtFCustomer.Text = String.Empty Then
                txtFCustomer.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTCustomer_Leave(sender As System.Object, e As System.EventArgs) Handles txtTCustomer.Leave
        Try
            If txtTCustomer.Text = String.Empty Then
                txtTCustomer.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTCustomer_Validated(sender As System.Object, e As System.EventArgs) Handles txtTCustomer.Validated
        Try
            If txtTCustomer.Text = String.Empty Then
                txtTCustomer.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFCGroup_Validated(sender As System.Object, e As System.EventArgs) Handles txtFCGroup.Validated
        Try
            If txtFCGroup.Text = String.Empty Then
                txtFCGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFCGroup_Leave(sender As System.Object, e As System.EventArgs) Handles txtFCGroup.Leave
        Try
            If txtFCGroup.Text = String.Empty Then
                txtFCGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTCGroup_Validated(sender As System.Object, e As System.EventArgs) Handles txtTCGroup.Validated
        Try
            If txtTCGroup.Text = String.Empty Then
                txtTCGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTCGroup_Leave(sender As System.Object, e As System.EventArgs) Handles txtTCGroup.Leave
        Try
            If txtTCGroup.Text = String.Empty Then
                txtTCGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFProgram_Validated(sender As System.Object, e As System.EventArgs) Handles txtFProgram.Validated
        Try
            If txtFProgram.Text = String.Empty Then
                txtFProgram.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFProgram_Leave(sender As System.Object, e As System.EventArgs) Handles txtFProgram.Leave
        Try
            If txtFProgram.Text = String.Empty Then
                txtFProgram.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTProgram_Validated(sender As System.Object, e As System.EventArgs) Handles txtTProgram.Validated
        Try
            If txtTProgram.Text = String.Empty Then
                txtTProgram.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTProgram_Leave(sender As System.Object, e As System.EventArgs) Handles txtTProgram.Leave
        Try
            If txtTProgram.Text = String.Empty Then
                txtTProgram.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFIGroup_Validated(sender As System.Object, e As System.EventArgs) Handles txtFIGroup.Validated
        Try
            If txtFIGroup.Text = String.Empty Then
                txtFIGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtFIGroup_Leave(sender As System.Object, e As System.EventArgs) Handles txtFIGroup.Leave
        Try
            If txtFIGroup.Text = String.Empty Then
                txtFIGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTIGroup_Validated(sender As System.Object, e As System.EventArgs) Handles txtTIGroup.Validated
        Try
            If txtTIGroup.Text = String.Empty Then
                txtTIGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTIGroup_Leave(sender As System.Object, e As System.EventArgs) Handles txtTIGroup.Leave
        Try
            If txtTIGroup.Text = String.Empty Then
                txtTIGroup.Tag = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Functions"

    Private Sub initialize()
        Try

            'Missed Clients List
            oDt_Customers = New DataTable
            oDt_Customers.Columns.Add("Select", GetType(System.Boolean))
            oDt_Customers.Columns.Add("CardCode", GetType(System.String))
            oDt_Customers.Columns.Add("CardName", GetType(System.String))
            oDt_Customers.Columns.Add("Dietitian", GetType(System.String))
            oDt_Customers.Columns.Add("IsCreated", GetType(System.String))

            'Program List
            oDt_Programs = New DataTable
            oDt_Programs.Columns.Add("Program_ID", GetType(System.String))
            oDt_Programs.Columns.Add("IsCons", GetType(System.Boolean))
            oDt_Programs.Columns.Add("PrgCode", GetType(System.String))
            oDt_Programs.Columns.Add("PrgName", GetType(System.String))
            oDt_Programs.Columns.Add("FromDate", GetType(System.String))
            oDt_Programs.Columns.Add("ToDate", GetType(System.String))
            oDt_Programs.Columns.Add("NoofDays", GetType(System.String))
            oDt_Programs.Columns.Add("FreeDays", GetType(System.String))
            oDt_Programs.Columns.Add("RemDays", GetType(System.String))


            'Program Dates
            oDt_ProgramDates = New DataTable
            oDt_ProgramDates.Columns.Add("PDate", GetType(System.DateTime))
            oDt_ProgramDates.Columns.Add("Name", GetType(System.String))
            oDt_ProgramDates.Columns.Add("Program_ID", GetType(System.String))
            oDt_ProgramDates.Columns.Add("PrgCode", GetType(System.String))

            'Break Fast
            oDt_BF_R = New DataTable
            oDt_BF_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_BF_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_BF_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_BF_R.Columns.Add("Qty", GetType(System.String))
            oDt_BF_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_BF_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_BF_R.Columns.Add("Remarks", GetType(System.String))

            oDt_BF_C = New DataTable
            oDt_BF_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_BF_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_BF_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_BF_C.Columns.Add("Qty", GetType(System.String))
            oDt_BF_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_BF_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_BF_C.Columns.Add("Remarks", GetType(System.String))

            'Lunch
            oDt_Lunch_R = New DataTable
            oDt_Lunch_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_Lunch_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Lunch_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Lunch_R.Columns.Add("Qty", GetType(System.String))
            oDt_Lunch_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Lunch_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_Lunch_R.Columns.Add("Remarks", GetType(System.String))

            oDt_Lunch_C = New DataTable
            oDt_Lunch_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_Lunch_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Lunch_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Lunch_C.Columns.Add("Qty", GetType(System.String))
            oDt_Lunch_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Lunch_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_Lunch_C.Columns.Add("Remarks", GetType(System.String))

            'Lunch Side
            oDt_LunchS_R = New DataTable
            oDt_LunchS_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_LunchS_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_LunchS_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_LunchS_R.Columns.Add("Qty", GetType(System.String))
            oDt_LunchS_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_LunchS_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_LunchS_R.Columns.Add("Remarks", GetType(System.String))

            oDt_LunchS_C = New DataTable
            oDt_LunchS_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_LunchS_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_LunchS_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_LunchS_C.Columns.Add("Qty", GetType(System.String))
            oDt_LunchS_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_LunchS_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_LunchS_C.Columns.Add("Remarks", GetType(System.String))

            'Snack
            oDt_Snack_R = New DataTable
            oDt_Snack_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_Snack_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Snack_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Snack_R.Columns.Add("Qty", GetType(System.String))
            oDt_Snack_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Snack_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_Snack_R.Columns.Add("Remarks", GetType(System.String))

            oDt_Snack_C = New DataTable
            oDt_Snack_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_Snack_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Snack_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Snack_C.Columns.Add("Qty", GetType(System.String))
            oDt_Snack_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Snack_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_Snack_C.Columns.Add("Remarks", GetType(System.String))

            'Dinner
            oDt_Dinner_R = New DataTable
            oDt_Dinner_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_Dinner_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Dinner_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Dinner_R.Columns.Add("Qty", GetType(System.String))
            oDt_Dinner_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Dinner_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_Dinner_R.Columns.Add("Remarks", GetType(System.String))

            oDt_Dinner_C = New DataTable
            oDt_Dinner_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_Dinner_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_Dinner_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_Dinner_C.Columns.Add("Qty", GetType(System.String))
            oDt_Dinner_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_Dinner_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_Dinner_C.Columns.Add("Remarks", GetType(System.String))

            'Dinner Side
            oDt_DinnerS_R = New DataTable
            oDt_DinnerS_R.Columns.Add("Select", GetType(System.Boolean))
            oDt_DinnerS_R.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_DinnerS_R.Columns.Add("U_ItemName", GetType(System.String))
            oDt_DinnerS_R.Columns.Add("Qty", GetType(System.String))
            oDt_DinnerS_R.Columns.Add("U_Dislike", GetType(System.String))
            oDt_DinnerS_R.Columns.Add("U_Medical", GetType(System.String))
            oDt_DinnerS_R.Columns.Add("Remarks", GetType(System.String))

            oDt_DinnerS_C = New DataTable
            oDt_DinnerS_C.Columns.Add("Select", GetType(System.Boolean))
            oDt_DinnerS_C.Columns.Add("U_ItemCode", GetType(System.String))
            oDt_DinnerS_C.Columns.Add("U_ItemName", GetType(System.String))
            oDt_DinnerS_C.Columns.Add("Qty", GetType(System.String))
            oDt_DinnerS_C.Columns.Add("U_Dislike", GetType(System.String))
            oDt_DinnerS_C.Columns.Add("U_Medical", GetType(System.String))
            oDt_DinnerS_C.Columns.Add("Remarks", GetType(System.String))

            ''Break Fast
            'oDt_Summary = New DataTable
            'oDt_Summary.Columns.Add("PDate", GetType(System.String))
            'oDt_Summary.Columns.Add("Food", GetType(System.String))
            'oDt_Summary.Columns.Add("Qty", GetType(System.String))
            'oDt_Summary.Columns.Add("Dislike", GetType(System.String))
            'oDt_Summary.Columns.Add("Medical", GetType(System.String))
            'oDt_Summary.Columns.Add("FoodType", GetType(System.String))
            'oDt_Summary.Columns.Add("Remarks", GetType(System.String))

            dgv_MissedClients.DataSource = oDt_Customers
            dgvProgramDate.DataSource = oDt_ProgramDates
            dgv_BF_R.DataSource = oDt_BF_R
            dgv_BF_C.DataSource = oDt_BF_C
            dgv_LN_R.DataSource = oDt_Lunch_R
            dgv_LN_C.DataSource = oDt_Lunch_C
            dgv_LS_R.DataSource = oDt_LunchS_R
            dgv_LS_C.DataSource = oDt_LunchS_C
            dgv_SK_R.DataSource = oDt_Snack_R
            dgv_SK_C.DataSource = oDt_Snack_C
            dgv_DI_R.DataSource = oDt_Dinner_R
            dgv_DI_C.DataSource = oDt_Dinner_C
            dgv_DS_R.DataSource = oDt_DinnerS_R
            dgv_DS_C.DataSource = oDt_DinnerS_C

            'dgv_Food_Summary.DataSource = oDt_Summary

            'dgv_MissedClients.AutoGenerateColumns = False
            'dgvProgramDate.AutoGenerateColumns = False
            'dgv_BF_D.AutoGenerateColumns = False
            'dgv_BF_C.AutoGenerateColumns = False
            'dgv_Lunch_C.AutoGenerateColumns = False
            'dgv_Lunch_R.AutoGenerateColumns = False
            'dgv_Food_Summary.AutoGenerateColumns = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub fillCustomers()
        Try
            oDt_Customers.Rows.Clear()

            Dim strFromDate As String = FromDate.Value.ToString(strdtFormat)
            Dim strToDate As String = ToDate.Value.ToString(strdtFormat)

            Dim strFromCust As String = IIf(IsNothing(txtFCustomer.Tag), "", txtFCustomer.Tag)
            Dim strToCust As String = IIf(IsNothing(txtTCustomer.Tag), "", txtTCustomer.Tag)
            Dim strProgram1 As String = IIf(IsNothing(txtFProgram.Tag), "", txtFProgram.Tag)
            Dim strProgram2 As String = IIf(IsNothing(txtTProgram.Tag), "", txtTProgram.Tag)
            Dim strCustGroup1 As String = IIf(IsNothing(txtFCGroup.Tag), "", txtFCGroup.Tag)
            Dim strCustGroup2 As String = IIf(IsNothing(txtTCGroup.Tag), "", txtTCGroup.Tag)
            Dim strItemGroup1 As String = IIf(IsNothing(txtFIGroup.Tag), "", txtFIGroup.Tag)
            Dim strItemGroup2 As String = IIf(IsNothing(txtTIGroup.Tag), "", txtTIGroup.Tag)

            strQuery = " Select DISTINCT T0.CardCode,CardName,Convert(VarChar(100),T0.aliasname) As 'Dietitian','No' As 'IsCreated' From OCRD T0  "
            strQuery += " JOIN [@Z_OCPM] T1 On T1.U_CardCode = T0.CardCode "
            strQuery += " JOIN [@Z_CPM1] T2 On T1.DocEntry = T2.DocEntry "
            strQuery += " And T1.U_RemDays > 0 And ISNULL(T1.U_Transfer,'N') = 'N' "
            strQuery += " LEFT OUTER JOIN [@Z_OCPR] T3 On T0.CardCode = T3.U_CardCode "
            strQuery += " JOIN OITM T4 ON T4.ItemCode = T1.U_PrgCode "
            strQuery += " LEFT OUTER JOIN OITB T5 On T4.ItmsGrpCod = T5.ItmsGrpCod "
            strQuery += " LEFT OUTER JOIN "
            strQuery += " RDR1 T6 ON T6.BaseCard = T1.U_CardCode  "
            strQuery += " And (T6.LineStatus = 'O' Or (T6.LineStatus = 'C' And T6.TargetType <> '-1')) "
            strQuery += " And T6.U_ProgramID = T1.DocEntry   "
            strQuery += " And T6.U_DelDate = T2.U_PrgDate "

            strQuery += " Where CardType = 'C' And ISNULL(T2.U_ONOFFSTA,'O') = 'O' AND ISNULL(T3.U_ONOFFSTA,'O') = 'O' AND ISNULL(T2.U_AppStatus,'I') = 'I' "

            If strFromCust.Length > 0 And strToCust.Length > 0 Then
                strQuery += " And T0.CardCode Between '" + strFromCust + "' AND '" + strToCust + "'"
            End If

            If strProgram1.Length > 0 And strProgram2.Length > 0 Then
                strQuery += " And T1.U_PrgCode Between '" + strProgram1 + "' And '" + strProgram2 + "'"
            End If

            If strCustGroup1.Length > 0 And strCustGroup2.Length > 0 Then
                strQuery += " And T0.GroupCode Between '" + strCustGroup1 + "' And '" + strCustGroup2 + "'"
            End If

            If strItemGroup1.Length > 0 And strItemGroup2.Length > 0 Then
                strQuery += " And T4.ItmsGrpCod Between '" + strItemGroup1 + "' And '" + strItemGroup2 + "'"
            End If

            If strFromDate.Length > 0 And strToDate.Length > 0 Then

                strQuery += " And "
                strQuery += " ( "
                strQuery += " Convert(VarChar(8),T2.U_PrgDate,112) >= '" + strFromDate + "' "
                strQuery += " AND Convert(VarChar(8),T2.U_PrgDate,112) <= '" + strToDate + "' "
                strQuery += " ) "
            End If

            strQuery += "  And U_PrgDate Between T1.U_PFromDate And T1.U_PToDate "
            strQuery += "  And  "
            strQuery += "  (  "
            strQuery += " (T1.U_PFromDate < T3.U_SuFrDt And ISNULL(T3.U_SuToDt,'') = '') "
            strQuery += "  OR  "
            strQuery += "  (1 = 1)  "
            strQuery += "  ) "
            strQuery += "  And T6.U_DelDate Is Null "
            strQuery += "  Order By CardCode "

            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_Customers.Load(oDr)
            oDr.Close()
            dgv_MissedClients.DataSource = oDt_Customers
            dgv_MissedClients.Refresh()
            If oDt_Customers.Rows.Count > 0 Then
                scWizard0.Panel1Collapsed = True
                sc_Missed_Clients_0.Enabled = True
            Else
                MainForm.ErrorMsg.StatusBarMsg("No Customers Found", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub fillProgram()
        Try
            Try
                oDt_Programs.Rows.Clear()
            Catch ex As Exception

            End Try


            Dim strFrmDate As String = dtpFrmDate.Value.ToString(strdtFormat)
            Dim strToDate As String = dtpToDate.Value.ToString(strdtFormat)
            Dim strCardCode As String = getCustomerCode()

            strQuery = " Select Distinct T1.DocEntry As 'Program_ID',  T1.U_PrgCode As 'PrgCode',U_PrgName As 'PrgName' "
            strQuery += " ,U_PFromDate As 'FromDate',U_PToDate As 'ToDate',U_NoOfDays As 'NoofDays' "
            strQuery += " ,U_FreeDays As 'FreeDays',U_RemDays As 'RemDays' "
            strQuery += " From [@Z_CPM1] T0 JOIN [@Z_OCPM] T1 On T0.DocEntry = T1.DocEntry "
            strQuery += " JOIN [@Z_OCPR] T3 On T1.U_CardCode = T3.U_CardCode  "

            strQuery += " LEFT OUTER JOIN "
            strQuery += " RDR1 T6 ON T6.BaseCard = T1.U_CardCode  "
            strQuery += " And (T6.LineStatus = 'O' Or (T6.LineStatus = 'C' And T6.TargetType <> '-1')) "
            strQuery += " And T6.U_ProgramID = T1.DocEntry   "
            strQuery += " And T6.U_DelDate = T0.U_PrgDate "

            strQuery += " Where Convert(VarChar(8),T0.U_PrgDate,112) >= '" & strFrmDate & "'"
            strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" & strToDate & "'"
            strQuery += " And T0.U_PrgDate >= T1.U_PFromDate "
            strQuery += " And T0.U_PrgDate <= T1.U_PToDate "
            strQuery += " And T1.U_CardCode = '" + strCardCode + "'"
            strQuery += " And T0.U_AppStatus = 'I' "
            strQuery += " And ISNULL(T0.U_ONOFFSTA,'O') = 'O' "
            strQuery += " AND T1.U_RemDays > 0  "
            'strQuery += " And T0.U_PrgDate Not In (Select Distinct U_DelDate From RDR1 Where BaseCard = '" & strCardCode & "' "
            'strQuery += " And (LineStatus = 'O' Or (LineStatus = 'C' And TargetType <> '-1')) "
            'strQuery += " ) "
            strQuery += "  And  "
            strQuery += "  (  "
            strQuery += "  (T0.U_PrgDate < T3.U_SuFrDt And ISNULL(T3.U_SuToDt,'') = '')  "
            strQuery += "  OR  "
            strQuery += "  (1 = 1)  "
            strQuery += "  ) "
            strQuery += "  And T6.U_DelDate Is Null "
            strQuery += "  Order By T1.DocEntry  "

            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_Programs.Load(oDr)
            oDr.Close()
            dgv_Programs.DataSource = oDt_Programs
            dgv_Programs.Refresh()
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub fillProgramDate()
        Try
            'oDt_ProgramDates.Rows.Clear()
            Try
                oDt_ProgramDates = New DataTable
            Catch ex As Exception

            End Try

            Dim strFrmDate As String = dtpFrmDate.Value.ToString(strdtFormat)
            Dim strToDate As String = dtpToDate.Value.ToString(strdtFormat)
            Dim strCardCode As String = getCustomerCode()
            Dim strType As String = "P"

            'Dim strRef As String = getProgramRef()

            strQuery = " Select T0.U_PrgDate As 'PDate',Convert(VarChar(8),T0.U_PrgDate,112) As 'Name' "
            strQuery += " ,T1.DocEntry As 'Program_ID',  T1.U_PrgCode As 'PrgCode' "
            strQuery += " From [@Z_CPM1] T0 JOIN [@Z_OCPM] T1 On T0.DocEntry = T1.DocEntry "
            strQuery += " JOIN [@Z_OCPR] T3 On T1.U_CardCode = T3.U_CardCode  "

            strQuery += " LEFT OUTER JOIN "
            strQuery += " RDR1 T6 ON T6.BaseCard = T1.U_CardCode  "
            strQuery += " And (T6.LineStatus = 'O' Or (T6.LineStatus = 'C' And T6.TargetType <> '-1')) "
            strQuery += " And T6.U_ProgramID = T1.DocEntry   "
            strQuery += " And T6.U_DelDate = T0.U_PrgDate "

            strQuery += " Where Convert(VarChar(8),T0.U_PrgDate,112) >= '" & strFrmDate & "'"
            strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" & strToDate & "'"
            strQuery += " And T1.U_CardCode = '" + strCardCode + "'"
            strQuery += " And T0.U_AppStatus = 'I' "
            strQuery += " And ISNULL(T0.U_ONOFFSTA,'O') = 'O' "
            strQuery += " AND T1.U_RemDays > 0  "
            strQuery += " And T0.U_PrgDate >= T1.U_PFromDate "
            strQuery += " And T0.U_PrgDate <= T1.U_PToDate "

            'If strType.Trim() = "I" Then
            '    strQuery += " And ISNULL(T1.U_InvRef,T2.U_InvRef) = '" & strRef & "'"
            'ElseIf strType.Trim() = "T" Then
            '    strQuery += " And T1.U_TrnRef = '" & strRef & "'"
            'ElseIf strType.Trim() = "P" Then
            '    strQuery += " And T1.DocEntry = '" & strRef & "'"
            'End If

            'strQuery += " And T0.U_PrgDate Not In (Select Distinct U_DelDate From RDR1 Where BaseCard = '" & strCardCode & "' "
            'strQuery += " And (LineStatus = 'O' Or (LineStatus = 'C' And TargetType <> '-1')) "
            'strQuery += " ) "

            strQuery += "  And  "
            strQuery += "  (  "
            strQuery += "  (T0.U_PrgDate < T3.U_SuFrDt And ISNULL(T3.U_SuToDt,'') = '')  "
            strQuery += "  OR  "
            strQuery += "  (1 = 1)  "
            strQuery += "  ) "
            strQuery += "  And T6.U_DelDate Is Null "
            strQuery += "  Order By T1.DocEntry,T0.U_PrgDate  "

            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_ProgramDates.Load(oDr)
            oDr.Close()
            Try
                dgvProgramDate.DataSource = oDt_ProgramDates
                dgvProgramDate.Refresh()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub fillMenuBasedOnDate()
        Try
            Dim strPrgDate As String = dtpSelectedDate.Value.ToString(strdtFormat)
            Dim strCardCode As String = getCustomerCode()
            Dim strProgramID As String = getProgramID()
            Dim strProgram As String = getProgramCode()


            oRecordSet = Singleton.getSAPCompany().GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            'Break Fast - Regular   
            oDt_BF_R.Rows.Clear()
            Dim dblQty As Double
            dblQty = getQuantityBasedonCaloriesRatio("dgv_BF_R")
            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select',"
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_BFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED1] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'BF' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_BF_R.Load(oDr)
                oDr.Close()
                dgv_BF_R.DataSource = oDt_BF_R
                dgv_BF_R.Refresh()
            Else
                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select',"
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_BFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED1] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
                strQuery += " And T1.U_CatType = 'G' "
                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'BF' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T1.U_GrpCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_BF_R.Load(oDr)
                oDr.Close()
                dgv_BF_R.DataSource = oDt_BF_R
                dgv_BF_R.Refresh()
            End If


            'Break Fast - Custom    
            oDt_BF_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'BF' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_BF_C.Load(oDr)
            oDr.Close()
            dgv_BF_C.DataSource = oDt_BF_C
            dgv_BF_C.Refresh()


            'Lunch - Regular
            oDt_Lunch_R.Rows.Clear()
            dblQty = getQuantityBasedonCaloriesRatio("dgv_LN_R")
            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_LFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED2] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'LN' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "

            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Lunch_R.Load(oDr)
                oDr.Close()
                dgv_LN_R.DataSource = oDt_Lunch_R
                dgv_LN_R.Refresh()
            Else

                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_LFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED2] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
                strQuery += " And T1.U_CatType = 'G' "
                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'LN' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"

                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "

                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T4.ItemCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "

                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Lunch_R.Load(oDr)
                oDr.Close()
                dgv_LN_R.DataSource = oDt_Lunch_R
                dgv_LN_R.Refresh()
            End If

            'Lunch - Custom
            oDt_Lunch_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'LN' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_Lunch_C.Load(oDr)
            oDr.Close()
            dgv_LN_C.DataSource = oDt_Lunch_C
            dgv_LN_C.Refresh()

            'Lunch(Side) - Regular
            oDt_LunchS_R.Rows.Clear()
            dblQty = getQuantityBasedonCaloriesRatio("dgv_LN_R")

            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_LSFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED3] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'LS' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "

            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_LunchS_R.Load(oDr)
                oDr.Close()
                dgv_LS_R.DataSource = oDt_LunchS_R
                dgv_LS_R.Refresh()
            Else

                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_LSFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED3] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "

                strQuery += " And T1.U_CatType = 'G' "

                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'LS' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"

                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "

                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T4.ItemCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "

                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_LunchS_R.Load(oDr)
                oDr.Close()
                dgv_LS_R.DataSource = oDt_LunchS_R
                dgv_LS_R.Refresh()

            End If

            'Lunch(Side) - Custom
            oDt_LunchS_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select' , "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'LS' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_LunchS_C.Load(oDr)
            oDr.Close()
            dgv_LS_C.DataSource = oDt_LunchS_C
            dgv_LS_C.Refresh()

            'Snack - Regular
            oDt_Snack_R.Rows.Clear()
            dblQty = getQuantityBasedonCaloriesRatio("dgv_SK_R")

            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_SFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED4] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'SK' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "

            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Snack_R.Load(oDr)
                oDr.Close()
                dgv_SK_R.DataSource = oDt_Snack_R
                dgv_SK_R.Refresh()
            Else

                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_SFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED4] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
                strQuery += " And T1.U_CatType = 'G' "
                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'SK' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T4.ItemCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "

                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Snack_R.Load(oDr)
                oDr.Close()
                dgv_SK_R.DataSource = oDt_Snack_R
                dgv_SK_R.Refresh()

            End If

            'Snack - Custom
            oDt_Snack_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'SK' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_Snack_C.Load(oDr)
            oDr.Close()
            dgv_SK_C.DataSource = oDt_Snack_C
            dgv_SK_C.Refresh()


            'Dinner - Regular
            oDt_Dinner_R.Rows.Clear()
            dblQty = getQuantityBasedonCaloriesRatio("dgv_DN_R")

            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_DFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED5] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'DI' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "
            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Dinner_R.Load(oDr)
                oDr.Close()
                dgv_DI_R.DataSource = oDt_Dinner_R
                dgv_DI_R.Refresh()
            Else
                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_DFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED5] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
                strQuery += " And T1.U_CatType = 'G' "
                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'DI' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T4.ItemCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "

                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_Dinner_R.Load(oDr)
                oDr.Close()
                dgv_DI_R.DataSource = oDt_Dinner_R
                dgv_DI_R.Refresh()
            End If


            'Dinner - Custom
            oDt_Dinner_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'DI' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_Dinner_C.Load(oDr)
            oDr.Close()
            dgv_DI_C.DataSource = oDt_Dinner_C
            dgv_DI_C.Refresh()


            'Dinner(Side) - Regular
            oDt_DinnerS_R.Rows.Clear()
            dblQty = getQuantityBasedonCaloriesRatio("dgv_DS_R")

            strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T0.U_ItemCode,U_ItemName, "
            strQuery += " (Select ISNULL( "
            strQuery += " (ISNULL(T2.U_Quantity, "
            strQuery += " (Select ISNULL(" & dblQty & ",U_DSFactor) From [@Z_OCAJ] T0 "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
            strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
            strQuery += " ),1)) As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From [@Z_MED6] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "
            strQuery += " And T1.U_CatType = 'I' "
            strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'DS' "
            strQuery += " And T2.U_SFood = 'R'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " Where T1.U_MenuType = 'R' "
            strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
            strQuery += " And T1.U_PrgCode = '" + strProgram + "'"
            strQuery += " And T0.U_ItemCode Is Not Null "
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_DinnerS_R.Load(oDr)
                oDr.Close()
                dgv_DS_R.DataSource = oDt_DinnerS_R
                dgv_DS_R.Refresh()
            Else

                strQuery = " Select (Select Case When T0.U_ItemCode = T2.U_ItemCode Then 1 ELSE 0 END) As 'Select', "
                strQuery += " T0.U_ItemCode,U_ItemName, "
                strQuery += " (Select ISNULL( "
                strQuery += " (ISNULL(T2.U_Quantity, "
                strQuery += " (Select ISNULL(" & dblQty & ",U_DSFactor) From [@Z_OCAJ] T0 "
                strQuery += " JOIN [@Z_OCPR] T1 On T0.U_Calories "
                strQuery += " = T1.U_CPAdj Where T1.U_CardCode = '" + strCardCode + "')) "
                strQuery += " ),1)) As 'Qty', "
                strQuery += " T2.U_Dislike,T2.U_Medical, "
                strQuery += " T2.U_Remarks As 'Remarks' "
                strQuery += " From [@Z_MED6] T0 JOIN [@Z_OMED] T1 On T1.DocEntry = T0.DocEntry  "

                strQuery += " And T1.U_CatType = 'G' "

                strQuery += " LEFT OUTER JOIN [@Z_OFSL] T2 ON T2.U_ItemCode = T0.U_ItemCode  "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = Convert(VarChar(8),T2.U_PrgDate,112)  "
                strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
                strQuery += " And T2.U_FType = 'DS' "
                strQuery += " And T2.U_SFood = 'R'  "
                strQuery += " And T2.U_Select = 'Y' "
                strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"

                strQuery += " JOIN OITB T3 On T3.ItmsGrpCod = T1.U_GrpCode "
                strQuery += " JOIN OITM T4 On T4.ItmsGrpCod = T3.ItmsGrpCod And T4.ItmsGrpCod = T1.U_GrpCode "

                strQuery += " Where T1.U_MenuType = 'R' "
                strQuery += " And Convert(VarChar(8),T1.U_MenuDate,112) = '" + strPrgDate + "'"
                strQuery += " And T4.ItemCode = '" + strProgram + "'"
                strQuery += " And T0.U_ItemCode Is Not Null "

                oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
                oDt_DinnerS_R.Load(oDr)
                oDr.Close()
                dgv_DS_R.DataSource = oDt_DinnerS_R
                dgv_DS_R.Refresh()
            End If

            'Dinner(Side) - Custom
            oDt_DinnerS_C.Rows.Clear()
            strQuery = " Select (Select Case When T2.U_ItemCode = T1.ItemCode Then 1 ELSE 0 END) As 'Select', "
            strQuery += " T2.U_ItemCode,T1.ItemName As U_ItemName, "
            strQuery += " T2.U_Quantity As 'Qty', "
            strQuery += " T2.U_Dislike,T2.U_Medical, "
            strQuery += " T2.U_Remarks As 'Remarks' "
            strQuery += " From  [@Z_OFSL] T2 JOIN OITM T1  ON T2.U_ItemCode = T1.ItemCode  "
            strQuery += " And T2.U_CardCode = '" + strCardCode + "' "
            strQuery += " And T2.U_FType = 'DS' "
            strQuery += " And T2.U_SFood = 'C'  "
            strQuery += " And T2.U_Select = 'Y' "
            strQuery += " And T2.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And Convert(VarChar(8),T2.U_PrgDate,112) = '" + strPrgDate + "'"
            oDr = Singleton.GetSQLDataObject().ExecuteReader(Singleton.getSAPCompany.CompanyDB, strQuery)
            oDt_DinnerS_C.Load(oDr)
            oDr.Close()
            dgv_DS_C.DataSource = oDt_DinnerS_C
            dgv_DS_C.Refresh()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function getQuantityBasedonCaloriesRatio(ByVal strType As String) As Double
        Try
            Dim _retVal As Double = 0
            Dim strCardCode As String = getCustomerCode()
            Dim strPrgDate As String = dtpSelectedDate.Value.ToString(strdtFormat)
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Select Case strType
                Case "dgv_BF_R", "dgv_BF_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_BF = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'BF' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
                Case "dgv_LN_R", "dgv_LN_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_LN = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'LN' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
                Case "dgv_LS_R", "dgv_LS_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_LS = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'LS' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
                Case "dgv_SK_R", "dgv_SK_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_SK = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'SK' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
                Case "dgv_DI_R", "dgv_DI_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_DI = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'DI' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
                Case "dgv_DS_R", "dgv_DS_C"
                    strQuery = " Select TOP 1 U_Ratio From [@Z_CPR7] T0 "
                    strQuery += " JOIN [@Z_OCPR] T1 ON T0.DocEntry = T1.DocEntry  "
                    strQuery += " JOIN [@Z_OCRT] T2 On T0.U_DS = T2.U_Code "
                    strQuery += " Where T1.U_CardCode = '" + strCardCode + "' And T2.U_FType = 'DS' "
                    strQuery += " And Convert(VarChar(8),T0.U_PrgDate,112) <= '" + strPrgDate + "'"
                    strQuery += " Order By T0.U_PrgDate DESC "
                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        _retVal = CDbl(oRecordSet.Fields.Item(0).Value)
                    Else
                        _retVal = 1
                    End If
            End Select
            Return _retVal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getCustomerCode() As String
        Try
            'Return (CType(lblCustomer_T.Text.Split("-"), String())(0))\
            Return txtCardCode.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getProgramID() As String
        Try
            'Return (CType(lblProgram_T.Text.Split("-"), String())(0))
            Return txt_Program_ID.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getProgramCode() As String
        Try
            ' Return (CType(lblProgram_T.Text.Split("-"), String())(1))
            Return txt_Program.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillSelection(ByVal CurrentItem As String, ByVal value1 As String, ByVal value2 As String)
        Try
            Dim strDisLike As String
            Dim strMedical As String

            Select Case CurrentItem
                Case "dgv_BF_C"
                    If oRowPoint + 1 >= dgv_BF_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_BF_C.DataSource, DataTable).NewRow
                        CType(dgv_BF_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_BF_C.Item("Select_BF_C", oRowPoint).Value = True
                    dgv_BF_C.Item("ItemCode_BF_C", oRowPoint).Value = value1
                    dgv_BF_C.Item("ItemName_BF_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_BF_C.Item("Quantity_BF_C", oRowPoint).Value = dblCaloriesQty
                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_BF_C.Item("Dislike_BF_C", oRowPoint).Value = strDisLike
                    dgv_BF_C.Item("Medical_BF_C", oRowPoint).Value = strMedical
                    oDt_BF_C.AcceptChanges()
                Case "dgv_LN_C"
                    If oRowPoint + 1 >= dgv_LN_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_LN_C.DataSource, DataTable).NewRow
                        CType(dgv_LN_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_LN_C.Item("Select_LN_C", oRowPoint).Value = True
                    dgv_LN_C.Item("ItemCode_LN_C", oRowPoint).Value = value1
                    dgv_LN_C.Item("ItemName_LN_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_LN_C.Item("Quantity_LN_C", oRowPoint).Value = dblCaloriesQty
                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_LN_C.Item("Dislike_LN_C", oRowPoint).Value = strDisLike
                    dgv_LN_C.Item("Medical_LN_C", oRowPoint).Value = strMedical

                    oDt_Lunch_C.AcceptChanges()
                Case "dgv_LS_C"
                    If oRowPoint + 1 >= dgv_LS_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_LS_C.DataSource, DataTable).NewRow
                        CType(dgv_LS_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_LS_C.Item("Select_LS_C", oRowPoint).Value = True
                    dgv_LS_C.Item("ItemCode_LS_C", oRowPoint).Value = value1
                    dgv_LS_C.Item("ItemName_LS_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_LS_C.Item("Quantity_LS_C", oRowPoint).Value = dblCaloriesQty

                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_LS_C.Item("Dislike_LS_C", oRowPoint).Value = strDisLike
                    dgv_LS_C.Item("Medical_LS_C", oRowPoint).Value = strMedical

                    oDt_LunchS_C.AcceptChanges()
                Case "dgv_SK_C"
                    If oRowPoint + 1 >= dgv_SK_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_SK_C.DataSource, DataTable).NewRow
                        CType(dgv_SK_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_SK_C.Item("Select_SK_C", oRowPoint).Value = True
                    dgv_SK_C.Item("ItemCode_SK_C", oRowPoint).Value = value1
                    dgv_SK_C.Item("ItemName_SK_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_SK_C.Item("Quantity_SK_C", oRowPoint).Value = dblCaloriesQty
                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_SK_C.Item("Dislike_SK_C", oRowPoint).Value = strDisLike
                    dgv_SK_C.Item("Medical_SK_C", oRowPoint).Value = strMedical

                    oDt_Snack_C.AcceptChanges()
                Case "dgv_DI_C"
                    If oRowPoint + 1 >= dgv_DI_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_DI_C.DataSource, DataTable).NewRow
                        CType(dgv_DI_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_DI_C.Item("Select_DI_C", oRowPoint).Value = True
                    dgv_DI_C.Item("ItemCode_DI_C", oRowPoint).Value = value1
                    dgv_DI_C.Item("ItemName_DI_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_DI_C.Item("Quantity_DI_C", oRowPoint).Value = dblCaloriesQty
                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_DI_C.Item("Dislike_DI_C", oRowPoint).Value = strDisLike
                    dgv_DI_C.Item("Medical_DI_C", oRowPoint).Value = strMedical
                    oDt_Dinner_C.AcceptChanges()

                Case "dgv_DS_C"
                    If oRowPoint + 1 >= dgv_DS_C.Rows.Count Then
                        Dim dr As DataRow = CType(dgv_DS_C.DataSource, DataTable).NewRow
                        CType(dgv_DS_C.DataSource, DataTable).Rows.Add(dr)
                    End If
                    dgv_DS_C.Item("Select_DS_C", oRowPoint).Value = True
                    dgv_DS_C.Item("ItemCode_DS_C", oRowPoint).Value = value1
                    dgv_DS_C.Item("ItemName_DS_C", oRowPoint).Value = value2
                    Dim dblCaloriesQty As Double = getQuantityBasedonCaloriesRatio(CurrentItem)
                    dgv_DS_C.Item("Quantity_DS_C", oRowPoint).Value = dblCaloriesQty
                    If (hasBOM(value1)) Then
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                        get_ChildItems(txtCardCode.Text, value1, strDisLike, strMedical)
                    Else
                        strDisLike = GetDisLikeItem(txtCardCode.Text, value1)
                        strMedical = GetMedicalItem(txtCardCode.Text, value1)
                    End If
                    dgv_DS_C.Item("Dislike_DS_C", oRowPoint).Value = strDisLike
                    dgv_DS_C.Item("Medical_DS_C", oRowPoint).Value = strMedical
                    oDt_DinnerS_C.AcceptChanges()

                Case "txtFCustomer"
                    txtFCustomer.Text = value2
                    txtFCustomer.Tag = value1

                Case "txtTCustomer"
                    txtTCustomer.Text = value2
                    txtTCustomer.Tag = value1

                Case "txtFCGroup"
                    txtFCGroup.Text = value2
                    txtFCGroup.Tag = value1

                Case "txtTCGroup"
                    txtTCGroup.Text = value2
                    txtTCGroup.Tag = value1

                Case "txtFProgram"
                    txtFProgram.Text = value2
                    txtFProgram.Tag = value1

                Case "txtTProgram"
                    txtTProgram.Text = value2
                    txtTProgram.Tag = value1

                Case "txtFIGroup"
                    txtFIGroup.Text = value2
                    txtFIGroup.Tag = value1

                Case "txtTIGroup"
                    txtTIGroup.Text = value2
                    txtTIGroup.Tag = value1

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub popUpCfl(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
                frmCFL.oCurrentForm = "PSWIZARD"
                frmCFL.oCurrentItem = CType(sender, TextBox).Name
                frmCFL.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub showCFL(ByVal sender As Object)
        Try
            frmCFL.oCurrentForm = "PSWIZARD"
            frmCFL.oCurrentItem = CType(sender, TextBox).Name
            frmCFL.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function validate_ClientSelection()
        Dim _retVal As Boolean = False
        Try
            For index = 0 To dgv_MissedClients.RowCount - 1
                Dim strCValue As String = IIf(IsDBNull(dgv_MissedClients.Item("IsCreated_MC", index).Value), "", dgv_MissedClients.Item("IsCreated_MC", index).Value)
                If strCValue = "No" Then
                    Dim strValue As String = IIf(IsDBNull(dgv_MissedClients.Item(0, index).Value), "", dgv_MissedClients.Item(0, index).Value)
                    If strValue = "True" Then
                        txtCardCode.Text = dgv_MissedClients.Item(1, index).Value
                        txtCardName.Text = dgv_MissedClients.Item(2, index).Value
                        oRowPoint_C = index
                        _retVal = True
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return _retVal
    End Function

    Private Function hasBOM(ByVal strItemCode As String) As Boolean
        Try
            Dim oBOM As SAPbobsCOM.ProductTrees
            oBOM = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductTrees)
            If oBOM.GetByKey(strItemCode) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub get_ChildItems(ByVal strCustomer As String, ByVal strItemCode As String, ByRef strDislike As String, ByRef strMedical As String)
        Try
            Dim oBOM As SAPbobsCOM.ProductTrees
            Dim oBOM_Lines As SAPbobsCOM.ProductTrees_Lines
            oBOM = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductTrees)
            If oBOM.GetByKey(strItemCode) Then
                oBOM_Lines = oBOM.Items
                For bomlineindex As Integer = 0 To oBOM_Lines.Count - 1
                    oBOM_Lines.SetCurrentLine(bomlineindex)
                    Dim strChildItem As String = oBOM_Lines.ItemCode
                    If hasBOM(strChildItem) Then

                        Dim strDislikeItem As String = GetDisLikeItem(strCustomer, strChildItem)
                        If strDislikeItem.Trim.Length > 0 Then
                            If strDislike.Length = 0 Then
                                strDislike = strDislikeItem
                            Else
                                If Not strDislike.Contains(strDislikeItem) Then
                                    strDislike += "," + strDislikeItem
                                End If
                            End If
                        End If

                        Dim strMedicalItem As String = GetMedicalItem(strCustomer, strChildItem)
                        If strMedicalItem.Trim.Length > 0 Then
                            If strMedical.Length = 0 Then
                                strMedical = strMedicalItem
                            Else
                                If Not strMedical.Contains(strMedicalItem) Then
                                    strMedical += "," + strMedicalItem
                                End If
                            End If
                        End If

                        get_ChildItems(strCustomer, strChildItem, strDislike, strMedical)
                    Else

                        Dim strDislikeItem As String = GetDisLikeItem(strCustomer, strChildItem)
                        If strDislikeItem.Trim.Length > 0 Then
                            If strDislike.Length = 0 Then
                                strDislike = strDislikeItem
                            Else
                                If Not strDislike.Contains(strDislikeItem) Then
                                    strDislike += "," + strDislikeItem
                                End If
                            End If
                        End If

                        Dim strMedicalItem As String = GetMedicalItem(strCustomer, strChildItem)
                        If strMedicalItem.Trim.Length > 0 Then
                            If strMedical.Length = 0 Then
                                strMedical = strMedicalItem
                            Else
                                If Not strMedical.Contains(strMedicalItem) Then
                                    strMedical += "," + strMedicalItem
                                End If
                            End If
                        End If
                    End If

                Next
            Else
                Dim strDislikeItem As String = GetDisLikeItem(strCustomer, strItemCode)
                If strDislikeItem.Trim.Length > 0 Then
                    If strDislike.Length = 0 Then
                        strDislike = strDislikeItem
                    Else
                        If Not strDislike.Contains(strDislikeItem) Then
                            strDislike += "," + strDislikeItem
                        End If
                    End If
                End If

                Dim strMedicalItem As String = GetMedicalItem(strCustomer, strItemCode)
                If strMedicalItem.Trim.Length > 0 Then
                    If strMedical.Length = 0 Then
                        strMedical = strMedicalItem
                    Else
                        If Not strMedical.Contains(strMedicalItem) Then
                            strMedical += "," + strMedicalItem
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetDisLikeItem(ByVal strCardCode As String, ByVal strItem As String) As String
        Dim _retVal As String = String.Empty
        Dim strQuery As String = String.Empty
        Try
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = Singleton.getSAPCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            strQuery = " Select ISNULL(T2.U_Name,'') From [@Z_CPR1] T0  "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.DocEntry = T1.DocEntry "
            strQuery += " JOIN [@Z_ODLK] T2 On T2.U_Code = T0.U_DLikeItem "
            strQuery += " JOIN [@Z_DLK1] T3 On T3.DocEntry = T2.DocEntry "
            strQuery += " Where T1.U_CardCode = '" + strCardCode + "'"
            strQuery += " And T3.U_ItemCode = '" + strItem + "'"
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                _retVal = oRecordSet.Fields.Item(0).Value
            End If
            Return _retVal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMedicalItem(ByVal strCardCode As String, ByVal strItem As String) As String
        Dim _retVal As String = String.Empty
        Dim strQuery As String = String.Empty
        Try
            Dim oRecordSet As SAPbobsCOM.Recordset
            oRecordSet = Singleton.getSAPCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
            strQuery = " Select ISNULL(T2.U_Name,'') From [@Z_CPR2] T0  "
            strQuery += " JOIN [@Z_OCPR] T1 On T0.DocEntry = T1.DocEntry "
            strQuery += " JOIN [@Z_OMST] T2 On T2.U_Code = T0.U_MSCode "
            strQuery += " JOIN [@Z_MST1] T3 On T3.DocEntry = T2.DocEntry "
            strQuery += " Where T1.U_CardCode = '" + strCardCode + "'"
            strQuery += " And T3.U_ItemCode = '" + strItem + "'"
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                _retVal = oRecordSet.Fields.Item(0).Value
            End If
            Return _retVal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function addremoveSession(ByVal strProgram As String) As String
        Dim _retVal As String = String.Empty
        Dim strNotSelFol As String = String.Empty
        Try

            'tcFood.TabPages.Add(tpBreak)
            'tcFood.TabPages.Add(tpLunch)
            'tcFood.TabPages.Add(tpLunchSide)
            'tcFood.TabPages.Add(tpSnack)
            'tcFood.TabPages.Add(tpDinner)
            'tcFood.TabPages.Add(tpDinnerSide)

            oRecordSet = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            strQuery = "Select "
            strQuery += " ISNULL(U_BF,'N') As 'U_BF',ISNULL(U_LN,'N') As 'U_LN',ISNULL(U_LS,'N') As 'U_LS', "
            strQuery += " ISNULL(U_SK,'N') As 'U_SK',ISNULL(U_DN,'N') As 'U_DN',ISNULL(U_DS,'N') As 'U_DS' "
            strQuery += " From OITM Where ItemCode = '" & strProgram & "'"
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then

                If (oRecordSet.Fields.Item("U_BF").Value = "Y") Then
                    'tcFood.TabPages.Add(tpBreak)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpBreak" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpBreak)
                        tpBreak.Controls.Add(scBF)
                    End If
                Else
                    tcFood.TabPages.Remove(tpBreak)
                End If

                If (oRecordSet.Fields.Item("U_LN").Value = "Y") Then
                    'tcFood.TabPages.Add(tpLunch)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpLunch" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpLunch)
                        tpLunch.Controls.Add(scLunch)
                    End If
                Else
                    tcFood.TabPages.Remove(tpLunch)
                End If

                If (oRecordSet.Fields.Item("U_LS").Value = "Y") Then
                    'tcFood.TabPages.Add(tpLunchSide)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpLunchSide" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpLunchSide)
                        tpLunchSide.Controls.Add(scLunchSide)
                    End If
                Else
                    tcFood.TabPages.Remove(tpLunchSide)
                End If

                If (oRecordSet.Fields.Item("U_SK").Value = "Y") Then
                    'tcFood.TabPages.Add(tpSnack)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpSnack" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpSnack)
                        tpSnack.Controls.Add(scSnack)
                    End If
                Else
                    tcFood.TabPages.Remove(tpSnack)
                End If

                If (oRecordSet.Fields.Item("U_DN").Value = "Y") Then
                    'tcFood.TabPages.Add(tpDinner)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpDinner" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpDinner)
                        tpDinner.Controls.Add(scDinner)
                    End If
                Else
                    tcFood.TabPages.Remove(tpDinner)
                End If

                If (oRecordSet.Fields.Item("U_DS").Value = "Y") Then
                    'tcFood.TabPages.Add(tpDinnerSide)
                    Dim blnExist As Boolean = False
                    For Each tp As TabPage In tcFood.TabPages
                        If tp.Name = "tpDinnerSide" Then
                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        tcFood.TabPages.Add(tpDinnerSide)
                        tpDinnerSide.Controls.Add(scDinnerSide)
                    End If
                Else
                    tcFood.TabPages.Remove(tpDinnerSide)
                End If

                _retVal = strNotSelFol

                Return _retVal
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try
        Return _retVal
    End Function

    Public Sub UpdateCustomerFoodMenu(ByVal dgvGrid As System.Windows.Forms.DataGridView, ByVal strFType As String, ByVal strSType As String)
        Dim oUserTable As SAPbobsCOM.UserTable
        Dim sCode As String
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim strQuery As String = String.Empty

        Try

            Dim strCardCode As String = txtCardCode.Text
            Dim strProgram As String = txt_Program.Text
            Dim strProgramID As String = txt_Program_ID.Text
            Dim strMenuDate As String = dtpSelectedDate.Value.ToString(strdtFormat)
            Dim dtPrgDate As Date = dtpSelectedDate.Value
            Dim strSession As String = txtInstance.Text

            For intRow As Integer = 0 To dgvGrid.RowCount - 1
                oRecordSet = Singleton.getSAPCompany.GetBusinessObject(BoObjectTypes.BoRecordset)
                oUserTable = Singleton.getSAPCompany.UserTables.Item("Z_OFSL")

                Dim strSelect As String = IIf(
                    IIf(IsDBNull(dgvGrid.Rows(intRow).Cells("Select_" & strFType & "_" & strSType).Value), "N",
                        dgvGrid.Rows(intRow).Cells("Select_" & strFType & "_" & strSType).Value) = "True", "Y", "N")
                Dim strItemCode As String = IIf(IsDBNull(dgvGrid.Rows(intRow).Cells("ItemCode_" & strFType & "_" & strSType).Value), "", dgvGrid.Rows(intRow).Cells("ItemCode_" & strFType & "_" & strSType).Value)
                If strItemCode <> "" Then
                    If strItemCode.Trim().Length = 0 Then
                        Continue For
                    End If
                ElseIf (IsNothing(strItemCode) Or strItemCode = "") Then
                    Continue For
                End If
               

                Dim dblQty As Double = CDbl(dgvGrid.Rows(intRow).Cells("Quantity_" & strFType & "_" & strSType).Value)
                Dim strRemarks As String = IIf(IsDBNull(dgvGrid.Rows(intRow).Cells("Remarks_" & strFType & "_" & strSType).Value), "", dgvGrid.Rows(intRow).Cells("Remarks_" & strFType & "_" & strSType).Value)

                Dim strDislike As String = IIf(IsDBNull(dgvGrid.Rows(intRow).Cells("Dislike_" & strFType & "_" & strSType).Value), "", dgvGrid.Rows(intRow).Cells("Dislike_" & strFType & "_" & strSType).Value)
                Dim strMedical As String = IIf(IsDBNull(dgvGrid.Rows(intRow).Cells("Medical_" & strFType & "_" & strSType).Value), "", dgvGrid.Rows(intRow).Cells("Medical_" & strFType & "_" & strSType).Value)

                sCode = Me.getMaxCode("@Z_OFSL", "Code")
                strQuery = "Select Code From [@Z_OFSL] Where U_ProgramID = '" + strProgramID + "'"
                strQuery += " And Convert(VarChar(8),U_PrgDate,112) = '" + strMenuDate + "'"
                strQuery += " And U_FType = '" + strFType + "'"
                strQuery += " And U_SFood = '" + strSType + "'"
                strQuery += " AND U_ItemCode = '" + strItemCode + "'"
                oRecordSet.DoQuery(strQuery)
                If oRecordSet.EoF Then
                    If Not oUserTable.GetByKey(sCode) Then
                        If strSelect = "Y" Then
                            oUserTable.Code = sCode
                            oUserTable.Name = sCode
                            With oUserTable.UserFields.Fields
                                .Item("U_ProgramID").Value = strProgramID
                                .Item("U_CardCode").Value = strCardCode
                                '.Item("U_PrgCode").Value = strProgram
                                .Item("U_PrgDate").Value = dtPrgDate
                                .Item("U_ItemCode").Value = strItemCode
                                .Item("U_Quantity").Value = dblQty
                                .Item("U_Dislike").Value = strDislike
                                .Item("U_Medical").Value = strMedical
                                .Item("U_FType").Value = strFType
                                .Item("U_SFood").Value = strSType
                                .Item("U_Select").Value = strSelect
                                .Item("U_Remarks").Value = strRemarks
                                .Item("U_Session").Value = strSession
                            End With
                            If oUserTable.Add <> 0 Then
                                Throw New Exception(Singleton.getSAPCompany.GetLastErrorDescription)
                            End If
                        Else
                            Continue For
                        End If
                    End If
                ElseIf oUserTable.GetByKey(oRecordSet.Fields.Item(0).Value.ToString()) Then
                    With oUserTable.UserFields.Fields
                        .Item("U_ProgramID").Value = strProgramID
                        .Item("U_CardCode").Value = strCardCode
                        '.Item("U_PrgCode").Value = strProgram
                        .Item("U_PrgDate").Value = dtPrgDate
                        .Item("U_ItemCode").Value = strItemCode
                        .Item("U_Quantity").Value = dblQty
                        .Item("U_Dislike").Value = strDislike
                        .Item("U_Medical").Value = strMedical
                        .Item("U_FType").Value = strFType
                        .Item("U_SFood").Value = strSType
                        .Item("U_Select").Value = strSelect
                        .Item("U_Remarks").Value = strRemarks
                        .Item("U_Session").Value = strSession
                    End With
                    If oUserTable.Update <> 0 Then
                        Throw New Exception(Singleton.getSAPCompany.GetLastErrorDescription)
                    End If
                End If
            Next

        Catch ex As Exception
            Throw ex
        Finally
            oUserTable = Nothing
        End Try
    End Sub

    Public Function getMaxCode(ByVal sTable As String, ByVal sColumn As String) As String
        Dim oRS As SAPbobsCOM.Recordset
        Dim MaxCode As Integer
        Dim sCode As String
        Dim strSQL As String
        Try
            strSQL = "SELECT MAX(CAST(" & sColumn & " AS Numeric)) FROM [" & sTable & "]"
            ExecuteSQL(oRS, strSQL)
            If Convert.ToString(oRS.Fields.Item(0).Value).Length > 0 Then
                MaxCode = oRS.Fields.Item(0).Value + 1
            Else
                MaxCode = 1
            End If
            sCode = Format(MaxCode, "00000000")
            Return sCode
        Catch ex As Exception
            Throw ex
        Finally
            oRS = Nothing
        End Try
    End Function

    Public Sub ExecuteSQL(ByRef oRecordSet As SAPbobsCOM.Recordset, ByVal SQL As String)
        Try
            If oRecordSet Is Nothing Then
                oRecordSet = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            End If
            oRecordSet.DoQuery(SQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function AddPreSalesOrder(ByVal strCardCode As String, strCardName As String _
                                    , ByVal strProgram As String, ByVal strProgramID As String _
                                    , ByVal strMinDate As String, ByVal strMaxDate As String _
                                     , ByVal strNoofDays As String, ByVal strRemDays As String _
                                    , ByVal IsCons As String _
                                    , ByRef strPSRef As String) As Boolean
        Dim oGeneralService As SAPbobsCOM.GeneralService
        Dim oGeneralData As SAPbobsCOM.GeneralData
        Dim oCompanyService As SAPbobsCOM.CompanyService
        Dim oGeneralDataCollection As SAPbobsCOM.GeneralDataCollection
        Dim oGeneralParams As SAPbobsCOM.GeneralDataParams
        Dim oChildData As SAPbobsCOM.GeneralData
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim strQuery As String = String.Empty
        Dim strCode As String = String.Empty
        oCompanyService = Singleton.getSAPCompany.GetCompanyService()
        Try
            oGeneralService = oCompanyService.GetGeneralService("Z_OPSL")
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)
            oGeneralParams = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)

            Dim intCode As Integer = getMaxCode("@Z_OPSL", "DocEntry")
            strCode = String.Format("{0:000000000}", intCode)
            oGeneralData.SetProperty("U_CardCode", strCardCode)
            oGeneralData.SetProperty("U_CardName", strCardName)
            oGeneralData.SetProperty("U_Program", strProgram)
            oGeneralData.SetProperty("U_ProgramID", strProgramID)
            oGeneralData.SetProperty("U_FromDate", strMinDate)
            oGeneralData.SetProperty("U_TillDate", strMaxDate)
            oGeneralData.SetProperty("U_NoOfDays", strNoofDays)
            oGeneralData.SetProperty("U_RNoOfDays", strRemDays)
            oGeneralData.SetProperty("U_Type", "P")
            oGeneralData.SetProperty("U_IsCon", IIf(IsCons = "True", "Y", "N"))

            strQuery = " Select "
            strQuery += " T0.*,T1.ItemName "
            strQuery += " From [@Z_OFSL] T0 JOIN OITM T1 On T0.U_ItemCode = T1.ItemCode "
            strQuery += " And T0.U_ProgramID = '" + strProgramID + "'"
            strQuery += " And T0.U_Session = '" & txtInstance.Text & "'"
            oRecordSet = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(strQuery)
            If Not oRecordSet.EoF Then
                oGeneralDataCollection = oGeneralData.Child("Z_PSL1")
                Dim intRow As String = 0
                While Not oRecordSet.EoF
                    oChildData = oGeneralDataCollection.Add()
                    oChildData.SetProperty("U_DelDate", oRecordSet.Fields.Item("U_PrgDate").Value.ToString())
                    oChildData.SetProperty("U_PrgCode", strProgram)
                    oChildData.SetProperty("U_FType", oRecordSet.Fields.Item("U_FType").Value)
                    oChildData.SetProperty("U_ItemCode", oRecordSet.Fields.Item("U_ItemCode").Value.ToString())
                    oChildData.SetProperty("U_ItemName", oRecordSet.Fields.Item("ItemName").Value.ToString())
                    oChildData.SetProperty("U_Quantity", oRecordSet.Fields.Item("U_Quantity").Value.ToString())
                    oChildData.SetProperty("U_UnitPrice", "0")
                    oChildData.SetProperty("U_Dislike", oRecordSet.Fields.Item("U_Dislike").Value.ToString())
                    oChildData.SetProperty("U_Medical", oRecordSet.Fields.Item("U_Medical").Value.ToString())
                    oChildData.SetProperty("U_Remarks", oRecordSet.Fields.Item("U_Remarks").Value.ToString())
                    oChildData.SetProperty("U_Status", "O")
                    oChildData.SetProperty("U_SFood", oRecordSet.Fields.Item("U_SFood").Value.ToString())
                    intRow += 1
                    oRecordSet.MoveNext()
                End While
                oGeneralParams = oGeneralService.Add(oGeneralData)
            End If
            strPSRef = oGeneralParams.GetProperty("DocEntry")
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function

    Public Function AddOrder(ByVal strDocEntry As String) As Boolean
        Dim _retVal As Boolean = False
        Try
            Dim oOrder As SAPbobsCOM.Documents
            Dim oRecordSet_H As SAPbobsCOM.Recordset
            Dim oRecordSet As SAPbobsCOM.Recordset

            Dim strQuery As String = String.Empty
            Dim intStatus As Integer
            Try
                oOrder = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
                oRecordSet_H = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecordSet = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                strQuery = "Select U_CardCode,U_CardName,ISNULL(U_IsCon,'N') As  U_IsCon,U_FromDate From [@Z_OPSL] Where DocEntry = '" + strDocEntry + "'"
                oRecordSet_H.DoQuery(strQuery)
                If Not oRecordSet_H.EoF Then
                    Dim strIsCon As String = oRecordSet_H.Fields.Item("U_IsCon").Value
                    oOrder.CardCode = oRecordSet_H.Fields.Item("U_CardCode").Value
                    oOrder.CardName = oRecordSet_H.Fields.Item("U_CardName").Value
                    oOrder.NumAtCard = strDocEntry

                    oOrder.DocDate = CDate(oRecordSet_H.Fields.Item("U_FromDate").Value) 'System.DateTime.Now
                    oOrder.TaxDate = System.DateTime.Now
                    oOrder.DocDueDate = System.DateTime.Now
                    oOrder.Comments = "Pre Sales Booking"
                    oOrder.UserFields.Fields.Item("U_PSNo").Value = strDocEntry
                    oOrder.UserFields.Fields.Item("U_IsCon").Value = strIsCon
                    If strIsCon = "Y" Then
                        oOrder.UserFields.Fields.Item("U_IsCon").Value = "Y"
                        oOrder.UserFields.Fields.Item("U_ConDate").Value = CDate(oRecordSet_H.Fields.Item("U_FromDate").Value)
                    End If
                    Dim intRow As Integer = 0

                    If strIsCon = "N" Then
                        strQuery = "  Select T0.DocEntry,T0.LineId,T0.U_ItemCode,T0.U_Quantity,T0.U_DelDate,T0.U_FType,T0.U_Dislike,T0.U_Medical, "
                        strQuery += " T1.U_Program,T0.U_Remarks,ISNULL(T1.U_IsCon,'N') As  U_IsCon,T1.U_FromDate, "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_SaleEmp,T4.U_SaleEmp),T5.SlpCode),-1) As 'U_SaleEmp', "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_Address,T4.U_Address),T5.ShipToDef),'') As 'U_Address', "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_Building,T4.U_Building),T5.MailBuildi),'') As 'U_Building' "
                        strQuery += " ,T1.U_ProgramID"
                        strQuery += " From [@Z_PSL1] T0 JOIN [@Z_OPSL] T1 On T0.DocEntry = T1.DocEntry "
                        strQuery += " LEFT OUTER JOIN [@Z_OCPR] T2 On T1.U_CardCode = T2.U_CardCode "
                        strQuery += " LEFT OUTER JOIN [@Z_CPR5] T3 On T2.DocEntry = T3.DocEntry "
                        strQuery += " AND Convert(VarChar(8),T0.U_DelDate,112) Between Convert(VarChar(8),T3.U_DelDate,112) And Convert(VarChar(8),T3.U_TDelDate,112) "
                        strQuery += " And ((T3.U_BF = 'Y' AND T0.U_FType = 'BF') "
                        strQuery += " OR (T3.U_LN = 'Y' AND T0.U_FType = 'LN') "
                        strQuery += " OR (T3.U_LS = 'Y' AND T0.U_FType = 'LS') "
                        strQuery += " OR (T3.U_SK = 'Y' AND T0.U_FType = 'SK') "
                        strQuery += " OR (T3.U_DI = 'Y' AND T0.U_FType = 'DI') "
                        strQuery += " OR (T3.U_DS = 'Y' AND T0.U_FType = 'DS')) "
                        strQuery += " LEFT OUTER JOIN [@Z_CPR6] T4 On T2.DocEntry = T4.DocEntry "
                        strQuery += " And ((T4.U_BF = 'Y' AND T0.U_FType = 'BF') "
                        strQuery += " OR (T4.U_LN = 'Y' AND T0.U_FType = 'LN') "
                        strQuery += " OR (T4.U_LS = 'Y' AND T0.U_FType = 'LS') "
                        strQuery += " OR (T4.U_SK = 'Y' AND T0.U_FType = 'SK') "
                        strQuery += " OR (T4.U_DI = 'Y' AND T0.U_FType = 'DI') "
                        strQuery += " OR (T4.U_DS = 'Y' AND T0.U_FType = 'DS')) "
                        strQuery += " AND T4.U_Day = DatePart(DW,T0.U_DelDate) "
                        strQuery += " JOIN OCRD T5 On T5.CardCode = T1.U_CardCode "
                        strQuery += " Where T0.DocEntry = '" + strDocEntry + "' "
                    Else
                        strQuery = "  Select T0.DocEntry,T0.LineId,T0.U_ItemCode,T0.U_Quantity,T0.U_DelDate,T0.U_FType,T0.U_Dislike,T0.U_Medical, "
                        strQuery += " T1.U_Program,T0.U_Remarks,ISNULL(T1.U_IsCon,'N') As  U_IsCon,T1.U_FromDate, "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_SaleEmp,T4.U_SaleEmp),T5.SlpCode),-1) As 'U_SaleEmp', "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_Address,T4.U_Address),T5.ShipToDef),'') As 'U_Address', "
                        strQuery += " ISNULL(ISNULL(ISNULL(T3.U_Building,T4.U_Building),T5.MailBuildi),'') As 'U_Building' "
                        strQuery += " ,T1.U_ProgramID"
                        strQuery += " From [@Z_PSL1] T0 JOIN [@Z_OPSL] T1 On T0.DocEntry = T1.DocEntry "
                        strQuery += " LEFT OUTER JOIN [@Z_OCPR] T2 On T1.U_CardCode = T2.U_CardCode "
                        strQuery += " LEFT OUTER JOIN [@Z_CPR5] T3 On T2.DocEntry = T3.DocEntry "
                        strQuery += " AND Convert(VarChar(8),T1.U_FromDate,112) Between Convert(VarChar(8),T3.U_DelDate,112) And Convert(VarChar(8),T3.U_TDelDate,112) "
                        strQuery += " And ((T3.U_BF = 'Y' AND T0.U_FType = 'BF') "
                        strQuery += " OR (T3.U_LN = 'Y' AND T0.U_FType = 'LN') "
                        strQuery += " OR (T3.U_LS = 'Y' AND T0.U_FType = 'LS') "
                        strQuery += " OR (T3.U_SK = 'Y' AND T0.U_FType = 'SK') "
                        strQuery += " OR (T3.U_DI = 'Y' AND T0.U_FType = 'DI') "
                        strQuery += " OR (T3.U_DS = 'Y' AND T0.U_FType = 'DS')) "
                        strQuery += " LEFT OUTER JOIN [@Z_CPR6] T4 On T2.DocEntry = T4.DocEntry "
                        strQuery += " And ((T4.U_BF = 'Y' AND T0.U_FType = 'BF') "
                        strQuery += " OR (T4.U_LN = 'Y' AND T0.U_FType = 'LN') "
                        strQuery += " OR (T4.U_LS = 'Y' AND T0.U_FType = 'LS') "
                        strQuery += " OR (T4.U_SK = 'Y' AND T0.U_FType = 'SK') "
                        strQuery += " OR (T4.U_DI = 'Y' AND T0.U_FType = 'DI') "
                        strQuery += " OR (T4.U_DS = 'Y' AND T0.U_FType = 'DS')) "
                        strQuery += " AND T4.U_Day = DatePart(DW,T1.U_FromDate) "
                        strQuery += " JOIN OCRD T5 On T5.CardCode = T1.U_CardCode "
                        strQuery += " Where T0.DocEntry = '" + strDocEntry + "' "
                    End If

                    oRecordSet.DoQuery(strQuery)
                    If Not oRecordSet.EoF Then
                        While Not oRecordSet.EoF

                            oOrder.Lines.SetCurrentLine(intRow)

                            oOrder.Lines.ItemCode = oRecordSet.Fields.Item("U_ItemCode").Value
                            oOrder.Lines.Quantity = oRecordSet.Fields.Item("U_Quantity").Value
                            oOrder.Lines.UnitPrice = 0

                            If oRecordSet.Fields.Item("U_IsCon").Value.ToString() = "Y" Then
                                oOrder.Lines.ShipDate = CDate(oRecordSet.Fields.Item("U_FromDate").Value)
                                oOrder.Lines.UserFields.Fields.Item("U_ConDate").Value = CDate(oRecordSet.Fields.Item("U_FromDate").Value)
                                oOrder.Lines.UserFields.Fields.Item("U_IsCon").Value = "Y"
                            Else
                                oOrder.Lines.ShipDate = CDate(oRecordSet.Fields.Item("U_DelDate").Value)
                            End If

                            oOrder.Lines.UserFields.Fields.Item("U_DelDate").Value = CDate(oRecordSet.Fields.Item("U_DelDate").Value)
                            oOrder.Lines.UserFields.Fields.Item("U_PSORef").Value = oRecordSet.Fields.Item("DocEntry").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_PSOLine").Value = oRecordSet.Fields.Item("LineId").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_FType").Value = oRecordSet.Fields.Item("U_FType").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_Dislike").Value = oRecordSet.Fields.Item("U_Dislike").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_Medical").Value = oRecordSet.Fields.Item("U_Medical").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_Program").Value = oRecordSet.Fields.Item("U_Program").Value.ToString()
                            oOrder.Lines.FreeText = oRecordSet.Fields.Item("U_Remarks").Value.ToString()

                            oOrder.Lines.UserFields.Fields.Item("U_Address").Value = oRecordSet.Fields.Item("U_Address").Value.ToString()
                            oOrder.Lines.UserFields.Fields.Item("U_Building").Value = oRecordSet.Fields.Item("U_Building").Value.ToString()

                            strQuery = "Select State From CRD1 Where CardCode = '" & oRecordSet_H.Fields.Item("U_CardCode").Value & "' And AdresType = 'S' "
                            strQuery += " And Address = '" & oRecordSet.Fields.Item("U_Address").Value.ToString() & "'"
                            Dim strState As String = getRecordSetValueString(strQuery, "State")
                            If strState <> "" Then
                                oOrder.Lines.UserFields.Fields.Item("U_State").Value = strState
                            End If

                            oOrder.Lines.SalesPersonCode = CInt(oRecordSet.Fields.Item("U_SaleEmp").Value.ToString())
                            oOrder.Lines.UserFields.Fields.Item("U_ProgramID").Value = oRecordSet.Fields.Item("U_ProgramID").Value.ToString()

                            strQuery = "Select U_PaidType From [@Z_CPM6] "
                            strQuery += " Where DocEntry = '" & oRecordSet.Fields.Item("U_ProgramID").Value.ToString() & "' "
                            strQuery += " AND '" & CDate(oRecordSet.Fields.Item("U_DelDate").Value).ToString("yyyyMMdd") & "' Between Convert(VarChar(8),U_Fdate,112) And Convert(VarChar(8),U_Edate,112) "
                            Dim strPayType As String = getRecordSetValueString(strQuery, "U_PaidType")
                            oOrder.Lines.UserFields.Fields.Item("U_PaidType").Value = strPayType

                            oOrder.Lines.Add()
                            intRow += 1
                            oRecordSet.MoveNext()

                        End While

                        intStatus = oOrder.Add
                        If intStatus = 0 Then
                            _retVal = True
                            Dim strOrder As String = Singleton.getSAPCompany.GetNewObjectKey()

                            'Header
                            strQuery = "Update [@Z_OPSL] Set U_SalesO = '" + strOrder + "' Where DocEntry = '" + strDocEntry + "'"
                            oRecordSet.DoQuery(strQuery)

                            'Rows
                            strQuery = "Update [@Z_PSL1] Set U_Status = 'C' Where DocEntry = '" + strDocEntry + "'"
                            oRecordSet.DoQuery(strQuery)
                        Else
                            _retVal = False

                        End If
                    End If
                End If
                Return _retVal
            Catch ex As Exception
                _retVal = False
            End Try
            Return _retVal
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getRecordSetValue(ByVal strQuery As String, strColumn As String) As Double
        Dim oTemp As SAPbobsCOM.Recordset
        oTemp = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTemp.DoQuery(strQuery)
        Return oTemp.Fields.Item(strColumn).Value
    End Function

    Public Function getRecordSetValueString(ByVal strQuery As String, strColumn As String) As String
        Dim oTemp As SAPbobsCOM.Recordset
        oTemp = Singleton.getSAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oTemp.DoQuery(strQuery)
        Return oTemp.Fields.Item(strColumn).Value
    End Function

    Private Sub clearFoodDataTable()
        Try
            oDt_BF_R.Rows.Clear()
            oDt_BF_C.Rows.Clear()
            oDt_Lunch_R.Rows.Clear()
            oDt_Lunch_C.Rows.Clear()
            oDt_LunchS_R.Rows.Clear()
            oDt_LunchS_C.Rows.Clear()
            oDt_Snack_R.Rows.Clear()
            oDt_Snack_C.Rows.Clear()
            oDt_Dinner_R.Rows.Clear()
            oDt_Dinner_C.Rows.Clear()
            oDt_DinnerS_R.Rows.Clear()
            oDt_DinnerS_C.Rows.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function Validation_Customer(strCardCode As String) As Boolean
        Try
            strQuery = "Select ISNULL(U_CPAdj,0) As U_CPAdj From [@Z_OCPR] Where U_CardCode = '" & strCardCode & "'"
            Dim dblCalories As Double = getRecordSetValue(strQuery, "U_CPAdj")
            If dblCalories <= 0 Then
                Return False
            End If
            strQuery = "Select Address From CRD1 Where CardCode = '" & strCardCode & "' And AdresType = 'S'"
            Dim strAddress As String = getRecordSetValueString(strQuery, "Address")
            If strAddress.Length <= 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
   

End Class