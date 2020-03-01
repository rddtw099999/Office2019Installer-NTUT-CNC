Imports System.IO

Public Class Installer
    Dim appExist(10) As Boolean
    Dim asked As Boolean = False
    Dim firstinit As Boolean = False

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            ' Turn on WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FILE_NAME As String = Path.GetTempPath() & "\configuration.xml"
        Dim sys_type As String
        Dim language As String
        Dim fs As FileStream = File.Create(FILE_NAME)  '先建立空白設定檔
        fs.Close()
        If System.IO.File.Exists(FILE_NAME) = True Then

            Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
            If sys_32.Checked Then
                sys_type = "32"                                 '//xml 的位元偵測
            Else
                sys_type = "64"
            End If

            If lng_zh.Checked Then
                language = "zh-tw"
            Else
                language = "en-us"
            End If

            'XML 輸出區

            '///XML 參考 office 部屬官方文件
            'https://docs.microsoft.com/zh-tw/deployoffice/office2019/deploy

            ''============================================================================================================================

            objWriter.WriteLine("<Configuration>")
            objWriter.Write("<Add OfficeClientEdition=""" & sys_type)         '//我覺得拿掉channel 可避免安裝時連網檢查下載新版本
            objWriter.WriteLine(""" Version=""16.0.10348.20020"" AllowCdnFallback=""FALSE"" ForceUpgrade=""FALSE""> ")
            objWriter.WriteLine("<Product ID=""ProPlus2019Volume""  PIDKEY=""NMMKJ-6RK4F-KMJVX-8D9MJ-6MWKP"">")              '//校園授權版
            objWriter.Write("   <Language ID=""")
            objWriter.WriteLine(language & """ />")
            '////軟體排除區
            If (sel_access.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Access"" />")
            End If
            If (sel_excel.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Excel"" />")
            End If
            If (sel_onedrive.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Groove"" />")
            End If
            If (sel_onenote.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""OneNote"" />")
            End If
            If (sel_outlook.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Outlook"" />")
            End If
            If (sel_powerpoint.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""PowerPoint"" />")
            End If
            If (sel_publisher.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Publisher"" />")
            End If
            If (sel_skype.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Lync"" />")
            End If
            If (sel_world.Checked = False) Then
                objWriter.WriteLine("<ExcludeApp ID=""Word"" />")
            End If

            objWriter.WriteLine("</Product>")


            '// Visio
            If (sel_visio.Checked = True) Then
                objWriter.WriteLine("<Product ID=""VisioPro2019Volume""  PIDKEY=""9BGNQ-K37YR-RQHF2-38RQ3-7VCBB"">")              '//校園授權版
                objWriter.Write("   <Language ID=""")
                objWriter.WriteLine(language & """ />")
                objWriter.WriteLine("</Product>")
            End If

            '//Project
            If (sel_project.Checked = True) Then
                objWriter.WriteLine("<Product ID=""ProjectStd2019Volume""  PIDKEY=""C4F7P-NCP8C-6CQPT-MQHV9-JXD2M"">")              '//校園授權版
                objWriter.Write("   <Language ID=""")
                objWriter.WriteLine(language & """ />")
                objWriter.WriteLine("</Product>")
            End If

            objWriter.WriteLine("</Add>")
            objWriter.WriteLine("<AppSettings>")
            objWriter.WriteLine("<Setup Name = ""Company"" Value=""NTUT Computer And Network Center"" />")
            objWriter.WriteLine("</AppSettings>")
            objWriter.WriteLine("<Display Level = ""Full"" AcceptEULA=""True"" />")
            objWriter.WriteLine("</Configuration>")
            objWriter.Close()

            '====================================================================================================================

            If Environment.Is64BitOperatingSystem = True And sys_32.Checked Then
                Dim response = MsgBox("你的作業系統版本是64位元的，你確定要安裝32位元的Office嗎?" & vbNewLine & "Your Operation System is 64bit , are you sure you want to install 32 bit version of Microsft Office 2019 ?", MsgBoxStyle.OkCancel, "哎 Oops?")
                If response = MsgBoxResult.Ok Then
                    If System.IO.File.Exists("bin\Setup.exe") = True Then
                        If System.IO.File.Exists(Path.GetTempPath() & "\configuration.xml") = True Then
                            Shell("bin\setup.exe /configure " & Path.GetTempPath() & "\configuration.xml")     '//用customize方式可避免下一次安裝要新增程式，但安裝時未勾選而被解除安裝的狀況
                        Else                                                                               '但是必須要偵測2019 是否 installed
                            MessageBox.Show("錯誤，無法在Temp資料夾中，建立Configuration.xml", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
                        End If
                    Else
                        MessageBox.Show("錯誤，Setup.exe 檔案不存在，請確認安裝檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
                    End If
                Else
                    sys_64.Checked = True

                End If

            ElseIf System.IO.File.Exists("bin\Setup.exe") = True Then
                If System.IO.File.Exists(Path.GetTempPath() & "\configuration.xml") = True Then
                    Shell("bin\setup.exe /configure " & Path.GetTempPath() & "\configuration.xml")     '//用customize方式可避免下一次安裝要新增程式，但安裝時未勾選而被解除安裝的狀況
                Else                                                                               '但是必須要偵測2019 是否 installed
                    MessageBox.Show("錯誤，無法在Temp資料夾中，建立Configuration.xml", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("錯誤，Setup.exe 檔案不存在，請確認安裝檔案完整性", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
            End If
        Else
            MessageBox.Show("錯誤，無法在Temp資料夾中，建立Configuration.xml", "發生錯誤拉拉拉拉拉拉", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Startup.Show()
        Me.Close()
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
    Private Declare Function GetSystemDefaultLangID Lib "kernel32" () As Integer
    Private Sub Installer_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If Not (GetSystemDefaultLangID = "1028") Then
            lng_en.Checked = True
        End If

        If Environment.Is64BitOperatingSystem = False Then
            sys_64.Enabled = False
            sys_32.Checked = True
        End If

        InstalledAppCheck()
        firstinit = True
    End Sub
    Private Sub InstalledAppCheck()

        Dim appFilename() As String = {"WINWORD", "EXCEL", "POWERPNT", "OUTLOOK", "ONENOTE", "MSACCESS", "MSPUB", "GROOVE", "LYNC", "WINPROJ", "VISIO"}
        Dim appChkbox() As CheckBox = New CheckBox() {sel_world, sel_excel, sel_powerpoint, sel_outlook, sel_onenote, sel_access, sel_publisher, sel_onedrive, sel_skype, sel_project, sel_visio}
        Dim SysLoc() As String = {"C:\Program Files (x86)", "C:\Program Files"}
        Dim lng As String = "(已安裝)"
        'Dim origname(10) As String
        'For a = 0 To 10
        '    origname(a) = appChkbox(a).Text
        'Next
        If Not (GetSystemDefaultLangID = "1028") Then
            lng = "(Installed)"
        End If

        For i = 0 To 1
            For j = 0 To 10
                If System.IO.File.Exists(SysLoc(i) & "\Microsoft Office\root\Office16\" & appFilename(j) & ".exe") = True Then
                    appChkbox(j).Text = appChkbox(j).Text & " " & lng
                    appChkbox(j).Checked = True
                    appExist(j) = True

                End If
            Next
        Next


    End Sub

    Private Sub sel_access_CheckedChanged(sender As Object, e As EventArgs) Handles sel_world.CheckedChanged, sel_excel.CheckedChanged, sel_powerpoint.CheckedChanged, sel_outlook.CheckedChanged, sel_onenote.CheckedChanged, sel_access.CheckedChanged, sel_publisher.CheckedChanged, sel_onedrive.CheckedChanged, sel_skype.CheckedChanged, sel_project.CheckedChanged, sel_visio.CheckedChanged
        Dim msgtext As String = "你目前取消勾選的應用程式已安裝在電腦中，這麼一來應用程式將被取消安裝，您確定要解除安裝嗎?"
        Dim msgtitle As String = "您確定要解除安裝嗎"
        If asked = False Then
            If appExist((sender).tag) = True Then
                asked = True
                If Not (GetSystemDefaultLangID = "1028") Then
                    msgtext = "You just deselected applications installed on the computer and the following operation will cause it to be removed. Continue?"
                    msgtitle = "Confirmation"
                End If
                Dim response = MsgBox(msgtext, vbYesNo, msgtitle)
                If response = MsgBoxResult.No Then
                    sender.checked = True

                End If

            End If
        End If

    End Sub
End Class
