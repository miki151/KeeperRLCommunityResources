Imports System.Reflection
Imports System.ComponentModel

Public Module DisplayNameHelper
    Public Function GetDisplayName(ByVal obj As Object, ByVal propertyName As String) As String
        If obj Is Nothing Then Return Nothing
        Return GetDisplayName(obj.[GetType](), propertyName)
    End Function

    Public Function GetDisplayName(ByVal type As Type, ByVal propertyName As String) As String
        Dim [property] = type.GetProperty(propertyName)
        If [property] Is Nothing Then Return Nothing
        Return GetDisplayName([property])
    End Function

    Public Function GetDisplayName(ByVal [property] As PropertyInfo) As String
        Dim attrName = GetAttributeDisplayName([property])
        Return attrName
    End Function

    Private Function GetAttributeDisplayName(ByVal [property] As PropertyInfo) As String
        Dim atts = [property].GetCustomAttributes(GetType(DisplayNameAttribute), True)
        If atts.Length = 0 Then Return Nothing
        Return (TryCast(atts(0), DisplayNameAttribute)).DisplayName
    End Function

End Module
