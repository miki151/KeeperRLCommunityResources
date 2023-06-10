xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DigCutMons\version_info" "C:\KeeperRLMods\Alpha31\A31DigCutMons\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DigCutMons\"
xcopy /s /i "C:\KeeperRLMods\Alpha31\A31DigCutMons" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DigCutMons"
pause