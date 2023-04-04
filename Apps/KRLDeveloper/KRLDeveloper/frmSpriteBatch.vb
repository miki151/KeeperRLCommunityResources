Public Class frmSpriteBatch

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            Dim bmp As New Bitmap(str)
            WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count)
            WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count)
            WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count)
            WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count)
        Next
    End Sub

    Private Sub WriteFile(ByVal bmp As Bitmap, format As Imaging.PixelFormat, ByRef count As Integer, Optional sheet As Boolean = False)
        '
        bmp = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), format)
        bmp = New Bitmap(bmp, New Size(24, 24))
        For f = 0 To 23
            bmp.MakeTransparent(bmp.GetPixel(0, f))
            bmp.MakeTransparent(bmp.GetPixel(23, f))
            bmp.MakeTransparent(bmp.GetPixel(f, 23))
            bmp.MakeTransparent(bmp.GetPixel(f, 0))
        Next f
        Dim outpath As String = TextBox1.Text
        Dim filename As String = outpath + "\Image_" + count.ToString() + ".png"
        If sheet Then filename = Replace(filename, "Image_", "Sheet_")
        bmp.Save(filename, Imaging.ImageFormat.Png)
        count = count + 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            Dim bmp As New Bitmap(str)
            CutSheet(bmp, Imaging.PixelFormat.Format1bppIndexed, count)
            CutSheet(bmp, Imaging.PixelFormat.Format4bppIndexed, count)
            CutSheet(bmp, Imaging.PixelFormat.Format8bppIndexed, count)
            CutSheet(bmp, Imaging.PixelFormat.Format16bppRgb555, count)
        Next
    End Sub

    Private Sub CutSheet(ByVal bmp As Bitmap, format As Imaging.PixelFormat, ByRef count As Integer)
        bmp = bmp.Clone(New Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1), format)
        For f = 0 To 23
            bmp.MakeTransparent(bmp.GetPixel(0, f))
            bmp.MakeTransparent(bmp.GetPixel(bmp.Width - 1, f))
            bmp.MakeTransparent(bmp.GetPixel(f, bmp.Height - 1))
            bmp.MakeTransparent(bmp.GetPixel(f, 0))
        Next f
        Dim transparent As Color = bmp.GetPixel(0, 0)
        Dim x As Integer = 0
        Do Until x = bmp.Width - 1
            x = 0
            Dim y As Integer = 0
            Dim lft As Integer = 0
            Dim tp As Integer = 0
            Dim lft2 As Integer = 0
            Dim tp2 As Integer = 0
            Do Until x = bmp.Width - 1 Or bmp.GetPixel(x, y) <> transparent
                Do Until y = bmp.Height - 1 Or bmp.GetPixel(x, y) <> transparent
                    y = y + 1
                Loop
                If y > tp Then
                    tp = y
                End If
                If x > lft Then
                    lft = x
                End If
                x = x + 1
            Loop
            If x = bmp.Width - 1 And y = bmp.Height - 1 Then Exit Sub
            x = lft
            tp2 = tp
            lft2 = lft
            Dim emptyline As Boolean = True
            Dim highscore As Boolean = False
            Do Until x = bmp.Width - 1 Or (emptyline And highscore)
                highscore = False
                emptyline = True
                y = tp
                Do Until y = bmp.Height - 1 Or bmp.GetPixel(x, y) = transparent
                    y = y + 1
                Loop
                If y > tp2 Then
                    tp2 = y
                End If
                If x > lft2 Then
                    lft2 = x
                    highscore = True
                Else
                    highscore = False
                End If
                x = x + 1
                For y1 As Integer = tp To tp2
                    If bmp.GetPixel(x, y1) <> transparent Then
                        emptyline = False
                        Exit For
                    End If
                Next
            Loop
            If lft2 = lft Or tp = tp2 Then
                Exit Sub
            End If
            Dim sprite = bmp.Clone(New Rectangle(lft, tp, lft2 - lft, tp2 - tp), format)
            For x1 As Integer = lft To lft2
                For y1 As Integer = tp To tp2
                    bmp.SetPixel(x1, y1, transparent)
                Next
            Next
            Dim outpath As String = TextBox1.Text
            Dim filename As String = outpath + "\Image_" + count.ToString() + ".png"
            filename = Replace(filename, "Image_", "Sheet_")
            sprite.Save(filename, Imaging.ImageFormat.Png)
            count = count + 1
            WriteFile(sprite, Imaging.PixelFormat.Format1bppIndexed, count, True)
            WriteFile(sprite, Imaging.PixelFormat.Format4bppIndexed, count, True)
            WriteFile(sprite, Imaging.PixelFormat.Format8bppIndexed, count, True)
            WriteFile(sprite, Imaging.PixelFormat.Format16bppRgb555, count, True)
        Loop
    End Sub

End Class

