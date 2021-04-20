Imports System.IO
Imports System.Net
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports Newtonsoft.Json.Linq

Public Class Utility
    Inherits ModuleBase
    <Command("check")>
    <Summary("Gets details about an ark battlemetrics server usage: (prefix)check [battlemetrics link or server id]")>
    Public Function check(ByVal text As String) As Task

        Dim kk As String = ""
        Dim newa As String = ""

        If text.Contains("https://www.battlemetrics.com/servers/ark/") Then
            Dim clean As String = text.Replace("https://www.battlemetrics.com/servers/ark/", "")
            clean = clean.Replace("/", "")
            newa = "https://api.battlemetrics.com/servers/" & clean
            kk = clean
        Else
            newa = "https://api.battlemetrics.com/servers/" & text
            kk = text
        End If

        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(newa), HttpWebRequest)
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0"
            request.Timeout = 5000
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim o As JObject = JObject.Parse(reader.ReadToEnd)
            Dim name As String = o("data")("attributes")("name").ToString()
            Dim ip As String = o("data")("attributes")("ip").ToString()
            Dim port As String = o("data")("attributes")("port").ToString()
            Dim portq As String = o("data")("attributes")("portQuery").ToString()
            Dim players As String = o("data")("attributes")("players").ToString()
            Dim maxplayers As String = o("data")("attributes")("maxPlayers").ToString()
            Dim rank As String = o("data")("attributes")("rank").ToString()
            Dim country As String = o("data")("attributes")("country").ToString()
            Dim map As String = o("data")("attributes")("details")("map").ToString()
            Dim offi As String = o("data")("attributes")("details")("official").ToString()
            Dim pve As String = o("data")("attributes")("details")("pve").ToString()
            Dim modded As String = o("data")("attributes")("details")("modded").ToString()
            Dim crossplay As String = o("data")("attributes")("details")("crossplay").ToString()
            Dim svsteamid As String = o("data")("attributes")("details")("serverSteamId").ToString()
            Dim status As String = o("data")("attributes")("status").ToString()
            Dim eb As New EmbedBuilder With {
                    .Title = name,
                    .Description = "Check is a command to get info about a specific ark server"
                     }
            eb.WithCurrentTimestamp()
            If status = "online" Then
                eb.Color = Color.Green
            Else
                eb.Color = Color.Red
            End If

            eb.WithAuthor("Server Info", Context.User.GetDefaultAvatarUrl)
            eb.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
            eb.WithFooter("Server Status: " & status & " | Requested By: " & Context.User.Username & "#" & Context.User.Discriminator)
            eb.AddField("IP", ip, True)
            eb.AddField("Port", port, True)
            eb.AddField("Port Query", portq, True)
            eb.AddField("Players", players, True)
            eb.AddField("Max Players", maxplayers, True)
            eb.AddField("Rank", rank, True)
            eb.AddField("Map", map, True)
            eb.AddField("Official", offi, True)
            eb.AddField("PVE", pve, True)
            eb.AddField("Modded", modded, True)
            eb.AddField("Cross Play", crossplay, True)
            eb.AddField("Server Steam ID", svsteamid, True)
            eb.AddField("Country", country, True)
            eb.AddField("BattleMetrics URL", $"[Click here to open the Battlemetrics link]({"https://www.battlemetrics.com/servers/ark/" & kk})", True)
            Return Context.Channel.SendMessageAsync("", False, eb.Build())



        Catch ex As Exception
            Return ReplyAsync("I couldn't get the info for that server, please check again.")
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    <Command("steamid")>
    <Summary("Gets you the player steam id usage: (prefix)steamid [player profile link]")>
    Public Function steamid(ByVal text As String) As Task
        Dim last As String = ""
        Dim newa As String = ""

        If text.Contains("https://steamcommunity.com/id/") Then
                Dim clean As String = text.Replace("https://steamcommunity.com/id/", "")
                clean = clean.Replace("/", "")
                last = clean
                newa = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=E53CD8C82962E05CF09C2A7AF18B90F8&vanityurl=" & clean

            Else
                last = text
                newa = "http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=E53CD8C82962E05CF09C2A7AF18B90F8&vanityurl=" & text
            End If

        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(newa), HttpWebRequest)
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0"
            request.Timeout = 5000
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim o As JObject = JObject.Parse(reader.ReadToEnd)

            Dim check As String = o("response")("success").ToString()

            If check = "1" Then
                Dim steamidd As String = o("response")("steamid").ToString()
                Dim eb As New EmbedBuilder With {
                        .Title = "Steam ID Extractor",
                        .Description = "SteamID is a command to get steam id of a specific player" & vbNewLine & vbNewLine & "Steam ID For " & last & " is: `" & steamidd & "`"
                         }
                eb.WithCurrentTimestamp()
                eb.Color = Color.Green
                '.ImageUrl = "",
                eb.WithAuthor("Steam ID Extractor", Context.User.GetDefaultAvatarUrl)
                eb.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
                eb.WithFooter("Status: " & "Success" & " | Requested By: " & Context.User.Username & "#" & Context.User.Discriminator) 'change it
                Return Context.Channel.SendMessageAsync("", False, eb.Build())
            Else
                Dim eb As New EmbedBuilder With {
                       .Title = "Steam ID Finder",
                       .Description = "SteamID is a command to get steam id of a specific player" & vbNewLine & vbNewLine & "Steam ID For  " & last & " couldn't get extracted, maybe user doesnt exist?"
                        }
                eb.WithCurrentTimestamp()
                '.ImageUrl = "",
                eb.Color = Color.Red

                eb.WithAuthor("Steam ID Extractor", Context.User.GetDefaultAvatarUrl)
                eb.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
                eb.WithFooter("Status: " & "Failed" & " | Requested By: " & Context.User.Username)
                Return Context.Channel.SendMessageAsync("", False, eb.Build())
            End If


        Catch ex As Exception
            Return ReplyAsync("I couldn't get the player steam id, please check again.")
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
        Return Nothing
    End Function
    Private last As String = Nothing
    Private newa As String = Nothing
    <Command("checksp")>
    <Summary("Gets you details about a steam player profile usage: (prefix)checksp [player steam id or profile link]")>
    Public Function checksteampro(ByVal text As String) As Task



        If text.Contains("https://steamcommunity.com/id/") Then
                Dim clean As String = text.Replace("https://steamcommunity.com/id/", "")
                clean = clean.Replace("/", "")
                last = clean
                newa = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=E53CD8C82962E05CF09C2A7AF18B90F8&steamids=" & last

            Else
                last = text
                newa = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=E53CD8C82962E05CF09C2A7AF18B90F8&steamids=" & last
            End If

        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(newa), HttpWebRequest)
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0"
            request.Timeout = 5000
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim rawJson = reader.ReadToEnd

            PLayero = JsonHelper.ToClass(Of Playersss)(rawJson)

            Dim eb As New EmbedBuilder With {
                    .Title = "Steam Profile Checker",
                    .Description = "Steam Profile Checker is a command to get steam profile details of a specific player"
                     }
            eb.WithCurrentTimestamp()
            eb.Color = Color.Green
            eb.AddField("Profile URL", $"[Click here to open the profile link]({Player.Profileurl})", True)

            Dim nTimestamp As Double = Player.Timecreated
            Dim nDateTime As System.DateTime = New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
            nDateTime = nDateTime.AddSeconds(nTimestamp)
            eb.AddField("Account Creation Date", nDateTime, True)
            eb.WithAuthor("Steam Profile Checker", Player.Avatarfull)
            eb.WithThumbnailUrl(Player.Avatarfull)
            eb.WithFooter("SteamID: " & Player.Steamid & " | Requested By: " & Context.User.Username)
            Return Context.Channel.SendMessageAsync("", False, eb.Build())



        Catch ex As Exception
            Return ReplyAsync("I couldn't get the player steam profile details, please check again.")
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
        Return Nothing
    End Function

End Class