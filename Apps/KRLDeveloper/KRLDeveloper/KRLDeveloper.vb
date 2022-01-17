Imports System.IO

Public Class KRLDeveloper

    Dim Cod As String
    Dim Added As Integer = 0
    Dim KeepGoing As Integer = 1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New Items
        frm.VanillaDir = TextBox1.Text
        frm.ShowDialog()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Me.DesignMode Then Exit Sub
        ModDir = TextBox1.Text
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        KeepGoing = 1
        Do While KeepGoing = 1
            Dim questi As String = "Display instructions/Create the code for a new creature by answering questions/View new creature's code/Exit"
            Dim optionSelection As String = SelectFromList("What do you wish to do?", questi)
            If optionSelection = Split(questi, "/")(0) Then DisplayInstructions()
            If optionSelection = Split(questi, "/")(1) Then Cod = GetCreatureCode()
            If optionSelection = Split(questi, "/")(2) Then ShowCode()
            If optionSelection = Split(questi, "/")(3) Then KeepGoing = 0
            If optionSelection = "" Then KeepGoing = 0
        Loop

    End Sub

    Sub DisplayInstructions()
        MsgBox(ByteToString(My.Resources.monster_generator_instructions))
    End Sub

    Function ByteToString(bye() As Byte) As String
        Return System.Text.Encoding.ASCII.GetString(bye)
    End Function

    Sub ShowCode()
        If Cod = "" Then
            MsgBox("No code has been generated yet.")
            Exit Sub
        End If
        TextViewer.RichTextBox1.Text = Cod
        TextViewer.ShowDialog()
    End Sub

    Public Function GetCreatureCode()
        Added = 0
        Dim replacement As String = ""
        Dim creaturetext As String = ByteToString(My.Resources.monster_template)
        Do While InStr(creaturetext, "|") > 0
            Dim section As String = Split(creaturetext, "|")(1)
            Dim Quest As String = Split(section, "@")(1)
            Dim Answ As String = Split(section, "@")(0)
            If InStr(Quest, "(Y/N)") > 0 Then
                If Choice(Quest) Then
                    replacement = Answ
                Else
                    replacement = ""
                End If
            ElseIf InStr(Answ, "/") > 0 Then
                replacement = SelectFromList(Quest, Answ)
                Do While replacement = ""
                    If Choice("Really cancel?") Then
                        Return ""
                    End If
                    replacement = SelectFromList(Quest, Answ)
                Loop
            Else
                replacement = GetText(Quest, Answ)
                Do While replacement = ""
                    If Choice("Really cancel?") Then
                        Return ""
                    End If
                    replacement = GetText(Quest, Answ)
                Loop
            End If
            creaturetext = Replace(creaturetext, "|" + section + "|", replacement)
        Loop
        creaturetext = RemoveRedundancy(creaturetext)
        creaturetext = InsertAttacks(creaturetext)
        Return creaturetext
    End Function

    Function RemoveRedundancy(txt)
        Dim contents As String = ByteToString(My.Resources.monster_template_redundancy)
        Do While InStr(contents, "|") > 0
            Dim section As String = Split(contents, "|")(1)
            txt = Replace(txt, section, "")
            contents = Replace(contents, "|" + section + "|", "")
        Loop
        Return txt
    End Function

    Function InsertAttacks(txt) As String
        Dim contents As String = ByteToString(My.Resources.monster_template_attacks)
        Do While InStr(contents, "|") > 0
            Dim section As String = Split(contents, "|")(1)
            Dim sKey As String = Split(section, "@")(0)
            Dim sValue As String = Split(section, "@")(1)
            txt = Replace(txt, sKey, sValue)
            contents = Replace(contents, "|" + section + "|", "")
        Loop
        Return txt
    End Function

    Function getContents(filnam As String) As String
        Return File.ReadAllText(filnam)
    End Function

    Function Choice(messages) As Boolean

        Choice = MsgBox(messages, 4, "Creature creation") - 7

    End Function

    Function GetText(messages As String, Def As String) As String

        Return Trim(InputBox(messages, "Creature creation", Def))

    End Function

    Function SelectFromList(questi As String, answ As String) As String
        questi = questi + vbCrLf + "Select from the list below:"
        Dim numbered As Integer = 0
        Dim count As Integer = 0
        For count = 0 To UBound(Split(answ, "/"))
            Dim entry As String = Split(answ, "/")(count)
            If IsNumeric(entry) Then
                questi = questi + vbCrLf + entry
                numbered = 1
            Else
                questi = questi + vbCrLf + CStr(count + 1) + ") " + entry
            End If
        Next
        questi = questi + vbCrLf + vbCrLf + vbCrLf
        Dim contin As Integer = 1
        Dim tryit As String = ""
        Do While contin = 1
            tryit = GetText(questi, Split(answ, "/")(0))
            If InStr(answ, tryit) > 0 And tryit <> "" Then contin = 0
            If IsNumeric(tryit) Then If CInt(tryit) > 0 And CInt(tryit) < count + 1 Then contin = 0
            If tryit = "" Then
                Return ""
            End If
        Loop
        If Not IsNumeric(tryit) Or numbered = 1 Then
            Return tryit
        Else
            Return Split(answ, "/")(tryit - 1)
        End If
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmVanillaViewer.VanillaFolder = TextBox1.Text
        frmVanillaViewer.ShowDialog()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmSprites.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextViewer.RichTextBox1.Text = My.Resources.COPYING
        TextViewer.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        frmA30PortHelper.ShowDialog()
    End Sub

End Class
