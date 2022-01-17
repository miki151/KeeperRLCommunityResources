Imports System.ComponentModel

Public Class Creatures

    Inherits DataTable

    Public Property Cells(row As Integer, column As Integer)
        Get
            Return Rows(row).Item(column).ToString
        End Get
        Set(value)
            Rows(row).Item(column) = value
        End Set
    End Property

End Class
