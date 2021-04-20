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


Public Class Breeder_Tools
    Inherits InteractiveBase
    <Command("breeders", RunMode:=RunMode.Async)>
    <Summary("Get original breeders list usage: (prefix)breeders")>
    Public Async Function bds() As Task
        Try
            servero.getbreeders()
            Dim first As String = Nothing
            Dim num As Integer = 0
            For Each tame As KeyValuePair(Of String, String) In paginatedpages
                Dim strarr() As String = tame.Key.Split("|")
                Dim tamename As String = strarr(0)
                Dim link As String = strarr(1)
                num = num + 1
                first += $"{num} - **{tamename}** {tame.Value} [Click Here To join their server!]({link})" & System.Environment.NewLine
            Next
            Dim pages() = {first}
            Await PagedReplyAsync(pages)
        Catch exs As Exception
            Console.WriteLine(exs.ToString)
        End Try
    End Function
    <Command("addbd", RunMode:=RunMode.Async)>
    <Summary("Adds a user to original breeders list usage: (prefix)addbd [mention user] [serverlink] [tame]")>
    Public Async Function addbd(ByVal user As IUser, ByVal serverlink As String, <Remainder> ByVal tame As String) As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                'If servero.Checkbreederexist(user.Id) = False Then
                servero.insertbreeder(user.Id, serverlink, tame, user.Username)
                    Dim ebds As New EmbedBuilder With {
                                               .Description = $"User added to original breeders list."
                                                }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                    'Else
                    '    Dim ebds As New EmbedBuilder With {
                    '                             .Description = $"User is already in original breeders list."
                    '                              }
                    '    ebds.Color = Color.Red
                    '    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                    'End If



                Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can add a member to original breeders."
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If


            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt add the user to original breeders list."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
        End Try

    End Function
    <Command("delbd", RunMode:=RunMode.Async)>
    <Summary("Deletes a user from original breeders list usage: (prefix)delbd [userid]")>
    Public Async Function delbd(ByVal userid As Long) As Task

        Try


            If Context.Message.Author.Id = devY Or Context.Message.Author.Id = devL Then
                If servero.Checkbreederexist(userid) = True Then
                    servero.deletebreeder(userid)
                    Dim ebds As New EmbedBuilder With {
                                               .Description = $"User removed from original breeders list."
                                                }
                    ebds.Color = Color.Green
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                Else
                    Dim ebds As New EmbedBuilder With {
                                             .Description = $"User isnt in original breeders list."
                                              }
                    ebds.Color = Color.Red
                    Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
                End If



            Else
                Dim ebds As New EmbedBuilder With {
                                                      .Description = $"Only {Context.Client.CurrentUser.Username} staff can delete a member from original breeders."
                                                       }
                ebds.Color = Color.Red
                Await ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(1))
            End If


            Await Context.Message.DeleteAsync()
        Catch e1 As Exception
            Dim ebds As New EmbedBuilder With {
                                              .Description = $"Couldnt delete the user from original breeders list."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
        End Try

    End Function
End Class