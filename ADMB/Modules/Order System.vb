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
Public Class Order_System
    Inherits InteractiveBase
    <Command("exportod", RunMode:=RunMode.Async)>
    <Summary("Exports completed orders list to excel usage: (prefix)exportod")>
    Public Async Function export() As Task

        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then

                    Dim ebd As New EmbedBuilder With {
                                              .Description = $"Drink a coffee while im cooking the data for you 🍳"
                                               }
                    ebd.Color = Color.Green
                    Dim k = Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    Await Context.Message.Author.SendFileAsync(servero.ExportCustomerData(Context.Guild.Id, 1), text:=Context.Message.Author.Mention & " Here is your customers completed orders sheet.")
                    Try
                        Dim location As String = System.AppDomain.CurrentDomain.BaseDirectory
                        Dim filename As String = ($"Customersodsheet\{Context.Guild.Id}.xls")
                        Dim fullpath As String = System.IO.Path.Combine(location, filename)

                        System.IO.File.Delete(fullpath)
                    Catch ex As Exception
                    End Try
                    Dim ebds As New EmbedBuilder With {
                                              .Description = $"I sent you via DM, please check your DM's."
                                               }
                    ebds.Color = Color.Green
                    Await k.ModifyAsync(Sub(x)
                                            x.Content = Context.Message.Author.Mention
                                            x.Embed = ebds.Build
                                        End Sub)

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
                                              .Description = $"Couldnt export data for this server."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
            Console.WriteLine(e1.ToString)
        End Try

    End Function
    <Command("exportcod", RunMode:=RunMode.Async)>
    <Summary("Exports current orders list to excel usage: (prefix)exportcod")>
    Public Async Function exportcod() As Task

        Try
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then

                    Dim ebd As New EmbedBuilder With {
                                              .Description = $"Drink a coffee while im cooking the data for you 🍳"
                                               }
                    ebd.Color = Color.Green
                    Dim k = Await ReplyAsync(Context.Message.Author.Mention, False, ebd.Build)
                    Await Context.Message.Author.SendFileAsync(servero.ExportCustomerData(Context.Guild.Id, 2), text:=Context.Message.Author.Mention & " Here is your customers current orders sheet." & System.Environment.NewLine & "Note: In case there is an order that you deleted it in orders queue channel use `$clorders` command it will remove all current onholding orders, or if its in order queue channel react with `❎` or if you cant find it `:negative_squared_cross_mark:` it will cancel the order.")
                    Try
                        Dim location As String = System.AppDomain.CurrentDomain.BaseDirectory
                        Dim filename As String = ($"Customerscodsheet\{Context.Guild.Id}.xls")
                        Dim fullpath As String = System.IO.Path.Combine(location, filename)

                        System.IO.File.Delete(fullpath)
                    Catch ex As Exception
                    End Try
                    Dim ebds As New EmbedBuilder With {
                                              .Description = $"I sent you via DM, please check your DM's."
                                               }
                    ebds.Color = Color.Green
                    Await k.ModifyAsync(Sub(x)
                                            x.Content = Context.Message.Author.Mention
                                            x.Embed = ebds.Build
                                        End Sub)

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
                                              .Description = $"Couldnt export data for this server."
                                               }
            ebds.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebds.Build, TimeSpan.FromMinutes(2))
            Console.WriteLine(e1.ToString)
        End Try

    End Function
    <Command("odtemplate", RunMode:=RunMode.Async)>
    <Summary("Setup a template for sending message to customer in order program usage: (prefix)odtemplate")>
    Public Async Function odtemplate() As Task
        Try

            Dim lola As Boolean = False
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkorder(Context.Guild.Id) = True Then
                        If servero.checkordertemplatescount(Context.Guild.Id) >= 3 Then
                            Await ReplyAsync(Context.Message.Author.Mention & " You cant have more than 3 templates, please remove one in order to make a new one.", False, Nothing)
                            Exit Function


                        Else


                            Await ReplyAsync(Context.Message.Author.Mention & " Alright, what template you want to use? you can use {pin} {vault} , {base} *e.g* `Your order is in {base}, vault number: {vault} and pincode: {pin}`", False, Nothing)
