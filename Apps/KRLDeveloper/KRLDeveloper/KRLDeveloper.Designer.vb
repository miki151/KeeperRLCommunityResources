<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KRLDeveloper
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(497, 169)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "New Item"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(129, 112)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(611, 22)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\data_free\game_config\"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(129, 140)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(610, 22)
        Me.TextBox2.TabIndex = 2
        Me.TextBox2.Text = "C:\KeeperRLMods\Alpha30\Alpha30Bonus\"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Vanilla folder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Mod Folder:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(366, 169)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(125, 32)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Creature Wizard"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(242, 169)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(118, 33)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Vanilla Viewer"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(129, 168)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(107, 33)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "Sprites"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(680, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "This app is just currently a collection of code that may be useful. It is not fit" &
    " for any particular purpose (yet)."
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(593, 56)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(119, 23)
        Me.Button5.TabIndex = 9
        Me.Button5.Text = "View License"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(619, 169)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(120, 32)
        Me.Button6.TabIndex = 10
        Me.Button6.Text = "A30 Port helper"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'KRLDeveloper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 204)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "KRLDeveloper"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
End Class
