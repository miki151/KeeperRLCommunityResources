Public Class frmVanillaViewer

    Public VanillaFolder As String = "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\data_free\game_config"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvPlayerCreatures.AutoGenerateColumns = True

        'txt.StartsWith("{""") Or
        'txt.StartsWith("type =") Or
        Dim gridData1 As New clsCreaturePopulator
        gridData1.FileName = VanillaFolder + "\keeper_creatures.txt"
        gridData1.lineBreak = "{ """
        gridData1.lineBreak2 = "creatureId = "
        gridData1.Load()
        dgvPlayerCreatures.DataSource = gridData1.DisplayData

        Dim gridData2 As New clsCreaturePopulator
        gridData2.FileName = VanillaFolder + "\creatures.txt"
        gridData2.lineBreak = """"
        gridData2.Load()
        dgvCreatures.DataSource = gridData2.DisplayData

        Dim gridData3 As New clsCreaturePopulator
        gridData3.FileName = VanillaFolder + "\creature_inventory.txt"
        gridData3.lineBreak = "{"""
        'gridData3.Load()
        'dgvCreatureInventory.DataSource = gridData3.DisplayData

        Dim gridData4 As New clsCreaturePopulator
        gridData4.FileName = VanillaFolder + "\technology.txt"
        gridData4.lineBreak = """"
        gridData4.Load()
        dgvTechnology.DataSource = gridData4.DisplayData

        Dim gridData5 As New clsCreaturePopulator
        gridData5.FileName = VanillaFolder + "\immigration.txt"
        gridData5.lineBreak = """"
        gridData5.lineBreak2 = "ids = "
        gridData5.Load()
        dgvImmigration.DataSource = gridData5.DisplayData

        Dim gridData6 As New clsCreaturePopulator
        gridData6.FileName = VanillaFolder + "\workshops_menu.txt"
        gridData6.lineBreak = "{item ="
        gridData6.lineBreak2 = "{{"
        gridData6.Load()
        dgvWorkshopsMenu.DataSource = gridData6.DisplayData

        Dim gridData7 As New clsCreaturePopulator
        gridData7.FileName = VanillaFolder + "\build_menu.txt"
        gridData7.lineBreak = """"
        gridData7.lineBreak2 = "{"
        gridData7.Load()
        dgvBuildMenu.DataSource = gridData7.DisplayData

        Dim gridData8 As New clsCreaturePopulator
        gridData8.FileName = VanillaFolder + "\campaign_villains.txt"
        gridData8.lineBreak = ""
        gridData8.Load()
        DgvCampaignVillains.DataSource = gridData8.DisplayData

        Dim gridData9 As New clsCreaturePopulator
        gridData9.FileName = VanillaFolder + "\zlevels.txt"
        gridData9.lineBreak = "type ="
        gridData9.Load()
        dgvZLevels.DataSource = gridData9.DisplayData

        Dim gridData10 As New clsCreaturePopulator
        gridData10.FileName = VanillaFolder + "\resources.txt"
        gridData10.lineBreak = "counts ="
        gridData10.Load()
        dgvEnemies.DataSource = gridData10.DisplayData

        Dim gridData11 As New clsCreaturePopulator
        gridData11.FileName = VanillaFolder + "\enemies.txt"
        gridData11.lineBreak = """"
        gridData11.Load()
        dgvEnemies.DataSource = gridData11.DisplayData
    End Sub

    Private Sub Sizeme() Handles Me.Resize
        If SplitContainer1.Height < 200 Then
            Exit Sub
        End If
        SplitContainer1.SplitterDistance = SplitContainer1.Height - 60
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim row As DataGridViewRow = dgvCreatures.CurrentRow
        If row Is Nothing Then Exit Sub
        Dim txt As String = My.Resources.WikiCreatureTemplate
        Dim final As String = txt
        Dim monster As String = ""
        For n = 1 To txt.Split("¬").Count - 1 Step 2
            Dim section As String = txt.Split("¬")(n)
            Dim value As String = ""
            For Each column As DataGridViewColumn In dgvCreatures.Columns
                If column.Name.ToUpper = section.ToUpper Then
                    value = row.Cells(column.Name).Value.ToString
                    Exit For
                End If
            Next
            If value = "" Then
                final = RemoveLine(final, "¬" + section + "¬")
            Else
                If section.ToUpper = "VIEWID" Then
                    monster = value.Replace("{", "")
                    monster = Split(monster, "Rgb")(0)
                    monster = Replace(monster, "}", "")
                    monster = Replace(monster, """", "").Trim()
                    final = Replace(final, "¬" + section + "¬", monster)
                Else
                    final = Replace(final, "¬" + section + "¬", value.Trim)
                End If
            End If
        Next
        final = Replace(Replace(final, "{", ""), "}", "")
        final = Replace(final, vbCrLf + "*", "*")
        final = Replace(final, "*", vbCrLf + "*")
        TextViewer.RichTextBox1.Text = final
        TextViewer.ShowDialog()
        If MsgBox("Copy to clipboard and launch editor on wiki?", vbYesNo) = vbNo Then
            Exit Sub
        End If
        Dim link As String = "http://keeperrl.com/wiki/index.php?title=¬Monster¬&action=edit"
        For Each word As String In monster.Split("_")
            If word.Length > 0 Then
                Dim capitalWord As String = word.Substring(0, 1).ToUpper + word.Substring(1, word.Length - 1)
                monster = monster.Replace(word, capitalWord)
            End If
        Next
        Clipboard.SetText(TextViewer.RichTextBox1.Text)
        monster = InputBox("Enter wiki entry for this creature", "Edit wiki entry", monster)
        Shell("Explorer """ + Replace(link, "¬Monster¬", monster) + """")
    End Sub

    Private Function RemoveLine(text As String, line As String)
        Dim removal As String = ""
        For Each lin As String In text.Split(vbCrLf)
            If lin.ToUpper.Contains(line.ToUpper) Then
                removal = lin
                Exit For
            End If
        Next
        Return Replace(text, removal, "",, CompareMethod.Text)
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim row As DataGridViewRow = dgvEnemies.CurrentRow
        If row Is Nothing Then Exit Sub
        Dim txt As String = My.Resources.WikiEnemiesTemplate
        Dim final As String = txt
        Dim tribe As String = ""
        For n = 1 To txt.Split("¬").Count - 1 Step 2
            Dim section As String = txt.Split("¬")(n)
            Dim value As String = ""
            For Each column As DataGridViewColumn In dgvEnemies.Columns
                If column.Name.ToUpper = section.ToUpper Then
                    value = row.Cells(column.Name).Value.ToString
                    Exit For
                End If
            Next
            If value = "" Then
                final = RemoveLine(final, "¬" + section + "¬")
            Else
                final = Replace(final, "¬" + section + "¬", value.Trim)
            End If
        Next
        final = Replace(Replace(final, "{", ""), "}", "")
        final = Replace(final, vbCrLf + "*", "*")
        final = Replace(final, "*", vbCrLf + "*")
        final = Replace(final, """", "")
        Dim reps As String = My.Resources.WikiEnemiesReplacements
        For n = 0 To reps.Split(vbCrLf).Count - 1
            Dim FindReplace As String = reps.Split(vbLf)(n)
            Dim find As String = Split(FindReplace, "|")(0)
            Dim replce As String = Split(FindReplace, "|")(1)
            final = final.Replace(find, replce)
        Next n
        final = final.Replace("*", vbCrLf + "*")
        final = final.Replace(vbCrLf + " :", ":")
        TextViewer.RichTextBox1.Text = final
        TextViewer.ShowDialog()
        If MsgBox("Copy to clipboard and launch editor on wiki?", vbYesNo) = vbNo Then
            Exit Sub
        End If
        Dim link As String = "http://keeperrl.com/wiki/index.php?title=¬tribe¬&action=edit"
        For Each word As String In tribe.Split("_")
            If word.Length > 0 Then
                Dim capitalWord As String = word.Substring(0, 1).ToUpper + word.Substring(1, word.Length - 1)
                tribe = tribe.Replace(word, capitalWord)
            End If
        Next
        Clipboard.SetText(TextViewer.RichTextBox1.Text)
        tribe = InputBox("Enter wiki entry for this tribe", "Edit wiki entry", tribe)
        Shell("Explorer """ + Replace(link, "¬tribe¬", tribe) + """")
    End Sub
End Class
