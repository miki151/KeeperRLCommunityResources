Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Text As String = TextBox1.Text
        Text = Replace(Text, vbCrLf + vbCrLf, "¬")
        Text = Replace(Text, vbCrLf, " ")
        Text = Replace(Text, "¬", vbCrLf + vbCrLf)
        Dim lines As String() = Text.Split(vbCrLf)
        Text = ""
        For Each line As String In lines
            Text = Text + "P(""" + line.Trim() + """)" + vbCrLf
        Next
        Text = Text + "P("""")" + vbCrLf
        Text = "Def P(Text) Paragraph(500, Text) End" + vbCrLf +
            "MiniWindow(Scrolling(MarginRight(30, MarginLeft(30,  " + vbCrLf +
            "Vertical {" + vbCrLf +
            "Horizontal{ViewId(1, {""scroll""}) Width 10 {} Label(""<New Page>"")}" + vbCrLf +
            "Height 30 {}" + vbCrLf +
            "# View with word wrap! " + vbCrLf + vbCrLf + Text + vbCrLf +
            "} ))))"
        TextBox1.Text = Text
    End Sub
End Class


