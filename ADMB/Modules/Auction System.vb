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
Public Class Auction_System
    Inherits InteractiveBase
    Private m_StopTime As Date
    Private location As String = System.AppDomain.CurrentDomain.BaseDirectory
    Private filename As String = ("auctiontemp.html")
    Private fullpath As String = System.IO.Path.Combine(location, filename)
    Private locationtosave As String = "C:\Apache24\htdocs\auctions\"
    Private auctionfree As String = $"{System.Environment.NewLine} Room is free, now you can make your own auction using `$auction` please follow server rules!"
    <Command("stop auction", RunMode:=RunMode.Async)>
    <Summary("Force stop an auction usage: (prefix)stop auction")>
    Public Async Function stopauction() As Task
        Try

            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If servero.checkallowedchan(Context.Guild.Id, Context.Channel.Id) = True Then
                    If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then

                        Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
                        If thisss.GuildPermissions.ManageMessages = True Then

                            Dim ebd As New EmbedBuilder With {
                                                 .Description = $"Auction stopped."
                                                  }
                            ebd.Color = Color.Green
                            Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                            listo.Remove(Context.Channel.Id)
                            servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                            aTimer.Stop()
                            aTimer.Dispose()
                        Else
                            Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                            Dim ebd As New EmbedBuilder With {
                                                           .Description = $"You need Manage_Messages permission to use this command."
                                                            }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        End If
                    Else
                        Await ReplyAndDeleteAsync(Context.User.Mention & " No auction is running in this channel.", False, Nothing, TimeSpan.FromSeconds(5))
                    End If
                Else
                    Dim kkwe As String = Nothing
                    servero.getallowedchans(Context.Guild.Id)


                    For Each channelid In channelidssss
                        Try
                            kkwe += "- " & Context.Guild.GetChannel(channelid).Name & System.Environment.NewLine
                        Catch ex As Exception
                        End Try
                    Next

                    Dim ebd As New EmbedBuilder With {
                                      .Description = $"Auction events are not permitted in this channel." & System.Environment.NewLine & "Allowed channels:" & System.Environment.NewLine & kkwe
                                       }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                End If

            Else
                Dim ebd As New EmbedBuilder With {
                                      .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                       }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    <Command("auction", RunMode:=RunMode.Async)>
    <Summary("Starts a new auction usage: (prefix)auction | (prefix)bid [number] to bid on the auction")>
    Public Async Function auction() As Task
        Try
            Dim snipeguard As Boolean = False

            Dim payment As String


            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If servero.checkallowedchan(Context.Guild.Id, Context.Channel.Id) = True Then
                    If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then
                        Await ReplyAndDeleteAsync(Context.User.Mention & " There is another auction running in this channel!", False, Nothing, TimeSpan.FromSeconds(10))
                    Else

                        Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Alright, What item you want to add to auction? ")
retryitemm:
                        Dim message = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(2))
                        If Not message.Channel.GetType.Name = "SocketDMChannel" Then
                            Exit Function
                        End If
                        Dim item As String = ""
                        Dim description As String = ""
                        Dim startingbid As Integer
                        Dim bidinc As String = ""

                        If CommandHandler.Badwords.Any(Function(b) message.Content.ToLower().Contains(b.ToLower())) Then
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Dont say badwords.")


                            GoTo retryitemm
                        Else
                            item = message.Content
recheckdescr:
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Do you want to add a description? yes/no")
                            Dim newu = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(2))
                            If Not newu.Channel.GetType.Name = "SocketDMChannel" Then
                                Exit Function
                            End If
                            If newu.Content = "yes" Then
lenghtrecheck:
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Please type your description, 100 character allowed.")
                                Dim bow = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                                If Not bow.Channel.GetType.Name = "SocketDMChannel" Then
                                    Exit Function
                                End If
                                Dim length As Integer = bow.Content.Length
                                If length >= 100 Then
                                    Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " You cannot type more than 100 characters, please retype your description, your description have " & length & " characters.")
                                    GoTo lenghtrecheck
                                Else


                                    description = bow.Content
                                    Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Got it")
                                    GoTo nextstepp
                                End If
                            ElseIf newu.Content = "no" Then
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Got it")
                                description = Nothing
                                GoTo nextstepp
                            Else
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Couldn't understand.")
                                GoTo recheckdescr
                            End If
nextstepp:
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Whats your starting bid? *only numbers*")
                            Dim k = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                            If Not k.Channel.GetType.Name = "SocketDMChannel" Then
                                Exit Function
                            End If
                            Dim lengthh As Integer = k.Content.Length
                            If lengthh >= 30 Then
                                startingbid = ""
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Cant accept more than 30 numbers.")

                                GoTo nextstepp
                            Else
