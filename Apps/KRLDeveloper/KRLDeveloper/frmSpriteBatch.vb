Public Class frmSpriteBatch

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        Dim outpath As String = TextBox1.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            Dim bmp As New Bitmap(str)
            bmp = New Bitmap(bmp, New Size(24, 24))
            Dim rbgs As New List(Of Color)
            For x = 0 To bmp.Width - 1
                For y = 0 To bmp.Height - 1
                    If Not rbgs.Contains(bmp.GetPixel(x, y)) Then rbgs.Add(bmp.GetPixel(x, y))
                    Application.DoEvents()
                Next
            Next
            For f = 15 To 0 Step -1
                Dim clone = bmp.Clone
                For Each col2 As Color In rbgs
                    Application.DoEvents()
                    Dim av As Long = (CLng(col2.R) + CLng(col2.B) + CLng(col2.B))
                    If av >= 256 - 4 * f Or av <= 4 * f Then
                        clone.MakeTransparent(col2)
                    ElseIf Rnd() > 0.75 And False Then
                        Dim substitute As Color = Color.FromArgb(av, av, av, av)
                        For x = 0 To bmp.Width - 1
                            For y = 0 To bmp.Height - 1
                                If clone.GetPixel(x, y).ToArgb = col2.ToArgb Then
                                    clone.SetPixel(x, y, substitute)
                                End If
                            Next
                        Next
                    End If
                Next
                clone.Save(outpath + "\" + count.ToString() + "_" + System.Guid.NewGuid.ToString() + ".png", Imaging.ImageFormat.Png)
                count = count + 1
            Next f
        Next
    End Sub

End Class

