Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Text As String = TextBox1.Text
        Text = Replace(Text, vbCrLf + vbCrLf, "¬")
        Text = Replace(Text, vbCrLf, " ")
        'Dim spaceSplit As String() = Split(Text, " ")
        'Text = ""
        'Dim pos As Integer = 0
        'For Each s As String In spaceSplit
        ' If pos + Len(s) > 63 Then
        ' Text = Text + vbCrLf + s
        ' pos = Len(s)
        ' ElseIf Text = "" Then
        ' Text = s
        ' pos = Len(s)
        ' Else
        ' Text = Text + " " + s
        ' pos = pos + Len(s) + 1
        ' End If
        'Next
        Text = Replace(Text, "¬", vbCrLf + vbCrLf)
        Dim lines As String() = Text.Split(vbCrLf)
        Text = ""
        For Each line As String In lines
            Text = Text + "P(""" + line.Trim() + """)" + vbCrLf
        Next
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


