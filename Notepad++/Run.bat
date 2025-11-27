@echo off
setlocal enabledelayedexpansion

:: Set target folder
set "TARGET_DIR=%~dp0"
set "NPP_DIR=%TARGET_DIR%npp"

:: URLs for the latest versions (update if needed)
set "NPP_URL=https://github.com/notepad-plus-plus/notepad-plus-plus/releases/download/v8.7.7/npp.8.7.7.portable.x64.zip"
set "NPP_FILE=%TARGET_DIR%npp.zip"

:: Download Notepad++ Portable if it doesn't exist
if not exist "%NPP_DIR%\Notepad++.exe" (
    echo.
    echo Downloading Notepad++ Portable...
    powershell -Command "Invoke-WebRequest -Uri '%NPP_URL%' -OutFile '%NPP_FILE%'"

    echo.
    echo Extracting Notepad++...
    powershell -Command "Expand-Archive -LiteralPath '%NPP_FILE%' -DestinationPath '%NPP_DIR%'"

    echo.
    echo Cleaning up...
    del "%NPP_FILE%"
)

:: Make sure destination folder exists
if not exist "%NPP_DIR%" mkdir "%NPP_DIR%"

:: Copy all files except the batch file itself and the npp folder
for /f "delims=" %%I in ('dir "%TARGET_DIR%" /b /a-d') do (
    if /i not "%%I"=="%~nx0" (
        copy /Y "%TARGET_DIR%%%I" "%NPP_DIR%"
    )
)

:: Copy all directories except 'npp'
for /f "delims=" %%D in ('dir "%TARGET_DIR%" /b /ad') do (
    if /i not "%%D"=="npp" (
        xcopy /E /I /Y /H "%TARGET_DIR%%%D" "%NPP_DIR%%%D"
    )
)

echo.
echo Done! Files have been copied to:
echo %NPP_DIR%

:: Only start Notepad++ if it exists
if exist "%NPP_DIR%\Notepad++.exe" (
    start "" "%NPP_DIR%\Notepad++.exe"
) else (
    echo Notepad++ was not found in %NPP_DIR%.
)

:: Optional: delete this batch file from NPP folder if it exists
if exist "%NPP_DIR%\%~nx0" del "%NPP_DIR%\%~nx0"

exit
