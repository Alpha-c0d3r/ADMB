Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text

Public Enum Emojii
	<StringValue("❤️")>
	RED_HEART
	<StringValue("💚")>
	GREEN_HEART
	<StringValue("💛")>
	YELLOW_HEART
	<StringValue("💙")>
	BLUE_HEART
	<StringValue("🧡")>
	ORANGE_HEART
	<StringValue("💜")>
	PURPLE_HEART
	<StringValue("💖")>
	SPARKLING_HEART
	<StringValue("💘")>
	CUPID_HEART
	<StringValue("😊")>
	SMILE
	<StringValue("🤪")>
	WILD
	<StringValue("🥰")>
	HEARTS
	<StringValue("😇")>
	HALO
	<StringValue("😍")>
	HEARTEYES
	<StringValue("😀")>
	GRIN
End Enum

Public Enum HeartEmoji
		<StringValue("❤️")>
		RED
		<StringValue("💚")>
		GREEN
		<StringValue("💛")>
		YELLOW
		<StringValue("💙")>
		BLUE
		<StringValue("🧡")>
		ORANGE
		<StringValue("💜")>
		PURPLE
		<StringValue("💖")>
		SPARKLING
		<StringValue("💘")>
		CUPID
	End Enum


Public Class StringValue
		Inherits Attribute

		Private ReadOnly _value As String

		Public Sub New(ByVal value_Conflict As String)
			_value = value_Conflict
		End Sub

		Public ReadOnly Property Value() As String
			Get
				Return _value
			End Get
		End Property
	End Class

	Public Module EnumUtil
		Public Function GetString(ByVal value As System.Enum) As String
			Dim output As String = ""
			Dim type As Type = value.GetType()

			Dim fi As FieldInfo = type.GetField(value.ToString())
			Dim attrs() As StringValue = TryCast(fi.GetCustomAttributes(GetType(StringValue), False), StringValue())
			If attrs.Length > 0 Then
				output = attrs(0).Value
			End If

			Return output
		End Function
	End Module
