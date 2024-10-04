@echo off
setlocal

:: Get the full path of the current batch file
set "currentFilePath=%~dp0"

:: Get the name of the folder containing the batch file
for %%F in ("%currentFilePath%.") do set "currentDirName=%%~nxF"

:: Display the folder name
echo The folder name containing the batch file is: %currentDirName%

:: Initialize variables
set "steamDir="
set "steamAppsDir="

:: Define common Steam installation paths as an array-like structure
set "commonPaths=C:\Program Files (x86)\Steam"
set "commonPaths2=C:\Program Files\Steam"
set "commonPaths3=D:\Steam"

:: Check common paths
for %%P in ("%commonPaths%" "%commonPaths2%" "%commonPaths3%") do (
    if exist "%%~P\steam.exe" (
        set "steamDir=%%~P"
        set "steamAppsDir=%%~P\steamapps"
        goto :found
    )
)

:: If Steam is not found, print an error message and pause
echo Error: Steam installation not found in common paths.
pause
exit /b

:found
echo Steam directory: %steamDir%
echo SteamApps directory: %steamAppsDir%

:: Set the target directory where the new folder will be created
set "targetDir=%steamAppsDir%\common\KeeperRL\mods\%currentDirName%"

:: Create the new folder
mkdir "%targetDir%" || (
    echo Error: Failed to create target directory %targetDir%.
    pause
    exit /b
)

:: Copy the contents of the current directory to the new folder
xcopy "%currentDir%*" "%targetDir%\" /E /I /Y || (
    echo Error: Failed to copy files to %targetDir%.
    pause
    exit /b
)

echo Files copied to %targetDir%.
pause
