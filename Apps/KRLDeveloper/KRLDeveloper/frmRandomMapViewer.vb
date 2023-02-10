Imports System.IO

Public Class frmRandomMapViewer

    Private Sub loadME() Handles Me.Load
        Dim txt As String = File.ReadAllText("C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\data_free\game_config\random_layouts.txt")
        For Each line As String In Split(txt, vbLf)
            If line.Trim.StartsWith("""") Then
                ListBox1.Items.Add(Split(line, """")(1))
            End If
        Next
    End Sub

    Private Sub lstClick() Handles ListBox1.Click
        Dim txt As String = ListBox1.SelectedItem.ToString()
        Dim batch As String = "cd ""C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\""" + vbCrLf
        batch = batch + "keeper.exe --layout_name ""castle"" --layout_size 20:15" + vbCrLf
        batch = batch + "pause"
        batch = Replace(batch, "castle", txt)
        Dim fil As String = "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\Test.bat"
        File.WriteAllText(fil, batch)
        Shell(fil, AppWinStyle.MaximizedFocus)
    End Sub

End Class