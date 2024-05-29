DecorationSetName=InputBox("Enter the name of your map decorator mod.")

Dim objFSO, objFile
Dim strFilePath, strContent

' Set the file path
strFilePath = CreateObject("Scripting.FileSystemObject").GetAbsolutePathName(".\keeper_creatures.txt")

' Define the content
strContent = "1_dark_mage modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "2_dark_knight modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "3_white_knight modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "4_necromancer modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "5_gnomes modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "6_dwarves modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "7_goblins modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "8_zombies modify append { buildingGroups = append { """+DecorationSetName+"""} }" & vbCrLf & _
             "9_cyclops modify append { buildingGroups = append { """+DecorationSetName+"""} }"

' Create the file and write the content
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFile = objFSO.CreateTextFile(strFilePath, True)
objFile.Write strContent
objFile.Close

strFilePath = CreateObject("Scripting.FileSystemObject").GetAbsolutePathName(".\build_menu.txt")
strContent = """"+DecorationSetName+""" {"+vbcrlf
Set objFile = objFSO.CreateTextFile(strFilePath, True)
strRootFolder = ".\Orig24"

 For Each File In objFSO.GetFolder(strRootFolder).Files
		Line="{ Furniture {{ ""<file_without_extension>"" } ""STONE"" 0} ""<file_with_spaces>"" ""<decoration_set_name> "" }"
		file_without_extension=Replace(File.Name,".png","")
		file_with_spaces=Replace(file_without_extension,"_"," ")
		Line=replace(Line,"<file_without_extension>",file_without_extension)
		Line=replace(Line,"<file_with_spaces>",file_with_spaces)
		Line=replace(Line,"<decoration_set_name>",DecorationSetName)
        strContent=strContent+Line+vbcrlf
Next
strContent=strcontent+"}"+vbcrlf
objFile.Write strContent
objFile.Close

strFilePath = CreateObject("Scripting.FileSystemObject").GetAbsolutePathName(".\tiles.txt")
Set objFile = objFSO.CreateTextFile(strFilePath, True)
strContent=""
 For Each File In objFSO.GetFolder(strRootFolder).Files
		Line="{""<file_without_extension>"" "" "" ColorId WHITE }"
		file_without_extension=Replace(File.Name,".png","")
		file_with_spaces=Replace(file_without_extension,"_"," ")
		Line=replace(Line,"<file_without_extension>",file_without_extension)
		Line=replace(Line,"<file_with_spaces>",file_with_spaces)
		Line=replace(Line,"<decoration_set_name>",DecorationSetName)
        strContent=strContent+Line+vbcrlf
Next
objFile.Write strContent
objFile.Close


strFilePath = CreateObject("Scripting.FileSystemObject").GetAbsolutePathName(".\furniture.txt")
Set objFile = objFSO.CreateTextFile(strFilePath, True)
strContent=""
 For Each File In objFSO.GetFolder(strRootFolder).Files
		Line="""<file_without_extension>"" { name = ""<file_with_spaces>"" viewId = { ""<file_without_extension>"" } canBuildOutsideOfTerritory = true }"
		file_without_extension=Replace(File.Name,".png","")
		file_with_spaces=Replace(file_without_extension,"_"," ")
		Line=replace(Line,"<file_without_extension>",file_without_extension)
		Line=replace(Line,"<file_with_spaces>",file_with_spaces)
		Line=replace(Line,"<decoration_set_name>",DecorationSetName)
        strContent=strContent+Line+vbcrlf
Next
objFile.Write strContent
objFile.Close

