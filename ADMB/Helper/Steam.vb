Imports Newtonsoft.Json

Public Class Player

    <JsonProperty("steamid")>
    Public Shared Property Steamid As String

    <JsonProperty("communityvisibilitystate")>
    Public Shared Property Communityvisibilitystate As Integer

    <JsonProperty("profilestate")>
    Public Shared Property Profilestate As Integer

    <JsonProperty("personaname")>
    Public Shared Property Personaname As String

    <JsonProperty("profileurl")>
    Public Shared Property Profileurl As String

    <JsonProperty("avatar")>
    Public Shared Property Avatar As String

    <JsonProperty("avatarmedium")>
    Public Shared Property Avatarmedium As String

    <JsonProperty("avatarfull")>
    Public Shared Property Avatarfull As String

    <JsonProperty("avatarhash")>
    Public Shared Property Avatarhash As String

    <JsonProperty("lastlogoff")>
    Public Shared Property Lastlogoff As Integer

    <JsonProperty("personastate")>
    Public Shared Property Personastate As Integer

    <JsonProperty("primaryclanid")>
    Public Shared Property Primaryclanid As String

    <JsonProperty("timecreated")>
    Public Shared Property Timecreated As Integer

    <JsonProperty("personastateflags")>
    Public Shared Property Personastateflags As Integer
End Class

Public Class Response

    <JsonProperty("players")>
    Public Shared Property Players As Player()
End Class

Public Class Playersss

    <JsonProperty("response")>
    Public Shared Property Response As Response
End Class