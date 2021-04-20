Imports System.IO
Imports System.Net
Imports Discord
Imports Discord.Addons.Interactive
Imports Discord.Commands
Imports Discord.WebSocket
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Timers

Public Class Partner
    Inherits InteractiveBase
    <Command("bump")>
    <Summary("Bumps your server in partner program usage: (prefix)bump")>
    Public Async Function bump() As Task
        Try

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.CreateInstantInvite = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkifpartner(Context.Guild.Id) = False Then
                        Dim ebd As New EmbedBuilder With {
                                                       .Description = $"This server dont have any partner program settings, setup one by using {prefix}partner"
                                                        }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                    Else
                        Dim timecome As Boolean = False
                        If Not servero.getpartnersettings(Context.Guild.Id, 4) = "0" Then

                            Dim dateww As Date
                            Dim seconds As Integer
                            dateww = DateTime.Parse(servero.getpartnersettings(Context.Guild.Id, 4))
                            dateww = DateAdd("h", 12, dateww)
                            dateww = DateAdd("n", 0, dateww)
                            dateww = DateAdd("s", 0, dateww)
                            seconds = DateDiff("s", DateTime.Now, dateww)
                            Dim d As TimeSpan = TimeSpan.FromSeconds(seconds)
                            If DateTime.Now >= dateww Then
                                timecome = True
                            ElseIf DateTime.Now < dateww Then
                                Dim ebd As New EmbedBuilder With {
                                                       .Description = $"You need to wait {d.Hours.ToString} hours and {d.Minutes.ToString} minutes and {d.Seconds.ToString} seconds in order to bump your server again."
                                                        }
                                ebd.Color = Color.Red

                                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                                timecome = False
                            Else

                            End If
                        Else
                            timecome = True
                        End If

                        If timecome = True Then
                            Dim servid As Long
                            Dim member As Integer
                            Dim owner As String
                            Dim channels As Integer
                            servid = Context.Guild.Id
                            member = Context.Guild.MemberCount.ToString
                            owner = Context.Guild.Owner.Username & "#" & Context.Guild.Owner.Discriminator
                            channels = Context.Guild.Channels.Count.ToString
                            Dim eb As New EmbedBuilder With {
                                                       .Title = servero.getpartnersettings(Context.Guild.Id, 1)
             }
                            Dim bot = Context.Client
                            eb.WithCurrentTimestamp()
                            eb.Color = Color.Blue
                            eb.WithAuthor(bot.CurrentUser.Username & " Partner Program", bot.CurrentUser.GetAvatarUrl)
                            If servero.getpartnersettings(Context.Guild.Id, 5) = "Nothing" Then
                                eb.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
                            Else
                                eb.WithThumbnailUrl(servero.getpartnersettings(Context.Guild.Id, 5))
                            End If
                            eb.AddField("Description", $"{servero.getpartnersettings(Context.Guild.Id, 2)}", False)
                            eb.AddField("Server Link", $"[Click here to open the server invite link]({servero.getpartnersettings(Context.Guild.Id, 3)})", False)
                            eb.WithFooter("Members: " & member & " | Channels: " & channels & " | Owner: " & owner)
                            For Each guild In Context.Client.Guilds
                                If servero.checkifpartner(guild.Id) = True Then

                                    If Not servid = guild.Id Then
                                        Dim chn As IMessageChannel = Context.Client.GetChannel(servero.getpartnersettings(guild.Id, 6))
                                        Await chn.SendMessageAsync(Nothing, False, eb.Build)
                                    End If
                                End If
                            Next
                            updatetime(DateTime.Now)
                            Dim ebbbii As New EmbedBuilder With {
                                                       .Description = $"Your server got bumped successfully."
                                                        }
                            ebbbii.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebbbii.Build, TimeSpan.FromMinutes(2))
                        End If
                        'Dim luka As Date = servero.getpartnersettings(Context.Guild.Id, 4)
                        'luka = DateAdd("h", 5, luka)


                        'Await ReplyAndDeleteAsync($"{luka}", False, Nothing, TimeSpan.FromMinutes(5))

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
                                               .Description = $"I dont have Create_Instant_Invite permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    Function updatetime(ByVal time As String)
        servero.updatepartner(Context.Guild.Id, servero.getpartnersettings(Context.Guild.Id, 2), servero.getpartnersettings(Context.Guild.Id, 3), time, servero.getpartnersettings(Context.Guild.Id, 5), servero.getpartnersettings(Context.Guild.Id, 1), servero.getpartnersettings(Context.Guild.Id, 6))
        Return True
    End Function
    <Command("partner", RunMode:=RunMode.Async)>
    <Summary("Register your server in partner program usage: (prefix)partner")>
    Public Async Function partners() As Task
        Try
            Dim lola As Boolean = False
            Dim success As Boolean = False

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
                If Context.Guild.CurrentUser.GuildPermissions.CreateInstantInvite = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If Context.Guild.MemberCount >= 20 Then
                        If Context.Guild.IconUrl IsNot Nothing Then
                            If servero.checkifpartner(Context.Guild.Id) = False Then
                                lola = False
                            Else
replyhereid:
                                Await ReplyAndDeleteAsync("There is an existing partner settings running, do you want to overwrite? yes/no *note this wont reset your bump timer*", False, Nothing, TimeSpan.FromMinutes(5))
                                Dim message223 = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(8))
                                If message223.Content.ToLower = "yes" Then
                                    lola = True
                                ElseIf message223.Content.ToLower = "no" Then
                                    Exit Function
                                    Await ReplyAndDeleteAsync("Got it.", False, Nothing, TimeSpan.FromMinutes(5))
                                Else
                                    GoTo replyhereid
                                End If


                            End If
