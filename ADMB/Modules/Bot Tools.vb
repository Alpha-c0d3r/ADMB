Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Timers
Imports Discord
Imports Discord.Addons
Imports Discord.Addons.Interactive
Imports Discord.Commands
Imports Discord.WebSocket
Imports Newtonsoft.Json

Public Class Bot_Tools
    Inherits InteractiveBase
    Public Shared updatetime As Boolean = False
    Public Shared botlink As String = "https://discord.com/oauth2/authorize?client_id=730132484121165845&scope=bot&permissions=224349"
    <Command("userinfo", RunMode:=RunMode.Async)>
    <[Alias]("whois")>
    <Summary("Displays basic user info usage: (prefix)whois [mention user]")>
    Public Function UserInfo(Optional ByVal user As SocketGuildUser = Nothing) As Task
        Try

            If user Is Nothing Then user = Context.User
            Dim status As UserStatus = user.Status
            Dim embedCol As Color
            Dim isinlocals As Boolean = False
            Dim verified = TryCast(user, IGuildUser).Guild.Roles.FirstOrDefault(Function(x) x.Name = "Verified")
            Dim Epic = TryCast(user, IGuildUser).Guild.Roles.FirstOrDefault(Function(x) x.Name = "Epic Launcher Player")
            If status = UserStatus.Online Then
                embedCol = Color.DarkGreen
            ElseIf status = UserStatus.DoNotDisturb Then
                embedCol = Color.DarkRed
            ElseIf status = UserStatus.AFK OrElse status = UserStatus.Idle Then
                embedCol = Color.Orange
            Else
                embedCol = Color.DarkGrey
            End If
            Dim eb As New EmbedBuilder With {
                    .Color = embedCol
                     }
            eb.WithCurrentTimestamp()
            '.ImageUrl = "",
            Dim nick As String = Nothing
            If Not user.Nickname = Nothing Then


                nick = " | " & user.Nickname
            End If
            Dim ss As String = user.CreatedAt.Day & "/" & user.CreatedAt.Month & "/" & user.CreatedAt.Year
            eb.WithAuthor(user.Username & "#" & user.Discriminator & nick & " | " & user.Status.ToString, user.GetAvatarUrl)
            eb.WithThumbnailUrl(user.GetAvatarUrl)
            If user.GetAvatarUrl = Nothing Then
                eb.WithFooter("Joined At: " & user.JoinedAt.Value.ToString("f") & " | Requested By: " & Context.User.Username, user.GetDefaultAvatarUrl)
            Else
                eb.WithFooter("Joined At: " & user.JoinedAt.Value.ToString("f") & " | Requested By: " & Context.User.Username, user.GetAvatarUrl)
            End If
            Try
                If Context.Client.GetGuild(LocalsServer).GetUser(user.Id) IsNot Nothing Then
                    isinlocals = True
                End If
            Catch ex As Exception

            End Try

            eb.AddField("Discord ID", user.Id, False)
            eb.AddField("Mutual Guilds", user.MutualGuilds.Count, False)
            eb.AddField("Account Creation Date", ss, False)
            If isinlocals = True Then
                If Context.Client.GetGuild(LocalsServer).GetUser(user.Id).Roles.Contains(verified) Then
                    eb.AddField("Is verified in admb:", "Yes", False)
                Else
                    eb.AddField("Is verified in admb:", "No", False)
                End If
                If Context.Client.GetGuild(LocalsServer).GetUser(user.Id).Roles.Contains(Epic) Then
                    eb.AddField("Is epic player in admb:", "Yes", False)
                    eb.Description = $"Caution mentioned user is playing via epic, we dont have a stable verification for epic, deal on your own risk"
                Else
                    eb.AddField("Is epic player in admb:", "No", False)
                End If
            ElseIf isinlocals = False Then
                eb.Description = $"User isnt in official {Context.Client.CurrentUser.Username} server"
            End If
            Return Context.Channel.SendMessageAsync(Context.Message.Author.Mention, False, eb.Build())

        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
        Return Nothing
    End Function
    <Command("binvite", RunMode:=RunMode.Async)>
    <Summary("Gets this bot invite link usage: (prefix)binvite")>
    Public Function binvite() As Task
        Try

            Return ReplyAsync($"{Context.User.Mention} to invite this bot use this link" & System.Environment.NewLine & botlink)
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
        Return Nothing
    End Function

    <Command("ping", RunMode:=RunMode.Async)>
    <[Alias]("latency")>
    <Summary("Shows the websocket connection's latency and time it takes to send a message.")>
    Public Async Function PingAsync() As Task
        Try

            Await Context.Client.SetGameAsync($"in {Context.Client.Guilds.Count} servers")
            Dim watch = Stopwatch.StartNew()
            Dim msg = Await ReplyAsync("Pong")
            Await msg.ModifyAsync(Sub(msgProperty) msgProperty.Content = $"🏓 {watch.ElapsedMilliseconds}ms")
            'Dim msg = Await ReplyAsync("Pong")
            'Dim msgg As IUserMessage = Await Context.Channel.GetMessageAsync(740660021154676756)
            'Await msgg.ModifyAsync(Sub(msgProperty) msgProperty.Content = $"🏓 {watch.ElapsedMilliseconds}ms")
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function

    Private bTimer As System.Timers.Timer
    Private dwazdah As Boolean = False
    Private yaks As Boolean = False
    Private sed As Boolean = False
    Private yakd As Boolean = False
    Private das As Boolean = False
    Private stoppp As Boolean = False
    Private penjchrka As Boolean = False

    <Command("setstatus")>
    <Remarks("botstatus [status]")>
    <Summary("Sets the status of the bot usage: (prefix)setstatus [status]")>
    Public Async Function status(<Remainder> ByVal statss As String) As Task
        Try

            If Context.User.Id = devY Then 'dev id

                Await Context.Client.SetGameAsync(statss)
                Await Task.Delay(0)
                updatetime = True
            Else
                Await ReplyAsync("You can't use this command.")
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("quiz")>
    <Summary("Sets the status of the bot *dedicated to bot developers* usage: (prefix)quiz [on/off]")>
    Public Async Function quiz(ByVal statss As Boolean) As Task
        Try

            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then 'dev id
                If statss = True Then
                    If quizstatus = False Then
                        quizstatus = True
                        Dim ebd As New EmbedBuilder With {
                                         .Description = $"Quiz enabled, accepting answers now."
                                          }
                        ebd.Color = Color.Green
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                    ElseIf quizstatus = True Then
                        Dim ebd As New EmbedBuilder With {
                                         .Description = $"Quiz is already enabled."
                                          }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

                    End If
                ElseIf statss = False Then
                    If quizstatus = True Then
                        quizstatus = False
                        Dim ebd As New EmbedBuilder With {
                                       .Description = $"Quiz disabled, and cleared answers."
                                        }
                        ebd.Color = Color.Green
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        quizanswers.Clear()
                    ElseIf quizstatus = False Then
                        Dim ebd As New EmbedBuilder With {
                                      .Description = $"Quiz is already disabled."
                                       }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                    End If
                End If
                Await Context.Message.DeleteAsync()
            Else
                Await ReplyAsync($"{Context.Message.Author.Mention} You can't use this command.")
            End If
        Catch ex As Exception

        End Try
    End Function
    <Command("help", RunMode:=RunMode.Async)>
    <Summary("All command details, for specific command details use (prefix)help [commandname]")>
    Public Async Function help(Optional ByVal commandname As String = "all") As Task
        Try


            Dim eb As New EmbedBuilder With {
                   .Color = Color.Blue
                   }
            Dim owner As New EmbedBuilder With {
                   .Color = Color.Blue
                   }
            Dim commands As List(Of CommandInfo) = CommandService.Commands.ToList
            Dim kwqw As Boolean = False
            Dim onemod As Boolean = False
            Dim newww As Boolean = False
            Dim neww2 As Boolean = False
            Dim list As String = Nothing
            If commandname = "all" Then
                For Each modulename As ModuleInfo In CommandService.Modules
                    list = Nothing
                    For Each command As CommandInfo In modulename.Commands

                        kwqw = True
                        list += $"`{command.Name}` | "
                    Next
                    eb.AddField(modulename.Name, list)
                Next
            ElseIf Not commandname = "all" Then
                For Each command As CommandInfo In commands
                    If command.Name = commandname Then
                        kwqw = True
                        onemod = True
                        eb.AddField(command.Name, command.Summary.Replace("(prefix)", prefix))
                    End If
                Next
            End If
            Dim bot = Context.Client
            If kwqw = False Then
                eb.Color = Color.Red
                eb.Description = $"Sorry but we couldn't find ({commandname}) in commands list"
            ElseIf kwqw = True Then
                eb.WithCurrentTimestamp()

                If Not onemod = True Then
                    eb.WithAuthor("Command List", bot.CurrentUser.GetAvatarUrl)
                    eb.Description = $"Use `{prefix}help [commandname]` to get more details about specific command"
                Else
                    eb.WithAuthor($"Info about {commandname} command", bot.CurrentUser.GetAvatarUrl)
                End If
                eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                eb.WithFooter("Coded By: " & "Illusion#4331 (Locals) | " & "Requested By: " & Context.User.Username & "#" & Context.User.Discriminator) 'change it
                Await Context.Message.Author.SendMessageAsync(Nothing, False, eb.Build)
            End If
            owner.Description = $"I sent you a DM with your request"
            owner.Color = Color.Green
            Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, owner.Build, TimeSpan.FromMinutes(1))
        Catch ex As Exception

        End Try

    End Function

    <Command("purge", RunMode:=RunMode.Async)>
    <Summary("Deletes bulk messages usage: (prefix)purge [number of messages]")>
    Public Async Function Purge(Optional ByVal amount As Integer = 10) As Task
        Try
            If amount <= 0 Then
                Await ReplyAsync("The amount of messages to remove must be positive.")
                Return
            End If
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageMessages = True Then
                If thisss.GuildPermissions.ManageMessages = True Then
                    Try
                        Dim messages = Await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount).FlattenAsync()
                        Dim filteredMessages = messages.Where(Function(x) (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14)
                        Dim count = filteredMessages.Count()
                        If count = 0 Then
                            Await ReplyAsync("Nothing to delete.")
                        Else
                            Await TryCast(Context.Channel, ITextChannel).DeleteMessagesAsync(filteredMessages)
                            Await ReplyAsync($"Removed {count} {If(count > 1, "messages", "message")} successfully.")
                        End If
                    Catch e1 As Exception

                    End Try

                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                                   .Description = $"You need Manage_Messages permission to use this command."
                                                    }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"I dont have Manage_Messages permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception

        End Try

    End Function
    <Command("atbk", RunMode:=RunMode.Async)>
    <Summary("Adds a user id to blacklist even if its not in the server *Only bot developers can use this command* usage: (prefix)atbk [user id] [reason/optional]")>
    Public Async Function atbk(ByVal user As Long, <Remainder> Optional ByVal reason As String = "Not mentioned") As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If servero.checkuserbanstatus(user, True) = False Then
                    servero.insertuserban(Context.Guild.Id, user, reason, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                    Dim ebds As New EmbedBuilder With {
                                               .Description = $"User added to blacklist."
                                                }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                Else
                    Dim ebds As New EmbedBuilder With {
                                             .Description = $"User is already in blacklist."
                                              }
                    ebds.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                End If



            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can add a member to blacklist."
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If


            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt add the user to blacklist."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
        End Try

    End Function

    <Command("checksb", RunMode:=RunMode.Async)>
    <Summary("Check server subscription expire date usage: (prefix)checksb")>
    Public Async Function checksb(Optional serverid As Long = 0) As Task

        Try

            Dim isowner As Boolean = False
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)

            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If Not serverid = 0 Then

                        If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                            If servero.checkpaid(serverid) = True Then
                                isowner = True
                            Else
                                Dim ebd111 As New EmbedBuilder With {
                                               .Description = $"Mentioned server id dont have any active subscription"
                                                }
                                ebd111.Color = Color.Red
                                Await ReplyAndDeleteAsync(Nothing, False, ebd111.Build, TimeSpan.FromSeconds(10))
                            End If
                        End If


                    End If
                    If isowner = True Then
                        Dim ebdswwwww As New EmbedBuilder With {
                                        .Description = $"Server subscription ends in: {CommandHandler.Checksub(serverid, servero.getpaid(serverid, 1))}" & System.Environment.NewLine & $"They are subscriber since {servero.getpaid(serverid, 2)}"
                                         }
                        ebdswwwww.Color = Color.Green
                        Await ReplyAndDeleteAsync(Nothing, False, ebdswwwww.Build, TimeSpan.FromMinutes(1))
                    ElseIf isowner = False Then
                        If servero.checkpaid(Context.Guild.Id) = True Then
                            Dim ebds As New EmbedBuilder With {
                                             .Description = $"Your subscription ends in: {CommandHandler.Checksub(Context.Guild.Id, servero.getpaid(Context.Guild.Id, 1))}" & System.Environment.NewLine & $"You are subscriber since {servero.getpaid(Context.Guild.Id, 2)}"
                                              }
                            ebds.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
                        Else
                            Dim ebdww As New EmbedBuilder With {
                                               .Description = $"You dont have any active subscriptions, to purchase a subscription please contact one of {Context.Client.CurrentUser.Username} staff in {Format.Url($"official  {Context.Client.CurrentUser.Username} discord server", "https://discord.gg/u9vebds")}"
                                                }
                            ebdww.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebdww.Build, TimeSpan.FromSeconds(10))
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



            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Error occured"
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

        End Try

    End Function
    '<Command("sblist", RunMode:=RunMode.Async)>
    '<Summary("Gets subscribed servers list *Only bot developers can use this command* usage: (prefix)sblist")>
    'Public Async Function sblist() As Task

    '    Try


    '        If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then

    '            Dim bot = Context.Client
    '            Dim eb As New EmbedBuilder With {
    '                                               .Title = "Subscription list",
    '                                               .Description = servero.getblacklist
    '     }

    '            eb.WithCurrentTimestamp()
    '            eb.Color = Color.Blue
    '            Await ReplyAndDeleteAsync(Nothing, False, eb.Build, TimeSpan.FromMinutes(5))

    '        Else
    '            Dim ebds As New EmbedBuilder With {
    '                                                  .Description = $"Only {Context.Client.CurrentUser.Username} staff can check subscriptions list"
    '                                                   }
    '            ebds.Color = Color.Red
    '            Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
    '        End If


    '        Await Context.Message.DeleteAsync()
    '    Catch e1 As Exception
    '        Dim ebds As New EmbedBuilder With {
    '                                          .Description = $"Error occured"
    '                                           }
    '        ebds.Color = Color.Red
    '        ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
    '    End Try

    'End Function
    <Command("delsb", RunMode:=RunMode.Async)>
    <Summary("Removes a server in subscription list *Only bot developers can use this command* usage: (prefix)delsb [serverid]")>
    Public Async Function delsb(ByVal serverid As Long) As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If servero.checkpaid(serverid) = True Then
                    servero.delsbsr(serverid)
                    Dim ebds As New EmbedBuilder With {
                                      .Description = $"{serverid} removed in subscriptions list successfully"
                                       }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                ElseIf servero.checkpaid(serverid) = False Then
                    Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Mentioned serverid dont have any active subscription"
                                                       }
                    ebds.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                End If


            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can delete a server in subscriptions list"
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If


            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Error occured"
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

        End Try

    End Function

    <Command("addsb", RunMode:=RunMode.Async)>
    <Summary("Upgrade a server to premium *Only bot developers can use this command* usage: (prefix)addsb [serverid] [amount] [month(m), day(d)]")>
    Public Async Function addsb(ByVal serverid As Long, ByVal amount As Integer, Optional type As String = "m") As Task

        Try

            Dim total As Integer = Nothing
            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If servero.checkpaid(serverid) = False Then
                    Dim dateww As Date
                    Dim name As String
                    dateww = DateTime.Now
                    If type.ToLower = "d" Then
                        total = amount
                    ElseIf type.ToLower = "m" Then

                        total = 30 * amount
                    Else
                        Exit Function
                    End If

                    Try
                        name = Context.Client.GetGuild(serverid).Name
                    Catch ex As Exception
                        name = "Server"
                    End Try
                    servero.insertpaid(Context.Guild.Id, total, DateTime.Now)
                    Dim ebds As New EmbedBuilder With {
                                           .Description = $"{name} added to paid subscription successfully" & System.Environment.NewLine & $"It will expire in {CommandHandler.Checksub(serverid, amount)}"
                                            }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                ElseIf servero.checkpaid(serverid) = True Then
