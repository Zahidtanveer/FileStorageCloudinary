Imports System.IO
Imports Microsoft.VisualBasic

Public Module LogWriter

    Sub LogWrite(ByVal logMessage As String, ByVal filePath As String)
        If Not File.Exists(filePath) Then File.Create(filePath)
        Try
            Using w As StreamWriter = New StreamWriter(filePath, True)
                w.WriteLine(logMessage)
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub AppendLog(ByVal logMessage As String, ByVal txtWriter As TextWriter)
        Try
            txtWriter.WriteLine(logMessage)
            txtWriter.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function ReadUrls(ByVal filePath As String) As List(Of String)
        Dim Urls As List(Of String) = New List(Of String)()
        Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(filePath)
        Dim a As String
        Do
            a = reader.ReadLine
            If a IsNot Nothing Then _
            Urls.Add(a)
        Loop Until a Is Nothing
        reader.Close()
        Return Urls
    End Function
End Module
