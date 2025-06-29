@echo off
setlocal enabledelayedexpansion

:: Set target folder
set "TARGET_DIR=."
set "NPP_DIR=%TARGET_DIR%\npp"

:: URLs for the latest versions (as of 2025 — update if needed)
set "NPP_URL=https://github.com/notepad-plus-plus/notepad-plus-plus/releases/download/v8.7.7/npp.8.7.7.portable.x64.zip"

:: File names
set "NPP_FILE=npp.zip"

echo.
echo Downloading Notepad++ Portable...
powershell -Command "Invoke-WebRequest -Uri '%NPP_URL%' -OutFile '%NPP_FILE%'"

echo.
echo Extracting Notepad++...
powershell -Command "Expand-Archive -LiteralPath '%NPP_FILE%' -DestinationPath '%NPP_DIR%'"

echo.
echo Cleaning up...
del "%NPP_FILE%"

@echo off
setlocal enabledelayedexpansion

:: Get the directory where the batch file is located
set "SOURCE_DIR=%~dp0"
set "DEST_DIR=%SOURCE_DIR%npp\"

:: Make sure destination folder exists
if not exist "%DEST_DIR%" (
    mkdir "%DEST_DIR%"
)

:: Loop through all files and folders except the 'npp' directory
for /f "delims=" %%I in ('dir "%SOURCE_DIR%" /b /a-d') do (
    if /i not "%%I"=="copy_to_npp.bat" (
        copy /Y "%SOURCE_DIR%%%I" "%DEST_DIR%"
    )
)

for /f "delims=" %%D in ('dir "%SOURCE_DIR%" /b /ad') do (
    if /i not "%%D"=="npp" (
        xcopy /E /I /Y /H "%SOURCE_DIR%%%D" "%DEST_DIR%%%D"
    )
)

echo All files and folders (except 'npp') copied to the 'npp' folder.
pause


echo.
echo Done! Git and Notepad++ have been extracted to:
echo %NPP_DIR%

pause
exit
