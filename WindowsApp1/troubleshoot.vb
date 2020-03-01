Imports System.IO

Public Class troubleshoot
    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            ' Turn on WS_EX_COMPOSITED
            Return cp
        End Get
    End Property
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Startup.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If System.IO.File.Exists("bin\uninstall.diagcab") = True Then
            File.Copy("bin\uninstall.diagcab", Path.GetTempPath() & "uninstall.diagcab", True)   '相對路徑問題，顯把檔案複製到temp
            Shell("cmd /c msdt /cab " & Path.GetTempPath() & "uninstall.diagcab")
        Else
            MessageBox.Show("找不到 uninstall.diagcab，請確認檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If System.IO.File.Exists("bin\dotnetfix.exe") = True Then
            Process.Start("bin\dotnetfix.exe")
        Else
            MessageBox.Show("錯誤，dotnetfix.exe 檔案不存在，請確認安裝檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub troubleshoot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class