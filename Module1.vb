Imports System.IO
Imports System.Reflection
Imports TobixLibs.Net
Imports System.Security.Cryptography
Imports System.Text

Module Module1

    Dim WithEvents Server As ServerClass
    Dim client As New ClientClass
    Private port As Integer = My.Settings.port
    Private integerline As Integer
    Private Interfacelogon As Boolean = False
    Private stringdataarry As String()
    Private Interfaceip As String
    Private connectedclients As New List(Of String)()
    Private application_path As DirectoryInfo
    Private userdatabasepath As FileInfo
    Private projectdatabasepath As FileInfo
    Private cameradatabasepath As FileInfo
    Private categorydatabasepath As FileInfo
    Private musicdatabasepath As FileInfo
    Private logpath As FileInfo
    Private project_path As DirectoryInfo
    Private temp_path As DirectoryInfo
    Private import_path As DirectoryInfo
    Private encode_path As DirectoryInfo
    Private template_path As DirectoryInfo
    Private template_custom_path As DirectoryInfo
    Private template_preset_path As DirectoryInfo
    Private update_path As DirectoryInfo
    Private music_path As DirectoryInfo
    Private filestream As FileStream
    Private userdatabase As New DataSet("Users")
    Private projectdatabase As New DataSet("Projects")
    Private camerdatabase As New DataSet("Camera")
    Private categorydatabase As New DataSet("Category")
    Private templatedatabase As New DataSet("Template")
    Private musicdatabase As New DataSet("Music")
    Dim music_import_path As DirectoryInfo
    Dim music_pool_path As DirectoryInfo
    Private static_ip As Boolean = My.Settings.cutom_ip_bo
    Private custom_ip As String = My.Settings.custom_ip
    Private current_client_version As String = My.Settings.client_update_version
    Private update_avi As Boolean = My.Settings.activate_update
    Private import_other As DirectoryInfo

    Sub Main()

