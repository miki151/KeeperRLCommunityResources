Imports System.IO
Imports System.Threading
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports Microsoft.Extensions.DependencyInjection

Public Class ExampleBot

    ''' <summary>
    ''' The property retrieves your bot token from a text file for use while logging in.
    ''' Since tokens are considered sensitive data, this is one method to avoid hard-coding it.
    ''' </summary>
    Private ReadOnly Property BotToken As String
        Get
            Return IO.File.ReadAllText("C:\PRJ\DiscordBotToken.txt").Trim
        End Get
    End Property

    Public Shared Async Function StartAsync() As Task
        Await New ExampleBot().RunAsync()
    End Function

    'Login and connect to Discord
    Private Async Function RunAsync() As Task
        Using services = ConfigureServices() 'Add required services to the Service Collection
            Dim client = services.GetRequiredService(Of DiscordSocketClient) 'Retrieve your client from the service collection
            services.GetRequiredService(Of LogService) 'Instantiate your logging service

            Await client.LoginAsync(TokenType.Bot, BotToken)
            Await client.StartAsync()

            Await services.GetRequiredService(Of CommandHandler).InitializeAsync() 'Initialize the command handling service

            Await Task.Delay(Timeout.Infinite)  'Infinite delay; Keeps the console open and the bot connected
        End Using
    End Function

    ''' <summary>
    ''' Dependency Injection is not a requirement, however, it's extremely useful when working with the <see cref="CommandService"/>.
    ''' Any services that your various command modules may require can simply be added to the <see cref="ServiceProvider"/> 
    ''' which allows for auto-injection into your service constructors.
    ''' 
    ''' Check out the DI guides for more info ~> https://docs.stillu.cc/guides/commands/dependency-injection.html
    ''' </summary>
    Private Function ConfigureServices() As ServiceProvider
        Dim collection = New ServiceCollection

        With collection
            .AddSingleton(New DiscordSocketClient(New DiscordSocketConfig With {.MessageCacheSize = 25, .LogLevel = LogSeverity.Verbose})) 'cache size is per channel
            .AddSingleton(New CommandService(New CommandServiceConfig With {.LogLevel = LogSeverity.Verbose}))
            .AddSingleton(Of CommandHandler)
            .AddSingleton(Of LogService)
        End With

        Return collection.BuildServiceProvider
    End Function

End Class