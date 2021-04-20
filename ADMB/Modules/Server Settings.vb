Imports System.IO
Imports System.Net
Imports System.Text
Imports Discord
Imports Discord.Addons
Imports Discord.Addons.Interactive
Imports Discord.Commands
Imports Discord.WebSocket
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class Server_Settings
    Inherits InteractiveBase



    <Command("sstatus", RunMode:=RunMode.Async)>
    <Summary("Turn on/off server status usage: (prefix)sstatus [on/off]")>
    Public Async Function sstatus(ByVal status As String) As Task
        Try

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ViewChannel = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If status = "on" Then
                        If servero.checksstats(Context.Guild.Id) = False Then

                            Try
                                servero.insertserverstats(Context.Guild.Id)

                                Dim ebd As New EmbedBuilder With {
                                                          .Description = $"Server status enabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Server status is already enabled."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If
                    ElseIf status = "off" Then
                        If servero.checksstats(Context.Guild.Id) = True Then

                            Try
                                servero.removeserverstatus(Context.Guild.Id)
                                Dim ebd As New EmbedBuilder With {
                                                       .Description = $"Server status disabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Server status isnt enabled."
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
                                               .Description = $"I dont have View_Channel permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If




        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
    <Command("setup", RunMode:=RunMode.Async)>
    <Summary("Setup settings for the bot usage: (prefix)setup")>
    Public Async Function setup() As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ViewChannel = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    Dim lola As Boolean = False
                    Dim thechan As Long = 0
                    Dim logschannel As Long = 0
                    Dim autobanasync As Boolean = False
                    If servero.checksetup(Context.Guild.Id) = False Then
againheree:
                        Try
resubmit2:
                            'should i respond in 1 channel
                            Dim ebddddd As New EmbedBuilder With {
                                           .Description = $"Alright, do you want me to respond to commands in 1 channel? yes/no"
                                            }
                            ebddddd.Color = Color.Green
                            Await ReplyAsync(Context.Message.Author.Mention, False, ebddddd.Build)
                            Dim respondtocommands = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                            If respondtocommands.Content.ToLower = "yes" Then
                                Await respondtocommands.AddReactionAsync(New Emoji(True_Command))
resubmit3:
                                Dim ex1 As New EmbedBuilder With {
                                          .Description = $"Mention the channel you want me to respond in there only?"
                                           }
                                ex1.Color = Color.Green
                                Await ReplyAsync(Context.Message.Author.Mention, False, ex1.Build)
                                Dim mentionedhcnanel = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                                If Not mentionedhcnanel.MentionedChannels.Count = 0 Then

                                    thechan = mentionedhcnanel.MentionedChannels.First.Id
                                    Await mentionedhcnanel.AddReactionAsync(New Emoji(True_Command))
                                Else
                                    GoTo resubmit3
                                End If
                            ElseIf respondtocommands.Content.ToLower = "no" Then
                                Await respondtocommands.AddReactionAsync(New Emoji(True_Command))
                            Else
                                GoTo resubmit2
                            End If

                            'logs channel
                            Dim pai As New EmbedBuilder With {
                                           .Description = $"Mention logs channel? *required*"
                                            }
                            pai.Color = Color.Green
                            Await ReplyAsync(Context.Message.Author.Mention, False, pai.Build)
                            Dim shouldilog = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                            Await respondtocommands.AddReactionAsync(New Emoji(True_Command))
resubmit5:

                            If Not shouldilog.MentionedChannels.Count = 0 Then

                                logschannel = shouldilog.MentionedChannels.First.Id
                                Await shouldilog.AddReactionAsync(New Emoji(True_Command))
                            Else
                                GoTo resubmit5
                            End If

                            'autobanasync
getherei:
                            Dim banas As New EmbedBuilder With {
                                           .Description = $"Do you want to enable auto ban async? yes/no" & System.Environment.NewLine & $"Auto ban async is a function that ban's members that are in your server and {Context.Client.CurrentUser.Username}'s blacklist  with reason, and logs it in log channel"
                                            }
                            banas.Color = Color.Green
                            Await ReplyAsync(Context.Message.Author.Mention, False, banas.Build)
                            Dim banassss = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                            If banassss.Content.ToLower = "yes" Then
                                Await banassss.AddReactionAsync(New Emoji(True_Command))
againask:
                                Await banassss.AddReactionAsync(New Emoji(True_Command))
                                autobanasync = True
                            ElseIf banassss.Content.ToLower = "no" Then
                                Await banassss.AddReactionAsync(New Emoji(True_Command))
                            Else
                                GoTo getherei
                            End If
                            If lola = False Then
                                servero.insertsetup(Context.Guild.Id, thechan, logschannel, autobanasync)
                            ElseIf lola = True Then
                                servero.updatesetup(Context.Guild.Id, thechan, logschannel, autobanasync)
                            End If

                            Dim aaaaaa As New EmbedBuilder With {
                                         .Description = $"Done."
                                          }
                            aaaaaa.Color = Color.Green
                            Await ReplyAsync(Context.Message.Author.Mention, False, aaaaaa.Build)
                        Catch ex As Exception
                            Console.WriteLine(ex.ToString)
                        End Try
                    Else
letmeask:
                        Await ReplyAndDeleteAsync($"This server already have a setup , do you want to re-setup? yes/no", False, Nothing, TimeSpan.FromMinutes(5))
                        Dim wewewe = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                        If wewewe.Content = "yes" Then
                            lola = True
                            GoTo againheree
                        ElseIf wewewe.Content = "no" Then
                            Await wewewe.AddReactionAsync(New Emoji(True_Command))
                            Exit Function
                        Else
                            GoTo letmeask
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
                                               .Description = $"I dont have View_Channel permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try



    End Function
    <Command("cstatus", RunMode:=RunMode.Async)>
    <Summary("Turn on/off currency exchange system usage: (prefix)cstatus [on/off]")>
    Public Async Function cstatus(ByVal status As String) As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ViewChannel = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If status = "on" Then
                        If servero.checkcurrency(Context.Guild.Id) = False Then

                            Try
                                servero.insertcurrencystatus(Context.Guild.Id)
                                Dim ebd As New EmbedBuilder With {
                                                          .Description = $"Currency exchange system enabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Currency exchange system is already enabled."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If
                    ElseIf status = "off" Then
                        If servero.checkmoneystatus(Context.Guild.Id) = True Then

                            Try
                                servero.removecurrencystatus(Context.Guild.Id)
                                Dim ebd As New EmbedBuilder With {
                                                       .Description = $"Currency exchange system disabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Currency exchange system isnt enabled."
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
                                               .Description = $"I dont have View_Channel permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try



    End Function
End Class