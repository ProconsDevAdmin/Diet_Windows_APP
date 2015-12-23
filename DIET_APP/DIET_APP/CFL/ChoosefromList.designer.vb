<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCFL
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SCParent = New System.Windows.Forms.SplitContainer()
        Me.cflFind = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.RowMatrix = New System.Windows.Forms.DataGridView()
        Me.VCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.SCParent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCParent.Panel1.SuspendLayout()
        Me.SCParent.Panel2.SuspendLayout()
        Me.SCParent.SuspendLayout()
        CType(Me.RowMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SCParent
        '
        Me.SCParent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCParent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SCParent.IsSplitterFixed = True
        Me.SCParent.Location = New System.Drawing.Point(0, 0)
        Me.SCParent.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.SCParent.Name = "SCParent"
        Me.SCParent.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SCParent.Panel1
        '
        Me.SCParent.Panel1.Controls.Add(Me.cflFind)
        '
        'SCParent.Panel2
        '
        Me.SCParent.Panel2.Controls.Add(Me.btnClose)
        Me.SCParent.Panel2.Controls.Add(Me.RowMatrix)
        Me.SCParent.Panel2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SCParent.Size = New System.Drawing.Size(626, 293)
        Me.SCParent.SplitterDistance = 25
        Me.SCParent.SplitterWidth = 5
        Me.SCParent.TabIndex = 1
        Me.SCParent.TabStop = False
        '
        'cflFind
        '
        Me.cflFind.BackColor = System.Drawing.Color.Silver
        Me.cflFind.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cflFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cflFind.ForeColor = System.Drawing.Color.Black
        Me.cflFind.Location = New System.Drawing.Point(0, 0)
        Me.cflFind.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.cflFind.Name = "cflFind"
        Me.cflFind.Size = New System.Drawing.Size(626, 24)
        Me.cflFind.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(613, 251)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(2, 1)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'RowMatrix
        '
        Me.RowMatrix.AllowUserToAddRows = False
        Me.RowMatrix.AllowUserToDeleteRows = False
        Me.RowMatrix.AllowUserToResizeRows = False
        Me.RowMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.RowMatrix.BackgroundColor = System.Drawing.Color.LightBlue
        Me.RowMatrix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RowMatrix.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.RowMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RowMatrix.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.VCode, Me.VName})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RowMatrix.DefaultCellStyle = DataGridViewCellStyle5
        Me.RowMatrix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RowMatrix.EnableHeadersVisualStyles = False
        Me.RowMatrix.GridColor = System.Drawing.Color.Silver
        Me.RowMatrix.Location = New System.Drawing.Point(0, 0)
        Me.RowMatrix.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.RowMatrix.MultiSelect = False
        Me.RowMatrix.Name = "RowMatrix"
        Me.RowMatrix.ReadOnly = True
        Me.RowMatrix.RowHeadersVisible = False
        Me.RowMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.RowMatrix.Size = New System.Drawing.Size(626, 263)
        Me.RowMatrix.TabIndex = 1
        '
        'VCode
        '
        Me.VCode.DataPropertyName = "VCode"
        Me.VCode.FillWeight = 64.98492!
        Me.VCode.HeaderText = "VCode"
        Me.VCode.Name = "VCode"
        Me.VCode.ReadOnly = True
        '
        'VName
        '
        Me.VName.DataPropertyName = "VName"
        Me.VName.FillWeight = 96.30421!
        Me.VName.HeaderText = "VName"
        Me.VName.Name = "VName"
        Me.VName.ReadOnly = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ItemCode"
        Me.DataGridViewTextBoxColumn1.FillWeight = 62.39257!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Item Code"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 130
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "ItemName"
        Me.DataGridViewTextBoxColumn2.FillWeight = 152.2843!
        Me.DataGridViewTextBoxColumn2.HeaderText = "Item Name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 129
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "InStock"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn3.FillWeight = 85.32317!
        Me.DataGridViewTextBoxColumn3.HeaderText = "InStock"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 130
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "TaxCode"
        Me.DataGridViewTextBoxColumn4.HeaderText = "TaxCode"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "TaxRate"
        Me.DataGridViewTextBoxColumn5.HeaderText = "TaxRate"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "UOM"
        Me.DataGridViewTextBoxColumn6.HeaderText = "UOM"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "IsSerial"
        Me.DataGridViewTextBoxColumn7.FillWeight = 35.0!
        Me.DataGridViewTextBoxColumn7.HeaderText = "Is Serial"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 156
        '
        'frmCFL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Azure
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(626, 293)
        Me.Controls.Add(Me.SCParent)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCFL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Choose From List"
        Me.SCParent.Panel1.ResumeLayout(False)
        Me.SCParent.Panel1.PerformLayout()
        Me.SCParent.Panel2.ResumeLayout(False)
        CType(Me.SCParent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCParent.ResumeLayout(False)
        CType(Me.RowMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SCParent As System.Windows.Forms.SplitContainer
    Friend WithEvents cflFind As System.Windows.Forms.TextBox
    Friend WithEvents RowMatrix As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