nextstepppp:
                                Dim int As Integer
                                If Integer.TryParse(k.Content, int) Then
                                    startingbid = k.Content
                                    Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Whats your bid increment number? *only numbers*")
                                    Dim kd = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                                    If Not kd.Channel.GetType.Name = "SocketDMChannel" Then
                                        Exit Function
                                    End If
                                    Dim lenghtttt As Integer = kd.Content.Length
                                    If lengthh >= 30 Then
                                        bidinc = ""
                                        Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Cant accept more than 30 numbers.")
                                        GoTo nextstepppp
                                    Else
                                        If Integer.TryParse(kd.Content, int) Then
                                            bidinc = kd.Content
                                            GoTo Timerrr
                                        Else
                                            bidinc = ""
                                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " It should be only numbers.")

                                            GoTo nextstepppp
                                        End If
                                    End If
                                Else
                                    startingbid = ""
                                    Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " It should be only numbers.")

                                    GoTo nextstepp
                                End If
                            End If


Timerrr:
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Auction end time? use HH:mm:ss style example 12:32:00 *max 48 hours*")
                            Dim kdd = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                            If Not kdd.Channel.GetType.Name = "SocketDMChannel" Then
                                Exit Function
                            End If
                            If checkdate(kdd.Content) = False Then
                                time = Nothing
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Please use the correct format HH:mm:ss and cannot be more than 48 hours.")
                                GoTo Timerrr
                            Else
                                Try
                                    Dim kkweweewe As String = kdd.Content.ToString
                                    time = kkweweewe
                                    GoTo finallllyyy
                                Catch ex As Exception
                                End Try

                            End If
finallllyyy:
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " What payment do you accept?")
                            Dim kdodo = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                            If Not kdodo.Channel.GetType.Name = "SocketDMChannel" Then
                                Exit Function
                            End If
                            Dim lengtfh As Integer = kdodo.Content.Length
                            If lengthh >= 30 Then
                                payment = ""
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Cant accept more than 30 character.")
                                GoTo finallllyyy
                            Else
                                payment = kdodo.Content.ToString
                                GoTo finalllllllyyyy
                            End If

