xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31BonusWorking\version_info" "C:\KeeperRLMods\Alpha31\A31BonusWorking\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31BonusWorking\"
xcopy /s /i "C:\KeeperRLMods\Alpha31\A31BonusWorking" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A31BonusWorking"
