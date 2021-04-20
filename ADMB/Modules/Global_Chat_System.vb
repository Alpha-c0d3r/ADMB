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
Imports Newtonsoft.Json.Linq
Public Class Global_Chat_System
    Inherits InteractiveBase
    <Command("glmute", RunMode:=RunMode.Async)>
    <Summary("Mutes a user in global chat *Only bot developers can use this command* usage: (prefix)glmute [user id]")>
    Public Async Function glmute(ByVal userid As Long) As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If servero.checkmuted(userid) = False Then
                    servero.insertmute(userid)
                    For Each guild In Context.Client.Guilds
                        If servero.checkglobalchat(guild.Id) = True Then
                            If globalchat IsNot Nothing Then
                                For Each user As KeyValuePair(Of Long, Long) In globalchat
                                    Try

                                        If user.Value = userid Then
                                            Dim chn As IMessageChannel = Context.Client.GetChannel(servero.getglchat(guild.Id, 1))
                                            Dim kk As IUserMessage = Await chn.GetMessageAsync(user.Key)
                                            If kk.Channel.Id = chn.Id Then
                                                Await chn.DeleteMessageAsync(kk.Id)
                                            End If


                                        End If
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If

                        End If
                    Next
                    Dim ebds As New EmbedBuilder With {
                                                   .Description = $"User muted successfully."
                                                    }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                Else
                    Dim ebds As New EmbedBuilder With {
                                                 .Description = $"User is already muted."
                                                  }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                End If

            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can mute a member."
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If




            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt mute the user."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
            'LogService.ClientLog(Nothing, e1.ToString)
        End Try

    End Function
    <Command("glunmute", RunMode:=RunMode.Async)>
    <Summary("Unmutes a user in global chat *Only bot developers can use this command* usage: (prefix)glunmute [user id]")>
    Public Async Function glunmute(ByVal userid As Long) As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If Not servero.checkmuted(userid) = False Then
                    servero.removemute(userid)
                    Dim ebds As New EmbedBuilder With {
                                                   .Description = $"User unmuted successfully."
                                                    }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                Else
                    Dim ebds As New EmbedBuilder With {
                                                 .Description = $"User isnt muted."
                                                  }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                End If

            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can mute a member."
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If




            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt mute the user."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
            LogService.ClientLog(Nothing, e1.ToString)
        End Try

    End Function
    <Command("glmcheck", RunMode:=RunMode.Async)>
    <Summary("Checks if the user is muted in global chat usage: (prefix)glmcheck [user id]")>
    Public Async Function glmcheck(ByVal userid As Long) As Task

        Try


            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageMessages = True Then
                If thisss.GuildPermissions.ManageMessages = True Then
                    Try
                        If servero.checkmuted(userid) = False Then
                            Dim ebd As New EmbedBuilder With {
                                                  .Description = $"User isnt muted."
                                                   }
                            ebd.Color = Color.Green
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                 .Description = $"User is muted."
                                                  }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                        End If
                    Catch e1 As ArgumentOutOfRangeException
                        Return
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



            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt check the user."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
        End Try

    End Function
    <Command("glchat", RunMode:=RunMode.Async)>
    <Summary("Turn on/off global chat system usage: (prefix)glchat [on/off]")>
    Public Async Function glchat(ByVal status As String) As Task
        Try
            Dim lola As Boolean = False

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)

            If Context.Guild.CurrentUser.GuildPermissions.ViewChannel = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If status = "on" Then
                        If servero.checkglobalchat(Context.Guild.Id) = False Then
againheree:
                            Try
resubmit2:

                                Await ReplyAndDeleteAsync("Mention the channel you want it to be the global chat channel?", False, Nothing, TimeSpan.FromMinutes(5))
                                Dim ww = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                If Not ww.MentionedChannels.Count = 0 Then
                                    Dim glchannelid As Long
                                    glchannelid = ww.MentionedChannels.First.Id
                                    If lola = True Then
                                        servero.updateglobalchat(Context.Guild.Id, glchannelid)
                                    Else
                                        servero.insertglobalchat(Context.Guild.Id, glchannelid)
                                    End If

                                    Dim ebd As New EmbedBuilder With {
                                                              .Description = $"Global chat system enabled."
                                                               }
                                    ebd.Color = Color.Green
                                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                                    Dim chn As IMessageChannel = Context.Client.GetChannel(glchannelid)
                                    Dim bot = Context.Client
                                    Dim description1 As String = "1- No spamming." & System.Environment.NewLine & "2- No pornographic content, or NSFW content." & System.Environment.NewLine & "3- No advertising." & System.Environment.NewLine & "4- No hate or bad speechs." & System.Environment.NewLine & "5- Following this server rules." & System.Environment.NewLine & "6- You agree that any message you send here will be sended to all other servers that global chat system is enabled."
                                    Dim eb As New EmbedBuilder With {
                                                           .Title = "Global Chat Rules",
                                                       .Description = description1
                                                        }
                                    eb.WithCurrentTimestamp()
                                    eb.Color = Color.Blue
                                    eb.WithAuthor(bot.CurrentUser.Username & " Global Chat System", bot.CurrentUser.GetAvatarUrl)
                                    eb.WithThumbnailUrl(bot.CurrentUser.GetAvatarUrl)
                                    eb.WithFooter("Global Chat Enabled By: " & Context.User.Username & "#" & Context.User.Discriminator)
                                    Dim aa = Await chn.SendMessageAsync(Nothing, False, eb.Build)
                                    Await aa.PinAsync()
                                Else
                                    GoTo resubmit2
                                End If

                            Catch ex As Exception
                                Console.WriteLine(ex.ToString)
                            End Try
                        Else
letmeask:
                            Await ReplyAndDeleteAsync($"This server already have a setup for global chat system, do you want to re-setup? yes/no", False, Nothing, TimeSpan.FromMinutes(5))
                            Dim wewewe = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                            If wewewe.Content = "yes" Then
                                lola = True
                                GoTo againheree
                            ElseIf wewewe.Content = "no" Then
                                Await ReplyAndDeleteAsync($"Alright.", False, Nothing, TimeSpan.FromMinutes(5))
                                Exit Function
                            Else
                                GoTo letmeask
                            End If

                        End If
                    ElseIf status = "off" Then
                        If servero.checkglobalchat(Context.Guild.Id) = True Then

                            Try
                                servero.removeglobalchat(Context.Guild.Id)
                                Dim ebd As New EmbedBuilder With {
                                                          .Description = $"Global chat system disabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Global chat system isnt enabled."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If
                    End If

                Else
                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                    Dim ebd As New EmbedBuilder With {
                                                   .Description = $"You need View_Channel permission to use this command."
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
End Class
