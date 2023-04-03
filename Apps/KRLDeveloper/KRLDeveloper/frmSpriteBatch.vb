Public Class frmSpriteBatch

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            WriteFile(str, Imaging.PixelFormat.Format1bppIndexed, count)
            WriteFile(str, Imaging.PixelFormat.Format4bppIndexed, count)
            WriteFile(str, Imaging.PixelFormat.Format8bppIndexed, count)
            WriteFile(str, Imaging.PixelFormat.Format16bppRgb555, count)
        Next
    End Sub

    Private Sub WriteFile(str As String, format As Imaging.PixelFormat, ByRef count As Integer)

        Dim bmp As New Bitmap(str)
        bmp = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), format)
        bmp = New Bitmap(bmp, New Size(24, 24))
        For f = 0 To 23
            bmp.MakeTransparent(bmp.GetPixel(0, f))
            bmp.MakeTransparent(bmp.GetPixel(23, f))
            bmp.MakeTransparent(bmp.GetPixel(f, 23))
            bmp.MakeTransparent(bmp.GetPixel(f, 0))
        Next f
        Dim outpath As String = TextBox1.Text
        Dim filename As String = outpath + "\" + count.ToString() + ".png"
        bmp.Save(filename, Imaging.ImageFormat.Png)
        count = count + 1
    End Sub

End Class

