@echo off
setlocal

:: Get directory of the batch file
set "BASE_DIR=%~dp0"

:: Define paths
set "NPP_PATH=%BASE_DIR%notepad++\npp\notepad++.exe"
set "FALLBACK_PATH=%BASE_DIR%notepad++\LaunchKeeperRLModEditor.bat"

:: Check if Notepad++ exists
if exist "%NPP_PATH%" (
    echo Launching Notepad++ from: %NPP_PATH%
    start "" "%NPP_PATH%"
) else (
    echo Notepad++ not found. Launching fallback script: %FALLBACK_PATH%
    start "" "%FALLBACK_PATH%"
)

endlocal
