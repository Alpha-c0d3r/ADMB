Imports System.IO
Imports System.Net
Imports Discord
Imports Discord.Addons.Interactive
Imports Discord.Commands
Imports Discord.WebSocket
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Windows.Forms.Timer
Imports System.Timers
Imports System.Globalization
Public Class Ark
    Inherits InteractiveBase
    <Command("tgen", RunMode:=RunMode.Async)>
    <Summary("Calculates tek generator usage, usage: (prefix)tgen [element or shard amount] [shard or element] [radius]")>
    Public Async Function tgen(ByVal number As Double, ByVal type As String, ByVal radius As String) As Task
        Try
            Dim elenumber As Double = Nothing
            Dim check As Boolean = False
            Dim radx As Double = radius.Replace("x", Nothing)
            If type.ToLower.StartsWith("ele") Then
                elenumber = number
                check = True
            ElseIf type.ToLower.StartsWith("shard") Then

                elenumber = number / 100
                check = True
            End If
            If Not check = False Then
                Dim rate As Double = 1
                Dim base As Double = 1 + (radx - 1) * 0.33
                Dim hourrate As Double = 18 / base
                Dim willlast As Double = ((elenumber * hourrate) * rate)
                Dim allseconds As Double = willlast * 3600
                Dim ts As TimeSpan = TimeSpan.FromSeconds(allseconds)

                Dim ebd As New EmbedBuilder With {
                                              .Description = $"It will last for {ts.Days.ToString} days & {ts.Hours.ToString} hours & {ts.Minutes.ToString} minutes & {ts.Seconds.ToString} seconds"
                                               }
                ebd.Color = Color.Green
                Await Context.Channel.SendMessageAsync(Context.Message.Author.Mention, False, ebd.Build)
            Else
                Dim ebd As New EmbedBuilder With {
                                             .Description = $"Please recheck your command usage"
                                              }
                ebd.Color = Color.Red
                Await Context.Channel.SendMessageAsync(Context.Message.Author.Mention, False, ebd.Build)
            End If

        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
    <Command("pin", RunMode:=RunMode.Async)>
    <Summary("Generates a random 4 digit pin code *only works via direct message* usage, usage: (prefix)pin")>
    Public Async Function pin() As Task
        Try
            Await ReplyAsync(Context.Message.Author.Mention & $" You can use this command only via direct messaging me")

        Catch ex As Exception

        End Try

    End Function
    <Command("egen", RunMode:=RunMode.Async)>
    <Summary("Calculates electric generator usage, usage: (prefix)egen [gas amount]")>
    Public Async Function egen(ByVal gasamount As Double) As Task
        Try

            Dim rate As Double = 1
            Dim base As Double = gasamount * 60
            Dim willlast As Double = (base * rate)
            Dim seconds As Double = willlast * 60
            Dim ts As TimeSpan = TimeSpan.FromSeconds(seconds)

            Dim ebd As New EmbedBuilder With {
                                              .Description = $"It will last for {ts.Days.ToString} days & {ts.Hours.ToString} hours & {ts.Minutes.ToString} minutes & {ts.Seconds.ToString} seconds"
                                               }
                ebd.Color = Color.Green
                Await Context.Channel.SendMessageAsync(Context.Message.Author.Mention, False, ebd.Build)


        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
End Class
