Imports System.Net

Module Program

    Sub Main()
        'ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("
  
░█████╗░██████╗░███╗░░░███╗██████╗░  ██████╗░░█████╗░████████╗
██╔══██╗██╔══██╗████╗░████║██╔══██╗  ██╔══██╗██╔══██╗╚══██╔══╝
███████║██║░░██║██╔████╔██║██████╦╝  ██████╦╝██║░░██║░░░██║░░░
██╔══██║██║░░██║██║╚██╔╝██║██╔══██╗  ██╔══██╗██║░░██║░░░██║░░░
██║░░██║██████╔╝██║░╚═╝░██║██████╦╝  ██████╦╝╚█████╔╝░░░██║░░░
╚═╝░░╚═╝╚═════╝░╚═╝░░░░░╚═╝╚═════╝░  ╚═════╝░░╚════╝░░░░╚═╝░░░
---------------------------------------------------------------")
        Console.WriteLine()
        Dim server As New Server()
        server.createdatabase()
        ADMB.StartAsync().GetAwaiter().GetResult()
    End Sub

End Module 'Unable to resolve service for type 'Discord.WebSocket.DiscordShardedClient' while attempting to activate 'ADMB.CommandHandler'.'