#Region "Paths"

        If My.Settings.main_path <> "" Then

            application_path = New DirectoryInfo(My.Settings.main_path)

        Else

            application_path = New DirectoryInfo("C:\FCS-Verwaltung")

            My.Settings.main_path = "C:\FCS-Verwaltung"
            My.Settings.Save()

        End If

        If My.Settings.userdatabase_path <> "" Then

            userdatabasepath = New FileInfo(My.Settings.main_path & "\" & My.Settings.userdatabase_path)

        Else

            userdatabasepath = New FileInfo(My.Settings.main_path & "\" & "user.usfcs")

            My.Settings.userdatabase_path = "user.usfcs"
            My.Settings.Save()

        End If

        If My.Settings.projectdatabse_path <> "" Then

            projectdatabasepath = New FileInfo(My.Settings.main_path & "\" & My.Settings.projectdatabse_path)

        Else

            projectdatabasepath = New FileInfo(My.Settings.main_path & "\" & "projects.prfcs")

            My.Settings.projectdatabse_path = "projects.prfcs"
            My.Settings.Save()

        End If

        If My.Settings.cameradatabse_path <> "" Then

            cameradatabasepath = New FileInfo(My.Settings.main_path & "\" & My.Settings.cameradatabse_path)

        Else

            cameradatabasepath = New FileInfo(My.Settings.main_path & "\" & "camera.cafcs")

            My.Settings.cameradatabse_path = "camera.cafcs"
            My.Settings.Save()

        End If

        If My.Settings.categorydatabase_path <> "" Then

            categorydatabasepath = New FileInfo(My.Settings.main_path & "\" & My.Settings.categorydatabase_path)

        Else

            categorydatabasepath = New FileInfo(My.Settings.main_path & "\" & "category.ctfcs")

            My.Settings.categorydatabase_path = "category.ctfcs"
            My.Settings.Save()

        End If

        If My.Settings.project_path <> "" Then

            project_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.project_path)

        Else

            project_path = New DirectoryInfo(My.Settings.main_path & "\" & "projects")

            My.Settings.project_path = "projects"
            My.Settings.Save()

        End If

        If My.Settings.temp_path <> "" Then

            temp_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.temp_path)

        Else

            temp_path = New DirectoryInfo(My.Settings.main_path & "\" & "temp")

            My.Settings.temp_path = "temp"
            My.Settings.Save()

        End If

        If My.Settings.import_path <> "" Then

            import_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.import_path)

        Else

            import_path = New DirectoryInfo(My.Settings.main_path & "\" & "import")

            My.Settings.import_path = "import"
            My.Settings.Save()

        End If

        If My.Settings.encode_path <> "" Then

            encode_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.encode_path)

        Else

            encode_path = New DirectoryInfo(My.Settings.main_path & "\" & "encode")

            My.Settings.encode_path = "encode"
            My.Settings.Save()

        End If

        If My.Settings.template_path <> "" Then

            template_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.template_path)

        Else

            template_path = New DirectoryInfo(My.Settings.main_path & "\" & "template")

            My.Settings.template_path = "template"
            My.Settings.Save()

        End If

        If My.Settings.template_custom_path <> "" Then

            template_custom_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.template_path & "\" & My.Settings.template_custom_path)

        Else

            template_custom_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.template_path & "\" & "custom")

            My.Settings.template_custom_path = "custom"
            My.Settings.Save()

        End If

        If My.Settings.template_preset_path <> "" Then

            template_preset_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.template_path & "\" & My.Settings.template_preset_path)

        Else

            template_preset_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.template_path & "\" & "preset")

            My.Settings.template_preset_path = "preset"
            My.Settings.Save()

        End If

        If My.Settings.musicdatabase_path <> "" Then

            musicdatabasepath = New FileInfo(My.Settings.main_path & "\" & My.Settings.musicdatabase_path)

        Else

            musicdatabasepath = New FileInfo(My.Settings.main_path & "\" & "music.mufcs")

            My.Settings.musicdatabase_path = "music.mufcs"
            My.Settings.Save()

        End If

        If My.Settings.update_path <> "" Then

            update_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.update_path)

        Else

            update_path = New DirectoryInfo(My.Settings.main_path & "\" & "update")

            My.Settings.update_path = "update"
            My.Settings.Save()

        End If

        If My.Settings.music_path <> "" Then

            music_path = New DirectoryInfo(My.Settings.main_path & "\" & My.Settings.music_path)

        Else

            music_path = New DirectoryInfo(My.Settings.main_path & "\" & "music")

            My.Settings.music_path = "music"
            My.Settings.Save()

        End If

        If My.Settings.log_path <> "" Then

            logpath = New FileInfo(My.Settings.main_path & "\" & My.Settings.log_path)

        Else

            logpath = New FileInfo(My.Settings.main_path & "\" & "log.txt")

            My.Settings.log_path = "log.txt"
            My.Settings.Save()

        End If

#End Region

        If Not File.Exists(update_path.FullName & "\client.exe") Then

            Console.WriteLine("Client.exe is missing! No automatic Updatefunction aviable!")

            update_avi = False

        Else
            If My.Settings.activate_update = True Then

                update_avi = True

            Else

                update_avi = False

            End If

        End If

        Console.WindowWidth = 135

        Console.Title = "FCS-Verwaltungs-Server"

        Console.WriteLine("Starting FCS-Verwaltungs-Server")

        Server = New ServerClass(port, True, static_ip, custom_ip)

        Server.StartServer()

        Console.WriteLine("")

        Console.WriteLine("Server Started")

        Console.WriteLine("Current IP is:                   " & Server.LocalIP)

        Console.WriteLine("Current Port is:                 " & port)

        Console.WriteLine("Interface Port is:               " & port + 1)

        Console.WriteLine("Current Application Path is:     " & application_path.FullName)

        Console.WriteLine("Current Userdatabase Path is:    " & userdatabasepath.FullName)

        Console.WriteLine("Current Projectdatabse Path is:  " & projectdatabasepath.FullName)

        Console.WriteLine("Current Cameradatabse Path is:   " & cameradatabasepath.FullName)

        Console.WriteLine("Current Musicdatabase Path is:   " & musicdatabasepath.FullName)

        Console.WriteLine("Current Import Path is:          " & import_path.FullName)

        Console.WriteLine("Current Encode Path is:          " & encode_path.FullName)

        Console.WriteLine("Current Tempalte Path is:        " & template_path.FullName)

        Console.WriteLine("Current Project Path is:         " & project_path.FullName)

        Console.WriteLine("Current Temporary Path is:       " & temp_path.FullName)

        Console.WriteLine("Current Update Path is:          " & update_path.FullName)

        Console.WriteLine("Current Music Path is:           " & music_path.FullName)

        Console.WriteLine("Update function enabled?:        " & update_avi)

        Console.WriteLine("Current Client Version is:       " & current_client_version)

        If Not application_path.Exists Then

            application_path.Create()

        End If

        If Not import_path.Exists Then

            import_path.Create()

        End If

        If Not encode_path.Exists Then

            encode_path.Create()

        End If

        If Not template_path.Exists Then

            template_path.Create()

        End If

        If Not template_custom_path.Exists Then

            template_custom_path.Create()

        End If

        If Not template_preset_path.Exists Then

            template_preset_path.Create()

        End If

        import_other = New DirectoryInfo(import_path.FullName & "\other")

        If Not import_other.Exists Then

            import_other.Create()

        End If

#Region "Create Presets"

        template_preset_path.CreateSubdirectory("1080P")
        template_preset_path.CreateSubdirectory("720P")

        template_preset_path.CreateSubdirectory("1080P\AVCHD 1080P 25FPS")
        template_preset_path.CreateSubdirectory("1080P\AVCHD 1080P 30FPS")
        template_preset_path.CreateSubdirectory("1080P\AVCHD 1080P 50FPS")
        template_preset_path.CreateSubdirectory("1080P\AVCHD 1080P 60FPS")


        template_preset_path.CreateSubdirectory("720P\AVCHD 720P 25FPS")
        template_preset_path.CreateSubdirectory("720P\AVCHD 720P 30FPS")
        template_preset_path.CreateSubdirectory("720P\AVCHD 720P 50FPS")
        template_preset_path.CreateSubdirectory("720P\AVCHD 720P 60FPS")

        Dim A1080P25 As New FileInfo(template_preset_path.FullName & "\1080P\AVCHD 1080P 25FPS\info.conf")
        Dim A1080P30 As New FileInfo(template_preset_path.FullName & "\1080P\AVCHD 1080P 30FPS\info.conf")
        Dim A1080P50 As New FileInfo(template_preset_path.FullName & "\1080P\AVCHD 1080P 50FPS\info.conf")
        Dim A1080P60 As New FileInfo(template_preset_path.FullName & "\1080P\AVCHD 1080P 60FPS\info.conf")
        Dim A720P25 As New FileInfo(template_preset_path.FullName & "\720P\AVCHD 720P 25FPS\info.conf")
        Dim A720P30 As New FileInfo(template_preset_path.FullName & "\720P\AVCHD 720P 30FPS\info.conf")
        Dim A720P50 As New FileInfo(template_preset_path.FullName & "\720P\AVCHD 720P 50FPS\info.conf")
        Dim A720P60 As New FileInfo(template_preset_path.FullName & "\720P\AVCHD 720P 60FPS\info.conf")


        If Not A1080P25.Exists Then

            Dim sw As StreamWriter = A1080P25.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 1080p HD-Video mit 25 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A1080P30.Exists Then

            Dim sw As StreamWriter = A1080P30.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 1080p HD-Video mit 29,97 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A1080P50.Exists Then

            Dim sw As StreamWriter = A1080P50.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 1080p HD-Video mit 50 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A1080P60.Exists Then

            Dim sw As StreamWriter = A1080P60.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 1080p HD-Video mit 59,94 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A720P25.Exists Then

            Dim sw As StreamWriter = A720P25.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 720p HD-Video mit 25 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A720P30.Exists Then

            Dim sw As StreamWriter = A720P30.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 720p HD-Video mit 29,97 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A720P50.Exists Then

            Dim sw As StreamWriter = A720P50.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 720p HD-Video mit 50 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

        End If

        If Not A720P60.Exists Then

            Dim sw As StreamWriter = A720P60.CreateText

            sw.WriteLine("Nur Premiere! 16:9 Progressive-Scan 720p HD-Video mit 59,94 Frames pro Sekunde, 48 kHz Audio" & vbCrLf & "3 Videospuren" & vbCrLf & "Audiospuren:" & vbCrLf & "Art der Masterspur: Stereo" & vbCrLf & "Samplerate: 48000 Samples/Sekunde" & vbCrLf & "Audio 1: Standard" & vbCrLf & "Audio 2: Standard" & vbCrLf & "Audio 3: Standard")

            sw.Flush()

            sw.Close()

            Console.WriteLine("ERROR: Templates missing! Please copy them to templates\preset")

        End If

#End Region

        If Not project_path.Exists Then

            project_path.Create()

        End If

        If Not update_path.Exists Then

            update_path.Create()

        End If

#Region "Music_Path"

        music_import_path = New DirectoryInfo(music_path.FullName & "\import")
        music_pool_path = New DirectoryInfo(music_path.FullName & "\Pool")

        If Not music_path.Exists Then

            music_path.Create()

        End If

        If Not music_import_path.Exists Then

            music_import_path.Create()

        End If

        If music_import_path.GetFiles.Count <> 0 Then

            Try

                music_import_path.Delete(True)
                music_import_path.Create()

            Catch ex As Exception

            End Try

        End If

        If Not music_pool_path.Exists Then

            music_pool_path.Create()

        End If

#End Region

        If Not File.Exists(update_path.FullName & "\update.bat") Then

            Dim sw As StreamWriter = File.CreateText(update_path.FullName & "\update.bat")

            sw.WriteLine("@echo off")

            sw.WriteLine("title FCS-Update-Programm")

            sw.WriteLine("echo Update in Progress!")

            sw.WriteLine("TIMEOUT 4")

            sw.WriteLine("del client.exe")

            sw.WriteLine("ren update.exe client.exe")

            sw.WriteLine("start client.exe")

            sw.WriteLine("del update.bat")

            sw.Flush()

            sw.Close()

        End If

        If Not temp_path.Exists Then

            temp_path.Create()

        End If

        If Not userdatabasepath.Exists Then

            Dim sw As StreamWriter = userdatabasepath.CreateText

            sw.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " standalone=" & Chr(34) & "yes" & Chr(34) & "?>" & vbCrLf & "<User />")

            sw.Flush()

            sw.Close()

        End If

        If Not projectdatabasepath.Exists Then

            Dim sw As StreamWriter = projectdatabasepath.CreateText

            sw.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " standalone=" & Chr(34) & "yes" & Chr(34) & "?>" & vbCrLf & "<Project />")

            sw.Flush()

            sw.Close()

        End If

        If Not cameradatabasepath.Exists Then

            Dim sw As StreamWriter = cameradatabasepath.CreateText

            sw.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " standalone=" & Chr(34) & "yes" & Chr(34) & "?>" & vbCrLf & "<Camera />")

            sw.Flush()

            sw.Close()

        End If

        If Not categorydatabasepath.Exists Then

            Dim sw As StreamWriter = categorydatabasepath.CreateText

            sw.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " standalone=" & Chr(34) & "yes" & Chr(34) & "?>" & vbCrLf & "<Category />")

            sw.Flush()

            sw.Close()

        End If

        If Not musicdatabasepath.Exists Then

            Dim sw As StreamWriter = musicdatabasepath.CreateText

            sw.WriteLine("<?xml version=" & Chr(34) & "1.0" & Chr(34) & " standalone=" & Chr(34) & "yes" & Chr(34) & "?>" & vbCrLf & "<Music />")

            sw.Flush()

            sw.Close()

        End If

        datatable_create()

        userdatabase.ReadXml(userdatabasepath.FullName)

        projectdatabase.ReadXml(projectdatabasepath.FullName)

        camerdatabase.ReadXml(cameradatabasepath.FullName)

        categorydatabase.ReadXml(categorydatabasepath.FullName)

        musicdatabase.ReadXml(musicdatabasepath.FullName)

        create_temp_to_project_watcher()

        create_import_to_temp_watcher()

        'Importpath freigeben
        cmd_process("net share import_share=" & import_path.FullName & " /grant:netzwerkzugriff,full", "", False)

        cmd_process("cacls " & import_path.FullName & " /G netzwerkzugriff:f /E /T", "", False)

        'Projectpath freigeben
        cmd_process("net share project_share=" & project_path.FullName & " /grant:netzwerkzugriff,full", "", False)

        cmd_process("cacls " & project_path.FullName & " /G netzwerkzugriff:f /E /T", "", False)

        'Updatepath freigeben
        cmd_process("net share update_share=" & update_path.FullName & " /grant:netzwerkzugriff,full", "", False)

        cmd_process("cacls " & update_path.FullName & " /G netzwerkzugriff:f /E /T", "", False)

        'Music_import freigeben
        cmd_process("net share music_import_share=" & music_import_path.FullName & " /grant:netzwerkzugriff,full", "", False)

        cmd_process("cacls " & music_import_path.FullName & " /G netzwerkzugriff:f /E /T", "", False)

        'Music_pool freigeben
        cmd_process("net share music_pool_share=" & music_pool_path.FullName & " /grant:netzwerkzugriff,full", "", False)

        cmd_process("cacls " & music_pool_path.FullName & " /G netzwerkzugriff:f /E /T", "", False)

        'Firewall Port öffnen
        Dim cmdpro As New Process
        cmdpro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        cmdpro.StartInfo.FileName = "cmd.exe"
        cmdpro.StartInfo.Arguments = " /c netsh advfirewall firewall show rule name=""FCS-Verwaltung Server Client"" || netsh advfirewall firewall add rule name=""FCS-Verwaltung Server Client"" dir=in action=allow localport=" & port & " protocol=tcp"
        cmdpro.Start()

        'Firewall Interfaceport öffnen
        Dim cmdpro_1 As New Process
        cmdpro_1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        cmdpro_1.StartInfo.FileName = "cmd.exe"
        cmdpro_1.StartInfo.Arguments = " /c netsh advfirewall firewall show rule name=""FCS-Verwaltung Server Interface"" || netsh advfirewall firewall add rule name=""FCS-Verwaltung Server Interface"" dir=in action=allow localport=" & port + 1 & " protocol=tcp"
        cmdpro_1.Start()

#Region "read templates"

        Dim templates_preset_1080 As New DirectoryInfo(template_preset_path.FullName & "\1080P\")
        Dim templates_preset_720 As New DirectoryInfo(template_preset_path.FullName & "\720P\")


        'add to database TemplateGroup(720)

        Dim row1 As DataRow

        row1 = templatedatabase.Tables("TemplateGroup").NewRow

        row1("Name") = "1080P"

        templatedatabase.Tables("TemplateGroup").Rows.Add(row1)


        For Each folder As DirectoryInfo In templates_preset_1080.GetDirectories

            'read conf file

            Dim info As String = File.ReadAllText(folder.FullName & "\info.conf")

            'add to database

            Dim row As DataRow

            row = templatedatabase.Tables("Template").NewRow

            row("Name") = folder.Name
            row("Info") = info
            row("Resolution") = "1080P"
            row("Group") = "1080P"
            row("Path") = folder.FullName

            templatedatabase.Tables("Template").Rows.Add(row)

        Next


        'add to database TemplateGroup(720)

        Dim row2 As DataRow

        row2 = templatedatabase.Tables("TemplateGroup").NewRow

        row2("Name") = "720P"

        templatedatabase.Tables("TemplateGroup").Rows.Add(row2)


        For Each folder As DirectoryInfo In templates_preset_720.GetDirectories

            'read conf file

            Dim info As String = File.ReadAllText(folder.FullName & "\info.conf")

            'add to database Template

            Dim row As DataRow

            row = templatedatabase.Tables("Template").NewRow

            row("Name") = folder.Name
            row("Info") = info
            row("Resolution") = "720P"
            row("Group") = "720P"
            row("Path") = folder.FullName

            templatedatabase.Tables("Template").Rows.Add(row)

        Next

        For Each folder As DirectoryInfo In template_custom_path.GetDirectories

            'add to database TemplateGroup(Custom)

            Dim row3 As DataRow

            row3 = templatedatabase.Tables("TemplateGroup").NewRow

            row3("Name") = folder.Name

            templatedatabase.Tables("TemplateGroup").Rows.Add(row3)

            For Each sub_folder As DirectoryInfo In folder.GetDirectories

                'read conf file

                Dim info As String

                Try

                    info = File.ReadAllText(sub_folder.FullName & "\info.conf")

                Catch ex As Exception

                    info = ""

                End Try

                'add to database

                Dim row As DataRow

                row = templatedatabase.Tables("Template").NewRow

                row("Name") = sub_folder.Name
                row("Info") = info
                row("Resolution") = folder.Name
                row("Group") = folder.Name
                row("Path") = sub_folder.FullName

                templatedatabase.Tables("Template").Rows.Add(row)


            Next

        Next

#End Region

        'noch nicht importiere Dateien importieren
        If check_for_import_files() = True Then

            Console.WriteLine("")

            Console.Write(">")

            Dim remaingfiles = import_path.GetFiles("*.*", SearchOption.TopDirectoryOnly)

            move_to_temp(remaingfiles(0).FullName)

        End If

        If check_for_import_files_other() = True Then

            Console.WriteLine("")

            Console.Write(">")

            Dim remaingfiles = import_other.GetFiles("*.*", SearchOption.TopDirectoryOnly)

            move_to_other(remaingfiles(0).FullName)

        End If

        'auf eingabe warten
        waitingforinput()

    End Sub

    Private Sub datatable_create()

#Region "User"

        Dim UserIDColumn As DataColumn = New DataColumn
        UserIDColumn.DataType = System.Type.GetType("System.Int32")
        With UserIDColumn
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "UserID"
        End With

        Dim GroupIDColumn As DataColumn = New DataColumn
        GroupIDColumn.DataType = System.Type.GetType("System.Int32")
        With GroupIDColumn
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "GroupID"
        End With

        Dim PermissionsColumn As DataColumn = New DataColumn
        PermissionsColumn.DataType = System.Type.GetType("System.String")
        With PermissionsColumn

            .ColumnName = "Permissions"
            .DefaultValue = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0"

        End With

        Dim AdminColumn As DataColumn = New DataColumn
        AdminColumn.DataType = System.Type.GetType("System.Boolean")
        With AdminColumn

            .ColumnName = "admin"
            .DefaultValue = "False"

        End With

        Dim usertable As DataTable = New DataTable("User")
        usertable.Columns.Add(UserIDColumn)
        usertable.Columns.Add("UserName")
        usertable.Columns.Add("Password")
        usertable.Columns.Add("Salt")
        usertable.Columns.Add("GroupID")
        usertable.Columns.Add("enabled")
        usertable.Columns.Add(AdminColumn)

        Dim grouptable As DataTable = New DataTable("Groups")
        grouptable.Columns.Add(GroupIDColumn)
        grouptable.Columns.Add("GroupName")
        grouptable.Columns.Add("Description")
        grouptable.Columns.Add(PermissionsColumn) 'for more infos look at arguemnts.txt #94

        userdatabase.Tables.Add(usertable)
        userdatabase.Tables.Add(grouptable)

#End Region

#Region "Project"

        Dim ProjectIDColumn As New DataColumn
        ProjectIDColumn.DataType = System.Type.GetType("System.Int32")
        With ProjectIDColumn
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "ProjectID"
        End With

        Dim ClipID As New DataColumn
        ClipID.DataType = System.Type.GetType("System.Int32")
        ClipID.ColumnName = "ClipID"
        ClipID.DefaultValue = Format(0, "000")

        Dim otherData As New DataColumn
        otherData.DataType = System.Type.GetType("System.Int32")
        otherData.ColumnName = "otherData"
        otherData.DefaultValue = Format(0, "000")

        Dim archived As New DataColumn
        archived.DataType = System.Type.GetType("System.Boolean")
        archived.ColumnName = "Archived"
        archived.DefaultValue = False


        Dim projecttable As DataTable = New DataTable("Project")

        projecttable.Columns.Add(ProjectIDColumn)
        projecttable.Columns.Add("Name")
        projecttable.Columns.Add("Category")
        projecttable.Columns.Add("Description")
        projecttable.Columns.Add(ClipID)
        projecttable.Columns.Add(archived)
        projecttable.Columns.Add(otherData)

        projectdatabase.Tables.Add(projecttable)

#End Region

#Region "Camera"

        Dim CameraIDColumn As New DataColumn
        CameraIDColumn.DataType = System.Type.GetType("System.Int32")
        With CameraIDColumn
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "CameraID"

        End With

        Dim cameratable As New DataTable("Camera")

        cameratable.Columns.Add(CameraIDColumn)
        cameratable.Columns.Add("Name")
        cameratable.Columns.Add("description")

        camerdatabase.Tables.Add(cameratable)

#End Region

#Region "Category"

        Dim CategoryIDColumn As New DataColumn
        With CategoryIDColumn
            .DataType = Type.GetType("System.Int32")
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "CategoryID"
        End With

        Dim categorytable As New DataTable("Category")

        categorytable.Columns.Add(CategoryIDColumn)
        categorytable.Columns.Add("Name")
        categorytable.Columns.Add("Description")

        categorydatabase.Tables.Add(categorytable)

#End Region

#Region "templates"

        Dim TemplateIDColumn As New DataColumn
        With TemplateIDColumn
            .DataType = Type.GetType("System.Int32")
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "TemplateID"
        End With

        Dim TemplateGroupIDColumn As New DataColumn
        With TemplateGroupIDColumn
            .DataType = Type.GetType("System.Int32")
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "TemplateGroupID"
        End With

        Dim templatetable As New DataTable("Template")

        templatetable.Columns.Add(templateIDColumn)
        templatetable.Columns.Add("Name")
        templatetable.Columns.Add("Info")
        templatetable.Columns.Add("Resolution")
        templatetable.Columns.Add("Group")
        templatetable.Columns.Add("Path")

        templatedatabase.Tables.Add(templatetable)

        Dim templategrouptable As New DataTable("TemplateGroup")

        templategrouptable.Columns.Add(TemplateGroupIDColumn)
        templategrouptable.Columns.Add("Name")

        templatedatabase.Tables.Add(templategrouptable)

#End Region

#Region "music"

        Dim musicIDColumn As New DataColumn
        With musicIDColumn
            .DataType = Type.GetType("System.Int32")
            .AutoIncrement = True
            .AutoIncrementSeed = 0
            .AutoIncrementStep = 1
            .ColumnName = "musicID"
        End With

        Dim musictable As New DataTable("Music")

        musictable.Columns.Add(musicIDColumn)
        musictable.Columns.Add("Name")
        musictable.Columns.Add("Interpret")
        musictable.Columns.Add("Album")
        musictable.Columns.Add("Year")
        musictable.Columns.Add("Genre")
        musictable.Columns.Add("Comment")
        musictable.Columns.Add("Extension")
        musictable.Columns.Add("Time")
        musictable.Columns.Add("Path") 'is music_path.FullName & "\" & path

        musicdatabase.Tables.Add(musictable)

#End Region

    End Sub

    Private Sub OnIncomingMessage(ByVal Args As ServerClass.InMessEvArgs) Handles Server.IncomingMessage

        Console.WriteLine()

        Dim senderip As String = Args.senderIP
        Dim DATA As String = Args.message

#Region "Scan"

        If DATA = "scan" Then

            Console.Write("Scan from '" & senderip & "'")

            client_sendMessage(20, 2, senderip, False, "")

            waitingforinput()

        End If

#End Region

        stringdataarry = DATA.Split(CChar(";"))

        If Not arraytest_2(senderip) = True Then

            waitingforinput()

        End If

        Select Case True

            Case stringdataarry(0) = "interface" And senderip = Server.LocalIP

                If stringdataarry(1) = "login" And Interfacelogon = False Then

                    Console.WriteLine("Interface Login from " & senderip)
                    Interfaceip = Server.LocalIP
                    client_sendMessage(0, 0, Server.LocalIP, True, "")
                    Interfacelogon = True
                    If connectedclients.Count <> 0 Then

                        client_sendMessage(1, 1, Server.LocalIP, True, "")

                    End If

                ElseIf stringdataarry(1) = "logout" And Interfacelogon = True Then

                    Console.WriteLine("Interface logout from " & senderip)
                    Interfacelogon = False

                End If

            Case stringdataarry(2) = "login"
                If arraytest_2(senderip) = True Then

                    login(senderip)

                Else

                    waitingforinput()

                End If


            Case stringdataarry(2) = "logout"
                If arraytest_2(senderip) = True Then

                    logoff(senderip, stringdataarry(0), stringdataarry(1))

                Else

                    waitingforinput()

                End If


#Region "User"

            Case stringdataarry(2) = "createUser"
                create_User(stringdataarry(0), stringdataarry(1), stringdataarry(3), stringdataarry(4), stringdataarry(5), stringdataarry(6), stringdataarry(7), senderip, stringdataarry(8))
                          'Username           Password          New Username       New Password       Salt               GroupID            Enabled            IP

            Case stringdataarry(2) = "deleteUser"
                delete_User(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3))

            Case stringdataarry(2) = "editUser"
                edit_User(stringdataarry(0), stringdataarry(1), stringdataarry(3), senderip, Convert.ToInt32(stringdataarry(4)), stringdataarry(5), stringdataarry(6))


#End Region

#Region "Category"

            Case stringdataarry(2) = "createCategory"
                create_Category(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4))

            Case stringdataarry(2) = "deleteCategory"
                delte_category(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3))

            Case stringdataarry(2) = "editCategory"
                edit_Category(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), Convert.ToInt32((stringdataarry(4))), stringdataarry(5), stringdataarry(6))
#End Region

#Region "Camera"

            Case stringdataarry(2) = "createCamera"

                Dim stringdataarray_4 As String = ""

                Try

                    If stringdataarry(4) = "" Then


                    End If

                    stringdataarray_4 = stringdataarry(4)

                Catch ex As Exception

                    stringdataarray_4 = ""

                End Try

                create_Camera(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarray_4)

            Case stringdataarry(2) = "deleteCamera"
                delete_Camera(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3))

            Case stringdataarry(2) = "editCamera"
                edit_Camera(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), Convert.ToInt32(stringdataarry(4)), stringdataarry(5), stringdataarry(6))

#End Region

#Region "Project"

            Case stringdataarry(2) = "createProject"

                Dim stringdataarray_5 As String = ""

                Try

                    If stringdataarry(5) = "" Then


                    End If

                    stringdataarray_5 = stringdataarry(5)

                Catch ex As Exception

                    stringdataarray_5 = ""

                End Try

                create_Project(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4), stringdataarray_5, Convert.ToInt32(stringdataarry(6)))

            Case stringdataarry(2) = "deleteProject"
                delete_Project(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4))

            Case stringdataarry(2) = "editProject"
                edit_Project(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), Convert.ToInt32(stringdataarry(4)), stringdataarry(5), stringdataarry(6))

#End Region

#Region "User-Groups"

            Case stringdataarry(2) = "createGroup"

                create_Group(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4), stringdataarry(5))

            Case stringdataarry(2) = "deleteGroup"
                delete_Group(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3))

            Case stringdataarry(2) = "editGroup"
                edit_Group(stringdataarry(0), stringdataarry(1), stringdataarry(3), senderip, Convert.ToInt32(stringdataarry(4)), stringdataarry(5), stringdataarry(6))



#End Region

#Region "Music"

            Case stringdataarry(2) = "createMusic"
                create_Musicentry(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4), stringdataarry(5), stringdataarry(6), stringdataarry(7), stringdataarry(8), stringdataarry(9), stringdataarry(10))

            Case stringdataarry(2) = "editMusic"
                edit_Musicentry(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4), Convert.ToInt32(stringdataarry(5)), stringdataarry(6))

            Case stringdataarry(2) = "deleteMusic"
                delete_Musicentry(stringdataarry(0), stringdataarry(1), senderip, stringdataarry(3), stringdataarry(4))

