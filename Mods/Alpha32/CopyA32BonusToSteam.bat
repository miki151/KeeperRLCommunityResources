xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A32Bonus\version_info" "C:\KeeperRLMods\Alpha32\A32Bonus\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A32Bonus\"
xcopy /s /i "C:\KeeperRLMods\Alpha32\A32Bonus" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\A32Bonus"
