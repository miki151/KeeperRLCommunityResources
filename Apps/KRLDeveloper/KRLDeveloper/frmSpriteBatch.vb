Public Class frmSpriteBatch

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            Dim bmp As New Bitmap(str)
            WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 30)
            WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 30)
            WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 30)
            WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 30)
            WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 24)
            WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 24)
            WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 24)
            WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 24)
        Next
    End Sub

    Private Sub WriteFile(ByVal bmp As Bitmap, format As Imaging.PixelFormat, ByRef count As Integer, size As Integer)
        bmp = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), format)
        bmp = New Bitmap(bmp, New Size(size, size))
        For f = 0 To 23
            bmp.MakeTransparent(bmp.GetPixel(0, f))
            bmp.MakeTransparent(bmp.GetPixel(size - 1, f))
            bmp.MakeTransparent(bmp.GetPixel(f, size - 1))
            bmp.MakeTransparent(bmp.GetPixel(f, 0))
        Next f
        Dim outpath As String = TextBox1.Text
        Dim filename As String = outpath + "\orig" + size.ToString() + "_" + count.ToString() + ".png"
        bmp.Save(filename, Imaging.ImageFormat.Png)
        count = count + 1
    End Sub

End Class