finalllllllyyyy:
                            Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Do you want to enable snipe guard? 2 minutes per each extend. yes/no")
                            Dim ww = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(5))
                            If Not ww.Channel.GetType.Name = "SocketDMChannel" Then
                                Exit Function
                            End If
                            If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then
                                Await Context.Message.Author.SendMessageAsync(Context.User.Mention & " Unfortunately someone else started auction before you in that channel.")
                            Else
                                If ww.Content = "yes" Then
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " All done.")
                                snipeguard = True

                            ElseIf ww.Content = "no" Then
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " All done.")
                                snipeguard = False
                            Else
                                Await Context.Message.Author.SendMessageAsync(Context.Message.Author.Mention & " Couldn't understand.")
                                GoTo finalllllllyyyy
                            End If

                            Dim eb As New EmbedBuilder With {
                                   .Title = item,
                                   .Description = description
                                    }
                                eb.WithCurrentTimestamp()
                                '.ImageUrl = "",
                                eb.Color = Color.Blue
                                eb.WithAuthor("Auction", Context.User.GetAvatarUrl)
                                eb.WithThumbnailUrl(Context.User.GetAvatarUrl)
                                eb.WithFooter("Auction Time:  " & time & " | Auction Requested By: " & Context.User.Username & "#" & Context.User.Discriminator) 'change it
                                eb.AddField("Starting Bid: ", startingbid, False)
                                eb.AddField("Bid Increment: ", bidinc, False)
                                eb.AddField("Payment: ", payment, False)
                                eb.AddField("Auction Time: ", time, False)
                                eb.AddField("Snipeguard: ", snipeguard.ToString, False)
                                Dim msg = Await ReplyAsync("", False, eb.Build())


                                Dim fields() As String
                                Dim hourss As Long
                                Dim minutess As Long
                                Dim secondss As Long

                                fields = Split(time, ":")
                                hourss = fields(0)
                                minutess = fields(1)
                                secondss = fields(2)

                                m_StopTime = DateTime.Now
                                m_StopTime = DateAdd("h", hourss, m_StopTime)
                                m_StopTime = DateAdd("n", minutess, m_StopTime)
                                m_StopTime = DateAdd("s", secondss, m_StopTime)
                                Dim link As String
                                Try
                                    Dim kkoo = m_StopTime.ToString("MMMM dd, yyyy H:mm:ss", CultureInfo.InvariantCulture)

                                    'Dim newqqq As String = IO.File.ReadAllText(fullpath)
                                    'newqqq = newqqq.Replace("AUCTIONNAMEEE", item).Replace("DATEEEEEE", kkoo)

                                    'Dim filenametosave As String = (item & ".html")
                                    'Dim newkaskas As String
                                    'If IO.File.Exists(System.IO.Path.Combine(locationtosave, filenametosave)) Then
                                    '    newkaskas = item.Replace(" ", "_") & "a" & ".html"
                                    '    IO.File.WriteAllText(System.IO.Path.Combine(locationtosave, (item.Replace(" ", "_") & "a" & ".html")), newqqq)
                                    'Else
                                    '    newkaskas = item.Replace(" ", "_") & ".html"
                                    '    IO.File.WriteAllText(System.IO.Path.Combine(locationtosave, (item.Replace(" ", "_") & ".html")), newqqq)
                                    'End If
                                    'link = "http://admb.xyz/auctions/" & newkaskas
                                Catch ex As Exception

                                End Try
                                link = "None"
                                If description = Nothing Then
                                    description = "None"
                                End If


                                Dim seconds As Long
                                seconds = DateDiff("s", DateTime.Now, m_StopTime)
                                Dim d As TimeSpan = TimeSpan.FromSeconds(seconds)
                                Dim newest = Await ReplyAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}> new auction is hosted by: <@{Context.Message.Author.Id}> , Time left is: `{d.Days.ToString} days & {d.Hours.ToString} hours & {d.Minutes.ToString} minutes & {d.Seconds.ToString} seconds` use `{prefix}acheck` to check time remaining and `{prefix}archeck` to check available channels for auction, `$bid [amount]` to bid e.g `$bid 100`") '& System.Environment.NewLine & $"Here is the link for live timer {link} the time might be later than bots timer by 1 minute.")
                                'Await msg.PinAsync()
                                'Await newest.PinAsync
                                servero.insertauction(Context.Guild.Id, Context.Channel.Id, newest.Id, msg.Id, item, description, payment, startingbid, bidinc, m_StopTime, link, snipeguard.ToString, Context.Message.Author.Id, Context.Message.Author.GetAvatarUrl, 0, 0, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator, "a")

                                Await timerr("dev")
                            End If

                        End If



                    End If
                Else
                    Dim kkwe As String = Nothing
                    Dim num As Integer = 0
                    servero.getallowedchans(Context.Guild.Id)


                    For Each channelid In channelidssss
                        Try
                            kkwe += "- " & Context.Guild.GetChannel(channelid).Name & System.Environment.NewLine
                            num += 1
                        Catch ex As Exception
                        End Try
                    Next
                    If Not num = 0 Then
                        Dim ebd As New EmbedBuilder With {
                                   .Description = $"Auction events are not permitted in this channel." & System.Environment.NewLine & "Allowed channels:" & System.Environment.NewLine & kkwe
                                    }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                    Else
                        Dim ebd As New EmbedBuilder With {
                                   .Description = $"Couldn't find any allowed channels for auctions please tell an admin to use `{prefix}astatus on` and put some channels for auctions."
                                    }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                    End If

                End If

                'here
            Else
                Dim ebd As New EmbedBuilder With {
                                              .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                               }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))

            End If

        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
    <Command("acheck", RunMode:=RunMode.Async)>
    <Summary("Checks auction timer and the channel you run the command on usage: (prefix)acheck")>
    Public Async Function check() As Task
        Try

            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If servero.checkallowedchan(Context.Guild.Id, Context.Channel.Id) = True Then
                    If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then
                        Dim startingbid As Integer = servero.getauctiond(Context.Channel.Id, 1)
                        Dim lastbid As Integer = servero.getauctiond(Context.Channel.Id, 2)
                        Dim lastbidname As String = servero.getauctiond(Context.Channel.Id, 3)
                        Dim snipeguard As String = servero.getauctiond(Context.Channel.Id, 4)
                        Dim m_StopTimet As Date = servero.getauctiond(Context.Channel.Id, 5)
                        Dim payment As String = servero.getauctiond(Context.Channel.Id, 6)
                        Dim item As String = servero.getauctiond(Context.Channel.Id, 7)
                        Dim description As String = servero.getauctiond(Context.Channel.Id, 8)
                        Dim bidinc As Integer = servero.getauctiond(Context.Channel.Id, 9)
                        Dim requestedby As String = servero.getauctiond(Context.Channel.Id, 10)
                        Dim authorp As String = servero.getauctiond(Context.Channel.Id, 11)
                        Dim msgid As Long = servero.getauctiond(Context.Channel.Id, 12)
                        Dim embedid As Long = servero.getauctiond(Context.Channel.Id, 13)
                        Dim authid As Long = servero.getauctiond(Context.Channel.Id, 14)
                        Dim link As String = servero.getauctiond(Context.Channel.Id, 15)
                        Dim lastbidid As Long = servero.getauctiond(Context.Channel.Id, 16)


                        If DateTime.Now >= m_StopTimet Then

                            If lastbid = Nothing Then
                                Await Context.Channel.SendMessageAsync($"Auction Finished, <@{servero.getauctiond(Context.Channel.Id, 14)}> unfortunately there wasn't any winner. {auctionfree}")
                                servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                            Else

                                Await Context.Channel.SendMessageAsync($"Auction Finished" & $" Winner is: <@{lastbidid}> By: {lastbid} {payment}, <@{servero.getauctiond(Context.Channel.Id, 14)}> please contact your auction winner! {auctionfree}")
                                servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                                Try
                                    Dim ebd As New EmbedBuilder With {
                                   .Description = $"You won an auction in `{Context.Channel.Name}` channel in `{Context.Guild.Name}` server, item: `{item}` for `{servero.getauctiond(Context.Channel.Id, 2)}  {payment}`"
                                    }
                                    ebd.Color = Color.Green

                                    Await Context.Guild.GetUser(lastbidid).SendMessageAsync(Nothing, False, ebd.Build)
                                Catch ex As Exception
                                End Try

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


                            Dim ebd As New EmbedBuilder With {
                                               .Description = $"Time remaining for this auction: " & hours & ":" & minutes & ":" & seconds & $" | Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                                }
                            ebd.Color = Color.Green
                            Await Context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
                        End If

                    Else
                        Await ReplyAndDeleteAsync(Context.User.Mention & " No auction is running in this channel.", False, Nothing, TimeSpan.FromSeconds(5))
                    End If
                Else
                    Dim kkwe As String = Nothing
                    servero.getallowedchans(Context.Guild.Id)


                    For Each channelid In channelidssss
                        Try
                            kkwe += "- " & Context.Guild.GetChannel(channelid).Name & System.Environment.NewLine
                        Catch ex As Exception
                        End Try
                    Next

                    Dim ebd As New EmbedBuilder With {
                                      .Description = $"Auction events are not permitted in this channel." & System.Environment.NewLine & "Allowed channels:" & System.Environment.NewLine & kkwe
                                       }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                End If


            Else
                Dim ebd As New EmbedBuilder With {
                                       .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                        }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If

            Await Context.Message.DeleteAsync()
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function
    <Command("archeck", RunMode:=RunMode.Async)>
    <Summary("Checks for running auctions usage: (prefix)archeck")>
    Public Async Function archeck() As Task
        Try

            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                Dim ebd As New EmbedBuilder

                ebd.Color = Color.Green
                Dim desclist As String = Nothing
                Dim available As String = Nothing
                For Each channel In Context.Guild.Channels

                    If servero.checkallowedchan(Context.Guild.Id, channel.Id) = True Then
                        If servero.checkauctionexist(Context.Guild.Id, channel.Id) = True Then

                            Dim requestedby As String = servero.getauctiond(channel.Id, 10)
                            Dim item As String = servero.getauctiond(channel.Id, 7)

                            desclist += $"- <#{channel.Id}> | item name: " & item & " | hosted by: " & requestedby & System.Environment.NewLine
                        Else
                            available += $"- <#{channel.Id}>" & System.Environment.NewLine
                        End If
                    End If

                Next
                Dim lol As String = Nothing
                If Not available = Nothing Then
                    lol = available
                Else
                    lol = "None" & System.Environment.NewLine
                End If
                Dim lol2 As String = Nothing
                If Not desclist = Nothing Then
                    lol2 = desclist
                Else
                    lol2 = "None" & System.Environment.NewLine
                End If
                ebd.Description = $"Available channels: {System.Environment.NewLine & lol}" & System.Environment.NewLine & $"Occupied channels: {System.Environment.NewLine & lol2}"

                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            Else
                Dim ebd As New EmbedBuilder With {
                                       .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                        }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If

            Await Context.Message.DeleteAsync()
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function
    <Command("bid", RunMode:=RunMode.Async)>
    <Summary("Use this command to bid on an auction usage: (prefix)bid [number] to bid on the auction")>
    Public Async Function bid(ByVal issss As Integer) As Task
        Try

            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If servero.checkallowedchan(Context.Guild.Id, Context.Channel.Id) = True Then
                    If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then
                        Dim startingbid As Integer = servero.getauctiond(Context.Channel.Id, 1)
                        Dim lastbid As Integer = servero.getauctiond(Context.Channel.Id, 2)
                        Dim lastbidname As String = servero.getauctiond(Context.Channel.Id, 3)
                        Dim snipeguard As String = servero.getauctiond(Context.Channel.Id, 4)
                        Dim m_StopTimet As Date = servero.getauctiond(Context.Channel.Id, 5)
                        Dim payment As String = servero.getauctiond(Context.Channel.Id, 6)
                        Dim item As String = servero.getauctiond(Context.Channel.Id, 7)
                        Dim description As String = servero.getauctiond(Context.Channel.Id, 8)
                        Dim bidinc As Integer = servero.getauctiond(Context.Channel.Id, 9)
                        Dim requestedby As String = servero.getauctiond(Context.Channel.Id, 10)
                        Dim authorp As String = servero.getauctiond(Context.Channel.Id, 11)
                        Dim msgid As Long = servero.getauctiond(Context.Channel.Id, 12)
                        Dim embedid As Long = servero.getauctiond(Context.Channel.Id, 13)
                        Dim authid As Long = servero.getauctiond(Context.Channel.Id, 14)
                        Dim link As String = servero.getauctiond(Context.Channel.Id, 15)
                        Dim lastbidid As Long = servero.getauctiond(Context.Channel.Id, 16)
                        If Not issss < startingbid Then
                            If issss > lastbid Then
                                If issss = startingbid And lastbid = 0 Then
                                    GoTo hereo
                                ElseIf issss - lastbid >= bidinc Then
hereo:
                                    If DateTime.Now >= m_StopTimet Then
                                        If lastbid = Nothing Then
                                            Await Context.Channel.SendMessageAsync($"Auction Finished, <@{servero.getauctiond(Context.Channel.Id, 14)}> unfortunately there wasn't any winner. {auctionfree}")
                                            servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                                        Else

                                            Await Context.Channel.SendMessageAsync($"Auction Finished" & $" Winner is: <@{lastbidid}> By: {lastbid} {payment}, <@{servero.getauctiond(Context.Channel.Id, 14)}> please contact your auction winner! {auctionfree}")
                                            servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                                            Try
                                                Dim ebd As New EmbedBuilder With {
                                               .Description = $"You won an auction in `{Context.Channel.Name}` channel in `{Context.Guild.Name}` server, item: `{item}` for `{servero.getauctiond(Context.Channel.Id, 2)}  {payment}`"
                                                }
                                                ebd.Color = Color.Green

                                                Await Context.Guild.GetUser(lastbidid).SendMessageAsync(Nothing, False, ebd.Build)
                                            Catch ex As Exception
                                            End Try

                                        End If
                                    Else
                                        Try
                                            Dim ebddd As New EmbedBuilder With {
                                               .Description = $"Somone outbid you in `{Context.Channel.Name}` channel in `{Context.Guild.Name}` server, item: `{item}` for `{issss} {payment}`"
                                                }
                                            ebddd.Color = Color.Green

                                            Await Context.Guild.GetUser(lastbidid).SendMessageAsync(Nothing, False, ebddd.Build)
                                        Catch ex As Exception
                                        End Try
                                        servero.updateauction(Context.Channel.Id, issss, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator, Context.Message.Author.Id, m_StopTimet)

                                        Dim hours As Long
                                        Dim minutes As Long
                                        Dim seconds As Long
                                        seconds = DateDiff("s", DateTime.Now, m_StopTimet)
                                        minutes = seconds \ 60
                                        seconds = seconds - minutes * 60
                                        hours = minutes \ 60
                                        minutes = minutes - hours * 60
                                        Try
                                            Dim ebdi As New EmbedBuilder With {
                                               .Description = "Your bid submitted: " & issss & " " & payment & " In `" & Context.Channel.Name & "` channel in `" & Context.Guild.Name & "` server"
                                                }
                                            ebdi.Color = Color.Green

                                            Await Context.Message.Author.SendMessageAsync(Nothing, False, ebdi.Build)
                                        Catch ex As Exception
                                        End Try
                                        If snipeguard = 1 And hours = 0 And minutes <= 0 And seconds >= 1 Then
                                            Dim time As Date
                                            time = DateAdd("n", 2, m_StopTimet)
                                            servero.updateauction(Context.Channel.Id, issss, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator, Context.Message.Author.Id, time)
                                            Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}> Auction extended by 2 minutes." & " | Last Bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " By: " & servero.getauctiond(Context.Channel.Id, 3))
                                        End If
                                        Dim ebd As New EmbedBuilder With {
                                               .Description = $"Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & servero.getauctiond(Context.Channel.Id, 3) & $" | Time left is: {hours}:{minutes}:{seconds}"
                                                }
                                        ebd.Color = Color.Green
                                        Await Context.Channel.SendMessageAsync(Nothing, False, ebd.Build)
                                    End If
                                ElseIf issss - lastbid < bidinc Then

                                    Try
                                        Await Context.Message.Author.SendMessageAsync($"Your bid in `{Context.Channel.Name}` channel in `{Context.Guild.Name}` server is lower than the bid increment which should be `{bidinc}` `{payment}` your bid was: `{issss}` `{payment}`")
                                    Catch ex As Exception
                                    End Try

                                End If


                            End If

                        End If
                    Else
                        Await ReplyAndDeleteAsync(Context.User.Mention & " No auction is running in this channel.", False, Nothing, TimeSpan.FromSeconds(5))
                    End If
                Else
                    Dim kkwe As String = Nothing
                    servero.getallowedchans(Context.Guild.Id)


                    For Each channelid In channelidssss
                        Try
                            kkwe += "- " & Context.Guild.GetChannel(channelid).Name & System.Environment.NewLine
                        Catch ex As Exception
                        End Try
                    Next

                    Dim ebd As New EmbedBuilder With {
                                      .Description = $"Auction events are not permitted in this channel." & System.Environment.NewLine & "Allowed channels:" & System.Environment.NewLine & kkwe
                                       }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                End If

            Else
                Dim ebd As New EmbedBuilder With {
                                       .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                        }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If


        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    Private aTimer As System.Timers.Timer

    <Command("timer", RunMode:=RunMode.Async)>
    <Summary("Use this command to start live timer on an auction usage: (prefix)timer [status]")>
    Public Async Function timerr(ByVal issss As String) As Task
        Try


            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If servero.checkallowedchan(Context.Guild.Id, Context.Channel.Id) = True Then
                    If servero.getauction(Context.Guild.Id, servero.getauctionservid(Context.Channel.Id, Context.Guild.Id)) = (Context.Channel.Id) Then
                        Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
                        If issss = "on" Then
                            If Not listo.ContainsKey(Context.Channel.Id) = True Then
                                aTimer = New System.Timers.Timer(1000)
                                AddHandler aTimer.Elapsed, AddressOf OnTimedEvent
                                aTimer.AutoReset = True
                                aTimer.Enabled = True
                                listo.Add(Context.Channel.Id, True)
                                Dim ebd As New EmbedBuilder With {
                                             .Description = $"Timer enabled"
                                              }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                                Await Context.Message.DeleteAsync()
                            ElseIf listo.ContainsKey(Context.Channel.Id) = True Then
                                Dim ebd As New EmbedBuilder With {
                                              .Description = $"Timer is already enabled"
                                               }
                                ebd.Color = Color.Red
                                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                                Await Context.Message.DeleteAsync()
                            End If
                        ElseIf issss = "dev" Then
                            If Not listo.ContainsKey(Context.Channel.Id) = True Then
                                aTimer = New System.Timers.Timer(1000)
                                AddHandler aTimer.Elapsed, AddressOf OnTimedEvent
                                aTimer.AutoReset = True
                                aTimer.Enabled = True
                                listo.Add(Context.Channel.Id, True)
                            ElseIf listo.ContainsKey(Context.Channel.Id) = True Then

                            End If
                        ElseIf issss = "off" And thisss.GuildPermissions.ManageMessages = True Then

                            If listo.ContainsKey(Context.Channel.Id) = True Then

                                Dim ebd As New EmbedBuilder With {
                                                  .Description = $"Timer Disabled"
                                                   }
                                ebd.Color = Color.Red
                                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                                listo.Remove(Context.Channel.Id)
                                aTimer.Stop()
                                aTimer.Dispose()
                                Await Context.Message.DeleteAsync()
                            ElseIf Not listo.ContainsKey(Context.Channel.Id) = True Then
                                Dim ebd As New EmbedBuilder With {
                                                                                  .Description = $"Timer is already disabled"
                                                                                   }
                                ebd.Color = Color.Red
                                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                                Await Context.Message.DeleteAsync()
                            End If
                        ElseIf issss = "off" And thisss.GuildPermissions.ManageMessages = False Then
                            Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                            Dim ebd As New EmbedBuilder With {
                                                           .Description = $"You need Manage_Messages permission to use this command."
                                                            }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        End If

                    Else
                        Await ReplyAndDeleteAsync(Context.User.Mention & " No auction is running in this channel.", False, Nothing, TimeSpan.FromSeconds(5))
                    End If
                Else
                    Dim kkwe As String = Nothing
                    servero.getallowedchans(Context.Guild.Id)


                    For Each channelid In channelidssss
                        Try
                            kkwe += "- " & Context.Guild.GetChannel(channelid).Name & System.Environment.NewLine
                        Catch ex As Exception
                        End Try
                    Next

                    Dim ebd As New EmbedBuilder With {
                                      .Description = $"Auction events are not permitted in this channel." & System.Environment.NewLine & "Allowed channels:" & System.Environment.NewLine & kkwe
                                       }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
                End If


            Else
                Dim ebd As New EmbedBuilder With {
                                       .Description = $"This server didnt enable auction program, use {prefix}astatus on to enable it"
                                        }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If


        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function
    <Command("astatus", RunMode:=RunMode.Async)>
    <Summary("Enable or disable auction program in the server usage: (prefix)astatus [on/off]")>
    Public Async Function astatus(ByVal kk As String) As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    Dim update As Boolean = False

                    If servero.checkauctionstatus(Context.Guild.Id) = True Then
                        update = True
                    Else
                        update = False
                    End If

                    If kk = "on" Then

                        If update = True Then
