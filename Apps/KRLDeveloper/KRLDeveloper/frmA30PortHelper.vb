Imports System.IO

Public Class frmA30PortHelper

    Sub loadMe() Handles Me.Load
        ComboBox1.SelectedIndex = 0
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim filname As String = KRLDeveloper.TextBox2.Text + ComboBox1.Text
        RichTextBox1.Text = File.ReadAllText(filname)
        RichTextBox2.Text = Suggest(RichTextBox1.Text)
    End Sub

    Function Suggest(text As String) As String
        text = Replace(text, vbLf, vbCr)
        text = Replace(text, vbCr, vbLf)
        text = Replace(text, vbLf, vbCrLf)
        text = Replace(text, vbCrLf + vbCrLf, vbCrLf)
        Dim lines() As String = text.Split(vbCrLf)
        Dim output As String = ""
        Dim spellname As String = ""
        For Each line As String In lines
            If line.Trim.StartsWith("""") Then
                spellname = Split(line, """")(1)
            End If
            If line.Trim.StartsWith("effect = ") Then
                output = output + Replace(line, "effect = ", "effect = Name """ + spellname + """ ")
            Else
                output = output + line
            End If
        Next
        Return output
    End Function

End Class