<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucSound
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"BEAST_DEATH", "HUMANOID_DEATH", "BLUNT_DAMAGE", "BLADE_DAMAGE", "BLUNT_NO_DAMAGE", "BLADE_NO_DAMAGE", "MISSED_ATTACK", "DIG_MARK", "DIG_UNMARK", "ADD_CONSTRUCTION", "REMOVE_CONSTRUCTION", "DIGGING", "CONSTRUCTING", "TREE_CUTTING", "TRAP_ARMING", "BANG_DOOR", "SHOOT_BOW", "WHIP", "CREATE_IMP", "DYING_PIG", "DYING_DONKEY", "SPELL_HEALING", "SPELL_SUMMON_INSECTS", "SPELL_DECEPTION", "SPELL_SPEED_SELF", "SPELL_STR_BONUS", "SPELL_DEX_BONUS", "SPELL_FIRE_SPHERE_PET", "SPELL_TELEPORT", "SPELL_INVISIBILITY", "SPELL_WORD_OF_POWER", "SPELL_AIR_BLAST", "SPELL_SUMMON_SPIRIT", "SPELL_PORTAL", "SPELL_CURE_POISON", "SPELL_METEOR_SHOWER", "SPELL_MAGIC_SHIELD", "SPELL_BLAST", "SPELL_STUN_RAY"})
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(200, 24)
        Me.ComboBox1.TabIndex = 0
        '
        'ucSound
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "ucSound"
        Me.Size = New System.Drawing.Size(200, 25)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ComboBox1 As ComboBox
End Class
