Imports System.Reflection
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Text

Module KKK
    Public Const UNKNOWN_COMMAND As String = "❓"
    Public Const True_Command As String = "✅"
    Public Const NOT_ALLOWED As String = "🚫"
    Public Property PLayero() As Playersss
    Public development As Boolean = False
    Public prefix As String = "$"
    Public devY As Long = "622876870085705748"
    Public devL As Long = "717780921063047239"
    Public devS As Long = "709877784335351861"
    Public LocalsServer As Long = "605949631771312147"
    Public Rng As Random
    Public time As String
    Public listo As SortedList(Of Long, Boolean) = New SortedList(Of Long, Boolean)
    Public quizanswers As SortedList(Of Long, String) = New SortedList(Of Long, String)
    Public banlist As New List(Of Long)
    Public banlistwithreason As SortedList(Of Long, String) = New SortedList(Of Long, String)
    Public ordersexpire As SortedList(Of Long, String) = New SortedList(Of Long, String)
    Public paginatedpages As SortedList(Of String, String) = New SortedList(Of String, String)
    Public channelidssss As New List(Of Long)
    Public quizstatus As Boolean = False
    Public servero As New Server
    Public CommandService As CommandService
    Public globalchat As SortedList(Of Long, Long) = New SortedList(Of Long, Long)
    Public topmoney As SortedList(Of Long, Double) = New SortedList(Of Long, Double)
End Module

