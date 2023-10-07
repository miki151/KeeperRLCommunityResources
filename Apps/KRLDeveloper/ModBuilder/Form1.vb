Public Class Form1

    Public Sub mestart() Handles Me.Load
        KRLExtractor.ExtractEnumsInFiles()
        Dim generator As New Questionnaire()
        'generator.GenerateQuestionnaire(GetType(ModBuilder.game_info.SpellInfo), "C:\PRJ\KeeperRLCommunityResources\Apps\KRLDeveloper\ModBuilder\SpellInfo.txt")
    End Sub

End Class