hereiii:
                    Dim ebdsss As New EmbedBuilder With {
                                                         .Description = $"This server have a subscription already, do you want to extend it? yes/no"
                                                          }
                    ebdsss.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebdsss.Build, TimeSpan.FromMinutes(2))
                    Dim iow = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                    If iow.Content.ToLower = "yes" Then
                        Dim int As Integer = servero.getpaid(serverid, 1)
                        Dim name As String
                        If type.ToLower = "d" Then
                            total = amount + int
                        ElseIf type.ToLower = "m" Then
                            total = 30 * amount + int
                        End If
                        Try
                            name = Context.Client.GetGuild(serverid).Name
                        Catch ex As Exception
                            name = "Server"
                        End Try
                        servero.updatepaid(Context.Guild.Id, total, servero.getpaid(serverid, 2))
                        Dim ebds As New EmbedBuilder With {
                                          .Description = $"{name} server subscription extended" & System.Environment.NewLine & $"It will expire in {CommandHandler.Checksub(serverid, servero.getpaid(serverid, 1))}"
                                           }
                        ebds.Color = Color.Green
                        Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                    ElseIf iow.Content.ToLower = "no" Then
                        Await Context.Message.AddReactionAsync(New Emoji(True_Command))
                        Exit Function
                    Else
                        GoTo hereiii
                    End If

                End If


            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can upgrade a server to premium"
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If


            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Error occured"
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

        End Try

    End Function

    <Command("verify", RunMode:=RunMode.Async)>
    <Summary("Verifies the mentioned epic player usage: (prefix)verify [mention user] [epicid]")>
    Public Async Function Verify(ByVal user As IUser, ByVal epicid As String) As Task

        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.Id = LocalsServer Then
                If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                    If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then

                        If servero.Checkmemberverify(user.Id) = False Then
                            If servero.Checkepicverify(epicid) = False Then
                                servero.insertverification(user.Id, epicid)
                                Dim userkk As IGuildUser = Context.User
                                Dim epicverifiedrole = Context.Guild.Roles.FirstOrDefault(Function(x) x.Id = 740935368505032806)
                                Dim verifiedole = Context.Guild.Roles.FirstOrDefault(Function(x) x.Id = 709877784335351861)
                                Await userkk.AddRoleAsync(verifiedole)
                                Await userkk.AddRoleAsync(epicverifiedrole)
                                Dim ebds As New EmbedBuilder With {
                                                          .Description = $"User has been verified successfully."
                                                           }
                                ebds.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                            Else
                                Dim ebds As New EmbedBuilder With {
                                                                                        .Description = $"There is another discord account linked with this epic id. Discord id: `{servero.Getthememberid(epicid)}`"
                                                                                         }
                                ebds.Color = Color.Red
                                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                            End If
                        Else
                            Dim ebds As New EmbedBuilder With {
                                                        .Description = $"This user already verified with epic. Epic id:`{servero.Gettheepic(user.Id)}`"
                                                         }
                            ebds.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                        End If



                    Else
                        Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                        Dim ebd As New EmbedBuilder With {
                                               .Description = $"I dont have Manage_Channels permission."
                                                }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

                    End If

                End If
            End If

            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Console.WriteLine(e1.ToString)
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt verify the user, make sure its in the server."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

        End Try

    End Function
    <Command("ban", RunMode:=RunMode.Async)>
    <Summary("Bans the mentioned user usage: (prefix)ban [mention user] [reason/optional]")>
    Public Async Function ban(ByVal user As IUser, <Remainder> Optional ByVal reason As String = "Not mentioned") As Task

        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.BanMembers = True Then
                If thisss.GuildPermissions.BanMembers = True Then

                    Await Context.Guild.AddBanAsync(user, 7, reason)
                    Dim ebds As New EmbedBuilder With {
                                                      .Description = $"User banned."
                                                       }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))


                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                               .Description = $"You need Ban_Members permission to use this command."
                                                }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                           .Description = $"I dont have Ban_Members permission."
                                            }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If

            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt ban the user, make sure its in the server."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

        End Try

    End Function

    <Command("banasync", RunMode:=RunMode.Async)>
    <Summary("Bans all members that are in locals blacklist usage: (prefix)banasync")>
    Public Async Function banasync() As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.BanMembers = True Then
                If thisss.GuildPermissions.BanMembers = True Then
                    Try
                        Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Working on it ... ⚒️"
                                                       }
                        ebds.Color = Color.Red
                        Dim kk = Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                        servero.getuserbanlist()
                        Dim kkw As Integer = 0

                        For Each ll In banlist
                            Try
                                Await Context.Guild.AddBanAsync(ll, 7, Nothing)
                                kkw += 1
                            Catch ex As Exception

                            End Try
                        Next
                        Dim ebdws As New EmbedBuilder With {
                                                      .Description = $"{kkw} Members banned successfully."
                                                       }
                        ebdws.Color = Color.Green
                        Await kk.ModifyAsync(Sub(x) x.Embed = ebdws.Build)
                        Await Context.Message.DeleteAsync()
                    Catch e1 As Exception
                        Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Something went wrong."
                                                       }
                        ebds.Color = Color.Red
                        ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                    End Try

                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                               .Description = $"You need Ban_Members permission to use this command."
                                                }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                           .Description = $"I dont have Ban_Members permission."
                                            }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
        End Try

    End Function
    <Command("checkban", RunMode:=RunMode.Async)>
    <Summary("Checks if a member is in blacklist or not usage: (prefix)checkban [userid]")>
    Public Async Function checkban(ByVal userid As Long) As Task

        Try
            Dim ebds As New EmbedBuilder With {
                                                  .Description = $"Checking ... ⚒️"
                                                   }
            ebds.Color = Color.Red
            Dim kk = Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))

            If Not servero.checkuserbanstatus(userid, True) = False Then
                ebds.Description = servero.checkuserbanstatus(userid, False)

                ebds.Color = Color.Green
                Await kk.ModifyAsync(Sub(x) x.Embed = ebds.Build)
            Else
                ebds.Description = $"User isn't in blacklist."

                ebds.Color = Color.Green
                Await kk.ModifyAsync(Sub(x) x.Embed = ebds.Build)
            End If

            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                                  .Description = $"Something went wrong."
                                                   }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
        End Try

    End Function
    <Command("unban", RunMode:=RunMode.Async)>
    <Summary("Unbans a user by id usage: (prefix)unban [userid] [remove from blacklist? true/false]")>
    Public Async Function unban(ByVal user As Long, Optional removeit As Boolean = False) As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.BanMembers = True Then
                If thisss.GuildPermissions.BanMembers = True Then
                    Try
                        If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then

                            Dim nottt As Boolean = True

                            If removeit = True Then
                                If Not servero.checkuserbanstatus(user, True) = False Then
                                    Try
                                        Await Context.Guild.RemoveBanAsync(user)
                                    Catch ex As Exception
                                        nottt = False
                                    End Try
                                    servero.unbanuser(user)
                                    Dim ebds As New EmbedBuilder
                                    ebds.Color = Color.Green

                                    If Not nottt = False Then
                                        ebds.Description = $"User unbanned, and removed from blacklist."
                                    Else
                                        ebds.Description = $"User not found in this server but removed from blacklist."
                                    End If
                                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                                Else
                                    Dim ebds As New EmbedBuilder With {
                                                           .Description = $"User isnt blacklisted."
                                                            }
                                    ebds.Color = Color.Green
                                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                                End If

                            Else
                                Await Context.Guild.RemoveBanAsync(user)
                                Dim ebds As New EmbedBuilder With {
                                                           .Description = $"User unbanned."
                                                            }
                                ebds.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                            End If

                        Else
                            Await Context.Guild.RemoveBanAsync(user)
                            Dim ebds As New EmbedBuilder With {
                                                          .Description = $"User unbanned."
                                                           }
                            ebds.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                        End If
                    Catch e1 As Exception
                        Console.WriteLine(e1.ToString)
                        Dim ebds As New EmbedBuilder With {
                                                        .Description = $"Couldnt find any user matches the user id you provided."
                                                         }
                        ebds.Color = Color.Red
                        ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                    End Try
                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                                   .Description = $"You need Ban_Members permission to use this command."
                                                    }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"I dont have Ban_Members permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception

        End Try

    End Function

    <Command("permissions", RunMode:=RunMode.Async)>
    <Summary("Checks for bot permissions usage: (prefix)permissions")>
    Public Async Function permissions() As Task
        Try

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)

            If thisss.GuildPermissions.ManageChannels = True Then
                Dim ebd As New EmbedBuilder
                Dim kk As String = Nothing
                kk += $"- Adminstrator | {Context.Guild.CurrentUser.GuildPermissions.Administrator.ToString} | If you use order or commands that need to mention admins" & System.Environment.NewLine
                kk += $"- Manage_Channels | {Context.Guild.CurrentUser.GuildPermissions.ManageChannels.ToString} | Needed for most commands" & System.Environment.NewLine
                kk += $"- Ban_Members | {Context.Guild.CurrentUser.GuildPermissions.BanMembers.ToString} | Needed for ban command" & System.Environment.NewLine
                kk += $"- Create_Instant_Invite | {Context.Guild.CurrentUser.GuildPermissions.CreateInstantInvite.ToString} | Needed for partner program" & System.Environment.NewLine
                kk += $"- Read_Messages | {Context.Guild.CurrentUser.GuildPermissions.ReadMessages.ToString} | Needed for all commands" & System.Environment.NewLine
                kk += $"- Read_Message_History | {Context.Guild.CurrentUser.GuildPermissions.ReadMessageHistory.ToString} | Needed for all commands" & System.Environment.NewLine
                kk += $"- Send_Messages | {Context.Guild.CurrentUser.GuildPermissions.SendMessages.ToString} | Needed for all commands" & System.Environment.NewLine
                kk += $"- View_Channels | {Context.Guild.CurrentUser.GuildPermissions.ViewChannel.ToString} | Needed for most commands" & System.Environment.NewLine
                kk += $"- Embed_Links | {Context.Guild.CurrentUser.GuildPermissions.EmbedLinks.ToString} | Needed for partner program" & System.Environment.NewLine
                kk += $"- Add_Reactions | {Context.Guild.CurrentUser.GuildPermissions.EmbedLinks.ToString} | Optional to react to messages" & System.Environment.NewLine
                kk += $"- Manage_Messages | {Context.Guild.CurrentUser.GuildPermissions.ManageMessages.ToString} | Needed for most commands, and to make channel clean when typing a command" & System.Environment.NewLine
                kk += $"- Mention @everyone | {Context.Guild.CurrentUser.GuildPermissions.MentionEveryone.ToString} | Optional in case you want to ping all in auction program" & System.Environment.NewLine
                Dim iffalse As Boolean = False
                If kk.Contains("False") Then
                    iffalse = True
                Else
                    iffalse = False
                End If
                kk = kk.Replace("False", "❌").Replace("True", "✅")
                ebd.Description = kk
                If iffalse = True Then
                    ebd.Color = Color.Red
                Else
                    ebd.Color = Color.Green
                End If

                Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(1))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                           .Description = $"You need Manage_Channels permission to use this command."
                                            }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If



        Catch ex As Exception
        End Try
    End Function
    <Command("bans", RunMode:=RunMode.Async)>
    <Summary("bans a specific server usage: (prefix)bans [serverid] [reason]")>
    Public Async Function banserver(ByVal serverid As Long, <Remainder> Optional ByVal reason As String = "Nothing") As Task
        Try
            If Context.User.Id = devY Or Context.User.Id = devL Then
                If Not servero.checkblacklist(serverid) Then
                    Dim bot = Context.Client
                    Dim ol As Long
                    If Long.TryParse(serverid, ol) Then
                        If reason = "Nothing" Then
                            Dim ebd As New EmbedBuilder With {
                                             .Description = $"Please write the reason {prefix}ban [serverid] [reason]"
                                              }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                        Else

                            servero.insertban(serverid, reason, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                            Dim ebd As New EmbedBuilder With {
                                              .Description = $"Server got banned successfully."
                                               }
                            ebd.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                        End If
                    End If

                Else
                    Dim ebd As New EmbedBuilder With {
                                              .Description = $"This server is already in blacklist."
                                               }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                End If
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can ban a server."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("unbans", RunMode:=RunMode.Async)>
    <Summary("Removes a server from blacklist usage: (prefix)unbans [serverid]")>
    Public Async Function unban(ByVal serverid As Long) As Task
        Try
            If servero.checkblacklist(serverid) Then
                If Context.User.Id = devY Or Context.User.Id = devL Then 'dev id
                    servero.unbanserver(serverid)
                    Dim ebd As New EmbedBuilder With {
                                                   .Description = $"Server got unbanned successfully."
                                                    }
                    ebd.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                                   .Description = $"Only {Context.Client.CurrentUser.Username} staff can unban a server."
                                                    }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                End If
            Else
                Dim ebd As New EmbedBuilder With {
                                                 .Description = $"This server isnt in blacklist"
                                                  }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("clblacklist", RunMode:=RunMode.Async)>
    <Summary("Removes all banned servers from blacklist usage: (prefix)clblacklist")>
    <RequireBotPermission(GuildPermission.ViewChannel)>
    Public Async Function clblacklist() As Task
        Try
            If Context.User.Id = devY Then 'dev id
                servero.removeblacklist()
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"All banned servers got removed from blacklist successfully."
                                                }
                ebd.Color = Color.Green
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can clear blacklist."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("clreports", RunMode:=RunMode.Async)>
    <Summary("Removes all reports usage: (prefix)clreports")>
    <RequireBotPermission(GuildPermission.ViewChannel)>
    Public Async Function clreports() As Task
        Try
            If Context.User.Id = devY Or Context.User.Id = devL Then 'dev id
                servero.removereports()
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"All reports got removed successfully."
                                                }
                ebd.Color = Color.Green
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can clear reports."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("blacklist", RunMode:=RunMode.Async)>
    <Summary("Checks for blacklist usage: (prefix)blacklist")>
    Public Async Function blacklist() As Task
        Try
            If Context.User.Id = devY Or Context.User.Id = devL Then 'dev id

                Dim bot = Context.Client
                Dim eb As New EmbedBuilder With {
                                                   .Title = "Blacklist",
                                                   .Description = servero.getblacklist
         }
                eb.WithCurrentTimestamp()
                eb.Color = Color.Blue
                eb.WithAuthor(bot.CurrentUser.Username & " Blacklist", bot.CurrentUser.GetAvatarUrl)
                eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                eb.WithFooter("Requested by: " & Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                Await ReplyAndDeleteAsync(Nothing, False, eb.Build, TimeSpan.FromMinutes(5))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can check blacklist."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("chreports", RunMode:=RunMode.Async)>
    <Summary("Checks for reports list usage: (prefix)chreports")>
    Public Async Function chreports() As Task
        Try
            If Context.User.Id = devY Or Context.User.Id = devL Then 'dev id

                Dim bot = Context.Client
                Dim eb As New EmbedBuilder With {
                                                   .Title = "Reports List",
                                                   .Description = servero.getreports
         }
                eb.WithCurrentTimestamp()
                eb.Color = Color.Blue
                eb.WithAuthor(bot.CurrentUser.Username & " Partner Program Report List", bot.CurrentUser.GetAvatarUrl)
                eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                eb.WithFooter("Requested by: " & Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                Await ReplyAndDeleteAsync(Nothing, False, eb.Build, TimeSpan.FromMinutes(5))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can check reports list."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("guilds", RunMode:=RunMode.Async)>
    <Summary("Lists all servers that the bot is in *staff only* usage: (prefix)guilds")>
    Public Async Function guilds() As Task
        Try
            If Context.User.Id = devY Or Context.User.Id = devL Then 'dev id

                Dim bot = Context.Client
                Dim eb As New EmbedBuilder
                Dim nummm As Integer = 0
                For Each guild In Context.Client.Guilds
                    Try
                        Dim ll = Await guild.DefaultChannel.CreateInviteAsync(0, Nothing, False, False, Nothing)
                        eb.AddField($"{guild.Name} | {guild.Id}", $"[Join]({ll.Url})", False)
                        nummm += 1
                    Catch ex As Exception
                    End Try
                Next
                eb.WithCurrentTimestamp()
                eb.Color = Color.Blue
                eb.WithAuthor(bot.CurrentUser.Username & $" Guilds list {nummm} guilds", bot.CurrentUser.GetAvatarUrl)
                eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                eb.WithFooter("Requested by: " & Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                Await ReplyAndDeleteAsync(Nothing, False, eb.Build, TimeSpan.FromMinutes(5))
            Else
                Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Only {Context.Client.CurrentUser.Username} staff can check guilds list."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            End If
        Catch ex As Exception
        End Try
    End Function
    <Command("report", RunMode:=RunMode.Async)>
    <Summary("Report a server or a user usage: (prefix)report [user/server name or id] [reason without spaces]")>
    Public Async Function report(ByVal kk As String, <Remainder> ByVal reason As String) As Task
        Try
            servero.insertreport(Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator & "|" & Context.Message.Author.Id, kk, reason)
            Dim bot = Context.Client
            Dim eb As New EmbedBuilder With {
                                               .Title = "New report"
     }
            eb.WithCurrentTimestamp()
            eb.Color = Color.Blue
            eb.WithAuthor(bot.CurrentUser.Username & " Partner Program Report", bot.CurrentUser.GetAvatarUrl)
            eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
            eb.AddField("Reported Server Name/ID", $"{kk}", False)
            eb.AddField("Reason", $"{reason}", False)
            eb.WithFooter("Reported by: " & Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator & "|" & Context.Message.Author.Id)
            Dim chn As IMessageChannel = Context.Client.GetChannel(733395609020792833)
            Await chn.SendMessageAsync(Nothing, False, eb.Build)
            Dim ebd As New EmbedBuilder With {
                                        .Description = $"Report have been submitted and sended {Context.Client.CurrentUser.Username} staff, we will check the case as soon as possible, if your case is high priority please submit your report in {Context.Client.CurrentUser.Username} official discord"
                                         }
            ebd.Color = Color.Green
            ebd.AddField("Bots Official Discord", $"[Click here](https://discord.gg/uqdkxv3)", False)
            Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))

        Catch ex As Exception
        End Try
    End Function
    <Command("sscheck")>
    <Summary("Checks server status usage: (prefix)sscheck")>
    Public Async Function sscheck() As Task
        Try

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checksstats(Context.Guild.Id) = True Then
                        Dim ebd As New EmbedBuilder
                        ebd.Color = Color.Green
                        ebd.WithThumbnailUrl(Context.Guild.IconUrl)
                        ebd.WithAuthor(Context.Guild.Name, Context.Guild.IconUrl)
                        ebd.AddField("Server Name", Context.Guild.Name)
                        ebd.AddField("Server ID", Context.Guild.Id)
                        ebd.AddField("Creation Date", Context.Guild.CreatedAt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture))
                        ebd.AddField("Members", Context.Guild.MemberCount - Context.Guild.Users.Where(Function(x) x.IsBot).Count)
                        ebd.AddField("Roles Count", Context.Guild.Roles.Count)
                        ebd.AddField("Emojis Count", Context.Guild.Emotes.Count)
                        ebd.AddField("Region", Context.Guild.VoiceRegionId)
                        ebd.AddField("Security Level", Context.Guild.VerificationLevel)
                        ebd.AddField("Text Channels", Context.Guild.TextChannels.Count)
                        ebd.AddField("Voice Channels", Context.Guild.VoiceChannels.Count)
                        ebd.WithFooter("Requested by: " & Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                    Else
                        Dim ebd As New EmbedBuilder With {
                                                      .Description = $"Server status isnt enabled in this server, {prefix}sstatus on to enable it.."
                                                       }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))


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
        End Try

    End Function
End Class