Imports System
Imports System.IO
Imports System.Text.RegularExpressions

Public Module Go
    Sub Main()
        Dim qst As New Questionnaire
        qst.questionsFilePath = "C:\PRJ\KeeperRLCommunityResources\Apps\KRLDeveloper\KeeperRLModBuilder\Spell.txt.Questions.txt"
        qst.Main()
    End Sub
End Module

Class Questionnaire

    Public Property questionsFilePath As String

    Sub Main()
        ' Initialize a Dictionary to store user responses.
        Dim questionnaireResponses As New Dictionary(Of String, String)()

        ' Check if the questions file exists.
        If File.Exists(questionsFilePath) Then
            ' Read questions from the text file into an array.
            Dim questions() As String = File.ReadAllLines(questionsFilePath)

            ' Prompt the user for responses.
            Console.WriteLine("Please fill in the questionnaire:")
            For Each question As String In questions
                Dim validOptions As List(Of String) = GetValidOptions(question)

                If validOptions.Count > 0 Then
                    ' Display valid options to the user.
                    Console.WriteLine($"{question}:")
                    Console.WriteLine()

                    ' Prompt until a valid response is provided.
                    Dim response As String = ""
                    While Not validOptions.Contains(response)
                        response = Console.ReadLine().Trim()
                        If Not validOptions.Contains(response) Then
                            Console.WriteLine("Invalid response. Please try again. Valid responses are: (" + Join(validOptions.ToArray(), "/") + ")....")
                        End If
                    End While

                    questionnaireResponses.Add(question, response)
                Else
                    ' Check if the question contains text enclosed in curly braces.
                    Dim match As Match = Regex.Match(question, "\{([^}]+)\}")
                    If match.Success Then
                        ' Extract and print the text inside curly braces to a new line.
                        Dim curlyBraceText As String = match.Groups(1).Value
                        Console.WriteLine(curlyBraceText)
                    End If

                    ' No valid options found or curly braces, just collect the response.
                    Console.Write(question)
                    Console.WriteLine()
                    Dim response As String = Console.ReadLine()
                    questionnaireResponses.Add(question, response)
                End If
            Next

            ' Display the collected responses.
            Console.WriteLine("You have filled in the questionnaire with the following responses:")
            For Each kvp As KeyValuePair(Of String, String) In questionnaireResponses
                Console.WriteLine($"{kvp.Key}: {kvp.Value}")
            Next
        Else
            Console.WriteLine("Questions file not found.")
        End If

        ' You can now use the questionnaireResponses Dictionary to access user responses in your program.
        ' For example: Dim spellName As String = questionnaireResponses("Enter spell name (Start with a capital letter): ")

        ' Optionally, you can save the responses to a file or process them further as needed.

        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Sub

    Function GetValidOptions(question As String) As List(Of String)
        ' Use a regular expression to find valid options enclosed in parentheses.
        Dim pattern As String = "\(([^)]+)\)"
        Dim matches As MatchCollection = Regex.Matches(question, pattern)

        Dim validOptions As New List(Of String)

        For Each match As Match In matches
            ' Split options by '/' and trim whitespace.
            Dim options As String() = match.Groups(1).Value.Split("/"c).Select(Function(s) s.Trim()).ToArray()
            validOptions.AddRange(options)
        Next

        Return validOptions
    End Function
End Class