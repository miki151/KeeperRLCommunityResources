xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DungeonTowns\version_info" "C:\KeeperRLMods\Alpha31\A31DungeonTowns\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DungeonTowns\"
xcopy /s /i "C:\KeeperRLMods\Alpha31\A31DungeonTowns" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31DungeonTowns"
pause