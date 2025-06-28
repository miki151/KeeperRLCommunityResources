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

echo.
echo Done! Git and Notepad++ have been extracted to:
echo %NPP_DIR%

pause
exit