imhere:
                            Dim kk = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                            If kk.Content.Contains("{pin}") Or kk.Content.Contains("{vault}") Or kk.Content.Contains("{base}") Then
                                Dim template As String = kk.Content
                                If CommandHandler.Badwords.Any(Function(b) kk.Content.ToLower().Contains(b.ToLower())) Then
                                    Await ReplyAsync(Context.Message.Author.Mention & " No badwords please.")
                                Else
                                    Await ReplyAsync(Context.Message.Author.Mention & " Name of the template?", False, Nothing)
herepls:
                                    Dim tempname = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                    If CommandHandler.Badwords.Any(Function(b) tempname.Content.ToLower().Contains(b.ToLower())) Then
                                        Await ReplyAsync(Context.Message.Author.Mention & " No badwords please.")
                                    ElseIf tempname.Content.Length >= 25 Then
                                        Await ReplyAsync(Context.Message.Author.Mention & " Name length cant be more than 25 characters.")
                                        GoTo herepls
                                    Else

                                        servero.insertordertemplate(Context.Guild.Id, kk.Content, tempname.Content)

                                        Dim ebd As New EmbedBuilder With {
                                                                           .Description = $"Order template saved successfully"
                                                                            }
                                        ebd.Color = Color.Green
                                        Await ReplyAsync(Nothing, False, ebd.Build)
                                    End If



                                End If

                            Else
                                Await ReplyAndDeleteAsync("Your message template should at least one of the types *e.g* `{base}`", False, Nothing)
                                GoTo imhere
                            End If


                        End If
                    Else
                        Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(5))
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
    <Command("odtemplates", RunMode:=RunMode.Async)>
    <Summary("Gets you the list of your saved order templates usage: (prefix)odtemplates")>
    Public Async Function odtemplates() As Task
        Try

            Dim lola As Boolean = False
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkorder(Context.Guild.Id) = True Then
                        If servero.checkordertemplatescount(Context.Guild.Id) = 0 Then
                            Await ReplyAsync(Context.Message.Author.Mention & " You dont have any saved order templates, use `$odtemplate` to save one.", False, Nothing)
                            Exit Function


                        Else
                            Dim ebd As New EmbedBuilder With {
                                              .Description = servero.getalltemplates(Context.Guild.Id)
                                               }
                            ebd.Color = Color.Green
                            Dim removeme = Await ReplyAsync(Context.Message.Author.Mention & " If you want to delete an order template reply with `-[order template name]` **e.g** `-Template1`", False, ebd.Build)
                            Dim kk = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(1))
                            Dim delete As String = Nothing
                            If kk.Content.StartsWith("-") Then
                                delete = kk.Content.Replace("-", Nothing)
                                If servero.checkordertemplateexist(Context.Guild.Id, delete) = True Then
                                    servero.removeodtemplate(Context.Guild.Id, delete)
                                    Dim ebdw As New EmbedBuilder With {
                                                   .Description = $"Order template removed successfully."
                                                    }
                                    ebdw.Color = Color.Green
                                    Await ReplyAsync(Nothing, False, ebdw.Build)
                                Else
                                    Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} Couldnt find the order template.", False, Nothing, TimeSpan.FromMinutes(5))
                                End If
                            Else
                                Await removeme.DeleteAsync
                            End If

                        End If
                    Else
                        Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(5))
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
    <Command("sendod", RunMode:=RunMode.Async)>
    <Summary("Sends a message to mentioned user id with a the saved message template usage: (prefix)sendod [template name] [userid] [base] [vault number] [pincode]")>
    Public Async Function odtemplate(ByVal name As String, ByVal userid As Long, ByVal base As String, ByVal vaultnumber As Integer, ByVal pincode As Integer) As Task
        Try
            Dim lola As Boolean = False
            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkorder(Context.Guild.Id) = True Then
                        If servero.checkordertemplateexist(Context.Guild.Id, name) = True Then
                            Dim theuser As IUser = Context.Guild.GetUser(userid)
                            Dim template As String = servero.gettemplate(Context.Guild.Id, name)
                            Dim submittemplate As String = template.Replace("{base}", $"**{base}**").Replace("{pin}", $"**{pincode}**").Replace("{vault}", $"**{vaultnumber}**")
                            Dim ebd As New EmbedBuilder With {
                                                          .Description = $"{submittemplate}"
                                                           }
                            ebd.Color = Color.Green
                            ebd.WithCurrentTimestamp()
                            ebd.WithAuthor($"{Context.Guild.Name} Order Confirmation", Context.Guild.IconUrl)
                            ebd.WithFooter("Sent by: " & Context.Guild.Name & " Server Admins")
                            Await theuser.SendMessageAsync(Nothing, False, ebd.Build)
                            Await Context.Message.AddReactionAsync(New Emoji(True_Command))
                        Else
                            Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} Couldnt find the order template.", False, Nothing, TimeSpan.FromMinutes(5))
                        End If
                    Else
                        Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(5))
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
            Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
            Dim ebd As New EmbedBuilder With {
                                               .Description = $"An error occured, please recheck your input."
                                                }
            ebd.Color = Color.Red
            ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function
    <Command("sorder", RunMode:=RunMode.Async)>
    <Summary("Setup order program usage: (prefix)sorder")>
    Public Async Function SetupOrder() As Task
        Try
            Dim lola As Boolean = False

            Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
            If Context.Guild.CurrentUser.GuildPermissions.ManageChannels = True Then
                If thisss.GuildPermissions.ManageChannels = True Then
                    If servero.checkorder(Context.Guild.Id) = False Then
