Imports System.ComponentModel

Public Class frmEffect

    Public Sub frm_load() Handles Me.Load
        GetSignatures
    End Sub


    Public Property EffectText As String
        Get
            Return TextBox1.Text
        End Get
        Set(value As String)
            TextBox1.Text = value
        End Set
    End Property

    Private Sub CloseMe(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Me.DesignMode Then Exit Sub
        Me.Visible = False
        e.Cancel = True
    End Sub
    Private Sub LoadMe() Handles Me.Load
        If Me.DesignMode Then Exit Sub
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim = "" Then
            Exit Sub
        End If
        Dim TotalText As String = ComboBox1.Text
        TextBox1.Text = TotalText
        Dim FirstWord As String = Split(TotalText, " ")(0)
        If FirstWord <> TotalText Then
            TotalText = Microsoft.VisualBasic.Strings.Right(TotalText, Len(TotalText) - FirstWord.Length - 1)
        End If
        TextBox2.Text = FirstWord
        Do While TotalText.Contains("  ")
            TotalText = Replace(TotalText, "  ", " ")
        Loop
        Dim replacements As Integer = 0
        Do While TotalText.Split(" ").Count > 2 + replacements * 2
            Dim NewField As String = InputBox(TotalText.Split(" ")(1 + replacements * 2), TotalText.Split(" ")(2 + replacements * 2))
            Dim ToReplace As String = TotalText.Split(" ")(1 + replacements * 2) + " " + TotalText.Split(" ")(2 + replacements * 2)
            TotalText = TotalText.Replace(ToReplace, "INPUT: " + NewField)
            replacements = replacements + 1
            TextBox1.Text = FirstWord + TotalText
        Loop
        TextBox2.Left = ComboBox1.Left - 8 - TextBox2.Width
        TextBox1.Text = Replace(TextBox1.Text, "INPUT: ", "")
    End Sub

    Private Sub GetSignatures()
        ComboBox1.Items.Clear()
        Dim txt As String = My.Resources.effect_type
        Dim started As Boolean = False
        Dim newStruct As Boolean = False
        Dim lstStructs As New Dictionary(Of String, String)
        Dim structList As String = ""
        Dim NewStructName As String = ""
        For Each line As String In Split(txt, vbCrLf)
            If line.Contains("namespace Effects {") Then
                started = True
                Continue For
            End If
            If started = False Then
                Continue For
            End If
            If line.StartsWith("SIMPLE_EFFECT") Then
                Dim item As String = Replace(line, "SIMPLE_EFFECT(", "")
                item = Replace(item, ");", "")
                ComboBox1.Items.Add(item)
                Continue For
            End If
            If line.StartsWith("struct ") Then
                NewStruct = True
                structList = ""
                NewStructName = line.Replace("struct ", "")
                NewStructName = NewStructName.Replace("{", "")
                Continue For
            End If
            If newStruct = True And line.StartsWith("}") Then
                lstStructs.Add(NewStructName, structList)
                newStruct = False
                Continue For
            End If
            If newStruct = True Then
                If line.Contains("SERIALIZE") Then
                    'Do nothing
                ElseIf line.Contains("SERIAL(") Then
                    Dim text As String = line.Replace("SERIAL(", "")
                    text = Replace(text, ");", "")
                    structList = structList + text
                Else
                    structList = structList + line
                End If
            End If
        Next
        For Each Struct As String In lstStructs.Keys
            ComboBox1.Items.Add(Struct + lstStructs(Struct))
        Next
    End Sub

End Class