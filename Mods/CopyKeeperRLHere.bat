@echo off
setlocal enabledelayedexpansion

:: Get current directory (where the batch file is)
set "TARGET_DIR=%~dp0\"

:: Source directory (Steam installation path)
set "SOURCE_DIR=C:\Program Files (x86)\Steam\steamapps\common\KeeperRL"

:: Create target directory if it doesn't exist
if not exist "%TARGET_DIR%" (
    mkdir "%TARGET_DIR%"
)

echo Copying KeeperRL to "%TARGET_DIR%" excluding 'mods' folders...
robocopy "%SOURCE_DIR%" "%TARGET_DIR%" /E

echo.
echo Done.
pause
exit
