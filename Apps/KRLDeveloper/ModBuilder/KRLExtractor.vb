Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

Module KRLExtractor

    Public wordlist As New List(Of String)

    Public Sub ExtractEnumsInFiles()

        On Error Resume Next

        Dim inputFolderPath As String = "C:\PRJ\keeperrl\"
        Dim outputFileName As String = "C:\PRJ\KeeperRLCommunityResources\Apps\KRLDeveloper\ModBuilder\KRL_Enums.vb"
        Dim outputFolderPath = Path.GetDirectoryName(outputFileName)

        If Not Directory.Exists(outputFolderPath) Then
            Directory.CreateDirectory(outputFolderPath)
        End If

        ' Define the pattern to identify C++ enums in the text
        Dim enumPattern As String = "enum\s+(\w+)\s*{([^}]*)}"
        Dim content As String = ""

        ' Get a list of all files in the input folder with a .cpp or .h extension
        Dim files As String() = Directory.GetFiles(inputFolderPath, "*")
        For Each filePath As String In files
            If Not filePath.EndsWith(".exe") And Not filePath.EndsWith(".dll") And Not filePath.EndsWith(".txt") And Not filePath.EndsWith("theoraplay.h") And Not filePath.EndsWith("map_gui.h") Then
                Dim fileContent As String = File.ReadAllText(filePath)

                ' Find matches for the enum pattern in the file content
                Dim matches As MatchCollection = Regex.Matches(fileContent, enumPattern, RegexOptions.Singleline)
                Dim ClassName As String = Replace(Path.GetFileName(filePath), ".", "_")
                ClassName = Replace(ClassName, "-", "_")
                content = content + "Public Module " + ClassName + vbCrLf

                ' Process each match (enum definition) and generate VB.NET code
                For Each match As Match In matches

                    Dim enumName As String = match.Groups(1).Value
                    Dim enumBody As String = match.Groups(2).Value

                    ' Process the enum body to remove unwanted characters or formatting
                    enumBody = Regex.Replace(enumBody, "\s+", " ").Trim()

                    ' Generate the VB.NET enum code
                    Dim vbNetEnumCode As String = GenerateEnumProperties(enumBody)
                    Dim ModuleName As String = Path.Combine(outputFolderPath, $"{enumName}")

                    ' Write the VB.NET enum code to the output file
                    content = content + vbTab + "Public Enum " + enumName + vbCrLf
                    content = content + Replace(vbTab + vbTab + vbNetEnumCode, ", ", vbCrLf + vbTab + vbTab) + vbCrLf
                    content = content.Trim + vbCrLf + vbTab + "End Enum" + vbCrLf + vbCrLf

                Next

                content = content + GetProperties(fileContent)
                content = content + ConvertRichEnums(fileContent)
                content = content + GetClassProperties(fileContent)
                content = content + "End Module" + vbCrLf + vbCrLf + vbCrLf
            End If
        Next
        wordlist.Add("Boolean")
        wordlist.Add("Integer")
        wordlist.Add("List")
        content = ReplaceAsStrWithAsString(content)
        content = RemoveLinesMatchingRegex(content, "^.*Public Property \[[A-Za-z]+\(\)\] As [A-Za-z]+$")
        content = RemoveLinesMatchingRegex(content, "Public Property\s+\[[A-Za-z]+\([A-Za-z]+\)\]\s+As\s+[^\s]+")
        content = RemoveLinesMatchingRegex(content, "^.*Public Property \[[A-Za-z0-9]+\([^]]+\)\] As [A-Za-z0-9]+$")
        content = RemoveLinesMatchingRegex(content, "\([^)]*$")
        content = RemoveLinesMatchingRegex(content, "\[[^\]]*$")
        content = RemoveLinesMatchingRegex(content, "\{.*")
        content = RemoveLinesMatchingRegex(content, "\[=\]")
        content = RemoveLinesMatchingRegex(content, "\[operator\]")
        content = RemoveLinesMatchingRegex(content, "^.*Public Property \[[A-Za-z_]+\(\)\] As [A-Za-z]+$")
        content = RemoveLinesMatchingRegex(content, "\[[^\]]*\([^)]*\)[^\]]*\]")
        content = ReplaceListOfType(content)
        content = FilterLinesByText(content, "Public Property [attributes] As List(Of String)")
        content = FilterLinesByText(content, "Public Enum Dir N S E W NE NW SE SW")
        content = FilterLinesWithMismatchedBrackets(content)
        content = RemoveEmptyClasses(content)
        content = RemoveTextAfterColon(content)
        content = FilterLinesWithMismatchedBrackets(content)
        content = RemoveClassByName(content, "LastingEffects")
        content = ReplaceHashStrings(content)
        content = ReplaceSerialStrings(content)
        content = Replace(content, "As int" + vbCrLf, "As Integer" + vbCrLf)
        content = Replace(content, "As bool" + vbCrLf, "As Boolean" + vbCrLf)
        content = ReplaceVectorStrings(content)
        content = ReplaceOptionalStrings(content)
        content = ReplaceUniqueEntityStrings(content)
        content = RemoveConsecutiveDuplicateLines(content)


        File.WriteAllText(outputFileName, content)
    End Sub


    Private Function GenerateEnumProperties(enumBody As String) As String
        ' Split the enum body by commas
        Dim enumLines As String() = enumBody.Split(","c)

        ' Generate properties for the enum values
        Dim properties As New List(Of String)()
        For Each enumLine As String In enumLines
            Dim enumValue As String = enumLine.Trim()
            If Not String.IsNullOrEmpty(enumValue) Then
                properties.Add($"    {enumValue}")
            End If
        Next

        Return String.Join($"{Environment.NewLine}", properties)
    End Function

    Public Function GetProperties(cppFileContents As String) As String

        Dim content As String = ""

        ' Define a regular expression pattern to match C++ struct declarations
        Dim pattern As String = "struct\s+\w+\s*{[^}]*}"

        ' Use regex to find all struct declarations in the file
        Dim matches As MatchCollection = Regex.Matches(cppFileContents, pattern, RegexOptions.Singleline)

        ' Iterate through each match (struct declaration) and process it
        For Each match As Match In matches
            Dim structText As String = match.Value

            ' Convert the structText to a VB.NET class
            Dim vbNetClass As String = ConvertCppStructToVbClass(structText)
            content = content + vbNetClass
        Next
        Return RemoveLinesWithCharacters(content, "*<>.,/\&")
    End Function

    Public Function GetClassProperties(cppFileContents As String) As String

        Dim content As String = ""

        ' Define a regular expression pattern to match C++ struct declarations
        Dim pattern As String = "class\s+\w+\s*{[^}]*}"

        ' Use regex to find all struct declarations in the file
        Dim matches As MatchCollection = Regex.Matches(cppFileContents, pattern, RegexOptions.Singleline)

        ' Iterate through each match (struct declaration) and process it
        For Each match As Match In matches
            Dim structText As String = match.Value

            ' Convert the structText to a VB.NET class
            Dim vbNetClass As String = ConvertCppClassToVbClass(structText)
            content = content + vbNetClass
        Next
        Return RemoveLinesWithCharacters(content, "*<>.,/\&")
    End Function

    Public Function ConvertCppStructToVbClass(structText As String) As String
        ' Define a regular expression pattern to match C++ struct declarations
        Dim pattern As String = "struct\s+(\w+)\s*{([^}]*)}"

        ' Use regex to find the struct declaration in the input text
        Dim match As Match = Regex.Match(structText, pattern, RegexOptions.Singleline)

        ' Check if a match was found
        If Not match.Success Then
            Return String.Empty ' Return an empty string if no match is found
        End If

        ' Extract struct name and body
        Dim structName As String = match.Groups(1).Value
        Dim structBody As String = match.Groups(2).Value

        ' Split the struct body into individual members
        Dim members As String() = structBody.Split(";"c)
        members = members.Where(Function(member) Not String.IsNullOrWhiteSpace(member)).ToArray()

        ' Create a list to store the generated property declarations
        Dim propertyDeclarations As New List(Of String)()

        ' Generate VB.NET properties for each member of the struct
        For Each member As String In members
            ' Remove any leading or trailing whitespace
            Dim cleanedMember As String = member.Trim()

            ' Split the member into parts (type and name)
            Dim parts As String() = cleanedMember.Split({" "c}, StringSplitOptions.RemoveEmptyEntries)

            ' Ensure there are at least two parts (type and name)
            If parts.Length >= 2 Then
                Dim type As String = parts(0)
                Dim name As String = parts(1)

                ' Generate the VB.NET property declaration
                Dim propertyDeclaration As String = $"Public Property [{name}] As {type}"
                propertyDeclarations.Add(propertyDeclaration)
            End If
        Next

        ' Generate the VB.NET class with properties
        Dim vbNetClass As String = $"{String.Join(Environment.NewLine, propertyDeclarations)}{Environment.NewLine}"
        vbNetClass = ReplaceHashStrings(vbNetClass)
        vbNetClass = ReplaceSerialStrings(vbNetClass)
        vbNetClass = Replace(vbNetClass, "As int" + vbCrLf, "As Integer" + vbCrLf)
        vbNetClass = Replace(vbNetClass, "As bool" + vbCrLf, "As Boolean" + vbCrLf)
        vbNetClass = ReplaceVectorStrings(vbNetClass)
        vbNetClass = ReplaceOptionalStrings(vbNetClass)
        vbNetClass = ReplaceUniqueEntityStrings(vbNetClass)
        vbNetClass = "Public Class " + structName + vbCrLf + vbNetClass + vbCrLf + "End Class" + vbCrLf
        wordlist.Add(structName)
        Return vbNetClass
    End Function

    Public Function ConvertCppClassToVbClass(classText As String) As String
        ' Define a regular expression pattern to match C++ struct declarations
        Dim pattern As String = "class\s+(\w+)\s*{([^}]*)}"

        ' Use regex to find the struct declaration in the input text
        Dim match As Match = Regex.Match(classText, pattern, RegexOptions.Singleline)

        ' Check if a match was found
        If Not match.Success Then
            Return String.Empty ' Return an empty string if no match is found
        End If

        ' Extract struct name and body
        Dim structName As String = match.Groups(1).Value
        Dim structBody As String = match.Groups(2).Value

        ' Split the struct body into individual members
        Dim members As String() = structBody.Split(";"c)
        members = members.Where(Function(member) Not String.IsNullOrWhiteSpace(member)).ToArray()

        ' Create a list to store the generated property declarations
        Dim propertyDeclarations As New List(Of String)()

        ' Generate VB.NET properties for each member of the struct
        For Each member As String In members
            ' Remove any leading or trailing whitespace
            Dim cleanedMember As String = member.Trim()

            ' Split the member into parts (type and name)
            Dim parts As String() = cleanedMember.Split({" "c}, StringSplitOptions.RemoveEmptyEntries)

            ' Ensure there are at least two parts (type and name)
            If parts.Length >= 2 Then
                Dim type As String = parts(0)
                Dim name As String = parts(1)

                ' Generate the VB.NET property declaration
                Dim propertyDeclaration As String = $"Public Property [{name}] As {type}"
                propertyDeclarations.Add(propertyDeclaration)
            End If
        Next

        ' Generate the VB.NET class with properties
        Dim vbNetClass As String = $"{String.Join(Environment.NewLine, propertyDeclarations)}{Environment.NewLine}"
        vbNetClass = ReplaceHashStrings(vbNetClass)
        vbNetClass = ReplaceSerialStrings(vbNetClass)
        vbNetClass = Replace(vbNetClass, "As int" + vbCrLf, "As Integer" + vbCrLf)
        vbNetClass = Replace(vbNetClass, "As bool" + vbCrLf, "As Boolean" + vbCrLf)
        vbNetClass = ReplaceVectorStrings(vbNetClass)
        vbNetClass = ReplaceOptionalStrings(vbNetClass)
        vbNetClass = ReplaceUniqueEntityStrings(vbNetClass)
        vbNetClass = "Public Class " + structName + vbCrLf + vbNetClass + vbCrLf + "End Class" + vbCrLf
        wordlist.Add(structName)
        Return vbNetClass
    End Function

    Public Function ReplaceHashStrings(inputString As String) As String
        ' Define a regular expression pattern to match "HASH(str)"
        Dim pattern As String = "HASH\(([^)]+)\)"

        ' Use regex to replace all occurrences of the pattern with the captured group
        Dim replacedString As String = Regex.Replace(inputString, pattern, "$1")

        ' Return the modified string
        Return replacedString
    End Function

    Public Function ReplaceSerialStrings(inputString As String) As String
        ' Define a regular expression pattern to match "SERIAL(str)"
        Dim pattern As String = "SERIAL\(([^)]+)\)"

        ' Use regex to replace all occurrences of the pattern with the captured group
        Dim replacedString As String = Regex.Replace(inputString, pattern, "$1")

        ' Return the modified string
        Return replacedString
    End Function

    Public Function ReplaceVectorStrings(inputString As String) As String
        ' Define a regular expression pattern to match "vector<str>"
        Dim pattern As String = "vector<([^>]+)>"

        ' Use regex to replace all occurrences of the pattern with "List(Of str)"
        Dim replacedString As String = Regex.Replace(inputString, pattern, "List(Of $1)")

        ' Return the modified string
        Return replacedString
    End Function

    Public Function ReplaceOptionalStrings(inputString As String) As String
        ' Define a regular expression pattern to match "optional<str>"
        Dim pattern As String = "optional<([^>]+)>"

        ' Use regex to replace all occurrences of the pattern with "$1"
        Dim replacedString As String = Regex.Replace(inputString, pattern, "$1")

        ' Return the modified string
        Return replacedString
    End Function

    Public Function ReplaceUniqueEntityStrings(inputString As String) As String
        ' Define a regular expression pattern to match "UniqueEntity<str1>:str2"
        Dim pattern As String = "UniqueEntity<([^>]+):([^>]+)>"

        ' Use regex to replace all occurrences of the pattern with "UniqueEntity_$1_$2"
        Dim replacedString As String = Regex.Replace(inputString, pattern, "UniqueEntity_$1_$2")

        ' Return the modified string
        Return replacedString
    End Function

    Public Function RemoveLinesWithCharacters(inputString As String, charactersToCheck As String) As String

        For Each character As String In charactersToCheck.ToCharArray
            inputString = RemoveLinesWithCharacter(inputString, character)
        Next
        Return inputString

    End Function

    Public Function RemoveLinesWithCharacter(inputString As String, characterToCheck As String) As String

        ' Split the input string into lines
        Dim lines As String() = Split(inputString, vbCrLf)

        ' Filter out lines that contain the specified characters
        Dim filteredLines As IEnumerable(Of String) = lines.Where(Function(line) Not line.Contains(characterToCheck))

        ' Join the filtered lines back into a single string
        Dim resultString As String = Join(filteredLines.ToArray, Environment.NewLine)
        Return resultString
    End Function

    Public Function ReplaceAsStrWithAsString(inputString As String) As String
        ' Define the pattern to match " As str"
        Dim pattern As String = " As ([a-zA-Z_][a-zA-Z0-9_]*)"

        ' Use regex to replace occurrences of the pattern
        Dim replacedString As String = Regex.Replace(inputString, pattern, Function(match)
                                                                               Dim word As String = match.Groups(1).Value
                                                                               If wordlist.Contains(word) Then
                                                                                   Return match.Value ' Keep the original text if word is in wordList
                                                                               Else
                                                                                   Return " As String" ' Replace with " As String" if word is not in wordList
                                                                               End If
                                                                           End Function)

        ' Return the modified string
        Return replacedString
    End Function

    Public Function RemoveConsecutiveDuplicateLines(inputString As String) As String
        ' Split the input string into lines
        Dim lines As String() = inputString.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        ' Initialize a list to store unique lines
        Dim uniqueLines As New List(Of String)

        ' Iterate through the lines and add non-consecutive duplicate lines to the list
        Dim previousLine As String = Nothing
        For Each line As String In lines
            If Not String.Equals(line, previousLine) Or line.Contains("End Class") Then
                uniqueLines.Add(line)
            End If
            previousLine = line
        Next

        ' Join the unique lines back into a single string
        Dim resultString As String = String.Join(Environment.NewLine, uniqueLines)

        Return resultString
    End Function

    Public Function RemoveLinesWithWildcardPattern(inputString As String, wildcardPattern As String) As String
        ' Escape any regex metacharacters in the wildcard pattern
        Dim escapedPattern As String = Regex.Escape(wildcardPattern)

        ' Replace the wildcard character (*) with a regex pattern (.*)
        Dim regexPattern As String = "\[" & escapedPattern.Replace("\*", ".*") & "\(\)\]"

        ' Use regex to remove lines containing the pattern
        Dim removedLines As String = Regex.Replace(inputString, $"^(?s).*{regexPattern}.*$", "", RegexOptions.Multiline)

        ' Remove any empty lines resulting from the removal
        Dim cleanedString As String = String.Join(Environment.NewLine, removedLines.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))

        Return cleanedString
    End Function

    Public Function RemoveLinesMatchingRegex(inputString As String, regexPattern As String) As String
        ' Split the inputString into lines
        Dim lines As String() = inputString.Split({Environment.NewLine}, StringSplitOptions.None)

        ' Create a regex pattern object
        Dim regex As New Regex(regexPattern)

        ' Use regex to match lines and create a list of non-matching lines
        Dim nonMatchingLines As New List(Of String)()
        For Each line In lines
            If Not regex.IsMatch(line) Then
                nonMatchingLines.Add(line)
            End If
        Next

        ' Join the non-matching lines back into a single string
        Dim cleanedString As String = String.Join(Environment.NewLine, nonMatchingLines)

        Return cleanedString
    End Function

    Public Function RemoveTextAfterColon(input As String) As String
        Dim lines As String() = input.Split(Environment.NewLine)
        Dim modifiedLines As New List(Of String)()

        For Each line As String In lines
            Dim index As Integer = line.LastIndexOf(":")
            If index >= 0 Then
                ' Remove text after last ":" and add the modified line
                modifiedLines.Add(line.Substring(0, index))
            Else
                ' Keep lines without ":"
                modifiedLines.Add(line)
            End If
        Next

        ' Join the modified lines back into a single string
        Return String.Join("", modifiedLines)
    End Function

    Public Function RemoveEmptyClasses(input As String) As String
        ' Define a regular expression pattern to match empty class definitions
        Dim pattern As String = "(?s)Public\s+Class\s+\w+\s+End\s+Class"

        ' Use Regex.Replace to remove all empty class definitions
        Dim result As String = Regex.Replace(input, pattern, String.Empty)

        Return result
    End Function

    Public Function RemoveClassByName(input As String, className As String) As String
        ' Define a regular expression pattern to match the class and its content
        Dim pattern As String = $"Public Class {className}(.*?)End Class"

        ' Use Regex.Replace to remove the matched class and its content
        Dim result As String = Regex.Replace(input, pattern, String.Empty, RegexOptions.Singleline)

        Return result
    End Function

    Public Function TrimAndReplaceEndClass(input As String) As String
        ' Trim any leading or trailing white spaces
        Dim trimmedInput As String = input.Trim()

        ' Check if the input ends with "End Class"
        If trimmedInput.EndsWith("End Class") Then
            ' Remove "End Class" and add "End Module" instead
            Return trimmedInput.Substring(0, trimmedInput.Length - "End Class".Length) & "End Module"
        Else
            ' If not, return the original input
            Return trimmedInput
        End If
    End Function

    Public Function ReplaceListOfType(input As String) As String
        Dim pattern As String = "List\(Of ([A-Za-z_]+)\)"

        ' Create a regular expression object
        Dim regex As New Regex(pattern)

        ' Define a delegate for the MatchEvaluator
        Dim evaluator As MatchEvaluator = Function(match)
                                              Dim sequence As String = match.Groups(1).Value
                                              If wordlist.Contains(sequence) Then
                                                  Return match.Value ' No replacement
                                              Else
                                                  Return "List(Of String)"
                                              End If
                                          End Function

        ' Use the regular expression to replace
        Dim result As String = regex.Replace(input, evaluator)

        Return result
    End Function

    Public Function FilterLinesWithMismatchedBrackets(inputText As String) As String
        ' Split the input text into lines
        Dim lines() As String = inputText.Split({Environment.NewLine, vbCrLf, vbLf}, StringSplitOptions.None)

        ' Initialize a list to store filtered lines
        Dim filteredLines As New List(Of String)()

        ' Track the bracket counts
        Dim openBrackets As Integer = 0
        Dim squareBrackets As Integer = 0
        Dim curlyBrackets As Integer = 0

        For Each line As String In lines
            ' Iterate through each character in the line
            Dim lineChars() As Char = line.ToCharArray()
            Dim isLineValid As Boolean = True

            For Each c As Char In lineChars
                ' Check for brackets and update counts
                If c = "("c Then
                    openBrackets += 1
                ElseIf c = ")"c Then
                    openBrackets -= 1
                ElseIf c = "["c Then
                    squareBrackets += 1
                ElseIf c = "]"c Then
                    squareBrackets -= 1
                ElseIf c = "{"c Then
                    curlyBrackets += 1
                ElseIf c = "}"c Then
                    curlyBrackets -= 1
                End If

                ' If any bracket count goes negative, the line is invalid
                If openBrackets < 0 OrElse squareBrackets < 0 OrElse curlyBrackets < 0 Then
                    isLineValid = False
                    Exit For
                End If
            Next

            ' Check if all bracket counts are zero (balanced)
            If isLineValid AndAlso openBrackets = 0 AndAlso squareBrackets = 0 AndAlso curlyBrackets = 0 Then
                ' If the line is valid, add it to the filtered list
                filteredLines.Add(line)
            End If

            ' Reset bracket counts for the next line
            openBrackets = 0
            squareBrackets = 0
            curlyBrackets = 0
        Next

        ' Join the filtered lines and return as a single string
        Dim filteredText As String = String.Join(Environment.NewLine, filteredLines)

        Return filteredText
    End Function

    Public Function FilterLinesByText(inputText As String, searchText As String) As String
        Dim lines() As String = inputText.Split(New String() {Environment.NewLine, vbCrLf, vbLf}, StringSplitOptions.None)
        Dim filteredLines As New List(Of String)()

        For Each line As String In lines
            If Not line.Contains(searchText) Then
                filteredLines.Add(line)
            End If
        Next

        Return String.Join(Environment.NewLine, filteredLines)
    End Function

    Public Function FilterLinesEndingWith(inputText As String, endingText As String) As String
        Dim lines() As String = inputText.Split({Environment.NewLine, vbCrLf, vbLf}, StringSplitOptions.None)
        Dim filteredLines As New List(Of String)()

        For Each line As String In lines
            If Not line.EndsWith(endingText) Then
                filteredLines.Add(line)
            End If
        Next

        Return String.Join(Environment.NewLine, filteredLines)
    End Function

    Public Function ConvertRichEnums(input As String) As String

        If input.Contains("LastingEffect") Then
            Dim a As String = ""
        End If

        ' Define the start and end markers for RichEnums
        Const startMarker As String = "RICH_ENUM("
        Const endMarker As String = ");"

        ' Initialize the result string
        Dim result As New StringBuilder()

        ' Find the index of the start marker
        Dim startIndex As Integer = input.IndexOf(startMarker)

        ' Process RichEnums if the start marker is found
        While startIndex >= 0
            ' Find the index of the end marker
            Dim endIndex As Integer = input.IndexOf(endMarker, startIndex)

            ' If the end marker is found, extract the RichEnum content
            If endIndex >= 0 Then
                ' Extract the RichEnum content
                Dim richEnumContent As String = input.Substring(startIndex + startMarker.Length, endIndex - startIndex - startMarker.Length).Trim()

                ' Split the RichEnum content into individual enum values
                Dim enumValues As String() = Split(richEnumContent, ",", StringSplitOptions.RemoveEmptyEntries)

                ' Extract the enum name from the first line
                Dim enumName As String = enumValues(0).Trim()

                ' Create a StringBuilder to build the VB enum
                Dim vbEnumBuilder As New StringBuilder()
                vbEnumBuilder.AppendLine($"Public Enum {enumName}")

                ' Iterate through the enum values and add them to the VB enum
                For i As Integer = 1 To enumValues.Length - 1
                    ' Trim and remove any leading and trailing spaces
                    Dim trimmedValue As String = enumValues(i).Trim()
                    ' Replace any invalid characters with underscores
                    Dim validName As String = New String(trimmedValue.Where(Function(c) Char.IsLetterOrDigit(c) Or c = "_"c).ToArray())
                    ' Add the VB enum value
                    vbEnumBuilder.AppendLine($"    {validName}")
                Next

                ' Close the VB enum definition
                vbEnumBuilder.AppendLine("End Enum")

                ' Append the VB enum to the result string
                result.AppendLine(vbEnumBuilder.ToString())

                ' Find the next start marker
                startIndex = input.IndexOf(startMarker, endIndex + endMarker.Length)
            Else
                ' If no end marker is found, exit the loop
                Exit While
            End If
        End While

        Dim ret As String = RemoveLinesWithCharacters(Replace(result.ToString(), ",", vbCrLf), "*<>.,/\&")
        Return ret

    End Function


End Module
