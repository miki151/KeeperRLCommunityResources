Public Class ucEffect

    Private frm As New frmEffect

    Public Overrides Property Text As String
        Set(value As String)
            frmEffect.EffectText = value
            Button1.Text = value
        End Set
        Get
            Return frmEffect.EffectText
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmEffect.Show()
        Do While frmEffect.visible
            Application.DoEvents()
        Loop
        Me.Text = frmEffect.EffectText
    End Sub

End Class
