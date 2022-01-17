
Public Class Items

    Private Property CurrentItem As Integer = 1
    Public VanillaDir As String

    Private Sub Generate() Handles TabControl1.SelectedIndexChanged
        If Me.DesignMode Then Exit Sub
        RichTextBox1.Text = ""
        Dim lstCTRLs As New SortedDictionary(Of Long, Control)
        Dim nCount As Integer = 0
        For Each ctrl As Control In TabControl1.TabPages(0).Controls
            If ctrl.Tag Is Nothing Then
                ctrl.Tag = ctrl.Name
            End If
            If Len(ctrl.Tag.ToString()) = 0 Then
                ctrl.Tag = ctrl.Name
            End If
            lstCTRLs.Add(ctrl.Top * 1000 + ctrl.Left, ctrl)
            nCount = nCount + 1
        Next
        For Each ctrl As Control In lstCTRLs.Values
            If ctrl.Text <> "" And Not ctrl.Name.ToUpper().Contains("LABEL") Then
                RichTextBox1.Text = RichTextBox1.Text + ctrl.Tag + " = " + ctrl.Text + vbCrLf
            End If
        Next
    End Sub
    Private Sub ButtonLabelled_Click(sender As Object, e As EventArgs) Handles ButtonLabelled.Click
        Try
            CurrentItem = CurrentItem + 1
            ReadItem()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ReadItem() Handles Me.Load
        Dim fil As String = Replace(VanillaDir + "\items.txt", "\\", "\")
        Dim content As String = System.IO.File.ReadAllText(fil)
        Dim kvps As Dictionary(Of String, String) = GetSection(content, CurrentItem)
        For Each ctrl As Control In TabControl1.TabPages(0).Controls
            If kvps.ContainsKey(UCase(ctrl.Name.Trim)) Then
                ctrl.Text = kvps(ctrl.Name.Trim.ToUpper)
            ElseIf Not ctrl.Tag Is Nothing Then
                If kvps.ContainsKey(UCase(ctrl.Tag.Trim)) Then
                    ctrl.Text = kvps(ctrl.Tag.Trim.ToUpper)
                ElseIf Not ctrl.Name.ToUpper.Contains("LABEL") Then
                    ctrl.Text = ""
                End If
            ElseIf Not ctrl.Name.ToUpper.Contains("LABEL") Then
                ctrl.Text = ""
            End If
        Next
    End Sub

    Private Function GetSection(input As String, requiredSection As Integer) As Dictionary(Of String, String)
        Dim splitByOpen As String() = Split(input, "{")
        Dim ret As String = ""
        Dim sectionNo As Integer = 1
        For opens = 1 To splitByOpen.Count - 1
            If ret.Split("{").Count = ret.Split("}").Count Then
                sectionNo = sectionNo + 1
                If sectionNo = requiredSection + 2 Then
                    Exit For
                End If
                ret = ""
            End If
            ret = ret + "{" + splitByOpen(opens)
        Next
        Return trimSection(ret)
    End Function

    Private Function trimSection(input As String) As Dictionary(Of String, String)
        Dim ret As New Dictionary(Of String, String)
        Dim inputNoWhiteSpace As String = Replace(input, vbCrLf, " ")
        inputNoWhiteSpace = Replace(inputNoWhiteSpace, vbCr, " ")
        inputNoWhiteSpace = Replace(inputNoWhiteSpace, vbLf, " ")
        inputNoWhiteSpace = Replace(inputNoWhiteSpace, vbTab, " ")

        Do While inputNoWhiteSpace.Contains("  ")
            inputNoWhiteSpace = Replace(inputNoWhiteSpace, "  ", " ")
        Loop

        Dim inputWithCorrectLineBreaks As String = ""

        For ncount = 1 To inputNoWhiteSpace.Split(" ").Length - 3
            Dim CurrentPart As String = inputNoWhiteSpace.Split(" ")(ncount)
            Dim NextPart As String = inputNoWhiteSpace.Split(" ")(ncount + 1)
            If NextPart.Contains("=") And inputWithCorrectLineBreaks.Split("{").Count <= inputWithCorrectLineBreaks.Split("}").Count Then
                inputWithCorrectLineBreaks = inputWithCorrectLineBreaks + vbCrLf + CurrentPart + " "
            Else
                inputWithCorrectLineBreaks = inputWithCorrectLineBreaks + CurrentPart + " "
            End If
        Next
        inputWithCorrectLineBreaks = inputWithCorrectLineBreaks.Trim
        Dim inputWithCorrectEnding As String = ""
        Dim ncount2 As Integer = 0
        Do While ncount2 <= inputWithCorrectLineBreaks.Split("}").Length - 3
            inputWithCorrectEnding = inputWithCorrectEnding + inputWithCorrectLineBreaks.Split("}")(ncount2) + "}"
            ncount2 = ncount2 + 1
        Loop
        inputWithCorrectEnding = inputWithCorrectEnding + inputWithCorrectLineBreaks.Split("}")(ncount2)
        Dim splitByLines As String() = inputWithCorrectEnding.Split(vbCrLf)
        For ncount = 0 To splitByLines.Length - 1
            Dim key As String = UCase(splitByLines(ncount).Split("=")(0).Trim)
            Dim value As String = splitByLines(ncount).Substring(Len(key) + 3)
            ret.Add(key, value)
        Next ncount
        Return ret
    End Function
    Private Sub ButtonLabel2_Click(sender As Object, e As EventArgs) Handles ButtonLabel2.Click
        If CurrentItem > 1 Then
            CurrentItem = CurrentItem - 1
            ReadItem()
        End If
    End Sub

End Class
