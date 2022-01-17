<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucBracketLastingEffect
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
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"SLEEP", "PANIC", "RAGE", "SLOWED", "SPEED", "DAM_BONUS", "DEF_BONUS", "BLEEDING", "HALLU", "BLIND", "INVISIBLE", "POISON", "ENTANGLED", "TIED_UP", "IMMOBILE", "STUNNED", "POISON_RESISTANT", "FIRE_RESISTANT", "COLD_RESISTANT", "FLYING", "COLLAPSED", "INSANITY", "PEACEFULNESS", "LIGHT_SOURCE", "DARKNESS_SOURCE", "PREGNANT", "PLAGUE", "PLAGUE_RESISTANT", "SLEEP_RESISTANT", "MAGIC_RESISTANCE", "MELEE_RESISTANCE", "RANGED_RESISTANCE", "MAGIC_VULNERABILITY", "MELEE_VULNERABILITY", "RANGED_VULNERABILITY", "MAGIC_CANCELLATION", "SPELL_DAMAGE", "ELF_VISION", "NIGHT_VISION", "REGENERATION", "WARNING", "TELEPATHY", "SUNLIGHT_VULNERABLE", "SATIATED", "RESTED", "SUMMONED", "HATE_DWARVES", "HATE_UNDEAD", "HATE_HUMANS", "HATE_GREENSKINS", "HATE_ELVES", "FAST_CRAFTING", "FAST_TRAINING", "SLOW_CRAFTING", "SLOW_TRAINING", "ENTERTAINER", "BAD_BREATH", "ON_FIRE", "FROZEN", "AMBUSH_SKILL", "STEALING_SKILL", "SWIMMING_SKILL", "DISARM_TRAPS_SKILL", "CONSUMPTION_SKILL", "COPULATION_SKILL", "CROPS_SKILL", "SPIDER_SKILL", "EXPLORE_SKILL", "EXPLORE_NOCTURNAL_SKILL", "EXPLORE_CAVES_SKILL", "BRIDGE_BUILDING_SKILL", "NAVIGATION_DIGGING_SKILL", "DISAPPEAR_DURING_DAY", "NO_CARRY_LIMIT", "SPYING"})
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(198, 24)
        Me.ComboBox1.TabIndex = 0
        '
        'ucBracketLastingEffect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "ucBracketLastingEffect"
        Me.Size = New System.Drawing.Size(198, 27)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ComboBox1 As ComboBox
End Class
