Imports System.IO

Public Class clsCreaturePopulator

    Private inputRow As Long
    Dim bracketcount As Integer

    Public Property FileName As String
    Public Property dctLines As Dictionary(Of Integer, String)
    Public Property DisplayData As Creatures
    Public Property lineBreak As String = "<NEVER>"
    Public Property lineBreak2 As String = "<NEVER>"

    Public Sub Load()
        inputRow = 0
        DisplayData = New Creatures
        FileName = Replace(FileName, "\\", "\")
        Dim txt As String = File.ReadAllText(FileName)
        txt = txt.Replace(vbCr, vbLf)
        txt = txt.Replace(vbLf + vbLf, vbLf)
        Dim lins As String() = txt.Split(vbLf)
        dctLines = New Dictionary(Of Integer, String)
        For Each lin As String In lins
            dctLines.Add(dctLines.Count + 1, lin)
        Next
        Do While inputRow < dctLines.Count
            inputRow = inputRow + 1
            UseText(ztrim(dctLines(inputRow)))
        Loop
    End Sub

    Sub UseText(txt As String)
        Debug.Print(txt)
        Dim key As String
        Dim value As String
        If txt = "" Then Exit Sub
        txt = txt.Trim
        If txt.Trim.StartsWith(lineBreak) Or txt.Trim.StartsWith(lineBreak2) Then
            AddSection(txt)
            Exit Sub
        End If
        If txt.Trim.StartsWith("#") Then
            Exit Sub
        End If
        If txt.Contains("{") And txt.Contains("=") Then
            populateMultiCell(txt)
            Exit Sub
        End If
        If Not txt.Contains("=") Then
            If DisplayData.Columns.Count = 0 Or txt.Trim = "{" Or txt.Trim() = "}" Then Exit Sub
            Dim ncount As Integer
            For Each col As DataColumn In DisplayData.Columns
                If col.ColumnName.StartsWith("Value") Then
                    If DisplayData.Rows(DisplayData.Rows.Count - 1).Item(col).ToString <> "" Then
                        ncount = ncount + 1
                    End If
                End If
            Next
            key = "Value" + ncount.ToString()
            value = txt
        Else
            key = Split(txt, "=")(0)
            key = ztrim(key)
            value = Split(txt, "=")(1)
            value = ztrim(value)
        End If
        PopulateCell(key, value)
    End Sub

    Private Sub AddSection(txt As String)
        DisplayData.Rows.Add()
        PopulateCell("Section", txt)
    End Sub

    Private Sub populateMultiCell(txt As String)
        Dim finaltext As String = ""
        bracketcount = 0
        Do
            Dim addedText As String
            addedText = ztrim(dctLines(inputRow))
            If finaltext <> "" Then finaltext = finaltext + " "
            If Not finaltext.Contains("#") Then
                finaltext = finaltext + addedText
            End If
            bracketcount = bracketcount + Split(addedText, "{").Count
            bracketcount = bracketcount - Split(addedText, "}").Count
            inputRow = inputRow + 1
        Loop While inputRow < dctLines.Count And bracketcount > 0
        inputRow = inputRow - 1
        Dim key As String
        Dim value As String
        If Split(finaltext, "=").Count > 1 Then
            key = Trim(Split(finaltext, "=")(0))
            value = Right(finaltext, Len(finaltext) - Len(finaltext.Split("=")(0)) - 1)
            PopulateCell(key, value)
        End If
    End Sub

    Private Sub PopulateCell(key As String, value As String)
        If Not DisplayData.Columns.Contains(key) Then
            DisplayData.Columns.Add(key)
        End If
        If PopulateCellAttempt(key, value) Then Exit Sub
    End Sub

    Private Function PopulateCellAttempt(key As String, value As String) As Boolean

        Dim outputcolumn As Integer
        Application.DoEvents()
        outputcolumn = 0
        For Each col As DataColumn In DisplayData.Columns
            Dim ColKey As String
            ColKey = col.ColumnName
            ColKey = UCase(ztrim(ColKey))
            If ColKey = UCase(ztrim(key)) Then
                DisplayData.Cells(DisplayData.Rows.Count - 1, outputcolumn) = DisplayData.Cells(DisplayData.Rows.Count - 1, outputcolumn) + value
                Return True
            End If
            outputcolumn = outputcolumn + 1
        Next

    End Function

    Private Function ztrim(txt As String) As String
        For f = 1 To 31
            txt = Replace(txt, Asc(f), "")
        Next f
        ztrim = Trim(txt)
    End Function

End Class
