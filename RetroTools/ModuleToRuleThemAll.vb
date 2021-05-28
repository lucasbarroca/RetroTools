Module ModuleToRuleThemAll
    Sub Mains()
        Dim string1 As String = "four score and seven years ago"
        Dim string2 As String = "for scor and sevn yeres ago"
        Dim similarity As Single =
        GetSimilarity(string1, string2)
        ' RESULT : 0.8
    End Sub

    Public Function GetSimilarity(string1 As String, string2 As String) As Single
        Dim dis As Single = ComputeDistance(string1, string2)
        Dim maxLen As Single = string1.Length
        If maxLen < string2.Length Then
            maxLen = string2.Length
        End If
        If maxLen = 0.0F Then
            Return 1.0F
        Else
            Return 1.0F - dis / maxLen
        End If
    End Function

    Private Function ComputeDistance(s As String, t As String) As Integer
        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim distance As Integer(,) = New Integer(n, m) {}
        ' matrix
        Dim cost As Integer = 0
        If n = 0 Then
            Return m
        End If
        If m = 0 Then
            Return n
        End If
        'init1

        Dim i As Integer = 0
        While i <= n
            distance(i, 0) = System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Dim j As Integer = 0
        While j <= m
            distance(0, j) = System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
        End While
        'find min distance

        For i = 1 To n
            For j = 1 To m
                cost = (If(t.Substring(j - 1, 1) = s.Substring(i - 1, 1), 0, 1))
                distance(i, j) = Math.Min(distance(i - 1, j) + 1, Math.Min(distance(i, j - 1) + 1, distance(i - 1, j - 1) + cost))
            Next
        Next
        Return distance(n, m)
    End Function

    Public Function GetFileName(ByVal FilePath As String) As String
        Dim Str() As String = Split(FilePath, "\")

        If Str(UBound(Str)) = FilePath Then
            Dim Str2() As String = Split(FilePath, "/")
            Return Str2(UBound(Str2))
        Else
            Return Str(UBound(Str))
        End If
    End Function

    Public Function PrepareFileName(ByVal Filename As String) As String
        If Filename.Contains("#") Then
            Return Filename.Substring(0, Filename.IndexOf("#"))
        End If

        Return Filename
    End Function
End Module
