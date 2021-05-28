Imports System.IO

Public Class FrmPlaylist
    Dim MainPl As New Playlist
    Dim ActualFilter As Integer = -1
    Dim Not_Found_Count(3) As Integer

    Public Sub RefreshItems()
        TxtCFile.Text = MainPl.CorePath
        TxtCName.Text = MainPl.CoreDisplayName
        TxtRFolder.Text = "T:\" & MainPl.PlaylistName.Replace(".lpl", "")
        TxtRPath.Text = MainPl.BaseROMsPath
        lblCheck.Text = "| CHECK: Miss{ROMs: " & Not_Found_Count(0) & ", Boxarts: " & Not_Found_Count(1) & ", Snaps: " & Not_Found_Count(2) & ", Titles: " & Not_Found_Count(3) & "}"
        DGAllP.Rows.Clear()
        'DGUSAP.Rows.Clear()
        'DGEUR.Rows.Clear()
        'DGJAPAN.Rows.Clear()
        'DGNOT.Rows.Clear()

        For Each R As PlaylistItem In MainPl.ROMs
            DGAllP.Rows.Add(R.FExists(0), R.FExists(1), R.FExists(2), R.FExists(3), R.DisplayName, GetFileName(PrepareFileName(R.ROMPath)))
            'FileExists.Visible = False
            'F_Boxarts.Visible = False
            'F_Snaps.Visible = False
            'F_Titles.Visible = False
            'If R.DisplayName.ToUpper.Contains("USA") Or R.DisplayName.ToUpper.Contains("(U)") Then
            'DGUSAP.Rows.Add(File.Exists(TxtRFolder.Text & Path.DirectorySeparatorChar & PrepareFileName(R.ROMPath)), R.DisplayName)
            'ElseIf R.DisplayName.ToUpper.Contains("EUROPE") Or R.DisplayName.ToUpper.Contains("(E)") Then
            'DGEUR.Rows.Add(File.Exists(TxtRFolder.Text & Path.DirectorySeparatorChar & PrepareFileName(R.ROMPath)), R.DisplayName)
            'ElseIf R.DisplayName.ToUpper.Contains("JAPAN") Or R.DisplayName.ToUpper.Contains("(J)") Then
            'DGJAPAN.Rows.Add(File.Exists(TxtRFolder.Text & Path.DirectorySeparatorChar & PrepareFileName(R.ROMPath)), R.DisplayName)
            'Else
            'DGNOT.Rows.Add(File.Exists(TxtRFolder.Text & Path.DirectorySeparatorChar & PrepareFileName(R.ROMPath)), R.DisplayName)
            'End If
        Next
    End Sub

    Public Sub LoadP()
        If File.Exists(TxtPName.Text) Then
            MainPl = New Playlist

            Try
                MainPl.Load(TxtPName.Text)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

            RefreshItems()
        End If

        lblTotalP.Text = "Total: " & DGAllP.Rows.Count

        TotalUSA.Text = "| USA: " & DGUSAP.Rows.Count
        TotalEUR.Text = "| EUROPE: " & DGEUR.Rows.Count
        TotalJP.Text = "| JAPAN: " & DGJAPAN.Rows.Count
        TotalNot.Text = "| NOT SET: " & DGNOT.Rows.Count

    End Sub

    Private Sub FrmPlaylist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtRFolder.Text = Environment.CurrentDirectory
        TxtPName.Text = Environment.CurrentDirectory & Path.DirectorySeparatorChar & "roms.lpl"
    End Sub

    Private Sub BtFile_Click(sender As Object, e As EventArgs) Handles BtFile.Click
        OpenFileDialog1.InitialDirectory = Replace(TxtPName.Text, GetFileName(TxtPName.Text), "")
        OpenFileDialog1.FileName = GetFileName(TxtPName.Text)
        'OpenFileDialog1.ShowDialog()

        If Not OpenFileDialog1.ShowDialog() = DialogResult.OK Then Exit Sub
        TxtPName.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub BTLoadP_Click(sender As Object, e As EventArgs) Handles BTLoadP.Click
        LoadP()
    End Sub

    Private Sub BTRFolder_Click(sender As Object, e As EventArgs) Handles BTRFolder.Click
        FolderBrowserDialog1.SelectedPath = TxtRFolder.Text

        If Not FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then Exit Sub
        TxtRFolder.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Label1_DragEnter(sender As Object, e As DragEventArgs) Handles Label1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Label1_DragDrop(sender As Object, e As DragEventArgs) Handles Label1.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        TxtPName.Text = files(0)
        LoadP()
    End Sub

    Private Sub DGAllP_DragEnter(sender As Object, e As DragEventArgs) Handles DGAllP.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub DGAllP_DragDrop(sender As Object, e As DragEventArgs) Handles DGAllP.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)

        TxtPName.Text = files(0)
        LoadP()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DGFilter.Rows.Clear()

        For S1 = ActualFilter + 1 To MainPl.ROMs.Count - 1
            Dim Found As Boolean = False
            For S2 = 0 To MainPl.ROMs.Count - 1
                If Not S1 = S2 And GetSimilarity(MainPl.ROMs(S1).DisplayName, MainPl.ROMs(S2).DisplayName) * 100 >= FilterP.Value Then
                    DGFilter.Rows.Add(GetSimilarity(MainPl.ROMs(S1).DisplayName, MainPl.ROMs(S2).DisplayName) * 100 & "%", MainPl.ROMs(S2).DisplayName)
                    Found = True
                End If
            Next

            If Found Then
                txtFilter.Text = MainPl.ROMs(S1).DisplayName
                ActualFilter = S1
                Exit For
            End If
        Next
        Exit Sub

        For Each S1 In MainPl.ROMs
            For Each S2 In MainPl.ROMs
                If GetSimilarity(S1.DisplayName, S2.DisplayName) * 100 >= FilterP.Value Then
                    DGFilter.Rows.Add(GetSimilarity(S1.DisplayName, S2.DisplayName) * 100 & "%", S1.DisplayName, S2.DisplayName)
                End If
            Next
        Next
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DGFilter.Rows.Clear()
        txtFilter.Text = ""
        ActualFilter = -1
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim DCount As Integer = 0

        For S1 = 0 To MainPl.ROMs.Count - 1
            If S1 < MainPl.ROMs.Count Then
                For S2 = MainPl.ROMs.Count - 1 To 0 Step -1
                    If S2 < MainPl.ROMs.Count Then
                        If Not S1 = S2 Then
                            If MainPl.ROMs(S1).DisplayName = MainPl.ROMs(S2).DisplayName Then
                                MainPl.RemoveItem(S2)
                                DCount += 1
                            End If
                        End If
                    End If
                Next
            End If
        Next

        RefreshItems()
        MsgBox(DCount & " Items Removed!")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        For Each R As DataGridViewRow In DGAllP.Rows

        Next
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'Dim BaseP As String = TxtRPath.Text

        'If Not BaseP.EndsWith(Path.DirectorySeparatorChar) Then BaseP &= Path.DirectorySeparatorChar

        Not_Found_Count(0) = 0
        For Each R As PlaylistItem In MainPl.ROMs
            R.FExists(0) = File.Exists(PrepareFileName(R.ROMPath))
            If Not R.FExists(0) Then Not_Found_Count(0) += 1
        Next

        RefreshItems()
        'FileExists.Visible = True

        MsgBox("CHECKED!")
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim BaseP As String = TxtRFolder.Text

        If Not BaseP.EndsWith(Path.DirectorySeparatorChar) Then BaseP &= Path.DirectorySeparatorChar

        Not_Found_Count(1) = 0
        Not_Found_Count(2) = 0
        Not_Found_Count(3) = 0

        For Each R As PlaylistItem In MainPl.ROMs
            If chkBoxarts.Checked Then R.FExists(1) = True
            If chkSnaps.Checked Then R.FExists(2) = True
            If chkTitles.Checked Then R.FExists(3) = True

            Dim TFileName As String = R.DisplayName.Replace("&", "_").Replace(":", "_").Replace("\", "_").Replace("?", "_")
            If chkBoxarts.Checked And Not File.Exists(BaseP & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                R.FExists(1) = False
                Not_Found_Count(1) += 1
            End If

            If chkSnaps.Checked And Not File.Exists(BaseP & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                R.FExists(2) = False
                Not_Found_Count(2) += 1
            End If

            If chkTitles.Checked And Not File.Exists(BaseP & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                R.FExists(3) = False
                Not_Found_Count(3) += 1
            End If
        Next

        RefreshItems()
        'F_Boxarts.Visible = chkBoxarts.Checked
        'F_Snaps.Visible = chkSnaps.Checked
        'F_Titles.Visible = chkTitles.Checked

        MsgBox("CHECKED!")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim SPath As String = Environment.CurrentDirectory & Path.DirectorySeparatorChar & MainPl.PlaylistName.Replace(".lpl", "") & Path.DirectorySeparatorChar
        If Not Directory.Exists(SPath) Then Directory.CreateDirectory(SPath)

        Using pwriter_B As New StreamWriter(SPath & "Named_Boxarts.txt", False)
            Using pwriter_S As New StreamWriter(SPath & "Named_Snaps.txt", False)
                Using pwriter_T As New StreamWriter(SPath & "Named_Titles.txt", False)
                    For Each R As DataGridViewRow In DGAllP.Rows
                        If chkBoxarts.Checked Then
                            If Not R.Cells("F_Boxarts").Value Then pwriter_B.WriteLine(R.Cells("DisplayName").Value)
                        End If
                        If chkSnaps.Checked Then
                            If Not R.Cells("F_Snaps").Value Then pwriter_S.WriteLine(R.Cells("DisplayName").Value)
                        End If
                        If chkTitles.Checked Then
                            If Not R.Cells("F_Titles").Value Then pwriter_T.WriteLine(R.Cells("DisplayName").Value)
                        End If
                    Next
                End Using
            End Using
        End Using

        MsgBox("SAVED!")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim BaseP As String = TxtRFolder.Text

        If Not BaseP.EndsWith(Path.DirectorySeparatorChar) Then BaseP &= Path.DirectorySeparatorChar

        Dim MSetCount() As Integer = {0, 0, 0}
        Dim MSetErrorCount() As Integer = {0, 0, 0}

        Dim BasePath As String = Environment.CurrentDirectory & Path.DirectorySeparatorChar
        For Each R As PlaylistItem In MainPl.ROMs
            Dim TFileName As String = R.DisplayName.Replace("&", "_").Replace(":", "_").Replace("\", "_").Replace("?", "_")

            'If chkBoxarts.Checked And Not File.Exists(BaseP & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png") Then
            'Try
            'File.Copy(BasePath & "Default_Boxart.png", BaseP & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png")
            'MSetCount(0) += 1
            'Catch ex As Exception
            'MSetErrorCount(0) += 1
            'End Try
            'End If

            'If chkSnaps.Checked And Not File.Exists(BaseP & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png") Then
            'Try
            'File.Copy(BasePath & "Default_Snap.png", BaseP & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png")
            'MSetCount(1) += 1
            'Catch ex As Exception
            'MSetErrorCount(1) += 1
            'End Try
            'End If

            If chkTitles.Checked And Not File.Exists(BaseP & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                Try
                    File.Copy(BasePath & "Default_Title.png", BaseP & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png")
                    MSetCount(2) += 1
                Catch ex As Exception
                    MSetErrorCount(2) += 1
                End Try
            End If
        Next

        'RefreshItems()
        'F_Boxarts.Visible = chkBoxarts.Checked
        'F_Snaps.Visible = chkSnaps.Checked
        'F_Titles.Visible = chkTitles.Checked

        MsgBox("Default Thumbnails Copied! Total: {" & MSetCount(0) & ", " & MSetCount(1) & ", " & MSetCount(2) & "}" & vbNewLine & "Errors: {" & MSetErrorCount(0) & ", " & MSetErrorCount(1) & ", " & MSetErrorCount(2) & "}")
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim BaseP As String = TxtRFolder.Text
        If Not BaseP.EndsWith(Path.DirectorySeparatorChar) Then BaseP &= Path.DirectorySeparatorChar

        Dim BaseP2 As String = BaseP & "_Moved"
        If Not BaseP2.EndsWith(Path.DirectorySeparatorChar) Then BaseP2 &= Path.DirectorySeparatorChar

        Try
            Directory.CreateDirectory(BaseP2)
            Directory.CreateDirectory(BaseP2 & "Named_Boxarts")
            Directory.CreateDirectory(BaseP2 & "Named_Snaps")
            Directory.CreateDirectory(BaseP2 & "Named_Titles")
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
            Exit Sub
        End Try

        Dim MSetCount() As Integer = {0, 0, 0}
        Dim MSetErrorCount() As Integer = {0, 0, 0}

        Dim BasePath As String = Environment.CurrentDirectory & Path.DirectorySeparatorChar
        For Each R As PlaylistItem In MainPl.ROMs
            Dim TFileName As String = R.DisplayName.Replace("&", "_").Replace(":", "_").Replace("\", "_").Replace("?", "_")

            If chkBoxarts.Checked And File.Exists(BaseP & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                Try
                    File.Move(BaseP & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png", BaseP2 & "Named_Boxarts" & Path.DirectorySeparatorChar & TFileName & ".png")
                    MSetCount(0) += 1
                Catch ex As Exception
                    MSetErrorCount(0) += 1
                End Try
            End If

            If chkSnaps.Checked And File.Exists(BaseP & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                Try
                    File.Move(BaseP & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png", BaseP2 & "Named_Snaps" & Path.DirectorySeparatorChar & TFileName & ".png")
                    MSetCount(1) += 1
                Catch ex As Exception
                    MSetErrorCount(1) += 1
                End Try
            End If

            If chkTitles.Checked And File.Exists(BaseP & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png") Then
                Try
                    File.Copy(BaseP & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png", BaseP2 & "Named_Titles" & Path.DirectorySeparatorChar & TFileName & ".png")
                    MSetCount(2) += 1
                Catch ex As Exception
                    MSetErrorCount(2) += 1
                End Try
            End If
        Next

        MsgBox("Default Thumbnails Moved! Total: {" & MSetCount(0) & ", " & MSetCount(1) & ", " & MSetCount(2) & "}" & vbNewLine & "Errors: {" & MSetErrorCount(0) & ", " & MSetErrorCount(1) & ", " & MSetErrorCount(2) & "}")
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

    End Sub

    Private Sub BTSaveP_Click(sender As Object, e As EventArgs) Handles BTSaveP.Click

    End Sub
End Class