againheree:
                        Await ReplyAndDeleteAsync("Mention the channel you want me to accept orders from?", False, Nothing, TimeSpan.FromMinutes(5))
                        Dim kk = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))

                        If Not kk.MentionedChannels.Count = 0 Then
resubmit:
                            Await ReplyAndDeleteAsync("Mention the channel you want me to submit the orders?", False, Nothing, TimeSpan.FromMinutes(5))
                            Dim qq = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                            Dim getorder As Long
                            getorder = kk.MentionedChannels.First.Id
                            If Not qq.MentionedChannels.Count = 0 Then
                                Dim submitorder As Long
                                submitorder = qq.MentionedChannels.First.Id
resubmit3:
                                Await ReplyAndDeleteAsync("Mention the role you want me to mention when an order gets submitted?", False, Nothing, TimeSpan.FromMinutes(5))
                                Dim wwee = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                If Not wwee.MentionedRoles.Count = 0 Then
                                    Dim rolemen As Long
                                    rolemen = wwee.MentionedRoles.First.Id
Answermeeee:
                                    Dim logcustomer As Boolean = False
                                    Dim logchannel As Long = 0
                                    Await ReplyAndDeleteAsync("Want me to log customers? *To keep track on how many successful orders been made by customer* yes/no", False, Nothing, TimeSpan.FromMinutes(5))
                                    Dim aaaa = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                    If aaaa.Content.ToLower = "yes" Then
                                        logcustomer = True
mentionthechannel:
                                        Await ReplyAndDeleteAsync("Mention the channel you want me to send logs?", False, Nothing, TimeSpan.FromMinutes(5))
                                        Dim aaaaddd = Await NextMessageAsync(True, True, TimeSpan.FromMinutes(5))
                                        If Not aaaaddd.MentionedChannels.Count = 0 Then
                                            logchannel = aaaaddd.MentionedChannels.First.Id
                                            Await ReplyAndDeleteAsync("To mark an order as completed, react with ✅ on the order. reaction name is `:white_check_mark:` if you couldnt find it." & System.Environment.NewLine & "To mark an order as canceled, react with ❎ on the order. reaction name is `:negative_squared_cross_mark:` if you couldnt find it.", False, Nothing, TimeSpan.FromMinutes(5))
                                        Else
                                            GoTo mentionthechannel
                                        End If
                                    ElseIf aaaa.Content.ToLower = "no" Then
                                        GoTo alrightfnally
                                    Else
                                        GoTo Answermeeee
                                    End If
