Public Class UcBoolean

    Public Property DefaultValue As Boolean

    Public Property CheckText As String
        Set(value As String)
            CheckBox1.Text = value
        End Set
        Get
            Return CheckBox1.Text
        End Get
    End Property

    Public Property checked As Boolean
        Get
            Return CheckBox1.Checked
        End Get
        Set(value As Boolean)
            CheckBox1.Checked = value
        End Set
    End Property

    Public Overrides Property Text As String
        Set(value As String)
            If value = "" Then
                CheckBox1.Checked = DefaultValue
            ElseIf UCase(value) = "TRUE" Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
        End Set
        Get
            If CheckBox1.Checked = DefaultValue Then
                Return ""
            End If
            If CheckBox1.Checked Then
                Return "true"
            Else
                Return "false"
            End If
        End Get
    End Property


End Class
