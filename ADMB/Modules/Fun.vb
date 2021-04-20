Imports System.IO
Imports System.Net
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports Discord.Addons.Interactive
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Color = System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class Fun
    Inherits ModuleBase(Of SocketCommandContext)
	<Command("dice")>
	<Summary("Roll a dice usage: (prefix)dice [optional/number]")>
	Private Async Function DiceAsync(Optional ByVal amount As Integer = 1) As Task
		Try
			If amount < 1 Then
				amount = 1
			ElseIf amount > 100 Then
				amount = 100
			End If

			Dim rnd As New Random()
			Dim rolls As New List(Of Integer)()
			Dim total As Integer = 0
			Dim text As String = ""

			For i As Integer = 0 To amount - 1
				Dim roll As Integer = rnd.Next(1, 7)
				rolls.Add(roll)
				total += roll
				text &= $"**{roll}**, "
			Next i
			text = text.Substring(0, text.Length - 2)

			Dim embed As New EmbedBuilder() With {
				.Title = "Dice Roll",
				.Description = $"You rolled **{amount}** dice for **{total}**! Dice: [{text}]",
				.Color = Color.Green
			}

			Await Context.Channel.SendMessageAsync(Context.Message.Author.Mention, False, embed.Build())
		Catch ex As Exception
		End Try
	End Function
	<Command("hi", RunMode:=RunMode.Async)>
    <[Alias]("hello")>
    <Summary("Replies to the user that typed the command usage: (prefix)hi")>
    Public Function SayAsync() As Task
		Try

			Return ReplyAsync($"Hello {Context.User.Mention}")

		Catch ex As Exception
		End Try
        Return Nothing
    End Function

    <Command("say", RunMode:=RunMode.Async)>
    <Summary("Responds with whatever the user enters after the command trigger usage: (prefix)say [message]")>
    Public Function EchoAsync(<Remainder> ByVal text As String) As Task
		Try

			Return ReplyAsync(text)

		Catch ex As Exception
		End Try
        Return Nothing
    End Function



End Class