alrightfnally:
                                    If lola = True Then
                                        servero.updateorder(Context.Guild.Id, getorder, submitorder, rolemen, Context.Guild.Name, logcustomer, logchannel)
                                        Await ReplyAndDeleteAsync("Your order settings have been updated successfully.", False, Nothing, TimeSpan.FromMinutes(5))
                                    Else
                                        servero.insertorder(Context.Guild.Id, getorder, submitorder, rolemen, Context.Guild.Name, logcustomer, logchannel)
                                        Await ReplyAndDeleteAsync("All Done.", False, Nothing, TimeSpan.FromMinutes(5))
                                    End If

                                Else
                                    GoTo resubmit3
                                End If

                            Else
                                GoTo resubmit
                            End If
                        Else
                            GoTo againheree

                        End If
                    Else
letmeask:
                        Await ReplyAndDeleteAsync($"This server already have a setup for orders, do you want to re-setup? yes/no", False, Nothing, TimeSpan.FromMinutes(5))
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
    <Command("eorder", RunMode:=RunMode.Async)>
    <Summary("Edit an order usage: (prefix)eorder [orderid]")>
    Public Async Function eorder(ByVal orderid As Long) As Task
        Try

            If servero.checkorder(Context.Guild.Id) = True Then


                If servero.getordersettings(Context.Guild.Id, 1) = Context.Channel.Id Then
                    Dim chn As IMessageChannel = Context.Client.GetChannel(servero.getordersettings(Context.Guild.Id, 2))
                    'Dim kk As IUserMessage = Await chn.GetMessageAsync(orderid)

                    If Not servero.checkiforderlistexist(Context.Guild.Id, orderid) = True Then
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Couldn't find the order or 24 hours passed since you submitted your order.")
                    Else
                        If servero.getnumberorderlist(Context.Guild.Id, orderid, Context.Message.Author.Id) = 0 Then
                            If servero.checkorderlistt(Context.Guild.Id, orderid, Context.Message.Author.Id) = True Then
                                Await Context.Message.Author.SendMessageAsync($"Are you sure you want to edit your order? yes/no *note: you can edit your order only once.*")
                                Dim aaa = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(1))
                                If Not aaa.Channel.GetType.Name = "SocketDMChannel" Then
                                    Exit Function
                                End If
                                If aaa.Content.ToLower = "no".ToLower Then
                                    Exit Function
                                ElseIf aaa.Content.ToLower = "yes".ToLower Then

