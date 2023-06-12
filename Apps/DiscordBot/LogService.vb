Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket

Public Class LogService
    Public Sub New(client As DiscordSocketClient, commands As CommandService)
        AddHandler client.Log, AddressOf ClientLog
        AddHandler commands.CommandExecuted, AddressOf CommandLog
    End Sub

    ''' <summary>
    ''' Log client events to the console
    ''' </summary>
    Public Function ClientLog(ByVal message As LogMessage) As Task
        Console.WriteLine(message.ToString)
        Return Task.CompletedTask
    End Function

    ''' <summary>
    ''' Log command error
    ''' PLEASE DO NOT IMPLEMENT THIS AS IS 
    ''' Currently this simply the default error reason to eithe the console or the channel the command was invoked in
    ''' </summary>
    Private Function CommandLog(ByVal info As [Optional](Of CommandInfo),
                                 ByVal context As ICommandContext,
                                 ByVal result As IResult) As Task
        If result.IsSuccess Then Return Task.CompletedTask

        Select Case result.Error.Value
            Case CommandError.UnknownCommand : Return Task.CompletedTask
            Case CommandError.ParseFailed : context.Channel.SendMessageAsync(result.ErrorReason)
            Case CommandError.BadArgCount : Console.WriteLine(result.ErrorReason)
            Case CommandError.ObjectNotFound : context.Channel.SendMessageAsync(result.ErrorReason)
            Case CommandError.MultipleMatches : Console.WriteLine(result.ErrorReason)
            Case CommandError.UnmetPrecondition : context.Channel.SendMessageAsync(result.ErrorReason)
            Case CommandError.Exception : Console.WriteLine(result.ErrorReason)
            Case CommandError.Unsuccessful : context.Channel.SendMessageAsync(result.ErrorReason)
        End Select

        Return Task.CompletedTask
    End Function

End Class