Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports Microsoft.Extensions.Configuration
Imports Discord.Addons.Interactive
Imports Microsoft.Extensions.DependencyInjection
Imports System.Timers
Public Class ADMB


    Public Shared Async Function StartAsync() As Task
        Await New ADMB().RunAsync()
    End Function
    Private Async Function RunAsync() As Task
        Dim config = BuildConfig()
        Using services = ConfigureServices()
            Dim client = services.GetRequiredService(Of DiscordSocketClient)
            services.GetRequiredService(Of LogService)
            Await client.LoginAsync(TokenType.Bot, config("token"))
            Await client.StartAsync()
            Await services.GetRequiredService(Of CommandHandler).InitializeAsync()
            Await Task.Delay(Timeout.Infinite)
        End Using
    End Function
    Private Function BuildConfig() As IConfiguration
        Return New ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory).AddJsonFile("config.json").Build
    End Function

    Private Function ConfigureServices() As ServiceProvider
        Dim collection = New ServiceCollection

        With collection
            .AddSingleton(New DiscordSocketClient(New DiscordSocketConfig With {.MessageCacheSize = 50, .AlwaysDownloadUsers = True, .ExclusiveBulkDelete = True, .LogLevel = LogSeverity.Verbose, .GatewayIntents = GatewayIntents.DirectMessageReactions Or GatewayIntents.DirectMessages Or GatewayIntents.GuildBans Or GatewayIntents.GuildInvites Or GatewayIntents.GuildMembers Or GatewayIntents.GuildMessageReactions Or GatewayIntents.GuildMessages Or GatewayIntents.Guilds Or GatewayIntents.GuildIntegrations Or GatewayIntents.GuildPresences})) ', .TotalShards = 3}))
            .AddSingleton(New CommandService(New CommandServiceConfig With {.LogLevel = LogSeverity.Verbose, .DefaultRunMode = RunMode.Async}))
            .AddSingleton(Of CommandHandler)
            .AddSingleton(Of LogService)
            .AddSingleton(Of InteractiveService)
            .AddSingleton(Of Partner)
        End With

        Return collection.BuildServiceProvider
    End Function

End Class