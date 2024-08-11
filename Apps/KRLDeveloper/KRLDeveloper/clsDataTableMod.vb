Imports System.ComponentModel

Public Class Creatures

    Inherits DataTable

    Public Property Cells(row As Integer, column As Integer)
        Get
            If row < 1 Then
                Return ""
            End If
            Return Rows(row).Item(column).ToString
        End Get
        Set(value)
            If row < 1 Then
                Return
            End If
            Rows(row).Item(column) = value
        End Set
    End Property

End Class