Public Class CommandHandler
    Private ReadOnly Client As DiscordSocketClient
    Private ReadOnly Services As IServiceProvider

    Public Sub New(ByVal client As DiscordSocketClient, ByVal commandService As CommandService, ByVal services As IServiceProvider)
        Me.Client = client
        KKK.CommandService = commandService
        Me.Services = services
    End Sub

    Public Async Function InitializeAsync() As Task
        Await CommandService.AddModulesAsync(Assembly.GetEntryAssembly, Services)
        AddHandler Client.Ready, AddressOf ready
        AddHandler Client.UserJoined, AddressOf welcome
        AddHandler Client.MessageReceived, AddressOf HandleCommandAsync
        AddHandler Client.MessagesBulkDeleted, AddressOf BulkDeleteAsync
        AddHandler Client.ReactionAdded, AddressOf HandleReactionAddedAsync
        AddHandler Client.JoinedGuild, AddressOf joinedguild
    End Function
    Private bTimer As System.Timers.Timer
    Private expiretimer As System.Timers.Timer
    Private dwazdah As Boolean = False
    Private yaks As Boolean = False
    Private sed As Boolean = False
    Private yakd As Boolean = False
    Private das As Boolean = False
    Private stoppp As Boolean = False
    Private expire As Boolean = False
    Public once As Boolean = False
    Private Async Function joinedguild(ByVal guild As SocketGuild) As Task
        Try
            Dim chn22 As IMessageChannel = Client.GetChannel(709874986466410596)
            Dim ll = Await guild.DefaultChannel.CreateInviteAsync(0, Nothing, False, False, Nothing)
            Dim text As String = $"Just joined {guild.Name} server, they have {guild.MemberCount} members and {guild.Channels.Count} channels, and the owner is {guild.Owner.Username}#{guild.Owner.Discriminator}, [Join]({ll.Url})"
            LogService.writetolog(text)
            Dim ebddd22 As New EmbedBuilder With {
            .Description = text
           }
            ebddd22.Color = Color.Green
            Await chn22.SendMessageAsync(Nothing, False, ebddd22.Build)
        Catch ex As Exception
            LogService.ClientLog(Nothing, $"{ex.ToString}")
        End Try

    End Function
    Public Async Function HandleReactionAddedAsync(ByVal cachedMessage As Cacheable(Of IUserMessage, ULong), ByVal originChannel As ISocketMessageChannel, ByVal reaction As SocketReaction) As Task
        Dim k = Task.Run(Async Function()
                             Try
                                 If Not reaction.User.IsSpecified Then Return Nothing
                                 'Dim message = Await cachedMessage.GetOrDownloadAsync()
                                 If reaction.Emote.Name.Equals("✅") Then
                                     Dim chn As SocketGuildChannel = Client.GetChannel(reaction.Channel.Id)
                                     Dim channel As IMessageChannel = Client.GetChannel(reaction.Channel.Id)
                                     If servero.Checktrack(chn.Guild.Id) = True Then
                                         Dim msgg As IUserMessage = Await channel.GetMessageAsync(reaction.MessageId)
                                         Dim OrderAuthor As IUser = Client.GetUser(servero.GetCustomer(msgg.Id, 2))
                                         If servero.Checkiforderexist(chn.Guild.Id, OrderAuthor.Id) = True Then

                                             Dim userforpermission As SocketUser = chn.Guild.GetUser(reaction.UserId)
                                             If msgg.Author.IsBot AndAlso msgg.Author.Username = Client.CurrentUser.Username Then
                                                 Dim permission = TryCast(userforpermission, SocketGuildUser)
                                                 If permission.GuildPermissions.ManageMessages = True Then

                                                     Dim sendchannel As IMessageChannel = Client.GetChannel(servero.getordersettings(chn.Guild.Id, 4))




                                                     If servero.getsubmitchan(chn.Guild.Id) = chn.Id Then
                                                         If Not servero.Checkifhaveprevious(chn.Guild.Id, OrderAuthor.Id) = True Then
                                                             servero.Insertordertrack(chn.Guild.Id, OrderAuthor.Id)
                                                         End If




                                                         Dim number As Integer = servero.Getcutomersorder(chn.Guild.Id, OrderAuthor.Id)
                                                         number += 1

                                                         Await sendchannel.SendMessageAsync($"Order id: `{msgg.Id}` Order list: `{servero.GetCustomer(msgg.Id, 1)}` by: `{OrderAuthor.Username}#{OrderAuthor.Discriminator}` has been marked as completed by: {chn.GetUser(reaction.UserId).Mention}, This customer have `{number}` completed orders so far!", False)
                                                         servero.Updatecustomerorders(chn.Guild.Id, OrderAuthor.Id, number, OrderAuthor.Username & "#" & OrderAuthor.Discriminator, chn.GetUser(reaction.UserId).Username & "#" & chn.GetUser(reaction.UserId).Discriminator, chn.GetUser(reaction.UserId).Id, msgg.Id, servero.GetCustomer(msgg.Id, 1))
                                                         servero.Deleteuserorder(msgg.Id)
                                                     End If
                                                 End If
                                             End If
                                         End If
                                     End If
                                 ElseIf reaction.Emote.Name.Equals("❎") Then
                                     Dim chn As SocketGuildChannel = Client.GetChannel(reaction.Channel.Id)
                                     Dim channel As IMessageChannel = Client.GetChannel(reaction.Channel.Id)
                                     If servero.Checktrack(chn.Guild.Id) = True Then
                                         Dim msgg As IUserMessage = Await channel.GetMessageAsync(reaction.MessageId)
                                         Dim OrderAuthor As IUser = Client.GetUser(servero.GetCustomer(msgg.Id, 2))
                                         If servero.Checkiforderexist(chn.Guild.Id, OrderAuthor.Id) = True Then

                                             Dim userforpermission As SocketUser = chn.Guild.GetUser(reaction.UserId)
                                             If msgg.Author.IsBot AndAlso msgg.Author.Username = Client.CurrentUser.Username Then
                                                 Dim permission = TryCast(userforpermission, SocketGuildUser)
                                                 If permission.GuildPermissions.ManageMessages = True Then

                                                     Dim sendchannel As IMessageChannel = Client.GetChannel(servero.getordersettings(chn.Guild.Id, 4))
                                                     If servero.getsubmitchan(chn.Guild.Id) = chn.Id Then
                                                         If Not servero.Checkifhaveprevious(chn.Guild.Id, OrderAuthor.Id) = True Then
                                                             servero.Insertordertrack(chn.Guild.Id, OrderAuthor.Id)
                                                         End If
                                                         Dim number As Integer = servero.Getcutomersorder(chn.Guild.Id, OrderAuthor.Id)
                                                         Await sendchannel.SendMessageAsync($"Order id: `{msgg.Id}` Order list: `{servero.GetCustomer(msgg.Id, 1)}` by: `{OrderAuthor.Username}#{OrderAuthor.Discriminator}` has been marked as canceled by: {chn.GetUser(reaction.UserId).Mention}, This customer have `{number}` completed orders so far!", False)
                                                         servero.Updatecustomerorders(chn.Guild.Id, OrderAuthor.Id, number, OrderAuthor.Username & "#" & OrderAuthor.Discriminator, chn.GetUser(reaction.UserId).Username & "#" & chn.GetUser(reaction.UserId).Discriminator, chn.GetUser(reaction.UserId).Id, msgg.Id, servero.GetCustomer(msgg.Id, 1))
                                                         servero.Deleteuserorder(msgg.Id)
                                                     End If
                                                 End If
                                             End If
                                         End If
                                     End If
                                 End If
                                 'If message IsNot Nothing AndAlso reaction.User.IsSpecified Then Console.WriteLine($"{reaction.User.Value} just added a reaction '{reaction.Emote}' '{reaction.Emote.Name}' " & $"to {message.Author}'s message ({message.Id}).")
                             Catch ex As Exception
                                 Dim chn As SocketGuildChannel = Client.GetChannel(reaction.Channel.Id)
                                 LogService.ClientLog(Nothing, $"{chn.Guild.Name} Error validating order: {ex.ToString}")
                             End Try
                         End Function)
    End Function
    Public Shared Function Checksub(ByVal serverid As Long, ByVal amount As Integer)
        Try
            Dim dateww As Date
            Dim seconds As Integer
            dateww = DateTime.Parse(servero.getpaid(serverid, 2))
            dateww = DateAdd("d", amount, dateww)
            seconds = DateDiff("s", DateTime.Now, dateww)
            Dim d As TimeSpan = TimeSpan.FromSeconds(seconds)
            If DateTime.Now >= dateww Then
                Return False
            ElseIf DateTime.Now < dateww Then
                Return $"{d.Days.ToString} days & {d.Hours.ToString} hours & {d.Minutes.ToString} minutes & {d.Seconds.ToString} seconds"
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Return Nothing
    End Function
    Private Async Function BulkDeleteAsync(ByVal messages As IReadOnlyCollection(Of Cacheable(Of IMessage, ULong)), ByVal channel As ISocketMessageChannel) As Task
        Await Task.CompletedTask
    End Function
    Private ReadOnly dontshow() As String = {"Test", "Bots"}


    Public Async Function ready() As Task
        Try
            restoreauction()
            auctioncheck()
            If expire = False Then
                expiretimer = New System.Timers.Timer(60000)
                AddHandler expiretimer.Elapsed, Async Sub(sender, e)
                                                    checkexpire()
                                                    orderexpirecheck()
                                                    checkautobananddownloadusers()
                                                End Sub
                expiretimer.AutoReset = True
                expiretimer.Enabled = True
                expire = True
            End If
            Dim quotes As New List(Of String)({"The secret of getting ahead is getting started.", "Be kind whenever possible. It is always possible.", "Keep your eyes on the stars, and your feet on the ground."})

            'Dim stream As String = "https://www.localstrading.xyz"
            Dim commands As List(Of CommandInfo) = CommandService.Commands.ToList

            If development = False Then
                Dim status = Task.Run(Async Sub()
                                          Dim i As Integer = 0
                                          Do
                                              If Not Bot_Tools.updatetime = True Then
                                                  Select Case i
                                                      Case 0
                                                          'Await Client.SetGameAsync($"to {client.Guilds.Count} servers {GetRandomHeartEmoji()}", stream, ActivityType.Streaming)
                                                          Await Client.SetGameAsync($"in {Client.Guilds.Count} servers {GetRandomHeartEmoji()}", Nothing, ActivityType.Playing)
                                                          i += 1
                                                      Case 1
                                                          Await Client.SetGameAsync($"with {memberscount()} members {GetRandomHeartEmoji()}", Nothing, ActivityType.Playing)
                                                          i += 1
                                                      Case 2
                                                          Await Client.SetGameAsync($"{prefix}help {GetRandomHeartEmoji()}", Nothing, ActivityType.Listening)
                                                          i += 1
                                                      Case 3
                                                          Dim rnd As New Random()
                                                          Dim j As Integer = rnd.Next(0, Client.Guilds.Count)

                                                          Await Client.SetGameAsync($"{Client.Guilds.ToList.Item(j).Name} server {GetRandomHeartEmoji()}", Nothing, ActivityType.Watching)
                                                          i += 1
                                                      'Case 4
                                                      '    Dim rnd As New Random()

                                                      '    Dim j As Integer = rnd.Next(0, quotes.Count)
                                                      '    Await Client.SetGameAsync($"{quotes.Item(j).ToString} {GetRandomHeartEmoji()}", Nothing, ActivityType.Listening)
                                                      '    i += 1
                                                      Case 4
                                                          Await Client.SetGameAsync($"$report to report any server/user that doesnt follow admb rules {GetRandomHeartEmoji()}", Nothing, ActivityType.Listening)
                                                          i += 1
                                                      Case Else
                                                          Dim rnd As New Random()

                                                          Dim j As Integer = rnd.Next(0, commands.Count)
                                                          Await Client.SetGameAsync($"{prefix}{commands.Item(j).Name} {GetRandomHeartEmoji()}", Nothing, ActivityType.Listening)
                                                          i = 0
                                                          Exit Select
                                                  End Select
                                                  Await Task.Delay(70000)
                                              End If

                                          Loop
                                      End Sub)
            Else
                Await Client.SetGameAsync($"Bot is going through some tests, please dont use until next announcement {GetRandomHeartEmoji()}", Nothing, ActivityType.Listening)
            End If


        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try

    End Function
    Private Function memberscount()
        Dim membercount As Integer = Nothing
        For Each guild In Client.Guilds
            membercount = membercount + guild.Users.Count
        Next
        Return membercount
    End Function
    Public Shared Function GetRandomHeartEmoji() As String
        Dim values As Array = System.Enum.GetValues(GetType(HeartEmoji))
        Dim random As New Random()
        Dim randomEmoji As HeartEmoji = DirectCast(values.GetValue(random.Next(values.Length)), HeartEmoji)

        Return EnumUtil.GetString(randomEmoji)
    End Function
    Private Async Function welcome(ByVal user As SocketGuildUser) As Task
        Try
            If servero.checksetup(user.Guild.Id) = True AndAlso Not servero.getsetupstatus(user.Guild.Id, 3) = False AndAlso servero.checkblacklist(user.Guild.Id) = False Then
                If servero.checkuserbanstatus(user.Id, True) = True Then
                    Try
                        Dim channelidd As Long = Nothing
                        Dim reason As String = Nothing
                        reason = servero.checkuserbanstatus(user.Id, False, True)

                        channelidd = servero.getsetupstatus(user.Guild.Id, 2)
                        Dim list As String = $"- User: {user.Username}#{user.Discriminator} | Reason: {reason}" & System.Environment.NewLine
                        Await user.Guild.AddBanAsync(user, 7, reason)
                        Dim name As String = Nothing
                        Try

                            name = Client.GetGuild(user.Guild.Id).Name
                        Catch ex As Exception
                            name = user.Guild.Id
                        End Try

                        Dim chn As IMessageChannel = Client.GetChannel(channelidd)
                        Dim ebddd As New EmbedBuilder With {
                                                                                                                  .Description = $"A blacklisted user tried to join and automatically got banned | {DateTime.Now}" & System.Environment.NewLine & System.Environment.NewLine & list
                                                                                                                   }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)
                        Dim chn22 As IMessageChannel = Client.GetChannel(709874986466410596)
                        Dim ebddd22 As New EmbedBuilder With {
                                                                                                                  .Description = $"A blacklisted user tried to join and automatically got banned in {name} server | {DateTime.Now}" & System.Environment.NewLine & System.Environment.NewLine & list
                                                                                                                   }
                        ebddd22.Color = Color.Green
                        Await chn22.SendMessageAsync(Nothing, False, ebddd22.Build)

                    Catch ex As Exception
                        Console.WriteLine(ex.ToString)
                    End Try
                End If
                If user.Guild.Id = LocalsServer Then

                    Dim chn As IMessageChannel = Client.GetChannel(669021895366017025)
                    Await chn.SendMessageAsync($"{user.Mention} Welcome to locals trading community, please read our <#619581840180641792> and verify yourself if you dont know how, read <#732367448548048936>  <a:verify:742861306289324193>", False, Nothing)
                End If
            Else
                If user.Guild.Id = LocalsServer Then

                    Dim chn As IMessageChannel = Client.GetChannel(669021895366017025)
                    Await chn.SendMessageAsync($"{user.Mention} Welcome to locals trading community, please read our <#619581840180641792> and verify yourself if you dont know how, read <#732367448548048936>  <a:verify:742861306289324193>", False, Nothing)
                End If
            End If

        Catch ex As Exception
            LogService.ClientLog(Nothing, $"{ex.ToString}")
        End Try

    End Function
    Public Function antilink(ByVal guild As Long, ByVal channelid As Long, ByVal authorid As Long, ByVal message As String) As Boolean

        If guild = LocalsServer Then
            If Not channelid = "766692223143641088" Then
                If Not authorid = devY Or authorid = devL Then
                    If Links.Any(Function(b) message.ToLower().Contains(b.ToLower())) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If
        Return Nothing
    End Function
    Public Async Sub checkautobananddownloadusers()
        Try
            For Each guilddd In Client.Guilds


                servero.getuserbanlist()
                Dim number As Integer = 0
                Dim list As String = Nothing
                Dim usertoban As IUser = Nothing
                Dim reason As String = Nothing
                Dim channelidd As Long = Nothing

                If servero.checksetup(guilddd.Id) AndAlso servero.getsetupstatus(guilddd.Id, 3) = True AndAlso servero.checkblacklist(guilddd.Id) = False Then
                    For Each user As KeyValuePair(Of Long, String) In banlistwithreason
                        Try

                            usertoban = guilddd.GetUser(user.Key)
                            reason = user.Value
                            list += $"- User: {usertoban.Username}#{usertoban.Discriminator} | Reason: {reason}" & System.Environment.NewLine
                            Await guilddd.AddBanAsync(usertoban, 7, reason)
                            number = number + 1
                        Catch ex As Exception
                        End Try
                    Next

                    channelidd = servero.getsetupstatus(guilddd.Id, 2)
                    Dim name As String = Nothing
                    Try

                        name = Client.GetGuild(guilddd.Id).Name
                    Catch ex As Exception
                        name = guilddd.Id
                    End Try
                    If Not number = 0 Then

                        Dim chn As IMessageChannel = Client.GetChannel(channelidd)
                        Dim ebddd As New EmbedBuilder With {
                                                                                                                  .Description = $"{number} members automatically banned | {DateTime.Now}" & System.Environment.NewLine & System.Environment.NewLine & list
                                                                                                                   }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)
                        Dim chn22 As IMessageChannel = Client.GetChannel(709874986466410596)
                        Dim ebddd22 As New EmbedBuilder With {
                                                                                                                  .Description = $"{number} members automatically banned in {name} server | {DateTime.Now}" & System.Environment.NewLine & System.Environment.NewLine & list
                                                                                                                   }
                        ebddd22.Color = Color.Green
                        Await chn22.SendMessageAsync(Nothing, False, ebddd22.Build)

                    End If
                End If

            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Async Sub auctioncheck()
        Try
            For Each guilddd In Client.Guilds
                Dim number As Integer = 0
                Dim name As String = Nothing
                If servero.checkauctionstatus(guilddd.Id) = True Then
                    servero.getallowedchans(guilddd.Id)
                    For Each channelid In channelidssss
                        Try
                            Dim kk = guilddd.GetChannel(channelid).Name

                        Catch ex As Exception
                            name = guilddd.Name
                            number += 1
                            servero.removeexallowedchannel(guilddd.Id, channelid)
                        End Try




                    Next
                    If Not number = 0 AndAlso Not name = Nothing Then
                        Dim chn As IMessageChannel = Client.GetChannel(709874986466410596)
                        Dim ebddd As New EmbedBuilder With {
                                                                                                                  .Description = $"Removed {number} auction channel ids in auction database, couldnt find the channel in {name} server | {DateTime.Now}"
                                                                                                                   }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)
                        Await LogService.ClientLog(Nothing, $"Removed {number} auction channel ids in auction database, couldnt find the channel in {name} server | {DateTime.Now}")
                    End If
                End If

            Next

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub
    Public Async Sub orderexpirecheck()
        Try
            For Each guilddd In Client.Guilds
                If servero.checkorder(guilddd.Id) = True AndAlso servero.checkblacklist(guilddd.Id) = False Then
                    Dim number As Integer = Nothing

                    servero.getorderexpire(guilddd.Id)
                    For Each order As KeyValuePair(Of Long, String) In ordersexpire
                        Dim dateww As Date = Nothing
                        Dim seconds As Integer
                        dateww = DateTime.Parse(order.Value)
                        dateww = DateAdd("h", 24, dateww)
                        seconds = DateDiff("s", DateTime.Now, dateww)
                        Dim d As TimeSpan = TimeSpan.FromSeconds(seconds)
                        If DateTime.Now >= dateww Then
                            servero.removeexpiredorder(guilddd.Id, order.Key)
                            number = number + 1
                        End If

                    Next
                    Dim name As String = Nothing
                    Try

                        name = Client.GetGuild(guilddd.Id).Name
                    Catch ex As Exception
                        name = guilddd.Id
                    End Try
                    If Not number = 0 Then
                        Dim chn As IMessageChannel = Client.GetChannel(709874986466410596)
                        Dim ebddd As New EmbedBuilder With {
                                                                                                                  .Description = $"Removed {number} expired orders in {name} server orders list | {DateTime.Now}"
                                                                                                                   }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)
                        Await LogService.ClientLog(Nothing, $"Removed {number} expired orders in {name} server orders list | {DateTime.Now}")
                    End If

                Else
                    Try
                        servero.clearorderlist(guilddd.Id, 1)
                    Catch ex As Exception
                    End Try
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private ReadOnly Links() As String = {"discord.gg", "discordapp.com/invite"}
    Public Shared ReadOnly Badwords() As String = {"discord.gg", "discordapp.com/invite", "fuck", "pussy", "idiot", "penis", "porn", "hentai", "nigga", "hoe", "slut", "bitch", "dickhead", "vagina", "hentai", "asshole", "cunt", "xnxx.com"}
    Private ReadOnly ext() As String = {".png", ".bmp", ".jpeg", ".gif"}
    Public Async Sub checkexpire()

        Try
            For Each guilddd In Client.Guilds
                If servero.checkpaid(guilddd.Id) = True AndAlso servero.checkblacklist(guilddd.Id) = False Then
                    If Checksub(guilddd.Id, servero.getpaid(guilddd.Id, 1)) = False Then
                        Dim name As String = Nothing

                        Dim chn As IMessageChannel = Client.GetChannel(709874986466410596)
                        Try
                            name = Client.GetGuild(guilddd.Id).Name
                        Catch ex As Exception
                            name = guilddd.Id
                        End Try
                        servero.delsbsr(guilddd.Id)
                        Dim ebddd As New EmbedBuilder With {
                                                                 .Description = $"{name} server subscription expired and removed from subscription list | {DateTime.Now}"
                                                                  }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)

                    End If
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub
    Public Async Sub restoreauction()
        Dim kk As Integer = 0
        For Each guild In Client.Guilds
            If servero.checkblacklist(guild.Id) = False Then
                If servero.getauctionstatus(guild.Id, 1) = "True" Then

                    For Each channel In guild.Channels
                        If servero.checkallowedchan(guild.Id, channel.Id) = True Then
                            If servero.getauction(guild.Id, servero.getauctionservid(channel.Id, guild.Id)) = (channel.Id) Then
                                If Not listo.ContainsKey(channel.Id) = True Then
                                    bTimer = New System.Timers.Timer(1000)
                                    AddHandler bTimer.Elapsed, Async Sub(sender, e)
                                                                   Try
                                                                       If servero.getauctionstatus(guild.Id, 1) = "True" Then
                                                                           If listo.ContainsKey(channel.Id) = True Then
                                                                               Dim startingbid As Integer = servero.getauctiond(channel.Id, 1)
                                                                               Dim lastbid As Integer = servero.getauctiond(channel.Id, 2)
                                                                               Dim lastbidname As String = servero.getauctiond(channel.Id, 3)
                                                                               Dim snipeguard As String = servero.getauctiond(channel.Id, 4)
                                                                               Dim m_StopTimet As Date = servero.getauctiond(channel.Id, 5)
                                                                               Dim payment As String = servero.getauctiond(channel.Id, 6)
                                                                               Dim item As String = servero.getauctiond(channel.Id, 7)
                                                                               Dim description As String = servero.getauctiond(channel.Id, 8)
                                                                               Dim bidinc As Integer = servero.getauctiond(channel.Id, 9)
                                                                               Dim requestedby As String = servero.getauctiond(channel.Id, 10)
                                                                               Dim authorp As String = servero.getauctiond(channel.Id, 11)
                                                                               Dim msgid As Long = servero.getauctiond(channel.Id, 12)
                                                                               Dim embedid As Long = servero.getauctiond(channel.Id, 13)
                                                                               Dim authid As Long = servero.getauctiond(channel.Id, 14)
                                                                               Dim link As String = servero.getauctiond(channel.Id, 15)
                                                                               Dim lastbidid As Long = servero.getauctiond(channel.Id, 16)

                                                                               Dim Guildddd As SocketGuild = Client.GetGuild(guild.Id)
                                                                               Dim chn As IMessageChannel = Guildddd.GetChannel(channel.Id)


                                                                               If DateTime.Now >= servero.getauctiond(channel.Id, 5) And stoppp = False Then
                                                                                   If lastbid = Nothing Then

                                                                                       If Not stoppp = True Then : Await chn.SendMessageAsync($"Auction Finished, <@{servero.getauctiond(channel.Id, 14)}> unfortunately there wasn't any winner.") : End If
                                                                                       listo.Remove(channel.Id)
                                                                                       servero.removeauction(guild.Id, channel.Id)
                                                                                       stoppp = True
                                                                                       bTimer.Stop()
                                                                                       bTimer.Dispose()


                                                                                   Else

                                                                                       If Not stoppp = True Then : Await chn.SendMessageAsync($"Auction Finished" & $" Winner is: <@{lastbidid}> By: {lastbid} {payment}, <@{servero.getauctiond(channel.Id, 14)}> please contact your auction winner!") : End If
                                                                                       Try
                                                                                           Dim eeeeee As New EmbedBuilder With {
                                                                                                              .Description = $"You won an auction in `{chn.Name}` channel in `{Guildddd.Name}` server, item: `{item}` for `{servero.getauctiond(channel.Id, 2)}  {payment}`"
                                                                                                               }
                                                                                           eeeeee.Color = Color.Green

                                                                                           If Not stoppp = True Then : Await Client.GetUser(lastbidid).SendMessageAsync(Nothing, False, eeeeee.Build) : End If
                                                                                       Catch ex As Exception
                                                                                       End Try
                                                                                       listo.Remove(channel.Id)
                                                                                       servero.removeauction(guild.Id, channel.Id)

                                                                                       stoppp = True
                                                                                       bTimer.Stop()
                                                                                       bTimer.Dispose()


                                                                                   End If
                                                                               Else

                                                                                   Dim hours As Long
                                                                                   Dim minutes As Long
                                                                                   Dim seconds As Long
                                                                                   seconds = DateDiff("s", DateTime.Now, m_StopTimet)
                                                                                   minutes = seconds \ 60
                                                                                   seconds = seconds - minutes * 60
                                                                                   hours = minutes \ 60
                                                                                   minutes = minutes - hours * 60
                                                                                   If hours = 12 AndAlso minutes >= 59 AndAlso seconds >= 58 AndAlso dwazdah = False Then

                                                                                       Dim ebddd As New EmbedBuilder With {
                                                                                                         .Description = $"Less than 12 hour remaining on this auction, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                          }
                                                                                       ebddd.Color = Color.Green
                                                                                       If Not dwazdah = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                       dwazdah = True
                                                                                   ElseIf hours = 0 AndAlso minutes >= 59 AndAlso seconds >= 58 AndAlso yaks = False Then

                                                                                       Dim ebddd As New EmbedBuilder With {
                                                                                                      .Description = $"Less than 1 hour remaining on this auction, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                       }
                                                                                       ebddd.Color = Color.Green
                                                                                       If Not yaks = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                       yaks = True

                                                                                   ElseIf hours = 0 AndAlso minutes = 29 AndAlso seconds >= 59 AndAlso sed = False Then

                                                                                       Dim ebddd As New EmbedBuilder With {
                                                                                                     .Description = $"Less than 30 minutes remaining on this auction, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                      }
                                                                                       ebddd.Color = Color.Green
                                                                                       If Not sed = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                       sed = True
                                                                                   ElseIf hours = 0 AndAlso minutes = 0 AndAlso seconds <= 59 AndAlso seconds >= 58 AndAlso yakd = False Then
                                                                                       If snipeguard = 1 Then
                                                                                           Dim ebddd As New EmbedBuilder With {
                                                                                                   .Description = $"Less than 1 minute remaining on this auction, Snipeguard is enabled, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                    }
                                                                                           ebddd.Color = Color.Green
                                                                                           If Not yakd = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                           yakd = True
                                                                                       Else
                                                                                           Dim ebddd As New EmbedBuilder With {
                                                                                                  .Description = $"Less than 1 minute remaining on this auction, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                   }
                                                                                           ebddd.Color = Color.Green
                                                                                           If Not yakd = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                           yakd = True
                                                                                       End If

                                                                                   ElseIf hours = 0 AndAlso minutes = 0 AndAlso seconds <= 30 AndAlso seconds >= 29 AndAlso das = False Then
                                                                                       If snipeguard = 1 Then
                                                                                           Dim ebddd As New EmbedBuilder With {
                                                                                                   .Description = $"Less than 30 seconds remaining on this auction, Snipeguard is enabled, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                    }
                                                                                           ebddd.Color = Color.Green
                                                                                           If Not das = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                           das = True
                                                                                       Else
                                                                                           Dim ebddd As New EmbedBuilder With {
                                                                                                  .Description = $"Less than 30 seconds remaining on this auction, Last bid is: " & servero.getauctiond(channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                                                                   }
                                                                                           ebddd.Color = Color.Green
                                                                                           If Not das = True Then : Await chn.SendMessageAsync($"<@&{servero.getauctionstatus(guild.Id, 2)}>", False, ebddd.Build) : End If
                                                                                           das = True
                                                                                       End If
                                                                                   End If

                                                                                   'Dim ebddds As New EmbedBuilder With {
                                                                                   '  .Description = $"This is live timer"
                                                                                   '   }
                                                                                   'ebddds.Color = Color.Green

                                                                                   'Dim kio = Await Context.Channel.SendMessageAsync(Nothing, False, ebddds.Build)
                                                                                   'ebddds.Description = $"Time left is: {hours}:{minutes}:{seconds} , Last bid is: " & servero.getauctiond(channel.id, 2)
                                                                                   '    Await kio.ModifyAsync(Sub(x) x.Embed = ebddds.Build)
                                                                                   'Await Context.Message.


                                                                               End If
                                                                           End If
                                                                       End If
                                                                   Catch ex As Exception
                                                                   End Try
                                                               End Sub
                                    bTimer.AutoReset = True
                                    bTimer.Enabled = True
                                    listo.Add(channel.Id, True)
                                    kk = kk + 1
                                End If
                            End If
                        End If
                    Next
                End If
            End If

        Next

        Await LogService.ClientLog(Nothing, $"{kk} Auctions restored successfully")
    End Sub
    Private Async Function HandleCommandAsync(ByVal message As SocketMessage) As Task
        Dim userMessage = TryCast(message, SocketUserMessage)
        If userMessage Is Nothing OrElse userMessage.Author.IsBot OrElse userMessage.Author.IsWebhook Then Return
        Dim argPos = 0
        Dim context As New SocketCommandContext(Client, userMessage)
        Try
            If Not message.Channel.GetType.ToString = "Discord.WebSocket.SocketDMChannel" Then
                If servero.checkblacklist(context.Guild.Id) = False Then
                    If servero.checkuserbanstatus(userMessage.Author.Id, True) = False Then
                        If servero.checkglobalchat(context.Guild.Id) = True AndAlso Not userMessage.HasMentionPrefix(Client.CurrentUser, argPos) AndAlso Not userMessage.HasStringPrefix(prefix, argPos, StringComparison.OrdinalIgnoreCase) AndAlso context.Channel.Id = servero.getglchat(context.Guild.Id, 1) Then
                            Try
                                If context.Channel.Id = servero.getglchat(context.Guild.Id, 1) Then
                                    If servero.checkmuted(userMessage.Author.Id) = False Then
                                        Dim servid As Long = context.Guild.Id
                                        For Each guild In context.Client.Guilds
                                            If servero.checkglobalchat(guild.Id) = True Then
                                                If Badwords.Any(Function(b) userMessage.Content.ToLower().Contains(b.ToLower())) Then
                                                    Await userMessage.DeleteAsync()
                                                Else
                                                    Dim eb As New EmbedBuilder
                                                    eb.Color = Color.Blue
                                                    'If userMessage.Author.GetAvatarUrl = Nothing Then
                                                    '    eb.WithAuthor(userMessage.Author.Username & "#" & userMessage.Author.Discriminator, userMessage.Author.GetDefaultAvatarUrl)
                                                    'Else
                                                    '    eb.WithAuthor(userMessage.Author.Username & "#" & userMessage.Author.Discriminator & $": {userMessage.Content}", userMessage.Author.GetAvatarUrl)
                                                    'End If

                                                    eb.WithFooter("User id: " & userMessage.Author.Id & " | Sended from: " & context.Guild.Name & " Server")
                                                    eb.WithCurrentTimestamp()

                                                    If Not servid = guild.Id Then
                                                        Dim chn As IMessageChannel = context.Client.GetChannel(servero.getglchat(guild.Id, 1))
                                                        If userMessage.Author.Id = devY Or userMessage.Author.Id = devL Or userMessage.Author.Id = devS Then
                                                            If userMessage.Author.GetAvatarUrl = Nothing Then
                                                                eb.WithAuthor($"{userMessage.Author.Username}#{userMessage.Author.Discriminator} | {context.Client.CurrentUser.Username} Staff", userMessage.Author.GetDefaultAvatarUrl)
                                                            Else
                                                                eb.WithAuthor($"{userMessage.Author.Username}#{userMessage.Author.Discriminator} | {context.Client.CurrentUser.Username} Staff", userMessage.Author.GetAvatarUrl)
                                                            End If
                                                        Else
                                                            If userMessage.Author.GetAvatarUrl = Nothing Then
                                                                eb.WithAuthor($"{userMessage.Author.Username}#{userMessage.Author.Discriminator}", userMessage.Author.GetDefaultAvatarUrl)
                                                            Else
                                                                eb.WithAuthor($"{userMessage.Author.Username}#{userMessage.Author.Discriminator}", userMessage.Author.GetAvatarUrl)
                                                            End If
                                                        End If
                                                        If Not userMessage.Attachments.Count = 0 AndAlso userMessage.Attachments.Count <= 1 Then
                                                            If ext.Any(Function(b) System.IO.Path.GetExtension(userMessage.Attachments.First.Url).ToLower.Contains(b.ToLower())) Then
                                                                eb.Description = $"{userMessage.Content}"
                                                                eb.ImageUrl = userMessage.Attachments.First.Url
                                                                Dim k = Await chn.SendMessageAsync(Nothing, False, eb.Build)
                                                                globalchat.Add(k.Id, userMessage.Author.Id)
                                                            End If



                                                        Else


                                                            eb.Description = $"{userMessage.Content}"
                                                            Dim k = Await chn.SendMessageAsync(Nothing, False, eb.Build)
                                                            globalchat.Add(k.Id, userMessage.Author.Id)
                                                        End If




                                                    End If
                                                End If

                                            End If
                                        Next
                                        Return

                                    Else
                                        Await context.Channel.SendMessageAsync($"{userMessage.Author.Mention} You are muted from global chat.", False, Nothing)
                                        Await userMessage.DeleteAsync()
                                    End If

                                End If
                            Catch ex As Exception
                            End Try
                        Else
                            If Not userMessage.HasMentionPrefix(Client.CurrentUser, argPos) AndAlso Not userMessage.HasStringPrefix(prefix, argPos, StringComparison.OrdinalIgnoreCase) AndAlso Not antilink(context.Guild.Id, userMessage.Channel.Id, userMessage.Author.Id, userMessage.Content) = True Then
                                Return
                            Else
                                If antilink(context.Guild.Id, userMessage.Channel.Id, userMessage.Author.Id, userMessage.Content) = True Then
                                    Dim ebd As New EmbedBuilder With {
                                                             .Description = $"Invite links are not permitted in this channel."
                                                              }
                                    ebd.Color = Color.Red
                                    Await context.Channel.SendMessageAsync(userMessage.Author.Mention, False, ebd.Build)
                                    Await userMessage.DeleteAsync()
                                    Exit Function


                                Else

                                    If servero.checksetup(context.Guild.Id) = True AndAlso Not servero.getsetupstatus(context.Guild.Id, 1) = 0 AndAlso Not context.Channel.Id = servero.getsetupstatus(context.Guild.Id, 1) Then
                                        Dim thecommand As String = userMessage.Content.Substring(argPos).Trim
                                        Dim auction() As String = {"auction", "bid", "acheck", "timer", "stop auction"}
                                        Dim order() As String = {"order", "eorder"}
                                        If servero.checkallowedchan(context.Guild.Id, context.Channel.Id) = True AndAlso auction.Any(Function(b) thecommand.ToLower().Contains(b.ToLower())) Then
                                            GoTo Imhere
                                        End If
                                        If servero.getordersettings(context.Guild.Id, 1) = context.Channel.Id AndAlso order.Any(Function(b) thecommand.ToLower().Contains(b.ToLower())) Then
                                            GoTo Imhere
                                        End If

                                        Dim thisss = TryCast(context.Message.Author, SocketGuildUser)
                                        If thisss.GuildPermissions.ManageChannels = True Then : GoTo Imhere : End If
                                        Try
                                            If Not context.Channel.Id = servero.getsetupstatus(context.Guild.Id, 1) Then
                                                Await context.Message.AddReactionAsync(New Emoji(NOT_ALLOWED))

                                                Dim chn As IMessageChannel = context.Guild.GetChannel(servero.getsetupstatus(context.Guild.Id, 1))
                                                Dim ebddd As New EmbedBuilder With {
                                                     .Description = $"You cant use {context.Client.CurrentUser.Username} commands in `{context.Channel.Name}` channel, please use `{chn.Name}` channel in `{context.Guild.Name}` server"
                                                      }
                                                ebddd.Color = Color.Red

                                                Await context.Message.Author.SendMessageAsync(Nothing, False, ebddd.Build)

                                            End If
                                        Catch ex As Exception
                                        End Try
                                    Else
Imhere:

                                        Dim command As String = userMessage.Content.Substring(argPos).Trim
                                        Dim result = Await CommandService.ExecuteAsync(context, command, Services)
                                        If Not result.IsSuccess Then
                                            If Not result.Error = CommandError.UnknownCommand Then
                                                Await context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                                            End If
                                        End If
                                    End If

                                End If

                            End If
                        End If
                    Else
                        Await context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    End If

                Else
                    Await context.Channel.SendMessageAsync($"{userMessage.Author.Mention} This server is in blacklist and cant use my commands.", False, Nothing)
                    Await userMessage.DeleteAsync()
                End If

            Else
                'treat DMs
                Await Itsviadm(message)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function
    Private Async Function Itsviadm(ByVal message As SocketMessage) As Task
        Dim userMessage = TryCast(message, SocketUserMessage)
        Dim context As New SocketCommandContext(Client, userMessage)
        Dim argPos = 0
        If userMessage.Content.StartsWith("quiz ") Then
            Dim gotanswer As String = userMessage.Content.Replace("quiz ", Nothing)

            If Not quizstatus = False Then
                If Not quizanswers.ContainsKey(userMessage.Author.Id) Then
                    Dim Guildddd As SocketGuild = context.Client.GetGuild(LocalsServer)
                    Dim chn As IMessageChannel = Guildddd.GetChannel(741717928126185623)
                    Try
                        Guildddd.GetUser(userMessage.Author.Id)
                        quizanswers.Add(userMessage.Author.Id, "A")
                        Dim ebd As New EmbedBuilder With {
                                   .Description = $"Submitted."
                                    }
                        ebd.Color = Color.Green
                        Await context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
                        Dim ebddd As New EmbedBuilder With {
                                    .Description = $"New quiz answer from " & userMessage.Author.Username & "#" & userMessage.Author.Discriminator & "| " & userMessage.Author.Id & System.Environment.NewLine & " Answer is: " & gotanswer
                                     }
                        ebddd.Color = Color.Green
                        Await chn.SendMessageAsync(Nothing, False, ebddd.Build)
                    Catch ex As Exception
                        Console.WriteLine(ex.ToString)
                        Dim ebd As New EmbedBuilder With {
                                    .Description = $"You need to be in locals server in order to submit quiz answers."
                                     }
                        ebd.Color = Color.Red
                        context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
                    End Try
                Else
                    Dim ebd As New EmbedBuilder With {
                                       .Description = $"You already submitted an answer."
                                        }
                    ebd.Color = Color.Red
                    Await context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
                End If
            Else
                Dim ebd As New EmbedBuilder With {
                                     .Description = $"There isnt any quiz going on or its finished."
                                      }
                ebd.Color = Color.Red
                Await context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
            End If
        ElseIf userMessage.HasStringPrefix(prefix, argPos, StringComparison.OrdinalIgnoreCase) Then
            Dim command As String = userMessage.Content.Substring(argPos).Trim
            If command = "binvite" Then
                Try

                    Await context.Channel.SendMessageAsync($"To invite this bot use this link" & System.Environment.NewLine & Bot_Tools.botlink)
                Catch ex As Exception
                End Try
            ElseIf command = "pin" Then
                Dim ebd As New EmbedBuilder With {
                                     .Description = $"{random(4, 4)}"
                                      }
                ebd.Color = Color.Green
                Await context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
            Else

                Dim ebd As New EmbedBuilder With {
                                                 .Description = $"Direct message commands are not supported, the only supported command is `{prefix}binvite`"
                                                  }
                ebd.Color = Color.Red
                Await context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
            End If
        End If
    End Function
    Function random(minCharacters As Integer, maxCharacters As Integer)
        Dim s As String = "1234567890"
        Static r As New Random
        Dim chactersInString As Integer = r.Next(minCharacters, maxCharacters)
        Dim sb As New StringBuilder
        For i As Integer = 1 To chactersInString
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
End Class