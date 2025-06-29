@echo off
setlocal enabledelayedexpansion

:: Get current directory (where the batch file is)
set "TARGET_DIR=%~dp0"

:: Source directory (Steam installation path)
set "SOURCE_DIR=C:\Program Files (x86)\Steam\steamapps\common\KeeperRL"

echo Copying KeeperRL to "%TARGET_DIR%" excluding 'mods' folders...

:: Use robocopy to copy everything except any "mods" directories
robocopy "%SOURCE_DIR%" "%TARGET_DIR%" /E /XD mods

echo.
echo Copy complete.
pause
exit
