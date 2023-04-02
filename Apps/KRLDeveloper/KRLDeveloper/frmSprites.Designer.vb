<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSprites
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnLoadNext = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.btnNextColour = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(52, 116)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(80, 71)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'btnLoadNext
        '
        Me.btnLoadNext.Location = New System.Drawing.Point(785, 21)
        Me.btnLoadNext.Name = "btnLoadNext"
        Me.btnLoadNext.Size = New System.Drawing.Size(53, 20)
        Me.btnLoadNext.TabIndex = 1
        Me.btnLoadNext.Text = "Next"
        Me.btnLoadNext.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(538, 22)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(173, 20)
        Me.TextBox1.TabIndex = 2
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(358, 116)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(480, 240)
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(52, 22)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(462, 20)
        Me.TextBox2.TabIndex = 4
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(298, 219)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(54, 23)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add ->"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(358, 389)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(338, 20)
        Me.TextBox4.TabIndex = 8
        '
        'btnNextColour
        '
        Me.btnNextColour.Location = New System.Drawing.Point(298, 164)
        Me.btnNextColour.Name = "btnNextColour"
        Me.btnNextColour.Size = New System.Drawing.Size(54, 49)
        Me.btnNextColour.TabIndex = 9
        Me.btnNextColour.Text = "Next color"
        Me.btnNextColour.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(713, 388)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(49, 389)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Label1"
        '
        'Timer1
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(125, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Input image"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(565, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "output image"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(640, 81)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 29)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(726, 22)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(53, 20)
        Me.btnLast.TabIndex = 17
        Me.btnLast.Text = "Back"
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(297, 139)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(54, 20)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Invis"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmSprites
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(850, 439)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnNextColour)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnLoadNext)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "frmSprites"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnLoadNext As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents btnNextColour As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents btnLast As Button
    Friend WithEvents Button2 As Button
End Class
