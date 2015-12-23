<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ErrorLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.SystemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModulesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogOffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.PrintPreview = New System.Windows.Forms.ToolStripButton()
        Me.PDF = New System.Windows.Forms.ToolStripButton()
        Me.ExcelExport = New System.Windows.Forms.ToolStripButton()
        Me.AddMode = New System.Windows.Forms.ToolStripButton()
        Me.FindMode = New System.Windows.Forms.ToolStripButton()
        Me.FirstRecord = New System.Windows.Forms.ToolStripButton()
        Me.Previous = New System.Windows.Forms.ToolStripButton()
        Me.NextRecord = New System.Windows.Forms.ToolStripButton()
        Me.LastRecord = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.companyStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.timeStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Progress = New System.Windows.Forms.ToolStripProgressBar()
        Me.oTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuTree = New System.Windows.Forms.TreeView()
        Me.SCParent = New System.Windows.Forms.SplitContainer()
        Me.SCMenu = New System.Windows.Forms.SplitContainer()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BGMsgWorker = New System.ComponentModel.BackgroundWorker()
        Me.AlertTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnExpCol = New System.Windows.Forms.Button()
        Me.SalesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalesOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SCParent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCParent.Panel1.SuspendLayout()
        Me.SCParent.SuspendLayout()
        CType(Me.SCMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCMenu.Panel1.SuspendLayout()
        Me.SCMenu.Panel2.SuspendLayout()
        Me.SCMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ErrorLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 415)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(944, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ErrorLabel
        '
        Me.ErrorLabel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ErrorLabel.Name = "ErrorLabel"
        Me.ErrorLabel.Size = New System.Drawing.Size(929, 17)
        Me.ErrorLabel.Spring = True
        Me.ErrorLabel.Tag = "1"
        Me.ErrorLabel.Text = "Status"
        Me.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip
        '
        Me.MenuStrip.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SystemsToolStripMenuItem, Me.ModulesToolStripMenuItem, Me.ReportsToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(944, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Tag = "2"
        Me.MenuStrip.Text = "MenuStrip"
        '
        'SystemsToolStripMenuItem
        '
        Me.SystemsToolStripMenuItem.Name = "SystemsToolStripMenuItem"
        Me.SystemsToolStripMenuItem.Size = New System.Drawing.Size(72, 20)
        Me.SystemsToolStripMenuItem.Text = "Systems"
        '
        'ModulesToolStripMenuItem
        '
        Me.ModulesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalesToolStripMenuItem})
        Me.ModulesToolStripMenuItem.Name = "ModulesToolStripMenuItem"
        Me.ModulesToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.ModulesToolStripMenuItem.Text = "&Modules"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogOffToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
        Me.ReportsToolStripMenuItem.Text = "&Exit"
        '
        'LogOffToolStripMenuItem
        '
        Me.LogOffToolStripMenuItem.Name = "LogOffToolStripMenuItem"
        Me.LogOffToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.LogOffToolStripMenuItem.Text = "LogOff"
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPreview, Me.PDF, Me.ExcelExport, Me.AddMode, Me.FindMode, Me.FirstRecord, Me.Previous, Me.NextRecord, Me.LastRecord})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(944, 25)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip"
        '
        'PrintPreview
        '
        Me.PrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreview.Name = "PrintPreview"
        Me.PrintPreview.Size = New System.Drawing.Size(23, 22)
        Me.PrintPreview.Text = "Print"
        '
        'PDF
        '
        Me.PDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PDF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PDF.Name = "PDF"
        Me.PDF.Size = New System.Drawing.Size(23, 22)
        Me.PDF.Text = "Convert to PDF"
        '
        'ExcelExport
        '
        Me.ExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExcelExport.Name = "ExcelExport"
        Me.ExcelExport.Size = New System.Drawing.Size(23, 22)
        Me.ExcelExport.Text = "Export to Excel"
        '
        'AddMode
        '
        Me.AddMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddMode.Name = "AddMode"
        Me.AddMode.Size = New System.Drawing.Size(23, 22)
        Me.AddMode.Text = "Add"
        '
        'FindMode
        '
        Me.FindMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FindMode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FindMode.Name = "FindMode"
        Me.FindMode.Size = New System.Drawing.Size(23, 22)
        Me.FindMode.Text = "Find"
        '
        'FirstRecord
        '
        Me.FirstRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FirstRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FirstRecord.Name = "FirstRecord"
        Me.FirstRecord.Size = New System.Drawing.Size(23, 22)
        Me.FirstRecord.Text = "First"
        '
        'Previous
        '
        Me.Previous.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Previous.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Previous.Name = "Previous"
        Me.Previous.Size = New System.Drawing.Size(23, 22)
        Me.Previous.Text = "Prevoius"
        '
        'NextRecord
        '
        Me.NextRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NextRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NextRecord.Name = "NextRecord"
        Me.NextRecord.Size = New System.Drawing.Size(23, 22)
        Me.NextRecord.Text = "Next"
        '
        'LastRecord
        '
        Me.LastRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LastRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LastRecord.Name = "LastRecord"
        Me.LastRecord.Size = New System.Drawing.Size(23, 22)
        Me.LastRecord.Text = "Last"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.companyStatusLabel, Me.timeStatusLabel, Me.ProgressMsg, Me.Progress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 393)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(944, 22)
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'companyStatusLabel
        '
        Me.companyStatusLabel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.companyStatusLabel.Name = "companyStatusLabel"
        Me.companyStatusLabel.Size = New System.Drawing.Size(100, 17)
        Me.companyStatusLabel.Tag = "1"
        Me.companyStatusLabel.Text = "Company Details"
        '
        'timeStatusLabel
        '
        Me.timeStatusLabel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timeStatusLabel.Name = "timeStatusLabel"
        Me.timeStatusLabel.Size = New System.Drawing.Size(727, 17)
        Me.timeStatusLabel.Spring = True
        Me.timeStatusLabel.Tag = "1"
        Me.timeStatusLabel.Text = "as"
        Me.timeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProgressMsg
        '
        Me.ProgressMsg.Name = "ProgressMsg"
        Me.ProgressMsg.Size = New System.Drawing.Size(0, 17)
        '
        'Progress
        '
        Me.Progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(100, 16)
        '
        'oTimer
        '
        Me.oTimer.Enabled = True
        Me.oTimer.Interval = 10
        '
        'MenuTree
        '
        Me.MenuTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuTree.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuTree.Location = New System.Drawing.Point(0, 0)
        Me.MenuTree.Name = "MenuTree"
        Me.MenuTree.Size = New System.Drawing.Size(222, 310)
        Me.MenuTree.TabIndex = 2
        Me.MenuTree.Tag = "3"
        '
        'SCParent
        '
        Me.SCParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SCParent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCParent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCParent.IsSplitterFixed = True
        Me.SCParent.Location = New System.Drawing.Point(0, 49)
        Me.SCParent.Name = "SCParent"
        '
        'SCParent.Panel1
        '
        Me.SCParent.Panel1.Controls.Add(Me.SCMenu)
        '
        'SCParent.Panel2
        '
        Me.SCParent.Panel2.BackColor = System.Drawing.Color.White
        Me.SCParent.Panel2.BackgroundImage = CType(resources.GetObject("SCParent.Panel2.BackgroundImage"), System.Drawing.Image)
        Me.SCParent.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.SCParent.Panel2.Tag = "1"
        Me.SCParent.Size = New System.Drawing.Size(944, 344)
        Me.SCParent.SplitterDistance = 226
        Me.SCParent.TabIndex = 11
        '
        'SCMenu
        '
        Me.SCMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCMenu.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCMenu.IsSplitterFixed = True
        Me.SCMenu.Location = New System.Drawing.Point(0, 0)
        Me.SCMenu.Name = "SCMenu"
        Me.SCMenu.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCMenu.Panel1
        '
        Me.SCMenu.Panel1.Controls.Add(Me.btnExpCol)
        Me.SCMenu.Panel1MinSize = 18
        '
        'SCMenu.Panel2
        '
        Me.SCMenu.Panel2.Controls.Add(Me.MenuTree)
        Me.SCMenu.Size = New System.Drawing.Size(222, 340)
        Me.SCMenu.SplitterDistance = 26
        Me.SCMenu.TabIndex = 0
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'BGMsgWorker
        '
        '
        'AlertTimer
        '
        Me.AlertTimer.Interval = 1000
        '
        'btnExpCol
        '
        Me.btnExpCol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnExpCol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExpCol.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExpCol.Image = Global.DIET_APP.My.Resources.Resources.Expand
        Me.btnExpCol.Location = New System.Drawing.Point(0, 0)
        Me.btnExpCol.Name = "btnExpCol"
        Me.btnExpCol.Size = New System.Drawing.Size(222, 26)
        Me.btnExpCol.TabIndex = 0
        Me.btnExpCol.UseVisualStyleBackColor = True
        '
        'SalesToolStripMenuItem
        '
        Me.SalesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalesOrderToolStripMenuItem})
        Me.SalesToolStripMenuItem.Image = Global.DIET_APP.My.Resources.Resources.A
        Me.SalesToolStripMenuItem.Name = "SalesToolStripMenuItem"
        Me.SalesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SalesToolStripMenuItem.Text = "Sales"
        '
        'SalesOrderToolStripMenuItem
        '
        Me.SalesOrderToolStripMenuItem.Image = Global.DIET_APP.My.Resources.Resources.B
        Me.SalesOrderToolStripMenuItem.Name = "SalesOrderToolStripMenuItem"
        Me.SalesOrderToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SalesOrderToolStripMenuItem.Tag = "menuSaleOrder"
        Me.SalesOrderToolStripMenuItem.Text = "Pre - Sale Order"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 437)
        Me.Controls.Add(Me.SCParent)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DIET CENTER(DIET) - (SAP Business One - Bridged)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SCParent.Panel1.ResumeLayout(False)
        CType(Me.SCParent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCParent.ResumeLayout(False)
        Me.SCMenu.Panel1.ResumeLayout(False)
        Me.SCMenu.Panel2.ResumeLayout(False)
        CType(Me.SCMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents SystemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModulesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents PrintPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents Previous As System.Windows.Forms.ToolStripButton
    Friend WithEvents FirstRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents NextRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents LastRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddMode As System.Windows.Forms.ToolStripButton
    Friend WithEvents FindMode As System.Windows.Forms.ToolStripButton
    Friend WithEvents ExcelExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents PDF As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents companyStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents timeStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Progress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ProgressMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents oTimer As System.Windows.Forms.Timer
    Friend WithEvents SalesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuTree As System.Windows.Forms.TreeView
    Friend WithEvents SCParent As System.Windows.Forms.SplitContainer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SalesOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SCMenu As System.Windows.Forms.SplitContainer
    Friend WithEvents btnExpCol As System.Windows.Forms.Button
    Friend WithEvents BGMsgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents AlertTimer As System.Windows.Forms.Timer

End Class