#End Region

            Case stringdataarry(2) = "salt"

                client_sendMessage(11, 2, senderip, False, get_salt_from_username(stringdataarry(0)))

            Case stringdataarry(2) = "port"

                If arraytest_3(senderip) = True Then

                    editport(stringdataarry(0), stringdataarry(1), stringdataarry(3), senderip)

                Else

                    waitingforinput()

                End If


            Case stringdataarry(2) = "currentport"
                If senderip = Server.LocalIP Then

                    Console.WriteLine("Interface has requested the current Port")
                    client_sendMessage(3, 4, senderip, True, "")

                Else

                    Console.WriteLine("PC " & senderip & " has requested the current Port")
                    client_sendMessage(3, 3, senderip, False, "")

                End If

            Case stringdataarry(2) = "exit"
                beenden(stringdataarry(0), stringdataarry(1), senderip)

            Case stringdataarry(2) = "restart"
                server_restart(stringdataarry(0), stringdataarry(1), senderip)

            Case stringdataarry(2) = "refresh"
                client_sendMessage(19, 2, senderip, False, "")
                send_content_on_login(senderip)

            Case stringdataarry(2) = "reload"
                reload_data(stringdataarry(0), stringdataarry(1), senderip)

            Case stringdataarry(2) = "askupdate"
                If update_avi = True Then
                    client_sendMessage(31, 2, senderip, False, update_for_client(stringdataarry(3)).ToString & ";" & current_client_version)
                Else
                    client_sendMessage(31, 2, senderip, False, "False;" & current_client_version)
                End If

            Case stringdataarry(2) = "askupdateauto"
                If update_avi = True Then
                    client_sendMessage(32, 2, senderip, False, update_for_client(stringdataarry(3)).ToString & ";" & current_client_version)
                Else
                    client_sendMessage(32, 2, senderip, False, "False;" & current_client_version)
                End If

            Case stringdataarry(2) = "stopmachine"
                stop_machine(stringdataarry(0), stringdataarry(1), senderip)

            Case stringdataarry(2) = "setVersion"
                set_version(stringdataarry(0), stringdataarry(1), stringdataarry(3), senderip)
                client_sendMessage(32, 2, senderip, False, "False;" & current_client_version)

            Case stringdataarry(2) = "dataother"
                gathering_other_and_Project(stringdataarry(3), senderip)

            Case Else
                Console.WriteLine("Unrecognized command '" & DATA & "' from " & senderip)
                If Interfacelogon = True Then

                    client_sendMessage(0, 5, Server.LocalIP, True, DATA & " from " & senderip)

                End If


        End Select

        waitingforinput()

    End Sub

    Private Function arraytest_3(ip As String) As Boolean

        Try

            If stringdataarry(3) = "testvalue" Then

            End If

        Catch ex As Exception

            Console.WriteLine("PC " & ip & " has send a wrong String! Exeption: " & ex.Message)

            Return False

            Exit Function

        End Try

        Return True

    End Function

    Private Function arraytest_2(ip As String) As Boolean

        Try

            If stringdataarry(2) = "testvalue" Then

            End If

        Catch ex As Exception

            Console.WriteLine("PC " & ip & " has send a wrong String! Exeption: " & ex.Message)

            Return False

            Exit Function

        End Try

        Return True

    End Function

#Region "login - logut"

    Private Sub login(ip As String)

        'https://msdn.microsoft.com/de-de/library/det4aw50(v=vs.110).aspx
        'http://www.csharp-examples.net/dataview-rowfilter/

        Dim ausgabe As Boolean = False

        Dim abfrage As String = "UserName = '" & stringdataarry(0) & "' AND Password = '" & stringdataarry(1) & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(5).ToString = "False" Then

                Console.WriteLine("User " & foundrows(i)(1).ToString & " has tried to login, but is not enabled!")

                client_sendMessage(28, 6, ip, False, stringdataarry(0))

                If Interfacelogon = True Then

                    client_sendMessage(4, 6, Server.LocalIP, True, foundrows(i)(1).ToString)

                End If

                waitingforinput()

            End If

            client_sendMessage(12, 2, ip, False, "true;" & foundrows(i)(6).ToString)

            Console.WriteLine("User " & foundrows(i)(1).ToString & " has Linked in")

            ausgabe = True

            add_to_connected(foundrows(i)(1).ToString, ip, Convert.ToBoolean(foundrows(i)(6)))

            If Interfacelogon = True Then

                client_sendMessage(1, 2, Server.LocalIP, True, foundrows(i)(1).ToString & ";" & ip)

            End If

            System.Threading.Thread.Sleep(200)

            send_content_on_login(ip)

        Next i

        If ausgabe = False Then

            Console.WriteLine("PC " & ip & " has tried to login with Unregonized Username or Password")

            If Interfacelogon = True Then

                client_sendMessage(4, 3, ip, True, stringdataarry(1))

            End If

            client_sendMessage(12, 2, ip, False, "false")

        End If

    End Sub

    Private Sub logoff(ip As String, username As String, password As String)

        If check_all(username, password, ip) = False Then

            Console.WriteLine("Error! User loggout failed! User:" & username)

        End If

        For Each userandip As String In connectedclients

            Dim splitstring() As String = userandip.Split(CChar(";"))

            If splitstring(0) = username And splitstring(1) = ip Then

                connectedclients.Remove(userandip)

                Console.WriteLine("User " & username & " has successfully loged off")

                If Interfacelogon = True Then

                    client_sendMessage(2, 2, Server.LocalIP, True, username & ";" & ip)

                End If

                waitingforinput()

            Else

                Console.Write(username & " from " & ip & " has tried to logoff, but he wasn't logged in!")

                If Interfacelogon = True Then

                    client.SendMessage(Interfaceip, port + 1, "message;" & username & " from " & ip & " has tried to logoff, but he wasn't logged in!")

                End If

            End If

        Next


    End Sub

#End Region

