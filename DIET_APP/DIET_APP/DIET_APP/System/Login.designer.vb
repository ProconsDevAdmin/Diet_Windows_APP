<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.scParent = New System.Windows.Forms.SplitContainer()
        Me.scHeader = New System.Windows.Forms.SplitContainer()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Rowmatrix = New System.Windows.Forms.DataGridView()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Database = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDBServer = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUserCode = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.TextBox()
        Me.UserCode = New System.Windows.Forms.TextBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.Add = New System.Windows.Forms.Button()
        Me.lblMask = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.scFooter = New System.Windows.Forms.SplitContainer()
        Me.ErrMsg = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.scParent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scParent.Panel1.SuspendLayout()
        Me.scParent.Panel2.SuspendLayout()
        Me.scParent.SuspendLayout()
        CType(Me.scHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scHeader.Panel2.SuspendLayout()
        Me.scHeader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Rowmatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scFooter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scFooter.Panel1.SuspendLayout()
        Me.scFooter.Panel2.SuspendLayout()
        Me.scFooter.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'scParent
        '
        Me.scParent.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scParent.Location = New System.Drawing.Point(0, 0)
        Me.scParent.Margin = New System.Windows.Forms.Padding(0)
        Me.scParent.Name = "scParent"
        Me.scParent.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scParent.Panel1
        '
        Me.scParent.Panel1.Controls.Add(Me.scHeader)
        '
        'scParent.Panel2
        '
        Me.scParent.Panel2.Controls.Add(Me.scFooter)
        Me.scParent.Size = New System.Drawing.Size(783, 342)
        Me.scParent.SplitterDistance = 278
        Me.scParent.TabIndex = 9
        Me.scParent.TabStop = False
        '
        'scHeader
        '
        Me.scHeader.BackColor = System.Drawing.Color.White
        Me.scHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scHeader.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scHeader.Location = New System.Drawing.Point(0, 0)
        Me.scHeader.Margin = New System.Windows.Forms.Padding(0)
        Me.scHeader.Name = "scHeader"
        '
        'scHeader.Panel1
        '
        Me.scHeader.Panel1.CausesValidation = False
        Me.scHeader.Panel1Collapsed = True
        Me.scHeader.Panel1MinSize = 0
        '
        'scHeader.Panel2
        '
        Me.scHeader.Panel2.BackColor = System.Drawing.Color.White
        Me.scHeader.Panel2.Controls.Add(Me.PictureBox1)
        Me.scHeader.Panel2.Controls.Add(Me.Rowmatrix)
        Me.scHeader.Panel2.Controls.Add(Me.Label5)
        Me.scHeader.Panel2.Controls.Add(Me.cmbDBServer)
        Me.scHeader.Panel2.Controls.Add(Me.Label4)
        Me.scHeader.Panel2.Controls.Add(Me.lblPassword)
        Me.scHeader.Panel2.Controls.Add(Me.lblUserCode)
        Me.scHeader.Panel2.Controls.Add(Me.Password)
        Me.scHeader.Panel2.Controls.Add(Me.UserCode)
        Me.scHeader.Panel2.Controls.Add(Me.Cancel)
        Me.scHeader.Panel2.Controls.Add(Me.Add)
        Me.scHeader.Panel2.Controls.Add(Me.lblMask)
        Me.scHeader.Panel2.Controls.Add(Me.ShapeContainer1)
        Me.scHeader.Size = New System.Drawing.Size(783, 278)
        Me.scHeader.SplitterDistance = 25
        Me.scHeader.TabIndex = 0
        Me.scHeader.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(29, 35)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(405, 203)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'Rowmatrix
        '
        Me.Rowmatrix.AllowUserToAddRows = False
        Me.Rowmatrix.AllowUserToDeleteRows = False
        Me.Rowmatrix.AllowUserToResizeRows = False
        Me.Rowmatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Rowmatrix.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Rowmatrix.BackgroundColor = System.Drawing.Color.White
        Me.Rowmatrix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Rowmatrix.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGreen
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Rowmatrix.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Rowmatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Rowmatrix.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Company, Me.Database})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.MidnightBlue
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Rowmatrix.DefaultCellStyle = DataGridViewCellStyle2
        Me.Rowmatrix.EnableHeadersVisualStyles = False
        Me.Rowmatrix.GridColor = System.Drawing.Color.DarkGray
        Me.Rowmatrix.Location = New System.Drawing.Point(452, 142)
        Me.Rowmatrix.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.Rowmatrix.MultiSelect = False
        Me.Rowmatrix.Name = "Rowmatrix"
        Me.Rowmatrix.ReadOnly = True
        Me.Rowmatrix.RowHeadersVisible = False
        Me.Rowmatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Rowmatrix.Size = New System.Drawing.Size(319, 98)
        Me.Rowmatrix.TabIndex = 4
        Me.Rowmatrix.Tag = "2"
        '
        'Company
        '
        Me.Company.DataPropertyName = "Company"
        Me.Company.HeaderText = "Company"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'Database
        '
        Me.Database.DataPropertyName = "Database"
        Me.Database.FillWeight = 70.0!
        Me.Database.HeaderText = "Database"
        Me.Database.Name = "Database"
        Me.Database.ReadOnly = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(449, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "List Of Companies"
        '
        'cmbDBServer
        '
        Me.cmbDBServer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbDBServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDBServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDBServer.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDBServer.ForeColor = System.Drawing.Color.Black
        Me.cmbDBServer.FormattingEnabled = True
        Me.cmbDBServer.Location = New System.Drawing.Point(542, 91)
        Me.cmbDBServer.Name = "cmbDBServer"
        Me.cmbDBServer.Size = New System.Drawing.Size(227, 23)
        Me.cmbDBServer.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Location = New System.Drawing.Point(449, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "DB Server"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPassword.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblPassword.Location = New System.Drawing.Point(449, 61)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(61, 15)
        Me.lblPassword.TabIndex = 0
        Me.lblPassword.Text = "Password"
        '
        'lblUserCode
        '
        Me.lblUserCode.AutoSize = True
        Me.lblUserCode.BackColor = System.Drawing.Color.Transparent
        Me.lblUserCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblUserCode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserCode.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblUserCode.Location = New System.Drawing.Point(449, 31)
        Me.lblUserCode.Name = "lblUserCode"
        Me.lblUserCode.Size = New System.Drawing.Size(62, 15)
        Me.lblUserCode.TabIndex = 0
        Me.lblUserCode.Text = "User Code"
        '
        'Password
        '
        Me.Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Password.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Password.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Password.Location = New System.Drawing.Point(539, 59)
        Me.Password.MaxLength = 100
        Me.Password.Name = "Password"
        Me.Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Password.Size = New System.Drawing.Size(232, 23)
        Me.Password.TabIndex = 2
        Me.Password.UseSystemPasswordChar = True
        '
        'UserCode
        '
        Me.UserCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserCode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UserCode.ForeColor = System.Drawing.Color.MidnightBlue
        Me.UserCode.Location = New System.Drawing.Point(539, 29)
        Me.UserCode.MaxLength = 50
        Me.UserCode.Name = "UserCode"
        Me.UserCode.Size = New System.Drawing.Size(232, 23)
        Me.UserCode.TabIndex = 1
        '
        'Cancel
        '
        Me.Cancel.BackColor = System.Drawing.Color.Khaki
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Cancel.Image = Global.DIET_APP.My.Resources.Resources.Close
        Me.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel.Location = New System.Drawing.Point(661, 245)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(110, 32)
        Me.Cancel.TabIndex = 6
        Me.Cancel.Text = "Cancel"
        Me.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel.UseVisualStyleBackColor = False
        '
        'Add
        '
        Me.Add.BackColor = System.Drawing.Color.Khaki
        Me.Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Add.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Add.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Add.Image = Global.DIET_APP.My.Resources.Resources.Lock
        Me.Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Add.Location = New System.Drawing.Point(539, 245)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(98, 32)
        Me.Add.TabIndex = 5
        Me.Add.Text = "Login"
        Me.Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Add.UseVisualStyleBackColor = False
        '
        'lblMask
        '
        Me.lblMask.AutoSize = True
        Me.lblMask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMask.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblMask.Font = New System.Drawing.Font("Viner Hand ITC", 140.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMask.ForeColor = System.Drawing.Color.Aquamarine
        Me.lblMask.Location = New System.Drawing.Point(0, 0)
        Me.lblMask.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMask.Name = "lblMask"
        Me.lblMask.Size = New System.Drawing.Size(484, 302)
        Me.lblMask.TabIndex = 0
        Me.lblMask.Tag = "5"
        Me.lblMask.Text = "POS"
        Me.lblMask.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(783, 278)
        Me.ShapeContainer1.TabIndex = 9
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.Location = New System.Drawing.Point(540, 89)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(230, 26)
        '
        'scFooter
        '
        Me.scFooter.BackColor = System.Drawing.Color.White
        Me.scFooter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scFooter.ForeColor = System.Drawing.Color.Transparent
        Me.scFooter.Location = New System.Drawing.Point(0, 0)
        Me.scFooter.Margin = New System.Windows.Forms.Padding(0)
        Me.scFooter.Name = "scFooter"
        Me.scFooter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scFooter.Panel1
        '
        Me.scFooter.Panel1.BackColor = System.Drawing.Color.White
        Me.scFooter.Panel1.Controls.Add(Me.ErrMsg)
        '
        'scFooter.Panel2
        '
        Me.scFooter.Panel2.BackColor = System.Drawing.Color.White
        Me.scFooter.Panel2.Controls.Add(Me.pnlFooter)
        Me.scFooter.Size = New System.Drawing.Size(783, 60)
        Me.scFooter.SplitterDistance = 31
        Me.scFooter.SplitterWidth = 1
        Me.scFooter.TabIndex = 0
        Me.scFooter.TabStop = False
        '
        'ErrMsg
        '
        Me.ErrMsg.BackColor = System.Drawing.Color.White
        Me.ErrMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ErrMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ErrMsg.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrMsg.ForeColor = System.Drawing.Color.Red
        Me.ErrMsg.Location = New System.Drawing.Point(0, 0)
        Me.ErrMsg.Margin = New System.Windows.Forms.Padding(0)
        Me.ErrMsg.Name = "ErrMsg"
        Me.ErrMsg.Size = New System.Drawing.Size(783, 31)
        Me.ErrMsg.TabIndex = 7
        Me.ErrMsg.Tag = "7"
        Me.ErrMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.White
        Me.pnlFooter.Controls.Add(Me.Label3)
        Me.pnlFooter.Controls.Add(Me.Label2)
        Me.pnlFooter.Controls.Add(Me.Label1)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFooter.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.pnlFooter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFooter.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(783, 28)
        Me.pnlFooter.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(3, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Tag = "1"
        Me.Label3.Text = "Build Date : 07 - Sep - 2015"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(669, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Tag = "1"
        Me.Label2.Text = "Version : 1.0.0.0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(300, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Tag = "1"
        Me.Label1.Text = "System : (DIET)"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Company"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Company"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 186
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Database"
        Me.DataGridViewTextBoxColumn2.FillWeight = 70.0!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Database"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 130
        '
        'Login
        '
        Me.AcceptButton = Me.Add
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(783, 342)
        Me.ControlBox = False
        Me.Controls.Add(Me.scParent)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.MidnightBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.scParent.Panel1.ResumeLayout(False)
        Me.scParent.Panel2.ResumeLayout(False)
        CType(Me.scParent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scParent.ResumeLayout(False)
        Me.scHeader.Panel2.ResumeLayout(False)
        Me.scHeader.Panel2.PerformLayout()
        CType(Me.scHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scHeader.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Rowmatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scFooter.Panel1.ResumeLayout(False)
        Me.scFooter.Panel2.ResumeLayout(False)
        CType(Me.scFooter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scFooter.ResumeLayout(False)
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents scParent As System.Windows.Forms.SplitContainer
    Friend WithEvents scHeader As System.Windows.Forms.SplitContainer
    Friend WithEvents Password As System.Windows.Forms.TextBox
    Friend WithEvents UserCode As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUserCode As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents lblMask As System.Windows.Forms.Label
    Friend WithEvents scFooter As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents ErrMsg As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbDBServer As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Rowmatrix As System.Windows.Forms.DataGridView
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Database As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
