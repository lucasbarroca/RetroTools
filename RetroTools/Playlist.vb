Imports System.IO
Imports System.Text

Public Class PlaylistItem
    Public ROMPath As String
    Public DisplayName As String
    Public CorePath As String
    Public CoreDisplayName As String
    Public DB_CRC As String
    Public FExists(3) As Boolean

    Sub New(Optional ByVal ROMPath As String = "", Optional ByVal DisplayName As String = "", Optional ByVal CorePath As String = "", Optional ByVal CoreDisplayName As String = "", Optional ByVal DB_CRC As String = "")
        Me.ROMPath = ROMPath
        Me.DisplayName = DisplayName
        Me.CorePath = CorePath
        Me.CoreDisplayName = CoreDisplayName
        Me.DB_CRC = DB_CRC

        For Each F In FExists
            F = False
        Next
    End Sub
End Class

Public Class Playlist
    Public PlaylistName As String = ""
    Public CorePath As String = ""
    Public CoreDisplayName As String = ""
    Public BaseROMsPath As String = ""

    Public ROMs As List(Of PlaylistItem)

    Sub New()
        ROMs = New List(Of PlaylistItem)
        ROMs.Clear()
    End Sub

    Public Sub Load(ByVal Filename As String)

        Using preader As New StreamReader(Filename, Encoding.Default)
            ROMs.Clear()

            Dim line As String

            Dim PCount As Integer = 0

            Dim Iadd As New PlaylistItem
            Do
                line = preader.ReadLine()

                Select Case PCount
                    Case 0  'ROM Path
                        If String.IsNullOrEmpty(GetFileName(line)) Then Exit Do
                        Iadd.ROMPath = line

                        If ROMs.Count < 1 Then BaseROMsPath = line.Replace(GetFileName(line), "")

                    Case 1 'Display Name
                        Iadd.DisplayName = line

                    Case 2 'Core Path
                        Iadd.CorePath = line
                        If ROMs.Count < 1 Then CorePath = line

                    Case 3 'Core Display Name
                        Iadd.CoreDisplayName = line
                        If ROMs.Count < 1 Then CoreDisplayName = line

                    Case 4 'DB CRC
                        Iadd.DB_CRC = line

                    Case 5 'Playlist Name
                        'PlaylistName = line
                        ROMs.Add(New PlaylistItem(Iadd.ROMPath, Iadd.DisplayName, Iadd.CorePath, Iadd.CoreDisplayName, Iadd.DB_CRC))
                        PCount = -1
                End Select

                PCount += 1
            Loop Until line Is Nothing
        End Using

        PlaylistName = GetFileName(Filename)
    End Sub

    Public Sub RemoveItem(ByVal Index As Integer)
        ROMs.RemoveAt(Index)
    End Sub
End Class