Public Class Form1

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ExampleBot.StartAsync().GetAwaiter().GetResult()
    End Sub
End Class