retryitemm:
                                    Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Alright, Whats your order list?")

                                    Dim message = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(3))
                                    If Not message.Channel.GetType.Name = "SocketDMChannel" Then
                                        Exit Function
                                    End If
                                    Dim item As String = Nothing
                                    Dim charactername As String = Nothing
                                    Dim payeee As String = Nothing
                                    Dim note As String = Nothing
                                    If CommandHandler.Badwords.Any(Function(b) message.Content.ToLower().Contains(b.ToLower())) Then
                                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Badwords are not allowed.")

                                        GoTo retryitemm
                                    Else

                                        item = message.Content
                                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Whats your character name?")
                                        Dim msge = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(2))
                                        If Not msge.Channel.GetType.Name = "SocketDMChannel" Then
                                            Exit Function
                                        End If
                                        charactername = msge.Content
                                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Payment?")
                                        Dim wwwwwww = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(2))
                                        If Not wwwwwww.Channel.GetType.Name = "SocketDMChannel" Then
                                            Exit Function
                                        End If
                                        payeee = wwwwwww.Content
                                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Do you have any notes for your order processor? *e.g* `I need a sleeping bag` type `no` if you dont have any notes.")
                                        Dim wwwwqqqq = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(2))
                                        If Not wwwwqqqq.Channel.GetType.Name = "SocketDMChannel" Then
                                            Exit Function
                                        End If
                                        If Not wwwwqqqq.Content.ToLower = "no".ToLower Then
                                            note = wwwwqqqq.Content
                                        Else
                                            note = "None"
                                        End If
                                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Order Edited. check your order in <#{servero.getordersettings(Context.Guild.Id, 2)}>")
                                        Dim ebe As New EmbedBuilder With {
                                              .Title = "New Order",
                                              .Description = "There is a new order, Details below:"
                                               }
                                        ebe.WithCurrentTimestamp()
                                        '.ImageUrl = "",
                                        ebe.Color = Color.Blue
                                        If Context.User.GetAvatarUrl IsNot Nothing Then
                                            ebe.WithAuthor("New Order By: " & Context.User.Username & "#" & Context.User.Discriminator, Context.User.GetAvatarUrl)
                                            ebe.WithThumbnailUrl(Context.User.GetAvatarUrl)
                                        Else
                                            ebe.WithAuthor("New Order By: " & Context.User.Username & "#" & Context.User.Discriminator, Context.User.GetDefaultAvatarUrl)
                                            ebe.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
                                        End If
                                        ebe.WithFooter("Discord ID:  " & Context.User.Id & " | Order Submitted By: " & Context.User.Username & "#" & Context.User.Discriminator) 'change it
                                        ebe.AddField("Discord Username: ", Context.User.Username & "#" & Context.User.Discriminator, False)
                                        ebe.AddField("Character Name: ", charactername, False)
                                        ebe.AddField("Order List: ", item, False)
                                        ebe.AddField("Payment: ", payeee, False)
                                        ebe.AddField("Notes: ", note, False)
                                        ebe.AddField("Order ID: ", orderid, False)
                                        Dim chnn As IMessageChannel = Context.Client.GetChannel(servero.getordersettings(Context.Guild.Id, 2))
                                        Dim chdn As IMessageChannel = Context.Client.GetChannel(servero.getordersettings(Context.Guild.Id, 3))
                                        Dim msgg As IUserMessage = Await chnn.GetMessageAsync(orderid)
                                        'Await chdn.SendMessageAsync($"<@&{servero.getordersettings(Context.Guild.Id, 3)}> Order ID: {orderid} | Edited by: {Context.User.Username}#{Context.User.Discriminator} in <#{servero.getordersettings(Context.Guild.Id, 2)}> channel, previous order:", False, msgg.Embeds.First.ToEmbedBuilder.Build)
                                        Await msgg.ModifyAsync(Sub(x)
                                                                   x.Content = $"<@&{servero.getordersettings(Context.Guild.Id, 3)}> There is new order by <@{Context.User.Id}>{System.Environment.NewLine}<@{Context.User.Id}> admins will react to this post When its proceed and you will be dm'd when completes.{System.Environment.NewLine}`Order is edited already, you cant edit it anymore.`"
                                                                   x.Embed = ebe.Build
                                                               End Sub)

                                        servero.updateordertolist(Context.Guild.Id, Context.Channel.Id, item, charactername, payeee, note, orderid, Context.Message.Author.Id, 1)
                                        If servero.Checktrack(Context.Guild.Id) = True Then
                                            servero.Updatetrackcustomer(Context.Guild.Id, item, orderid, Context.Message.Author.Id, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                                        End If
                                    End If



                                End If
                            ElseIf servero.checkorderlistt(Context.Guild.Id, orderid, Context.Message.Author.Id) = False Then
                                Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} The order id that you provided doesnt belong to you.")
                            End If
                        Else
                            Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} You reached the limit of editing an order.")
                        End If
                    End If
                End If
            Else
                Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(1))
            End If

        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
    <Command("order", RunMode:=RunMode.Async)>
    <Summary("Starts a new order usage: (prefix)order")>
    Public Async Function order(<Remainder> Optional ByVal idiot As String = Nothing) As Task
        Try

            If servero.checkorder(Context.Guild.Id) = True Then


                If servero.getordersettings(Context.Guild.Id, 1) = Context.Channel.Id Then

