Public Class Form1

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Text As String = TextBox1.Text
        Text = Replace(Text, vbCrLf + vbCrLf, "¬")
        Text = Replace(Text, vbCrLf, " ")
        Text = Replace(Text, "¬", vbCrLf + vbCrLf)
        Dim lines As String() = Text.Split(vbCrLf)
        Text = ""
        For Each line As String In lines
            Text = Text + "P(""" + line.Trim() + """)" + vbCrLf
        Next
        TextBox1.Text = "# View with word wrap! " + vbCrLf + vbCrLf + Text
    End Sub

End Class