hereeeee:
                            Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Mention auctioners role"
                                                      }
                            ebd.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Dim io = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                            If Not io.MentionedRoles.Count = 0 Then
                                servero.updateauctionstatus(Context.Guild.Id, "True", io.MentionedRoles.First.Id)

                                Dim ebdsss As New EmbedBuilder With {
                                                         .Description = $"Mention the channels you want auctions to be run in"
                                                          }
                                ebdsss.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebdsss.Build, TimeSpan.FromMinutes(2))
herei23848:
                                Dim iow = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                If iow.MentionedChannels.Count = 0 Then
                                    Dim ebdssss As New EmbedBuilder With {
                                                                                      .Description = $"No channel mentions detected, please mention at least 1 channel"
                                                                                       }
                                    ebdssss.Color = Color.Red
                                    Await ReplyAndDeleteAsync(Nothing, False, ebdssss.Build, TimeSpan.FromMinutes(2))
                                    GoTo herei23848
                                Else
                                    For Each channel In iow.MentionedChannels
                                        servero.insertallowedauctionchan(Context.Guild.Id, channel.Id)
                                    Next
                                    Dim ebds As New EmbedBuilder With {
                                                             .Description = $"Auction program got enabled."
                                                              }
                                    ebds.Color = Color.Green
                                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

                                End If
                            Else
                                GoTo hereeeee
                            End If

                        ElseIf update = False Then
