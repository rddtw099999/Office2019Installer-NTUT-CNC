Imports Microsoft.Win32
Imports System.IO

Public Class Startup
    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            ' Turn on WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If System.IO.File.Exists("bin\uninstall.diagcab") = True Then
            File.Copy("bin\uninstall.diagcab", Path.GetTempPath() & "uninstall.diagcab", True)   '相對路徑問題，顯把檔案複製到temp
            Shell("cmd /c msdt /cab " & Path.GetTempPath() & "uninstall.diagcab")
        Else
            MessageBox.Show("找不到 uninstall.diagcab，請確認檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
        End If


    End Sub


    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '            拿掉好惹，VPN 因為志豪的route rule ,偵測不到        且仰賴外部api

        '===============    IP check ======================= 
        'Dim myip As String

        'Try  '例外處理
        '    myip = New System.Net.WebClient().DownloadString("https://api.ipify.org/")  '依賴外部api https://api.ipify.org/
        'Catch
        '    MsgBox("網路錯誤或API掛惹", MsgBoxStyle.OkOnly, "唉 QQ")
        '    myip = "Not Found"
        'End Try



        'IPAddress.Text = "IP:" & myip
        'If Not myip.StartsWith("140.124") Then
        '    Button4.Enabled = False                          '若非校內網路，直接停用啟用按鈕
        '    Button4.Text = "請連接校園網路"

        'End If
        '=========================================




        '===========偵測方法可能要在嚴謹點================
        '=================  registry key check ==============
        Dim Reg As RegistryKey
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", True)
        If Reg.GetSubKeyNames().Contains("Office") = True Then
            Button5.Visible = True
            Label6.Visible = True
        End If
        ' ==========================================================



    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim Reg As RegistryKey
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft", True)

        If Reg.GetSubKeyNames().Contains("Office") = True Then
            Dialogue.Show()
        Else
            Installer.Show()
            'Me.Close()
        End If

        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        troubleshoot.Show()
        Me.Close()

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class