Public Class Form1

    Private Declare Function ShowWindow Lib "user32" (ByVal handle As IntPtr, ByVal nCmdShow As Integer) As Integer

    Dim doing As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If doing Then
            Button1.Enabled = False
            Exit Sub
        End If

        doing = True

        Dim text As String = System.IO.File.ReadAllText("C:\PRJ\KeeperRLCommunityResources\Apps\IssueRaiser\Ideas_Upvoted_13.04.2023.txt")
        text = Split(text, "MessageID: 1003294016974106696")(1)
        For Each issue As String In Split(text, "Click to jump to message!")
            If issue.Contains(vbCrLf + vbCrLf) Then
                Dim rest As String = Replace(Replace(Split(issue, vbCrLf + vbCrLf)(1), vbCrLf, " "), "#", " ").Trim
                Dim issuetext As String = Replace(Split(issue, vbCrLf + vbCrLf)(0), vbCrLf, " ").Trim
                If issuetext <> "" Then
                    raiseissue(rest, issuetext)
                End If
            End If
        Next

    End Sub

    Private Sub raiseissue(title As String, Description As String)

        Try

            Dim URL As String = "https://github.com/SoftMonster/keeperrl/issues/"
            Dim tickerURL = URL + "new?title=" + title + "&body=" + Description
            Dim edge As Process = Process.Start("microsoft-edge:" + tickerURL)
            ShowWindow(edge.MainWindowHandle, 3) 'maximize = 3
            Threading.Thread.Sleep(20000)
            SendKeys.SendWait("{TAB}{TAB}{TAB}{TAB}{TAB}{TAB}{TAB}")
            Threading.Thread.Sleep(20000)
            SendKeys.SendWait("{ENTER}")

            Threading.Thread.Sleep(20000)
            Do While Process.GetProcessesByName("msedge").Count > 0
                For Each proc In Process.GetProcessesByName("msedge")
                    proc.Kill()
                Next
            Loop

        Catch ex As Exception
        End Try


    End Sub

End Class
