Imports System.Net
Public Class Form1
    Dim savelocation As String
    Dim gamedownload As String = "C:\Riot Games\League of Legends\Config\backup2.cfg"
    Dim game As String = "C:\Riot Games\League of Legends\Config\game.cfg"
    Dim backupgame As String = "C:\Riot Games\League of Legends\Config\backup.cfg"
    Dim datumgame As String = System.IO.File.GetCreationTime(game)
    Dim datumbackup As String = System.IO.File.GetCreationTime(backupgame)
    Dim downloadlocation As String = "https://github.com/Mathero11/GameConfigBackup/blob/master/game.cfg"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If System.IO.File.Exists(game) Then
            Label1.Text = "game.cfg exist " + datumgame + ""
            Button3.Enabled = True
        Else
            Label1.Text = "game.cfg does not exist"
            Button3.Enabled = False
        End If
        If System.IO.File.Exists(backupgame) Then
            Label2.Text = "backup.cfg exist " + datumbackup + ""
            Button2.Enabled = True
        Else
            Label2.Text = "backup.cfg does not exist"
            Button2.Enabled = False
        End If

    End Sub
    Public WithEvents httpclient As WebClient

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ask As MsgBoxResult
        Dim blask As MsgBoxResult
        If System.IO.File.Exists(game) = False Then
            ask = MsgBox("Your game.cfg file does not exist! Create a original one?", MsgBoxStyle.YesNo, "")
            If ask = MsgBoxResult.Yes Then
                Dim objwriter As New System.IO.StreamWriter("C:\Riot Games\League of Legends\Config\game.cfg")
                objwriter.Write(RichTextBox1.Text)
                objwriter.Close()
            Else

            End If
        End If

        If System.IO.File.Exists(backupgame) = True Then
            MsgBox("Backup game does already exist, overwrite it?", MsgBoxStyle.YesNo, "")
            If blask = MsgBoxResult.Yes Then
                System.IO.File.Delete(backupgame)
                System.IO.File.Copy(game, backupgame)
                MsgBox("Copied")
                checkifexist()
            Else

                'nothing here to see
            End If
        End If
        System.Threading.Thread.Sleep(1000)
        If System.IO.File.Exists(backupgame) = False Then
            If System.IO.File.Exists(game) = True Then
                System.IO.File.Copy(game, backupgame)
                MsgBox("Succesfully created a back-up")
                checkifexist()

            Else
                MsgBox("I cannot copy anything if you have no .cfg file, im not a wizard..")
            End If

        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If System.IO.File.Exists(backupgame) = True Then
            Dim yesorno As MsgBoxResult
            yesorno = MsgBox("Keep the backupfile?", MsgBoxStyle.YesNo, "")
            If yesorno = MsgBoxResult.Yes Then
                System.IO.File.Delete(game)
                System.IO.File.Copy(backupgame, gamedownload)
                My.Computer.FileSystem.RenameFile(backupgame, "game.cfg")
                My.Computer.FileSystem.RenameFile(gamedownload, "backup.cfg")
                MsgBox("Replaced")
                checkifexist()
            Else
                System.IO.File.Delete(game)
                My.Computer.FileSystem.RenameFile(backupgame, "game.cfg")
                System.IO.File.Delete(backupgame)
                MsgBox("Replaced")
                checkifexist()

            End If
        Else
            MsgBox("No backupfile!")
        End If


    End Sub

    Public Sub checkifexist()
        Dim datumgame2 As String = System.IO.File.GetLastWriteTime(game)
        Dim datumbackup2 As String = System.IO.File.GetLastWriteTime(backupgame)
        If System.IO.File.Exists(game) = True Then
            Label1.Text = "game.cfg exist " + datumgame2 + ""
            Button3.Enabled = True
        Else
            Label1.Text = "game.cfg does not exist"
            Button3.Enabled = False
        End If
        If System.IO.File.Exists(backupgame) Then
            Label2.Text = "backup.cfg exist " + datumbackup2 + ""
            Button2.Enabled = True
        Else
            Label2.Text = "backup.cfg does not exist"
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If System.IO.File.Exists(game) Then
            Dim ask As MsgBoxResult
            ask = MsgBox("Are you sure?", MsgBoxStyle.YesNo, "")
            If ask = MsgBoxResult.Yes Then
                System.IO.File.Delete(game)
                checkifexist()
            Else
                'do nothing
                checkifexist()
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        checkifexist()
    End Sub

    Private Sub httpclient_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles httpclient.DownloadFileCompleted
        MsgBox("Downloaded in " + gamedownload + "")
        Dim ask As MsgBoxResult
        If ask = MsgBoxResult.Yes Then
            httpclient = New WebClient
            Try
                httpclient.DownloadFileAsync(New Uri(downloadlocation), (gamedownload))
            Catch ex As Exception
                MsgBox("Cannot download from github" + ErrorToString(), MsgBoxStyle.Critical)
            End Try
        Else
            'do nothing here
        End If
    End Sub

End Class