#Region "User"

    Private Sub create_User(Username As String, Password As String, New_Username As String, New_Password As String, salt As String, groupID As String, enabled As String, ip As String, admin As String)

        If check_all(Username, Password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(9) = "0" Then

            Console.WriteLine("User " & Username & " has tried to create a User, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 1, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        row = userdatabase.Tables("User").NewRow
        row("UserName") = New_Username
        row("Password") = New_Password
        row("Salt") = salt
        row("GroupID") = groupID
        row("enabled") = enabled
        row("admin") = admin

        userdatabase.Tables("User").Rows.Add(row)

        groupname = groupid_to_Name(groupID)

        Console.WriteLine("User " & Username & " has created a new User! Name: " & New_Username & ", Group: " & groupname)

        If Interfacelogon = True Then

            client_sendMessage(5, 2, Server.LocalIP, True, New_Username)

        End If

        send_content_on_creation_user(New_Username, salt)
        call_admins(True, 9, Username, New_Username)

        userdatabase.WriteXml(userdatabasepath.FullName)

    End Sub

    Private Sub edit_User(Username As String, password As String, edit_Username As String, ip As String, edit_column As Integer, edit_value As String, edit_UserID As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(10) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a User, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 9, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "UserName = '" & edit_Username & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = edit_Username Then

                If foundrows(i)(0).ToString = edit_UserID Then

                    foundrows(i)(edit_column) = edit_value

                End If

            End If

        Next i

        refresh_all_data_all_clients()
        call_admins(True, 10, Username, edit_Username)

        userdatabase.WriteXml(userdatabasepath.FullName)

    End Sub

    Private Sub delete_User(Username As String, password As String, ip As String, delete_Username As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(11) = "0" Then

            Console.WriteLine("User " & Username & " has tried to delte a User, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 4, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "Username = '" & delete_Username & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            userdatabase.Tables("User").Rows.Remove(foundrows(i))

            userdatabase.WriteXml(userdatabasepath.FullName)

        Next

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(26, 2, iparray(1), False, delete_Username)

        Next

        call_admins(True, 11, Username, delete_Username)

        waitingforinput()

    End Sub

    Private Sub list_user()

        If Not userdatabase.Tables("User").Rows.Count > 0 Then

            Console.WriteLine("No Users found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10}", "ID", "Name", "Group ID", "Enabled")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10}", userdatabase.Tables("User").Rows.Item(i)(0), userdatabase.Tables("User").Rows.Item(i)(1), userdatabase.Tables("User").Rows.Item(i)(4), userdatabase.Tables("User").Rows.Item(i)(5))

            i += 1

        Loop Until i = userdatabase.Tables("User").Rows.Count


    End Sub

    Private Sub send_content_on_creation_user(username As String, salt As String)

        Dim foundrows As DataRowCollection = userdatabase.Tables("User").Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = username And foundrows(i)(3).ToString = salt) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(17, 2, iparray(1), False, foundrows(i)(0).ToString & ";" & foundrows(i)(1).ToString & ";" & foundrows(i)(4).ToString & ";" & foundrows(i)(5).ToString & ";" & foundrows(i)(6).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

#End Region

#Region "UserGroups"

    Private Sub create_Group(Username As String, Password As String, ip As String, Groupname As String, Description As String, permissions As String)

        If check_all(Username, Password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(12) = "0" Then

            Console.WriteLine("User " & Username & " has tried to create a User-Group, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 15, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        row = userdatabase.Tables("Groups").NewRow
        row("GroupName") = Groupname
        If Description <> "" Then

            row("Description") = Description

        Else

            row("Description") = ""

        End If

        row("Permissions") = permissions

        userdatabase.Tables("Groups").Rows.Add(row)

        Console.WriteLine("User " & Username & " has created a new User-Group! Name: " & Groupname)

        If Interfacelogon = True Then

            client_sendMessage(13, 2, Server.LocalIP, True, Groupname)

        End If

        send_content_on_creation_usergroup(Groupname, permissions)

        userdatabase.WriteXml(userdatabasepath.FullName)

    End Sub

    Private Sub edit_Group(Username As String, password As String, edit_Groupname As String, ip As String, edit_column As Integer, edit_value As String, edit_UserGroupID As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(13) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a Usergroup, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 16, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "GroupName = '" & edit_Groupname & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("Groups").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = edit_Groupname Then

                If foundrows(i)(0).ToString = edit_UserGroupID Then

                    foundrows(i)(edit_column) = edit_value

                End If

            End If

        Next i

        refresh_all_data_all_clients()

        userdatabase.WriteXml(userdatabasepath.FullName)

    End Sub

    Private Sub delete_Group(Username As String, password As String, ip As String, delete_Groupname As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(14) = "0" Then

            Console.WriteLine("User " & Username & " has tried to delte a User-Group, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 17, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "GroupName = '" & delete_Groupname & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("Groups").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            userdatabase.Tables("Groups").Rows.Remove(foundrows(i))

            userdatabase.WriteXml(userdatabasepath.FullName)

        Next

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(27, 2, iparray(1), False, delete_Groupname)

        Next

        waitingforinput()

    End Sub

    Private Sub list_group()

        If Not userdatabase.Tables("Groups").Rows.Count > 0 Then

            Console.WriteLine("No Groups found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-10}", "ID", "Name", "Description", "Permissions")

        Do

            Console.WriteLine("{0,-5} {1,-20} {2,-30} {3,-10}", userdatabase.Tables("Groups").Rows.Item(i)(0), userdatabase.Tables("Groups").Rows.Item(i)(1), userdatabase.Tables("Groups").Rows.Item(i)(2), userdatabase.Tables("Groups").Rows.Item(i)(3))

            i += 1

        Loop Until i = userdatabase.Tables("Groups").Rows.Count


    End Sub

    Private Sub send_content_on_creation_usergroup(usergroupname As String, permissions As String)

        Dim foundrows As DataRowCollection = userdatabase.Tables("Groups").Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = usergroupname And foundrows(i)(3).ToString = permissions) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(18, 2, iparray(1), False, foundrows(i)(0).ToString & ";" & foundrows(i)(1).ToString & ";" & foundrows(i)(2).ToString & ";" & foundrows(i)(3).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

#End Region

#Region "Category"

    Private Sub create_Category(Username As String, password As String, ip As String, categoryname As String, description As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(3) = "0" Then

            Console.WriteLine("User " & Username & " has tried to create a Category, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 5, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        row = categorydatabase.Tables(0).NewRow
        row("Name") = categoryname
        row("Description") = description

        categorydatabase.Tables(0).Rows.Add(row)

        Console.WriteLine("User " & Username & " has created a new Category! Name: " & categoryname)

        If Interfacelogon = True Then

            client_sendMessage(8, 2, Server.LocalIP, True, categoryname)

        End If

        send_content_on_creation_category(categoryname, description)
        call_admins(True, 3, Username, categoryname)

        categorydatabase.WriteXml(categorydatabasepath.FullName)

    End Sub

    Private Sub edit_Category(Username As String, password As String, ip As String, edit_Categorytname As String, edit_column As Integer, edit_value As String, edit_CategoryID As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(4) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a Category, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 12, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "Name = '" & edit_Categorytname & "'"

        Dim foundrows() As DataRow

        foundrows = categorydatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = edit_Categorytname Then

                If foundrows(i)(0).ToString = edit_CategoryID Then

                    foundrows(i)(edit_column) = edit_value

                End If

            End If

        Next i

        refresh_all_data_all_clients()
        call_admins(True, 4, Username, edit_Categorytname)

        categorydatabase.WriteXml(categorydatabasepath.FullName)

    End Sub

    Private Sub delte_category(username As String, password As String, ip As String, delete_category As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(5) = "0" Then

            Console.WriteLine("User " & username & " has tried to delte a Category, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 6, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "Name = '" & delete_category & "'"

        Dim foundrows() As DataRow

        foundrows = categorydatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            categorydatabase.Tables(0).Rows.Remove(foundrows(i))

            categorydatabase.WriteXml(categorydatabasepath.FullName)

        Next

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(24, 2, iparray(1), False, delete_category)

        Next

        call_admins(True, 5, username, delete_category)

        waitingforinput()

    End Sub

    Private Sub list_category()

        If Not categorydatabase.Tables("Category").Rows.Count > 0 Then

            Console.WriteLine("No Categorys found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-10}", "ID", "Name", "Description")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-10}", categorydatabase.Tables("Category").Rows.Item(i)(0), categorydatabase.Tables("Category").Rows.Item(i)(1), categorydatabase.Tables("Category").Rows.Item(i)(2))

            i += 1

        Loop Until i = categorydatabase.Tables("Category").Rows.Count


    End Sub

    Private Sub send_content_on_creation_category(categoryname As String, description As String)

        Dim foundrows As DataRowCollection = categorydatabase.Tables(0).Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = categoryname And foundrows(i)(2).ToString = description) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(15, 2, iparray(1), False, foundrows(i)(0).ToString & ";" & foundrows(i)(1).ToString & ";" & foundrows(i)(2).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

#End Region

#Region "Camera"

    Private Sub create_Camera(username As String, password As String, ip As String, cameraname As String, description As String)


        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(6) = "0" Then

            Console.WriteLine("User " & username & " has tried to create a Camera, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 7, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        row = camerdatabase.Tables(0).NewRow
        row("Name") = cameraname
        row("Description") = description

        camerdatabase.Tables(0).Rows.Add(row)

        Console.WriteLine("User " & username & " has created a new Category! Name: " & cameraname)

        If Interfacelogon = True Then

            client_sendMessage(9, 2, Server.LocalIP, True, cameraname)

        End If

        send_content_on_creation_camera(cameraname, description)
        call_admins(True, 6, username, cameraname)

        camerdatabase.WriteXml(cameradatabasepath.FullName)

    End Sub

    Private Sub edit_Camera(Username As String, password As String, ip As String, edit_Cameraname As String, edit_column As Integer, edit_value As String, edit_CameraID As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(7) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a Camera, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 13, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "Name = '" & edit_Cameraname & "'"

        Dim foundrows() As DataRow

        foundrows = camerdatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = edit_Cameraname Then

                If foundrows(i)(0).ToString = edit_CameraID Then

                    foundrows(i)(edit_column) = edit_value

                End If
            End If

        Next i

        refresh_all_data_all_clients()
        call_admins(True, 7, Username, edit_Cameraname)

        userdatabase.WriteXml(userdatabasepath.FullName)

    End Sub

    Private Sub delete_Camera(username As String, password As String, ip As String, delete_cameraname As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(8) = "0" Then

            Console.WriteLine("User " & username & " has tried to delte a Camera, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 8, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "Name = '" & delete_cameraname & "'"

        Dim foundrows() As DataRow

        foundrows = camerdatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            camerdatabase.Tables(0).Rows.Remove(foundrows(i))

            camerdatabase.WriteXml(cameradatabasepath.FullName)

        Next

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(25, 2, iparray(1), False, delete_cameraname)

        Next

        call_admins(True, 8, username, delete_cameraname)

        waitingforinput()

    End Sub

    Private Sub list_camera()

        If Not camerdatabase.Tables("Camera").Rows.Count > 0 Then

            Console.WriteLine("No Cameras found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-10}", "ID", "Name", "Description")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-10}", camerdatabase.Tables("Camera").Rows.Item(i)(0), camerdatabase.Tables("Camera").Rows.Item(i)(1), camerdatabase.Tables("Camera").Rows.Item(i)(2))

            i += 1

        Loop Until i = camerdatabase.Tables("Camera").Rows.Count


    End Sub

    Private Sub send_content_on_creation_camera(cameraname As String, description As String)

        Dim foundrows As DataRowCollection = camerdatabase.Tables(0).Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = cameraname And foundrows(i)(2).ToString = description) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(16, 2, iparray(1), False, foundrows(i)(0).ToString & ";" & foundrows(i)(1).ToString & ";" & foundrows(i)(2).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

#End Region

#Region "Project"

    Private Sub create_Project(username As String, password As String, ip As String, projectname As String, categoryID As String, description As String, template As Integer)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(0) = "0" Then

            Console.WriteLine("User " & username & " has tried to create a Project, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 11, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        row = projectdatabase.Tables(0).NewRow
        row("Name") = projectname
        row("Category") = categoryID
        row("Description") = description

        projectdatabase.Tables(0).Rows.Add(row)

        Console.WriteLine("User " & username & " has created a new Project! Name: " & projectname)
        call_admins(True, 0, username, projectname)

        If Interfacelogon = True Then

            client_sendMessage(10, 2, Server.LocalIP, True, projectname)

        End If

        send_content_on_creation_project(projectname, categoryID, description)

        projectdatabase.WriteXml(projectdatabasepath.FullName)

#Region "Abfrage&Ordner erstellen"

        Dim projectID As String

        Dim abfrage As String = "Name = '" & projectname & "'"

        Dim foundrows() As DataRow

        Dim folder As DirectoryInfo

        foundrows = projectdatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = projectname Then

                projectID = foundrows(i)(0).ToString
                folder = New DirectoryInfo(project_path.FullName & "\" & Format(foundrows(i)(0), "000"))

                folder.Create()

                folder.CreateSubdirectory("workfiles")

                folder.CreateSubdirectory("sourcefiles")

                folder.CreateSubdirectory("projectfiles")

                folder.CreateSubdirectory("otherdata")

                'template

                If template <> -1 Then

                    abfrage = "TemplateID = '" & template & "'"

                    foundrows = templatedatabase.Tables("template").Select(abfrage)

                    For i1 = 0 To foundrows.GetUpperBound(0)

                        Dim files As New DirectoryInfo(foundrows(i)("path").ToString)

                        For Each temp_file As FileInfo In files.GetFiles

                            If temp_file.Name <> "info.conf" Then

                                Dim ext As String() = temp_file.Name.Split(CChar("."))

                                temp_file.CopyTo(folder.FullName & "\projectfiles\" & projectname & "." & ext(1), True)

                            End If

                        Next

                    Next i1

                End If


            End If


        Next i


#End Region

    End Sub

    Private Sub edit_Project(Username As String, password As String, ip As String, edit_Projecttname As String, edit_column As Integer, edit_value As String, edit_Projectid As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(1) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a Project, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 14, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "Name = '" & edit_Projecttname & "'"

        Dim foundrows() As DataRow

        foundrows = projectdatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(1).ToString = edit_Projecttname Then

                If foundrows(i)(0).ToString = edit_Projectid Then

                    foundrows(i)(edit_column) = edit_value

                End If

            End If

        Next i

        refresh_all_data_all_clients()

        call_admins(True, 1, Username, edit_Projecttname)

        projectdatabase.WriteXml(projectdatabasepath.FullName)

    End Sub

    Private Sub delete_Project(username As String, password As String, ip As String, delete_projectaname As String, delete_projectid As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(2) = "0" Then

            Console.WriteLine("User " & username & " has tried to delte a Project, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 10, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "Name = '" & delete_projectaname & "'"

        Dim foundrows() As DataRow

        foundrows = projectdatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            If foundrows(i)(0).ToString = delete_projectid Then

                projectdatabase.Tables(0).Rows.Remove(foundrows(i))

                projectdatabase.WriteXml(projectdatabasepath.FullName)

            End If

        Next

        call_admins(True, 2, username, delete_projectaname)

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(23, 2, iparray(1), False, delete_projectaname)

        Next

        waitingforinput()

    End Sub

    Private Sub list_project()

        If Not projectdatabase.Tables("Project").Rows.Count > 0 Then

            Console.WriteLine("No Projetcs found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-10}", "ID", "Name", "Category", "Latest Clip ID", "Archived")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-10}", projectdatabase.Tables("Project").Rows.Item(i)(0), projectdatabase.Tables("Project").Rows.Item(i)(1), projectdatabase.Tables("Project").Rows.Item(i)(2), projectdatabase.Tables("Project").Rows.Item(i)(4), projectdatabase.Tables("Project").Rows.Item(i)(5))

            i += 1

        Loop Until i = projectdatabase.Tables("Project").Rows.Count

    End Sub

    Private Sub send_content_on_creation_project(projectname As String, categoryID As String, description As String)

        Dim foundrows As DataRowCollection = projectdatabase.Tables(0).Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = projectname And foundrows(i)(2).ToString = categoryID And foundrows(i)(3).ToString = description) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(14, 2, iparray(1), False, foundrows(i)(0).ToString & ";" & foundrows(i)(1).ToString & ";" & foundrows(i)(2).ToString & ";" & foundrows(i)(3).ToString & ";" & foundrows(i)(5).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

#End Region

#Region "tempaltes"

    Private Sub list_tempaltes()

        If Not templatedatabase.Tables("Template").Rows.Count > 0 Then

            Console.WriteLine("No Tempaltes found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-15}", "ID", "Name", "Resolution", "Path")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-15}", templatedatabase.Tables("Template").Rows.Item(i)(0), templatedatabase.Tables("Template").Rows.Item(i)(1), templatedatabase.Tables("Template").Rows.Item(i)(3), templatedatabase.Tables("Template").Rows.Item(i)(4))

            i += 1

        Loop Until i = templatedatabase.Tables("Template").Rows.Count



    End Sub

    Private Sub template_info(id As String)

        Dim row As DataRow

        Dim abfrage As String = "TemplateID = '" & id & "'"

        Dim foundrows() As DataRow

        foundrows = templatedatabase.Tables(0).Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            Console.WriteLine(foundrows(i)(2))

        Next i


    End Sub

#End Region

#Region "music"

    Private Sub create_Musicentry(username As String, password As String, ip As String, name As String, interpret As String, album As String, year As String, genre As String, comment As String, ext As String, time As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(15) = "0" Then

            Console.WriteLine("User " & username & " has tried to create a Music, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 18, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim albumpathpath As New DirectoryInfo(music_pool_path.FullName & "\" & interpret & "\" & album & "\")
        Dim songpath As New FileInfo(albumpathpath.FullName & name & "." & ext)

        If Not albumpathpath.Exists Then

            albumpathpath.Create()

        End If

        Dim row As DataRow

        row = musicdatabase.Tables("Music").NewRow
        row("Name") = name
        row("Interpret") = interpret
        row("Album") = album
        row("Year") = year
        row("Genre") = genre
        row("Comment") = comment
        row("Extension") = ext
        row("Time") = time
        row("Path") = songpath.FullName

        musicdatabase.Tables("Music").Rows.Add(row)

        Console.WriteLine("User " & username & " has created a new Musicentry! Title: " & name)

        If Interfacelogon = True Then

            client_sendMessage(10, 2, Server.LocalIP, True, name)

        End If

        send_content_on_creation_music(name, comment, album)
        call_admins(True, 13, username, name)

        musicdatabase.WriteXml(musicdatabasepath.FullName)

#Region "copy song"

        Threading.Thread.Sleep(500)

        Dim import As New DirectoryInfo(music_import_path.FullName)
        Dim last_writetime As Date

        For Each song As FileInfo In import.GetFiles

            last_writetime = song.LastWriteTime

            Do Until last_writetime = song.LastWriteTime

                Threading.Thread.Sleep(1000)

            Loop


            Try

                song.CopyTo(songpath.FullName, False)

            Catch ex As Exception

            End Try

            song.Delete()

            Exit For

        Next

#End Region

    End Sub

    Private Sub edit_Musicentry(Username As String, password As String, ip As String, edit_Musictitle As String, edit_MusicID As String, edit_column As Integer, edit_value As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(16) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit a Musicentry, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 14, Server.LocalIP, True, Username)

            End If

            waitingforinput()

        End If

        Dim row As DataRow

        Dim groupname As String

        Dim abfrage As String = "Name = '" & edit_Musictitle & "' and musicID = '" & edit_MusicID & "'"

        Dim foundrows() As DataRow

        foundrows = musicdatabase.Tables("Music").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            foundrows(i)(edit_column) = edit_value

        Next i

        refresh_all_data_all_clients()
        call_admins(True, 14, Username, edit_Musictitle)

        musicdatabase.WriteXml(musicdatabasepath.FullName)

    End Sub

    Private Sub delete_Musicentry(username As String, password As String, ip As String, delete_musicname As String, delete_musicID As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(17) = "0" Then

            Console.WriteLine("User " & username & " has tried to delte a Musicentry, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 10, Server.LocalIP, True, username)

            End If

            waitingforinput()

        End If

        Dim abfrage As String = "Name = '" & delete_musicname & "' and musicID = '" & delete_musicID & "'"

        Dim foundrows() As DataRow

        foundrows = musicdatabase.Tables("Music").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            Dim songfile As New FileInfo(foundrows(i)(9).ToString)

            songfile.Delete()

            musicdatabase.Tables("Music").Rows.Remove(foundrows(i))

            musicdatabase.WriteXml(musicdatabasepath.FullName)

        Next

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(36, 2, iparray(1), False, delete_musicID)

        Next

        call_admins(True, 13, username, delete_musicname)

        waitingforinput()

    End Sub

    Private Sub send_content_on_creation_music(musicentry As String, comment As String, album As String)

        'Dim abfrage As String = "Name = '" & musicentry & "' AND Comment = '" & comment & "' AND Album = '" & album & "'"

        'Dim foundrows() As DataRow = musicdatabase.Tables("Music").Select(abfrage

        Dim foundrows As DataRowCollection = musicdatabase.Tables("Music").Rows

        Dim i As Integer

        For i = 0 To foundrows.Count - 1

            If (foundrows(i)(1).ToString = musicentry And foundrows(i)(6).ToString = comment And foundrows(i)(3).ToString = album) Then

                For Each ip As String In connectedclients

                    Dim iparray() As String = ip.Split(CChar(";"))

                    client_sendMessage(35, 2, iparray(1), False, musicdatabase.Tables("Music").Rows.Item(i)(0).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(1).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(2).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(3).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(4).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(5).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(6).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(7).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(8).ToString)

                    Exit For

                Next

            End If

        Next i

    End Sub

    Private Sub list_music()

        If Not musicdatabase.Tables("Music").Rows.Count > 0 Then

            Console.WriteLine("No Musicentrys found")

            waitingforinput()

        End If

        Dim i As Integer = 0

        Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-10}", "ID", "Name", "Interpret", "Album", "Year")

        Do

            Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-10}", musicdatabase.Tables(0).Rows.Item(i)(0), musicdatabase.Tables(0).Rows.Item(i)(1), musicdatabase.Tables(0).Rows.Item(i)(2), musicdatabase.Tables(0).Rows.Item(i)(3), musicdatabase.Tables(0).Rows.Item(i)(4))

            i += 1

        Loop Until i = musicdatabase.Tables(0).Rows.Count

    End Sub


#End Region

#Region "Functions"

    Public Function groupid_to_Name(id As String) As String

        Dim abfrage As String = "GroupID = '" & id & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("Groups").Select(abfrage)

        Dim i As Integer
        Dim result As String = ""
        Dim ausgabe As Boolean = False

        For i = 0 To foundrows.GetUpperBound(0)

            result = foundrows(i)(1).ToString

            ausgabe = True

        Next i

        If ausgabe = True Then

            Return result

        Else

            Return "Groupname was not found in Database!"

        End If

    End Function

    Public Function permissions_from_Username(Username As String, ip As String) As String()

        Dim nopermissions As String = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0"

        If Username = "server" And ip = Server.LocalIP Then

            Dim PermissionsServer As String = "1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1:1"

            Return PermissionsServer.Split(CChar(":"))

        End If

        Dim groupIDabfrage As String = "UserName = '" & Username & "'"

        Dim groupID As String = "0"

        Dim found As Boolean = False

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(groupIDabfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            groupID = foundrows(i)(4).ToString

            found = True

        Next i

        If found = False Then

            Dim temp0 As String = nopermissions

            Dim temp As String() = temp0.Split(CChar(":"))

            Return temp

            Exit Function

        End If

        found = False

        Dim permissionabfrage As String = "GroupID = '" & groupID & "'"

        Dim Permissions As String = nopermissions

        foundrows = userdatabase.Tables("Groups").Select(permissionabfrage)

        For i = 0 To foundrows.GetUpperBound(0)

            Permissions = foundrows(i)(3).ToString

            found = True

        Next i

        If found = False Then

            Dim temp0 As String = nopermissions

            Dim temp As String() = temp0.Split(CChar(":"))

            Return temp

            Exit Function

        End If

        Return Permissions.Split(CChar(":"))

    End Function

    Public Function is_admin(username As String) As Boolean

        For Each row As DataRow In userdatabase.Tables("User").Rows

            If row.Item(1).ToString = username Then

                If Convert.ToBoolean(row.Item(6)) = True Then

                    Return True

                Else

                    Return False

                End If

            End If

        Next

        Return False

    End Function

    Public Function is_User_logged_in(username As String, ip As String, admin As Boolean) As Boolean

        If username = "server" And ip = Server.LocalIP Then

            Return True

        End If

        If Not connectedclients.Contains(username & ";" & ip & ";" & admin) Then

            Return False

        Else

            Return True

        End If

    End Function

    Public Function is_password_correct(username As String, password As String, ip As String) As Boolean

        If username = "server" And ip = Server.LocalIP Then

            Return True

        End If

        Dim correct As Boolean = False

        Dim abfrage As String = "UserName = '" & username & "' AND Password = '" & password & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            correct = True

        Next i

        If correct = True Then

            Return True

        Else

            Return False

        End If

    End Function

    Public Function check_all(username As String, password As String, ip As String) As Boolean

        Dim is_admin As Boolean = False

        'Dim abfrage As String = "UserName = '" & stringdataarry(0) & "' AND Password = '" & stringdataarry(1) & "'"
        Dim abfrage As String = "UserName = '" & username & "' AND Password = '" & password & "'"

        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            is_admin = Convert.ToBoolean(foundrows(i)(6))

        Next i

        If is_User_logged_in(username, ip, is_admin) = False Then

            Console.WriteLine("User " & username & " is not loggend in!")

            If Interfacelogon = True Then

                client_sendMessage(4, 2, Server.LocalIP, True, username)

            End If

            Return False

        End If

        If is_password_correct(username, password, ip) = False Then

            Console.WriteLine("User " & username & " has tried to create a User, but password hash was wrong!")

            If Interfacelogon = True Then

                client_sendMessage(4, 3, Server.LocalIP, True, username)

            End If

            Return False

        End If

        Return True

    End Function

    Public Function create_salt(password As String) As String
        Dim hashedResult As String = ""
        Dim salt As String = CreateRandowmSalt()

        Dim convertedToBytes As Byte() = Encoding.UTF8.GetBytes(password & salt)
        Dim hashType As HashAlgorithm = New SHA512Managed()
        Dim hashBytes As Byte() = hashType.ComputeHash(convertedToBytes)
        hashedResult = Convert.ToBase64String(hashBytes)

        Return "Hashed Password: " & hashedResult & vbCrLf & "Salt: " & salt

    End Function

    Public Function CreateRandowmSalt() As String

        'the following is the string that will hold the salt charachters

        Dim mix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{"

        Dim salt As String = ""

        Dim rnd As New Random

        Dim sb As StringBuilder

        For i As Integer = 1 To 100 'lengt of salt

            Dim x As Integer = rnd.Next(0, mix.Length - 1)

            salt &= (mix.Substring(x, 1))

        Next

        Return salt

    End Function

    Public Function get_salt_from_username(username As String) As String
        Dim salt As String = ""
        Dim groupIDabfrage As String = "UserName = '" & username & "'"
        Dim foundrows() As DataRow

        foundrows = userdatabase.Tables("User").Select(groupIDabfrage)

        For i = 0 To foundrows.GetUpperBound(0)

            salt = foundrows(i)(3).ToString

        Next i

        If salt = "" Then

            salt = "false"

        End If

        Return salt

    End Function

    Public Sub send_content_on_login(ip As String)

        System.Threading.Thread.Sleep(400)

        category_send(ip)

        projects_send(ip)

        camera_send(ip)

        usergroups_send(ip)

        user_send(ip)

        template_send(ip)

        templateGroup_send(ip)

        music_send(ip)

    End Sub

    Private Sub projects_send(ip As String)

        If Not projectdatabase.Tables("Project").Rows.Count > 0 Then

            client_sendMessage(14, 2, ip, False, "NO")
            Exit Sub
        End If


        Dim row As DataRow

        Dim i As Integer = 0

        Do

            client_sendMessage(14, 2, ip, False, projectdatabase.Tables("Project").Rows.Item(i)(0).ToString & ";" & projectdatabase.Tables("Project").Rows.Item(i)(1).ToString & ";" & projectdatabase.Tables("Project").Rows.Item(i)(2).ToString & ";" & projectdatabase.Tables("Project").Rows.Item(i)(3).ToString & ";" & projectdatabase.Tables("Project").Rows.Item(i)(5).ToString)

            i += 1

        Loop Until i = projectdatabase.Tables("Project").Rows.Count

    End Sub

    Private Sub category_send(ip As String)

        If Not categorydatabase.Tables(0).Rows.Count > 0 Then

            client_sendMessage(15, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim row As DataRow

        Dim i As Integer = 0

        Do

            client_sendMessage(15, 2, ip, False, categorydatabase.Tables(0).Rows.Item(i)(0).ToString & ";" & categorydatabase.Tables(0).Rows.Item(i)(1).ToString & ";" & categorydatabase.Tables(0).Rows.Item(i)(2).ToString)

            i += 1

        Loop Until i = categorydatabase.Tables(0).Rows.Count


    End Sub

    Private Sub camera_send(ip As String)

        If Not camerdatabase.Tables(0).Rows.Count > 0 Then

            client_sendMessage(16, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim row As DataRow

        Dim i As Integer = 0

        Do

            client_sendMessage(16, 2, ip, False, camerdatabase.Tables(0).Rows.Item(i)(0).ToString & ";" & camerdatabase.Tables(0).Rows.Item(i)(1).ToString & ";" & camerdatabase.Tables(0).Rows.Item(i)(2).ToString)

            i += 1

        Loop Until i = camerdatabase.Tables(0).Rows.Count


    End Sub

    Private Sub usergroups_send(ip As String)

        If Not userdatabase.Tables(1).Rows.Count > 0 Then

            client_sendMessage(18, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim row As DataRow

        Dim i As Integer = 0

        Do

            client_sendMessage(18, 2, ip, False, userdatabase.Tables(1).Rows.Item(i)(0).ToString & ";" & userdatabase.Tables(1).Rows.Item(i)(1).ToString & ";" & userdatabase.Tables(1).Rows.Item(i)(2).ToString & ";" & userdatabase.Tables(1).Rows.Item(i)(3).ToString)

            i += 1

        Loop Until i = userdatabase.Tables(1).Rows.Count

    End Sub

    Private Sub user_send(ip As String)

        If Not userdatabase.Tables(0).Rows.Count > 0 Then

            client_sendMessage(17, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim row As DataRow

        Dim i As Integer = 0

        Do

            client_sendMessage(17, 2, ip, False, userdatabase.Tables(0).Rows.Item(i)(0).ToString & ";" & userdatabase.Tables(0).Rows.Item(i)(1).ToString & ";" & userdatabase.Tables(0).Rows.Item(i)(4).ToString & ";" & userdatabase.Tables(0).Rows.Item(i)(5).ToString & ";" & userdatabase.Tables(0).Rows.Item(i)(6).ToString)

            i += 1

        Loop Until i = userdatabase.Tables(0).Rows.Count


    End Sub

    Private Sub template_send(ip As String)

        If Not templatedatabase.Tables(0).Rows.Count > 0 Then

            client_sendMessage(33, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim i As Integer = 0

        Do

            client_sendMessage(33, 2, ip, False, templatedatabase.Tables("Template").Rows.Item(i)(0).ToString & ";" & templatedatabase.Tables("Template").Rows.Item(i)(1).ToString & ";" & templatedatabase.Tables("Template").Rows.Item(i)(2).ToString & ";" & templatedatabase.Tables("Template").Rows.Item(i)(3).ToString & ";" & templatedatabase.Tables("Template").Rows.Item(i)(4).ToString & ";" & templatedatabase.Tables("Template").Rows.Item(i)(5).ToString)

            i += 1

        Loop Until i = templatedatabase.Tables("Template").Rows.Count


    End Sub

    Private Sub templateGroup_send(ip As String)

        If Not templatedatabase.Tables(0).Rows.Count > 0 Then

            client_sendMessage(34, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim i As Integer = 0

        Do

            client_sendMessage(34, 2, ip, False, templatedatabase.Tables("TemplateGroup").Rows.Item(i)(0).ToString & ";" & templatedatabase.Tables("TemplateGroup").Rows.Item(i)(1).ToString)

            i += 1

        Loop Until i = templatedatabase.Tables("TemplateGroup").Rows.Count


    End Sub

    Private Sub music_send(ip As String)

        If Not musicdatabase.Tables("Music").Rows.Count > 0 Then

            client_sendMessage(35, 2, ip, False, "NO")
            Exit Sub
        End If

        Dim i As Integer = 0

        Do

            client_sendMessage(35, 2, ip, False, musicdatabase.Tables("Music").Rows.Item(i)(0).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(1).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(2).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(3).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(4).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(5).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(6).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(7).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(8).ToString & ";" & musicdatabase.Tables("Music").Rows.Item(i)(9).ToString)

            i += 1

        Loop Until i = musicdatabase.Tables("Music").Rows.Count

    End Sub

    Private Sub reload_data(username As String, password As String, ip As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(20) = "0" Then

            Console.WriteLine("User " & username & " has tried to restart the Server, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 21, Server.LocalIP, True, username)

            End If

            Exit Sub

        End If

        userdatabase.Clear()

        projectdatabase.Clear()

        camerdatabase.Clear()

        categorydatabase.Clear()

        userdatabase.ReadXml(userdatabasepath.FullName)

        projectdatabase.ReadXml(projectdatabasepath.FullName)

        camerdatabase.ReadXml(cameradatabasepath.FullName)

        categorydatabase.ReadXml(categorydatabasepath.FullName)

    End Sub

    Private Sub beenden(username As String, password As String, ip As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(23) = "0" Then

            Console.WriteLine("User " & username & " has tried to stop the Server, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 20, Server.LocalIP, True, username)

            End If

            Exit Sub

        End If

        Server.StopServer()

        My.Settings.Save()

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(22, 2, iparray(1), False, "")

        Next

        Environment.Exit(0)

    End Sub

    Private Sub server_restart(username As String, password As String, ip As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(24) = "0" Then

            Console.WriteLine("User " & username & " has tried to restart the Server, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 19, Server.LocalIP, True, username)

            End If

            Exit Sub

        End If

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(21, 2, iparray(1), False, "")

        Next

        System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location())

        beenden("server", "", Server.LocalIP)

    End Sub

    Private Sub editport(Username As String, password As String, input As String, ip As String)

        If check_all(Username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(Username, ip)(22) = "0" Then

            Console.WriteLine("User " & Username & " has tried to edit the port of the Server, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 18, Server.LocalIP, True, Username)

            End If

            Exit Sub

        End If

        If Integer.TryParse(input, integerline) Then

            Console.WriteLine("Server is restarting")

            Server.StopServer()

            My.Settings.port = integerline

            My.Settings.Save()

            port = My.Settings.port

            Server = New ServerClass(port, True, False)

            Server.StartServer()

            Console.WriteLine()

            Console.Write("Server restarted succsesfully! Current Port is " & port)

        Else

            Console.WriteLine("Input was not a Number! Please try again!")

        End If

        waitingforinput()

    End Sub

    Private Function add_to_connected(username As String, ip As String, is_admin As Boolean) As Boolean

        Dim connected As Boolean = False
        Dim oldusernameip As String = ""

        For Each con_ip As String In connectedclients

            Dim iparray() As String = con_ip.Split(CChar(";"))

            If iparray(1) = ip Then 'corrupted

                connected = True

                oldusernameip = con_ip

            Else

            End If

        Next

        If connected = False Then

            connectedclients.Add(username & ";" & ip & ";" & is_admin)

        Else

            Console.Write("IP " & ip & " is already connected!")

            connectedclients.Remove(oldusernameip)

            connectedclients.Add(username & ";" & ip & ";" & is_admin)

        End If

    End Function

    Private Function refresh_all_data_all_clients() As Boolean

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(19, 2, iparray(1), False, "")

            send_content_on_login(iparray(1))

        Next

        Return True

    End Function

    Private Function send_to_all_Users(sendcode As Integer, contencode As Integer, Optional message As String = "") As Boolean

        For Each ip As String In connectedclients

            Dim iparray() As String = ip.Split(CChar(";"))

            client_sendMessage(sendcode, contencode, iparray(1), False, message)

        Next

    End Function

    Private Function update_for_client(version As String) As Boolean

        Dim tempveri As String() = version.Split(CChar("."))
        Dim tempvericurr As String() = current_client_version.Split(CChar("."))

        If Convert.ToInt32(tempveri(0)) < Convert.ToInt32(tempvericurr(0)) Then

            Return True

        ElseIf Convert.ToInt32(tempveri(1)) < Convert.ToInt32(tempvericurr(1)) Then

            Return True

        ElseIf Convert.ToInt32(tempveri(2)) < Convert.ToInt32(tempvericurr(2)) Then

            Return True

        Else

            Return False

        End If

    End Function

    Private Sub stop_machine(username As String, password As String, ip As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If permissions_from_Username(username, ip)(23) = "0" Then

            Console.WriteLine("User " & username & " has tried to stop the Server, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 20, Server.LocalIP, True, username)

            End If

            Exit Sub

        End If

        Server.StopServer()

        My.Settings.Save()

        For Each ip123 As String In connectedclients

            Dim iparray() As String = ip123.Split(CChar(";"))

            client_sendMessage(22, 2, iparray(1), False, "")

        Next

        cmd_process("shutdown", "-s -f -t 30", False)

        Environment.Exit(0)

    End Sub

    Private Sub set_version(username As String, password As String, version As String, ip As String)

        If check_all(username, password, ip) = False Then

            waitingforinput()

        End If

        If is_admin(username) = True Then

            Console.WriteLine("User " & username & " has tried to set the Current Version, but has no permissions to perform this action!")

            If Interfacelogon = True Then

                client_sendMessage(4, 20, Server.LocalIP, True, username)

            End If

            Exit Sub

        End If

        current_client_version = version

        My.Settings.client_update_version = version

    End Sub

#End Region

    Public Function call_admins(log As Boolean, sendcode As Integer, username As String, content As String) As Boolean

        Dim adminip As New List(Of String)()

        For Each ip As String In connectedclients

            Dim iparray() As String = ip.Split(CChar(";"))

            If Convert.ToBoolean(iparray(2)) = True Then

                adminip.Add(iparray(1))

            End If

        Next

        Dim send As String

        If log = True Then

            If sendcode = 0 Then
                send = "createProject"

            ElseIf sendcode = 1 Then
                send = "editProject"

            ElseIf sendcode = 2 Then
                send = "deleteProject"

            ElseIf sendcode = 3 Then
                send = "createCategory"

            ElseIf sendcode = 4 Then
                send = "editCategory"

            ElseIf sendcode = 5 Then
                send = "deleteCategory"

            ElseIf sendcode = 6 Then
                send = "createCamera"

            ElseIf sendcode = 7 Then
                send = "editCamera"

            ElseIf sendcode = 8 Then
                send = "deleteCamera"

            ElseIf sendcode = 9 Then
                send = "createUser"

            ElseIf sendcode = 10 Then
                send = "editUser"

            ElseIf sendcode = 11 Then
                send = "deleteUser"

            ElseIf sendcode = 12 Then
                send = "createUsergroup"

            ElseIf sendcode = 12 Then
                send = "editUsergroup"

            ElseIf sendcode = 12 Then
                send = "deleteUsergroup"

            ElseIf sendcode = 13 Then
                send = "createMusic"

            ElseIf sendcode = 14 Then
                send = "editMusic"

            ElseIf sendcode = 15 Then
                send = "deleteMusic"

            End If

        End If

        For Each admin_ip As String In adminip

            Try
                client.SendMessage(admin_ip, port, "log;" & send & ";" & username & ";" & content)
            Catch ex As Exception
                Console.WriteLine("ERROR " & ex.Message.ToString & "! Could Not send Message! IP: " & admin_ip)
            End Try

        Next

        If Not logpath.Exists Then

            logpath.Create()

        End If

        Dim write_log As Boolean = False

        Do While log <> True

            Try

                Dim sw As StreamWriter = logpath.AppendText

                sw.WriteLine(TimeString & " log;" & send & ";" & username & ";" & content)

                sw.Flush()

                sw.Close()

                write_log = True

            Catch ex As Exception

            End Try

        Loop

    End Function

    Private Sub client_sendMessage(sendcode As Integer, contentcode As Integer, ip As String, interface_client_port As Boolean, userdefinedmessage As String)

        'interface_client_port
        '= true == Interface  port + 1
        '= false == client    normal port

        Dim tempport = port
        Dim send As String = ""
        Dim content As String = ""

        If interface_client_port = True Then

            tempport = port + 1

        End If

        Select Case True

            Case sendcode = 0
                send = "message"

            Case sendcode = 1
                send = "addClient"

            Case sendcode = 2
                send = "removeClient"

            Case sendcode = 3
                send = "currentport"

            Case sendcode = 4
                send = "permissionerror"

            Case sendcode = 5
                send = "newClient"

            Case sendcode = 6
                send = "fileEncoding"

            Case sendcode = 7
                send = "fileEncoded"

            Case sendcode = 8
                send = "newCategory"

            Case sendcode = 9
                send = "newCamera"

            Case sendcode = 10
                send = "newProject"

            Case sendcode = 11
                send = "salt"

            Case sendcode = 12
                send = "logincorrect"

            Case sendcode = 13
                send = "newGroup"

            Case sendcode = 14
                send = "addProject"

            Case sendcode = 15
                send = "addCategory"

            Case sendcode = 16
                send = "addCamera"

            Case sendcode = 17
                send = "addUser"

            Case sendcode = 18
                send = "addUsergroup"

            Case sendcode = 19
                send = "clear"

            Case sendcode = 20
                send = "scan"

            Case sendcode = 21
                send = "serverrestart"

            Case sendcode = 22
                send = "serverstop"

            Case sendcode = 23
                send = "removeProject"

            Case sendcode = 24
                send = "removeCategory"

            Case sendcode = 25
                send = "removeCamera"

            Case sendcode = 26
                send = "removeUser"

            Case sendcode = 27
                send = "removeUsergroup"

            Case sendcode = 28
                send = "userdisabled"

            Case sendcode = 29
                send = "FileImport"

            Case sendcode = 30
                send = "FileEncoding"

            Case sendcode = 31
                send = "update"

            Case sendcode = 32
                send = "autoupdate"

            Case sendcode = 33
                send = "addTemplate"

            Case sendcode = 34
                send = "addTemplateGroup"

            Case sendcode = 35
                send = "addMusic"

            Case sendcode = 36
                send = "removeMusic"

            Case sendcode = 37
                send = "dataotherfiles"

            Case sendcode = 38
                send = "datafiles"

        End Select

        Select Case True

            Case contentcode = 0
                content = "Erfolgreich eingeloggt"

            Case contentcode = 1 'AUSNAHME ACHTUNG!
                For Each cclient As String In connectedclients

                    client.SendMessage(ip, tempport, send & ";" & cclient)

                Next
                Exit Sub

            Case contentcode = 2
                content = userdefinedmessage

            Case contentcode = 3
                content = port.ToString

            Case contentcode = 4
                content = (port + 1).ToString

            Case contentcode = 5
                content = userdefinedmessage

            Case contentcode = 6
                content = userdefinedmessage

            Case contentcode = 7
                content = userdefinedmessage

        End Select

        If sendcode = 4 Then

            Select Case True

                Case contentcode = 0
                    content = "0;" & userdefinedmessage

                Case contentcode = 1
                    content = "1;" & userdefinedmessage

                Case contentcode = 2
                    content = "2;" & userdefinedmessage

                Case contentcode = 3
                    content = "3;" & userdefinedmessage

                Case contentcode = 4
                    content = "4;" & userdefinedmessage

                Case contentcode = 5
                    content = "5;" & userdefinedmessage

                Case contentcode = 6
                    content = "6;" & userdefinedmessage

                Case contentcode = 7
                    content = "7;" & userdefinedmessage

                Case contentcode = 8
                    content = "8;" & userdefinedmessage

                Case contentcode = 9
                    content = "9;" & userdefinedmessage

                Case contentcode = 10
                    content = "10;" & userdefinedmessage

                Case contentcode = 11
                    content = "11;" & userdefinedmessage

                Case contentcode = 12
                    content = "12;" & userdefinedmessage

                Case contentcode = 13
                    content = "13;" & userdefinedmessage

                Case contentcode = 14
                    content = "14;" & userdefinedmessage

                Case contentcode = 15
                    content = "15;" & userdefinedmessage

                Case contentcode = 16
                    content = "16;" & userdefinedmessage

                Case contentcode = 17
                    content = "17;" & userdefinedmessage

                Case contentcode = 18
                    content = "18;" & userdefinedmessage

                Case contentcode = 19
                    content = "19;" & userdefinedmessage

                Case contentcode = 20
                    content = "20;" & userdefinedmessage

                Case contentcode = 21
                    content = "21;" & userdefinedmessage

                Case contentcode = 22
                    content = "22;" & userdefinedmessage

                Case contentcode = 23
                    content = "23;" & userdefinedmessage

            End Select

        End If

        Try

            client.SendMessage(ip, tempport, send & ";" & content)

        Catch ex As Exception

            Console.WriteLine("Error " & ex.Message.ToString & "! Could Not send Message! IP: " & ip)

        End Try

    End Sub

#Region "Console"

    Private Sub showhelp()

        Console.WriteLine()

        Console.WriteLine("help             - shows the help")
        Console.WriteLine("port             - change the current port           ussage: port [newport]")
        Console.WriteLine("ip               - shows the current IP")
        Console.WriteLine("stop             - stops the application")
        Console.WriteLine("restart          - restarts the application")
        Console.WriteLine("reload           - reload all data")
        Console.WriteLine("staticip         - use custom ip                     ussage: staticip [true/false] [IP]")
        Console.WriteLine("version          - change Client Version             ussage: version [Version]")
        Console.WriteLine("update           - activate Update function          ussage: update [true/false]")
        Console.WriteLine()

        Console.WriteLine("create project   - creates a new Project             ussage: create project [name] [category] [{description}]")
        Console.WriteLine("create category  - creates a new Category            ussage: create category [name] [{description}]")
        Console.WriteLine("create camera    - creates a new Camera              ussage: create camera [name] [{description}]")
        Console.WriteLine("create user      - creates a new User                ussage: create user [name] [password salt] [salt] [groupID] [enabled] [admin]")
        Console.WriteLine("create group     - creates a new User-Group          ussgae: create group [name] [{description}] [Permissionsarray]")
        Console.WriteLine("salt             - salt generator                    ussage: salt [password]")
        Console.WriteLine()

        Console.WriteLine("list projects    - lists the Projects                ussage: list projects")
        Console.WriteLine("list categorys   - lists the Categorys               ussage: list categorys")
        Console.WriteLine("list cameras     - lists the Cameras                 ussage: list cameras")
        Console.WriteLine("list users       - lists the Users                   ussage: list users")
        Console.WriteLine("list groups      - lists the User-Groups             ussage: list group")
        Console.WriteLine()

        Console.WriteLine("edit project     - edit a Project                    ussage: edit project [Projectname] [edit Value] [edit Column]")
        Console.WriteLine("edit category    - edit a Category                   ussage: edit category [Categoryname] [edit Value] [edit Column]")
        Console.WriteLine("edit camera      - edit a Camera                     ussage: edit camera [Cameraname] [edit Value] [edit Column]")
        Console.WriteLine("edit user        - edit a User                       ussage: edit user [Username] [edit Value] [edit Column]")
        Console.WriteLine("edit group       - edit a User-Group                 ussage: edit group [Groupname] [edit Value] [edit Column]")
        Console.WriteLine()

        Console.WriteLine("delete project   - deletes a Project                 ussage: delete project [name] [ID]")
        Console.WriteLine("delete category  - deletes a Category                ussage: delete category [name] [ID]")
        Console.WriteLine("delete camera    - deletes a Camera                  ussage: delete camera [name] [ID]")
        Console.WriteLine("delete user      - deletes a User                    ussage: delete user [name] [ID]")
        Console.WriteLine("delete group     - deletes a User-Group              ussage: delete group [name] [ID]")
        Console.WriteLine()

        Console.WriteLine("applicationpath  - set a new application Path        ussage: applicationpath [new path]")
        Console.WriteLine("projectdatabase  - set a new projectdatabase Path    ussage: projectdatabase [new path]\[projectdatabse].prfcs")
        Console.WriteLine("categorydatabase - set a new categorydatabase Path   ussage: categorydatabase [new path]\[categorydatabse].ctfcs")
        Console.WriteLine("cameradatabase   - set a new cameradatabase Path     ussage: cameradatabase [new path]\[camerdatabase].cafcs")
        Console.WriteLine("userdatabase     - set a new userdatabase Path       ussage: userdatabase [new path]\[userdatabse].usfcs")
        Console.WriteLine("importpath       - set a new import Path             ussage: importpath [new folder]")
        Console.WriteLine("encdepath        - set a new encode Path             ussage: encdepath [new folder]")
        Console.WriteLine("projectpath      - set a new project Path            ussage: projectpath [new folder]")
        Console.WriteLine("temppath         - set a new temporary Path          ussage: temppath [new folder]")

        waitingforinput()

    End Sub

    Private Sub inputcommand(input As String)

        Dim commandarray As String() = input.Split(CChar(" "))

        Select Case True

            Case input = "stop"

                beenden("server", "", Server.LocalIP)

            Case input = "help"
                showhelp()

            Case commandarray(0) = "port"
                editport("server", "", commandarray(1), Server.LocalIP)

            Case commandarray(0) = "version"
                My.Settings.client_update_version = commandarray(1)
                current_client_version = commandarray(1)

            Case commandarray(0) = "update"
                My.Settings.activate_update = Convert.ToBoolean(commandarray(1))
                update_avi = Convert.ToBoolean(commandarray(1))

            Case input = "ip"
                Console.WriteLine(Server.LocalIP)

            Case commandarray(0) = "staticip"
                My.Settings.cutom_ip_bo = Convert.ToBoolean(commandarray(1))
                My.Settings.custom_ip = commandarray(2)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "applicationpath"
                My.Settings.main_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "userdatabase"
                My.Settings.userdatabase_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "projectdatabase"
                My.Settings.projectdatabse_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "camerdatabase"
                My.Settings.cameradatabse_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "categorydatabase"
                My.Settings.categorydatabase_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "importpath"
                My.Settings.import_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "encodepath"
                My.Settings.encode_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "projectpath"
                My.Settings.project_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "temppath"
                My.Settings.temp_path = commandarray(1)
                Console.WriteLine("Application needs a restart!")

            Case commandarray(0) = "salt"
                Console.WriteLine(create_salt(commandarray(1)))

#Region "create"

            Case commandarray(0) = "create"

                Dim commandarray_3 = ""

                Try

                    If commandarray(1) = "user" Then

                        create_User("server", "", commandarray(2), commandarray(3), commandarray(4), commandarray(5), commandarray(6), Server.LocalIP, commandarray(7))

                    ElseIf commandarray(1) = "camera" Then

                        Try

                            If commandarray(3) = "" Then


                            End If

                            commandarray_3 = commandarray(3)

                        Catch ex As Exception

                            commandarray_3 = ""

                        End Try

                        create_Camera("server", "", Server.LocalIP, commandarray(2), commandarray_3)

                    ElseIf commandarray(1) = "project" Then

                        Try

                            If commandarray(4) = "" Then


                            End If

                            commandarray_3 = commandarray(4)

                        Catch ex As Exception

                            commandarray_3 = ""

                        End Try

                        create_Project("server", "", Server.LocalIP, commandarray(2), commandarray(3), commandarray(4), Convert.ToInt32(commandarray(5)))

                    ElseIf commandarray(1) = "category" Then

                        Try

                            If commandarray(3) = "" Then


                            End If

                            commandarray_3 = commandarray(3)

                        Catch ex As Exception

                            commandarray_3 = ""

                        End Try

                        create_Category("server", "", Server.LocalIP, commandarray(2), commandarray(3))

                    ElseIf commandarray(1) = "group" Then

                        Dim commandarray_4 As String = ""

                        Try

                            If commandarray(3) = "" Then


                            End If

                            commandarray_3 = commandarray(3)


                        Catch ex As Exception

                            commandarray_3 = ""

                        End Try

                        Try

                            If commandarray(4) = "" Then


                            End If

                            commandarray_4 = commandarray(4)


                        Catch ex As Exception

                            commandarray_4 = ""

                        End Try

                        create_Group("server", "", Server.LocalIP, commandarray(2), commandarray_3, commandarray_4)

                    Else

                        Console.WriteLine("Unrecognized parameter:'" & commandarray(1) & "' for create")
                        waitingforinput()

                    End If

                Catch ex As Exception

                    Console.WriteLine("No parameter for list")
                    waitingforinput()

                End Try




#End Region

#Region "edit"

            Case commandarray(0) = "edit"

                Try

                    If commandarray(1) = "user" Then

                        edit_User("server", "", commandarray(2), Server.LocalIP, Convert.ToInt32(commandarray(4)), commandarray(3), stringdataarry(4))


                    ElseIf commandarray(1) = "category" Then

                        edit_Category("server", "", Server.LocalIP, commandarray(2), Convert.ToInt32(commandarray(4)), commandarray(3), stringdataarry(4))

                    ElseIf commandarray(1) = "camera" Then

                        edit_Camera("server", "", Server.LocalIP, commandarray(2), Convert.ToInt32(commandarray(4)), commandarray(3), stringdataarry(4))

                    ElseIf commandarray(1) = "project" Then

                        edit_Project("server", "", Server.LocalIP, commandarray(2), Convert.ToInt32(commandarray(4)), commandarray(3), commandarray(5))

                    ElseIf commandarray(1) = "group" Then

                        edit_Group("server", "", Server.LocalIP, commandarray(2), Convert.ToInt32(commandarray(4)), commandarray(3), stringdataarry(4))

                    Else

                        Console.WriteLine("Unrecognized parameter:'" & commandarray(1) & "' for edit")
                        waitingforinput()

                    End If

                Catch ex As Exception

                    Console.WriteLine("No parameter for edit")
                    Console.WriteLine(ex.Message)
                    waitingforinput()

                End Try


#End Region

#Region "list"

            Case commandarray(0) = "list"

                Try

                    If commandarray(1) = "users" Then

                        list_user()

                    ElseIf commandarray(1) = "categorys" Then

                        list_category()

                    ElseIf commandarray(1) = "cameras" Then

                        list_camera()

                    ElseIf commandarray(1) = "projects" Then

                        list_project()

                    ElseIf commandarray(1) = "groups" Then

                        list_group()

                    ElseIf commandarray(1) = "templates" Then

                        list_tempaltes()

                    Else

                        Console.WriteLine("Unrecognized parameter:'" & commandarray(1) & "' for list")
                        waitingforinput()

                    End If

                Catch ex As Exception

                    Console.WriteLine("No parameter for list")
                    waitingforinput()

                End Try


#End Region

#Region "delete"

            Case commandarray(0) = "delete"

                Try

                    If commandarray(1) = "user" Then

                        delete_User("server", "", Server.LocalIP, commandarray(2))

                    ElseIf commandarray(1) = "category" Then

                        delte_category("server", "", Server.LocalIP, commandarray(2))

                    ElseIf commandarray(1) = "camera" Then

                        delete_Camera("server", "", Server.LocalIP, commandarray(2))

                    ElseIf commandarray(1) = "project" Then

                        delete_Project("server", "", Server.LocalIP, commandarray(2), commandarray(3))

                    ElseIf commandarray(1) = "group" Then

                        delete_Group("server", "", Server.LocalIP, commandarray(2))

                    Else

                        Console.WriteLine("Unrecognized parameter:'" & commandarray(1) & "' for delete")
                        waitingforinput()

                    End If

                Catch ex As Exception

                    Console.WriteLine("No parameter for delete")
                    waitingforinput()

                End Try


#End Region

            Case input = "restart"

                server_restart("server", "", Server.LocalIP)

            Case input = "reload"
                reload_data("server", "", Server.LocalIP)

            Case commandarray(0) = "info"

                template_info(commandarray(1))

            Case input = ""
                waitingforinput()

            Case Else
                Console.WriteLine("Unrecognized command:'" & input & "'")
                waitingforinput()


        End Select

        waitingforinput()

    End Sub

    Private Sub waitingforinput()

        Console.WriteLine()

        Console.Write(">")

        inputcommand(Console.ReadLine)

    End Sub

    Private Sub waiting()

        inputcommand(Console.ReadLine)

    End Sub

#End Region

#Region "TEMP TO PROJECT"

    Private Sub create_temp_to_project_watcher()

        ' Create a new FileSystemWatcher and set its properties.
        Dim encoder_watcher As New FileSystemWatcher()
        encoder_watcher.Path = encode_path.FullName

        ' Only watch mov files.
        encoder_watcher.Filter = "*.mov"

        ' Watch for changes in LastAccess and LastWrite times
        encoder_watcher.NotifyFilter = (NotifyFilters.LastWrite)

        ' Add event handlers.
        AddHandler encoder_watcher.Changed, AddressOf call_file_encoding
        AddHandler encoder_watcher.Created, AddressOf call_file_encoding

        ' Begin watching.
        encoder_watcher.EnableRaisingEvents = True

    End Sub

    Private Sub call_file_encoding(source As Object, e As FileSystemEventArgs)

        file_encoding(e.FullPath)

    End Sub

    Private Sub file_encoding(fullpath As String)

        Dim encodedfile_RAW As New FileInfo(fullpath)

        Dim typ As String() = encodedfile_RAW.Name.Split(CChar("."))

        If Interfacelogon = True Then

            client_sendMessage(6, 7, Interfaceip, True, encodedfile_RAW.Name)

        End If

        Dim encodedfile As New FileInfo(encode_path.FullName & "\" & encodedfile_RAW.Name)
        Dim sourcefile As FileInfo

        Dim tempsplit As String() = encodedfile_RAW.Name.Split(CChar("-"))

        Try
            If tempsplit(2) = "" Then


            End If
        Catch ex As Exception

            Console.WriteLine()
            Console.WriteLine("Clip Name was not in right format! Clip: " & encodedfile_RAW.Name)
            waitingforinput()

        End Try

        Dim projectID As String = tempsplit(0)
        Dim subProjectpath As New DirectoryInfo(project_path.FullName & "\" & projectID)

        Dim workPath As New DirectoryInfo(subProjectpath.FullName & "\workfiles")
        Dim sourcePath As New DirectoryInfo(subProjectpath.FullName & "\sourcefiles")

        Dim cameraID As String = tempsplit(1)
        Dim workcamerapath As New DirectoryInfo(workPath.FullName & "\" & cameraID)
        Dim sourcecamerapath As New DirectoryInfo(sourcePath.FullName & "\" & cameraID)

        Dim clipID As String = tempsplit(2)


        Dim allimportfilenames = temp_path.GetFiles("*", SearchOption.TopDirectoryOnly)
        Dim found As Boolean = False

        For Each filename As FileInfo In allimportfilenames

            Dim tempfilesplit As String() = filename.Name.Split(CChar("."))

            If tempfilesplit(0) = typ(0) Then

                sourcefile = New FileInfo(temp_path.FullName & "\" & filename.Name)
                found = True

            End If

        Next

        If found = False Then

            Console.WriteLine("Importfile " & encodedfile_RAW.Name & " was not Found!")

            waitingforinput()

        End If

        If Not subProjectpath.Exists Then

            subProjectpath.Create()

        End If

        If Not workPath.Exists Then

            workPath.Create()

        End If

        If Not sourcePath.Exists Then

            sourcePath.Create()

        End If

        If Not workcamerapath.Exists Then

            workcamerapath.Create()

        End If

        If Not sourcecamerapath.Exists Then

            sourcecamerapath.Create()

        End If

        Dim sourceClipID As String() = clipID.Split(CChar("."))

        send_to_all_Users(29, 7, Format(clipID, "000") & "." & tempsplit(1) & ":" & tempsplit(0))

        move_to_project(encodedfile_RAW, sourcefile, workcamerapath.FullName & "\" & clipID, sourcecamerapath.FullName & "\" & sourceClipID(0) & sourcefile.Extension)


    End Sub

    Private Sub move_to_project(encodedfile As FileInfo, sourcefile As FileInfo, encodeddest As String, sourcedest As String)

        Do

            System.Threading.Thread.Sleep(3000)

            Try
                encodedfile.MoveTo(encodeddest)

                sourcefile.MoveTo(sourcedest)

                Exit Do

            Catch ex As Exception

            End Try

        Loop

        If Interfacelogon = True Then

            client_sendMessage(7, 7, Interfaceip, True, sourcefile.Name)

        End If

        System.Threading.Thread.Sleep(5000)

        If check_for_encode_files() = True Then

            Dim remaingfiles = encode_path.GetFiles("*", SearchOption.TopDirectoryOnly)

            file_encoding(remaingfiles(0).FullName)

        End If

        waiting()

    End Sub

    Private Function check_for_encode_files() As Boolean

        Dim remaingfiles = encode_path.GetFiles("*.*", SearchOption.TopDirectoryOnly)

        If remaingfiles.Count > 0 Then

            Return True

        End If

        Return False

    End Function

#End Region

#Region "IMPORT TO TEMP AND ORG-PROJECT AND OTHER"

    Private Sub create_import_to_temp_watcher()

        ' Create a new FileSystemWatcher and set its properties.
        Dim import_watcher As New FileSystemWatcher()
        import_watcher.Path = import_path.FullName
        import_watcher.IncludeSubdirectories = False

        ' Watch for changes in LastAccess and LastWrite times
        'import_watcher.NotifyFilter = (NotifyFilters.LastWrite)
        import_watcher.NotifyFilter = (NotifyFilters.Size)

        ' Add event handlers.
        AddHandler import_watcher.Changed, AddressOf call_move_to_temp
        AddHandler import_watcher.Created, AddressOf call_move_to_temp

        ' Begin watching.
        import_watcher.EnableRaisingEvents = True


        'OTHER DATA

        ' Create a new FileSystemWatcher and set its properties.
        Dim import_other_watcher As New FileSystemWatcher()
        import_other_watcher.Path = import_other.FullName

        ' Watch for changes in LastAccess and LastWrite times
        import_other_watcher.NotifyFilter = (NotifyFilters.LastWrite)
        import_other_watcher.NotifyFilter = (NotifyFilters.Size)

        ' Add event handlers.
        AddHandler import_other_watcher.Changed, AddressOf call_move_to_other
        AddHandler import_other_watcher.Created, AddressOf call_move_to_other

        ' Begin watching.
        import_other_watcher.EnableRaisingEvents = True

    End Sub

    Private Sub call_move_to_temp(source As Object, e As FileSystemEventArgs)

        move_to_temp(e.FullPath)

    End Sub

    Private Sub call_move_to_other(source As Object, e As FileSystemEventArgs)

        move_to_other(e.FullPath)

    End Sub

    Private Sub move_to_temp(fullpath As String)

        Dim moviefile As New FileInfo(fullpath)

        Dim clipid As Integer = 0

        Dim splittemp As String() = moviefile.Name.Split(CChar("."))

        Dim splittemp2 As String() = splittemp(0).Split(CChar("-"))

        Dim abfrage As String = "ProjectID = '" & splittemp2(0) & "'"

        Dim foundrows() As DataRow

        foundrows = projectdatabase.Tables("Project").Select(abfrage)

        Dim i As Integer

        For i = 0 To foundrows.GetUpperBound(0)

            clipid = Convert.ToInt32(foundrows(i)(4))

            clipid = clipid + 1

            foundrows(i)(4) = Format(clipid, "000")

            projectdatabase.WriteXml(projectdatabasepath.FullName)

        Next i

        Dim clipname As String = splittemp2(0) & "-" & splittemp2(1) & "-" & Format(clipid, "000") & "." & splittemp(1)

        send_to_all_Users(29, 7, Format(clipid, "000") & "." & splittemp(1) & ":" & splittemp2(0))

        Dim errorcounter As Int32 = 0
        Dim destfile As String = temp_path.FullName & "\" & clipname

        If File.Exists(destfile) Then
            File.Delete(destfile)
        End If

        Do

            Try

                File.Move(moviefile.FullName, temp_path.FullName & "\" & clipname)

                Exit Do

            Catch ex As Exception

                errorcounter += 1

                If errorcounter <= 10 Then
                    Console.WriteLine(ex.Message)
                    Exit Do
                End If

            End Try

            System.Threading.Thread.Sleep(1000)

        Loop

        If check_for_import_files() = True Then

            Dim remaingfiles = import_path.GetFiles("*.*", SearchOption.TopDirectoryOnly)

            move_to_temp(remaingfiles(0).FullName)

        End If

        waiting()

    End Sub

    Private Sub move_to_other(fullpath As String)

        Dim datafile As New FileInfo(fullpath)

        Dim otherid As Integer = 0

        Dim splittemp As String() = datafile.Name.Split(CChar("."))         'split the extension away

        Dim splittemp2 As String() = splittemp(0).Split(CChar("-"))         'split the data in the name

        Dim abfrage As String = "ProjectID = '" & splittemp2(0) & "'"

        Dim foundrows() As DataRow

        foundrows = projectdatabase.Tables("Project").Select(abfrage)

        Dim i As Integer
        Dim projectidtemp As String

        For i = 0 To foundrows.GetUpperBound(0)

            otherid = Convert.ToInt32(foundrows(i)(6))

            otherid = otherid + 1

            foundrows(i)(6) = Format(otherid, "000")

            projectdatabase.WriteXml(projectdatabasepath.FullName)

            projectidtemp = Format(foundrows(i)(0), "000")

        Next i

        Dim clipname As String = splittemp2(0) & "-" & splittemp2(1) & "-" & Format(otherid, "000") & "." & splittemp(1)

        send_to_all_Users(29, 7, Format(otherid, "000") & "." & splittemp(1) & ":" & splittemp2(0))

        Dim errorcounter As Int32 = 0
        Dim destfile As String = project_path.FullName & "\" & projectidtemp & "\otherdata\" & clipname

        If File.Exists(destfile) Then
            File.Delete(destfile)
        End If

        Do

            Try

                File.Move(datafile.FullName, destfile)


                Exit Do

            Catch ex As Exception

                errorcounter += 1

                If errorcounter <= 10 Then
                    Console.WriteLine(ex.Message)
                    Exit Do
                End If

            End Try

            System.Threading.Thread.Sleep(1000)

        Loop

        If check_for_import_files_other() = True Then

            Dim remaingfiles = import_other.GetFiles("*.*", SearchOption.TopDirectoryOnly)
            move_to_other(remaingfiles(0).FullName.ToString)

        End If

    End Sub

    Private Function check_for_import_files() As Boolean

        Dim remaingfiles = import_path.GetFiles("*.*", SearchOption.TopDirectoryOnly)

        If remaingfiles.Count > 0 Then

            Return True

        End If

        Return False

    End Function

    Private Function check_for_import_files_other() As Boolean

        Dim remaingfiles = import_other.GetFiles("*.*", SearchOption.TopDirectoryOnly)

        If remaingfiles.Count > 0 Then

            Return True

        End If

        Return False

    End Function

#End Region

#Region "Project data"

    Private Sub gathering_other_and_Project(id As String, ip As String)

        'Dim ProjectIDquery As String = "ID = '" & id & "'"

        'Dim foundrows() As DataRow

        'foundrows = userdatabase.Tables("Project").Select(ProjectIDquery)

        'Dim i As Integer

        'For i = 0 To foundrows.GetUpperBound(0)

        '    groupID = foundrows(i)(4).ToString

        'Next i

        Dim dataother As New DirectoryInfo(project_path.FullName & "\" & id & "\otherdata")

        Dim remaingfiles = dataother.GetFiles("*.*", SearchOption.TopDirectoryOnly)

        If remaingfiles.Count > 0 Then

            Dim dataotherfiles As String = ""


            For Each file In dataother.GetFiles

                If remaingfiles.Count = 1 Then

                    dataotherfiles = file.Name
                    Exit For

                End If

                dataotherfiles = file.Name & "°" & dataotherfiles

            Next

            client_sendMessage(37, 6, ip, False, dataotherfiles)

        Else

            client_sendMessage(37, 6, ip, False, "empty")

        End If

        Dim datafiles As New DirectoryInfo(project_path.FullName & "\" & id & "\workfiles")

        Dim remaingfiles1 = datafiles.GetFiles("*.*", SearchOption.TopDirectoryOnly)

        If remaingfiles1.Count > 0 Then

            Dim datafiles1 As String = ""


            For Each file In datafiles.GetFiles

                If remaingfiles1.Count = 1 Then

                    datafiles1 = file.Name
                    Exit For

                End If

                datafiles1 = file.Name & "°" & datafiles1

            Next



            client_sendMessage(38, 6, ip, False, datafiles1)

        Else

            client_sendMessage(38, 6, ip, False, "empty")

        End If

    End Sub

#End Region

#Region "commandline"

    Private Sub cmd_process(command As String, arguments As String, permanent As Boolean)

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
        pi.FileName = "cmd.exe"
        pi.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo = pi
        p.Start()

    End Sub

#End Region

End Module