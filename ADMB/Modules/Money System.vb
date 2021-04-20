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


Public Class Money_System
    Inherits InteractiveBase
    <Command("exch", RunMode:=RunMode.Async)>
    <Summary("Exchange a currency usage: (prefix)exch [number] [currency name]")>
    Public Async Function exch(ByVal count As Double, ByVal currencyname As String) As Task
        Try


            If servero.checkcurrency(Context.Guild.Id) = True Then
                If currencyname.ToLower.Contains("tek") Then
                    Dim ebd As New EmbedBuilder With {
                                                              .Description = $"{count } {currencyname} Equals to {count} 💸 | {count } tek | {count * servero.getrates(3)} ingots | {count * servero.getrates(4)} polymer | {count * servero.getrates(5)} crystal | {count * servero.getrates(6)} CP"
                                                               }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                ElseIf currencyname.ToLower.Contains("money") Then
                    Dim ebd As New EmbedBuilder With {
                                                                  .Description = $"{count } {currencyname} Equals to {count} 💸 | {count } tek | {count * servero.getrates(3)} ingots | {count * servero.getrates(4)} polymer | {count * servero.getrates(5)} crystal | {count * servero.getrates(6)} CP"
                                                                   }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                ElseIf currencyname.ToLower.Contains("ingot") Then
                    Dim ebd As New EmbedBuilder With {
                                                            .Description = $"{count } {currencyname} Equals to {count / servero.getrates(3)} 💸 | {count / servero.getrates(3)} tek | {count} ingots | {count / 3} polymer | {count / 1.5} crystal | {count / 1.5} CP"
                                                             }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                ElseIf currencyname.ToLower.Contains("poly") Then
                    Dim ebd As New EmbedBuilder With {
                                                           .Description = $"{count } {currencyname} Equals to {count / servero.getrates(4)} 💸 | {count / servero.getrates(4)} tek | {count * 3} ingots | {count} polymer | {count * 2} crystal | {count * 2} CP"
                                                            }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                ElseIf currencyname.ToLower.Contains("crystal") Then
                    Dim ebd As New EmbedBuilder With {
                                                         .Description = $"{count } {currencyname} Equals to  {count / servero.getrates(5)} 💸 | {count / servero.getrates(5)} tek | {count * 1.5} ingots | {count / 2} polymer | {count} crystal | {count} CP"
                                                          }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                ElseIf currencyname.ToLower.Contains("cp") Then
                    Dim ebd As New EmbedBuilder With {
                                                         .Description = $"{count } {currencyname} Equals to  {count / servero.getrates(5)} 💸 | {count / servero.getrates(5)} tek | {count * 1.5} ingots | {count / 2} polymer | {count} crystal | {count} CP"
                                                          }
                    ebd.Color = Color.Green
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                Else
                    Dim ebd As New EmbedBuilder With {
                                                       .Description = $"Couldnt find {currencyname} in currency list, currency list: Tek, Ingots, Polymer, Crystal, CP, Money"
                                                        }
                    ebd.Color = Color.Red
                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                End If
            Else
                Dim ebd As New EmbedBuilder With {
                                               .Description = $"Currency exchange system isnt enabled in this server."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
            End If


        Catch exs As Exception
        End Try
    End Function


    <Command("wallet", RunMode:=RunMode.Async)>
    <Summary("Checks your wallet usage: (prefix)wallet [mention user/optional]")>
    Public Async Function wallet(Optional ByVal user As IUser = Nothing) As Task
        Try
            Dim userrr As Long
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            Dim ifcheck As Boolean = False


            If servero.checkmoneystatus(Context.Guild.Id) = True Then
                If user Is Nothing Then
                    userrr = Context.Message.Author.Id
                ElseIf user IsNot Nothing And thisss.GuildPermissions.ManageChannels = True Then
                    userrr = user.Id
                    ifcheck = True
                ElseIf user IsNot Nothing And thisss.GuildPermissions.ManageChannels = False Then
                    Dim ebd As New EmbedBuilder With {
                                                              .Description = $"Only server users with Manage_Channels permission can check user wallets."
                                                               }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                    Exit Function
                End If
                If servero.ifwalletexist(userrr, Context.Guild.Id) = True Then
                    If ifcheck = True Then
                        Dim ebd As New EmbedBuilder With {
                                                           .Description = $"This user have {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                            }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    ElseIf ifcheck = False Then
                        Dim ebd As New EmbedBuilder With {
                                                         .Description = $"You have {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                          }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    End If
                ElseIf servero.ifwalletexist(userrr, Context.Guild.Id) = False Then

                    If ifcheck = True Then
                        servero.insertwallet(Context.Guild.Id, userrr)
                        Dim ebd As New EmbedBuilder With {
                                                           .Description = $"This user have 0 💸"
                                                            }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    ElseIf ifcheck = False Then
                        servero.insertwallet(Context.Guild.Id, userrr)
                        Dim ebd As New EmbedBuilder With {
                                                         .Description = $"You have 0 💸"
                                                          }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    End If

                End If

            Else
                Dim ebd As New EmbedBuilder With {
                                            .Description = $"Money system isnt enabled in this server."
                                             }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try




    End Function
    <Command("daily", RunMode:=RunMode.Async)>
    <Summary("Get daily points in money system *if server enabled* usage: (prefix)daily")>
    Public Async Function daily() As Task
        Try
            If servero.checkmoneystatus(Context.Guild.Id) = True Then
                If servero.checkdaily(Context.Guild.Id, 1) = True Then
                    If servero.ifwalletexist(Context.Message.Author.Id, Context.Guild.Id) = False Then
                        servero.insertwallet(Context.Guild.Id, Context.Message.Author.Id)
                    End If
                    Dim timecome As Boolean = False
                    If Not servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 2) = "0" Then

                        Dim dateww As Date
                        Dim seconds As Integer
                        dateww = DateTime.Parse(servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 2))
                        dateww = DateAdd("h", 24, dateww)
                        dateww = DateAdd("n", 0, dateww)
                        dateww = DateAdd("s", 0, dateww)
                        seconds = DateDiff("s", DateTime.Now, dateww)
                        Dim d As TimeSpan = TimeSpan.FromSeconds(seconds)
                        If DateTime.Now >= dateww Then
                            timecome = True
                        ElseIf DateTime.Now < dateww Then
                            Dim ebd As New EmbedBuilder With {
                                                   .Description = $"Check again in {d.Hours.ToString} hours and {d.Minutes.ToString} minutes and {d.Seconds.ToString} seconds."
                                                    }
                            ebd.Color = Color.Red

                            Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                            timecome = False
                        Else

                        End If
                    Else
                        timecome = True
                    End If
                    If timecome = True Then
                        servero.updatemoney(Context.Guild.Id, Context.Message.Author.Id, servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1) + servero.checkdaily(Context.Guild.Id, 2), DateTime.Now)
                        Dim ebd As New EmbedBuilder With {
                                                                   .Description = $"You recieved {servero.checkdaily(Context.Guild.Id, 2)} 💸 , Your balance now is: {servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1)} 💸"
                                                                    }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    End If
                Else
                    Dim ebd As New EmbedBuilder With {
                                         .Description = $"Daily check-in isnt enabled in this server."
                                          }
                    ebd.Color = Color.Red
                    Await ReplyAsync(Nothing, False, ebd.Build)
                End If

            Else
                Dim ebd As New EmbedBuilder With {
                                            .Description = $"Money system isnt enabled in this server."
                                             }
                ebd.Color = Color.Red
                Await ReplyAsync(Nothing, False, ebd.Build)

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try




    End Function
    <Command("addm", RunMode:=RunMode.Async)>
    <Summary("Adds amount of money to mentioned user usage: (prefix)addm [number] [mention user]")>
    Public Async Function addm(ByVal amount As Integer, Optional ByVal user As IUser = Nothing) As Task
        Try
            Dim userrr As Long
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            Dim ifcheck As Boolean = False

            If Context.Guild.CurrentUser.GuildPermissions.ManageMessages = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkmoneystatus(Context.Guild.Id) = True Then
                        If user Is Nothing Then
                            userrr = Context.Message.Author.Id
                        ElseIf user IsNot Nothing And thisss.GuildPermissions.ManageChannels = True Then
                            userrr = user.Id
                            ifcheck = True
                        End If
                        If amount >= 0 Then
                            If servero.ifwalletexist(userrr, Context.Guild.Id) = False Then
                                servero.insertwallet(Context.Guild.Id, userrr)
                            End If
                            If ifcheck = True Then
                                servero.updatemoney(Context.Guild.Id, userrr, servero.getmoney(Context.Guild.Id, userrr, 1) + amount, servero.getmoney(Context.Guild.Id, userrr, 2))
                                Dim ebd As New EmbedBuilder With {
                                                                       .Description = $"{amount} 💸 added to user, User's balance now is: {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                                        }
                                ebd.Color = Color.Green
                                Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                Try
                                    Dim ebddd As New EmbedBuilder With {
                                   .Description = $"You received {amount} 💸 from {Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator} in {Context.Guild.Name} server, Your balance now is: {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                    }
                                    ebddd.Color = Color.Green

                                    Await Context.Guild.GetUser(userrr).SendMessageAsync(Nothing, False, ebddd.Build)
                                Catch ex As Exception
                                End Try
                            ElseIf ifcheck = False Then
                                servero.updatemoney(Context.Guild.Id, userrr, servero.getmoney(Context.Guild.Id, userrr, 1) + amount, servero.getmoney(Context.Guild.Id, userrr, 2))
                                Dim ebd As New EmbedBuilder With {
                                                                     .Description = $"{amount} 💸 added to you, Your balance now is: {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                                      }
                                ebd.Color = Color.Green
                                Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                            End If
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"You cant use negative numbers."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If


                    Else
                        Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Money system isnt enabled in this server."
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
                                               .Description = $"I dont have Manage_Messages permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try



    End Function
    <Command("sendm", RunMode:=RunMode.Async)>
    <Summary("Sends amount of money to mentioned user usage: (prefix)sendm [number] [mention user]")>
    Public Async Function sendm(ByVal amount As Integer, ByVal user As IUser) As Task
        Try
            If servero.checkmoneystatus(Context.Guild.Id) = True Then

                If amount >= 0 Then
                    If servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1) - amount >= 0 Then
                        If servero.ifwalletexist(Context.Message.Author.Id, Context.Guild.Id) = True Then
                            If servero.ifwalletexist(user.Id, Context.Guild.Id) = True Then
                                servero.updatemoney(Context.Guild.Id, Context.Message.Author.Id, servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1) - amount, servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 2))
                                servero.updatemoney(Context.Guild.Id, user.Id, servero.getmoney(Context.Guild.Id, user.Id, 1) + amount, servero.getmoney(Context.Guild.Id, user.Id, 2))
                                Dim ebd As New EmbedBuilder With {
                                                                       .Description = $"{amount} 💸 has been sent to <@{user.Id}> successfully, Your balance now is: {servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1)} 💸"
                                                                        }
                                ebd.Color = Color.Green
                                Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                Try
                                    Dim ebddd As New EmbedBuilder With {
                                   .Description = $"You received {amount} 💸 from {Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator} in {Context.Guild.Name} server, Your balance now is: {servero.getmoney(Context.Guild.Id, user.Id, 1)} 💸"
                                    }
                                    ebddd.Color = Color.Green

                                    Await Context.Guild.GetUser(user.Id).SendMessageAsync(Nothing, False, ebddd.Build)
                                Catch ex As Exception
                                End Try
                            ElseIf servero.ifwalletexist(user.Id, Context.Guild.Id) = False Then
                                servero.insertwallet(Context.Guild.Id, user.Id)
                                servero.updatemoney(Context.Guild.Id, Context.Message.Author.Id, servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1) - amount, servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 2))
                                servero.updatemoney(Context.Guild.Id, user.Id, servero.getmoney(Context.Guild.Id, user.Id, 1) + amount, servero.getmoney(Context.Guild.Id, user.Id, 2))
                                Dim ebd As New EmbedBuilder With {
                                                                       .Description = $"{amount} 💸 has been sent to <@{user.Id}> successfully, Your balance now is: {servero.getmoney(Context.Guild.Id, Context.Message.Author.Id, 1)} 💸"
                                                                        }
                                ebd.Color = Color.Green
                                Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                Try
                                    Dim ebddd As New EmbedBuilder With {
                                   .Description = $"You received {amount} 💸 from {Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator} in {Context.Guild.Name} server, Your balance now is: {servero.getmoney(Context.Guild.Id, user.Id, 1)} 💸"
                                    }
                                    ebddd.Color = Color.Green

                                    Await Context.Guild.GetUser(user.Id).SendMessageAsync(Nothing, False, ebddd.Build)
                                Catch ex As Exception
                                End Try
                            End If



                        End If
                    ElseIf servero.ifwalletexist(Context.Message.Author.Id, Context.Guild.Id) = False Then
                        servero.insertwallet(Context.Guild.Id, Context.Message.Author.Id)
                        Dim ebd As New EmbedBuilder With {
                                                              .Description = $"You dont have enough 💸 to send"
                                                                }
                        ebd.Color = Color.Red
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    Else
                        Dim ebd As New EmbedBuilder With {
                                                           .Description = $"You dont have enough 💸 to send"
                                                                }
                        ebd.Color = Color.Red
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    End If


                Else
                    Dim ebd As New EmbedBuilder With {
                                                .Description = $"You cant use negative numbers."
                                                 }
                    ebd.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                End If


            Else
                Dim ebd As New EmbedBuilder With {
                                                .Description = $"Money system isnt enabled in this server."
                                                 }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try


    End Function

    <Command("topm", RunMode:=RunMode.Async)>
    <Summary("Checks for top 10 members in money system usage: (prefix)topm")>
    Public Async Function sendm() As Task
        Try
            If servero.checkmoneystatus(Context.Guild.Id) = True Then
                servero.gettopmoney(Context.Guild.Id)
                Dim ebd As New EmbedBuilder
                ebd.Color = Color.Blue
                Dim orderByVal = topmoney.OrderByDescending(Function(v) v.Value)

                For Each member In orderByVal
                    Try
                        Dim kk As IUser = Context.Client.GetUser(member.Key)
                        ebd.Description += $"- {member.Value} 💸 | {kk.Mention} | ID: {member.Key}" & System.Environment.NewLine
                    Catch ex As Exception
                        ebd.Description += $"- {member.Value} 💸 | {member.Key}" & System.Environment.NewLine
                    End Try
                Next
                Await ReplyAsync(Context.Message.Author.Mention & " Here is top 10 members in money system for this server", False, ebd.Build)

            Else
                Dim ebd As New EmbedBuilder With {
                                                .Description = $"Money system isnt enabled in this server."
                                                 }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try


    End Function
    <Command("takem", RunMode:=RunMode.Async)>
    <Summary("Takes amount of money from mentioned user usage: (prefix)takem [number] [mention user]")>
    Public Async Function takem(ByVal amount As Integer, Optional ByVal user As IUser = Nothing) As Task
        Try
            Dim userrr As Long
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            Dim ifcheck As Boolean = False


            If Context.Guild.CurrentUser.GuildPermissions.ManageMessages = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkmoneystatus(Context.Guild.Id) = True Then
                        If user Is Nothing Then
                            userrr = Context.Message.Author.Id
                        ElseIf user IsNot Nothing And thisss.GuildPermissions.ManageChannels = True Then
                            userrr = user.Id
                            ifcheck = True
                        End If
                        If amount >= 0 Then
                            If servero.getmoney(Context.Guild.Id, userrr, 1) - amount >= 0 Then
                                If servero.ifwalletexist(userrr, Context.Guild.Id) = True Then
                                    If ifcheck = True Then

                                        servero.updatemoney(Context.Guild.Id, userrr, servero.getmoney(Context.Guild.Id, userrr, 1) - amount, servero.getmoney(Context.Guild.Id, userrr, 2))
                                        Dim ebd As New EmbedBuilder With {
                                                                           .Description = $"{amount} 💸 taken from user, User's balance now is: {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                                            }
                                        ebd.Color = Color.Green
                                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)


                                    ElseIf ifcheck = False Then

                                        servero.updatemoney(Context.Guild.Id, userrr, servero.getmoney(Context.Guild.Id, userrr, 1) - amount, servero.getmoney(Context.Guild.Id, userrr, 2))
                                        Dim ebd As New EmbedBuilder With {
                                                                     .Description = $"{amount} 💸 taken from you, Your balance now is: {servero.getmoney(Context.Guild.Id, userrr, 1)} 💸"
                                                                      }
                                        ebd.Color = Color.Green
                                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)

                                    End If


                                End If
                            ElseIf servero.ifwalletexist(userrr, Context.Guild.Id) = False Then
                                servero.insertwallet(Context.Guild.Id, userrr)
                                If ifcheck = True Then
                                    Dim ebd As New EmbedBuilder With {
                                                                             .Description = $"User doesnt have enough 💸"
                                                                            }
                                    ebd.Color = Color.Red
                                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                ElseIf ifcheck = False Then
                                    Dim ebd As New EmbedBuilder With {
                                                                        .Description = $"You dont have enough 💸"
                                                                          }
                                    ebd.Color = Color.Red
                                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                End If
                            ElseIf servero.getmoney(Context.Guild.Id, userrr, 1) - amount < 0 Then
                                If ifcheck = True Then
                                    Dim ebd As New EmbedBuilder With {
                                                                             .Description = $"User doesnt have enough 💸"
                                                                            }
                                    ebd.Color = Color.Red
                                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                ElseIf ifcheck = False Then
                                    Dim ebd As New EmbedBuilder With {
                                                                        .Description = $"You dont have enough 💸"
                                                                          }
                                    ebd.Color = Color.Red
                                    Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                                End If
                            End If
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"You cant use negative numbers."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If


                    Else
                        Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Money system isnt enabled in this server."
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
                                               .Description = $"I dont have Manage_Messages permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try


    End Function
    <Command("resetm", RunMode:=RunMode.Async)>
    <Summary("Resets all users wallet usage: (prefix)resetm")>
    Public Async Function resetm() As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageMessages = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkmoneystatus(Context.Guild.Id) = True Then


                        Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Are you sure you want to reset all users wallet? yes/no"
                                                         }
                        ebd.Color = Color.Green
                        Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                        Dim newu = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                        If newu.Content.ToLower = "yes" Then
                            servero.resetwallets(Context.Guild.Id)
                            Dim ebdddddd As New EmbedBuilder With {
                                                      .Description = $"All users wallet have been resetted successfully"
                                                       }
                            ebdddddd.Color = Color.Green
                            Await ReplyAsync(Context.Message.Author.Mention, False, ebdddddd.Build)
                        ElseIf newu.Content.ToLower = "no" Then
                            Await newu.AddReactionAsync(New Emoji(True_Command))
                        End If


                    Else
                        Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Money system isnt enabled in this server."
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
                                               .Description = $"I dont have Manage_Messages permission."
                                                }
                ebd.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))

            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try


    End Function
    <Command("mstatus", RunMode:=RunMode.Async)>
    <Summary("Turn on/off money system usage: (prefix)mstatus [on/off]")>
    Public Async Function mstatus(ByVal status As String) As Task
        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            Dim pointsget As Double = 0
            Dim dailyget As Integer = 0
            If Context.Guild.CurrentUser.GuildPermissions.ViewChannel = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If status = "on" Then
                        If servero.checkmoneystatus(Context.Guild.Id) = False Then

                            Await ReplyAndDeleteAsync("Do you want to enable daily points for users? yes/no", False, Nothing, TimeSpan.FromMinutes(2))
