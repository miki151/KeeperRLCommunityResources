xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\DigCutMonsters\version_info" "C:\KeeperRLMods\Alpha30\DigCutMonsters\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\DigCutMonsters\"
xcopy /s /i "C:\KeeperRLMods\Alpha30\DigCutMonsters" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\DigCutMonsters"
pause