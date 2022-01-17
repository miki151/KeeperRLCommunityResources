Imports System.IO

Public Class frmSprites

    Dim sDirectory As String = "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\data\images\orig24"
    Dim sFolder2 As String = "orig48x24"
    Dim dirs As String()
    Dim count As Integer
    Dim colCount As Integer = 0
    Dim rbgs As New List(Of Color)
    Dim bmp As Bitmap
    Dim fil As String
    Dim original As Bitmap
    Dim col As Color
    Dim substitutes As New Dictionary(Of Color, Color)
    Dim m_bWhite As Boolean
    Dim m_bDoing As Boolean = False

    Private Sub me_load() Handles Me.Load
        Try
            TextBox1.Text = "*.png"
            TextBox2.Text = sDirectory
            TextBox4.Text = sFolder2
            Timer1.Interval = 500
            Timer1.Start()
            count = -1
            btnLoadNext_Click()
            substitutes = New Dictionary(Of Color, Color)
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try

    End Sub

    Private Sub textbox2_change() Handles TextBox2.TextChanged, TextBox1.textchanged
        Try
            If Not Directory.Exists(TextBox2.Text) Then Exit Sub
            dirs = Directory.GetFiles(TextBox2.Text, TextBox1.Text)
            count = 0
            If dirs.Count = 0 Then Exit Sub
            fil = dirs(count)
            bmp = SafeGetImageFromFile(fil)
            original = bmp.Clone
            colCount = 1
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Sub go()
        If bmp Is Nothing Then Exit Sub
        rbgs = New List(Of Color)
        PictureBox1.BackgroundImage = bmp
        Label1.Text = Path.GetFileName(fil)
        For x = 0 To bmp.Width - 1
            For y = 0 To bmp.Height - 1
                If Not rbgs.Contains(bmp.GetPixel(x, y)) Then rbgs.Add(bmp.GetPixel(x, y))
            Next
        Next
        SelectColour()
    End Sub

    Private Sub Nxt() Handles btnNextColour.Click
        Try
            SelectColour()
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Sub SelectColour()
        If original Is Nothing Then Exit Sub
        bmp = original.Clone
        If colCount >= rbgs.Count Then
            colCount = 0
        End If
        col = rbgs(colCount)
        colCount = colCount + 1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If bmp Is Nothing Then Exit Sub
            If Not substitutes.ContainsKey(col) Then
                substitutes.Add(col, GetSub(col))
            End If
            Dim png2 As New Bitmap(bmp.Width * 2, bmp.Height)
            Dim gr_dest As Graphics = Graphics.FromImage(png2)
            gr_dest.DrawImage(original, 0, 0, original.Width, original.Height)

            Dim bmptemp As Bitmap = original.Clone
            For Each col2 As Color In rbgs
                If Not substitutes.ContainsKey(col2) Then
                    bmptemp.MakeTransparent(col2)
                Else
                    For x = 0 To bmp.Width - 1
                        For y = 0 To bmp.Height - 1
                            If bmptemp.GetPixel(x, y) = col2 Then
                                bmptemp.SetPixel(x, y, substitutes(col2))
                            End If
                        Next
                    Next
                End If
            Next
            gr_dest.DrawImage(bmptemp, bmp.Width, 0, bmp.Width, bmp.Height)
            PictureBox2.BackgroundImage = png2
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try

    End Sub

    Private Function GetSub(tmpcol As Color) As Color
        Dim av As Long = (CLng(tmpcol.R) + CLng(tmpcol.B) + CLng(tmpcol.B))
        If av > 255 Then av = av / 2
        If av > 255 Then av = av / 1.5
        Return Color.FromArgb(av, av, av)
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If m_bDoing Then Exit Sub
            m_bDoing = True
            If fil Is Nothing Then Exit Sub
            Dim fold As String = Replace(TextBox2.Text, Split(TextBox2.Text, "\").Last, TextBox4.Text) + "\"
            Dim tmpBMP As Bitmap = PictureBox2.BackgroundImage.Clone
            tmpBMP.Save("C:\temp\" + Label1.Text, Imaging.ImageFormat.Png)
            m_bDoing = False
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            If bmp Is Nothing Then Exit Sub
            If m_bDoing Then Exit Sub
            m_bDoing = True
            Dim bmpTemp As Bitmap = bmp.Clone
            For x = 0 To bmp.Width - 1
                For y = 0 To bmp.Height - 1
                    If m_bWhite Then
                        If original.GetPixel(x, y) = col Then bmpTemp.SetPixel(x, y, Color.White)
                    Else
                        If original.GetPixel(x, y) = col Then bmpTemp.SetPixel(x, y, Color.Black)
                    End If
                Next
            Next
            PictureBox1.BackgroundImage = bmpTemp
            PictureBox1.Refresh()
            m_bWhite = Not m_bWhite
            m_bDoing = False
        Catch ex As Exception
            m_bDoing = False
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ClearImage()
            substitutes = New Dictionary(Of Color, Color)
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Sub Button2_Click() Handles btnLast.Click
        Try
            substitutes = New Dictionary(Of Color, Color)
            If dirs.Count = 0 Then Exit Sub
            count = count - 1
            If count < 0 Then count = 0
            fil = dirs(count)
            bmp = SafeGetImageFromFile(fil)
            original = bmp.Clone
            colCount = 1

            Dim fold As String = Replace(TextBox2.Text, Split(TextBox2.Text, "\").Last, TextBox4.Text) + "\"
            If File.Exists(fold + Path.GetFileName(fil)) Then
                PictureBox2.BackgroundImage = SafeGetImageFromFile(fold + Path.GetFileName(fil))
            Else
                ClearImage()
            End If
            go()
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Function SafeGetImageFromFile(filename As String) As Bitmap
        Dim TMPBMP As Bitmap = Image.FromFile(filename)
        Dim tmpClone As Bitmap = TMPBMP.Clone
        TMPBMP.Dispose()
        Return tmpClone
    End Function

    Private Sub btnLoadNext_Click() Handles btnLoadNext.Click
        Try
            substitutes = New Dictionary(Of Color, Color)
            If dirs.Count = 0 Then Exit Sub
            count = count + 1
            If count > dirs.Count Then count = dirs.Count
            fil = dirs(count)
            bmp = SafeGetImageFromFile(fil)
            original = bmp.Clone
            colCount = 1

            Dim fold As String = Replace(TextBox2.Text, Split(TextBox2.Text, "\").Last, TextBox4.Text) + "\"
            If File.Exists(fold + Path.GetFileName(fil)) Then
                PictureBox2.BackgroundImage = SafeGetImageFromFile(fold + Path.GetFileName(fil))
            Else
                ClearImage()
            End If
            go()
        Catch ex As Exception
            MsgBox(ex.Message)
            m_bDoing = False
        End Try
    End Sub

    Private Sub ClearImage()
        On Error Resume Next
        Dim tmpbmp As Bitmap = PictureBox2.BackgroundImage
        PictureBox2.BackgroundImage = Nothing
        tmpbmp.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, btnLast.Click
        Try
            If MsgBox("Replace White with transparent?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim clone As Bitmap = bmp.Clone
                clone.MakeTransparent(Color.White)
                PictureBox1.BackgroundImage = clone
                Dim fil As String = getFile()
                File.Delete(fil)
                clone.Save(fil, Imaging.ImageFormat.Png)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function getFile() As String
        Dim dlg As New SaveFileDialog
        dlg.FileName = Label1.Text
        dlg.ShowDialog()
        Return dlg.FileName
    End Function

End Class
