Public Class ucSpell
    Public Overrides Property Text As String
        Set(value As String)
            value = value.Trim
            If value.StartsWith("""") And value.EndsWith("""") Then
                ComboBox1.Text = value.Substring(1, Len(value) - 2)
            Else
                ComboBox1.Text = value
            End If
        End Set
        Get
            If ComboBox1.Text = "" Then
                Return ""
            End If
            Return """" + ComboBox1.Text + """"
        End Get
    End Property

End Class