retryitemm:
                    Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Alright, Whats your order list?")
                    Dim message = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(3))
                    If Not message.Channel.GetType.Name = "SocketDMChannel" Then
                        Exit Function
                    End If
                    Dim item As String = Nothing
                    Dim charactername As String = Nothing
                    Dim payeee As String = Nothing
                    Dim note As String = Nothing
                    Dim orderid As Long = 0
                    If CommandHandler.Badwords.Any(Function(b) message.Content.ToLower().Contains(b.ToLower())) Then
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Badwords are not allowed.")

                        GoTo retryitemm
                    Else

                        item = message.Content
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Whats your character name?")
                        Dim msge = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(3))
                        If msge.Channel.Id = servero.getordersettings(Context.Guild.Id, 1) AndAlso Not msge.Channel.GetType.Name = "SocketDMChannel" Then
                            Exit Function
                        End If
                        charactername = msge.Content
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Payment?")
                        Dim wwwwwww = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(3))
                        If wwwwwww.Channel.Id = servero.getordersettings(Context.Guild.Id, 1) AndAlso Not wwwwwww.Channel.GetType.Name = "SocketDMChannel" Then
                            Exit Function
                        End If
                        payeee = wwwwwww.Content
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Do you have any notes for your order processor? *e.g* `I need a sleeping bag` type `no` if you dont have any notes.")
                        Dim wwwwqqqq = Await NextMessageAsync(True, False, TimeSpan.FromMinutes(3))
                        If wwwwwww.Channel.Id = servero.getordersettings(Context.Guild.Id, 1) AndAlso Not wwwwwww.Channel.GetType.Name = "SocketDMChannel" Then
                            Exit Function
                        End If
                        If Not wwwwqqqq.Content.ToLower = "no".ToLower Then
                            note = wwwwqqqq.Content
                        Else
                            note = "None"
                        End If
                        Await Context.Message.Author.SendMessageAsync($"{Context.Message.Author.Mention} Order Submitted. check your order in <#{servero.getordersettings(Context.Guild.Id, 2)}>")

                        Dim ebe As New EmbedBuilder With {
                              .Title = "New Order",
                              .Description = "There is a new order, Details below:"
                               }
                        ebe.WithCurrentTimestamp()
                        '.ImageUrl = "",
                        ebe.Color = Color.Blue
                        If Context.User.GetAvatarUrl IsNot Nothing Then
                            ebe.WithAuthor("New Order By: " & Context.User.Username & "#" & Context.User.Discriminator, Context.User.GetAvatarUrl)
                            ebe.WithThumbnailUrl(Context.User.GetAvatarUrl)
                        Else
                            ebe.WithAuthor("New Order By: " & Context.User.Username & "#" & Context.User.Discriminator, Context.User.GetDefaultAvatarUrl)
                            ebe.WithThumbnailUrl(Context.User.GetDefaultAvatarUrl)
                        End If
                        ebe.WithFooter("Discord ID:  " & Context.User.Id & " | Order Submitted By: " & Context.User.Username & "#" & Context.User.Discriminator)
                        ebe.AddField("Discord Username: ", Context.User.Username & "#" & Context.User.Discriminator, False)
                        ebe.AddField("Character Name: ", charactername, False)
                        ebe.AddField("Order List: ", item, False)
                        ebe.AddField("Payment: ", payeee, False)
                        ebe.AddField("Notes: ", note, False)
                        Dim chn As IMessageChannel = Context.Client.GetChannel(servero.getordersettings(Context.Guild.Id, 2))
                        Dim chdn As IMessageChannel = Context.Client.GetChannel(servero.getordersettings(Context.Guild.Id, 3))
                        Dim msgidddd = Await chn.SendMessageAsync($"<@&{servero.getordersettings(Context.Guild.Id, 3)}> There is new order by <@{Context.User.Id}>, <@{Context.User.Id}> admins will react to this post when its proceed and you will be dm'd when completes.", False, ebe.Build())
                        ebe.AddField("Order ID: ", msgidddd.Id, False)
                        Dim msgg As IUserMessage = Await chn.GetMessageAsync(msgidddd.Id)
                        Await msgg.ModifyAsync(Sub(x)
                                                   x.Content = $"<@&{servero.getordersettings(Context.Guild.Id, 3)}> There is new order by <@{Context.User.Id}>{System.Environment.NewLine}<@{Context.User.Id}> admins will react to this post when its proceed and you will be dm'd when completes." & System.Environment.NewLine & $"To edit your order copy this and paste in ordering channel `{prefix}eorder {msgidddd.Id}`" & System.Environment.NewLine & "You can edit your order once and before 24 hours of submitting your order."
                                                   x.Embed = ebe.Build
                                                   End Sub)
                        servero.Insertordertolist(Context.Guild.Id, Context.Channel.Id, item, charactername, payeee, note, msgidddd.Id, Context.Message.Author.Id, DateTime.Now)
                        If servero.Checktrack(Context.Guild.Id) = True Then
                            servero.Inserttrackcustomer(Context.Guild.Id, item, msgidddd.Id, Context.Message.Author.Id, Context.Message.Author.Username & "#" & Context.Message.Author.Discriminator)
                        End If
                    End If

                    End If
                Else
                Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(5))
            End If

        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try

    End Function
    <Command("clorders", RunMode:=RunMode.Async)>
    <Summary("Removes orders list or a specific order in the database*not the channel* usage: (prefix)clorders [optional/orderid]")>
    Public Async Function clorders(Optional orderid As Long = 0) As Task
        Try

            If servero.checkorder(Context.Guild.Id) = True Then
                If orderid = 0 Then
                    Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
                    If thisss.GuildPermissions.ManageMessages = True Then
                        servero.clearorderlist(Context.Guild.Id, 1)
                        Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Orders list database cleared."
                                                      }
                        ebd.Color = Color.Green
                        Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                    Else
                        Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                        Dim ebd As New EmbedBuilder With {
                                                               .Description = $"You need Manage_Messages permission to use this command."
                                                                }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                    End If
                Else
                    Dim thisss = TryCast(Context.Message.Author, SocketGuildUser)
                    If thisss.GuildPermissions.ManageMessages = True Then
                        servero.clearorderlist(Context.Guild.Id, 2, orderid)
                        Dim ebd As New EmbedBuilder With {
                                                     .Description = $"Order have been removed from the database."
                                                      }
                        ebd.Color = Color.Green
                        Await ReplyAndDeleteAsync(Context.Message.Author.Mention, False, ebd.Build, TimeSpan.FromMinutes(2))
                    Else
                        Await Context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
                        Dim ebd As New EmbedBuilder With {
                                                               .Description = $"You need Manage_Messages permission to use this command."
                                                                }
                        ebd.Color = Color.Red
                        Await ReplyAndDeleteAsync(Nothing, False, ebd.Build, TimeSpan.FromSeconds(10))
                    End If
                End If


            Else
                Await ReplyAndDeleteAsync($"{Context.Message.Author.Mention} This server doesnt have an order setting, in order to enable it an admin should use {prefix}sorder", False, Nothing, TimeSpan.FromMinutes(5))
            End If
        Catch ex As Exception
            LogService.ClientLog(Nothing, ex.ToString)
        End Try
    End Function
End Class
