Imports System.Windows.Forms
Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration.ConfigurationManager
Imports System.Threading
Imports System.ComponentModel
Imports SAPbobsCOM

Public Class MainForm

    Public ErrorMsg As New Bx_UI_COM_ErrorMsg.ErrorComponent
    Dim pVal As Integer
    Public oActive As String
    Dim oDr As SqlClient.SqlDataReader
    Private oDT As New DataTable
    Private oDocEntry As Integer
    Public Shared UserSign As Integer
    Public oCurrentForm As New Form
    Public SuperUser As String
    Public oMenuDT As New DataTable
    Public oFormLoading As Boolean = False
    Dim strQry As String
    Private m_ChildFormNumber As Integer

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            UXUTIL.clsUtilities.setAllControlsThemes(Me)
            companyStatusLabel.Text = "Welcome, " & Singleton.getSAPCompany.UserName & ". You are in the Home DIET of " & Singleton.getSAPCompany.CompanyName & "."
            ErrorMsg.InitializeStatusBarLabel(ErrorLabel)
            LoadMenu()
            SCParent.Panel2.BackColor = Color.White
            SCParent.SplitterDistance = 30
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Singleton.DisConnectSAPCompany()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub AlertTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlertTimer.Tick
        Try
            BackgroundWorker1.RunWorkerAsync()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            ' Me.MainForm.showMessage("Added Sucessfully..", MessageType.Success)
        End Try
    End Sub

    Private Sub oTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles oTimer.Tick
        timeStatusLabel.Text = Now
    End Sub

    Private Sub FirstRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FirstRecord.Click
        Try
            'Dim Proceed As Boolean = False
            'oDT.Columns.Clear()
            'oDT.Rows.Clear()
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OINV','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OINV Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                Invoice.LoadInvoiceDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Text = "Add Order" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','ORDR','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From ORDR Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                Invoice.LoadSaleOrder(oDocEntry)
            '            End If
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OPOR','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OPOR Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                PurchaseOrder.LoadPODoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OPDN','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OPDN Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                GRPO.LoadGRPODoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OMRQ"
            '        oDr = oConn.RunQuery("Select Top 1 DocEntry From OWTQ Order By DocEntry Asc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            InventoryRequest.LoadOMRQDoc(oDocEntry)
            '        End If
            '    Case "OWTR"
            '        oDr = oConn.RunQuery("Select Top 1 DocEntry From OWTR Order By DocEntry Asc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            InventoryTransfer.LoadTransferDoc(oDocEntry)
            '        End If
            '    Case "ORCT"
            '        If IncomingPayment.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','ORCT','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From ORCT Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                IncomingPayment.LoadIncomingPayDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OCHH"
            '        strQry = "Buson_POS_Navigate '','OCHH','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '        oDr = oConn.RunQuery(strQry)
            '        'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'K' Order By DeposNum Asc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            ChequeDeposits.LoadChequeDeposit(oDocEntry)
            '        End If
            '    Case "OCRH"
            '        If CreditCardDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OCRH','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'V' Order By DeposNum Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                CreditCardDeposits.LoadCrediCardDeposit(oDocEntry)
            '            End If
            '        End If
            '    Case "CDEP"
            '        If CashDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','CDEP','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'C' Order By DeposNum Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                CashDeposits.LoadCashDeposit(oDocEntry)
            '            End If
            '        End If
            '    Case "Denom"
            '        If CashDenomination.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            oDr = oConn.RunQuery("Select Top 1 DocDate From BUSON_CASHDENOM Where Branch = '" + Login.WhsCode.Trim + "' Order By DocDate Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                CashDenomination.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "OEXPBOOK"
            '        If ExpenseBooking.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OEXPBOOK','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OVPM Order By DocEntry Asc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                ExpenseBooking.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "GoodsReturn"
            '        If GoodsReturn.btnAdd.Text = "Add Return" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','GoodsReturn','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                GoodsReturn.LoadReturnDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "APInvoice"
            '        If APInvoice.btnAdd.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','APInvoice','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                APInvoice.LoadInvoiceDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "BulkOutWard"
            '        If BulkOutWard.btnGenerate.Text = "Generate OutWard" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','BulkOutWard','First','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                BulkOutWard.LoadBulkOutWardDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub LastRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastRecord.Click
        Try
            'Dim Proceed As Boolean = False
            'oDT.Columns.Clear()
            'oDT.Rows.Clear()
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OINV','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OINV Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                Invoice.LoadInvoiceDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Text = "Add Order" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','ORDR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From ORDR Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                Invoice.LoadSaleOrder(oDocEntry)
            '            End If
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OPOR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OPOR Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                PurchaseOrder.LoadPODoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OPDN','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OPDN Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                GRPO.LoadGRPODoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OMRQ"
            '        oDr = oConn.RunQuery("Select Top 1 DocEntry From OWTQ Order By DocEntry Desc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            InventoryRequest.LoadOMRQDoc(oDocEntry)
            '        End If
            '    Case "OWTR"
            '        oDr = oConn.RunQuery("Select Top 1 DocEntry From OWTR T1 Order By DocEntry Desc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            InventoryTransfer.LoadTransferDoc(oDocEntry)
            '        End If
            '    Case "ORCT"
            '        If IncomingPayment.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','ORCT','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From ORCT T1 Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                IncomingPayment.LoadIncomingPayDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OCHH"
            '        strQry = "Buson_POS_Navigate '','OCHH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '        oDr = oConn.RunQuery(strQry)
            '        'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'K' Order By DeposNum Desc")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            ChequeDeposits.LoadChequeDeposit(oDocEntry)
            '        End If
            '    Case "OCRH"
            '        If CreditCardDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OCRH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'V' Order By DeposNum Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                CreditCardDeposits.LoadCrediCardDeposit(oDocEntry)
            '            End If
            '        End If
            '    Case "CDEP"
            '        If CashDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','CDEP','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'C' Order By DeposNum Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                CashDeposits.LoadCashDeposit(oDocEntry)
            '            End If
            '        End If
            '    Case "Denom"
            '        If CashDenomination.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            oDr = oConn.RunQuery("Select Top 1 DocDate From BUSON_CASHDENOM Where Branch = '" + Login.WhsCode.Trim + "' Order By DocDate Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                CashDenomination.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "OEXPBOOK"
            '        If ExpenseBooking.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','OEXPBOOK','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OVPM T1 Order By DocEntry Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                ExpenseBooking.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "GoodsReturn"
            '        If GoodsReturn.btnAdd.Text = "Add Return" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','GoodsReturn','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                GoodsReturn.LoadReturnDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "APInvoice"
            '        If APInvoice.btnAdd.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','APInvoice','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                APInvoice.LoadInvoiceDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "BulkOutWard"
            '        If BulkOutWard.btnGenerate.Text = "Generate OutWard" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            strQry = "Buson_POS_Navigate '','BulkOutWard','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                BulkOutWard.LoadBulkOutWardDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub Previous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Previous.Click
        Try
            'Dim Proceed As Boolean = False
            'oDT.Columns.Clear()
            'oDT.Rows.Clear()
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = Invoice.cmbDocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OINV T1 Where DocNum = " + DocNum.ToString + "")
            '            If Invoice.InvDocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OINV','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + Invoice.InvDocEnt.Text + "','OINV','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    Invoice.LoadInvoiceDoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Text = "Add Order" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = Invoice.cmbDocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select DocEntry From ORDR T1 Where DocNum = " + DocNum.ToString + "")
            '            If Invoice.InvDocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','ORDR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + Invoice.InvDocEnt.Text + "','ORDR','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    Invoice.LoadSaleOrder(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = PurchaseOrder.cmbDocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OPOR T1 Where DocNum = " + DocNum.ToString + "")
            '            If PurchaseOrder.PODocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OPOR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + PurchaseOrder.PODocEnt.Text + "','OPOR','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    PurchaseOrder.LoadPODoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = GRPO.cmbDocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OPDN T1 Where DocNum = " + DocNum.ToString + "")
            '            If GRPO.GRPODocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OPDN','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + GRPO.GRPODocEnt.Text + "','OPDN','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    GRPO.LoadGRPODoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OMRQ"
            '        Dim DocNum As Integer = InventoryRequest.txtDocNum.Text
            '        DocNum -= 1
            '        oDr = oConn.RunQuery("Select DocEntry From OWTQ T1 Where DocNum = " + DocNum.ToString + "")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                InventoryRequest.LoadOMRQDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OWTR"
            '        Dim DocNum As Integer = InventoryTransfer.DocNum.Text
            '        DocNum -= 1
            '        oDr = oConn.RunQuery("Select DocEntry From OWTR T1 Where DocNum = " + DocNum.ToString + "")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                InventoryTransfer.LoadTransferDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "ORCT"
            '        If IncomingPayment.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = IncomingPayment.DocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select DocEntry From ORCT T1 Where DocNum = " + DocNum.ToString + "")
            '            If IncomingPayment.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','ORCT','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + IncomingPayment.DocEntry.Text + "','ORCT','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    IncomingPayment.LoadIncomingPayDoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OCHH"
            '        'Dim DocNum As Integer = ChequeDeposits.DocNum.Text
            '        'DocNum -= 1
            '        'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum <= " + DocNum.ToString + " And DeposType = 'K' Order By DeposNum Desc")
            '        If ChequeDeposits.DocEntry.Text = String.Empty Then
            '            strQry = "Buson_POS_Navigate '','OCHH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '        Else
            '            strQry = "Buson_POS_Navigate '" + ChequeDeposits.DocEntry.Text + "','OCHH','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '        End If
            '        oDr = oConn.RunQuery(strQry)
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                ChequeDeposits.LoadChequeDeposit(oDocEntry)
            '            End If
            '        End If
            '    Case "OCRH"
            '        If CreditCardDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = CreditCardDeposits.DocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum <= " + DocNum.ToString + " And DeposType = 'V' Order By DeposNum Desc")
            '            If CreditCardDeposits.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OCRH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + CreditCardDeposits.DocEntry.Text + "','OCRH','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    CreditCardDeposits.LoadCrediCardDeposit(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "CDEP"
            '        If CashDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = CashDeposits.DocNum.Text
            '            'DocNum -= 1
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum <= " + DocNum.ToString + " And DeposType = 'C' Order By DeposNum Desc")
            '            If CashDeposits.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','CDEP','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + CashDeposits.DocEntry.Text + "','CDEP','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    CashDeposits.LoadCashDeposit(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "Denom"
            '        If CashDenomination.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            Dim DocDate As Date = CashDenomination.dtpDocDate.Value
            '            oDr = oConn.RunQuery("Select Top 1 DocDate From BUSON_CASHDENOM Where Branch = '" + Login.WhsCode + "' And DocDate < '" + DocDate.ToString("yyyyMMdd") + "' Order By DocDate Desc")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                CashDenomination.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "OEXPBOOK"
            '        If ExpenseBooking.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = ExpenseBooking.txtDocNum.Text
            '            ''DocNum -= 1
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OVPM T1 Where DocNum < " + DocNum.ToString + " Order By DocEntry Desc")
            '            If ExpenseBooking.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OEXPBOOK','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + ExpenseBooking.txtDocEntry.Text + "','OEXPBOOK','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    ExpenseBooking.LoadDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "GoodsReturn"
            '        If GoodsReturn.btnAdd.Text = "Add Return" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If GoodsReturn.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','GoodsReturn','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + GoodsReturn.txtDocEntry.Text + "','GoodsReturn','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    GoodsReturn.LoadReturnDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "APInvoice"
            '        If APInvoice.btnAdd.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If APInvoice.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','APInvoice','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + APInvoice.txtDocEntry.Text + "','APInvoice','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    APInvoice.LoadInvoiceDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "BulkOutWard"
            '        If BulkOutWard.btnGenerate.Text = "Generate OutWard" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If BulkOutWard.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','BulkOutWard','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + BulkOutWard.txtDocEntry.Text + "','BulkOutWard','Previous','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    BulkOutWard.LoadBulkOutWardDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub NextRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextRecord.Click
        Try
            'Dim Proceed As Boolean = False
            'oDT.Columns.Clear()
            'oDT.Rows.Clear()
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = Invoice.cmbDocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OINV T1 Where DocNum = " + DocNum.ToString + "")
            '            If Invoice.InvDocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OINV','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + Invoice.InvDocEnt.Text + "','OINV','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    Invoice.LoadInvoiceDoc(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DocEntry From OINV Order By DocEntry Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        Invoice.LoadInvoiceDoc(oDocEntry)
            '                '    End If
            '            End If
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Text = "Add Order" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = Invoice.cmbDocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select DocEntry From ORDR T1 Where DocNum = " + DocNum.ToString + "")
            '            If Invoice.InvDocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','ORDR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + Invoice.InvDocEnt.Text + "','ORDR','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    Invoice.LoadSaleOrder(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DocEntry From ORDR Order By DocEntry Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        Invoice.LoadSaleOrder(oDocEntry)
            '                '    End If
            '            End If
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = PurchaseOrder.cmbDocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OPOR T1 Where DocNum = " + DocNum.ToString + "")
            '            If PurchaseOrder.PODocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OPOR','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + PurchaseOrder.PODocEnt.Text + "','OPOR','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    PurchaseOrder.LoadPODoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = GRPO.cmbDocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select DocEntry From OPDN T1 Where DocNum = " + DocNum.ToString + "")
            '            If GRPO.GRPODocEnt.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OPDN','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + GRPO.GRPODocEnt.Text + "','OPDN','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    GRPO.LoadGRPODoc(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "OMRQ"
            '        Dim DocNum As Integer = InventoryRequest.txtDocNum.Text
            '        DocNum += 1
            '        oDr = oConn.RunQuery("Select DocEntry From OWTQ T1 Where DocNum = " + DocNum.ToString + "")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                InventoryRequest.LoadOMRQDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "OWTR"
            '        Dim DocNum As Integer = InventoryTransfer.DocNum.Text
            '        DocNum += 1
            '        oDr = oConn.RunQuery("Select DocEntry From OWTR T1 Where DocNum = " + DocNum.ToString + "")
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                InventoryTransfer.LoadTransferDoc(oDocEntry)
            '            End If
            '        End If
            '    Case "ORCT"
            '        If IncomingPayment.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = IncomingPayment.DocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select DocEntry From ORCT T1 Where DocNum = " + DocNum.ToString + "")
            '            If IncomingPayment.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','ORCT','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + IncomingPayment.DocEntry.Text + "','ORCT','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            oDr.Close()
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    IncomingPayment.LoadIncomingPayDoc(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DocEntry From ORCT Order By DocEntry Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        If oDocEntry > 0 Then
            '                '            IncomingPayment.LoadIncomingPayDoc(oDocEntry)
            '                '        End If
            '                '    End If
            '            End If
            '        End If
            '    Case "OCHH"
            '        'Dim DocNum As Integer = ChequeDeposits.DocNum.Text
            '        'DocNum += 1
            '        'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum >= " + DocNum.ToString + " And DeposType = 'K'")
            '        If ChequeDeposits.DocEntry.Text = String.Empty Then
            '            strQry = "Buson_POS_Navigate '','OCHH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '        Else
            '            strQry = "Buson_POS_Navigate '" + ChequeDeposits.DocEntry.Text + "','OCHH','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '        End If
            '        oDr = oConn.RunQuery(strQry)
            '        oDT.Rows.Clear()
            '        oDT.Load(oDr)
            '        If oDT.Rows.Count > 0 Then
            '            oDocEntry = oDT.Rows(0).Item(0)
            '            If oDocEntry > 0 Then
            '                ChequeDeposits.LoadChequeDeposit(oDocEntry)
            '            End If
            '            'Else
            '            '    oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'K' Order By DeposNum Desc")
            '            '    oDT.Rows.Clear()
            '            '    oDT.Load(oDr)
            '            '    If oDT.Rows.Count > 0 Then
            '            '        oDocEntry = oDT.Rows(0).Item(0)
            '            '        ChequeDeposits.LoadChequeDeposit(oDocEntry)
            '            '    End If
            '        End If
            '    Case "OCRH"
            '        If CreditCardDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = CreditCardDeposits.DocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum >= " + DocNum.ToString + " And DeposType = 'V'")
            '            If CreditCardDeposits.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OCRH','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + CreditCardDeposits.DocEntry.Text + "','OCRH','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    CreditCardDeposits.LoadCrediCardDeposit(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'V' Order By DeposNum Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        CreditCardDeposits.LoadCrediCardDeposit(oDocEntry)
            '                '    End If
            '            End If
            '        End If
            '    Case "CDEP"
            '        If CashDeposits.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = CashDeposits.DocNum.Text
            '            'DocNum += 1
            '            'oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS T1 Where DeposNum >= " + DocNum.ToString + " And DeposType = 'C'")
            '            If CashDeposits.DocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','CDEP','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + CashDeposits.DocEntry.Text + "','CDEP','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    CashDeposits.LoadCashDeposit(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DeposNum From ODPS Where DeposType = 'C' Order By DeposNum Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        CashDeposits.LoadCashDeposit(oDocEntry)
            '                '    End If
            '            End If
            '        End If
            '    Case "Denom"
            '        If CashDenomination.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            Dim DocDate As Date = CashDenomination.dtpDocDate.Value
            '            oDr = oConn.RunQuery("Select Top 1 DocDate From BUSON_CASHDENOM Where Branch = '" + Login.WhsCode + "' And DocDate > '" + DocDate.ToString("yyyyMMdd") + "'")
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            If oDT.Rows.Count > 0 Then
            '                CashDenomination.LoadDocument(oDT.Rows(0).Item(0).ToString)
            '            End If
            '        End If
            '    Case "OEXPBOOK"
            '        If ExpenseBooking.btnAdd.Text = "Add" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            'Dim DocNum As Integer = ExpenseBooking.txtDocNum.Text
            '            ''DocNum += 1
            '            'oDr = oConn.RunQuery("Select Top 1 DocEntry From OVPM T1 Where DocNum > " + DocNum.ToString + "")
            '            If ExpenseBooking.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','OEXPBOOK','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + ExpenseBooking.txtDocEntry.Text + "','OEXPBOOK','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            oDr.Close()
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    ExpenseBooking.LoadDocument(oDocEntry)
            '                End If
            '                'Else
            '                '    oDr = oConn.RunQuery("Select Top 1 DocEntry From OVPM Order By DocEntry Desc")
            '                '    oDT.Rows.Clear()
            '                '    oDT.Load(oDr)
            '                '    If oDT.Rows.Count > 0 Then
            '                '        oDocEntry = oDT.Rows(0).Item(0)
            '                '        If oDocEntry > 0 Then
            '                '            ExpenseBooking.LoadDocument(oDocEntry)
            '                '        End If
            '                '    End If
            '            End If
            '        End If
            '    Case "GoodsReturn"
            '        If GoodsReturn.btnAdd.Text = "Add Return" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If GoodsReturn.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','GoodsReturn','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + GoodsReturn.txtDocEntry.Text + "','GoodsReturn','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            oDr.Close()
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    GoodsReturn.LoadReturnDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "APInvoice"
            '        If APInvoice.btnAdd.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If APInvoice.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','APInvoice','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + APInvoice.txtDocEntry.Text + "','APInvoice','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            oDr.Close()
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    APInvoice.LoadInvoiceDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            '    Case "BulkOutWard"
            '        If BulkOutWard.btnGenerate.Text = "Generate OutWard" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            End If
            '        Else
            '            Proceed = True
            '        End If
            '        If Proceed = True Then
            '            If BulkOutWard.txtDocEntry.Text = String.Empty Then
            '                strQry = "Buson_POS_Navigate '','BulkOutWard','Last','" + Login.WhsCode + "','" + SuperUser + "'"
            '            Else
            '                strQry = "Buson_POS_Navigate '" + BulkOutWard.txtDocEntry.Text + "','BulkOutWard','Next','" + Login.WhsCode + "','" + SuperUser + "'"
            '            End If
            '            oDr = oConn.RunQuery(strQry)
            '            oDT.Rows.Clear()
            '            oDT.Load(oDr)
            '            oDr.Close()
            '            If oDT.Rows.Count > 0 Then
            '                oDocEntry = oDT.Rows(0).Item(0)
            '                If oDocEntry > 0 Then
            '                    BulkOutWard.LoadBulkOutWardDocument(oDocEntry)
            '                End If
            '            End If
            '        End If
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Sub

    Private Sub PDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDF.Click
        Try
            'Select Case oActive
            '    Case "OINV"
            '        Invoice.OpenPDFFile()
            '    Case "ORCT"
            '        IncomingPayment.OpenPDFFile()
            '    Case "OEXPBOOK"
            '        ExpenseBooking.OpenPDFFile()
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub AddMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMode.Click
        Try
            Dim Proceed As Boolean = True
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Visible = True And Invoice.AddInv.Text = "Add Invoice" Then
            '            Proceed = False
            '        End If
            '        If Proceed Then
            '            Invoice.LoadAddMode()
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Visible = True And Invoice.AddOrder.Text = "Add Order" Then
            '            Proceed = False
            '        End If
            '        If Proceed Then
            '            Invoice.LoadAddMode()
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Proceed = False
            '        End If
            '        If Proceed Then
            '            PurchaseOrder.LoadAddMode()
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Proceed = False
            '        End If
            '        If Proceed Then
            '            GRPO.LoadAddMode()
            '        End If
            '    Case "OMRQ"
            '        InventoryRequest.LoadAddMode()
            '    Case "OWTR"
            '        InventoryTransfer.LoadAddMode()
            '    Case "ORCT"
            '        IncomingPayment.LoadAddMode()
            '    Case "CDEP"
            '        CashDeposits.LoadAddMode()
            '    Case "OCHH"
            '        ChequeDeposits.LoadAddMode()
            '    Case "OCRH"
            '        CreditCardDeposits.LoadAddMode()
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub FindMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindMode.Click
        Try
            Dim Proceed As Boolean = True
            'Select Case oActive
            '    Case "OINV"
            '        If Invoice.AddInv.Visible = True And Invoice.AddInv.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            Else
            '                Proceed = False
            '            End If
            '        End If
            '        If Proceed Then
            '            Invoice.LoadFindMode()
            '        End If
            '    Case "ORDR"
            '        If Invoice.AddOrder.Visible = True And Invoice.AddOrder.Text = "Add Order" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            Else
            '                Proceed = False
            '            End If
            '        End If
            '        If Proceed Then
            '            Invoice.LoadFindMode()
            '        End If
            '    Case "OPOR"
            '        If PurchaseOrder.AddPO.Text = "Add PO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            Else
            '                Proceed = False
            '            End If
            '        End If
            '        If Proceed Then
            '            PurchaseOrder.LoadFindMode()
            '        End If
            '    Case "OPDN"
            '        If GRPO.AddGRPO.Text = "Add GRPO" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Confirmation", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                Proceed = True
            '            Else
            '                Proceed = False
            '            End If
            '        End If
            '        If Proceed Then
            '            GRPO.LoadFindMode()
            '        End If
            '    Case "OMRQ"
            '        InventoryRequest.LoadFindMode()
            '    Case "OWTR"
            '        InventoryTransfer.LoadFindMode()
            '    Case "ORCT"
            '        IncomingPayment.LoadFindMode()
            '    Case "CDEP"
            '        CashDeposits.LoadFindMode()
            '    Case "OCHH"
            '        ChequeDeposits.LoadFindMode()
            '    Case "OCRH"
            '        CreditCardDeposits.LoadFindMode()
            '    Case "GoodsReturn"
            '        If GoodsReturn.btnAdd.Text = "Add Return" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "Goods Return", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                GoodsReturn.LoadFindMode()
            '            End If
            '        Else
            '            GoodsReturn.LoadFindMode()
            '        End If
            '    Case "APInvoice"
            '        If APInvoice.btnAdd.Text = "Add Invoice" Then
            '            Dim oMsgResult As MsgBoxResult
            '            oMsgResult = MsgBoxNew.Shows("Unsaved data will be lost. Do you want to continue?", "AP Invoice", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '            If oMsgResult = MsgBoxResult.Yes Then
            '                APInvoice.LoadFindMode()
            '            End If
            '        Else
            '            APInvoice.LoadFindMode()
            '        End If
            'End Select
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub LogOffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOffToolStripMenuItem.Click
        Try
            Me.Close()
        Catch ex As Exception
            'ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub SCParent_Panel2_ControlRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles SCParent.Panel2.ControlRemoved
        'MenuTree.SelectedNode = MenuTree.SelectedNode.Parent
        Try
            If SCParent.Panel2.Controls.Count = 0 Then
                'If oFormLoading = False Then
                '    Dim oDashDt As DataTable = oMenuDT.Copy
                '    oDashDt.DefaultView.RowFilter = "ReportType = 'D'"
                '    If oDashDt.DefaultView.ToTable().Rows.Count > 0 Then
                '        reloadDashBoard()
                '    Else
                '        'SCParent.Panel2.BackgroundImage = Global.POS.My.Resources.POSLogo
                '    End If
                'End If
            End If
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub MenuTree_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles MenuTree.NodeMouseClick
        Try
            Dim Proceed As Boolean = True
            oFormLoading = True
            'Select Case e.Node.Name
            '    Case "menuInvoice"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            oCurrentForm = PSWizard
            oCurrentForm.TopLevel = False
            oCurrentForm.Parent = Me.SCParent.Panel2
            oCurrentForm.Dock = DockStyle.Fill
            'If SCParent.SplitterDistance = 226 Then
            '    oCurrentForm.Dock = DockStyle.Fill
            'ElseIf SCParent.SplitterDistance = 30 Then
            '    oCurrentForm.Dock = DockStyle.None
            '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
            '    oCurrentForm.Height = SCParent.Panel2.Height
            'End If
            oActive = "OINV"
            oCurrentForm.Show()
            oCurrentForm.Focus()
            oCurrentForm.Select()
            Me.ActiveControl = Me.SCParent.Panel2
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSaleOrder"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = Invoice
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    'If SCParent.SplitterDistance = 226 Then
            '                    '    oCurrentForm.Dock = DockStyle.Fill
            '                    'ElseIf SCParent.SplitterDistance = 30 Then
            '                    '    oCurrentForm.Dock = DockStyle.None
            '                    '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
            '                    '    oCurrentForm.Height = SCParent.Panel2.Height
            '                    'End If
            '                    oActive = "ORDR"
            '                    Invoice.strFormName = "SaleOrder"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSalesIncentiveShare"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = SalesIncentiveShare
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuCombi"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    ExpCfl.Close()
            '                    ExpCfl.strCurrentForm = "Combi"
            '                    oCurrentForm = ExpCfl
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "CombiCFL"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuOrdertoInvoice"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    ExpCfl.Close()
            '                    ExpCfl.strCurrentForm = "OrdertoInvoice"
            '                    oCurrentForm = ExpCfl
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OrdertoInvoice"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuInvoiceToCreditBill"
            '        If CheckUserInitialized() Then
            '            If oConfigDT.Rows(0).Item("AlSalesRet") = "Y" Then
            '                If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                    If SCParent.Panel2.HasChildren Then
            '                        If Not oCurrentForm.Name = "Dashboard" Then
            '                            Dim oMsgResult As MsgBoxResult
            '                            oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                            If oMsgResult = MsgBoxResult.Yes Then
            '                                Proceed = True
            '                            Else
            '                                Proceed = False
            '                            End If
            '                        End If
            '                    End If
            '                    If Proceed Then
            '                        oCurrentForm.Close()
            '                        ExpCfl.Close()
            '                        ExpCfl.strCurrentForm = "InvoiceToCreditBill"
            '                        oCurrentForm = ExpCfl
            '                        oCurrentForm.TopLevel = False
            '                        oCurrentForm.Parent = Me.SCParent.Panel2
            '                        oCurrentForm.Dock = DockStyle.Fill
            '                        oActive = "InvoiceToCreditBill"
            '                        oCurrentForm.Show()
            '                    End If
            '                Else
            '                    ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuExchangeSale"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ExchangeSale
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ExchangeSale"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuGiftOfferInvoice"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = GiftOfferInvoice
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "menuGiftOfferInvoice"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPO"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = PurchaseOrder
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    'If SCParent.SplitterDistance = 226 Then
            '                    '    oCurrentForm.Dock = DockStyle.Fill
            '                    'ElseIf SCParent.SplitterDistance = 30 Then
            '                    '    oCurrentForm.Dock = DockStyle.None
            '                    '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
            '                    '    oCurrentForm.Height = SCParent.Panel2.Height
            '                    'End If
            '                    oActive = "OPOR"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPOToGRPO"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    ExpCfl.Close()
            '                    ExpCfl.strCurrentForm = "POtoGRPO"
            '                    oCurrentForm = ExpCfl
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "POtoGRPO"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuGRPO"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = GRPO
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    'If SCParent.SplitterDistance = 226 Then
            '                    '    oCurrentForm.Dock = DockStyle.Fill
            '                    'ElseIf SCParent.SplitterDistance = 30 Then
            '                    '    oCurrentForm.Dock = DockStyle.None
            '                    '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
            '                    '    oCurrentForm.Height = SCParent.Panel2.Height
            '                    'End If
            '                    oActive = "OPDN"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuInvReq"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = InventoryRequest
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OMRQ"
            '                    oCurrentForm.Text = "Stock Transfer Request"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuTransToTransit"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = InventoryTransfer
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OWTR"
            '                    oCurrentForm.Text = "Transfer To Transit"
            '                    oCurrentForm.Show()
            '                    InventoryTransfer.SetFormMode("TransToTransit")
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuRecFrmTransit"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = InventoryTransfer
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OWTR"
            '                    oCurrentForm.Text = "Receipt From Transit"
            '                    oCurrentForm.Show()
            '                    InventoryTransfer.SetFormMode("RecFrmTransit")
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuRetFrmTransit"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = InventoryTransfer
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OWTR"
            '                    oCurrentForm.Text = "Return From Transit"
            '                    oCurrentForm.Show()
            '                    InventoryTransfer.SetFormMode("RetFrmTransit")
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If

            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuInPaymt"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = IncomingPayment
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ORCT"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuExpSetup"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ExpenseBookingSetup
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OEXPSETUP"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuExpBook"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ExpenseBooking
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    'oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OEXPBOOK"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuUsers"
            '        If SuperUser = "Y" Then
            '            If SCParent.Panel2.HasChildren Then
            '                If Not oCurrentForm.Name = "Dashboard" Then
            '                    Dim oMsgResult As MsgBoxResult
            '                    oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                    If oMsgResult = MsgBoxResult.Yes Then
            '                        Proceed = True
            '                    Else
            '                        Proceed = False
            '                    End If
            '                End If
            '            End If
            '            If Proceed Then
            '                oCurrentForm.Close()
            '                oCurrentForm = UserSetup
            '                oCurrentForm.TopLevel = False
            '                oCurrentForm.Parent = Me.SCParent.Panel2
            '                'oCurrentForm.Dock = DockStyle.Fill
            '                oActive = String.Empty
            '                oCurrentForm.Show()
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSysInit"
            '        If SuperUser = "Y" Then
            '            If SCParent.Panel2.HasChildren Then
            '                If Not oCurrentForm.Name = "Dashboard" Then
            '                    Dim oMsgResult As MsgBoxResult
            '                    oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                    If oMsgResult = MsgBoxResult.Yes Then
            '                        Proceed = True
            '                    Else
            '                        Proceed = False
            '                    End If
            '                End If
            '            End If
            '            If Proceed Then
            '                oCurrentForm.Close()
            '                oCurrentForm = Settings
            '                oCurrentForm.TopLevel = False
            '                oCurrentForm.Parent = Me.SCParent.Panel2
            '                'oCurrentForm.Dock = DockStyle.Fill
            '                oActive = String.Empty
            '                oCurrentForm.Show()
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuCheque"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ChequeDeposits
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OCHH"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuCreditCard"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = CreditCardDeposits
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "OCRH"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuCash"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = CashDeposits
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "CDEP"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuCashDenom"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = CashDenomination
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oActive = "Denom"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuROLSetUp"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ROLImport
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ROL"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuROLDisplay"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ROLDisplayMatrix
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ROLDisplay"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPreVen"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = PreferredVendorSetup
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "PreVen"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSendMail"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = Mailer
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "SendMail"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuROLWizard"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ROLWizard
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ROLWizard"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPOCancel"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = POCancel
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "POCancel"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuTransInward"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = TransferInward
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "TransInward"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuTransOutward"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = TransferOutward
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "TransOutWard"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuReportConfig"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ReportConfig
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    'oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "ReportConfig"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSerNumTrans"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = SerialNumMovement
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "SerNumTrans"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuStockTaking"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = StockTaking
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.None
            '                    oActive = "StockTaking"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPointsTarget"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = IncentiveSetup
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuSalesIncentive"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = SalesIncentive
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuQuerySetup"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = QuerySetup
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuPriceImport"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = ImportXL
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuQueryGen"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = QueryGen
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "QueryGenerator"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuGRPOToGoodsReturn"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    ExpCfl.Close()
            '                    ExpCfl.strCurrentForm = "GRPOToGoodsReturn"
            '                    oCurrentForm = ExpCfl
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "GRPOToGoodsReturn"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuGRPOToAPInvoice"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    ExpCfl.Close()
            '                    ExpCfl.strCurrentForm = "GRPOToAPInvoice"
            '                    oCurrentForm = ExpCfl
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = "GRPOToAPInvoice"
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuEmpSalesTarget1"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    EmployeeSalesTarget.FormType = "MT"
            '                    EmployeeSalesTarget.Text = "Employee Sales Target - Main Products"
            '                    oCurrentForm = EmployeeSalesTarget
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuEmpSalesTarget2"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    EmployeeSalesTarget.FormType = "A"
            '                    EmployeeSalesTarget.Text = "Employee Sales Target - Other Products"
            '                    oCurrentForm = EmployeeSalesTarget
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuBranchIncentive"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = BranchIncentiveSetup
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuFestiveCombiOffer"
            '        If CheckUserInitialized() Then
            '            If SuperUser = "Y" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = FestiveCombiOffer
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuBulkOutWard"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = BulkOutWard
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case "menuBulkInWard"
            '        If CheckUserInitialized() Then
            '            If oAuthDetailsHT.Item(e.Node.Name) = "F" Then
            '                If SCParent.Panel2.HasChildren Then
            '                    If Not oCurrentForm.Name = "Dashboard" Then
            '                        Dim oMsgResult As MsgBoxResult
            '                        oMsgResult = MsgBoxNew.Shows("Are you sure you want to Close This Screen?", "Confirm", MsgBoxNew.Buttons.YesNo, MsgBoxNew.Icons.Question, MsgBoxNew.AnimateStyle.FadeIn)
            '                        If oMsgResult = MsgBoxResult.Yes Then
            '                            Proceed = True
            '                        Else
            '                            Proceed = False
            '                        End If
            '                    End If
            '                End If
            '                If Proceed Then
            '                    oCurrentForm.Close()
            '                    oCurrentForm = BulkInWard
            '                    oCurrentForm.TopLevel = False
            '                    oCurrentForm.Parent = Me.SCParent.Panel2
            '                    oCurrentForm.Dock = DockStyle.Fill
            '                    oActive = String.Empty
            '                    oCurrentForm.Show()
            '                End If
            '            Else
            '                ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '            End If
            '        Else
            '            ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '        End If
            '    Case Else
            '        For i = 0 To oMenuDT.Rows.Count - 1
            '            If oMenuDT.Rows(i).Item("MenuID").ToString = e.Node.Name Then
            '                If oMenuDT.Rows(i).Item("ReportType").ToString = "R" Or oMenuDT.Rows(i).Item("ReportType").ToString = "D" Then
            '                    If oMenuDT.Rows(i).Item("SourceType").ToString = "S" Then
            '                        Dim oSSRSReport As New SSRSReport 'Thhis should be replaced with sys info..ok... ok..
            '                        'Dim orsCredentials As IReportServerCredentials = New MyReportServerCredentials("eshvanth", "abc@123", "victory")
            '                        Dim orsCredentials As IReportServerCredentials = New MyReportServerCredentials(oMenuDT.Rows(i).Item("ServerUserName").ToString, oMenuDT.Rows(i).Item("ServerPassword").ToString, oMenuDT.Rows(i).Item("ServerDomain").ToString)
            '                        oSSRSReport.ReportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = orsCredentials.NetworkCredentials

            '                        oSSRSReport.ReportViewer1.ServerReport.ReportServerUrl = New Uri(oMenuDT.Rows(i).Item("Path").ToString.Trim)
            '                        oSSRSReport.ReportViewer1.ServerReport.ReportPath = IIf(oMenuDT.Rows(i).Item("Folder").ToString.Trim = String.Empty, "/" + oMenuDT.Rows(i).Item("FileName").ToString.Trim, "/" + oMenuDT.Rows(i).Item("Folder").ToString.Trim + "/" + oMenuDT.Rows(i).Item("FileName").ToString.Trim)

            '                        If oMenuDT.Rows(i).Item("ParamName").ToString.Trim() <> "" Then
            '                            Dim oParam As New ReportParameter()
            '                            oParam.Name = oMenuDT.Rows(i).Item("ParamName").ToString
            '                            oParam.Values.Add(Login.WhsCode)
            '                            If CheckSuperUser() = "Y" Then
            '                                oParam.Visible = True
            '                            Else
            '                                oParam.Visible = False
            '                            End If
            '                            'Set the report parameters for the report
            '                            Dim parameters() As ReportParameter = {oParam}
            '                            oSSRSReport.ReportViewer1.ServerReport.SetParameters(parameters)
            '                        End If

            '                        'Dim backgroundThread1 As New Thread(New ThreadStart(Function()
            '                        '                                                        oSSRSReport.Text = oMenuDT.Rows(i).Item("MenuName").ToString
            '                        '                                                        oSSRSReport.ReportViewer1.RefreshReport()
            '                        '                                                    End Function))

            '                        'backgroundThread1.Start()

            '                        Dim backgroundThread As New Thread(New ThreadStart(Function()
            '                                                                               ExeProgress.Title.Text = "Loading Report...!!  Please Wait!!"
            '                                                                               ExeProgress.SetEdge(1)
            '                                                                           End Function))

            '                        backgroundThread.Start()

            '                        oSSRSReport.Text = oMenuDT.Rows(i).Item("MenuName").ToString
            '                        oSSRSReport.ReportViewer1.RefreshReport()
            '                        oCurrentForm.Close()
            '                        oCurrentForm = oSSRSReport
            '                        oCurrentForm.TopLevel = False
            '                        oCurrentForm.Parent = Me.SCParent.Panel2
            '                        oCurrentForm.Dock = DockStyle.Fill
            '                        oActive = String.Empty
            '                        oCurrentForm.Show()



            '                        Application.DoEvents()
            '                        ExeProgress.PBar.Dispose()
            '                        ExeProgress.Close()
            '                        backgroundThread.Abort()
            '                        'backgroundThread1.Abort()
            '                        Exit Sub
            '                    Else
            '                        Dim oReport As Report = New Report(0, "Reports")
            '                        oReport.ParamName = oMenuDT.Rows(i).Item("ParamName").ToString.Trim
            '                        oReport.FileName = IIf(oMenuDT.Rows(i).Item("Folder").ToString.Trim = String.Empty, oMenuDT.Rows(i).Item("FileName").ToString.Trim, oMenuDT.Rows(i).Item("Folder").ToString.Trim + "/" + oMenuDT.Rows(i).Item("FileName").ToString.Trim)
            '                        oCurrentForm.Close()
            '                        oCurrentForm = oReport
            '                        oCurrentForm.Text = oMenuDT.Rows(i).Item("MenuName").ToString
            '                        oCurrentForm.TopLevel = False
            '                        oCurrentForm.Parent = Me.SCParent.Panel2
            '                        oCurrentForm.Dock = DockStyle.Fill
            '                        oActive = String.Empty
            '                        oCurrentForm.Show()
            '                        Exit Sub
            '                    End If
            '                End If
            '            End If
            '        Next
            'End Select 'as of now disable ok.. ok..
            'If SCParent.Panel2.Controls.Count = 0 Then
            '    Dim oDashDt As DataTable = oMenuDT.Copy
            '    oDashDt.DefaultView.RowFilter = "ReportType = 'D'"
            '    If oDashDt.DefaultView.ToTable().Rows.Count > 0 Then
            '        oCurrentForm.Close()
            '        oCurrentForm = New Dashboard("N")
            '        oCurrentForm.TopLevel = False
            '        oCurrentForm.Parent = Me.SCParent.Panel2
            '        oCurrentForm.Dock = DockStyle.Fill
            '        oActive = String.Empty
            '        oCurrentForm.Show()
            '    Else
            '        SCParent.Panel2.BackgroundImage = Global.POS.My.Resources.POSLogo
            '    End If
            'End If
            'oFormLoading = False
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            'FillInboxView(True)
            BackgroundWorker1.CancelAsync()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub SalesOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesOrderToolStripMenuItem.Click
        Try
            'Dim Proceed As Boolean = True
            'If CheckUserInitialized() Then
            '    If oAuthDetailsHT.Item(SalesOrderToolStripMenuItem.Tag) = "F" Then
            oFormLoading = True
            oCurrentForm.Close()
            oCurrentForm = PSWizard
            oCurrentForm.TopLevel = False
            oCurrentForm.Parent = Me.SCParent.Panel2
            oCurrentForm.Dock = DockStyle.Fill
            'If SCParent.SplitterDistance = 226 Then
            '    oCurrentForm.Dock = DockStyle.Fill
            'ElseIf SCParent.SplitterDistance = 30 Then
            '    oCurrentForm.Dock = DockStyle.None
            '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
            '    oCurrentForm.Height = SCParent.Panel2.Height
            'End If
            'oActive = "ORDR"
            'Invoice.strFormName = "SaleOrder"
            oCurrentForm.Show()
            oFormLoading = False
            '    Else
            '        ErrorMsg.StatusBarMsg("You Are Not Authorized To Perform This Operation", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            '    End If
            'Else
            '    ErrorMsg.StatusBarMsg("Initialize The User In System Initialization Menu And Relogin The Application", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
            'End If
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'FillInboxView(False)
            'LoadAlertsDT()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub BGMsgWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGMsgWorker.DoWork
        Try
            'FillInboxView()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            'If e.KeyCode = Keys.F2 Then
            '    OpenSaleBill()
            'ElseIf e.KeyCode = Keys.F3 Then
            '    OpenPurchaseOrder()
            'ElseIf e.KeyCode = Keys.F4 Then
            '    OpenPOToGRPO()
            'ElseIf e.KeyCode = Keys.F5 Then
            '    OpenStockTransInward()
            'ElseIf e.KeyCode = Keys.F6 Then
            '    OpenStockTransOutward()
            'End If
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            'BackgroundWorker1.RunWorkerAsync()
            ' LoadAlertsDT()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub MainForm_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Singleton.DisConnectSAPCompany()
            System.GC.Collect()
            Application.Exit()
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Private Sub btnExpCol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpCol.Click
        Try
            If SCParent.SplitterDistance = 226 Then
                'If oActive = "OINV" Or oActive = "ORDR" Then
                '    oCurrentForm.Dock = DockStyle.None
                '    oCurrentForm.Location = New System.Drawing.Point(0, 0)
                '    oCurrentForm.Height = SCParent.Panel2.Height
                'End If
                SCParent.SplitterDistance = 30
                MenuTree.CollapseAll()
            ElseIf SCParent.SplitterDistance = 30 Then
                'If oActive = "OINV" Or oActive = "ORDR" Then
                '    oCurrentForm.Dock = DockStyle.Fill
                'End If
                SCParent.SplitterDistance = 226
            End If
        Catch ex As Exception
            ErrorMsg.StatusBarMsg(ex.Message, Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Error)
        End Try
    End Sub

    Public Function CheckSuperUser() As String
        Try
            Dim retVal As String = "N"
            If Login.UserType = "SuperUser" Or Login.UserType = "Administrator" Then
                retVal = "Y"
            Else
                retVal = "N"
            End If
            Return retVal
        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oDr) Then
                If Not oDr.IsClosed Then
                    oDr.Close()
                End If
            End If
        End Try
    End Function

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub LoadMenu()
        Try
            Dim oImageList As New ImageList()
            Try
                For Each path As String In System.IO.Directory.GetFiles("MenuIcons")
                    oImageList.Images.Add(Image.FromFile(path))
                Next
            Catch ex As Exception

            End Try

            MenuTree.ImageList = oImageList

            'MenuTree.Nodes.Add("menuAdmin", "Administration", 13)
            'MenuTree.Nodes.Item("menuAdmin").SelectedImageIndex = 13
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuUsers", "Users", 14)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuUsers").SelectedImageIndex = 14
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuSysInit", "System Initialization", 15)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuSysInit").SelectedImageIndex = 15
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuExpSetup", "Expense Setup", 11)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuExpSetup").SelectedImageIndex = 11
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuROLSetUp", "ROL Import", 21)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuROLSetUp").SelectedImageIndex = 21
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuROLDisplay", "ROL Display Matrix", 21)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuROLDisplay").SelectedImageIndex = 21
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuPreVen", "Preferred Vendor", 22)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuPreVen").SelectedImageIndex = 22
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuReportConfig", "Report Configuration", 15)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuReportConfig").SelectedImageIndex = 15
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuPointsTarget", "Point Based Target Setup", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuPointsTarget").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuSalesIncentive", "Sales Incentive Setup", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuSalesIncentive").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuSalesIncentiveShare", "Sales Incentive Share Setup", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuSalesIncentiveShare").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuEmpSalesTarget1", "Employee Sales Target(Main Products)", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuEmpSalesTarget1").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuEmpSalesTarget2", "Employee Sales Target(Other Products)", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuEmpSalesTarget2").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuBranchIncentive", "Branch Incentive Setup", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuBranchIncentive").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuFestiveCombiOffer", "Festive Gift Combi Setup", 28)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuFestiveCombiOffer").SelectedImageIndex = 28
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuQuerySetup", "Query Setup", 29)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuQuerySetup").SelectedImageIndex = 29
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Add("menuPriceImport", "Price Import", 32)
            'MenuTree.Nodes.Find("menuAdmin", True)(0).Nodes.Item("menuPriceImport").SelectedImageIndex = 32

            MenuTree.Nodes.Add("menuSales", "PreSales", 0)
            MenuTree.Nodes.Item("menuSales").SelectedImageIndex = 0
            MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuSaleOrder", "Pre - Sale Order", 1)

            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuSaleOrder").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuOrdertoInvoice", "Sale Order To Sale Bill", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuOrdertoInvoice").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuInvoice", "Sale Bill", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuInvoice").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuCombi", "Combi Invoice", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuCombi").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuInvoiceToCreditBill", "Sale Bill To Sale Return", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuInvoiceToCreditBill").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuExchangeSale", "Exchange Sale", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuExchangeSale").SelectedImageIndex = 1
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Add("menuGiftOfferInvoice", "Gift Offer Invoice", 1)
            'MenuTree.Nodes.Find("menuSales", True)(0).Nodes.Item("menuGiftOfferInvoice").SelectedImageIndex = 1

            'MenuTree.Nodes.Add("menuPurchase", "Purchase", 2)
            'MenuTree.Nodes.Item("menuPurchase").SelectedImageIndex = 2
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Add("menuPO", "Purchase Order", 3)
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Item("menuPO").SelectedImageIndex = 3
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Add("menuPOToGRPO", "Purchase Order To GRPO", 3)
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Item("menuPOToGRPO").SelectedImageIndex = 3
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Add("menuGRPO", "GRPO", 4)
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Item("menuGRPO").SelectedImageIndex = 4
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Add("menuGRPOToGoodsReturn", "GRPO To Goods Return", 4)
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Item("menuGRPOToGoodsReturn").SelectedImageIndex = 4
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Add("menuGRPOToAPInvoice", "GRPO To A/P Invoice", 4)
            'MenuTree.Nodes.Find("menuPurchase", True)(0).Nodes.Item("menuGRPOToAPInvoice").SelectedImageIndex = 4

            'MenuTree.Nodes.Add("menuInventory", "Inventory", 5)
            'MenuTree.Nodes.Item("menuInventory").SelectedImageIndex = 5

            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuInvReq", "Stock Transfer Request", 6)
            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuInvReq").SelectedImageIndex = 6

            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuTransToTransit", "Transfer To Transit", 7)
            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuTransToTransit").SelectedImageIndex = 7

            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuRecFrmTransit", "Receipt From Transit", 7)
            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuRecFrmTransit").SelectedImageIndex = 7

            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuRetFrmTransit", "Return From Transit", 7)
            ''MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuRetFrmTransit").SelectedImageIndex = 7

            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuTransInward", "Stock Transfer Inward", 7)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuTransInward").SelectedImageIndex = 7

            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuTransOutward", "Stock Transfer Outward", 7)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuTransOutward").SelectedImageIndex = 7

            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuSerNumTrans", "Serial Number Transaction", 27)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuSerNumTrans").SelectedImageIndex = 27


            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuStockTaking", "Stock Taking", 27)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuStockTaking").SelectedImageIndex = 27

            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuBulkOutWard", "Bulk OutWard", 7)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuBulkOutWard").SelectedImageIndex = 7

            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Add("menuBulkInWard", "Bulk InWard", 7)
            'MenuTree.Nodes.Find("menuInventory", True)(0).Nodes.Item("menuBulkInWard").SelectedImageIndex = 7

            'MenuTree.Nodes.Add("menuPayment", "Payment", 8)
            'MenuTree.Nodes.Item("menuPayment").SelectedImageIndex = 8
            'MenuTree.Nodes.Find("menuPayment", True)(0).Nodes.Add("menuInPaymt", "Incoming Payment", 9)
            'MenuTree.Nodes.Find("menuPayment", True)(0).Nodes.Item("menuInPaymt").SelectedImageIndex = 9

            'MenuTree.Nodes.Add("menuExpenses", "Expenses", 10)
            'MenuTree.Nodes.Item("menuExpenses").SelectedImageIndex = 10
            'MenuTree.Nodes.Find("menuExpenses", True)(0).Nodes.Add("menuExpBook", "Expense Booking", 12)
            'MenuTree.Nodes.Find("menuExpenses", True)(0).Nodes.Item("menuExpBook").SelectedImageIndex = 12

            'MenuTree.Nodes.Add("menuDeposits", "Deposits", 16)
            'MenuTree.Nodes.Item("menuDeposits").SelectedImageIndex = 16
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Add("menuCheque", "Cheque", 17)
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Item("menuCheque").SelectedImageIndex = 17
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Add("menuCreditCard", "Credit Card", 18)
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Item("menuCreditCard").SelectedImageIndex = 18
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Add("menuCash", "Cash", 19)
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Item("menuCash").SelectedImageIndex = 19
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Add("menuCashDenom", "Cash Denomination", 19)
            'MenuTree.Nodes.Find("menuDeposits", True)(0).Nodes.Item("menuCashDenom").SelectedImageIndex = 19

            'MenuTree.Nodes.Add("menuROL", "ROL", 20)
            'MenuTree.Nodes.Item("menuROL").SelectedImageIndex = 20
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Add("menuROLWizard", "ROL Wizard", 24)
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Item("menuROLWizard").SelectedImageIndex = 24
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Add("menuSendMail", "Send Mail", 23)
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Item("menuSendMail").SelectedImageIndex = 23
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Add("menuPOCancel", "PO Cancellation", 6)
            'MenuTree.Nodes.Find("menuROL", True)(0).Nodes.Item("menuPOCancel").SelectedImageIndex = 6

            'MenuTree.Nodes.Add("menuQueries", "Queries", 31)
            'MenuTree.Nodes.Item("menuQueries").SelectedImageIndex = 31
            'MenuTree.Nodes.Find("menuQueries", True)(0).Nodes.Add("menuQueryGen", "Query Generator", 30)
            'MenuTree.Nodes.Find("menuQueries", True)(0).Nodes.Item("menuQueryGen").SelectedImageIndex = 30

            'oMenuDT.Rows.Clear()
            'oDr = oConn.RunQuery("Buson_POS_GetMenus '" + Login.UserId.Trim + "'")
            'oMenuDT.Load(oDr)
            'oDr.Close()

            'Dim oTempDT As New DataTable
            'oDr = oConn.RunQuery("Buson_POS_GetUserAuthDetails '" + Login.UserId.Trim + "'")
            'oTempDT.Load(oDr)
            'oDr.Close()

            'For i = 0 To oTempDT.Rows.Count - 1
            '    oAuthDetailsHT.Add(oTempDT.Rows(i).Item(0).ToString, oTempDT.Rows(i).Item(1).ToString)
            'Next

            'If oMenuDT.Rows.Count > 1 Then
            '    For i = 0 To oMenuDT.Rows.Count - 1
            '        'If oMenuDT.Rows(i).Item("ReportType").ToString <> "D" Then
            '        If 1 = 1 Then
            '            If oMenuDT.Rows(i).Item("Parent").ToString = "" Then
            '                MenuTree.Nodes.Add(oMenuDT.Rows(i).Item("MenuID").ToString, oMenuDT.Rows(i).Item("MenuName").ToString, CInt(oMenuDT.Rows(i).Item("ImageIndex")))
            '                MenuTree.Nodes.Item(oMenuDT.Rows(i).Item("MenuID").ToString).SelectedImageIndex = CInt(oMenuDT.Rows(i).Item("ImageIndex"))
            '            Else
            '                MenuTree.Nodes.Find(oMenuDT.Rows(i).Item("Parent").ToString, True)(0).Nodes.Add(oMenuDT.Rows(i).Item("MenuID").ToString, oMenuDT.Rows(i).Item("MenuName").ToString, CInt(oMenuDT.Rows(i).Item("ImageIndex")))
            '                MenuTree.Nodes.Find(oMenuDT.Rows(i).Item("Parent").ToString, True)(0).Nodes.Item(oMenuDT.Rows(i).Item("MenuID").ToString).SelectedImageIndex = CInt(oMenuDT.Rows(i).Item("ImageIndex"))
            '            End If
            '        End If
            '    Next
            'End If

            MenuTree.ShowRootLines = False
            MenuTree.ShowLines = False
            MenuTree.CollapseAll()
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

    Private Sub SAPLogin()
        Try
            'If Not IsDBNull(Login.oUSER.Rows.Item(0).Item("SAPUser")) And Not IsDBNull(Login.oUSER.Rows.Item(0).Item("SAPPwd")) Then
            '    oCompany.Server = Login.strDBServer
            '    oCompany.LicenseServer = System.Configuration.ConfigurationManager.AppSettings("LicenseServer").ToString
            '    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
            '    oCompany.CompanyDB = Login.strDBName
            '    oCompany.DbUserName = Login.strSqlUser
            '    oCompany.DbPassword = Login.strSqlPwd
            '    oCompany.UserName = Login.oUSER.Rows.Item(0).Item("SAPUser").ToString
            '    oCompany.Password = Login.oUSER.Rows.Item(0).Item("SAPPwd").ToString

            '    Dim I As Integer
            '    I = oCompany.Connect()

            '    If I = 0 Then
            '        ErrorMsg.StatusBarMsg("Successfully Logged Into SAP Business One!!", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Success)
            '        companyStatusLabel.Text = oCompany.CompanyName + "(" + oCompany.CompanyDB.ToString + ")" + " - SAP User(" + oCompany.UserName + ") - POS User(" + Login.UserId + ")"
            '        UserSign = oCompany.UserSignature
            '        'LoadInvoice()
            '        SuperUser = CheckSuperUser()
            '    Else
            '        Dim eCode As Integer
            '        Dim eDesc As String
            '        Dim oMsg As MsgBoxResult
            '        oCompany.GetLastError(eCode, eDesc)
            '        oMsg = MsgBox("Connection with SAP Business One Failed!! (" + eCode.ToString + " - " + eDesc + " :  Do you want to continue without connecting to SAP Business One. To Continue Press Yes, to Exit Press No)", MsgBoxStyle.YesNo)
            '        If oMsg = MsgBoxResult.Yes Then
            '            ErrorMsg.StatusBarMsg("Not Connected with SAP Business One!! You cannot perform any backend task with SAP Business One!!", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Warning)
            '        ElseIf oMsg = MsgBoxResult.No Then
            '            End
            '        End If
            '    End If
            'Else
            '    Dim oMsg As MsgBoxResult
            '    oMsg = MsgBox("Connection with SAP Business One Failed!! :  Do you want to continue without connecting to SAP Business One. To Continue Press Yes, to Exit Press No)", MsgBoxStyle.YesNo)
            '    If oMsg = MsgBoxResult.Yes Then
            '        ErrorMsg.StatusBarMsg("Not Connected with SAP Business One!! You cannot perform any backend task with SAP Business One!!", Bx_UI_COM_ErrorMsg.ErrorComponent.MessageType.bx_Warning)
            '    ElseIf oMsg = MsgBoxResult.No Then
            '        End
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Public Sub StartPB(ByVal Min As Integer, ByVal Max As Integer)
    '    Progress.Minimum = Min
    '    Progress.Maximum = Max
    '    Progress.Value = Min + 1
    '    Progress.PerformStep()
    'End Sub

    'Public Sub StopPB()
    '    Progress.Value = Progress.Minimum
    '    Progress.PerformStep()
    '    Progress.Minimum = 0
    '    Progress.Maximum = 0
    '    ProgressMsg.Text = ""
    'End Sub

    'Public Sub MovePB(ByVal val As Integer, ByVal msg As String)
    '    pVal = val
    '    pStr = msg
    '    trd = New Thread(AddressOf Mythread)
    '    trd.IsBackground = True
    '    trd.Start()
    'End Sub

    'Sub Mythread()
    '    ThreadPB()
    'End Sub

    'Private Sub ThreadPB()
    '    If Me.InvokeRequired Then
    '        Me.Invoke(New MethodInvoker(AddressOf ThreadPB))
    '    Else
    '        Progress.Value = pVal
    '        ProgressMsg.Text = pStr
    '        Progress.PerformStep()
    '        Thread.Sleep(100)
    '    End If
    'End Sub

    'Private Sub wait(ByVal interval As Integer)
    '    Try
    '        Dim sw As New Stopwatch
    '        sw.Start()
    '        Do While sw.ElapsedMilliseconds < interval
    '            ' Allows UI to remain responsive
    '            Application.DoEvents()
    '        Loop
    '        sw.Stop()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

End Class
