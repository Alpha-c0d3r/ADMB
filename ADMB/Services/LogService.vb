Imports System.Net
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket

Public Class LogService
    Private ReadOnly Client As DiscordSocketClient
    Public Sub New(client As DiscordSocketClient, commands As CommandService)
        AddHandler client.Log, AddressOf ClientLog
        AddHandler commands.CommandExecuted, AddressOf CommandLog
        'AddHandler client.JoinedGuild, AddressOf joinedguild
        'AddHandler client.ShardLatencyUpdated, AddressOf ShardLatencyUpdatedAsync
        'AddHandler client.ShardDisconnected, AddressOf ShardDisconnectedAsync
    End Sub
    Private Async Function ShardDisconnectedAsync(ByVal exception As Exception, ByVal shard As DiscordSocketClient) As Task

        Try
            Await ClientLog(Nothing, $"Shard: `{shard.ShardId}` Disconnected with the reason {exception.Message}")
        Catch e1 As Exception
        End Try
    End Function

    Private Async Function ShardLatencyUpdatedAsync(ByVal oldPing As Integer, ByVal updatePing As Integer, ByVal shard As DiscordSocketClient) As Task
        If updatePing < 500 AndAlso oldPing < 500 Then
            Return
        End If
        Try
            Await ClientLog(Nothing, $"Shard: `{shard.ShardId}` Latency update from **{oldPing}** ms to **{updatePing}** ms")
        Catch e1 As Exception
        End Try
    End Function
    Public Shared Function ClientLog(ByVal message As LogMessage, Optional ByVal text As String = "None") As Task
        If text = "None" Then
            Console.WriteLine(message.ToString)
            writetolog(message.ToString)
            Tl("1340337612:AAF4jqdHtJIyxeOTLTeBWm-j41YJ_8twM5c", 338115971, Date.Now & " | " & message.ToString)
            Return Task.CompletedTask
        Else
            Console.WriteLine(Date.Now & " | " & text)
            writetolog(Date.Now & " | " & text)
            Tl("1340337612:AAF4jqdHtJIyxeOTLTeBWm-j41YJ_8twM5c", 338115971, Date.Now & " | " & text)
            Return Task.CompletedTask
        End If

    End Function
    Public Shared Sub writetolog(ByVal text As String)
        Dim location As String = System.AppDomain.CurrentDomain.BaseDirectory
        Dim fullpath As String = System.IO.Path.Combine(location, "logs.log")
        Try
            IO.File.AppendAllText(System.IO.Path.Combine(location, "logs.log"), Date.Now & " | " & text & System.Environment.NewLine)
        Catch ex As Exception
        End Try

    End Sub
    Shared Sub Tl(ByVal Tok As String, ByVal CID As String, ByVal msg As String)
        Try
            Dim wreq As WebRequest
            wreq = WebRequest.Create("https://api.telegram.org/bot" & Tok & "/sendMessage?chat_id=" & CID & "&text=" & msg.Replace("#", "%23"))
            wreq.Timeout = 1500
            wreq.GetResponse()
        Catch ex As Exception

        End Try
    End Sub

    Private Function CommandLog(ByVal info As [Optional](Of CommandInfo),
                                 ByVal context As ICommandContext,
                                 ByVal result As IResult) As Task
        If result.IsSuccess Then Return Task.CompletedTask

        Select Case result.Error.Value
            Case CommandError.UnknownCommand
                Return Task.CompletedTask
            Case CommandError.ParseFailed
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
            Case CommandError.BadArgCount
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
                Console.ForegroundColor = ConsoleColor.Green
            Case CommandError.ObjectNotFound
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
                Console.ForegroundColor = ConsoleColor.Green
            Case CommandError.MultipleMatches
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
                Console.ForegroundColor = ConsoleColor.Green
            Case CommandError.UnmetPrecondition
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
            Case CommandError.Exception
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
            Case CommandError.Unsuccessful
                Console.WriteLine(context.Guild.Name & " | " & result.ToString)
            Case Else

        End Select

        Return Task.CompletedTask
    End Function

End Class