Public Class frmSpriteResizer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim count As Integer = 1
        Dim pth As String = TextBox2.Text
        For Each str As String In System.IO.Directory.GetFiles(pth)
            Dim bmp As New Bitmap(str)
            If CheckBox4.Checked Then
                WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 30)
                WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 30)
                WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 30)
                WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 30)
            End If
            If CheckBox3.Checked Then
                WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 24)
                WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 24)
                WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 24)
                WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 24)
            End If
            If CheckBox2.Checked Then
                WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 16)
                WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 16)
                WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 16)
                WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 16)
            End If
            If CheckBox1.Checked Then
                'WriteFile(bmp, Imaging.PixelFormat.Format1bppIndexed, count, 8)
                WriteFile(bmp, Imaging.PixelFormat.Format4bppIndexed, count, 8)
                WriteFile(bmp, Imaging.PixelFormat.Format8bppIndexed, count, 8)
                WriteFile(bmp, Imaging.PixelFormat.Format16bppRgb555, count, 8)
            End If
        Next
    End Sub

    Private Sub WriteFile(ByVal bmp As Bitmap, format As Imaging.PixelFormat, ByRef count As Integer, size As Integer)
        bmp = bmp.Clone(New Rectangle(0, 0, bmp.Width, bmp.Height), format)
        bmp = New Bitmap(bmp, New Size(size, size))
        For f = 0 To size - 1
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dlg As New FolderBrowserDialog
        dlg.SelectedPath = TextBox2.Text
        dlg.ShowDialog()
        TextBox2.Text = dlg.SelectedPath
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dlg As New FolderBrowserDialog
        dlg.SelectedPath = TextBox1.Text
        dlg.ShowDialog()
        TextBox1.Text = dlg.SelectedPath
    End Sub
End Class

