Imports System.IO
Public Class Dialogue
    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            ' Turn on WS_EX_COMPOSITED
            Return cp
        End Get
    End Property
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Installer.Show()
        Me.Close()
    End Sub

    Private Sub Dialogue_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If System.IO.File.Exists("uninstall.diagcab") = True Then
            File.Copy(".\uninstall.diagcab", Path.GetTempPath() & "uninstall.diagcab", True)   '相對路徑問題，顯把檔案複製到temp
            Shell("cmd /c msdt /cab " & Path.GetTempPath() & "uninstall.diagcab")

            Startup.Show()
            Me.Close()
        Else
            MessageBox.Show("找不到 uninstall.diagcab，請確認檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
        End If
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class