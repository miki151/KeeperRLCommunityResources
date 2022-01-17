
Public Class ucQuotedString

        Public Overrides Property Text As String
        Set(value As String)
            value = value.Trim
            If value.StartsWith("""") And value.EndsWith("""") Then
                TextBox1.Text = value.Substring(1, Len(value) - 2)
            Else
                TextBox1.Text = value
            End If
        End Set
        Get
            If TextBox1.Text = "" Then
                Return ""
            End If
            Return """" + TextBox1.Text + """"
        End Get
    End Property

End Class