hereeeee22:
                            Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Mention auctioners role"
                                                      }
                            ebd.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Dim io = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                            If Not io.MentionedRoles.Count = 0 Then
                                servero.insertauctionstatus(Context.Guild.Id, "True", io.MentionedRoles.First.Id)

                                Dim ebdsss As New EmbedBuilder With {
                                                         .Description = $"Mention the channels you want auctions to be run in"
                                                          }
                                ebdsss.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebdsss.Build, TimeSpan.FromMinutes(2))
herei2223:
                                Dim iow = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                If iow.MentionedChannels.Count = 0 Then
                                    Dim ebdssss As New EmbedBuilder With {
                                                                                      .Description = $"No channel mentions detected, please mention at least 1 channel"
                                                                                       }
                                    ebdssss.Color = Color.Red
                                    Await ReplyAndDeleteAsync(Nothing, False, ebdssss.Build, TimeSpan.FromMinutes(2))
                                    GoTo herei2223
                                Else
                                    For Each channel In iow.MentionedChannels
                                        servero.insertallowedauctionchan(Context.Guild.Id, channel.Id)
                                    Next
                                    Dim ebds As New EmbedBuilder With {
                                                             .Description = $"Auction program got enabled."
                                                              }
                                    ebds.Color = Color.Green
                                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                                End If
                            Else
                                GoTo hereeeee22
                            End If
                        End If

                    ElseIf kk = "off" Then
                        If servero.checkauctionstatus(Context.Guild.Id) = True Then
                            servero.removeauctionstatus(Context.Guild.Id)
                            servero.removeallowedchans(Context.Guild.Id)
                            Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Auction program got disabled."
                                                      }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                        Else

                            Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Auction program is already disabled."
                                                      }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                        End If


                    End If

                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                               .Description = $"You need Manage_Channels permission to use this command."
                                                }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                           .Description = $"I dont have Manage_Channels permission."
                                            }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If


        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    Private dwazdah As Boolean = False
    Private yaks As Boolean = False
    Private sed As Boolean = False
    Private yakd As Boolean = False
    Private das As Boolean = False
    Private stoppp As Boolean = False
    Private Async Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        Try
            If servero.getauctionstatus(Context.Guild.Id, 1) = "True" Then
                If listo.ContainsKey(Context.Channel.Id) = True Then
                    Dim startingbid As Integer = servero.getauctiond(Context.Channel.Id, 1)
                    Dim lastbid As Integer = servero.getauctiond(Context.Channel.Id, 2)
                    Dim lastbidname As String = servero.getauctiond(Context.Channel.Id, 3)
                    Dim snipeguard As String = servero.getauctiond(Context.Channel.Id, 4)
                    Dim m_StopTimet As Date = servero.getauctiond(Context.Channel.Id, 5)
                    Dim payment As String = servero.getauctiond(Context.Channel.Id, 6)
                    Dim item As String = servero.getauctiond(Context.Channel.Id, 7)
                    Dim description As String = servero.getauctiond(Context.Channel.Id, 8)
                    Dim bidinc As Integer = servero.getauctiond(Context.Channel.Id, 9)
                    Dim requestedby As String = servero.getauctiond(Context.Channel.Id, 10)
                    Dim authorp As String = servero.getauctiond(Context.Channel.Id, 11)
                    Dim msgid As Long = servero.getauctiond(Context.Channel.Id, 12)
                    Dim embedid As Long = servero.getauctiond(Context.Channel.Id, 13)
                    Dim authid As Long = servero.getauctiond(Context.Channel.Id, 14)
                    Dim link As String = servero.getauctiond(Context.Channel.Id, 15)
                    Dim lastbidid As Long = servero.getauctiond(Context.Channel.Id, 16)


                    If DateTime.Now >= servero.getauctiond(Context.Channel.Id, 5) And stoppp = False Then
                        If lastbid = Nothing Then

                            If Not stoppp = True Then : Await Context.Channel.SendMessageAsync($"Auction Finished, <@{servero.getauctiond(Context.Channel.Id, 14)}> unfortunately there wasn't any winner. {auctionfree}") : End If
                            listo.Remove(Context.Channel.Id)
                            servero.removeauction(Context.Guild.Id, Context.Channel.Id)
                            stoppp = True
                            aTimer.Stop()
                            aTimer.Dispose()


                        Else

                            If Not stoppp = True Then : Await Context.Channel.SendMessageAsync($"Auction Finished" & $" Winner is: <@{lastbidid}> By: {lastbid} {payment}, <@{servero.getauctiond(Context.Channel.Id, 14)}> please contact your auction winner! {auctionfree}") : End If
                            Try
                                Dim ebd As New EmbedBuilder With {
                                           .Description = $"You won an auction in `{Context.Channel.Name}` channel in `{Context.Guild.Name}` server, item: `{item}` for `{servero.getauctiond(Context.Channel.Id, 2)}  {payment}`"
                                            }
                                ebd.Color = Color.Green

                                If Not stoppp = True Then : Await Context.Guild.GetUser(lastbidid).SendMessageAsync(Nothing, False, ebd.Build) : End If
                            Catch ex As Exception
                            End Try
                            listo.Remove(Context.Channel.Id)
                            servero.removeauction(Context.Guild.Id, Context.Channel.Id)

                            stoppp = True
                            aTimer.Stop()
                            aTimer.Dispose()


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
                                      .Description = $"Less than 12 hour remaining on this auction, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                       }
                            ebddd.Color = Color.Green
                            If Not dwazdah = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                            dwazdah = True
                        ElseIf hours = 0 AndAlso minutes >= 59 AndAlso seconds >= 58 AndAlso yaks = False Then

                            Dim ebddd As New EmbedBuilder With {
                                   .Description = $"Less than 1 hour remaining on this auction, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                    }
                            ebddd.Color = Color.Green
                            If Not yaks = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                            yaks = True

                        ElseIf hours = 0 AndAlso minutes = 29 AndAlso seconds >= 59 AndAlso sed = False Then

                            Dim ebddd As New EmbedBuilder With {
                                  .Description = $"Less than 30 minutes remaining on this auction, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                   }
                            ebddd.Color = Color.Green
                            If Not sed = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                            sed = True
                        ElseIf hours = 0 AndAlso minutes = 0 AndAlso seconds <= 59 AndAlso seconds >= 58 AndAlso yakd = False Then
                            If snipeguard = 1 Then
                                Dim ebddd As New EmbedBuilder With {
                                .Description = $"Less than 1 minute remaining on this auction, Snipeguard is enabled, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                 }
                                ebddd.Color = Color.Green
                                If Not yakd = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                                yakd = True
                            Else
                                Dim ebddd As New EmbedBuilder With {
                               .Description = $"Less than 1 minute remaining on this auction, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                }
                                ebddd.Color = Color.Green
                                If Not yakd = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                                yakd = True
                            End If

                        ElseIf hours = 0 AndAlso minutes = 0 AndAlso seconds <= 30 AndAlso seconds >= 29 AndAlso das = False Then
                            If snipeguard = 1 Then
                                Dim ebddd As New EmbedBuilder With {
                                .Description = $"Less than 30 seconds remaining on this auction, Snipeguard is enabled, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                 }
                                ebddd.Color = Color.Green
                                If Not das = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                                das = True
                            Else
                                Dim ebddd As New EmbedBuilder With {
                               .Description = $"Less than 30 seconds remaining on this auction, Last bid is: " & servero.getauctiond(Context.Channel.Id, 2) & " " & payment & " by: " & lastbidname
                                }
                                ebddd.Color = Color.Green
                                If Not das = True Then : Await Context.Channel.SendMessageAsync($"<@&{servero.getauctionstatus(Context.Guild.Id, 2)}>", False, ebddd.Build) : End If
                                das = True
                            End If
                        End If

                        'Dim ebddds As New EmbedBuilder With {
                        '  .Description = $"This is live timer"
                        '   }
                        'ebddds.Color = Color.Green

                        'Dim kio = Await Context.Channel.SendMessageAsync(Nothing, False, ebddds.Build)
                        'ebddds.Description = $"Time left is: {hours}:{minutes}:{seconds} , Last bid is: " & servero.getauctiond(Context.Channel.Id, 2)
                        '    Await kio.ModifyAsync(Sub(x) x.Embed = ebddds.Build)
                        'Await Context.Message.


                    End If
                End If
            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try


    End Sub
    Function checkdate(ByVal wwwww As String)
        Try
            Dim fields() As String
            Dim hourss As Long
            Dim minutess As Long
            Dim secondss As Long
            Dim kk As Date
            fields = Split(wwwww, ":")
            If fields(0).StartsWith("5") Or fields(0).StartsWith("6") Or fields(0).StartsWith("7") Or fields(0).StartsWith("8") Or fields(0).StartsWith("9") Or fields(0).StartsWith("49") Then
                Return False
            Else
                hourss = fields(0)
                minutess = fields(1)
                secondss = fields(2)

                kk = DateTime.Now
                kk = DateAdd("h", hourss, kk)
                kk = DateAdd("n", minutess, kk)
                kk = DateAdd("s", secondss, kk)
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

End Class