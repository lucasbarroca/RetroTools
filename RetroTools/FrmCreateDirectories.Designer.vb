<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCreateDirectories
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
        Me.txtC = New System.Windows.Forms.TextBox()
        Me.BtFolder = New System.Windows.Forms.Button()
        Me.TxtFolder = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtC
        '
        Me.txtC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtC.Location = New System.Drawing.Point(12, 47)
        Me.txtC.Multiline = True
        Me.txtC.Name = "txtC"
        Me.txtC.Size = New System.Drawing.Size(563, 270)
        Me.txtC.TabIndex = 0
        '
        'BtFolder
        '
        Me.BtFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtFolder.Location = New System.Drawing.Point(526, 12)
        Me.BtFolder.Name = "BtFolder"
        Me.BtFolder.Size = New System.Drawing.Size(49, 29)
        Me.BtFolder.TabIndex = 15
        Me.BtFolder.Text = "..."
        Me.BtFolder.UseVisualStyleBackColor = True
        '
        'TxtFolder
        '
        Me.TxtFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFolder.Location = New System.Drawing.Point(12, 12)
        Me.TxtFolder.Name = "TxtFolder"
        Me.TxtFolder.Size = New System.Drawing.Size(508, 29)
        Me.TxtFolder.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(12, 323)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(144, 29)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "CREATE FOLDERS"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(431, 323)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 29)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "REPLACE %R"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(281, 323)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(144, 29)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "OPEN URLS/FILES"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'FrmCreateDirectories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 362)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtFolder)
        Me.Controls.Add(Me.TxtFolder)
        Me.Controls.Add(Me.txtC)
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "FrmCreateDirectories"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmCreateDirectories"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtC As TextBox
    Friend WithEvents BtFolder As Button
    Friend WithEvents TxtFolder As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
