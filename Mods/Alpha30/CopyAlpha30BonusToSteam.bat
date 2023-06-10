xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\Alpha30Bonus\version_info" "C:\KeeperRLMods\Alpha30\Alpha30Bonus\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\Alpha30Bonus\"
xcopy /s /i "C:\KeeperRLMods\Alpha30\Alpha30Bonus" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\Alpha30Bonus"
