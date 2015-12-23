Imports System.Drawing
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices


Public Class MsgBoxNew
    Inherits Form
    Private Const CS_DROPSHADOW As Integer = &H20000
    Private Shared _msgBox As MsgBoxNew
    Private _plHeader As New Panel()
    Private _plFooter As New Panel()
    Private _plIcon As New Panel()
    Private _picIcon As New PictureBox()
    Private _flpButtons As New FlowLayoutPanel()
    Private _lblTitle As Label
    Private _lblMessage As Label
    Private _buttonCollection As New List(Of Button)()
    Private Shared _buttonResult As New DialogResult()
    Private Shared _timer As Timer


    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function MessageBeep(ByVal type As UInteger) As Boolean

    End Function
    Dim oConfigDt As New DataTable

    Private Sub SetColors()
        Dim oDr As SqlClient.SqlDataReader = Nothing
        'Dim oConn As New Syn_DO.Bridge
        Try
            oConfigDt.Rows.Clear()
            'If Main'Form.oCompany.Connected Then
            '    oConn.SecureConnect(Login.strDBServer, Login.strDBName, Login.strSqlUser, Login.strSqlPwd)
            '    Dim sQuery As String = "Select ThemeColor,LBForeColor,TBBackColor,TBForeColor,PaySecLColor,BBackColor,PaySecThmColor,GColHeaderBackColor,GColHeaderForeColor,GSelectionBackColor,GSelectionForeColor,GColor From BUSON_GLACCOUNTS Where UserID = '" & IIf(IsNothing(Login.UserId), "manager", Login.UserId) & "'"
            '    oDr = oConn.RunQuery(sQuery)
            '    If oDr.HasRows Then
            '        oConfigDt.Load(oDr)
            '    End If
            '    oDr.Close()
            'End If
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

    Public Sub New()
        InitializeComponent()
        SetColors()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        If oConfigDt.Rows.Count > 0 Then
            Me.BackColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("ThemeColor").ToString)

        Else
            Me.BackColor = Color.FromArgb(45, 45, 48)
        End If
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Width = 400
        Me.ShowInTaskbar = False
        Me.TopMost = False

        _lblTitle = New Label()
        If oConfigDt.Rows.Count > 0 Then
            _lblTitle.ForeColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("LBForeColor").ToString)
        Else
            _lblTitle.ForeColor = Color.White
        End If
        _lblTitle.Font = New System.Drawing.Font("Calibri", 16, FontStyle.Bold)
        _lblTitle.Dock = DockStyle.Top
        _lblTitle.Height = 50

        _lblMessage = New Label()
        If oConfigDt.Rows.Count > 0 Then
            _lblMessage.ForeColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("LBForeColor").ToString)
        Else
            _lblMessage.ForeColor = Color.White
        End If
        _lblMessage.Font = New System.Drawing.Font("Calibri", 11, FontStyle.Bold)
        _lblMessage.Dock = DockStyle.Fill

        _flpButtons.FlowDirection = FlowDirection.RightToLeft
        _flpButtons.Dock = DockStyle.Fill

        _plHeader.Dock = DockStyle.Fill
        _plHeader.Padding = New Padding(20)
        _plHeader.Controls.Add(_lblMessage)
        _plHeader.Controls.Add(_lblTitle)

        _plFooter.Dock = DockStyle.Bottom
        _plFooter.Padding = New Padding(20)
        If oConfigDt.Rows.Count > 0 Then
            Me.BackColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("ThemeColor").ToString)
        Else
            _plFooter.BackColor = Color.FromArgb(37, 37, 38)
        End If
        _plFooter.Height = 80
        _plFooter.Controls.Add(_flpButtons)

        _picIcon.Width = 32
        _picIcon.Height = 32
        _picIcon.Location = New Point(30, 50)

        _plIcon.Dock = DockStyle.Left
        _plIcon.Padding = New Padding(20)
        _plIcon.Width = 70
        _plIcon.Controls.Add(_picIcon)

        Me.Controls.Add(_plHeader)
        Me.Controls.Add(_plIcon)
        Me.Controls.Add(_plFooter)
    End Sub

    Public Shared Sub Shows(ByVal message As String)
        _msgBox = New MsgBoxNew()
        _msgBox._lblMessage.Text = message
        _msgBox.ShowDialog()
        MessageBeep(0)
    End Sub

    Public Shared Sub Shows(ByVal message As String, ByVal title As String)
        _msgBox = New MsgBoxNew()
        _msgBox._lblMessage.Text = message
        _msgBox._lblTitle.Text = title
        _msgBox.Size = MsgBoxNew.MessageSize(message)
        _msgBox.ShowDialog()
        MessageBeep(0)
    End Sub

    Public Shared Function Shows(ByVal message As String, ByVal title As String, ByVal buttons As Buttons) As DialogResult
        _msgBox = New MsgBoxNew()
        _msgBox._lblMessage.Text = message
        _msgBox._lblTitle.Text = title
        _msgBox._plIcon.Hide()

        MsgBoxNew.InitButtons(buttons)

        _msgBox.Size = MsgBoxNew.MessageSize(message)
        _msgBox.ShowDialog()
        MessageBeep(0)
        Return _buttonResult
    End Function

    Public Shared Function Shows(ByVal message As String, ByVal title As String, ByVal buttons As Buttons, ByVal icon As Icons) As DialogResult
        _msgBox = New MsgBoxNew()
        _msgBox._lblMessage.Text = message
        _msgBox._lblTitle.Text = title

        MsgBoxNew.InitButtons(buttons)
        MsgBoxNew.InitIcon(icon)

        _msgBox.Size = MsgBoxNew.MessageSize(message)
        _msgBox.ShowDialog()
        MessageBeep(0)
        Return _buttonResult
    End Function

    Public Shared Function Shows(ByVal message As String, ByVal title As String, ByVal buttons As Buttons, ByVal icon As Icons, ByVal style As AnimateStyle) As DialogResult
        _msgBox = New MsgBoxNew()
        _msgBox._lblMessage.Text = message
        _msgBox._lblTitle.Text = title
        _msgBox.Height = 0

        MsgBoxNew.InitButtons(buttons)
        MsgBoxNew.InitIcon(icon)

        _timer = New Timer()
        Dim formSize As Size = MsgBoxNew.MessageSize(message)

        Select Case style
            Case MsgBoxNew.AnimateStyle.SlideDown
                _msgBox.Size = New Size(formSize.Width, 0)
                _timer.Interval = 1
                _timer.Tag = New AnimateMsgBox(formSize, style)
                Exit Select

            Case MsgBoxNew.AnimateStyle.FadeIn
                _msgBox.Size = formSize
                _msgBox.Opacity = 0
                _timer.Interval = 20
                _timer.Tag = New AnimateMsgBox(formSize, style)
                Exit Select

            Case MsgBoxNew.AnimateStyle.ZoomIn
                _msgBox.Size = New Size(formSize.Width + 100, formSize.Height + 100)
                _timer.Tag = New AnimateMsgBox(formSize, style)
                _timer.Interval = 1
                Exit Select
        End Select

        AddHandler _timer.Tick, AddressOf timer_Tick
        _timer.Start()
       
        _msgBox._buttonCollection.Item(0).Select()
        _msgBox._buttonCollection.Item(0).Focus()
        _msgBox._buttonCollection.Item(0).TabIndex = 0
        _msgBox._buttonCollection.Item(1).TabIndex = 1
        _msgBox._buttonCollection.Item(0).TabStop = True
        _msgBox._buttonCollection.Item(1).TabStop = True
        _msgBox.Controls("btnCanc").TabStop = False
        _msgBox.AcceptButton = _msgBox._buttonCollection.Item(0)
        _msgBox.ShowDialog()
        Return _buttonResult
    End Function

    Private Shared Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim timer As Timer = DirectCast(sender, Timer)
        Dim animate As AnimateMsgBox = DirectCast(timer.Tag, AnimateMsgBox)

        Select Case animate.Style
            Case MsgBoxNew.AnimateStyle.SlideDown
                If _msgBox.Height < animate.FormSize.Height Then
                    _msgBox.Height += 17
                    _msgBox.Invalidate()
                Else
                    _timer.[Stop]()
                    _timer.Dispose()
                End If
                Exit Select

            Case MsgBoxNew.AnimateStyle.FadeIn
                If Not _msgBox.IsDisposed Then
                    If _msgBox.Opacity < 1 Then
                        _msgBox.Opacity += 0.1
                        _msgBox.Invalidate()
                    Else
                        _timer.[Stop]()
                        _timer.Dispose()
                    End If
                    Exit Select
                End If

            Case MsgBoxNew.AnimateStyle.ZoomIn
                If _msgBox.Width > animate.FormSize.Width Then
                    _msgBox.Width -= 17
                    _msgBox.Invalidate()
                End If
                If _msgBox.Height > animate.FormSize.Height Then
                    _msgBox.Height -= 17
                    _msgBox.Invalidate()
                End If
                Exit Select
        End Select
    End Sub

    Private Sub InitButtons(ByVal buttons As Buttons)
        Select Case buttons
            Case MsgBoxNew.Buttons.AbortRetryIgnore
                _msgBox.InitAbortRetryIgnoreButtons()
                Exit Select

            Case MsgBoxNew.Buttons.OK
                _msgBox.InitOKButton()
                Exit Select

            Case MsgBoxNew.Buttons.OKCancel
                _msgBox.InitOKCancelButtons()
                Exit Select

            Case MsgBoxNew.Buttons.RetryCancel
                _msgBox.InitRetryCancelButtons()
                Exit Select

            Case MsgBoxNew.Buttons.YesNo
                _msgBox.InitYesNoButtons()
                Exit Select

            Case MsgBoxNew.Buttons.YesNoCancel
                _msgBox.InitYesNoCancelButtons()
                Exit Select
        End Select

        For Each btn As Button In _msgBox._buttonCollection
            If oConfigDt.Rows.Count > 0 Then
                btn.ForeColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("LBForeColor").ToString)
                btn.BackColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("BBackColor").ToString)
            Else
                btn.ForeColor = Color.FromArgb(170, 170, 170)
            End If
            btn.Font = New System.Drawing.Font("Calibri", 9, FontStyle.Bold)
            btn.Padding = New Padding(3)
            btn.FlatStyle = FlatStyle.Flat
            btn.Height = 30
            btn.FlatAppearance.BorderColor = Color.FromArgb(99, 99, 98)

            _msgBox._flpButtons.Controls.Add(btn)
        Next
    End Sub

    Private Shared Sub InitIcon(ByVal icon As Icons)
        Select Case icon
            Case MsgBoxNew.Icons.Application
                _msgBox._picIcon.Image = SystemIcons.Application.ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.Exclamation
                _msgBox._picIcon.Image = SystemIcons.Exclamation.ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.[Error]
                _msgBox._picIcon.Image = SystemIcons.[Error].ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.Info
                _msgBox._picIcon.Image = SystemIcons.Information.ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.Question
                _msgBox._picIcon.Image = SystemIcons.Question.ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.Shield
                _msgBox._picIcon.Image = SystemIcons.Shield.ToBitmap()
                Exit Select

            Case MsgBoxNew.Icons.Warning
                _msgBox._picIcon.Image = SystemIcons.Warning.ToBitmap()
                Exit Select
        End Select
    End Sub

    Private Sub InitAbortRetryIgnoreButtons()
        Dim btnAbort As New Button()
        btnAbort.Text = "Abort"
        AddHandler btnAbort.Click, AddressOf ButtonClick

        Dim btnRetry As New Button()
        btnRetry.Text = "Retry"
        AddHandler btnRetry.Click, AddressOf ButtonClick

        Dim btnIgnore As New Button()
        btnIgnore.Text = "Ignore"
        AddHandler btnIgnore.Click, AddressOf ButtonClick

        Me._buttonCollection.Add(btnAbort)
        Me._buttonCollection.Add(btnRetry)
        Me._buttonCollection.Add(btnIgnore)
    End Sub

    Private Sub InitOKButton()
        Dim btnOK As New Button()
        btnOK.Text = "OK"
        AddHandler btnOK.Click, AddressOf ButtonClick

        Me._buttonCollection.Add(btnOK)
    End Sub

    Private Sub InitOKCancelButtons()
        Dim btnOK As New Button()
        btnOK.Text = "OK"
        AddHandler btnOK.Click, AddressOf ButtonClick

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        AddHandler btnCancel.Click, AddressOf ButtonClick

        If oConfigDt.Rows.Count > 0 Then
            btnOK.ForeColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("LBForeColor").ToString)
            btnOK.BackColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("BBackColor").ToString)

            btnCancel.ForeColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("LBForeColor").ToString)
            btnCancel.BackColor = System.Drawing.Color.FromName(oConfigDt.Rows(0).Item("BBackColor").ToString)
        End If

        Me._buttonCollection.Add(btnOK)
        Me._buttonCollection.Add(btnCancel)
    End Sub

    Private Sub InitRetryCancelButtons()
        Dim btnRetry As New Button()
        btnRetry.Text = "OK"
        AddHandler btnRetry.Click, AddressOf ButtonClick

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        AddHandler btnCancel.Click, AddressOf ButtonClick


        Me._buttonCollection.Add(btnRetry)
        Me._buttonCollection.Add(btnCancel)
    End Sub

    Private Sub InitYesNoButtons()
        Dim btnYes As New Button()

        btnYes.Text = "Yes"
        AddHandler btnYes.Click, AddressOf ButtonClick

        Dim btnNo As New Button()
        btnNo.Text = "No"
        AddHandler btnNo.Click, AddressOf ButtonClick

        Me._buttonCollection.Add(btnNo)
        Me._buttonCollection.Add(btnYes)
    End Sub

    Private Sub InitYesNoCancelButtons()
        Dim btnYes As New Button()
        btnYes.Text = "Abort"
        AddHandler btnYes.Click, AddressOf ButtonClick

        Dim btnNo As New Button()
        btnNo.Text = "Retry"
        AddHandler btnNo.Click, AddressOf ButtonClick

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        AddHandler btnCancel.Click, AddressOf ButtonClick

        Me._buttonCollection.Add(btnYes)
        Me._buttonCollection.Add(btnNo)
        Me._buttonCollection.Add(btnCancel)
    End Sub

    Private Shared Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)

        Select Case btn.Text
            Case "Abort"
                _buttonResult = DialogResult.Abort
                Exit Select

            Case "Retry"
                _buttonResult = DialogResult.Retry
                Exit Select

            Case "Ignore"
                _buttonResult = DialogResult.Ignore
                Exit Select

            Case "OK"
                _buttonResult = DialogResult.OK
                Exit Select

            Case "Cancel"
                _buttonResult = DialogResult.Cancel
                Exit Select

            Case "Yes"
                _buttonResult = DialogResult.Yes
                Exit Select

            Case "No"
                _buttonResult = DialogResult.No
                Exit Select
        End Select

        _msgBox.Dispose()
    End Sub
    Private Shared Function MessageSize(ByVal message As String) As Size
        Dim g As Graphics = _msgBox.CreateGraphics()
        Dim width As Integer = 400
        Dim height As Integer = 230

        Dim size As SizeF = g.MeasureString(message, New System.Drawing.Font("Calibri", 9))

        If message.Length < 150 Then
            If CInt(size.Width) > 350 Then
                width = CInt(size.Width)
            End If
        Else
            Dim groups As String() = (From m In Regex.Matches(message, ".{1,180}") Select m.Value).ToArray()
            Dim lines As Integer = groups.Length + 1
            width = 700
            height += CInt(size.Height + 10) * lines
        End If
        Return New Size(width, height)
    End Function

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics
        Dim rect As New Rectangle(New Point(0, 0), New Size(Me.Width - 1, Me.Height - 1))
        Dim pen As New Pen(Color.FromArgb(0, 151, 251))

        g.DrawRectangle(pen, rect)
    End Sub

    Public Enum Buttons
        AbortRetryIgnore = 1
        OK = 2
        OKCancel = 3
        RetryCancel = 4
        YesNo = 5
        YesNoCancel = 6
    End Enum

    Public Enum Icons
        Application = 1
        Exclamation = 2
        [Error] = 3
        Warning = 4
        Info = 5
        Question = 6
        Shield = 7
        Search = 8
    End Enum

    Public Enum AnimateStyle
        SlideDown = 1
        FadeIn = 2
        ZoomIn = 3
    End Enum

    Private Sub InitializeComponent()
        Me.btnCanc = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCanc
        '
        Me.btnCanc.DialogResult = System.Windows.Forms.DialogResult.No
        Me.btnCanc.Location = New System.Drawing.Point(260, 210)
        Me.btnCanc.Name = "btnCanc"
        Me.btnCanc.Size = New System.Drawing.Size(0, 0)
        Me.btnCanc.TabIndex = 28
        Me.btnCanc.Text = "No"
        Me.btnCanc.UseVisualStyleBackColor = True
        '5
        'MsgBoxNew
        '
        Me.CancelButton = Me.btnCanc
        Me.ClientSize = New System.Drawing.Size(265, 217)
        Me.Controls.Add(Me.btnCanc)
        Me.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "MsgBoxNew"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub

    Private Sub btnCanc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCanc.Click, btnCanc.Click
        _buttonResult = DialogResult.No
    End Sub

    Private Sub MsgBoxNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class

Class AnimateMsgBox
    Public FormSize As Size
    Public Style As MsgBoxNew.AnimateStyle

    Public Sub New(ByVal formSize As Size, ByVal style As MsgBoxNew.AnimateStyle)
        Me.FormSize = formSize
        Me.Style = style
    End Sub
End Class