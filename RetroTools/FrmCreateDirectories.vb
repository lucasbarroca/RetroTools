Imports System.IO

Public Class FrmCreateDirectories
    Private Sub FrmCreateDirectories_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtFolder.Text = Environment.CurrentDirectory
    End Sub

    Private Sub BtFolder_Click(sender As Object, e As EventArgs) Handles BtFolder.Click
        FolderBrowserDialog1.SelectedPath = TxtFolder.Text

        If Not FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then Exit Sub
        TxtFolder.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TxtFolder.Text.EndsWith(Path.DirectorySeparatorChar) Then TxtFolder.Text &= Path.DirectorySeparatorChar

        Try
            If Not Directory.Exists(TxtFolder.Text) Then Directory.CreateDirectory(TxtFolder.Text)
        Catch ex As Exception

        End Try

        Dim Dirs() As String = Split(txtC.Text, vbNewLine)
        For Each D In Dirs
            Try
                If Not Directory.Exists(TxtFolder.Text & D) Then Directory.CreateDirectory(TxtFolder.Text & D)
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Lines() As String = Split(txtC.Text, vbNewLine)
        Dim newLines(UBound(Lines)) As String

        Dim R As String = TxtFolder.Text
        For L = 0 To UBound(Lines)
            newLines(L) = R.Replace("%R", Lines(L))
        Next

        txtC.Text = ""
        For Each NL In newLines
            txtC.Text &= NL & vbNewLine
        Next

        txtC.Text = txtC.Text.Substring(0, txtC.Text.Length - 1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim URLs() As String = Split(txtC.Text, vbNewLine)
        For Each U In URLs
            Process.Start(U)
        Next
    End Sub
End Class