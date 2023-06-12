Imports System.Reflection
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket

Public Class CommandHandler
    Private ReadOnly Client As DiscordSocketClient
    Private ReadOnly CommandService As CommandService
    Private ReadOnly Services As IServiceProvider


    ''' <summary>
    ''' This adds a question mark reaction to invalid command attempts
    ''' Simply remove this line and the usage of <see cref="UNKNOWN_COMMAND"/> in <see cref="HandleCommandAsync(SocketMessage)"/>
    ''' </summary>
    Private Const UNKNOWN_COMMAND As String = "❓"

    Public Sub New(ByVal client As DiscordSocketClient, ByVal commandService As CommandService, ByVal services As IServiceProvider)
        Me.Client = client
        Me.CommandService = commandService
        Me.Services = services
    End Sub

    ''' <summary>
    ''' While this example makes use of Dependency Injection, it is not necessarily required.
    ''' If you are not implementing DI, set the second parameter of AddModulesAsync() to Nothing
    ''' </summary>
    Public Async Function InitializeAsync() As Task
        Await CommandService.AddModulesAsync(Assembly.GetEntryAssembly, Services)
        AddHandler Client.MessageReceived, AddressOf HandleCommandAsync 'Subscribe to the MessageReceived event
    End Function

    ''' <summary>
    ''' Process all incoming messages to determine if they are a command or not
    ''' </summary>
    ''' <param name="message">Incoming Discord message - recieved from server channels and direct messages</param>
    Private Async Function HandleCommandAsync(ByVal message As SocketMessage) As Task
        Dim userMessage = TryCast(message, SocketUserMessage)

        'Ignore system messages and messages from bot accounts
        If userMessage Is Nothing OrElse userMessage.Author.IsBot OrElse userMessage.Author.IsWebhook Then Return

        Dim argPos = 0
        Dim context As New SocketCommandContext(Client, userMessage)

        'THIS IS WHERE YOUR PREFIX IS SET. IF YOU NEED TO CHANGE THE PREFIX YOU'D DO SO HERE
        If Not userMessage.HasMentionPrefix(Client.CurrentUser, argPos) AndAlso
            Not userMessage.HasStringPrefix("?", argPos, StringComparison.OrdinalIgnoreCase) Then Return

        'This action is not necessary, doing this will ignore any white spaces between the prefix and the rest of the user input
        'This mean "?help" and "?    help" will both be treated the same.
        Dim command As String = userMessage.Content.Substring(argPos).Trim

        'The trim above can be excluded and the command String can be replaced with argPos instead
        Dim result = Await CommandService.ExecuteAsync(context, command, Services)

        'Check to see if the result was successful or not.
        'If a command is set to RunMode.Async, IsSuccess will always return true. 
        'For that reason, the CommandService#CommandExecuted event should be hooked to handle post command execution logic
        'In this example, this is done in the LogService
        If Not result.IsSuccess Then
            If result.Error = CommandError.UnknownCommand Then
                Await context.Message.AddReactionAsync(New Emoji(UNKNOWN_COMMAND))
            Else
                Await context.Channel.SendMessageAsync(result.ErrorReason)
            End If
        End If
    End Function

End Class