comehere:
                            Dim bot = Context.Client
                            Dim description1 As String = "1- Everyone should have permission to read the partner channel." & System.Environment.NewLine & "2- No pornographic content, or NSFW content in description." & System.Environment.NewLine & "3- No advertising of servers that trade for in-real currency." & System.Environment.NewLine & "4- No hate or bad speechs in description." & System.Environment.NewLine & "5- No invite links in description, the bot will do it for you." & System.Environment.NewLine & "6- No redirect servers, Example: Making a trash server to redirect users to your original server." & System.Environment.NewLine & System.Environment.NewLine & "By typing agree you agree to all above rules, failing to follow our rules your server will be listed in blacklist and will be excluded from my features."
                            Dim eb As New EmbedBuilder With {
                                                           .Title = "Partner Program Rules",
                                                       .Description = description1
                                                        }
                            eb.WithCurrentTimestamp()
                            '.ImageUrl = "",
                            eb.Color = Color.Blue
                            eb.WithAuthor(bot.CurrentUser.Username & " Partner Program", bot.CurrentUser.GetAvatarUrl)
                            eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                            eb.WithFooter("Partner Requested By: " & Context.User.Username & "#" & Context.User.Discriminator)
letseee:
                            Await ReplyAsync("", False, eb.Build())
                            Await ReplyAndDeleteAsync("Type agree to continue or cancel to cancel.", False, Nothing, TimeSpan.FromMinutes(5))
                            Dim kk = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(8))

                            If kk.Content = "agree" Then
                                Await ReplyAndDeleteAsync("Alright, Write a description about your server 500 character allowed, without invite links", False, Nothing, TimeSpan.FromMinutes(5))
                                Dim message = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(8))
                                Dim desc As String
                                Dim iconurl As String
                                Dim chanid As Long
                                If CommandHandler.Badwords.Any(Function(b) message.Content.ToLower().Contains(b.ToLower())) Then
                                    Await ReplyAndDeleteAsync("Badwords or links are not allowed.", False, Nothing, TimeSpan.FromMinutes(5))

                                    GoTo comehere
                                Else
                                    Dim length As Integer = message.Content.Length
                                    If length > 500 Then
                                        Await ReplyAndDeleteAsync("You cannot type more than 500 characters, please retype your description, your description have " & length & " characters.", False, Nothing, TimeSpan.FromMinutes(5))
                                        GoTo comehere
                                    Else
                                        desc = message.Content
                                        Await ReplyAndDeleteAsync("Alright, Mention the partner channel? *the channel to recieve promotions*", False, Nothing, TimeSpan.FromMinutes(5))
recheckid:
                                        Dim message22 = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(8))

                                        If message22.MentionedChannels.Count = 0 Then
                                            Await ReplyAndDeleteAsync("Please mention a channel.", False, Nothing, TimeSpan.FromMinutes(5))
                                            GoTo recheckid
                                        Else
                                            chanid = message22.MentionedChannels.First.Id
                                            Dim chn As IMessageChannel = Context.Client.GetChannel(chanid)
                                            Await chn.SendMessageAsync("This is a test message for partner program, please use `$report [serverid] [reason]` if you see any server advertise for in-real currency. ")

                                            If Context.Guild.IconUrl = Nothing Then
                                                iconurl = "Nothing"
                                            Else
                                                iconurl = Context.Guild.IconUrl
                                            End If
                                            If lola = True Then
                                                Dim ll = Await Context.Guild.DefaultChannel.CreateInviteAsync(0, Nothing, False, False, Nothing)
                                                servero.updatepartner(Context.Guild.Id, desc, ll.Url, servero.getpartnersettings(Context.Guild.Id, 4), iconurl, Context.Guild.Name, chanid)
                                                Dim ebbbb As New EmbedBuilder With {
                                                       .Description = "Your partner program settings got updated successfully."
                                                        }
                                                ebbbb.Color = Color.Green
                                                Await ReplyAndDeleteAsync(Nothing, False, ebbbb.Build, TimeSpan.FromMinutes(2))
                                            Else
                                                Dim ll = Await Context.Guild.DefaultChannel.CreateInviteAsync(0, Nothing, False, False, Nothing)
                                                servero.inserpartner(Context.Guild.Id, desc, ll.Url, "0", iconurl, Context.Guild.Name, chanid)
                                                Dim ebbbb As New EmbedBuilder With {
                                                           .Description = $"You have registered in partner program successfully, now you can bump your server every 12 hours with {prefix}bump ."
                                                            }
                                                ebbbb.Color = Color.Green
                                                Await ReplyAndDeleteAsync(Nothing, False, ebbbb.Build, TimeSpan.FromMinutes(2))
                                            End If

                                            Await chn.DeleteMessageAsync(chanid)


                                        End If








                                    End If
                                End If
                            ElseIf kk.Content = "cancel" Then
                                Await ReplyAndDeleteAsync("Your partner registration got canceled.", False, Nothing, TimeSpan.FromMinutes(2))
                                Exit Function
                            Else
                                GoTo letseee
                            End If
                        Else
                            Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                            Dim ebd As New EmbedBuilder With {
                                                       .Description = $"Your server need to have an icon to apply for partner program"
                                                        }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        End If

                    Else
                        Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                        Dim ebd As New EmbedBuilder With {
                                                   .Description = $"Your server need to have at least 20 members to apply for partner program"
                                                    }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
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
                                               .Description = $"I dont have Create_Instant_Invite permission."
                                                }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

                End If




        Catch e1 As Exception
            LogService.ClientLog(Nothing, e1.ToString)
        End Try

    End Function
End Class