here:
                            Dim daily = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                            If daily.Content.ToLower = "yes" Then
                                dailyget = 1
                                Await ReplyAndDeleteAsync("How many points you want users get when they check in daily? *only numbers*", False, Nothing, TimeSpan.FromMinutes(2))
here333:
                                Dim points = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(2))
                                Dim kss As Double
                                If Double.TryParse(points.Content, kss) Then
                                    pointsget = points.Content
                                    Try
                                        servero.insertmoneystatus(Context.Guild.Id, dailyget, pointsget)
                                        Dim ebd As New EmbedBuilder With {
                                                              .Description = $"Money system enabled."
                                                               }
                                        ebd.Color = Color.Green
                                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                                    Catch ex As Exception
                                    End Try
                                Else
                                    Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                                    Await ReplyAndDeleteAsync("Didnt understand please type *only numbers*", False, Nothing, TimeSpan.FromMinutes(2))
                                    GoTo here333
                                End If
                            ElseIf daily.Content.ToLower = "no" Then
                                Await daily.AddReactionAsync(New Emoji(True_Command))
                                Try
                                    servero.insertmoneystatus(Context.Guild.Id, 0, 0)
                                    Dim ebd As New EmbedBuilder With {
                                                              .Description = $"Money system enabled."
                                                               }
                                    ebd.Color = Color.Green
                                    Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                                Catch ex As Exception
                                End Try
                                Exit Function
                            Else
                                Await ReplyAndDeleteAsync("I didnt understand, Do you want to enable daily points for users? yes/no", False, Nothing, TimeSpan.FromMinutes(2))
                                GoTo here
                            End If
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Money system is already enabled."
                                                         }
                            ebd.Color = Color.Red
                            Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))

                        End If
                    ElseIf status = "off" Then
                        If servero.checkmoneystatus(Context.Guild.Id) = True Then

                            Try
                                servero.removemoneystatus(Context.Guild.Id)
                                Dim ebd As New EmbedBuilder With {
                                                          .Description = $"Money system disabled."
                                                           }
                                ebd.Color = Color.Green
                                Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromMinutes(2))
                            Catch ex As Exception
                            End Try
                        Else
                            Dim ebd As New EmbedBuilder With {
                                                        .Description = $"Money system isnt enabled."
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